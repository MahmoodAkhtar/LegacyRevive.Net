# DEV-SPEC-004 — Stage 02 Artifact Format and Family Classification

Status: Rejected  
Blocked by: None  
Supersedes: None  
Superseded by: None

## Purpose

Operationalize the first bounded Stage 02 capability of the canonical Recovery Process for the MVP local-directory Recovery Input path.

For each Stage 01 Artifact that has a successfully preserved workspace-owned byte representation, LegacyRevive.NET must inspect that preserved representation and establish a durable Artifact format/family classification result representing what can actually be established about that Artifact instance.

The slice must:

- classify from the preserved Artifact instance rather than from filename extension alone;
- preserve uncertainty, ambiguity, unsupportedness, malformed/corrupt input, and classification failure explicitly;
- persist classification as later recovery progress without rewriting Stage 01 historical state;
- remain replayable after the original supplied source tree is unavailable;
- preserve existing Artifact identity, provenance, hash, and preservation state; and
- avoid crossing into Stage 03 direct extraction, evidence integration, inference, reconstruction, or ownership classification.

This slice establishes the first material Stage 01 → Stage 02 hand-off.

It does not implement the complete MVP Stage 03 analyzer set or deeper semantic extraction for every supported Artifact family. It does, however, establish Stage 02 classification/routing coverage for the MVP Artifact families explicitly listed in this specification.

## Canonical basis

Primary canonical requirements:

- `06-recovery-matrix.md` — Recovery Matrix and Artifact-instance capability.
  - actual recovery capability is established from inspection of the Artifact instance rather than filename extension alone;
  - extension is insufficient to establish actual Artifact format/type;
  - managed implementation assemblies, reference assemblies, native/non-managed executables or libraries, configuration, PDBs, XML documentation, dependency/runtime manifests, satellite/resource assemblies, and package metadata are materially distinct Artifact families;
  - an actual recovery run produces Artifact-Instance Profiles describing what was actually established for the supplied Artifact instance rather than merely reproducing the capability-level Recovery Matrix.
- `07-recovery-process.md` §4 — Stage 02: Artifact Inventory and Classification.
  - record Artifact identity/path;
  - detect actual Artifact format/type;
  - distinguish filename/extension from actual format;
  - classify Artifact family using the Recovery Matrix;
  - preserve corrupt, unreadable, unsupported, and ambiguous conditions explicitly;
  - identify possible companion relationships without silently accepting them;
  - produce the Stage 02 portion of Artifact-Instance Profiles for later recovery stages.
- `12-architecture.md` — workspace ownership, analyzer boundaries, checkpoints/history/replay, and persistence.
  - subsequent recovery operates from workspace-owned preserved Original Artifact bytes;
  - the original supplied external path becomes provenance rather than an ongoing operational dependency after successful preservation;
  - analyzers/adapters return direct observations/diagnostics and do not silently perform inference;
  - recovery history/checkpoints remain replayable and later progress does not rewrite earlier historical conclusions.
- `13-tooling-and-dependencies.md` — managed metadata tooling baseline.
  - `System.Reflection.Metadata` is the accepted primary managed PE/metadata inspection mechanism where managed-format inspection requires it;
  - external/concrete tooling remains behind accepted inward dependency boundaries.
- `14-mvp.md` — first-release artifact coverage and workspace persistence.
  - every discovered file is inventoried;
  - the MVP identifies and analyzes defined Artifact families;
  - required Artifact coverage includes managed `.dll`/`.exe`, configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nupkg`, `.nuspec`, and satellite/resource assemblies;
  - unsupported but preserved content remains visible;
  - recovery workspaces are persistent and resumable rather than requiring reclassification merely because the process restarted.
- `16-development-process.md` — bounded Development Slice, acceptance baseline, executable/supporting verification, readiness assessment, qualification escalation, Definition of Done, and historical stability.

Durable decisions constraining this slice include:

- D003 — Provenance Is a Core Requirement.
- D014 — Prefer Deterministic Recovery Where Practical.
- D047 — Actual Recovery Capability Is Established from Artifact-Instance Inspection.
- D048 — Filename Extension Alone Is Insufficient to Establish Artifact Format.
- D049 — Missing Recovery Artifact Does Not Prove Historical Nonexistence.
- D051 — Artifact-Instance Profiles Are Distinct from the Recovery Matrix.
- D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process.
- D060 — Recovery Distinguishes Workspace, Run, and Checkpoint.
- D061 — Recovery Supports Partial, Blocked, Workable, and Validated States.
- D106 — Canonical Recovery State Areas Remain Logically Separated.
- D107 — Analyzer Outputs Preserve Epistemic Boundaries and Do Not Silently Infer.
- D113 — Recovery Checkpoints Preserve Stable Replayable History.
- D116 — Persistence Technology Is Replaceable but Must Preserve Canonical History and Provenance.
- D119 — Managed Metadata Inspection Uses the Accepted Metadata Tooling Baseline.
- D125 — SQLite via `Microsoft.Data.Sqlite` Is the Default Local Persistence.
- D129 — LegacyRevive-Generated IDs Use UUIDv7 and Artifacts Retain SHA-256 Content Identity.
- D130 — xUnit v3 Is the Baseline Automated Test Framework.
- D134 — MVP Uses Local Directory Recovery Input with Defined Managed Artifact Analyzers.
- D141 — MVP Recovery Workspaces Are Persistent and Resumable.
- D163 — Local Recovery Workspace Separates Internal State from Materialized Reconstruction.
- D167 — MVP Original Artifacts Use Workspace-Owned Immutable Byte Snapshots.
- D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications.
- D170 — Intake Discovery Failure Preserves Known State Without Fabricating Artifacts.
- D171 — Positive Stage 01 File Discovery Survives Later Descendant-Discovery Failure.

DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 remain historical implementation/verification sources for Stage 01 and do not redefine the canonical requirements above.

## Existing implementation baseline

The repository entering this slice has implemented DEV-SPEC-001 through DEV-SPEC-003.

The current implementation establishes:

- stable `ArtifactId` identity for admitted supplied files;
- supplied provenance distinct from content identity and workspace storage identity;
- SHA-256 content identity for successfully preserved Artifacts;
- workspace-owned immutable preserved-byte references for successfully preserved Artifacts;
- explicit preservation failure with no preserved-byte reference when capture fails;
- replay/reopen from workspace-owned state without requiring the original supplied source tree;
- `Completed`, `Partial`, and `Blocked` Stage 01 outcomes;
- trustworthy preserved Artifacts within a `Partial` Stage 01 run;
- Stage 01 discovery diagnostics distinct from Artifact-scoped preservation diagnostics;
- deterministic substantive Stage 01 state where practical;
- durable SQLite persistence/checkpoint state across process restart.

The pre-DEV-SPEC-004 verification baseline is:

- 65 total tests;
- 65 passed;
- 0 failed;
- 0 skipped.

DEV-SPEC-004 must preserve all existing DEV-SPEC-001 through DEV-SPEC-003 acceptance behavior unless a later governed decision explicitly changes it.

## Repository transition constraint

The current implementation is intentionally Stage-01-shaped.

In particular:

- `OriginalArtifact` currently represents supplied Artifact identity, provenance, preservation state, content hash, and workspace-owned preserved-byte reference;
- `RecoveryWorkspaceState` currently represents the Stage 01 Recovery Run, Recovery Intake Snapshot, Recovery Checkpoint, admitted Artifacts, and intake-discovery diagnostics; and
- `IIntakeStateRepository` / `SqliteIntakeStateRepository` currently persist that intake-oriented state.

DEV-SPEC-004 introduces the first durable post-intake recovery knowledge.

Implementation must therefore treat Stage 02 classification as later recovery state associated with an existing `ArtifactId`, rather than casually extending Stage 01 preservation state as though classification had been known at intake time.

In particular, an implementation must not satisfy this specification merely by:

- adding classification fields directly to the historical Stage 01 representation and rewriting that history in place;
- treating `ArtifactPreservationStatus` as a classification lifecycle;
- rewriting the original Recovery Intake Snapshot so that it appears to have contained Stage 02 knowledge;
- replacing the Stage 01 Recovery Checkpoint with a classification-enriched version of the same historical state; or
- changing Artifact identity merely because more knowledge has been established about the Artifact.

The exact Domain types, repository abstractions, SQLite schema, checkpoint persistence mechanism, and class/interface names remain implementation details.

Whatever implementation design is chosen must preserve the canonical distinctions between:

- Artifact preservation and Artifact classification;
- Stage 01 historical state and later Stage 02 recovery knowledge;
- Original Artifact identity/provenance and later knowledge about that Artifact; and
- earlier and later Recovery Checkpoints.

This constraint guides implementation without prescribing a premature physical persistence design.

## Scope

### Included

- Determine which admitted Stage 01 Artifacts are eligible for Stage 02 content classification.
- Read successfully preserved workspace-owned Original Artifact bytes through an inward-owned application capability.
- Detect actual Artifact format/type from the Artifact instance rather than extension alone.
- Assign the most specific supported Recovery Matrix Artifact family that can be directly established within Stage 02.
- Represent Stage 02 classification as durable recovery state associated with the existing `ArtifactId`.
- Preserve explicit classification outcomes for:
  - directly classified/supported Artifact families;
  - unsupported or unrecognized content;
  - ambiguous classification;
  - malformed/corrupt candidate content; and
  - operational classification/read failure.
- Preserve Stage 01 Artifact identity, provenance, SHA-256 content identity, preserved-byte reference, and preservation result unchanged.
- Allow successfully preserved Artifacts from a `Partial` Stage 01 Recovery Run to proceed to Stage 02.
- Exclude failed-preservation Artifacts from content classification when no trustworthy preserved representation exists.
- Persist Stage 02 classification state and later recovery/checkpoint progress.
- Reopen/replay classification without requiring the original supplied source tree.
- Preserve deterministic substantive classification for equivalent preserved inputs and relevant configuration where practical.
- Add executable acceptance verification and focused Domain/Application/adapter/persistence verification.
- Preserve DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 behavior.

### Excluded

- CLR type extraction.
- Member/method extraction.
- Method-body or IL recovery.
- Assembly reference/dependency extraction.
- Assembly ownership classification such as `ApplicationOwned`, `InternalDependency`, `ThirdParty`, or `Framework`.
- Candidate Project creation or project-boundary inference.
- PDB document/sequence-point extraction.
- XML documentation member/text extraction.
- Semantic configuration extraction or interpretation.
- WCF configuration recovery beyond any minimum structural inspection strictly required to classify the current Artifact family.
- `.deps.json` dependency extraction.
- `.runtimeconfig.json` runtime-property extraction.
- NuGet dependency/content analysis.
- Resource extraction.
- Cross-Artifact evidence integration.
- Identifying or correlating possible companion-Artifact relationships as a distinct Stage 02 capability; that canonical Stage 02 obligation is deliberately deferred to a later bounded slice.
- Establishing companion relationships as accepted facts merely because names/extensions appear related.
- Convention inference.
- Developer intervention.
- Source reconstruction or decompilation.
- Build attempts.
- Validation.
- Recovery metrics/reporting/export.
- Full filesystem graph reconstruction.
- Directory Artifact identities.
- Treating filename extension as historical fact about actual Artifact format.
- Persisting Stage 03 observations merely because the Stage 02 parser happens to expose them.
- Prescribing exact private class names, parser layout, SQLite table names, or one mandatory physical persistence design.

## Stage 02 eligibility

A Stage 01 Artifact is eligible for Stage 02 content classification when:

- it is an admitted Artifact in current trustworthy Stage 01 state;
- its preservation status is `Preserved`;
- it has a verified SHA-256 content hash; and
- it has a workspace-owned preserved-byte reference.

Eligibility is Artifact-scoped.

A Stage 01 `Partial` Recovery Run does not prevent successfully preserved Artifacts within that run from proceeding to Stage 02.

An admitted Artifact whose preservation failed is not eligible for content classification because LegacyRevive.NET does not possess a trustworthy preserved byte representation for that Artifact.

Such an Artifact must not receive a fabricated content classification such as `Unknown` merely to make Stage 02 appear complete.

Stage 01 discovery incompleteness also does not imply anything about undiscovered material. Stage 02 operates only on admitted Artifacts actually established by Stage 01.

## Classification semantics

### Artifact identity remains stable

Stage 02 enriches recovery knowledge about an existing Artifact.

It does not create a replacement Artifact identity merely because additional information has been established.

The Artifact's identity, supplied provenance, content hash, preserved-byte reference, and Stage 01 preservation result remain historically stable.

### Preservation and classification remain distinct

Preservation answers whether LegacyRevive.NET successfully captured a trustworthy workspace-owned representation of the supplied Artifact.

Classification answers what can directly be established about the format/family of that preserved Artifact instance.

These are separate lifecycle concerns and must remain separately representable.

### Actual format is not filename extension

Filename/path/extension may be retained as provenance/context and may assist parser routing.

They must not by themselves establish actual Artifact format.

Examples include:

- a native PE named `legacy.dll` must not become a managed assembly because of `.dll`;
- arbitrary bytes renamed to `fake.dll` must not become a managed assembly;
- a valid structured Artifact with a misleading extension remains classifiable according to its actual bytes/structure where Stage 02 can establish that format; and
- a configuration-looking filename must not establish valid XML/configuration where content inspection fails.

### Most specific directly supportable classification

Stage 02 records the most specific Artifact format/family that can be directly established without relying on later inference.

Where the available direct inspection establishes only a broader format, the result remains at that broader level.

The implementation must not manufacture specificity merely for completeness.

### Required family distinctions and DEV-SPEC-004 coverage

DEV-SPEC-004 must establish Stage 02 classification/routing coverage for the MVP Artifact families below where the Artifact instance directly supports the distinction:

- managed implementation assembly;
- reference assembly;
- native/non-managed PE executable or library;
- classic XML configuration;
- Portable PDB / supported PDB family;
- XML documentation Artifact;
- `.deps.json` dependency-context Artifact;
- `.runtimeconfig.json` runtime-configuration Artifact;
- package metadata such as `packages.config`;
- NuGet package/container metadata such as `.nupkg` / `.nuspec`;
- satellite/resource assembly; and
- unsupported/unrecognized preserved content.

This is classification/routing coverage only. It does not require the Stage 03 semantic analyzers for those families to be implemented in this slice. For example, recognizing a `.deps.json` Artifact does not extract its dependency graph, and recognizing XML documentation does not extract documented members/text.

This specification does not require one parser class or enum value per bullet. The durable representation must nevertheless preserve the material distinctions needed for later analyzer routing.

### Ambiguity

Where more than one materially different classification remains plausible and Stage 02 cannot directly disambiguate it, ambiguity must remain explicit.

The implementation must not choose one candidate merely to avoid an unresolved state.

### Unsupported/unrecognized content

Successfully preserved readable content that is not recognized or supported remains a valid Artifact.

Its classification result must retain an explicit unsupported/unrecognized state.

It must not be discarded or rewritten as a Stage 01 preservation failure.

### Malformed/corrupt candidate content

If content reaches a known format/parser check but cannot satisfy the structural requirements necessary to establish that format, Stage 02 must preserve an explicit non-success diagnostic/result.

The implementation must distinguish, where materially possible:

- unsupported/unrecognized content;
- recognized candidate content that is malformed/corrupt; and
- operational failure while attempting classification.

No malformed Artifact may produce fabricated successful classification findings.

### No Stage 03 leakage

A classification adapter may inspect internal structures necessary to answer the Stage 02 classification question.

It must not persist unrelated Stage 03 findings merely because a parser exposes them conveniently.

For managed PE inspection, for example, DEV-SPEC-004 may inspect PE/CLR metadata necessary to establish managed/reference/satellite classification, but it does not persist discovered types, methods, assembly references, method bodies, source mappings, or unrelated metadata as part of this slice.

## Durable classification state

Stage 02 classification must be durable and associated with the existing `ArtifactId`.

The durable state must preserve enough information to establish:

- which Artifact was classified;
- classification status;
- detected format/family where established;
- any ambiguity required to represent the result truthfully;
- applicable diagnostics; and
- replay-relevant tool/configuration identity where required by canonical checkpoint semantics.

Classification state must not be encoded solely by mutating Stage 01 preservation fields.

Exact type names, table names, serialization details, repository names, and migration mechanics remain implementation design choices constrained by the canonical model.

## Checkpoint and history behavior

Stage 01 historical state must remain stable.

DEV-SPEC-004 must not:

- rewrite the existing Stage 01 Recovery Checkpoint;
- rewrite the original Recovery Intake Snapshot to make it appear to contain Stage 02 knowledge;
- replace Artifact identities;
- change Stage 01 preservation outcomes because classification later failed or was unsupported; or
- mutate earlier recovery history merely because classification has now occurred.

Stage 02 establishes materially later recovery state.

The resulting current recovery state must therefore identify later classification-bearing progress while preserving prior checkpoint/history state.

The exact physical persistence shape is not prescribed by this specification.

## Preserved-byte access

Stage 02 must inspect the workspace-owned preserved representation.

The original supplied external path must not be reopened as the operational classification source after successful preservation.

The current Application-owned preserved-byte access boundary may be reused or evolved where necessary.

The implementation should not require eagerly loading every Artifact into memory merely because an existing convenience API can materialize `byte[]`. Streaming/read access should be preferred where the classification technology supports it.

Any evolution of preserved-byte access must retain:

- workspace-bound path safety;
- immutable Original Artifact semantics;
- cancellation behavior;
- integrity expectations; and
- inward dependency direction.

## Application behavior

The Application layer coordinates Stage 02 classification.

It must:

1. load/open the applicable current recovery state;
2. identify eligible successfully preserved Artifacts;
3. classify eligible Artifacts through application-owned capability boundaries;
4. preserve explicit non-success classification outcomes rather than silently dropping them;
5. combine results deterministically where practical;
6. persist Stage 02 recovery progress;
7. establish later checkpoint/current-state progress without rewriting Stage 01 history; and
8. expose classification-bearing recovery state through an Application-level API.

The Application layer must not contain concrete parser/tool implementation details that belong in adapters.

Stage 02 should remain separate from `ArtifactIntakeService` rather than turning intake into a combined intake-and-classification operation.

## Acceptance contract

### AC-CLASS-001 — Successfully preserved Artifacts are eligible

Every admitted Artifact with successful Stage 01 preservation is eligible for Stage 02 classification regardless of whether the Stage 01 Recovery Run outcome is `Completed` or `Partial`.

### AC-CLASS-002 — Failed preservation does not fabricate classification

An admitted Artifact whose preservation failed and therefore has no trustworthy preserved-byte representation is not content-classified.

The implementation must not create a fabricated `Unknown`, unsupported, or other byte-derived classification merely to produce a Stage 02 record.

### AC-CLASS-003 — Classification uses workspace-owned preserved bytes

Stage 02 classification operates from the workspace-owned preserved representation.

After successful Stage 01 preservation, deleting the original supplied source tree must not prevent classification or later reopen/replay of established classification state.

### AC-CLASS-004 — Extension does not establish actual format

A misleading filename or extension must not force the corresponding Artifact classification.

Actual Artifact inspection must take precedence over extension assumptions.

### AC-CLASS-005 — Managed and non-managed PE remain distinct

A managed PE Artifact and a native/non-managed PE Artifact must not receive the same managed classification merely because both use `.dll` or `.exe` naming.

### AC-CLASS-006 — Required MVP routing families are distinguishable where directly establishable

Stage 02 classification can represent the required MVP routing families listed under **Required family distinctions and DEV-SPEC-004 coverage** where the Artifact instance directly establishes them.

This includes preserving materially distinct managed families such as implementation, reference, and satellite/resource assemblies, and distinguishing supported configuration, symbol, documentation, runtime-manifest, and package/container families without performing their Stage 03 semantic extraction.

### AC-CLASS-007 — Unsupported/unrecognized preserved content remains visible

A successfully preserved but unsupported/unrecognized Artifact remains inventoried and receives explicit Stage 02 unsupported/unrecognized state.

It is not discarded and is not rewritten as a preservation failure.

### AC-CLASS-008 — Malformed/corrupt candidate content is explicit

Malformed/corrupt candidate content produces an explicit classification diagnostic/result and no fabricated successful format/family classification.

### AC-CLASS-009 — Ambiguity remains explicit

Where Stage 02 cannot directly choose between materially different plausible classifications, the classification state retains that ambiguity rather than selecting a candidate without sufficient evidence.

### AC-CLASS-010 — Artifact identity/provenance/preservation remain unchanged

Stage 02 classification does not alter the Artifact's:

- `ArtifactId`;
- supplied relative-path provenance;
- capture context;
- SHA-256 content identity;
- preserved-byte reference; or
- Stage 01 preservation status/diagnostic.

### AC-CLASS-011 — Classification persists across reopen

Successfully committed Stage 02 classification survives process restart and can be reopened without access to the original supplied source tree.

### AC-CLASS-012 — Stage 01 checkpoint/history is not rewritten

Committing Stage 02 progress does not rewrite or replace the historical Stage 01 Recovery Checkpoint/Recovery Intake Snapshot as though classification had been known during intake.

Earlier history remains stable while later classification-bearing state becomes current according to canonical checkpoint semantics.

### AC-CLASS-013 — Classification is substantively deterministic

Equivalent preserved Artifact bytes, relevant provenance/context, tooling baseline, and Stage 02 configuration produce equivalent substantive classification results independent of incidental Artifact processing order where canonical determinism is practical.

Generated identities or other non-semantic runtime metadata need not match where canonical identity rules do not require equality.

### AC-CLASS-014 — Partial Stage 01 state flows forward truthfully

A trustworthy positively discovered and successfully preserved Artifact retained by DEV-SPEC-002/DEV-SPEC-003 partial-discovery behavior remains classifiable even though another source subtree or descendant scope was not completely discovered.

Stage 02 must preserve the historical Stage 01 incompleteness and must not synthesize Artifacts or classifications for undiscovered material.

### AC-CLASS-015 — Classification failure remains Artifact-scoped where possible

An operational classification failure for one eligible Artifact must not erase already established trustworthy classification state for unrelated Artifacts.

The failure remains scoped to the narrowest reliably established Artifact/classification boundary and must not fabricate successful classification.

### AC-CLASS-016 — Stage 03 knowledge is not persisted by this slice

DEV-SPEC-004 does not persist unrelated CLR metadata, configuration semantics, dependency data, source mappings, cross-Artifact conclusions, inference, reconstruction, or other later-stage knowledge merely because the classification implementation can technically access it.

### AC-CLASS-017 — Existing Stage 01 acceptance remains green

All existing DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 acceptance behavior remains passing after DEV-SPEC-004 implementation.

## Representative scenarios

### S1 — Managed implementation assembly

Given a successfully preserved managed implementation assembly, when Stage 02 runs, then the Artifact is classified from its preserved bytes as the directly established managed implementation family; Artifact identity/provenance/preservation remain unchanged; and no unrelated CLR type/member extraction is persisted.

### S2 — Native PE with `.dll` extension

Given a native/non-managed PE file named `legacy.dll`, when Stage 02 runs, then the `.dll` extension does not make it a managed assembly and the native/non-managed PE distinction is retained where directly established.

### S3 — Misleading `.dll` extension

Given arbitrary non-PE content renamed to `fake.dll`, when Stage 02 runs, then the Artifact is not classified as managed solely from its extension and receives only the content-supported classification outcome.

### S4 — Reference assembly

Given a preserved assembly whose reference-assembly nature is directly establishable, when Stage 02 runs, then that Artifact remains distinguishable from an ordinary implementation assembly.

### S5 — Satellite/resource assembly

Given a recognizable satellite/resource assembly, when Stage 02 runs, then its resource/satellite family remains representable and it is not treated as application implementation code solely because it uses `.dll`.

### S6 — MVP structured routing families

Given representative valid preserved Artifacts for the MVP Stage 02 routing families—including classic XML configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, and `.nupkg`—when Stage 02 runs, then each directly established Artifact family is classified distinctly enough for later analyzer routing while its deeper semantic extraction remains outside this slice.

### S7 — Unsupported readable Artifact

Given successfully preserved arbitrary readable bytes with no supported/recognized classification, when Stage 02 runs, then the Artifact remains preserved and receives explicit unsupported/unrecognized classification state without fabricated semantic conclusions.

### S8 — Malformed/corrupt candidate

Given content that reaches a known format check but cannot satisfy the necessary structural requirements, when Stage 02 runs, then a diagnostic/non-success classification result is retained and no successful format/family finding is fabricated.

### S9 — Reopen after original source tree is removed

Given successful Stage 01 preservation and committed Stage 02 classification, when the original supplied directory is deleted and the workspace is reopened, then Artifact identity and Stage 01 state remain intact, Stage 02 classification remains available, and no original external Artifact path is operationally required.

### S10 — Partial Stage 01 discovery with surviving Artifact

Given a DEV-SPEC-003-style partial Stage 01 run containing one positively discovered and successfully preserved Artifact plus a descendant-discovery failure, when Stage 02 runs, then the preserved Artifact is classified, the historical incomplete-discovery diagnostic remains intact, and no Artifact/classification is synthesized for the undiscovered descendants.

### S11 — Preservation failure beside eligible Artifact

Given one successfully preserved Artifact and one admitted Artifact whose preservation failed, when Stage 02 runs, then the preserved Artifact is eligible for classification, the failed Artifact receives no fabricated byte-derived classification, and its Stage 01 preservation diagnostic remains intact.

### S12 — Equivalent processing order

Given equivalent eligible Artifact sets presented to Stage 02 in different processing orders, when classification completes, then substantive classification state and any persisted ordering that is semantically material remain deterministic where practical.

### S13 — Historical Stage 01 checkpoint remains stable

Given an existing Stage 01 Recovery Checkpoint, when Stage 02 classification is durably committed, then the Stage 01 checkpoint/history remains unchanged and later current recovery state identifies classification-bearing progress separately.

### Acceptance mapping

- S1 → AC-CLASS-001, 003, 006, 010, 016
- S2 → AC-CLASS-003, 004, 005, 006
- S3 → AC-CLASS-004, 007
- S4 → AC-CLASS-006, 016
- S5 → AC-CLASS-006, 016
- S6 → AC-CLASS-006, 016
- S7 → AC-CLASS-007
- S8 → AC-CLASS-008
- S9 → AC-CLASS-003, 010, 011, 012
- S10 → AC-CLASS-001, 003, 014
- S11 → AC-CLASS-001, 002, 010
- S12 → AC-CLASS-013
- S13 → AC-CLASS-012

AC-CLASS-009 and AC-CLASS-015 are cross-cutting failure/uncertainty propositions and require explicit executable cases in the DEV-SPEC-004 acceptance suite even though they are not limited to one representative happy-path scenario. AC-CLASS-017 is demonstrated by the full historical acceptance regression suites.

## Failure and cancellation semantics

### Artifact-local classification failure

Where classification of one eligible Artifact fails operationally:

- the failure remains scoped to that Artifact/classification attempt where reliably possible;
- already established trustworthy results for unrelated Artifacts are not invalidated merely because another Artifact failed;
- the failed Artifact does not receive fabricated successful classification; and
- the persisted/run state must truthfully reflect incomplete/partial Stage 02 progress where applicable.

The implementation must not silently overload Stage 01 discovery diagnostics to represent Stage 02 classification failures.

### Persistence failure

A failure to durably commit Stage 02 state must not be reported as successfully completed classification.

Previously stable current checkpoint/history must remain authoritative.

No partially published canonical Stage 02 state may become current after a failed commit.

### Cancellation

Cancellation remains cancellation.

It must not be converted into unsupported, corrupt, ambiguous, or failed Artifact classification merely to produce a normal Stage 02 result.

No partially published canonical Stage 02 state may be presented as a successfully completed checkpoint after cancellation.

## Architecture and implementation constraints

Expected logical implementation areas include:

### Domain

Introduce only the Domain concepts/invariants necessary to represent Stage 02 classification state and its relationship to an existing Artifact identity.

The Domain must not depend on:

- SQLite;
- `System.Reflection.Metadata`;
- local filesystem adapters; or
- other concrete parser libraries.

### Application

Introduce Stage 02 orchestration and application-owned capability/repository ports as needed.

The Application layer owns normal gateway/capability ports and coordinates classification without embedding concrete parser technology.

Do not fold Stage 02 into `ArtifactIntakeService` merely because Stage 01 already knows how to read/persist Artifacts.

### Concrete analyzer/adapter capability

Implement actual format inspection behind the accepted inward boundary.

Managed PE classification may use `System.Reflection.Metadata` in accordance with the tooling baseline.

Use BCL/platform functionality where sufficient before introducing new third-party dependencies.

### Local infrastructure/persistence

Persist Stage 02 classification and later checkpoint/current-state progress without rewriting Stage 01 history.

The implementation may evolve or split the existing intake-oriented persistence shape where necessary, provided canonical history/provenance/checkpoint semantics remain intact.

### Composition

Update local application composition only as necessary to expose the Stage 02 application capability and concrete adapters.

CLI command-surface expansion is not acceptance-bearing for this slice unless required to exercise the application boundary.

## Tooling constraints

For managed PE/assembly classification, prefer the accepted metadata-only inspection approach using `System.Reflection.Metadata` rather than runtime assembly loading where metadata inspection is sufficient.

Classification must not execute supplied assemblies.

A new third-party dependency must not be introduced merely for convenience where the BCL/accepted tooling is sufficient.

If implementation reveals a material tooling implication not already accepted by `13-tooling-and-dependencies.md`, process it through qualification/governance rather than silently expanding tooling policy.

## Security and integrity expectations

Classification must:

- operate read-only against preserved Original Artifact bytes;
- not execute supplied binaries;
- not mutate the preserved Original Artifact snapshot;
- retain workspace path-boundary protections;
- preserve cancellation semantics;
- handle malformed/untrusted input without treating parser crashes as successful classification; and
- avoid treating path/extension metadata as executable instructions or proof of actual format.

## Executable verification

### Acceptance verification

Create:

`tests/LegacyRevive.AcceptanceTests/Development/DevSpec004Stage02ArtifactFormatAndFamilyClassificationTests.cs`

Acceptance verification should exercise observable behavior through the application/local-workspace boundary rather than treating parser internals as the acceptance surface.

Acceptance coverage must include at least:

- managed implementation classification;
- reference assembly classification;
- satellite/resource assembly classification;
- native PE versus managed PE;
- classic XML configuration classification;
- Portable PDB classification;
- XML documentation classification;
- `.deps.json` classification;
- `.runtimeconfig.json` classification;
- `packages.config` classification;
- `.nuspec` classification;
- `.nupkg` classification;
- misleading extension;
- unsupported/unrecognized preserved content;
- malformed/corrupt candidate content;
- partial Stage 01 flow-forward;
- failed-preservation exclusion;
- reopen without original source tree;
- deterministic substantive state; and
- preservation of Stage 01 checkpoint/history.

## Supporting verification

### Domain verification

Add focused Domain tests for introduced classification invariants, including as applicable:

- classification remains associated with an existing `ArtifactId`;
- preservation and classification state are distinct;
- successful classification requires the corresponding directly established format/family state;
- ambiguous/non-success states satisfy their required invariants; and
- later classification does not mutate Original Artifact identity/provenance/preservation semantics.

### Application verification

Add focused Application tests covering:

- Stage 02 eligibility filtering;
- `Partial` Stage 01 flow-forward;
- failed-preservation exclusion;
- deterministic orchestration/order where material;
- adapter failure mapping;
- persistence invocation/commit semantics;
- cancellation; and
- later classification-bearing recovery/checkpoint progress without Stage 01 rewrite.

### Analyzer/adapter verification

Concrete classifier tests must prove actual content/format inspection rather than extension mapping.

Representative managed-format coverage should include where practical:

- valid managed implementation assembly;
- valid native/non-managed PE;
- misleading `.dll`;
- reference assembly;
- satellite/resource assembly; and
- malformed PE candidate.

Other implemented format recognizers should include representative valid and malformed inputs required by this slice.

### Persistence verification

Extend local persistence verification to demonstrate:

- Stage 02 classification round-trip;
- Stage 01 checkpoint/history remains unchanged;
- later classification-bearing state becomes current according to the implemented checkpoint model;
- failed Stage 02 save/commit does not partially publish canonical state;
- reopen retains classification;
- migration/reopen from an existing DEV-SPEC-003 workspace remains valid; and
- earlier Stage 01 historical state is not silently rewritten by schema evolution.

### Regression verification

Run the complete solution test suite.

Minimum completion requirement:

- all DEV-SPEC-004 acceptance tests pass;
- all supporting DEV-SPEC-004 Domain/Application/adapter/persistence tests pass;
- all existing DEV-SPEC-001/002/003 acceptance tests pass;
- all existing supporting tests pass; and
- no skipped acceptance-bearing test is used to claim completion.

## Determinism

For equivalent:

- preserved Artifact bytes;
- relevant Artifact provenance/context;
- Stage 02 tooling baseline; and
- Stage 02 configuration,

the substantive classification result must be equivalent where canonical determinism is practical.

Incidental processing order must not change substantive classification conclusions.

Where persisted ordering contributes to checkpoint substance or observable recovery state, ordering must be deterministic.

Fresh generated identities for genuinely distinct recovery records do not need to match across independent runs unless canonical identity rules require stability.

## Epistemic constraints

This slice must preserve the following distinctions:

- filename extension is context, not proof of actual format;
- parser success is evidence about the Artifact format, not proof of original source structure;
- successful managed-assembly classification is not proof of application ownership;
- presence in supplied deployment material is not proof of original project membership;
- classification does not establish dependency intent;
- classification does not establish original developer intent;
- classification does not establish historical source-file layout;
- classification does not establish behavioural equivalence;
- an unsupported result is not evidence that the Artifact lacks useful recoverable information;
- undiscovered material is not classified as absent; and
- reconstructed/generated output cannot corroborate the Original Artifact classification premises that caused it to be generated.

No reconstructed output may be promoted to Original-System Evidence for this Stage 02 classification work.

## Qualifications discovered

None.

The formal pre-implementation audit found specification-structure and scope-clarity defects only; it did not expose a material canonical ambiguity, conflict, unsupported assumption, boundary challenge, or other issue requiring registration in `91-design-qualification-register.md`.

Possible companion-Artifact relationship identification remains a canonical Stage 02 obligation but is explicitly deferred from this bounded slice rather than silently treated as completed here.

Implementation and verification exposed no material canonical ambiguity, conflict, unsupported assumption, architecture/tooling boundary issue, checkpoint/history defect, or Stage 03 dependency requiring registration in `91-design-qualification-register.md`.

## Verification status

Historical verification under the then-defined acceptance contract: **Satisfied on 2026-09-29**.

Current completion disposition: **Rejected on 2026-09-30 under DEV-003 / D174**. The passing verification result is retained as historical evidence of what the then-defined contract tested; it no longer establishes that this DEV-SPEC is an accepted completed slice.

Executable acceptance verification:

- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec004Stage02ArtifactFormatAndFamilyClassificationTests.cs`
  - required managed/native/reference/satellite and structured routing families;
  - preserved-byte operation after source removal and durable reopen;
  - misleading extensions, unsupported content, malformed PE/XML/JSON/PDB/package candidates, and explicit ambiguity;
  - Partial Stage 01 flow-forward, failed-preservation exclusion, and Artifact-local operational failure;
  - deterministic substantive classification and separate Stage 02 checkpoint/current-state progress without Stage 01 rewrite.

Supporting verification:

- `tests/LegacyRevive.Domain.Tests/Recovery/ArtifactClassificationStateTests.cs` — classification/status/candidate/diagnostic invariants and stable Stage 01 associations;
- `tests/LegacyRevive.Application.Tests/Classification/ArtifactClassificationServiceTests.cs` — eligibility, Partial flow-forward, failure scoping, cancellation, persistence failure, and commit behavior;
- `tests/LegacyRevive.AcceptanceTests/Supporting/SqliteWorkspaceStoreTests.cs` — classification round-trip, reopen, atomic rollback, DEV-SPEC-003 schema evolution, and unchanged Stage 01 history;
- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec001WorkspaceLocalArtifactIntakeTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec002IntakeEnumerationFailureHandlingTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec003Stage01PositiveDiscoveryCommitmentTests.cs`;
- existing Domain/Application/local-adapter supporting suites as complete regression verification.

Verification result on 2026-09-29: complete solution test run passed with 77 total tests, 77 passed, 0 failed, and 0 skipped. The pre-slice 65-test baseline remains green within that result. No acceptance-bearing test is skipped.

AC-CLASS-001 through AC-CLASS-017 are satisfied by the acceptance mapping and executable/supporting locations above. Canonical basis, included/excluded scope, architecture/tooling/epistemic boundaries, persistence/history requirements, and the Definition of Done remain satisfied.

Blocking qualifications: None at the time of the historical `Implemented` transition.

## Rejection disposition

DEV-SPEC-004 is retained as meaningful development/governance history but is not an accepted implementation baseline.

Post-implementation review established that the Stage 02 Artifact-family recognition truth conditions were materially under-specified. The implementation therefore had to originate product semantics that should have been canonically established before implementation. That gap was subsequently governed through RMAT-009/D172 and DEV-002/D173.

The production/test implementation produced for this specification was rejected and the repository was restored to the accepted post-DEV-SPEC-003 baseline. In addition, this specification's own `Implementation completion conditions` required the implementation to be committed as a coherent Development Slice; that condition had not been satisfied when the document transitioned to `Implemented`.

Under DEV-003/D174, these facts invalidate the prior completion judgment without erasing it. The historical verification results and `Verification Pending → Implemented` event remain recorded below, while the current lifecycle status is `Rejected`.

No rejected DEV-SPEC-004 production implementation is part of the accepted repository baseline. Any replacement Stage 02 classification implementation must use a new DEV-SPEC derived from RMAT-009/D172 and must pass the DEV-002/D173 semantic-readiness gate.

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| 2026-09-29 | Created → Draft | Initial DEV-SPEC-004 formulation from the accepted Stage 02/MVP classification requirements and the post-DEV-SPEC-003 repository baseline. |
| 2026-09-29 | Draft → Ready for Implementation | Initial formulation established the intended acceptance baseline before the formal document-structure audit. |
| 2026-09-29 | Ready for Implementation → Draft | Formal readiness audit found missing required DEV-SPEC lifecycle/verification sections and ambiguity over whether listed MVP Artifact families were implementation obligations or examples. No canonical qualification was required. |
| 2026-09-29 | Draft → Ready for Implementation | Required structure was restored, acceptance/scenario traceability was added, MVP Stage 02 routing-family coverage was made explicit, companion-correlation was explicitly deferred, and the canonical/repository readiness audit completed with no blocking qualification. |
| 2026-09-29 | Ready for Implementation → In Progress | Readiness and blocker review completed; acceptance-first implementation began against the established baseline. |
| 2026-09-29 | In Progress → Verification Pending | Bounded Domain/Application/classifier/persistence implementation and planned executable/supporting verification were complete and passing in focused runs. |
| 2026-09-29 | Verification Pending → Implemented | AC-CLASS-001 through AC-CLASS-017, focused verification, persistence/replay/history checks, and the complete 77-test solution regression passed with no blocking qualification. |
| 2026-09-30 | Implemented → Rejected | DEV-003/D174 resolved the post-implementation disposition: later review established a materially inadequate semantic/acceptance basis, the implementation was rejected and rolled back to the post-DEV-SPEC-003 baseline, and the specification-specific commit completion condition had not been satisfied. The historical Implemented event and verification result are retained rather than rewritten. |

## Readiness audit result

The formal pre-implementation audit completed on 2026-09-29 found no remaining material blocker after the corrections recorded above.

The specification now conforms to the required DEV-SPEC structure in `16-development-process.md`, preserves the canonical Stage 02/MVP boundaries, documents its deliberate deferrals, defines an executable acceptance baseline, and remains consistent with the current DEV-SPEC-001 through DEV-SPEC-003 repository state.

## Governance assessment

No new qualification is required before implementation.

The following remain implementation details already bounded by accepted canonical direction:

- exact class/type names;
- exact enum/value decomposition;
- exact SQLite table names;
- whether the existing persistence implementation is renamed, split, or generalized;
- whether a dedicated concrete analysis project is introduced;
- exact parser composition;
- exact checkpoint schema mechanics; and
- exact application API naming.

A new qualification must be opened if implementation instead exposes a material issue such as:

- canonical Artifact-family semantics are insufficient to represent a material required distinction;
- a Stage 02 classification currently assumed to be direct actually requires inference;
- accepted checkpoint/history semantics cannot represent later classification state without changing canonical history rules;
- classification requires changing what counts as Original-System Evidence;
- the accepted Artifact/persistence boundary cannot represent classification without materially changing architecture;
- a required tool introduces a material architecture/tooling policy change; or
- the slice cannot satisfy its acceptance contract without materially crossing into Stage 03.

Such an issue must not be resolved silently in code.

## Ready-for-Implementation assessment

This specification satisfies the `16-development-process.md` preconditions for `Ready for Implementation`:

- canonical basis is identified;
- the slice is bounded;
- included and excluded scope are explicit;
- known repository transition constraints are documented without prematurely prescribing physical design;
- no material unresolved qualification currently blocks the work;
- the acceptance contract is explicit;
- representative scenarios cover normal, partial, failure, replay, determinism, and history-preservation paths; and
- executable/supporting verification areas are identified.

The slice may therefore begin implementation without additional design work unless implementation itself exposes a material qualification under the governance rules above.

## Implementation starting point

Begin with the acceptance-facing Stage 02 model and orchestration boundary rather than deep parser functionality.

A suitable implementation progression is:

1. introduce the minimum Domain representation for Artifact classification state;
2. define Application-owned classification and persistence capability boundaries;
3. add acceptance/Application tests for eligibility, extension-independence, partial Stage 01 flow-forward, durable reopen, and Stage 01 history preservation;
4. implement the minimum concrete format inspection required to make those scenarios real;
5. extend durable workspace persistence/checkpoint state without rewriting Stage 01 history;
6. add representative managed/native/unsupported/corrupt cases;
7. complete supporting verification and full regression; and
8. perform the implementation-completion audit before changing this specification to `Implemented`.

Production implementation remains subordinate to this acceptance baseline and the canonical Project documents.

## Implementation completion conditions

DEV-SPEC-004 may transition to `Implemented` only when:

1. all acceptance propositions have executable verification;
2. all acceptance-bearing representative scenarios are implemented and passing;
3. Domain/Application/adapter/persistence supporting verification is passing;
4. DEV-SPEC-001/002/003 regression behavior remains green;
5. the complete solution verification passes;
6. no material qualification remains unresolved for the implemented slice;
7. actual verification paths/results are recorded in this specification;
8. status/history fields are updated consistently with `16-development-process.md`;
9. any governance changes exposed during implementation have completed required synchronization; and
10. the implementation is committed as a coherent Development Slice according to the repository development process.
