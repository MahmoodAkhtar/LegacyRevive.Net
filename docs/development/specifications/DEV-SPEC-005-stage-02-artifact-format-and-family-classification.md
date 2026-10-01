# DEV-SPEC-005 — Stage 02 Artifact Format and Family Classification

Status: Rejected  
Blocked by: None  
Supersedes: DEV-SPEC-004  
Superseded by: None

## Purpose

Implement the bounded Stage 02 Artifact format/family-classification capability required by the MVP, using the now-canonical direct recognition contracts established by RMAT-009 / D172.

For each Stage 01 Artifact that has a trustworthy workspace-owned preserved-byte representation, the slice must inspect that preserved Artifact instance and establish durable Stage 02 classification state that truthfully represents the most specific format/family directly established by the bytes and permitted Stage 02 structural inspection.

This replacement specification exists because the earlier `DEV-SPEC-004 — Stage 02 Artifact Format and Family Classification` reached `Implemented` under an acceptance/design basis later determined to be semantically under-specified. Under DEV-003 / D174, DEV-SPEC-004 remains historical `Rejected` material and its rejected implementation is not part of the accepted repository baseline.

DEV-SPEC-005 does not recreate the Stage 02 recognition semantics. Those semantics are canonically owned by `06-recovery-matrix.md` §5A and consumed by `07-recovery-process.md` §4. This specification translates those settled semantics into one bounded, acceptance-bearing implementation slice.

The slice establishes the first durable post-intake recovery knowledge in the current accepted repository baseline while preserving the accepted behavior of DEV-SPEC-001 through DEV-SPEC-003.

It does not begin Stage 03 semantic extraction, cross-Artifact evidence integration, inference, ownership classification, reconstruction, build/repair, validation, or roadmap work.

---

## Canonical basis

### Primary canonical owners

- `00-ai-context.md`
  - canonical ownership and governance orientation;
  - foundational epistemic distinctions;
  - Stage 02 is part of the canonical stage-oriented recovery process.

- `03-evidence-model.md`
  - direct observations remain distinct from inference;
  - generated or reconstructed output cannot become independent historical support;
  - provenance and epistemic distinctions must remain visible.

- `06-recovery-matrix.md`
  - §3: Artifact-type capability does not imply Artifact-instance content;
  - §5: companion-artifact relationships require defensible support and are not established by filename similarity alone;
  - §5A.1: Stage 02 classification is established only from direct structural inspection of the preserved Artifact instance;
  - §5A.2: canonical classification outcomes;
  - §5A.3: hierarchy, overlap, precedence, and ambiguity rules;
  - §5A.4: Stage 02 may inspect internal structure only as required for recognition and persists classification-bearing state only;
  - §5A.5: minimum direct recognition contracts for the current MVP formats/families.

- `07-recovery-process.md`
  - §4: Stage 02 objective, eligible Artifact handling, classification workflow, diagnostic scoping, determinism, and the Stage 02 / Stage 03 boundary;
  - later recovery state does not rewrite prior historical recovery state;
  - partial trustworthy work remains usable;
  - operational failure is scoped to the narrowest reliable boundary.

- `12-architecture.md`
  - Original Artifact Store includes stable Artifact identity, preserved-byte availability, and Artifact classification state;
  - preserved workspace-owned bytes are authoritative for subsequent recovery/replay;
  - Original Artifact bytes remain immutable;
  - later checkpoints do not rewrite earlier checkpoints;
  - persistence must preserve stable IDs, checkpoint/history semantics, provenance, partial progress, and replay;
  - Domain/Core remains independent of concrete tools;
  - capability adapters operate behind inward-owned contracts.

- `13-tooling-and-dependencies.md`
  - `System.Reflection.Metadata` / `PEReader` is the selected primary managed PE/CLR metadata inspection technology;
  - supplied assemblies are inspected statically rather than executed or loaded as the primary analysis model;
  - SQLite via `Microsoft.Data.Sqlite` remains the default local persistence technology;
  - xUnit v3 remains the automated-test baseline;
  - concrete tools must not redefine canonical classification semantics.

- `14-mvp.md`
  - the local-directory MVP must inventory/preserve discovered supplied files;
  - required MVP analyzer coverage includes managed `.dll` / `.exe`, classic XML configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nupkg`, `.nuspec`, and recognizable satellite/resource assemblies;
  - §4.1 explicitly delegates Stage 02 truth conditions to `06-recovery-matrix.md` §5A;
  - valid known but unsupported content must not be relabeled malformed merely because later deep analysis is unsupported;
  - workspace persistence/resume is required.

- `16-development-process.md`
  - this DEV-SPEC is a bounded, durable, non-canonical implementation specification;
  - acceptance propositions must derive from canonical requirements;
  - semantic readiness must be `Pass` before `Ready for Implementation`;
  - executable acceptance demonstrates accepted semantics but does not create them;
  - rejected specifications remain historical and replacements use new identities.

- `90-decisions.md`
  - durable decisions listed below.

- `91-design-qualification-register.md`
  - RMAT-009, DEV-002, and DEV-003 are Resolved;
  - no unresolved qualification identified in the current governed state blocks this replacement slice.

- `AGENTS.md`
  - implementation-facing authority flow and coding-agent discipline;
  - coding agents must confirm semantic-readiness `Pass`, implement only the active slice, preserve canonical boundaries, and return material ambiguities to governance.

### Durable decisions directly constraining this slice

- D003 — Provenance Is a Core Requirement.
- D014 — Prefer Deterministic Recovery Where Practical.
- D047 — Artifact-Type Capability Does Not Imply Artifact-Instance Content.
- D048 — Artifact Format Must Be Classified Beyond File Extension.
- D051 — Matrix Capability and Artifact-Instance Findings Are Separate.
- D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process.
- D055 — Stage Progression Does Not Promote Epistemic Category.
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
- D172 — Stage 02 Artifact Classification Uses Direct Format-Specific Recognition Contracts.
- D173 — Development Slices Require Semantic Readiness Before Implementation.
- D174 — Rejected DEV-SPECs Preserve Unsound Implemented Completion Judgments.

### Historical implementation baseline

The accepted repository baseline is the state after DEV-SPEC-003.

The rejected DEV-SPEC-004 Stage 02 production/test implementation has been removed and is not part of the accepted implementation baseline.

The supplied repository snapshot is still Stage-01-shaped. In particular, it currently exposes:

- `ArtifactIntakeService`;
- `OriginalArtifact` preservation state;
- `RecoveryWorkspaceState` containing Stage 01 run/snapshot/checkpoint/artifact/diagnostic state;
- `IIntakeStateRepository` / `SqliteIntakeStateRepository`;
- `LocalRecoveryWorkspaceApplication` exposing intake/open operations.

No surviving Stage 02 classification implementation is assumed by this specification.

The supplied pre-slice verification baseline is:

- 65 total tests;
- 65 passed;
- 0 failed;
- 0 skipped.

DEV-SPEC-001 through DEV-SPEC-003 remain the accepted earlier implementation history and must remain green.

---

## Scope

### Included

- Determine Stage 02 classification eligibility for admitted Stage 01 Artifacts.
- Classify only from trustworthy workspace-owned preserved bytes.
- Retain Artifact identity, supplied provenance, SHA-256 content identity, preserved-byte reference, and Stage 01 preservation state.
- Evaluate the canonical `06-recovery-matrix.md` §5A recognition contracts for:
  - managed CLI artifact;
  - managed CLI assembly;
  - managed implementation assembly;
  - reference assembly;
  - satellite/resource assembly;
  - native/non-managed PE;
  - classic .NET XML configuration;
  - Portable PDB;
  - XML documentation;
  - `.deps.json` dependency context;
  - `.runtimeconfig.json`;
  - `packages.config`;
  - `.nuspec`;
  - `.nupkg`.
- Represent the canonical Stage 02 outcomes where applicable:
  - Established;
  - Broader format established;
  - Valid but unsupported;
  - Unsupported/unrecognized preserved content;
  - Ambiguous;
  - Malformed/corrupt candidate;
  - Operational classification failure.
- Apply canonical specialization/overlap precedence rather than recognizer order.
- Preserve materially incompatible independently satisfied classifications as explicit ambiguity.
- Permit internal structure inspection only as far as required by a recognition contract.
- Persist only classification-bearing state and classification diagnostics from this slice.
- Associate durable Stage 02 classification state with the existing `ArtifactId`.
- Preserve diagnostics at the narrowest reliable Artifact/classification scope.
- Allow trustworthy successfully preserved Artifacts from a `Partial` Stage 01 run to proceed.
- Exclude admitted Artifacts with failed preservation from byte-derived classification when no trustworthy preserved representation exists.
- Preserve Stage 01 history/checkpoints/snapshots rather than rewriting them as though Stage 02 knowledge existed at intake.
- Persist later classification-bearing recovery progress and support reopen/replay without the original supplied source tree.
- Preserve deterministic substantive classification for equivalent bytes and relevant configuration regardless of incidental recognizer or Artifact processing order.
- Preserve cancellation and persistence-failure semantics without fabricating classification state.
- Add executable slice-level acceptance verification.
- Add focused Domain/Application/analyzer/persistence supporting verification.
- Preserve all DEV-SPEC-001 through DEV-SPEC-003 acceptance behavior.

### Excluded

- Stage 03 reusable semantic extraction, including:
  - CLR type/member/signature extraction;
  - assembly-reference/dependency extraction;
  - method-body/IL extraction;
  - PDB document/sequence-point extraction;
  - XML documentation member/text extraction;
  - configuration setting/value extraction;
  - WCF semantic extraction;
  - `.deps.json` dependency-entry extraction;
  - `.runtimeconfig.json` runtime-option extraction;
  - NuGet dependency/content extraction;
  - resource payload extraction.
- Cross-Artifact evidence integration.
- Companion-Artifact relationship establishment or correlation as a distinct capability.
  - Stage 02 canonically identifies possible companion relationships, but this replacement slice is bounded to format/family classification only.
  - No companion relationship may be silently treated as implemented by this slice.
- Assembly/application ownership classification such as `ApplicationOwned`, `InternalDependency`, `ThirdParty`, or `Framework`.
- Candidate Project creation, project-boundary inference, merge/split, solution generation, or dependency reconstruction.
- Convention inference.
- Developer Intervention workflows.
- Decompilation or source reconstruction.
- Build/restore/repair.
- Validation.
- Recovery metrics or final reporting.
- External enrichment.
- Roadmap capabilities.
- Runtime third-party plugin loading.
- Full filesystem graph reconstruction or directory Artifact identities.
- Executing supplied binaries.
- Treating filename, extension, relative path, deployment directory, parser order, or parser convenience as canonical classification truth.
- Persisting Stage 03 Observations merely because a Stage 02 recognizer reads the relevant structure.
- Prescribing private helper/class names, parser decomposition, buffering strategy, local data structures, concrete SQLite table layout, or other mechanics that preserve the accepted semantics.

---

## Acceptance contract

### AC-S02-001 — Only trustworthy preserved Artifacts are content-classified

Every admitted Artifact with successful Stage 01 preservation and a trustworthy workspace-owned preserved-byte representation is eligible for Stage 02 classification, whether the Stage 01 Recovery Run is `Completed` or `Partial`.

An Artifact whose preservation failed and has no trustworthy preserved representation is not given a fabricated byte-derived Stage 02 classification.

### AC-S02-002 — Classification uses the workspace-owned preserved representation

Stage 02 reads the workspace-owned preserved Artifact representation established by Stage 01.

After successful preservation, the original supplied external path is provenance, not an operational dependency for classification or replay.

### AC-S02-003 — Filename/path/context cannot establish format or family

Filename, extension, relative path, and deployment position may select efficient probes and may corroborate a classification established from content.

They do not by themselves establish actual Artifact format/family, malformed/corrupt status, reference-assembly status, satellite status, or another Stage 02 classification.

### AC-S02-004 — Managed CLI artifact recognition follows the canonical contract

A managed CLI Artifact is established only when the Artifact is a structurally valid PE/COFF image containing a CLR/CLI header whose referenced CLI metadata root is structurally readable as valid CLI metadata.

`MZ`, `.dll`, or `.exe` alone is insufficient.

A PE with a CLR/CLI header whose required CLI metadata cannot be structurally read is a malformed/corrupt managed candidate; it is not reclassified as native merely because managed parsing failed.

### AC-S02-005 — Managed CLI assembly recognition follows the canonical contract

A managed CLI assembly is established only when the managed CLI Artifact also contains a valid Assembly manifest/table.

A valid managed CLI module without an Assembly manifest remains a valid managed module/netmodule; for MVP purposes it is valid-but-unsupported rather than malformed.

### AC-S02-006 — Reference assembly recognition follows the canonical contract

A reference assembly is established only when a managed CLI assembly contains an assembly-level custom attribute whose metadata-resolved type is exactly:

`System.Runtime.CompilerServices.ReferenceAssemblyAttribute`

Simple attribute-name matching, absence of method bodies, or a `ref/`-like path is insufficient.

Absence of the attribute does not by itself establish implementation-assembly status.

### AC-S02-007 — Satellite/resource assembly recognition follows the canonical contract

A satellite/resource assembly is established only when a managed CLI assembly has:

- non-neutral assembly culture;
- one or more manifest resources; and
- no executable implementation method bodies.

`.resources.dll` naming, a culture-named directory, or culture alone is insufficient.

A resource-bearing managed assembly that does not satisfy the satellite conditions remains at the broader directly established managed/resource-bearing classification rather than being forced into the satellite subtype.

### AC-S02-008 — Managed implementation assembly recognition follows the canonical contract

A managed implementation assembly is established only when a managed CLI assembly:

- is not directly established as a reference assembly;
- is not directly established as a satellite/resource assembly; and
- contains direct implementation-bearing evidence, such as at least one executable managed method body or an executable entry point backed by implementation metadata.

A managed assembly, absence of `ReferenceAssemblyAttribute`, or `.dll` / `.exe` naming alone is insufficient.

A valid managed assembly lacking sufficient subtype evidence remains `Managed CLI assembly (broad)`.

### AC-S02-009 — Native/non-managed PE recognition follows the canonical contract

A native/non-managed PE is established when PE/COFF structure is valid and no CLR/CLI header establishes a managed CLI Artifact.

A PE containing a CLR/CLI header whose required CLI metadata is malformed is not classified as native.

### AC-S02-010 — Classic .NET XML configuration recognition follows the canonical contract

Classic .NET XML configuration is established when XML is well formed and the document element is the un-namespaced element `configuration`.

A `.config`, `app.config`, or `web.config` filename is insufficient.

Generic well-formed XML is insufficient.

A content-level `<configuration` candidate that cannot satisfy required XML well-formedness may be represented as a malformed configuration candidate; a misleading filename without content-level family evidence is not sufficient to create malformed configuration state.

Configuration section/value semantics are deferred to Stage 03.

### AC-S02-011 — Portable PDB recognition follows the canonical contract

Portable PDB is established when standalone ECMA-335-style metadata is structurally readable and contains both the required `#Pdb` and `#~` streams for standalone Portable-PDB debugging metadata.

A `.pdb` filename or generic metadata-root signature alone is insufficient.

A valid different metadata blob is not a malformed Portable PDB.

Content that establishes the Portable-PDB metadata shape but fails required stream structure is malformed/corrupt.

Native Windows PDB support remains optional for the MVP and is not required by this slice.

### AC-S02-012 — XML documentation recognition follows the canonical contract

XML documentation is established when:

- XML is well formed;
- the document element is `doc`; and
- a direct `members` child exists.

An `.xml` filename alone is insufficient.

An `assembly/name` element may corroborate but is not required for family recognition.

A content-level `<doc` candidate that cannot be parsed may be malformed XML documentation.

Member/documentation semantics and assembly correlation are outside this slice.

### AC-S02-013 — `.deps.json` recognition follows the canonical contract

A dependency-context Artifact is established when:

- JSON is well formed;
- the root is an object;
- `runtimeTarget`, `targets`, and `libraries` are present as objects; and
- `runtimeTarget.name` is a string.

The `.deps.json` filename, isolated property names, or arbitrary valid JSON is insufficient.

Wrong required member/container shapes are malformed dependency-context candidates when family-specific structure is otherwise established.

Dependency-entry and cross-reference semantics are deferred to Stage 03.

### AC-S02-014 — `.runtimeconfig.json` recognition follows the canonical contract

A runtime-configuration Artifact is established when:

- JSON is well formed;
- the root is an object; and
- `runtimeOptions` exists and is an object.

The `.runtimeconfig.json` filename is insufficient.

A family-specific `runtimeOptions` member whose required container shape is invalid produces malformed runtime-configuration candidate state rather than successful classification.

Individual runtime-option semantics are deferred to Stage 03.

### AC-S02-015 — `packages.config` recognition follows the canonical contract

`packages.config` is established when:

- XML is well formed;
- the document element is `packages`; and
- each `package` entry used by the manifest has non-empty `id` and `version` attributes.

Filename alone or generic XML is insufficient.

A `packages` manifest with structurally invalid package entries is a malformed packages-config candidate.

Dependency interpretation is deferred to Stage 03.

### AC-S02-016 — `.nuspec` recognition follows the canonical contract

NuSpec is established when:

- XML is well formed;
- the document element is `package`;
- a direct `metadata` element exists; and
- package `id` and `version` are non-empty.

Filename alone or generic `<package>` XML without NuGet metadata structure is insufficient.

Namespace/schema URI may provide corroborating/version context but is not the sole family discriminator.

Missing/invalid NuGet manifest structure after family-specific content is established produces malformed NuSpec candidate state.

Full package metadata/dependency extraction and schema-version validation are deferred to Stage 03.

### AC-S02-017 — `.nupkg` recognition follows the canonical contract

NuGet package/container classification is established when:

- the bytes form a readable ZIP package container; and
- the package contains a root package `.nuspec` manifest that itself satisfies AC-S02-016 / the canonical NuSpec recognition contract.

A `.nupkg` filename or ZIP signature/container alone is insufficient.

A valid ZIP without a recognized NuSpec is valid non-NuGet ZIP/unsupported format, not a malformed NuGet package.

Corrupt ZIP structure may be a malformed NuGet-package candidate only where content-level package evidence establishes that candidate boundary.

### AC-S02-018 — Most-specific directly established classification wins within a hierarchy

Where directly established classifications form a compatible specialization hierarchy, the current Stage 02 result is the most specific directly established classification.

Examples include:

- managed CLI Artifact → managed CLI assembly → managed implementation assembly;
- managed CLI Artifact → managed CLI assembly → reference assembly;
- managed CLI Artifact → managed CLI assembly → satellite/resource assembly.

Where only the broader contract is satisfied, the broader classification remains current.

Recognizer execution order must not create or remove specificity.

### AC-S02-019 — Materially incompatible independently satisfied contracts remain ambiguous

Where two or more materially incompatible family contracts are independently satisfied and neither canonically subsumes the other, Stage 02 preserves `Ambiguous`.

The result retains the directly supportable candidate classifications and enough diagnostic/context information to explain the ambiguity.

The implementation must not select by recognizer order, extension, path, parser preference, or convenience.

### AC-S02-020 — Valid-but-unsupported is distinct from malformed/corrupt

A structurally valid known format outside current MVP deep-analysis support remains valid-but-unsupported.

In particular, a valid managed CLI module/netmodule without an Assembly manifest is valid-but-unsupported for the MVP rather than malformed.

Lack of a later analyzer/capability does not convert valid content into corruption.

### AC-S02-021 — Unsupported/unrecognized preserved content remains visible

Successfully preserved content for which no supported or otherwise recognized Stage 02 format/family is established remains a valid preserved Artifact with explicit unsupported/unrecognized Stage 02 state.

It is not discarded, converted into a Stage 01 preservation failure, or assigned a guessed family.

### AC-S02-022 — Malformed/corrupt requires content-level known-format support

Malformed/corrupt candidate state is created only where content-level evidence establishes a known-format candidate and the format's required structural conditions fail.

Filename/extension alone is insufficient.

Parser rejection alone is not automatically corruption. Where determinable, diagnostics preserve whether rejection represents:

- deterministic content invalidity;
- unsupported format/version; or
- operational/tool/resource failure.

No malformed/corrupt candidate produces fabricated successful family classification.

### AC-S02-023 — Operational classification failure is distinct from content invalidity

A tool/resource/read/adapter failure that prevents classification from completing is represented as operational classification failure rather than unsupported, ambiguous, malformed/corrupt, or successful classification.

The failure remains scoped to the narrowest reliable Artifact/classification boundary.

Trustworthy classification state for unrelated Artifacts is not invalidated merely because another classification attempt fails operationally.

Cancellation remains cancellation and is not translated into a normal classification outcome.

### AC-S02-024 — Stage 02 inspection does not persist Stage 03 semantic content

A Stage 02 recognizer may inspect metadata records, container entries, XML/JSON shape, resources, attributes, method-body existence, or other structural fields only as necessary to evaluate a canonical recognition contract.

This slice persists only classification-bearing state and classification diagnostics.

It does not persist reusable semantic content such as:

- CLR type/member metadata;
- assembly-reference/dependency entries;
- configuration values;
- runtime options;
- PDB documents/sequence points;
- package dependencies;
- resource payloads;
- other direct semantic extraction belonging to Stage 03.

Stage 03 may later reread the same preserved bytes. Avoiding duplicate reads is an implementation optimization, not a reason to merge Stage 02 and Stage 03 semantics.

### AC-S02-025 — Stage 02 does not mutate Stage 01 Artifact truth

Classification does not change the existing Artifact's:

- `ArtifactId`;
- supplied relative-path provenance;
- capture context;
- SHA-256 content identity;
- preserved-byte reference;
- preservation status; or
- Stage 01 preservation/discovery diagnostics.

Classification state is later recovery knowledge associated with the existing Artifact, not a rewritten intake-time fact.

### AC-S02-026 — Stage 02 state is durable and history-preserving

Successfully committed classification state survives reopen/process restart and remains available without the original supplied source tree.

Committing Stage 02 progress:

- does not rewrite the historical Recovery Intake Snapshot;
- does not rewrite an earlier Stage 01 Recovery Checkpoint;
- does not make Stage 01 appear to have contained classification knowledge;
- produces later current recovery/checkpoint state capable of identifying the classification-bearing progress.

A later checkpoint does not rewrite an earlier checkpoint.

Exact physical persistence schema is not prescribed.

### AC-S02-027 — Partial Stage 01 state flows forward truthfully

A positively discovered and successfully preserved Artifact retained in a `Partial` Stage 01 run remains eligible for Stage 02 classification.

Existing Stage 01 discovery incompleteness and scoped diagnostics remain intact.

No Artifact or classification is synthesized for undiscovered possible files.

### AC-S02-028 — Substantive classification is deterministic

For equivalent:

- preserved Artifact bytes;
- relevant recorded Artifact provenance/context;
- tool/version baseline where semantically relevant; and
- Stage 02 configuration,

the substantive Stage 02 result is equivalent where canonical determinism is practical.

Recognizer order, Artifact processing order, parser registration order, or incidental concurrency scheduling must not change the classification conclusion, ambiguity result, or semantically material diagnostics.

Fresh generated identities for genuinely distinct new recovery records need not match across independent runs unless canonical identity semantics require equality.

### AC-S02-029 — Failed commit does not publish partial canonical Stage 02 state

A failure while durably committing Stage 02 state does not result in a successfully published classification-bearing current checkpoint.

The previously stable current checkpoint/history remains authoritative.

Cancellation before successful publication likewise does not produce a partially published successful Stage 02 checkpoint.

### AC-S02-030 — Existing accepted Stage 01 behavior remains green

All DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 acceptance behavior remains passing after implementation of this slice.

---

## Readiness assessment

Semantic readiness: **Pass**

### Canonical semantic basis

The material Stage 02 semantics required by this slice are now explicitly owned and determined by:

- `06-recovery-matrix.md` §5A for:
  - direct recognition truth conditions;
  - canonical outcome meanings;
  - broader-versus-specific behavior;
  - valid-but-unsupported versus malformed/corrupt;
  - operational failure boundaries;
  - specialization precedence;
  - incompatible-contract ambiguity;
  - Stage 02 structural-inspection limits;
  - all MVP family recognition contracts;

- `07-recovery-process.md` §4 for:
  - Stage 02 eligible Artifact workflow;
  - persisted Stage 02 output boundary;
  - diagnostic scoping;
  - deterministic substantive result;
  - Stage 02 / Stage 03 separation;

- `12-architecture.md` for:
  - workspace-owned preserved bytes;
  - immutable Original Artifact semantics;
  - logical classification state;
  - stable identity;
  - checkpoint/history/replay semantics;
  - persistence abstraction and architectural dependency boundaries;

- `14-mvp.md` for:
  - first-release format/family coverage and preserved unsupported content;

- `16-development-process.md` / D173 for:
  - the semantic-readiness gate and material semantic choice test.

### Material semantic choice test

| Material acceptance area | Canonical determination | Result |
|---|---|---|
| Which bytes are eligible for Stage 02 classification? | Stage 01 preservation + Stage 02 process boundary | Pass |
| Can filename/extension establish actual family? | No; only content-established structure may do so | Pass |
| Managed CLI versus native PE truth conditions | `06` §5A.5 | Pass |
| Assembly versus module/netmodule | `06` §5A.5 | Pass |
| Reference assembly truth condition | exact metadata-resolved assembly-level `ReferenceAssemblyAttribute` | Pass |
| Satellite/resource assembly truth condition | culture + resources + no executable implementation bodies | Pass |
| Managed implementation truth condition | non-reference/non-satellite plus direct implementation-bearing evidence | Pass |
| XML configuration truth condition | well-formed XML + un-namespaced `configuration` root | Pass |
| Portable PDB truth condition | standalone metadata + required `#Pdb` and `#~` streams | Pass |
| XML documentation truth condition | well-formed XML + `doc` root + direct `members` child | Pass |
| `.deps.json` truth condition | required root objects + string `runtimeTarget.name` | Pass |
| `.runtimeconfig.json` truth condition | object root + object `runtimeOptions` | Pass |
| `packages.config` truth condition | `packages` root + valid non-empty package id/version entries | Pass |
| `.nuspec` truth condition | `package` root + direct `metadata` + non-empty id/version | Pass |
| `.nupkg` truth condition | readable ZIP + root NuSpec satisfying NuSpec contract | Pass |
| Broader-versus-specific precedence | most specific directly established specialization | Pass |
| Incompatible overlap | explicit `Ambiguous`; recognizer order cannot decide | Pass |
| Valid-but-unsupported versus malformed | explicitly distinguished | Pass |
| Malformed/corrupt threshold | content-level candidate + failed required structure | Pass |
| Operational/tool/cancellation failure | explicitly distinct from content invalidity | Pass |
| Stage 02/Stage 03 boundary | classification-bearing state only; reusable semantic extraction deferred | Pass |
| Persistence/history | later state/checkpoint; prior state not rewritten | Pass |
| Partial Stage 01 flow-forward | trustworthy preserved Artifacts remain usable | Pass |
| Determinism | substantive result independent of incidental processing/recognizer order | Pass |
| Epistemic category | direct classification; no inference/reconstruction promotion | Pass |

No material acceptance area identified above requires implementation to originate a new product/design semantic.

### Qualifications

No new blocking qualification is discovered by this readiness review.

RMAT-009, DEV-002, and DEV-003 are already Resolved and their durable decisions D172, D173, and D174 provide the required governed basis.

If implementation reveals a material question not determined by the canonical owners—for example a new recognition threshold, a new semantic outcome, a new precedence rule, or a new persistent/history meaning—the slice must return to the qualification process rather than selecting an interpretation in code or tests.

### Implementation discretion intentionally left open

The following remain implementation mechanics provided every alternative preserves the acceptance contract and canonical architecture/tooling constraints:

- private type/helper decomposition;
- public/internal naming not itself acceptance-bearing;
- one recognizer per family versus composed recognizers;
- probe scheduling and short-circuit mechanics that do not change substantive outcomes;
- buffering versus streaming;
- local algorithms/data structures;
- exact enum/value decomposition;
- exact diagnostic-code names where no canonical code is prescribed, provided semantic distinctions remain observable;
- exact application API names;
- exact capability-port names;
- exact persistence repository names;
- exact SQLite tables/columns/indexes/migrations;
- whether the current intake-oriented persistence implementation is evolved, split, or generalized;
- whether a dedicated analysis project is introduced, subject to architecture/dependency rules;
- fixture-generation mechanics;
- whether a later Stage 03 analyzer rereads bytes or shares non-persisted mechanical parsing infrastructure without collapsing stage semantics.

---

## Representative scenarios

Representative scenarios demonstrate the acceptance propositions. Their fixture names, byte-generation techniques, and private implementation structure are not product semantics.

### S01 — Managed implementation assembly with misleading neutral filename

Given a successfully preserved managed CLI assembly whose bytes satisfy the managed implementation assembly contract, and whose filename does not carry a `.dll` or `.exe` extension, when Stage 02 runs, then:

- it is classified as the managed implementation subtype from its bytes;
- the broader managed CLI Artifact/assembly levels are not returned as competing ambiguity;
- filename absence does not prevent classification;
- no unrelated CLR semantic extraction is persisted.

Covers: AC-S02-001, 002, 003, 004, 005, 008, 018, 024, 025.

### S02 — Native PE named `.dll`

Given a structurally valid native/non-managed PE named `legacy.dll`, when Stage 02 runs, then:

- the `.dll` extension does not establish managed classification;
- no CLR/CLI header establishes managed CLI content;
- the Artifact is classified as native/non-managed PE.

Covers: AC-S02-003, 009, 018.

### S03 — CLR header with malformed CLI metadata

Given a structurally valid PE containing a CLR/CLI header whose required CLI metadata root cannot be structurally read, when Stage 02 runs, then:

- the Artifact is a malformed/corrupt managed candidate;
- it is not reclassified as native merely because managed inspection fails;
- no successful managed family is fabricated.

Covers: AC-S02-004, 009, 022.

### S04 — Valid managed netmodule

Given a structurally valid managed CLI Artifact with readable CLI metadata but no Assembly manifest/table, when Stage 02 runs, then:

- the managed CLI Artifact format is directly established;
- managed CLI assembly is not established;
- the Artifact is valid-but-unsupported for the MVP rather than malformed.

Covers: AC-S02-004, 005, 020.

### S05 — Broad managed assembly without sufficient subtype evidence

Given a valid managed CLI assembly that:

- does not carry the exact reference-assembly marker;
- does not satisfy the satellite/resource contract; and
- has no directly established executable implementation method body or implementation-backed executable entry point,

when Stage 02 runs, then the current classification remains the broader managed CLI assembly classification rather than forcing implementation/reference/satellite subtype.

Covers: AC-S02-005, 006, 007, 008, 018.

### S06 — Exact reference-assembly marker versus misleading near-match

Use two preserved managed assemblies:

1. one with an assembly-level attribute metadata-resolved exactly to `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`;
2. one with a different attribute type having the same simple name but a different fully qualified type identity.

Then Stage 02 must classify only the first as a reference assembly.

Covers: AC-S02-003, 006, 018.

### S07 — Satellite/resource assembly versus name-only lookalike

Use:

1. a managed assembly with non-neutral culture, one or more manifest resources, and no executable implementation bodies;
2. a managed implementation assembly named `Something.resources.dll` that does not satisfy the satellite structural contract.

Then only the first is classified as satellite/resource assembly.

Covers: AC-S02-003, 007, 008, 018.

### S08 — Classic configuration versus arbitrary XML and misleading filename

Use:

1. well-formed XML with un-namespaced `configuration` document element;
2. well-formed arbitrary XML named `web.config`;
3. non-XML bytes named `app.config`.

Then:

- only item 1 is established as classic .NET XML configuration;
- item 2 is not configuration solely from filename;
- item 3 is not made a malformed configuration candidate solely from filename.

Covers: AC-S02-003, 010, 021, 022.

### S09 — Content-level malformed configuration candidate

Given content that establishes a `<configuration` family candidate but fails required XML well-formedness, when Stage 02 runs, then it may be represented as malformed configuration candidate rather than successful configuration or generic filename-based corruption.

Covers: AC-S02-010, 022.

### S10 — Portable PDB positive and negative structure

Use:

1. structurally readable standalone metadata with required `#Pdb` and `#~` streams;
2. a valid different metadata blob named `.pdb`;
3. a content-level Portable-PDB-shaped metadata candidate whose required stream structure is invalid.

Then:

- item 1 is Portable PDB;
- item 2 is not Portable PDB merely from filename or generic metadata signature;
- item 3 is malformed/corrupt Portable PDB candidate.

Covers: AC-S02-003, 011, 022.

### S11 — XML documentation positive and malformed candidate

Use:

1. well-formed XML with `doc` root and direct `members` child;
2. well-formed XML named `.xml` without that shape;
3. content establishing a `<doc` candidate that cannot be parsed.

Then:

- item 1 is XML documentation;
- item 2 is not classified from extension alone;
- item 3 is malformed XML documentation candidate.

Covers: AC-S02-003, 012, 022.

### S12 — `.deps.json` direct contract

Given valid JSON whose root object contains:

- `runtimeTarget` object with string `name`;
- `targets` object;
- `libraries` object;

when Stage 02 runs, then dependency-context format is established regardless of filename.

A valid JSON object missing the canonical required shape is not established as `.deps.json` merely because it is named `app.deps.json`.

Covers: AC-S02-003, 013.

### S13 — `.deps.json` malformed required shape

Given family-specific dependency-context structure whose required canonical member/container shape is invalid, when Stage 02 runs, then it produces malformed dependency-context candidate state rather than successful dependency-context classification.

Covers: AC-S02-013, 022.

### S14 — `.runtimeconfig.json` direct and malformed contracts

Use:

1. valid JSON object with `runtimeOptions` object;
2. valid JSON object with `runtimeOptions` present but not an object;
3. arbitrary valid JSON named `app.runtimeconfig.json`.

Then:

- item 1 is runtime configuration;
- item 2 is malformed runtime-configuration candidate;
- item 3 is not runtime configuration from filename alone.

Covers: AC-S02-003, 014, 022.

### S15 — `packages.config` positive and malformed entries

Use:

1. well-formed XML with `packages` root and package entries carrying non-empty `id` and `version`;
2. a `packages` manifest containing a package entry with a missing/empty required `id` or `version`;
3. unrelated XML named `packages.config`.

Then:

- item 1 is `packages.config`;
- item 2 is malformed packages-config candidate;
- item 3 is not established from filename.

Covers: AC-S02-003, 015, 022.

### S16 — NuSpec positive and malformed manifest structure

Use:

1. well-formed XML with `package` root, direct `metadata`, and non-empty `id` and `version`;
2. NuGet-family package metadata structure missing/invalid required package identity/version;
3. generic `<package>` XML without NuGet metadata structure, named `.nuspec`.

Then:

- item 1 is NuSpec;
- item 2 is malformed NuSpec candidate;
- item 3 is not established from extension or generic root name alone.

Covers: AC-S02-003, 016, 022.

### S17 — NuGet package versus ordinary ZIP

Use:

1. readable ZIP containing a root package NuSpec satisfying S16 item 1;
2. valid ordinary ZIP with no recognized root NuSpec, named `.nupkg`.

Then:

- item 1 is `.nupkg`;
- item 2 is valid non-NuGet ZIP/unsupported format rather than malformed NuGet package.

Covers: AC-S02-003, 017, 020, 021.

### S18 — Material cross-family JSON ambiguity

Given one valid JSON root object that independently satisfies both canonical JSON-family contracts by containing:

- `runtimeOptions` as an object; and
- `runtimeTarget`, `targets`, and `libraries` with the required `.deps.json` shapes, including string `runtimeTarget.name`,

when Stage 02 runs, then both incompatible family contracts are directly satisfied and the Artifact remains `Ambiguous`.

Changing recognizer registration/order must not cause either `.deps.json` or `.runtimeconfig.json` to win.

Covers: AC-S02-013, 014, 019, 028.

### S19 — Unsupported/unrecognized readable content

Given successfully preserved readable bytes that satisfy none of the supported or otherwise recognized Stage 02 family contracts, when Stage 02 runs, then:

- the Artifact remains inventoried/preserved;
- explicit unsupported/unrecognized state is retained;
- no guessed family, malformed state, or Stage 01 preservation failure is fabricated.

Covers: AC-S02-021, 022, 025.

### S20 — Operational classifier failure beside successful Artifact

Given two eligible preserved Artifacts where:

- Artifact A classifies successfully;
- classification of Artifact B fails due to an injected operational/tool/read/resource failure unrelated to content validity,

when Stage 02 runs, then:

- Artifact A's trustworthy result remains usable;
- Artifact B receives operational classification failure at the narrowest reliable scope;
- Artifact B is not labeled malformed/unsupported merely because the operation failed;
- the run/current state truthfully reflects incomplete/partial Stage 02 progress where applicable.

Covers: AC-S02-023, 026.

### S21 — Cancellation remains cancellation

Given Stage 02 is cancelled during classification before successful durable publication, then:

- cancellation is propagated as cancellation;
- it is not converted into unsupported, malformed, ambiguous, or normal operational classification outcome merely to complete the run;
- no partial successful Stage 02 checkpoint is published.

Covers: AC-S02-023, 029.

### S22 — Reopen after original source tree removal

Given successful Stage 01 preservation and successfully committed Stage 02 classification, after deleting the original supplied directory and reopening the recovery workspace:

- Artifact identity/provenance/preservation remain intact;
- Stage 02 classification remains available;
- classification replay/open does not require the original external source path.

Covers: AC-S02-002, 025, 026.

### S23 — Partial Stage 01 flow-forward

Given a DEV-SPEC-003-style `Partial` Stage 01 state containing:

- one positively discovered and successfully preserved Artifact; and
- one descendant-discovery failure scope,

when Stage 02 runs:

- the preserved Artifact remains classifiable;
- the Stage 01 incomplete-discovery diagnostic remains intact;
- no hypothetical Artifact or classification is created for undiscovered descendants.

Covers: AC-S02-001, 025, 027, 030.

### S24 — Failed preservation beside eligible Artifact

Given:

- one admitted Artifact successfully preserved; and
- one admitted Artifact whose preservation failed,

when Stage 02 runs:

- the preserved Artifact is eligible for classification;
- the failed-preservation Artifact receives no fabricated byte-derived classification;
- its Stage 01 preservation diagnostic remains unchanged.

Covers: AC-S02-001, 025.

### S25 — Stage 02 does not leak Stage 03 data

Given a managed assembly whose recognizer must inspect metadata sufficient to determine reference/satellite/implementation status, after Stage 02 completes:

- classification-bearing state is persisted;
- unrelated type/member/reference/method-body data encountered during recognition is not persisted as Stage 03 recovery knowledge.

Repeat analogously for a structured JSON/XML/package family where the recognizer reads internal structure required for recognition.

Covers: AC-S02-024.

### S26 — Equivalent recognizer and Artifact ordering

Given equivalent eligible Artifact bytes/configuration processed with:

- different Artifact enumeration order; and/or
- different recognizer invocation/registration order,

the substantive Stage 02 results are equivalent, including specificity, ambiguity, outcome category, and semantically material diagnostics.

Covers: AC-S02-018, 019, 028.

### S27 — Stage 01 checkpoint remains historical

Given an existing Stage 01 Recovery Checkpoint, when Stage 02 classification is durably committed:

- the Stage 01 checkpoint and Recovery Intake Snapshot remain unchanged;
- later current recovery state identifies Stage 02 classification-bearing progress separately;
- reopening the workspace preserves both historical Stage 01 state and current Stage 02 state.

Covers: AC-S02-025, 026.

### S28 — Failed Stage 02 commit is atomic from the current-state perspective

Given an existing stable Stage 01/current checkpoint and a Stage 02 persistence failure during commit:

- the operation is not reported as successfully committed;
- the prior stable current checkpoint remains authoritative;
- no partially published Stage 02 classification-bearing current state is visible after reopen.

Covers: AC-S02-029.

### S29 — Full Stage 01 regression

Run all existing DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 acceptance tests plus existing supporting tests after Stage 02 implementation.

Covers: AC-S02-030.

### Scenario-to-acceptance mapping summary

| Scenario | Acceptance propositions |
|---|---|
| S01 | 001, 002, 003, 004, 005, 008, 018, 024, 025 |
| S02 | 003, 009, 018 |
| S03 | 004, 009, 022 |
| S04 | 004, 005, 020 |
| S05 | 005, 006, 007, 008, 018 |
| S06 | 003, 006, 018 |
| S07 | 003, 007, 008, 018 |
| S08 | 003, 010, 021, 022 |
| S09 | 010, 022 |
| S10 | 003, 011, 022 |
| S11 | 003, 012, 022 |
| S12 | 003, 013 |
| S13 | 013, 022 |
| S14 | 003, 014, 022 |
| S15 | 003, 015, 022 |
| S16 | 003, 016, 022 |
| S17 | 003, 017, 020, 021 |
| S18 | 013, 014, 019, 028 |
| S19 | 021, 022, 025 |
| S20 | 023, 026 |
| S21 | 023, 029 |
| S22 | 002, 025, 026 |
| S23 | 001, 025, 027, 030 |
| S24 | 001, 025 |
| S25 | 024 |
| S26 | 018, 019, 028 |
| S27 | 025, 026 |
| S28 | 029 |
| S29 | 030 |

---

## Executable verification

### Acceptance suite

Create:

`tests/LegacyRevive.AcceptanceTests/Development/DevSpec005Stage02ArtifactFormatAndFamilyClassificationTests.cs`

The acceptance suite must exercise the observable application/recovery-workspace behavior for AC-S02-001 through AC-S02-030.

It must include real or deterministically generated byte fixtures sufficient to prove the canonical recognition contracts rather than mocking the family decision itself.

Mocks/fakes may be used for operational-failure, cancellation, or persistence-failure injection where the acceptance question is the Stage 02 application boundary rather than the parser's structural truth condition.

### Minimum acceptance coverage

The executable acceptance suite must include:

- all positive canonical family contracts;
- misleading/missing extension cases proving content-first classification;
- managed CLI versus native PE;
- managed CLI module/netmodule valid-but-unsupported;
- broad managed assembly without subtype overclaim;
- exact reference-assembly attribute identity;
- satellite/resource structural recognition;
- classic XML configuration positive/negative/malformed candidate;
- Portable PDB positive/negative/malformed candidate;
- XML documentation positive/negative/malformed candidate;
- `.deps.json` positive and malformed-shape candidate;
- `.runtimeconfig.json` positive and malformed-shape candidate;
- `packages.config` positive and malformed-entry candidate;
- `.nuspec` positive and malformed-manifest candidate;
- `.nupkg` positive and ordinary-ZIP boundary;
- explicit cross-family ambiguity;
- unsupported/unrecognized content;
- operational classification failure;
- cancellation;
- Stage 03 non-persistence boundary;
- `Partial` Stage 01 flow-forward;
- failed-preservation exclusion;
- reopen after original source deletion;
- deterministic recognizer/Artifact ordering;
- Stage 01 historical checkpoint preservation;
- failed Stage 02 commit publication behavior;
- full DEV-SPEC-001/002/003 regression.

### Acceptance assertion boundary

Prefer assertions against:

- Application-level classification behavior;
- durable classification state through supported application abstractions;
- stable Artifact identity/provenance/preservation state;
- current versus historical checkpoint behavior;
- explicit classification outcomes/candidates/diagnostics.

Do not make acceptance depend unnecessarily on:

- exact SQLite table/column names;
- private recognizer class names;
- helper decomposition;
- parser call order;
- internal serialization;
- incidental fixture filenames except where the scenario deliberately proves filename insufficiency.

---

## Supporting verification

### Domain verification

Add focused Domain tests for any introduced classification-state model/invariants, including as applicable:

- Stage 02 classification remains associated with an existing `ArtifactId`;
- preservation and classification remain distinct;
- `Established` / broader / valid-but-unsupported / unsupported-unrecognized / ambiguous / malformed-candidate / operational-failure states satisfy their intended invariants;
- ambiguity can retain multiple materially incompatible directly supportable candidates;
- successful specific subtype state is compatible with canonical hierarchy semantics;
- a later classification record does not mutate Original Artifact identity/provenance/preservation state;
- diagnostic/result invariants do not collapse operational failure into content invalidity.

Exact Domain type names are implementation discretion.

### Application verification

Add focused Application tests for:

- Stage 02 eligibility;
- preserved-byte access;
- `Completed` and `Partial` Stage 01 flow-forward;
- failed-preservation exclusion;
- orchestration determinism;
- operational failure scoping;
- cancellation;
- persistence invocation/publication behavior;
- later checkpoint/current-state progress;
- reopen/replay without the original source tree;
- no Stage 01 historical rewrite.

### Analyzer/recognizer verification

Concrete recognizer tests must directly prove the canonical §5A.5 structural contracts.

At minimum provide focused recognizer coverage for every family in AC-S02-004 through AC-S02-017, including the negative/corrupt/unsupported boundaries that materially distinguish those contracts.

Tests must not define the recognition rule by simply asserting library/parser defaults. Where a library is used, the test must demonstrate that LegacyRevive.NET maps the inspected structure to the canonical contract rather than exposing the library's own category as product semantics.

### Persistence verification

Extend local persistence verification to demonstrate:

- Stage 02 classification round-trip;
- migration/reopen from the accepted DEV-SPEC-003 workspace schema/state;
- classification remains associated with the correct existing `ArtifactId`;
- earlier Stage 01 checkpoint/snapshot/history remains unchanged;
- later classification-bearing current state/checkpoint is durable;
- failed save/commit does not partially publish canonical Stage 02 current state;
- reopen retains Stage 02 classification;
- original external supplied path is not required;
- schema evolution does not silently rewrite historical Stage 01 state.

### Tooling verification

For managed PE/metadata recognition:

- use the accepted `System.Reflection.Metadata` / `PEReader` baseline where appropriate;
- do not execute supplied binaries;
- do not use runtime assembly loading as the primary classification model when metadata inspection is sufficient.

Use BCL/platform parsing capabilities where sufficient for XML/JSON/ZIP mechanics unless an already accepted dependency is appropriate.

Any new material dependency or tooling semantic discovered during implementation must be governed rather than introduced by convenience.

### Regression verification

Run the complete solution test suite.

Minimum verification requirement before `Implemented`:

- every AC-S02 proposition has executable acceptance coverage;
- every acceptance-bearing scenario required by this specification passes;
- focused Domain/Application/analyzer/persistence tests pass;
- all existing DEV-SPEC-001/002/003 acceptance tests pass;
- all existing supporting tests pass;
- no skipped acceptance-bearing test is used to claim completion.

The supplied starting regression baseline is 65/65 passing; completion is not defined by a required final test count because implementation may add a different number of focused tests.

---

## Qualifications discovered

None.

This specification's readiness review found no new material ambiguity, contradiction, unsupported assumption, boundary issue, design question, or challenge to an existing accepted decision that must be resolved before implementation.

The following are already governed and resolved:

- RMAT-009 / D172 — family-specific direct recognition contracts;
- DEV-002 / D173 — semantic-readiness gate;
- DEV-003 / D174 — rejection/history/replacement lifecycle.

If implementation exposes a material choice outside the canonical determination recorded in the Readiness assessment, implementation must stop at that semantic boundary and register the issue in `91-design-qualification-register.md`.

---

## Verification status

Historical implementation verification completed with 127/127 tests passing and 0 failed / 0 skipped, but the current completion disposition is **Rejected** following post-implementation review on 2026-10-01.

The historical passing run is retained as evidence of what the then-current executable suite exercised. It no longer establishes that DEV-SPEC-005 is an accepted completed slice because later review found acceptance-bearing implementation defects that the suite did not detect.

Actual executable acceptance verification:

- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec005Stage02ArtifactFormatAndFamilyClassificationTests.cs`
  - AC-S02-001 through AC-S02-030;
  - representative scenarios S01 through S29.

Actual supporting verification:

- `tests/LegacyRevive.Domain.Tests/Artifacts/ArtifactClassificationTests.cs`
  - classification outcome/candidate invariants, existing Artifact identity, and preservation/provenance separation;
- `tests/LegacyRevive.Application.Tests/Classification/ArtifactClassificationServiceTests.cs`
  - eligibility, Partial flow-forward, failed preservation exclusion, preserved-byte integrity, orchestration, scoped operational failure, cancellation, and failed publication;
- `tests/LegacyRevive.AcceptanceTests/Supporting/StructuralArtifactFormatClassifierTests.cs`
  - direct verification of every canonical §5A.5 family contract and content-based negative/malformed boundaries;
- `tests/LegacyRevive.AcceptanceTests/Supporting/Stage02PersistenceTests.cs`
  - classification round-trip, DEV-SPEC-003-shaped schema migration, Stage 01 historical stability, and transactional rollback;
- existing DEV-SPEC-001/002/003 acceptance and supporting suites
  - complete Stage 01 regression.

Verification result (Debug and Release):

- Test projects: 3
- Total tests: 127
- Passed: 127
- Failed: 0
- Skipped: 0
- Complete solution: Passed

Historical Definition-of-Done audit recorded at the 2026-09-30 `Implemented` transition:

- semantic readiness remains `Pass`; no implementation-originated material product semantic was introduced;
- included/excluded scope remains intact and no Stage 03 reusable semantic extraction is persisted;
- every canonical family contract, hierarchy/broader result, ambiguity, valid-but-unsupported, unsupported/unrecognized, malformed/corrupt, and operational-failure boundary is directly verified;
- Stage 01 Artifact identity, provenance, preservation state, discovery diagnostics, intake snapshot, and earlier checkpoint remain unchanged and replayable;
- Stage 02 classification is durable, associated with the existing `ArtifactId`, and reopens without the original supplied source;
- cancellation and failed persistence do not publish a partial successful Stage 02 current checkpoint;
- recognizer and Artifact ordering do not alter substantive classification;
- DEV-SPEC-001/002/003 acceptance and all supporting verification remain green;
- no acceptance-bearing test is skipped and no blocking qualification remains.

The 2026-10-01 post-implementation review invalidates that historical completion judgment: the passing suite did not expose the AC-S02-017 and AC-S02-022 nonconformances recorded in the Rejection disposition below. The historical audit remains recorded because the lifecycle history must not be rewritten.

Pre-slice accepted regression baseline supplied with this specification work:

- Test projects: 3
- Total tests: 65
- Passed: 65
- Failed: 0
- Skipped: 0

Historical planned executable acceptance location:

`tests/LegacyRevive.AcceptanceTests/Development/DevSpec005Stage02ArtifactFormatAndFamilyClassificationTests.cs`

Historical planned supporting verification areas:

- Domain classification-state invariants;
- Application Stage 02 orchestration;
- format/family recognizers;
- SQLite persistence/history/reopen;
- DEV-SPEC-001/002/003 regression;
- complete solution regression.

These planning entries are retained as historical specification text. Actual implementation verification paths/results are recorded earlier in this section; the later rejection disposition records why those passing results no longer establish accepted completion.

---

## Implementation constraints

These constraints are subordinate to the canonical owners and are included because they materially protect the accepted boundaries.

### Domain/Core

- Represent only the semantic concepts/invariants required by Stage 02 classification.
- Do not depend on SQLite, `System.Reflection.Metadata`, filesystem adapters, JSON/XML/ZIP parser implementations, or other concrete external tooling.
- Do not introduce Stage 03 Observation/Recovered Fact semantics merely to store classification.

### Application

- Stage 02 remains a separate application capability from Stage 01 intake.
- Do not fold classification into `ArtifactIntakeService` merely because intake already has Artifact access.
- Application coordinates:
  - eligible Artifact selection;
  - preserved-byte access;
  - classification capability invocation;
  - deterministic result collection;
  - durable classification progress;
  - checkpoint/current-state publication;
  - failure/cancellation handling.
- Application does not embed concrete parser/tool logic.

### Capability/adapters

- Concrete recognizers inspect untrusted preserved bytes behind inward-owned capability boundaries.
- Classification is read-only.
- Supplied binaries are not executed.
- Tool/library behavior is adapted to LegacyRevive.NET semantics; it does not define those semantics.

### Persistence

- Stage 02 classification is later recovery state associated with existing Artifacts.
- It must not be encoded by rewriting Stage 01 preservation truth.
- Physical schema evolution is implementation discretion only if:
  - stable Artifact identity is preserved;
  - earlier history remains replayable;
  - current-state publication is atomic enough to satisfy AC-S02-029;
  - Stage 01 and Stage 02 semantic distinctions remain visible.

### Security/integrity

- Preserve immutable workspace-owned Original Artifact bytes.
- Preserve workspace path-boundary protections.
- Treat all inspected inputs as untrusted.
- Parser exceptions/crashes must not be converted into false successful classification or false corruption claims.
- Preserve cancellation semantics.

---

## Definition-of-Done conditions for this slice

DEV-SPEC-005 may transition to `Implemented` only when all applicable canonical Development Process requirements and the following slice-specific conditions are satisfied:

1. semantic-readiness remains `Pass`;
2. no implementation-originated material product semantic has bypassed governance;
3. AC-S02-001 through AC-S02-030 are satisfied;
4. all required representative acceptance scenarios are executable and passing;
5. every canonical MVP family recognition contract in scope has direct recognizer verification;
6. hierarchy, broader classification, ambiguity, valid-but-unsupported, unsupported/unrecognized, malformed/corrupt, and operational-failure boundaries are all verified;
7. Stage 02/Stage 03 non-leakage is verified;
8. Stage 01 Artifact identity/provenance/preservation semantics remain unchanged;
9. Stage 01 checkpoint/snapshot/history remains replayable and unrewritten;
10. Stage 02 classification survives durable reopen without the original supplied source tree;
11. failed commit/cancellation does not publish partial successful current Stage 02 state;
12. all DEV-SPEC-001/002/003 acceptance behavior remains passing;
13. complete solution verification passes with no acceptance-bearing skips;
14. actual verification locations/results are recorded in this document;
15. any material qualification discovered during implementation has completed the required governance lifecycle before completion is claimed;
16. lifecycle history is updated consistently with `16-development-process.md`.

Source-control commit is not added here as a slice-specific completion condition. Repository/source-control practice remains separate from DEV-SPEC lifecycle unless another applicable repository rule explicitly requires it.

---

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| 2026-09-30 | Created → Draft | Created as the new replacement Stage 02 format/family-classification specification required after DEV-SPEC-004 was rejected under DEV-003 / D174. Derived from the now-settled RMAT-009 / D172 recognition semantics and the accepted post-DEV-SPEC-003 repository baseline. |
| 2026-09-30 | Draft → Ready for Implementation | Canonical-source review, per-family acceptance derivation, representative scenario design, repository-baseline check, architecture/tooling/MVP audit, and D173 material semantic choice test completed. No unresolved material semantic choice or new blocking qualification remains; semantic readiness recorded as `Pass`. |

---

| 2026-09-30 | Ready for Implementation → In Progress | Implementation commenced against the accepted AC-S02-001 through AC-S02-030 baseline after confirming the 65/65 pre-slice regression baseline and revalidating semantic readiness. |
| 2026-09-30 | In Progress → Verification Pending | Production implementation, executable acceptance, supporting verification, persistence migration/replay work, and full Debug/Release regression were completed; final Definition-of-Done audit commenced. |
| 2026-09-30 | Verification Pending → Implemented | AC-S02-001 through AC-S02-030, S01 through S29, focused Domain/Application/recognizer/persistence verification, Stage 01 regression, and the complete Definition-of-Done audit passed at 127/127 tests with 0 failed and 0 skipped in both Debug and Release. |
| 2026-10-01 | Implemented → Rejected | Post-implementation review established that the completion judgment was materially unsound: the implementation diverged from accepted AC-S02-017 handling for valid ZIPs containing an unrecognized/invalid root NuSpec, and malformed XML/JSON candidate detection could overclaim known-family evidence through loose substring matching contrary to AC-S02-022. The 127/127 result remains historical verification evidence, but DEV-SPEC-005 is no longer accepted as a completed Stage 02 slice. No new canonical semantic ambiguity was discovered; the defects are implementation/verification nonconformance against already-determined semantics. |

---

## Rejection disposition

DEV-SPEC-005 is rejected under the existing DEV-003 / D174 post-Implemented rejection model. The historical `Implemented` transition and 127/127 verification result remain part of the durable record; they are not rewritten or deleted.

The rejection is based on post-implementation review finding that the executable verification failed to expose two acceptance-bearing implementation defects:

1. **NuGet package boundary (AC-S02-017).** A readable ZIP containing a root `.nuspec` that does not itself satisfy the canonical NuSpec recognition contract is currently promoted to malformed/corrupt NuGet-package candidate state. The accepted contract requires NuGet-package classification only when the root NuSpec is recognized; otherwise a valid ZIP remains valid non-NuGet ZIP/unsupported content rather than being fabricated as malformed NuGet merely because an unrecognized `.nuspec` entry exists.
2. **Malformed candidate threshold (AC-S02-022 and affected XML/JSON family contracts).** After parser failure, current malformed-family candidate detection relies on loose textual substring checks for element/property names. Those checks can establish malformed known-family state from incidental text/prefix matches without sufficient content-level evidence of the relevant family candidate. The accepted contract requires defensible content-level known-format support before malformed/corrupt candidate state is claimed.

These findings do **not** introduce a new product semantic or reopen RMAT-009 / D172. The canonical recognition rules already determine the required behavior, so no new `91-design-qualification-register.md` entry is required for the defects themselves. They are implementation and verification defects against the existing acceptance contract.

### Implementation / repository disposition

The DEV-SPEC-005 production and test changes are currently uncommitted and unpushed. They are therefore **not accepted repository baseline state**.

They are intentionally retained in the local working tree as unaccepted work-in-progress material for the future replacement Development Slice Specification. Sound portions may be reused by that replacement, but none of the retained DEV-SPEC-005 implementation is considered accepted merely because it remains present locally or because the historical suite passed.

For this capability, the accepted implementation baseline remains the post-DEV-SPEC-003 state until a future replacement DEV-SPEC independently re-establishes the complete Stage 02 Artifact format/family-classification implementation and verification record.

The retained working-tree implementation must not be committed or pushed as accepted Stage 02 implementation under DEV-SPEC-005. A future replacement must use a new DEV-SPEC identity, correct the identified defects, add acceptance coverage for the missed boundaries, and re-verify the complete Stage 02 slice before the resulting implementation is accepted and committed.

### Future replacement

No replacement DEV-SPEC has yet been created in this document update, so `Superseded by` remains `None`. Under the canonical Development Process, any future replacement for this rejected specification must use a new DEV-SPEC and may link back through `Supersedes` / `Superseded by` while DEV-SPEC-005 remains `Rejected`.

---

## Replacement lineage

DEV-SPEC-005 is the implementation replacement for rejected DEV-SPEC-004.

This lineage does not alter DEV-SPEC-004's terminal historical status from `Rejected` to `Superseded`. DEV-SPEC-004 retains its own historical lifecycle, including its earlier `Implemented` transition and later `Rejected` disposition.

The old 77/77 DEV-SPEC-004 verification result is historical evidence only about the then-defined rejected contract. It is not evidence that Stage 02 is currently implemented and is not an acceptance baseline for DEV-SPEC-005.

DEV-SPEC-005 used the accepted repository baseline after DEV-SPEC-003 and established its own historical implementation/verification record, but that completion judgment is now rejected. A future replacement must independently establish the accepted Stage 02 implementation and verification record.
