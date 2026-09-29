using LegacyRevive.Application.Intake;
using LegacyRevive.Domain.Artifacts;
using LegacyRevive.Domain.Identity;
using LegacyRevive.Domain.Recovery;
using LegacyRevive.Infrastructure.Local.Workspace;
using Microsoft.Data.Sqlite;

namespace LegacyRevive.Infrastructure.Local.Persistence;

public sealed class SqliteIntakeStateRepository(LocalWorkspaceBoundary workspaceBoundary) : IIntakeStateRepository
{
    private readonly string _databasePath = Path.Combine(workspaceBoundary.InternalStateRoot, "workspace.db");

    public async Task InitializeAsync(WorkspaceId workspaceId, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenAsync(cancellationToken);
        await CreateSchemaAsync(connection, cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO workspace (workspace_id, current_checkpoint_id) VALUES ($id, NULL);";
        command.Parameters.AddWithValue("$id", workspaceId.Value.ToString("D"));
        try
        {
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (SqliteException exception) when (exception.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException("The selected workspace has already been initialized.", exception);
        }
    }

    public async Task SaveAsync(RecoveryWorkspaceState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        await using var connection = await OpenAsync(cancellationToken);
        await CreateSchemaAsync(connection, cancellationToken);
        using var transaction = connection.BeginTransaction();
        try
        {
            await ExecuteAsync(connection, transaction,
                "INSERT INTO recovery_run (run_id, outcome, tool_identity, configuration_identity) VALUES ($run, $outcome, $tool, $configuration);",
                cancellationToken,
                ("$run", state.Run.Id.Value.ToString("D")),
                ("$outcome", (int)state.Run.Outcome),
                ("$tool", state.Run.ToolIdentity),
                ("$configuration", state.Run.ConfigurationIdentity));

            foreach (var artifact in state.Artifacts)
            {
                await ExecuteAsync(connection, transaction,
                    """
                    INSERT INTO artifact (
                        artifact_id, run_id, relative_path, capture_context, content_hash, preservation_status,
                        preserved_byte_reference, diagnostic_code, diagnostic_message)
                    VALUES ($artifact, $run, $path, $capture, $hash, $status, $reference, $diagnosticCode, $diagnosticMessage);
                    """,
                    cancellationToken,
                    ("$artifact", artifact.Id.Value.ToString("D")),
                    ("$run", artifact.RecoveryRunId.Value.ToString("D")),
                    ("$path", artifact.Provenance.RelativePath),
                    ("$capture", artifact.Provenance.CaptureContext),
                    ("$hash", artifact.ContentHash?.Value),
                    ("$status", (int)artifact.PreservationStatus),
                    ("$reference", artifact.PreservedByteReference),
                    ("$diagnosticCode", artifact.Diagnostic?.Code),
                    ("$diagnosticMessage", artifact.Diagnostic?.Message));
            }

            var orderedDiscoveryDiagnostics = state.DiscoveryDiagnostics
                .OrderBy(diagnostic => diagnostic.Scope.Kind)
                .ThenBy(diagnostic => diagnostic.Scope.RelativePath, StringComparer.Ordinal)
                .ThenBy(diagnostic => diagnostic.Code, StringComparer.Ordinal)
                .ThenBy(diagnostic => diagnostic.Message, StringComparer.Ordinal)
                .ToArray();
            for (var position = 0; position < orderedDiscoveryDiagnostics.Length; position++)
            {
                var diagnostic = orderedDiscoveryDiagnostics[position];
                await ExecuteAsync(connection, transaction,
                    """
                    INSERT INTO intake_discovery_diagnostic (
                        run_id, position, diagnostic_code, diagnostic_message, scope_kind, scope_path)
                    VALUES ($run, $position, $code, $message, $scopeKind, $scopePath);
                    """,
                    cancellationToken,
                    ("$run", state.Run.Id.Value.ToString("D")),
                    ("$position", position),
                    ("$code", diagnostic.Code),
                    ("$message", diagnostic.Message),
                    ("$scopeKind", (int)diagnostic.Scope.Kind),
                    ("$scopePath", diagnostic.Scope.RelativePath));
            }

            if (state.IntakeSnapshot is not null)
            {
                await ExecuteAsync(connection, transaction,
                    "INSERT INTO intake_snapshot (snapshot_id, run_id, discovery_completeness) VALUES ($snapshot, $run, $completeness);",
                    cancellationToken,
                    ("$snapshot", state.IntakeSnapshot.Id.Value.ToString("D")),
                    ("$run", state.IntakeSnapshot.RecoveryRunId.Value.ToString("D")),
                    ("$completeness", (int)state.IntakeSnapshot.DiscoveryCompleteness));

                for (var position = 0; position < state.IntakeSnapshot.ArtifactIds.Count; position++)
                {
                    await ExecuteAsync(connection, transaction,
                        "INSERT INTO intake_snapshot_artifact (snapshot_id, artifact_id, position) VALUES ($snapshot, $artifact, $position);",
                        cancellationToken,
                        ("$snapshot", state.IntakeSnapshot.Id.Value.ToString("D")),
                        ("$artifact", state.IntakeSnapshot.ArtifactIds[position].Value.ToString("D")),
                        ("$position", position));
                }
            }

            await ExecuteAsync(connection, transaction,
                "INSERT INTO recovery_checkpoint (checkpoint_id, run_id, snapshot_id, substance_hash) VALUES ($checkpoint, $run, $snapshot, $hash);",
                cancellationToken,
                ("$checkpoint", state.Checkpoint.Id.Value.ToString("D")),
                ("$run", state.Checkpoint.RecoveryRunId.Value.ToString("D")),
                ("$snapshot", state.Checkpoint.IntakeSnapshotId?.Value.ToString("D")),
                ("$hash", state.Checkpoint.SubstanceHash));

            await ExecuteAsync(connection, transaction,
                "UPDATE workspace SET current_checkpoint_id = $checkpoint WHERE workspace_id = $workspace;",
                cancellationToken,
                ("$checkpoint", state.Checkpoint.Id.Value.ToString("D")),
                ("$workspace", state.WorkspaceId.Value.ToString("D")));
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<RecoveryWorkspaceState> LoadCurrentAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenAsync(cancellationToken);
        await CreateSchemaAsync(connection, cancellationToken);
        await using var header = connection.CreateCommand();
        header.CommandText = """
            SELECT w.workspace_id, r.run_id, r.outcome, r.tool_identity, r.configuration_identity,
                   c.snapshot_id, c.checkpoint_id, c.substance_hash, s.discovery_completeness
            FROM workspace w
            JOIN recovery_checkpoint c ON c.checkpoint_id = w.current_checkpoint_id
            JOIN recovery_run r ON r.run_id = c.run_id
            LEFT JOIN intake_snapshot s ON s.snapshot_id = c.snapshot_id
            LIMIT 1;
            """;
        await using var reader = await header.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            throw new InvalidOperationException("The workspace does not contain a checkpointed intake run.");
        }

        var workspaceId = WorkspaceId.From(Guid.Parse(reader.GetString(0)));
        var runId = RecoveryRunId.From(Guid.Parse(reader.GetString(1)));
        var run = new RecoveryRun(runId, (RecoveryRunOutcome)reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
        var snapshotId = reader.IsDBNull(5) ? (IntakeSnapshotId?)null : IntakeSnapshotId.From(Guid.Parse(reader.GetString(5)));
        var checkpointId = RecoveryCheckpointId.From(Guid.Parse(reader.GetString(6)));
        var substanceHash = reader.GetString(7);
        var completeness = reader.IsDBNull(8)
            ? (SuppliedSourceDiscoveryCompleteness?)null
            : (SuppliedSourceDiscoveryCompleteness)reader.GetInt32(8);
        await reader.DisposeAsync();

        var artifacts = new List<OriginalArtifact>();
        if (snapshotId is not null)
        {
            await using var artifactsCommand = connection.CreateCommand();
            artifactsCommand.CommandText = """
                SELECT a.artifact_id, a.relative_path, a.capture_context, a.content_hash, a.preservation_status,
                       a.preserved_byte_reference, a.diagnostic_code, a.diagnostic_message
                FROM intake_snapshot_artifact isa
                JOIN artifact a ON a.artifact_id = isa.artifact_id
                WHERE isa.snapshot_id = $snapshot
                ORDER BY isa.position;
                """;
            artifactsCommand.Parameters.AddWithValue("$snapshot", snapshotId.Value.Value.ToString("D"));
            await using var artifactReader = await artifactsCommand.ExecuteReaderAsync(cancellationToken);
            while (await artifactReader.ReadAsync(cancellationToken))
            {
                var diagnostic = artifactReader.IsDBNull(6)
                    ? null
                    : new ArtifactDiagnostic(artifactReader.GetString(6), artifactReader.GetString(7));
                artifacts.Add(OriginalArtifact.Rehydrate(
                    ArtifactId.From(Guid.Parse(artifactReader.GetString(0))),
                    runId,
                    SuppliedArtifactProvenance.Create(artifactReader.GetString(1), artifactReader.IsDBNull(2) ? null : artifactReader.GetString(2)),
                    artifactReader.IsDBNull(3) ? null : Sha256Hash.FromHex(artifactReader.GetString(3)),
                    (ArtifactPreservationStatus)artifactReader.GetInt32(4),
                    artifactReader.IsDBNull(5) ? null : artifactReader.GetString(5),
                    diagnostic));
            }
        }

        var discoveryDiagnostics = new List<IntakeDiscoveryDiagnostic>();
        await using (var diagnosticsCommand = connection.CreateCommand())
        {
            diagnosticsCommand.CommandText = """
                SELECT diagnostic_code, diagnostic_message, scope_kind, scope_path
                FROM intake_discovery_diagnostic
                WHERE run_id = $run
                ORDER BY position;
                """;
            diagnosticsCommand.Parameters.AddWithValue("$run", runId.Value.ToString("D"));
            await using var diagnosticReader = await diagnosticsCommand.ExecuteReaderAsync(cancellationToken);
            while (await diagnosticReader.ReadAsync(cancellationToken))
            {
                discoveryDiagnostics.Add(new IntakeDiscoveryDiagnostic(
                    diagnosticReader.GetString(0),
                    diagnosticReader.GetString(1),
                    IntakeDiscoveryScope.Rehydrate(
                        (IntakeDiscoveryScopeKind)diagnosticReader.GetInt32(2),
                        diagnosticReader.GetString(3))));
            }
        }

        var snapshot = snapshotId is null
            ? null
            : new RecoveryIntakeSnapshot(
                snapshotId.Value,
                runId,
                artifacts.Select(artifact => artifact.Id).ToArray(),
                completeness ?? throw new InvalidDataException("A persisted intake snapshot is missing discovery completeness."));
        var checkpoint = new RecoveryCheckpoint(checkpointId, runId, snapshotId, substanceHash);
        return new RecoveryWorkspaceState(workspaceId, run, snapshot, checkpoint, artifacts, discoveryDiagnostics);
    }

    private async Task<SqliteConnection> OpenAsync(CancellationToken cancellationToken)
    {
        var connection = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = _databasePath,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Pooling = false
        }.ToString());
        await connection.OpenAsync(cancellationToken);
        await using var foreignKeys = connection.CreateCommand();
        foreignKeys.CommandText = "PRAGMA foreign_keys = ON;";
        await foreignKeys.ExecuteNonQueryAsync(cancellationToken);
        return connection;
    }

    private static async Task CreateSchemaAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = """
                CREATE TABLE IF NOT EXISTS workspace (
                    workspace_id TEXT PRIMARY KEY,
                    current_checkpoint_id TEXT NULL
                );
                CREATE TABLE IF NOT EXISTS recovery_run (
                    run_id TEXT PRIMARY KEY,
                    outcome INTEGER NOT NULL,
                    tool_identity TEXT NOT NULL,
                    configuration_identity TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS artifact (
                    artifact_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    relative_path TEXT NOT NULL,
                    capture_context TEXT NULL,
                    content_hash TEXT NULL,
                    preservation_status INTEGER NOT NULL,
                    preserved_byte_reference TEXT NULL,
                    diagnostic_code TEXT NULL,
                    diagnostic_message TEXT NULL,
                    CHECK ((preservation_status = 1 AND content_hash IS NOT NULL AND preserved_byte_reference IS NOT NULL)
                        OR (preservation_status <> 1 AND preserved_byte_reference IS NULL)),
                    CHECK (preservation_status <> 2 OR diagnostic_code IS NOT NULL)
                );
                CREATE TABLE IF NOT EXISTS intake_snapshot (
                    snapshot_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    discovery_completeness INTEGER NOT NULL DEFAULT 0
                );
                CREATE TABLE IF NOT EXISTS intake_snapshot_artifact (
                    snapshot_id TEXT NOT NULL REFERENCES intake_snapshot(snapshot_id),
                    artifact_id TEXT NOT NULL REFERENCES artifact(artifact_id),
                    position INTEGER NOT NULL,
                    PRIMARY KEY (snapshot_id, artifact_id),
                    UNIQUE (snapshot_id, position)
                );
                CREATE TABLE IF NOT EXISTS intake_discovery_diagnostic (
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    position INTEGER NOT NULL,
                    diagnostic_code TEXT NOT NULL,
                    diagnostic_message TEXT NOT NULL,
                    scope_kind INTEGER NOT NULL,
                    scope_path TEXT NOT NULL,
                    PRIMARY KEY (run_id, position)
                );
                CREATE TABLE IF NOT EXISTS recovery_checkpoint (
                    checkpoint_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    snapshot_id TEXT NULL REFERENCES intake_snapshot(snapshot_id),
                    substance_hash TEXT NOT NULL
                );
                """;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        if (!await HasColumnAsync(connection, "intake_snapshot", "discovery_completeness", cancellationToken))
        {
            await using var addCompleteness = connection.CreateCommand();
            addCompleteness.CommandText = "ALTER TABLE intake_snapshot ADD COLUMN discovery_completeness INTEGER NOT NULL DEFAULT 0;";
            await addCompleteness.ExecuteNonQueryAsync(cancellationToken);
        }

        if (await IsNotNullColumnAsync(connection, "recovery_checkpoint", "snapshot_id", cancellationToken))
        {
            await using var migrateCheckpoint = connection.CreateCommand();
            migrateCheckpoint.CommandText = """
                PRAGMA foreign_keys = OFF;
                BEGIN;
                ALTER TABLE recovery_checkpoint RENAME TO recovery_checkpoint_legacy;
                CREATE TABLE recovery_checkpoint (
                    checkpoint_id TEXT PRIMARY KEY,
                    run_id TEXT NOT NULL REFERENCES recovery_run(run_id),
                    snapshot_id TEXT NULL REFERENCES intake_snapshot(snapshot_id),
                    substance_hash TEXT NOT NULL
                );
                INSERT INTO recovery_checkpoint (checkpoint_id, run_id, snapshot_id, substance_hash)
                    SELECT checkpoint_id, run_id, snapshot_id, substance_hash FROM recovery_checkpoint_legacy;
                DROP TABLE recovery_checkpoint_legacy;
                COMMIT;
                PRAGMA foreign_keys = ON;
                """;
            await migrateCheckpoint.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<bool> HasColumnAsync(
        SqliteConnection connection,
        string table,
        string column,
        CancellationToken cancellationToken) =>
        await GetColumnNotNullAsync(connection, table, column, cancellationToken) is not null;

    private static async Task<bool> IsNotNullColumnAsync(
        SqliteConnection connection,
        string table,
        string column,
        CancellationToken cancellationToken) =>
        await GetColumnNotNullAsync(connection, table, column, cancellationToken) == 1;

    private static async Task<int?> GetColumnNotNullAsync(
        SqliteConnection connection,
        string table,
        string column,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = $"PRAGMA table_info({table});";
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            if (string.Equals(reader.GetString(1), column, StringComparison.Ordinal))
            {
                return reader.GetInt32(3);
            }
        }

        return null;
    }

    private static async Task ExecuteAsync(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string sql,
        CancellationToken cancellationToken,
        params (string Name, object? Value)[] parameters)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = sql;
        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
