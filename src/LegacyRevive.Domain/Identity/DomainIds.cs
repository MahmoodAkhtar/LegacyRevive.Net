namespace LegacyRevive.Domain.Identity;

public readonly record struct WorkspaceId
{
    private WorkspaceId(Guid value) => Value = RequireValue(value);
    public Guid Value { get; }
    public static WorkspaceId Create() => new(Guid.CreateVersion7());
    public static WorkspaceId From(Guid value) => new(value);
    private static Guid RequireValue(Guid value) => value != Guid.Empty ? value : throw new ArgumentException("Identity cannot be empty.", nameof(value));
}

public readonly record struct ArtifactId
{
    private ArtifactId(Guid value) => Value = RequireValue(value);
    public Guid Value { get; }
    public static ArtifactId Create() => new(Guid.CreateVersion7());
    public static ArtifactId From(Guid value) => new(value);
    private static Guid RequireValue(Guid value) => value != Guid.Empty ? value : throw new ArgumentException("Identity cannot be empty.", nameof(value));
}

public readonly record struct RecoveryRunId
{
    private RecoveryRunId(Guid value) => Value = RequireValue(value);
    public Guid Value { get; }
    public static RecoveryRunId Create() => new(Guid.CreateVersion7());
    public static RecoveryRunId From(Guid value) => new(value);
    private static Guid RequireValue(Guid value) => value != Guid.Empty ? value : throw new ArgumentException("Identity cannot be empty.", nameof(value));
}

public readonly record struct IntakeSnapshotId
{
    private IntakeSnapshotId(Guid value) => Value = RequireValue(value);
    public Guid Value { get; }
    public static IntakeSnapshotId Create() => new(Guid.CreateVersion7());
    public static IntakeSnapshotId From(Guid value) => new(value);
    private static Guid RequireValue(Guid value) => value != Guid.Empty ? value : throw new ArgumentException("Identity cannot be empty.", nameof(value));
}

public readonly record struct RecoveryCheckpointId
{
    private RecoveryCheckpointId(Guid value) => Value = RequireValue(value);
    public Guid Value { get; }
    public static RecoveryCheckpointId Create() => new(Guid.CreateVersion7());
    public static RecoveryCheckpointId From(Guid value) => new(value);
    private static Guid RequireValue(Guid value) => value != Guid.Empty ? value : throw new ArgumentException("Identity cannot be empty.", nameof(value));
}
