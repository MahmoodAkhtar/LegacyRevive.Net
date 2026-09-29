# DEV-SPEC-003 — Stage 01 Positive Discovery Commitment

Status: Implemented  
Blocked by: None  
Supersedes: None  
Superseded by: None

## Purpose

Operationalize the governed Stage 01 discovery semantics established by PROC-011 / D171 for the MVP local-directory Recovery Input path.

This slice corrects the post-DEV-SPEC-002 case where direct file discovery within a known directory succeeds, but a subsequent discovery operation for possible child-directory/descendant scopes from that same directory fails. Positively established file discoveries must remain trustworthy Stage 01 state and remain eligible for ordinary Artifact admission/preservation rather than being discarded because a later, distinct descendant-discovery operation failed.

The slice must preserve this positive state without:

- overstating supplied-source discovery completeness;
- fabricating descendants or Artifact identities for material never discovered;
- converting descendant-discovery failure into Artifact-preservation failure;
- collapsing partial discovery at the supplied root into total root discovery failure;
- treating a zero-direct-file result as proof that undiscovered descendants contain no files; or
- expanding the MVP into a filesystem graph, directory identity model, journal, or transactional filesystem snapshot.

DEV-SPEC-002 remains the historical implementation/verification record for the earlier PROC-010 / D170 enumeration-failure slice. This specification is an additive follow-up Development Slice required by PROC-011 / D171 and does not retroactively modify DEV-SPEC-002's acceptance record.

## Canonical basis

Primary canonical requirements:

- `07-recovery-process.md` §3 — Stage 01: Intake and Preservation.
  - supplied-source discovery, Artifact admission, and Artifact preservation remain distinct responsibilities;
  - the supplied artifact set is identified only to the extent actually established by discovery;
  - a positively discovered supplied file becomes trustworthy discovery state when the applicable discovery operation has successfully and completely identified that concrete file instance;
  - later failure to discover possible child-directory/descendant scopes from the same known directory does not retroactively invalidate positively established file discoveries;
  - retained positive files remain eligible for Artifact admission/preservation while affected descendant discovery becomes incomplete/Blocked;
  - the rule applies at the supplied root and within nested directories when trustworthy supplied Artifact state has been established;
  - partial root discovery remains distinct from total root discovery failure;
  - zero direct files followed by descendant-discovery failure does not by itself justify an incomplete empty Recovery Intake Snapshot;
  - Stage 01 does not require a richer filesystem-object model to represent this condition.
- `07-recovery-process.md` §§24–26 — Partial Recovery, Blocked Recovery, and failure handling.
  - trustworthy completed work remains usable;
  - failures are scoped to the narrowest reliably established boundary;
  - failures propagate only where materially relevant;
  - prefer truthful partial progress over all-or-nothing invalidation without overstating completeness.
- `12-architecture.md` §17 — checkpoints/history/replay.
  - replay-relevant state retains admitted Artifact identities, discovery completeness, and scoped discovery diagnostics/blockers for incomplete intake.
- `12-architecture.md` §18 — diagnostics and partial progress.
  - later descendant-discovery failure does not retrospectively convert positively discovered files into undiscovered state;
  - diagnostics describe the failed descendant-discovery boundary;
  - partial discovery at the supplied root remains distinguishable from total root discovery failure.
- `12-architecture.md` §20 — persistence boundary.
  - persisted state preserves Stage 01 discovery completeness and applicable discovery-failure scope/diagnostics;
  - known-empty, partially discovered/incomplete, and wholly blocked root states remain semantically distinguishable.
- `14-mvp.md` §4 — Recovery Input boundary.
  - every discovered file is inventoried;
  - discovery is literal;
  - positive direct-file discovery survives later descendant-discovery failure;
  - the rule applies at the supplied root when a trustworthy supplied Artifact subset exists;
  - successful zero-direct-file enumeration followed by descendant-discovery failure does not itself justify an incomplete empty snapshot;
  - no full filesystem graph, directory-as-Artifact model, journal, or transactional snapshot is required.
- `16-development-process.md` §§4–23 — bounded Development Slice, acceptance baseline, executable/supporting verification, qualification escalation, Definition of Done, and historical stability.

Durable decisions constraining this slice:

- D003 — Provenance Is a Core Requirement.
- D014 — Prefer Deterministic Recovery Where Practical.
- D049 — Missing Recovery Artifact Does Not Prove Historical Nonexistence.
- D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process.
- D060 — Recovery Distinguishes Workspace, Run, and Checkpoint.
- D061 — Recovery Supports Partial, Blocked, Workable, and Validated States.
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
- D170 — Intake Discovery Failure Preserves Known State Without Fabricating Artifacts.
- D171 — Positive Stage 01 File Discovery Survives Later Descendant-Discovery Failure.

Resolved qualifications with direct relevance:

- PROC-010 — Incomplete Supplied-Directory Enumeration During Intake.
- PROC-011 — Positive File Discovery Before Later Descendant-Discovery Failure.
- ARCH-014 — Local Recovery Workspace Physical Boundary.
- ARCH-015 — Original Artifact Preservation Strategy.
- DEV-001 — Canonical Development Process and Development Slice Specifications.

DEV-SPEC-001 and DEV-SPEC-002 remain directly relevant implementation/verification history sources, but they are non-canonical and do not redefine the governed semantics above.

## Scope

### Included

- Refine the Stage 01 local-directory discovery implementation so positively established file discoveries are committed independently of later descendant-discovery success for the same known directory.
- Preserve direct files successfully discovered in a directory when subsequent discovery of possible child-directory/descendant scopes from that directory fails.
- Apply the same positive-discovery rule when the directory being processed is the supplied root.
- Preserve the distinction between:
  - complete successful root discovery;
  - total root discovery failure before a trustworthy supplied Artifact subset is established;
  - partial root discovery where positive file discoveries exist but descendant discovery fails;
  - nested-directory positive file discovery followed by descendant discovery failure;
  - a zero-direct-file discovery followed by descendant discovery failure;
  - an already discovered/admitted Artifact that later fails preservation.
- Allow retained positive files to continue through existing Artifact admission/preservation behavior.
- Represent affected descendant discovery as incomplete/Blocked at the narrowest reliably established non-Artifact source/directory/subtree boundary.
- Produce a Partial Recovery Run and incomplete Recovery Intake Snapshot when trustworthy admitted Artifact state is retained and the existing canonical snapshot conditions are met.
- Prevent an incomplete empty Recovery Intake Snapshot from being justified solely by a successful zero-direct-file enumeration followed by failed descendant discovery where no other trustworthy admitted Artifact state exists.
- Preserve admitted Artifact state, discovery incompleteness, failed-scope diagnostics, run outcome, snapshot association, and checkpoint substance through persistence/reopen/replay.
- Preserve deterministic substantive state for equivalent controlled discovery results independent of incidental traversal ordering/API grouping where practical.
- Add executable acceptance verification and focused lower-level verification for the newly governed boundary.
- Preserve DEV-SPEC-001 and DEV-SPEC-002 acceptance behavior except where this follow-up is deliberately additive under D171.

### Excluded

- Rewriting, reopening, or retroactively changing the DEV-SPEC-002 acceptance contract or lifecycle history.
- Redefining PROC-010 / D170 enumeration-failure semantics beyond the accepted PROC-011 / D171 refinement.
- Stage 02 Artifact Inventory and Classification semantics beyond preserving the Stage 01 state needed for later processing.
- Artifact format/type detection, metadata/IL extraction, PDB analysis, XML/configuration analysis, package analysis, correlation, inference, reconstruction, build, validation, metrics, reporting, or export.
- Creating Artifact identities for directories, roots, failed descendant scopes, unknown entries, or files never positively discovered.
- Creating hypothetical file paths or counts beneath failed descendant-discovery scopes.
- Inferring absence, deployment completeness, corruption, or historical nonexistence from failed descendant discovery.
- Introducing a canonical directory entity, directory identity lifecycle, full filesystem graph, parent/child filesystem model, or filesystem object catalog.
- Filesystem-journal capture.
- Transactional or point-in-time source-tree snapshots.
- Guaranteeing stability of the external filesystem during intake.
- Defining general filesystem race semantics beyond the accepted Stage 01 positive-discovery and preservation boundaries.
- Symlink/reparse-point traversal policy.
- Specifying an exact recursive traversal algorithm, enumeration ordering, parallelization strategy, filesystem API call sequence, database schema/table change, class/interface name, or private adapter structure.
- Requiring one particular diagnostic type/enum design to distinguish partial-root descendant failure from total root failure; the observable semantic distinction is required, but its internal representation remains implementation design constrained by the architecture.
- A new public filesystem-crawling API.

## Acceptance contract

### AC-PDISC-001 — Positive file discovery becomes independently trustworthy

When Stage 01 successfully and completely identifies a concrete supplied file through the applicable direct-file discovery operation, that file remains trustworthy discovered state even if a later operation for possible child-directory/descendant discovery from the same known directory fails.

The later failure must not remove, forget, or retrospectively classify that positively discovered file as undiscovered.

### AC-PDISC-002 — Retained positive files continue through Artifact admission/preservation

A file retained under AC-PDISC-001 remains eligible for the existing Artifact admission and preservation behavior.

Where preservation succeeds, the Artifact is represented and preserved exactly as an ordinarily discovered supplied file under the existing DEV-SPEC-001/DEV-SPEC-002 intake semantics.

Where that admitted Artifact later fails preservation, the failure remains Artifact-scoped under the existing preservation-failure model rather than being converted into descendant-discovery failure.

### AC-PDISC-003 — Descendant-discovery failure makes discovery incomplete without erasing positive state

When descendant discovery fails after one or more positive file discoveries have been established for the same known directory:

- supplied-source discovery is not reported as Complete;
- the affected descendant-discovery scope is Blocked;
- a structured discovery diagnostic identifies the narrowest reliably established non-Artifact source/directory/subtree boundary;
- positively discovered/admitted files remain valid current recovery state; and
- the failure does not claim that undiscovered descendants are absent.

### AC-PDISC-004 — Partial root discovery is distinct from total root discovery failure

When direct-file discovery at the supplied root has positively established one or more supplied files but later child-directory/descendant discovery from the root fails:

- the positive files are retained and remain eligible for admission/preservation;
- the condition is represented as incomplete discovery rather than total root discovery failure;
- the overall Recovery Run may be Partial when trustworthy Artifact state is retained;
- an incomplete Recovery Intake Snapshot may represent the actually admitted Artifact set; and
- persisted/replayed state must remain distinguishable from a wholly Blocked root intake in which no trustworthy supplied Artifact subset was established.

The implementation must not represent this partial-root condition as a successful Complete intake.

### AC-PDISC-005 — Total root discovery failure remains Blocked

If the supplied root cannot be enumerated sufficiently to establish any trustworthy supplied Artifact subset, the existing PROC-010 / D170 root-failure semantics remain unchanged:

- the intake is Blocked;
- no Recovery Intake Snapshot is produced as though a trustworthy admitted set exists;
- known-empty success is not reported; and
- no hypothetical Artifact is created.

The D171 positive-discovery rule must not weaken this boundary.

### AC-PDISC-006 — Zero direct files do not create an incomplete empty snapshot by themselves

If direct-file discovery for a known directory successfully establishes zero direct files and descendant discovery subsequently fails, the zero-file result does not by itself establish the absence of supplied files beneath the undiscovered descendant scopes.

At the supplied root, where no other trustworthy admitted Artifact state exists, Stage 01 remains Blocked and does not produce an incomplete empty Recovery Intake Snapshot merely because direct-file enumeration returned zero files.

Where trustworthy Artifact state has already been established elsewhere in the supplied source, that existing state may remain usable under the ordinary Partial/incomplete-discovery model; the zero-file result adds no fabricated Artifact state.

### AC-PDISC-007 — Nested-directory positive state is preserved

When positive file discovery within a known nested directory succeeds and later descendant discovery from that same directory fails:

- the positively discovered files within that directory remain eligible for admission/preservation;
- safely discovered state outside that directory remains unaffected;
- the failed descendant-discovery boundary is represented as Blocked/incomplete; and
- the overall run may be Partial when trustworthy state is retained.

### AC-PDISC-008 — Incomplete snapshot and checkpoint state are replayable

For a persisted Partial intake produced under this slice, workspace reopen/replay preserves substantively equivalent:

- admitted Artifact identities and preservation state;
- supplied-source discovery incompleteness;
- failed descendant-discovery scope(s);
- structured discovery diagnostics/blockers;
- Recovery Run outcome;
- Recovery Intake Snapshot association and Artifact membership; and
- checkpoint substance relevant to the incomplete discovery.

Reopen/replay must not require re-enumerating the original supplied tree merely to reconstruct the historical intake state.

### AC-PDISC-009 — Equivalent positive-discovery outcomes are deterministic

For equivalent controlled supplied-source state that establishes the same positive files and the same descendant-discovery failure scope(s), incidental traversal order or API grouping must not change the substantive persisted result where canonical determinism is practical.

At minimum, admitted Artifact ordering/association, discovery-completeness state, failure-scope representation, diagnostics, Recovery Run outcome, and checkpoint substance must remain substantively equivalent apart from generated identities or runtime metadata for which canonical identity equality is not required.

### AC-PDISC-010 — No filesystem ontology expansion

Satisfying this slice must not require or expose canonical Artifact identities for directories, a directory entity lifecycle, a full filesystem graph, filesystem-journal state, or a transactional point-in-time filesystem snapshot.

Internal traversal structures or more precise internal failure information may be introduced where needed, but they remain implementation details unless later governance deliberately establishes broader semantics.

### AC-PDISC-011 — No unsupported descendant claims

A descendant-discovery failure must not generate:

- placeholder Artifact records;
- generated file identities;
- hypothetical descendant paths;
- inferred descendant counts or types; or
- claims that undiscovered descendants are absent.

The retained state consists only of positively established files plus truthful incompleteness/failure context.

### AC-PDISC-012 — Existing intake boundaries remain regression-safe

The implementation change must preserve the already accepted observable behavior for:

- complete non-empty discovery;
- complete known-empty discovery;
- total root discovery failure;
- narrower subtree discovery failure with safe sibling progress;
- discovered-file preservation failure;
- immutable byte snapshot/hash/integrity behavior;
- persistence/reopen/replay; and
- cancellation propagation.

Only the newly governed positive-file-before-descendant-failure case is added/refined by this slice.

## Representative scenarios

### Scenario S1 — Root files discovered, then root descendant discovery fails

Setup:

- The supplied root contains at least one readable direct file, for example `root.dll`.
- The source may contain one or more possible child directories.
- Use controlled failure injection so direct-file discovery at the root completes successfully and identifies `root.dll`, then the subsequent operation that discovers possible child-directory/descendant scopes from the root fails.

Execution:

- Execute Stage 01 intake through the ordinary application boundary.

Expected:

- `root.dll` remains positively discovered;
- it is admitted and processed through ordinary preservation;
- supplied-source discovery is Incomplete;
- the descendant-discovery failure is represented as Blocked through a structured non-Artifact diagnostic at the narrowest reliable boundary;
- the Recovery Run is Partial when the retained Artifact is trustworthy;
- an incomplete Recovery Intake Snapshot contains the admitted Artifact and does not claim complete tree discovery;
- the condition is not represented as total root discovery failure;
- no unknown descendant Artifact is invented.

Verifies:

- AC-PDISC-001
- AC-PDISC-002
- AC-PDISC-003
- AC-PDISC-004
- AC-PDISC-011

### Scenario S2 — Nested directory files discovered, then its descendant discovery fails

Setup:

- The supplied tree contains a safe file outside a nested directory.
- A known nested directory contains at least one direct file.
- Direct-file discovery for that nested directory succeeds.
- Controlled failure injection causes the later child-directory/descendant discovery operation for that nested directory to fail.

Execution:

- Execute Stage 01 intake.

Expected:

- the nested directory's positively discovered direct file is retained and preserved where readable;
- the safe file outside the directory remains unaffected;
- the failed descendant-discovery boundary is Blocked and discovery is Incomplete;
- the run is Partial;
- the incomplete snapshot contains only actually admitted Artifacts;
- unknown nested descendants are not invented.

Verifies:

- AC-PDISC-001
- AC-PDISC-002
- AC-PDISC-003
- AC-PDISC-007
- AC-PDISC-011

### Scenario S3 — Root has zero direct files, then descendant discovery fails

Setup:

- The supplied root exists.
- Direct-file discovery completes successfully and yields zero files.
- Controlled failure injection causes the later root child-directory/descendant discovery operation to fail before any other trustworthy supplied Artifact state is established.

Execution:

- Execute Stage 01 intake.

Expected:

- no Artifact is invented;
- the state is Blocked;
- no Recovery Intake Snapshot is produced as an incomplete empty admitted set;
- the result is not represented as a successfully enumerated known-empty tree;
- the diagnostic identifies the failed discovery boundary without asserting anything about undiscovered descendants.

Verifies:

- AC-PDISC-005
- AC-PDISC-006
- AC-PDISC-011

### Scenario S4 — Nested zero-file directory fails descendant discovery while other Artifact state exists

Setup:

- A supplied file elsewhere in the source is successfully discovered.
- A known nested directory has zero direct files according to a successfully completed direct-file discovery operation.
- The later descendant-discovery operation for that nested directory fails.

Execution:

- Execute Stage 01 intake.

Expected:

- the previously discovered supplied file remains eligible for admission/preservation;
- no Artifact is created for the zero-file nested directory or unknown descendants;
- discovery is Incomplete and the nested failed scope is Blocked;
- the overall run is Partial when trustworthy Artifact state is retained;
- an incomplete snapshot may represent only the actually admitted Artifact state.

Verifies:

- AC-PDISC-003
- AC-PDISC-006
- AC-PDISC-007
- AC-PDISC-011

### Scenario S5 — Reopen partial-root intake without original source tree

Setup:

- Complete S1 successfully and persist/close the workspace.
- Record admitted Artifact identity/preservation state, discovery incompleteness, failed descendant-discovery diagnostic/scope, run, snapshot, and checkpoint.
- Move or delete the original supplied directory before reopen.

Execution:

- Reopen the Recovery Workspace through the supported application boundary.

Expected:

- admitted Artifact identity/preservation state remains unchanged;
- discovery remains Incomplete;
- failed descendant-discovery context remains represented;
- the run remains Partial;
- snapshot/checkpoint associations remain stable;
- the historical partial-root state is not reclassified as total root failure or Complete discovery;
- reopen does not re-enumerate the external source to reconstruct the historical state.

Verifies:

- AC-PDISC-004
- AC-PDISC-008

### Scenario S6 — Equivalent results under different incidental operation ordering

Setup:

- Two controlled discovery executions establish the same positive file set and the same descendant-discovery failure boundary.
- Their successful file-result ordering or other incidental traversal ordering differs without changing the substantive source state.

Execution:

- Execute intake in independent workspaces with the same tool/configuration baseline.

Expected:

- both executions retain the same substantive positive file set;
- both produce equivalent Incomplete/Partial semantics and equivalent failed-scope context;
- persisted Artifact ordering/association follows the existing deterministic intake rules;
- checkpoint substance is substantively equivalent apart from generated identity/non-semantic runtime values where equality is not canonically required.

Verifies:

- AC-PDISC-009

### Scenario S7 — Existing total root failure remains unchanged

Setup:

- Use controlled failure injection that prevents the supplied root from being enumerated sufficiently to establish any positive supplied file state.

Execution:

- Execute Stage 01 intake.

Expected:

- the run remains Blocked under existing DEV-SPEC-002 behavior;
- no Recovery Intake Snapshot is produced;
- no Artifact identity is fabricated;
- D171 does not convert the case into Partial discovery because no positive supplied Artifact subset was established.

Verifies:

- AC-PDISC-005
- AC-PDISC-012

### Scenario S8 — Positive file later fails preservation

Setup:

- Direct-file discovery positively identifies a file before descendant discovery later fails.
- After the file is admitted, controlled failure injection causes its byte preservation to fail.

Execution:

- Execute Stage 01 intake.

Expected:

- the file retains its Artifact identity as an admitted instance;
- the descendant-discovery failure remains a non-Artifact discovery diagnostic;
- the file's preservation failure remains Artifact-scoped;
- the two failures are not collapsed into one another;
- no unknown descendant Artifact is created.

Verifies:

- AC-PDISC-002
- AC-PDISC-003
- AC-PDISC-011
- AC-PDISC-012

## Executable verification

Acceptance verification:

- Test framework: xUnit v3, as selected by `13-tooling-and-dependencies.md` / D130.
- Preferred verification level: application-level acceptance tests using the same Stage 01 application/use-case boundary, real workspace persistence, and real Artifact preservation where practical.
- Controlled discovery failure injection should be used to establish the exact semantic sequence required by this slice: successful direct-file discovery followed by descendant-discovery failure for the same known directory. Tests must not depend on operating-system permission behavior or incidental filesystem implementation details where deterministic failure injection is available.
- Actual repository location:
  - `tests/LegacyRevive.AcceptanceTests/Development/DevSpec003Stage01PositiveDiscoveryCommitmentTests.cs`

Scenario/acceptance mapping:

- S1 → AC-PDISC-001, 002, 003, 004, 011
- S2 → AC-PDISC-001, 002, 003, 007, 011
- S3 → AC-PDISC-005, 006, 011
- S4 → AC-PDISC-003, 006, 007, 011
- S5 → AC-PDISC-004, 008
- S6 → AC-PDISC-009
- S7 → AC-PDISC-005, 012
- S8 → AC-PDISC-002, 003, 011, 012

AC-PDISC-010 is a cross-cutting boundary demonstrated by S1–S4 and S7–S8: executable state contains Artifact identities only for positively discovered files, while failed directory/descendant boundaries remain non-Artifact diagnostics and no directory lifecycle, graph, journal, or transactional snapshot is introduced.

Acceptance tests should assert observable recovery/application state, admitted Artifact state, discovery completeness, Partial/Blocked outcome, diagnostic semantics, persistence/replay, and non-fabrication boundaries. They should not assert an exact filesystem API call sequence, private traversal data structure, database table layout, or class name unless a later implementation-specific test needs that detail only as supporting verification.

## Supporting verification

Actual lower-level verification locations:

- `tests/LegacyRevive.Domain.Tests/Recovery/IntakeStateTests.cs`
- `tests/LegacyRevive.Application.Tests/Intake/ArtifactIntakeServiceTests.cs`
- `tests/LegacyRevive.AcceptanceTests/Supporting/LocalSuppliedFileEnumeratorTests.cs`
- `tests/LegacyRevive.AcceptanceTests/Supporting/SqliteWorkspaceStoreTests.cs`
- `tests/LegacyRevive.AcceptanceTests/Supporting/LocalArtifactStoreTests.cs`
- existing DEV-SPEC-001 and DEV-SPEC-002 acceptance suites as regression verification.

Supporting verification should cover, where the implementation boundary makes it applicable:

- Discovery-adapter behavior:
  - direct-file results successfully established before later child-directory/descendant enumeration failure are retained;
  - the root and nested-directory variants are both represented correctly;
  - zero direct files plus descendant failure does not synthesize positive file state;
  - cancellation remains cancellation;
  - failure handling does not synthesize descendant paths.
- Domain/application invariants:
  - partial-root descendant-discovery failure with retained positive Artifact state is representable without becoming total root failure;
  - total root failure without trustworthy Artifact state remains snapshot-less/Blocked;
  - incomplete state cannot masquerade as Complete;
  - a zero-artifact root state produced by failed descendant discovery cannot masquerade as known-empty success;
  - discovery diagnostics do not require fabricated Artifact identities;
  - preservation failure of an already admitted file remains Artifact-scoped.
- Persistence/replay:
  - partial-root and nested partial discovery round-trip with admitted Artifact state, Incomplete discovery, Partial outcome, diagnostics, snapshot and checkpoint associations intact;
  - wholly Blocked root failure remains distinguishable from Partial root discovery;
  - known-empty Complete remains distinguishable from both;
  - reopen does not require the original external source tree to reconstruct historical intake state.
- Determinism:
  - equivalent positive discovered sets/failure boundaries produce equivalent substantive persisted state independent of incidental enumeration ordering where practical.
- Regression:
  - all DEV-SPEC-001 acceptance behavior remains green;
  - all DEV-SPEC-002 acceptance behavior remains green except that the newly governed case is now additionally verified rather than silently following the earlier traversal assumption;
  - immutable exact-byte snapshot, SHA-256 verification, workspace-owned byte storage, integrity failure cleanup, and persistence transaction/history behavior remain unchanged.

Actual verification locations replace or confirm these planned references while the specification is `In Progress`, as required by `16-development-process.md`.

## Qualifications discovered

None at specification formulation.

PROC-011 is Resolved by D171. PROC-010 remains Resolved by D170. No known unresolved qualification in the current canonical register blocks this bounded slice at specification formulation.

This specification deliberately does not settle adjacent filesystem questions that remain outside the accepted canonical basis, including:

- symlink/reparse-point traversal semantics;
- general filesystem race guarantees;
- directory identity/lifecycle semantics;
- a richer filesystem graph or journal;
- transactional filesystem snapshotting; or
- a universal rule for every possible filesystem API partial-enumeration behavior beyond the accepted positive-file/descendant-discovery boundary.

If implementation exposes a material ambiguity, conflict, unsupported assumption, boundary issue, or challenge to D170/D171 or another accepted canonical direction, register a new qualification in `91-design-qualification-register.md` before treating the issue as settled. Record the qualification reference here and block the slice where necessary.

No new material qualification was discovered during implementation or verification. The `DirectoryDescendants` diagnostic scope is a bounded internal representation of the already accepted D171 distinction and does not introduce a directory Artifact or broader filesystem ontology.

## Verification status

Acceptance verification: Satisfied by `tests/LegacyRevive.AcceptanceTests/Development/DevSpec003Stage01PositiveDiscoveryCommitmentTests.cs`; scenarios S1–S8 cover AC-PDISC-001 through AC-PDISC-012 according to the mapping above.  
Supporting verification: Satisfied by the recorded domain, application, traversal-adapter, SQLite persistence/replay, Artifact-store, determinism, cancellation, DEV-SPEC-001, and DEV-SPEC-002 verification locations.  
Blocking qualifications: None.

The canonical basis and bounded scope remain coherent. Automated acceptance and required supporting verification pass repeatably, actual verification locations are recorded, DEV-SPEC-001 and DEV-SPEC-002 acceptance remain green, architecture/tooling/evidence/MVP boundaries are respected, and no new material design qualification was exposed. The Definition of Done in `16-development-process.md` is satisfied.

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| 2026-09-29 | Created → Draft | Follow-up Development Slice formulated from the post-DEV-SPEC-002 audit after PROC-011 / D171 governance resolved positive file discovery before later descendant-discovery failure. |
| 2026-09-29 | Draft → Ready for Implementation | Canonical basis, included/excluded scope, AC-PDISC-001 through AC-PDISC-012, representative scenarios, executable/supporting verification plan, and blocker audit completed; no unresolved qualification blocks implementation. |
| 2026-09-29 | Ready for Implementation → In Progress | Readiness and blocker review completed; executable acceptance and bounded production implementation commenced against the accepted baseline. |
| 2026-09-29 | In Progress → Verification Pending | Production implementation and planned acceptance/supporting verification were complete and passing in focused verification. |
| 2026-09-29 | Verification Pending → Implemented | AC-PDISC-001 through AC-PDISC-012 and all required focused and full-solution regression verification passed; the Definition of Done was satisfied with no blocking qualification. |
