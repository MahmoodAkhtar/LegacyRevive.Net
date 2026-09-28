# DEV-SPEC-001 — Workspace + Local Artifact Intake

Status: Implemented
Blocked by: None  
Supersedes: None  
Superseded by: None  

## Purpose

Operationalize the first bounded MVP implementation slice for creating/opening a local Recovery Workspace and admitting a local directory tree as supplied Recovery Input through Stage 01 — Intake and Preservation.

This slice establishes durable workspace state, local artifact enumeration, stable Artifact identity, SHA-256 content identity/integrity, workspace-owned immutable Original Artifact byte preservation beneath `.legacyrevive`, provenance for supplied paths/capture context, preservation diagnostics, persisted intake state, and reopen/replay behavior.

This specification applies existing canonical requirements. It does not create or redefine Recovery Workspace, Recovery Input, Original Artifact, Artifact identity, preservation, provenance, Recovery Run, Recovery Checkpoint, replay, reconstruction, Developer-Adjusted Source, Working-Tree Divergence, or export semantics.

## Canonical basis

Primary canonical requirements:

- `07-recovery-process.md` §3 — Stage 01: Intake and Preservation.
  - identify the supplied artifact set;
  - preserve original artifacts without modification;
  - establish stable artifact identity inside the Recovery Workspace;
  - record source/path/capture context where available;
  - distinguish Recovery Input from Validation Reference;
  - record Recovery Configuration separately from Evidence;
  - produce a Recovery Intake Snapshot;
  - perform no project/source reconstruction at this stage.
- `07-recovery-process.md` §13 and applicable Recovery Workspace / Recovery Run / Recovery Checkpoint sections — material state must remain traceable and recovery must support persisted, replayable state.
- `12-architecture.md` §5.1 — Original Artifact Store.
  - workspace-owned immutable snapshots of successfully preserved supplied Original Artifact bytes;
  - stable Artifact identity;
  - SHA-256 content hash;
  - supplied path/name and capture context as provenance;
  - preservation/integrity diagnostics;
  - successful preservation only after exact-byte snapshot and verification;
  - subsequent recovery/replay uses workspace-owned preserved bytes;
  - equal hashes do not collapse distinct Artifact identities/provenance;
  - large artifact bytes remain artifact-store/filesystem backed rather than SQLite BLOBs.
- `12-architecture.md` §§17–21 — checkpoints/history/replay, structured diagnostics, persistence semantics, and deterministic externally observable behavior where practical.
- `12-architecture.md` §27 — local Recovery Workspace physical model.
  - developer-facing workspace root;
  - reserved `.legacyrevive` subtree for LegacyRevive.NET-owned internal state;
  - exact internal `.legacyrevive` layout is non-canonical;
  - developer-facing reconstruction is outside `.legacyrevive`;
  - successfully preserved supplied Original Artifact bytes are stored immutably beneath `.legacyrevive`.
- `13-tooling-and-dependencies.md` §§12, 16, 17 — SQLite/`Microsoft.Data.Sqlite` as the default local state store, UUIDv7 for generated domain identities, SHA-256 for artifact content identity/checking, and xUnit v3 as the baseline automated test framework.
- `14-mvp.md` §§2, 4, 12, 20 and 23.
  - local directory tree is the primary MVP Recovery Input source;
  - every discovered file is inventoried even without a dedicated analyzer;
  - supplied relative path/capture context is retained as provenance;
  - SHA-256 is computed;
  - exact supplied bytes are snapshotted into workspace-owned immutable storage beneath `.legacyrevive`;
  - preserved bytes/content hash are verified before preservation succeeds;
  - preserved bytes become the recovery/replay source instead of the external path;
  - unsupported but readable supplied files are still preserved and inventoried;
  - unreadable/preservation-failure conditions remain explicit through scoped diagnostics;
  - failed/incomplete snapshots are not reported as successfully preserved;
  - Artifact identity, SHA-256, supplied provenance, and storage location remain distinct;
  - Recovery Workspaces are persistent and resumable;
  - reopening must not reclassify already-known historical material merely because the process restarted;
  - replay/persistence/history are MVP release obligations.
- `16-development-process.md` §§5–22 and §26 — this slice requires a Development Slice Specification, acceptance baseline, representative scenarios, executable acceptance verification, supporting verification, governance escalation for material questions, and Definition of Done.

Durable decisions constraining this slice:

- D003 — Provenance Is a Core Requirement.
- D011 — Original Recovery Artifacts Remain Immutable.
- D014 — Prefer Deterministic Recovery Where Practical.
- D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process.
- D057 — Newly Added Artifacts Re-enter through Intake and Classification.
- D060 — Recovery Distinguishes Workspace, Run, and Checkpoint.
- D106 — Canonical Recovery State Areas Remain Logically Separated.
- D113 — Recovery Checkpoints Preserve Stable Replayable History.
- D114 — Diagnostics Support Local Failure and Partial Progress.
- D116 — Persistence Technology Is Replaceable but Must Preserve Canonical History and Provenance.
- D125 — SQLite via `Microsoft.Data.Sqlite` Is the Default Local Persistence.
- D129 — LegacyRevive-Generated IDs Use UUIDv7 and Artifacts Retain SHA-256 Content Identity.
- D130 — xUnit v3 Is the Baseline Automated Test Framework.
- D134 — MVP Uses Local Directory Recovery Input with Defined Managed Artifact Analyzers.
- D141 — MVP Recovery Workspaces Are Persistent and Resumable.
- D163 — Local Recovery Workspace Separates Internal State from the Materialized Reconstruction.
- D167 — MVP Original Artifacts Use Workspace-Owned Immutable Byte Snapshots.
- D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications.

Resolved qualifications with direct historical relevance:

- ARCH-014 — Local Recovery Workspace Physical Boundary.
- ARCH-015 — Original Artifact Preservation Strategy.
- DEV-001 — Canonical Development Process and Development Slice Specifications.

## Scope

### Included

- Create a new local Recovery Workspace at a developer-selected workspace root.
- Open an existing local Recovery Workspace.
- Establish/use the reserved `.legacyrevive` boundary for LegacyRevive.NET-owned internal state.
- Accept a local directory tree as the supplied artifact source for this intake slice.
- Enumerate supplied files from that intake source.
- Create a stable Artifact identity for each supplied file admitted as an artifact instance.
- Retain supplied relative path/name and available capture context as provenance.
- Compute SHA-256 for supplied artifact content.
- Snapshot the exact supplied bytes of each successfully preserved artifact into workspace-owned immutable artifact storage beneath `.legacyrevive`.
- Verify the preserved representation before recording preservation as successful.
- Persist artifact inventory, Artifact identity, SHA-256, supplied provenance, preservation status, preserved-byte location/reference, and applicable scoped diagnostics.
- Preserve unsupported-but-readable supplied files even when no analyzer exists for them.
- Keep preservation failures explicit and avoid committing a successful-preservation state for incomplete/failed snapshots.
- Preserve distinct Artifact identities and supplied-path provenance for distinct supplied artifact instances even when SHA-256 values are equal.
- Reopen the workspace and recover the persisted intake state without requiring the original external artifact directory to remain available after successful preservation.
- Use workspace-owned preserved bytes as the authoritative byte source for later replay-facing intake state.
- Record a Recovery Run for the intake execution and a stable Recovery Checkpoint (or equivalent checkpointed state required by the canonical checkpoint model) identifying the completed intake state.
- Produce a persisted Recovery Intake Snapshot representing the immutable recovery-input set and run context established by this slice.
- Provide executable acceptance verification and lower-level verification for the above behavior.

### Excluded

The following are canonical MVP obligations but are not implemented by DEV-SPEC-001:

- artifact format/type detection beyond what is strictly necessary to record preservation/intake state;
- Stage 02 Artifact Inventory and Classification semantics;
- managed metadata/IL extraction;
- Portable/native PDB analysis;
- XML/configuration semantic analysis;
- package/dependency metadata analysis;
- companion-artifact correlation;
- direct Observation / Recovered Fact extraction;
- evidence integration;
- convention/inference processing;
- dependency inference or reconstruction;
- assembly ownership classification;
- Candidate Project generation;
- source reconstruction/decompilation;
- materialized Candidate Solution/project/source generation;
- Working-Tree Divergence detection/adoption/discard workflow;
- Developer-Adjusted Source workflow;
- restore/build;
- validation/metrics;
- deterministic Candidate Development Baseline export;
- external enrichment;
- remote package feeds;
- external immutable-reference preservation modes;
- physical artifact-store optimizations such as deduplication, compression, content-addressed layout, hardlinks, or reflinks as product semantics.

The exact `.legacyrevive` internal directory/table layout is not specified by this DEV-SPEC. It remains a non-canonical implementation choice subject to the canonical architecture and tooling constraints.

## Acceptance contract

### AC-INTAKE-001 — Workspace boundary is established

Creating a local Recovery Workspace establishes a developer-facing workspace root with LegacyRevive.NET-owned internal state beneath the reserved `.legacyrevive` boundary.

The slice does not require a canonical internal subdirectory layout beneath `.legacyrevive`.

### AC-INTAKE-002 — Existing workspace can be reopened

A previously created workspace can be reopened through the supported application boundary and its persisted intake state can be recovered without reconstructing that state from transient process memory.

### AC-INTAKE-003 — Supplied local files receive stable Artifact identities

Each supplied file admitted during intake is represented by a stable Artifact identity that is distinct from its SHA-256 content hash, supplied path, and physical preserved-byte location.

Artifact identity uses the selected generated-identity mechanism where a GUID identity is appropriate, without presenting that generated ID as a historical identifier recovered from the subject application.

### AC-INTAKE-004 — Supplied provenance is retained

For each admitted supplied artifact, the persisted intake state retains the original supplied relative path/name and available capture context as provenance.

After successful preservation, this external location remains provenance rather than a replay availability dependency.

### AC-INTAKE-005 — SHA-256 content identity is recorded

For each successfully read supplied artifact, intake computes and persists a SHA-256 content hash representing the supplied artifact content.

The SHA-256 value complements but does not replace Artifact identity.

### AC-INTAKE-006 — Successful preservation requires verified workspace-owned bytes

An artifact is recorded as successfully preserved only when its exact supplied bytes have been snapshotted into workspace-owned immutable artifact storage beneath `.legacyrevive` and the preserved representation has been verified against the expected SHA-256 content identity.

Possession of only a hash or external path is insufficient for successful preservation.

### AC-INTAKE-007 — Preserved Original Artifact bytes are immutable recovery input

After successful preservation, subsequent access through the intake/replay-facing application abstraction returns the workspace-owned preserved bytes; reconstruction or later workspace activity must not mutate those preserved Original Artifact bytes in place.

### AC-INTAKE-008 — External source availability is not required after successful preservation

After a successful intake has completed and the workspace is reopened, the persisted intake/replay state remains usable even if the original external artifact directory has been moved, deleted, or is otherwise unavailable.

### AC-INTAKE-009 — Equal hashes do not collapse Artifact identities

Two distinct supplied artifact instances with byte-identical content and therefore equal SHA-256 hashes remain distinct Artifact identities and retain their distinct supplied-path provenance.

Any physical byte deduplication used by an implementation must not be observable as identity/provenance collapse.

### AC-INTAKE-010 — Unsupported but readable supplied files are preserved and inventoried

A supplied file that has no dedicated analyzer in the current implementation is still represented in the artifact inventory and, when readable/preservable, has its bytes preserved under the same immutable-preservation contract.

No semantic interpretation of the file is required by this proposition.

### AC-INTAKE-011 — Preservation failure is explicit and cannot masquerade as success

If byte snapshotting or integrity verification fails or remains incomplete, the artifact is not persisted/reported as successfully preserved.

The workspace retains an appropriately scoped diagnostic and preservation state sufficient for later developer action/retry without fabricating durable byte availability.

### AC-INTAKE-012 — Intake state is persistent and resumable

Artifact inventory, Artifact identities, hashes, provenance, preservation state, diagnostics, and the intake run/checkpoint state survive process termination and workspace reopening.

Reopening does not create new Artifact identities for already persisted artifact instances merely because the application restarted.

### AC-INTAKE-013 — Intake produces stable run/snapshot/checkpoint state

A completed intake execution records the applicable Recovery Run and persists a Recovery Intake Snapshot plus a stable Recovery Checkpoint (or canonical equivalent state reference) identifying the intake state sufficiently for later replay/comparison/debugging.

A later checkpoint must not rewrite the earlier checkpoint.

### AC-INTAKE-014 — Repeated replay observes the same preserved intake substance

Given the same persisted workspace intake checkpoint and replay-relevant configuration/tool baseline, replay/opening of the intake state yields substantively equivalent artifact identities, content hashes, supplied provenance, preservation statuses, and preserved-byte content apart from explicitly non-semantic runtime metadata.

### AC-INTAKE-015 — Intake does not perform reconstruction or promote preserved bytes into conclusions

Completion of this slice does not create Candidate Projects, reconstructed source, dependency conclusions, convention conclusions, or other later-stage recovery conclusions merely because artifacts were preserved.

The intake snapshot records Recovery Input/preservation state; it does not promote artifact existence into unsupported historical conclusions.

## Representative scenarios

### Scenario S1 — Create workspace and preserve a small local tree

Setup:

- A temporary supplied directory contains several readable files with different byte contents and nested relative paths.
- A new workspace root is supplied.

Execution:

- Execute the DEV-SPEC-001 intake application use case.

Expected:

- `.legacyrevive` exists as the internal workspace boundary.
- Each supplied file has a stable Artifact identity, SHA-256, supplied relative path/provenance, and successful preservation state.
- Preserved bytes exist in workspace-owned storage beneath `.legacyrevive` and verify against their SHA-256 values.
- A persisted Recovery Run, Recovery Intake Snapshot, and checkpointed intake state exist.
- No Stage 02+ recovery conclusions are produced by this slice.

Verifies:

- AC-INTAKE-001
- AC-INTAKE-003
- AC-INTAKE-004
- AC-INTAKE-005
- AC-INTAKE-006
- AC-INTAKE-013
- AC-INTAKE-015

### Scenario S2 — Reopen after deleting the original supplied directory

Setup:

- Complete S1 successfully.
- Record the resulting Artifact identities/hashes.
- Terminate/dispose the application.
- Delete or move the original supplied directory.

Execution:

- Reopen the Recovery Workspace.
- Read/replay the persisted intake state through supported application abstractions.

Expected:

- The same persisted Artifact identities/hashes/provenance are available.
- Preserved bytes remain readable from workspace-owned storage and match their recorded hashes.
- No dependency on the original external path is required for the preserved artifacts.

Verifies:

- AC-INTAKE-002
- AC-INTAKE-007
- AC-INTAKE-008
- AC-INTAKE-012
- AC-INTAKE-014

### Scenario S3 — Distinct supplied files with identical content

Setup:

- The supplied tree contains two different relative paths with byte-identical files.

Execution:

- Run intake.

Expected:

- Both artifact instances have the same SHA-256.
- They have different Artifact identities.
- Each retains its own supplied-path provenance.
- Whether the physical byte store deduplicates the bytes is outside acceptance unless identity/provenance behavior is violated.

Verifies:

- AC-INTAKE-003
- AC-INTAKE-004
- AC-INTAKE-005
- AC-INTAKE-009

### Scenario S4 — Unsupported but readable file

Setup:

- The supplied tree contains a readable file for which no dedicated analyzer exists.

Execution:

- Run intake.

Expected:

- The file is inventoried.
- It receives Artifact identity/provenance/hash state.
- Its bytes are preserved and verified when preservation succeeds.
- Intake does not invent semantic observations about its content.

Verifies:

- AC-INTAKE-003
- AC-INTAKE-005
- AC-INTAKE-006
- AC-INTAKE-010
- AC-INTAKE-015

### Scenario S5 — Controlled preservation failure

Setup:

- A controlled test storage/preservation adapter or failure injection causes snapshot completion or integrity verification to fail for one supplied file while at least one other supplied file can be preserved successfully.

Execution:

- Run intake.

Expected:

- The failing artifact is not recorded as successfully preserved.
- A scoped preservation/integrity diagnostic identifies the affected artifact/item.
- Successfully preserved artifacts retain valid state.
- The run may retain partial progress where safe rather than converting the failed artifact into a false success.

Verifies:

- AC-INTAKE-011
- AC-INTAKE-012
- AC-INTAKE-013

### Scenario S6 — Preserved bytes cannot be mutated through normal recovery use

Setup:

- Complete successful intake for an artifact.
- Record the preserved bytes/hash.

Execution:

- Exercise the supported post-intake read/replay abstractions and any normal workspace operation available in this slice.
- Attempt mutation only through whatever internal test seam is appropriate to prove the immutability boundary; do not define a public mutation capability merely for the test.

Expected:

- Normal supported operations do not modify the preserved Original Artifact bytes.
- Subsequent read verifies the same bytes/hash.

Verifies:

- AC-INTAKE-006
- AC-INTAKE-007
- AC-INTAKE-014

### Scenario S7 — Process restart does not create new identities

Setup:

- Complete successful intake.
- Persist/close the workspace.

Execution:

- Reopen it in a fresh application/process instance.

Expected:

- Existing admitted artifacts retain their Artifact identities, hashes, provenance, preservation state, and checkpoint association.
- Restart alone does not create duplicate artifact records or reclassify the already-known intake state.

Verifies:

- AC-INTAKE-002
- AC-INTAKE-012
- AC-INTAKE-014

## Executable verification

Acceptance verification:

- Test framework: xUnit v3, as selected by `13-tooling-and-dependencies.md`.
- Preferred verification level: Application-level acceptance tests using the same application use-case/port boundaries used by the production host, with real local filesystem + real SQLite persistence where practical.
- Repository location:
  - `tests/LegacyRevive.AcceptanceTests/Development/DevSpec001WorkspaceLocalArtifactIntakeTests.cs`

Coverage mapping:

- S1 → AC-INTAKE-001, 003, 004, 005, 006, 013, 015
- S2 → AC-INTAKE-002, 007, 008, 012, 014
- S3 → AC-INTAKE-003, 004, 005, 009
- S4 → AC-INTAKE-003, 005, 006, 010, 015
- S5 → AC-INTAKE-011, 012, 013
- S6 → AC-INTAKE-006, 007, 014
- S7 → AC-INTAKE-002, 012, 014

Acceptance tests should assert observable application/domain state and preserved byte behavior rather than incidental SQLite table names, private class names, or an internal artifact-store directory algorithm.

Because the exact internal `.legacyrevive` layout is non-canonical, acceptance verification may assert only that LegacyRevive.NET-owned preserved state/bytes are beneath that reserved boundary, not a particular subdirectory naming scheme unless later governance makes one canonical.

The test path remains non-canonical implementation organization and may be adjusted without changing the acceptance contract.

## Supporting verification

Lower-level verification locations:

- `tests/LegacyRevive.Domain.Tests/Artifacts/ArtifactTests.cs`
- `tests/LegacyRevive.Application.Tests/Intake/ArtifactIntakeServiceTests.cs`
- `tests/LegacyRevive.AcceptanceTests/Supporting/LocalArtifactStoreTests.cs`
- `tests/LegacyRevive.AcceptanceTests/Supporting/SqliteWorkspaceStoreTests.cs`

Lower-level verification includes, as implementation structure requires:

- Domain/unit tests:
  - Artifact identity is separate from content hash/path/storage location.
  - equal hashes do not imply equal Artifact identities.
  - preservation state transitions cannot mark success before verified snapshot completion.
- Filesystem/artifact-store tests:
  - exact-byte copy/snapshot behavior;
  - SHA-256 verification of stored bytes;
  - failure cleanup/partial-write handling;
  - immutable/read-only recovery-facing behavior;
  - workspace-owned storage remains beneath `.legacyrevive`.
- Persistence integration tests:
  - SQLite round-trip for workspace, artifact inventory, provenance, hashes, preservation state, diagnostics, Recovery Run, intake snapshot/checkpoint metadata;
  - reopen retains stable identities;
  - transaction/atomicity behavior prevents false successful-preservation commits.
- Replay/resume tests:
  - reopen after original supplied path removal;
  - same checkpoint produces substantively equivalent intake state;
  - prior checkpoint/history is not rewritten.
- Determinism tests:
  - stable substantive artifact inventory/checkpoint content for the same admitted input and baseline, independent of incidental processing order where applicable.
- Failure-path tests:
  - snapshot failure;
  - integrity-verification failure;
  - persistence failure around preservation completion;
  - one-artifact failure does not silently corrupt the state of successfully preserved artifacts.
- Host/CLI tests only where implementation exposes DEV-SPEC-001 directly through an MVP command path during this slice. The canonical `recover` command ultimately spans later stages, so this DEV-SPEC does not require the full end-to-end `recover` behavior to exist yet.

The actual test-project/file locations above replaced the planned references while the specification was `In Progress`, as required by `16-development-process.md`.

## Qualifications discovered

None at specification formulation.

No currently open, in-review, blocked, deferred, or superseded qualification in `91-design-qualification-register.md` blocks this slice.

Implementation must create/register a new qualification before treating any newly exposed material ambiguity, contradiction, unsupported assumption, boundary issue, or challenge to canonical direction as settled.

In particular, this DEV-SPEC deliberately does not settle incidental implementation details such as:

- exact `.legacyrevive` subdirectory names;
- SQLite schema/table naming;
- repository/service/class names;
- physical artifact-store addressing strategy;
- compression/deduplication/hardlink/reflink strategy;
- exact transaction decomposition;
- internal temporary-file naming;
- parallel enumeration/copy scheduling;
- exact acceptance-test project naming.

Those remain implementation choices only while they preserve the acceptance contract and canonical architecture/tooling requirements. If any choice proves to carry material product semantics or conflicts with a canonical requirement, it must be routed through `91`.

## Verification status

Acceptance verification: Satisfied by `tests/LegacyRevive.AcceptanceTests/Development/DevSpec001WorkspaceLocalArtifactIntakeTests.cs`; scenarios S1–S7 cover AC-INTAKE-001 through AC-INTAKE-015 according to the mapping above.
Supporting verification: Satisfied by the recorded domain, application, filesystem/artifact-store, SQLite persistence, replay/resume, determinism, transaction, and failure-path test locations.
Blocking qualifications: None.  

The canonical basis and bounded scope remain coherent. Automated acceptance and required supporting verification pass repeatably, actual verification locations are recorded, architecture/tooling/evidence/MVP boundaries are respected, and no material design qualification was exposed during implementation.

The slice must not transition to `Implemented` until the Definition of Done in `16-development-process.md` is satisfied, including repeatable executable acceptance verification, required supporting verification, recorded actual verification locations, no unresolved blocker, and any governance change exposed during implementation having completed its synchronization lifecycle.

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| 2026-09-27 | Created → Draft | Initial DEV-SPEC-001 formulated from current canonical workspace/intake/preservation requirements after DEV-001 resolution. |
| 2026-09-27 | Draft → Ready for Implementation | Canonical basis, bounded scope, acceptance contract, representative scenarios, executable-verification approach, and supporting-verification plan established; no current governance blocker identified. |
| 2026-09-27 | Ready for Implementation → In Progress | Readiness and blocker audit completed; executable acceptance and production implementation commenced against the accepted baseline. |
| 2026-09-27 | In Progress → Verification Pending | S1–S7 acceptance implementation and required domain, filesystem, persistence, replay, determinism, and failure-path support were complete and passing in development verification. |
| 2026-09-27 | Verification Pending → Implemented | Complete Release verification passed for all acceptance and supporting suites; the Definition of Done was satisfied with no blocking qualification. |
