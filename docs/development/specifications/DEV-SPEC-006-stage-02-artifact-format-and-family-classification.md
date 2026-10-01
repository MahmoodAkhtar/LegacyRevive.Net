# DEV-SPEC-006 — Stage 02 Artifact Format and Family Classification

Status: Rejected  
Blocked by: None  
Supersedes: DEV-SPEC-005  
Superseded by: None

---

## Purpose

Implement the complete accepted Stage 02 Artifact format/family-classification slice for the current MVP against the now-settled canonical recognition, malformed-candidate, semantic-readiness, and boundary/falsification rules.

This specification replaces rejected DEV-SPEC-005. It is not a narrow two-defect patch. It must independently establish the complete accepted Stage 02 classification implementation and verification record.

The current local working tree may contain uncommitted implementation/test material created under rejected DEV-SPEC-005. That material may be mechanically reused where it conforms to this specification, but:

- it is not accepted implementation baseline state;
- its historical 127/127 verification result provides no acceptance credit for DEV-SPEC-006;
- every reused behavior remains subject to this specification's acceptance contract, D175 malformed-candidate thresholds, D176 boundary/falsification matrix, and complete Definition of Done;
- DEV-SPEC-006 must produce its own durable verification record before the resulting Stage 02 implementation may be accepted.

---

## Canonical basis

This slice is governed by the current canonical Project source set, especially:

- `01-purpose-and-principles.md` — trustworthy progressive recovery and visible uncertainty;
- `02-terminology.md` — canonical recovery terminology;
- `03-evidence-model.md` — evidence/provenance and epistemic-category boundaries;
- `06-recovery-matrix.md` §5A — Stage 02 format/family recognition contracts;
- `06-recovery-matrix.md` §5A.5.1 — malformed/corrupt family-candidate discriminator thresholds;
- `07-recovery-process.md` §4 — Stage 02 workflow, outcome handling, diagnostic scoping, and Stage 02/Stage 03 boundary;
- `12-architecture.md` — workspace-owned immutable artifact bytes, application/domain boundaries, persistence/history/replay responsibilities;
- `13-tooling-and-dependencies.md` — selected inspection/persistence/test technologies and dependency boundaries;
- `14-mvp.md` — MVP Artifact-family/analyzer scope and Stage 02 obligations;
- `16-development-process.md` §§12A, 13.1, 14, 17.2, 20, 22 and 25 — semantic readiness, D176 boundary/falsification verification, executable verification, readiness, verification status, Definition of Done, and DEV-SPEC template;
- `90-decisions.md`:
  - D047 — Artifact-Type Capability Does Not Imply Artifact-Instance Content;
  - D048 — Artifact Format Must Be Classified Beyond File Extension;
  - D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process;
  - D055 — Stage Progression Does Not Promote Epistemic Category;
  - D056 — Recovery Reevaluates from the Earliest Materially Affected Stage;
  - D106 — Canonical Recovery State Areas Remain Logically Separated;
  - D107 — Artifact Analyzers Emit Direct Observations and Diagnostics, Not Hidden Inference;
  - D113 — Recovery Checkpoints Preserve Stable Replayable History;
  - D114 — Diagnostics Support Local Failure and Partial Progress;
  - D116 — Persistence Technology Is Replaceable but Must Preserve Canonical History and Provenance;
  - D119 — `System.Reflection.Metadata` Is the Primary Managed Metadata Extraction API;
  - D121 — Portable PDB Is Primary and Native Windows PDB Support Is Optional;
  - D124 — NuGet Package Inspection Is Local-First and Remote Feed Access Is Explicit Enrichment;
  - D125 — SQLite via `Microsoft.Data.Sqlite` Is the Default Local Persistence;
  - D126 — Platform JSON and XML APIs Are the Structured Interchange Baseline;
  - D130 — xUnit v3 Is the Baseline Automated Test Framework;
  - D134 — MVP Uses Local Directory Recovery Input with Defined Managed Artifact Analyzers;
  - D141 — MVP Recovery Workspaces Are Persistent and Resumable;
  - D143 — MVP Preserves Partial, Blocked, Workable, and Validated Outcome Semantics;
  - D167 — MVP Original Artifacts Use Workspace-Owned Immutable Byte Snapshots;
  - D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications;
  - D170 — Intake Discovery Failure Preserves Known State Without Fabricating Artifacts;
  - D171 — Positive Stage 01 File Discovery Survives Later Descendant-Discovery Failure;
  - D172 — Stage 02 Artifact Classification Uses Direct Format-Specific Recognition Contracts;
  - D173 — Development Slices Require Semantic Readiness Before Implementation;
  - D174 — Rejected DEV-SPECs Preserve Unsound Implemented Completion Judgments;
  - D175 — Malformed Family Claims Require Direct Structural Candidate Discriminators;
  - D176 — Recognition/Classifiers Require Boundary-Falsification Verification;
- `91-design-qualification-register.md`:
  - RMAT-009 — Resolved;
  - RMAT-010 — Resolved;
  - DEV-002 — Resolved;
  - DEV-003 — Resolved;
  - DEV-004 — Resolved.

### Replacement lineage

DEV-SPEC-006 replaces rejected DEV-SPEC-005 as the specification that may establish the accepted Stage 02 classification implementation.

DEV-SPEC-005 remains historically `Rejected`; this replacement relationship does not rewrite its earlier lifecycle or historical 127/127 verification result.

DEV-SPEC-004 likewise remains historically `Rejected` and is not an implementation baseline for this slice.

### Accepted implementation baseline

The durable accepted implementation history before this Stage 02 slice is DEV-SPEC-001 through DEV-SPEC-003.

The local working tree may contain uncommitted DEV-SPEC-005-derived Stage 02 code and tests. For DEV-SPEC-006 they are treated as unaccepted WIP only. The implementer must inspect and either:

- retain/rework a piece because it satisfies this specification and current canonical semantics; or
- replace/remove it because it does not.

No rejected test expectation or implementation choice is authoritative merely because it already exists.

---

## Scope

### Included

- Determine Stage 02 classification eligibility for admitted Stage 01 Artifacts.
- Classify only from trustworthy workspace-owned preserved bytes.
- Retain Artifact identity, supplied provenance, SHA-256 content identity, preserved-byte reference, and Stage 01 preservation state.
- Evaluate the canonical `06-recovery-matrix.md` §5A successful-recognition contracts for:
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
- Evaluate the `06-recovery-matrix.md` §5A.5.1 / D175 malformed/corrupt family-candidate discriminators for applicable families.
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
- Permit deterministic partial structural/token inspection only as required to establish governed recognition/candidate discriminators.
- Persist only classification-bearing state and classification diagnostics from this slice.
- Associate durable Stage 02 classification state with the existing `ArtifactId`.
- Preserve diagnostics at the narrowest reliable Artifact/classification scope.
- Allow trustworthy successfully preserved Artifacts from a `Partial` Stage 01 run to proceed.
- Exclude admitted Artifacts with failed preservation from byte-derived classification when no trustworthy preserved representation exists.
- Preserve Stage 01 history/checkpoints/snapshots rather than rewriting them as though Stage 02 knowledge existed at intake.
- Persist later classification-bearing recovery progress and support reopen/replay without the original supplied source tree.
- Preserve deterministic substantive classification for equivalent bytes and relevant configuration regardless of incidental recognizer or Artifact processing order.
- Preserve cancellation and persistence-failure semantics without fabricating classification state.
- Establish D176 boundary/falsification verification for every material recognition predicate in this slice.
- Add/rework executable slice-level acceptance verification owned by DEV-SPEC-006.
- Add/rework focused Domain/Application/analyzer/persistence supporting verification.
- Preserve all DEV-SPEC-001 through DEV-SPEC-003 acceptance behavior.

### Excluded

- Stage 03 reusable semantic extraction, including:
  - CLR type/member/signature extraction;
  - assembly-reference/dependency extraction;
  - method-body/IL extraction beyond existence checks required for Stage 02 recognition;
  - PDB document/sequence-point extraction;
  - XML documentation member/text extraction;
  - configuration setting/value extraction;
  - WCF semantic extraction;
  - `.deps.json` dependency-entry extraction;
  - `.runtimeconfig.json` runtime-option extraction;
  - NuGet dependency/content extraction;
  - resource payload extraction.
- Cross-Artifact evidence integration and correlation.
- Establishing companion relationships between a main assembly and satellite/PDB/XML documentation/configuration artifacts; possible relationship identification remains separately bounded work.
- Candidate Project ownership/boundary reconstruction.
- Dependency-representation reconstruction.
- Source reconstruction/decompilation.
- Build/restore/repair.
- Validation and Recovery Metrics.
- External enrichment or remote NuGet lookup.
- Roadmap capabilities.
- Runtime third-party plugin loading.
- Executing supplied binaries.
- Native Windows PDB deep support beyond truthful unsupported/other-format handling required by MVP boundaries.
- Treating filename, extension, relative path, deployment directory, raw substring occurrence, parser order, parser exception, or parser convenience as canonical classification truth.
- Persisting Stage 03 Observations merely because a Stage 02 recognizer reads the relevant structure.
- Prescribing private helper/class names, parser decomposition, buffering strategy, local data structures, concrete SQLite table layout, or other mechanics that preserve accepted semantics.

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

They do not by themselves establish actual Artifact format/family, malformed/corrupt status, reference-assembly status, satellite status, NuGet/NuSpec status, Portable-PDB status, or another Stage 02 classification.

### AC-S02-004 — Managed CLI artifact recognition follows the canonical contract

A managed CLI Artifact is established only when the Artifact is a structurally valid PE/COFF image containing a CLR/CLI header whose referenced CLI metadata root is structurally readable as valid CLI metadata.

`MZ`, `.dll`, or `.exe` alone is insufficient.

A structurally valid PE/COFF with a directly established CLR/CLI header is the minimum malformed-managed candidate discriminator. If the referenced CLI metadata cannot then be structurally read, malformed/corrupt managed candidacy may be emitted; it is not reclassified as native merely because managed parsing failed.

### AC-S02-005 — Managed CLI assembly recognition follows the canonical contract

A managed CLI assembly is established only when the managed CLI Artifact also contains a valid Assembly manifest/table.

A valid managed CLI module without an Assembly manifest remains a valid managed module/netmodule; for MVP purposes it is valid-but-unsupported rather than malformed.

Failure to establish an assembly/subtype condition does not itself create malformed subtype state.

### AC-S02-006 — Reference assembly recognition follows the canonical contract

A reference assembly is established only when a managed CLI assembly contains an assembly-level custom attribute whose metadata-resolved type is exactly `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`.

Simple attribute-name matching, absence of method bodies, or a `ref/`-like path is insufficient.

Absence of the attribute does not by itself establish implementation-assembly status.

### AC-S02-007 — Satellite/resource assembly recognition follows the canonical contract

A satellite/resource assembly is established only when a managed CLI assembly has non-neutral assembly culture, one or more manifest resources, and no executable implementation method bodies.

`.resources.dll` naming, a culture-named directory, or culture alone is insufficient.

A resource-bearing managed assembly that does not satisfy the satellite conditions remains at the broader directly established classification rather than being forced into the satellite subtype or malformed subtype state.

### AC-S02-008 — Managed implementation assembly recognition follows the canonical contract

A managed implementation assembly is established only when a managed CLI assembly is not directly established as a reference assembly, is not directly established as a satellite/resource assembly, and contains direct implementation-bearing evidence such as at least one executable managed method body or an executable entry point backed by implementation metadata.

A managed assembly, absence of `ReferenceAssemblyAttribute`, or `.dll` / `.exe` naming alone is insufficient.

A valid managed assembly lacking sufficient subtype evidence remains `Managed CLI assembly (broad)`.

### AC-S02-009 — Native/non-managed PE recognition follows the canonical contract

A native/non-managed PE is established when PE/COFF structure is valid and no CLR/CLI header establishes a managed CLI Artifact.

A PE containing a directly established CLR/CLI header with malformed required CLI metadata is not classified as native.

### AC-S02-010 — Classic .NET XML configuration recognition follows the canonical contract

Classic .NET XML configuration is established when XML is well formed and the document element is the un-namespaced element `configuration`.

A malformed configuration candidate may be emitted only when token-aware partial XML structure establishes the document-element start tag with exact local name `configuration`; where namespace binding is structurally determinable it must be un-namespaced.

`<configurationBackup`, occurrences in comments/text/attributes, unrelated nested elements, filenames, and raw substring matches are insufficient.

Configuration section/value semantics are deferred to Stage 03.

### AC-S02-011 — Portable PDB recognition follows the canonical contract

Portable PDB is established when standalone ECMA-335-style metadata is structurally readable and contains both required `#Pdb` and `#~` stream-directory entries.

A malformed/corrupt Portable-PDB candidate requires standalone metadata structure established far enough to identify an exact `#Pdb` stream-directory entry before another required Portable-PDB structural condition fails.

A `.pdb` filename, metadata signature alone, `#~` alone, or raw `#Pdb` bytes outside a structurally identified stream entry is insufficient.

A valid different metadata blob is not a malformed Portable PDB. Native Windows PDB support remains optional for the MVP.

### AC-S02-012 — XML documentation recognition follows the canonical contract

XML documentation is established when XML is well formed, the document element is `doc`, and a direct `members` child exists.

Malformed XML-documentation candidacy requires token-aware partial XML structure establishing the document-element start tag with exact local name `doc`.

`<document`, comments/text/attributes, unrelated nested occurrences, and `.xml` filename alone are insufficient.

Member/documentation semantics and assembly correlation are outside this slice.

### AC-S02-013 — `.deps.json` recognition follows the canonical contract

A dependency-context Artifact is established when JSON is well formed, the root is an object, top-level `runtimeTarget`, `targets`, and `libraries` members are objects, and `runtimeTarget.name` is a string.

Malformed dependency-context candidacy requires token-aware partial JSON structure establishing a root object and exact top-level `runtimeTarget`, `targets`, and `libraries` members before a required shape/value condition fails.

A subset of those three members, occurrences in nested objects or string values, isolated/raw property-name text, arbitrary valid JSON, or `.deps.json` filename alone is insufficient.

Dependency-entry and cross-reference semantics are deferred to Stage 03.

### AC-S02-014 — `.runtimeconfig.json` recognition follows the canonical contract

A runtime-configuration Artifact is established when JSON is well formed, the root is an object, and exact top-level `runtimeOptions` exists and is an object.

Malformed runtime-configuration candidacy requires token-aware partial JSON structure establishing a root object and exact top-level `runtimeOptions` before its required object/container shape fails.

`runtimeOptions` text inside a string or nested object, raw textual occurrence, arbitrary valid JSON, or filename alone is insufficient.

Individual runtime-option semantics are deferred to Stage 03.

### AC-S02-015 — `packages.config` recognition follows the canonical contract

`packages.config` is established when XML is well formed, the document element is `packages`, and each direct `package` entry used by the manifest has non-empty `id` and `version` attributes.

Malformed packages-config candidacy requires token-aware partial XML structure establishing document element `packages`; invalid/malformed direct package entries may then support malformed candidacy.

Filename, generic XML, prefix/name collisions, nested/incidental `packages` occurrences, and raw substring matching are insufficient.

Dependency interpretation is deferred to Stage 03.

### AC-S02-016 — `.nuspec` recognition follows the canonical contract

NuSpec is established when XML is well formed, the document element is `package`, a direct `metadata` element exists, and package `id` and `version` are non-empty.

Malformed NuSpec candidacy requires token-aware partial XML structure establishing document element `package` and a direct child `metadata` element before required package identity/version structure fails.

A generic `package` root alone, `.nuspec` filename, namespace/schema URI alone, incidental `metadata` text, prefix/name collisions, or raw substring matching are insufficient.

Full package metadata/dependency extraction and schema-version validation are deferred to Stage 03.

### AC-S02-017 — `.nupkg` recognition follows the canonical contract

NuGet package classification is established when the bytes form a readable ZIP package container and contain a root package `.nuspec` manifest that itself satisfies AC-S02-016 / the canonical successful NuSpec recognition contract.

A readable ZIP containing no recognized NuSpec remains a valid non-NuGet ZIP/unsupported format, including when a root entry merely ends in `.nuspec` but its content does not satisfy the NuSpec successful-recognition contract.

For a corrupt/unreadable ZIP container, malformed NuGet-package candidacy requires partial archive structure that directly establishes a root `.nuspec` entry **and** available manifest bytes that independently satisfy the AC-S02-016 malformed-NuSpec candidate threshold.

A `.nupkg` filename, ZIP signature, raw `.nuspec` bytes, or root `.nuspec` entry name alone is insufficient.

### AC-S02-018 — Most-specific directly established classification wins within a hierarchy

Where directly established classifications form a compatible specialization hierarchy, the current Stage 02 result is the most specific directly established classification.

Where only the broader contract is satisfied, the broader classification remains current.

Recognizer execution order must not create or remove specificity.

### AC-S02-019 — Materially incompatible independently satisfied contracts remain ambiguous

Where two or more materially incompatible family contracts are independently satisfied and neither canonically subsumes the other, Stage 02 preserves `Ambiguous`.

The result retains directly supportable candidate classifications and enough diagnostic/context information to explain the ambiguity.

The implementation must not select by recognizer order, extension, path, parser preference, or convenience.

### AC-S02-020 — Valid-but-unsupported is distinct from malformed/corrupt

A structurally valid known format outside current MVP deep-analysis support remains valid-but-unsupported.

In particular, a valid managed CLI module/netmodule without an Assembly manifest is valid-but-unsupported for the MVP rather than malformed.

Lack of a later analyzer/capability does not convert valid content into corruption.

### AC-S02-021 — Unsupported/unrecognized preserved content remains visible

Successfully preserved content for which no supported or otherwise recognized Stage 02 format/family is established remains a valid preserved Artifact with explicit unsupported/unrecognized Stage 02 state.

It is not discarded, converted into a Stage 01 preservation failure, or assigned a guessed family.

### AC-S02-022 — Malformed/corrupt requires a governed direct family candidate discriminator

A malformed/corrupt family claim is made only when the family-specific discriminator in `06-recovery-matrix.md` §5A.5.1 is directly established independently of the structural condition that failed.

The discriminator must be an exact structural fact at the required format-specific scope. Filename/path, raw substring/byte sequence, partial-name/prefix collision, incidental text/string/payload content, generic outer-format markers, parser rejection, or tool/library error category are insufficient by themselves.

Deterministic partial structural/token inspection is allowed when full parsing fails, but it must establish the governed token/record/relationship at its required structural scope.

If the discriminator is not established, Stage 02 retains a broader valid format where one is directly established; otherwise it preserves unsupported/unrecognized content and diagnostics rather than fabricating malformed family membership.

### AC-S02-023 — Operational classification failure is distinct from content invalidity

A tool/resource/read/adapter failure that prevents classification from completing is represented as operational classification failure rather than unsupported, ambiguous, malformed/corrupt, or successful classification.

The failure remains scoped to the narrowest reliable Artifact/classification boundary.

Trustworthy classification state for unrelated Artifacts is not invalidated merely because another classification attempt fails operationally.

Cancellation remains cancellation and is not translated into a normal classification outcome.

### AC-S02-024 — Stage 02 inspection does not persist Stage 03 semantic content

A Stage 02 recognizer may inspect metadata records, container entries, XML/JSON shape, resources, attributes, method-body existence, or other structural fields only as necessary to evaluate a canonical successful-recognition or malformed-candidate contract.

This slice persists only classification-bearing state and classification diagnostics.

It does not persist reusable Stage 03 semantic content such as CLR type/member metadata, assembly-reference/dependency entries, configuration values, runtime options, PDB documents/sequence points, package dependencies, resource payloads, or comparable direct extraction.

Stage 03 may later reread the same preserved bytes.

### AC-S02-025 — Stage 02 does not mutate Stage 01 Artifact truth

Classification does not change the existing Artifact's `ArtifactId`, supplied relative-path provenance, capture context, SHA-256 content identity, preserved-byte reference, preservation status, or Stage 01 preservation/discovery diagnostics.

Classification state is later recovery knowledge associated with the existing Artifact, not rewritten intake-time truth.

### AC-S02-026 — Stage 02 state is durable and history-preserving

Successfully committed classification state survives reopen/process restart and remains available without the original supplied source tree.

Committing Stage 02 progress does not rewrite the historical Recovery Intake Snapshot or earlier Stage 01 Recovery Checkpoint and does not make Stage 01 appear to have contained classification knowledge.

A later checkpoint does not rewrite an earlier checkpoint.

Exact physical persistence schema is not prescribed.

### AC-S02-027 — Partial Stage 01 state flows forward truthfully

A positively discovered and successfully preserved Artifact retained in a `Partial` Stage 01 run remains eligible for Stage 02 classification.

Existing Stage 01 discovery incompleteness and scoped diagnostics remain intact.

No Artifact or classification is synthesized for undiscovered possible files.

### AC-S02-028 — Substantive classification is deterministic

For equivalent preserved Artifact bytes, relevant recorded Artifact provenance/context, tool/version baseline where semantically relevant, and Stage 02 configuration, the substantive Stage 02 result is equivalent where canonical determinism is practical.

Recognizer order, Artifact processing order, parser registration order, or incidental concurrency scheduling must not change the classification conclusion, ambiguity result, or semantically material diagnostics.

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

The material semantics required by this slice are determined by:

- `06-recovery-matrix.md` §5A / D172 for successful recognition truth conditions, outcome meanings, precedence/ambiguity, and Stage 02 structural-inspection scope;
- `06-recovery-matrix.md` §5A.5.1 / D175 for malformed/corrupt family candidate discriminators and explicit insufficient evidence;
- `07-recovery-process.md` §4 for Stage 02 eligibility, workflow, result/diagnostic persistence boundary, and Stage 02/Stage 03 separation;
- architecture/workspace/persistence decisions for immutable workspace-owned bytes, stable Artifact identity, later recovery state, replay/history, and atomic publication semantics;
- `14-mvp.md` for first-release format/family coverage and valid unsupported preservation;
- D173 for semantic-readiness delegation;
- D176 for the verification discipline used below.

### Material semantic choice test

| Material acceptance area | Canonical determination | Result |
|---|---|---|
| Eligible bytes / Stage 01 prerequisites | Stage 01 + Stage 02 process semantics | Pass |
| Filename/path as classification truth | Explicitly prohibited | Pass |
| Managed CLI / native PE | D172 + D175 | Pass |
| Assembly / module / managed subtypes | D172 + D175 | Pass |
| Reference assembly | exact metadata-resolved assembly-level attribute | Pass |
| Satellite/resource assembly | culture + resources + no executable bodies | Pass |
| Managed implementation assembly | implementation-bearing evidence after excluding directly established reference/satellite | Pass |
| XML configuration successful recognition | exact well-formed root contract | Pass |
| XML configuration malformed candidacy | exact document-element structural discriminator | Pass |
| Portable PDB successful recognition | standalone metadata + `#Pdb` + `#~` | Pass |
| Portable PDB malformed candidacy | structurally identified `#Pdb` stream entry before later required failure | Pass |
| XML documentation successful recognition | `doc` root + direct `members` | Pass |
| XML documentation malformed candidacy | exact `doc` document-element structural discriminator | Pass |
| `.deps.json` successful recognition | required top-level shapes + `runtimeTarget.name` string | Pass |
| `.deps.json` malformed candidacy | root object + all three exact top-level family members | Pass |
| `.runtimeconfig.json` successful recognition | top-level object `runtimeOptions` | Pass |
| `.runtimeconfig.json` malformed candidacy | root object + exact top-level `runtimeOptions` | Pass |
| `packages.config` successful recognition | `packages` root + valid package entries | Pass |
| `packages.config` malformed candidacy | exact `packages` document-element structural discriminator | Pass |
| `.nuspec` successful recognition | `package` root + direct `metadata` + identity/version | Pass |
| `.nuspec` malformed candidacy | exact `package` root + direct `metadata` | Pass |
| `.nupkg` successful recognition | readable ZIP + root successfully recognized NuSpec | Pass |
| readable ZIP with invalid/unrecognized root `.nuspec` | valid non-NuGet ZIP / unsupported, not malformed NuGet | Pass |
| corrupt `.nupkg` candidacy | structured root NuSpec entry + manifest satisfying malformed-NuSpec candidate threshold | Pass |
| Broader-versus-specific precedence | most-specific directly established specialization | Pass |
| Incompatible overlap | explicit `Ambiguous` | Pass |
| Valid-but-unsupported / unsupported / malformed / operational failure | distinct governed outcomes | Pass |
| Stage 02 / Stage 03 boundary | classification-bearing state only | Pass |
| History / replay / publication | later append-preserving recovery state | Pass |
| Partial Stage 01 flow-forward | trustworthy preserved Artifacts remain eligible | Pass |
| Determinism / recognizer order | order must not change substantive result | Pass |
| Epistemic category | direct Artifact-instance classification; no inference promotion | Pass |

No material acceptance area requires implementation to originate new product/design semantics.

### Material semantic questions

None.

All qualifications that previously blocked this slice are resolved:

- RMAT-009 / D172;
- RMAT-010 / D175;
- DEV-002 / D173;
- DEV-003 / D174;
- DEV-004 / D176.

If implementation encounters a material expected outcome that cannot be derived from the current canonical basis, implementation must stop at that boundary and register a new qualification. Tests must not choose the answer.

### Implementation discretion intentionally left open

Provided the acceptance contract and canonical boundaries remain unchanged, implementation may choose:

- private type/helper decomposition;
- recognizer composition and scheduling;
- streaming versus buffering;
- deterministic tokenization/parser mechanics;
- local algorithms/data structures;
- exact enum/value decomposition where semantic distinctions remain observable;
- exact diagnostic-code identifiers where no canonical code is prescribed;
- exact application/capability/repository names;
- exact SQLite schema/migration layout;
- whether existing DEV-SPEC-005 WIP is retained, refactored, or replaced;
- whether intake-oriented persistence code is evolved, split, or generalized;
- fixture-generation mechanics;
- non-persisted parser infrastructure sharing with later stages where Stage 02/Stage 03 semantic outputs remain separate.

---

## Representative scenarios

The scenarios are controlled examples. The canonical requirements and acceptance propositions govern.

### S01 — Trustworthy preserved Artifact eligibility

A successfully preserved Artifact from a Completed Stage 01 run is classified from workspace-owned bytes. A preservation-failed Artifact without trustworthy preserved bytes is not content-classified.

Verifies: AC-S02-001, 002.

### S02 — Partial Stage 01 flow-forward

A trustworthy preserved Artifact from a Partial Stage 01 run is classified while the prior discovery incompleteness and diagnostics remain intact.

Verifies: AC-S02-001, 025, 027.

### S03 — Misleading names do not classify content

Use misleading `.dll`, `.exe`, `.pdb`, `.config`, `.xml`, `.deps.json`, `.runtimeconfig.json`, `.nuspec`, `.nupkg`, and `packages.config` names around content that does not satisfy the applicable structural contract.

Verifies: AC-S02-003 and applicable family contracts.

### S04 — Managed CLI artifact versus malformed managed candidate versus native PE

Use: valid managed CLI; valid PE + CLR header + unreadable CLI metadata; valid native PE without CLR header; `MZ`/extension-only lookalikes.

Verifies: AC-S02-004, 009, 022.

### S05 — Managed assembly versus valid module/netmodule

Use a managed CLI assembly with Assembly manifest and a valid managed CLI module without Assembly manifest.

Verifies: AC-S02-005, 020.

### S06 — Reference assembly exact attribute identity

Use exact fully qualified assembly-level `ReferenceAssemblyAttribute`, a same-simple-name attribute from another namespace/type, and path/body lookalikes.

Verifies: AC-S02-006, 018.

### S07 — Satellite/resource assembly conjunction

Use the successful culture+resources+no-body combination and one-condition-missing cases for culture, resources, and executable bodies.

Verifies: AC-S02-007, 018.

### S08 — Managed implementation evidence

Use directly implementation-bearing assembly, broad managed assembly without implementation-bearing evidence, and absence-of-reference-attribute lookalike.

Verifies: AC-S02-008, 018.

### S09 — Classic configuration exact root and malformed discriminator

Use well-formed un-namespaced `configuration`; malformed exact document-element `configuration`; `<configurationBackup`; comments/text/attributes containing `configuration`; and namespaced `configuration` where namespace status is determinable.

Verifies: AC-S02-010, 022.

### S10 — Portable PDB exact stream structure and malformed discriminator

Use valid Portable PDB; generic valid metadata; metadata with structurally identified `#Pdb` but missing/invalid required `#~`; raw `#Pdb` bytes without stream-directory entry; `.pdb` filename lookalike.

Verifies: AC-S02-011, 022.

### S11 — XML documentation exact root and malformed discriminator

Use well-formed `doc` + direct `members`; malformed exact `doc` document element; `<document`; nested/incidental `doc`; `.xml` filename lookalike.

Verifies: AC-S02-012, 022.

### S12 — `.deps.json` positive, malformed and near-match boundaries

Use valid canonical dependency-context shape; all three exact top-level family members with wrong required shape/value; only two top-level members; required names nested; names inside string values; malformed JSON with only raw text occurrences.

Verifies: AC-S02-013, 022.

### S13 — `.runtimeconfig.json` positive, malformed and near-match boundaries

Use top-level object `runtimeOptions`; top-level non-object `runtimeOptions`; nested `runtimeOptions`; `runtimeOptions` inside a string; malformed raw textual occurrence.

Verifies: AC-S02-014, 022.

### S14 — `packages.config` root and entry boundaries

Use valid `packages` manifest; exact `packages` root with invalid package entry; `<packagesBackup`; nested/incidental `packages`; filename-only lookalike.

Verifies: AC-S02-015, 022.

### S15 — NuSpec successful and malformed candidate boundaries

Use valid `package` + direct `metadata` + id/version; exact `package` + direct `metadata` but invalid/missing id/version; generic `package` without direct metadata; incidental/nested metadata; filename-only lookalike.

Verifies: AC-S02-016, 022.

### S16 — Readable NuGet package versus ordinary ZIP

Use readable ZIP + root successfully recognized NuSpec; ordinary readable ZIP; readable ZIP + root `.nuspec` whose content is generic/unrecognized; readable ZIP + root malformed-NuSpec candidate.

Expected: only the first is NuGet package; all other readable ZIP cases remain broader valid ZIP/unsupported unless another independent contract establishes something else.

Verifies: AC-S02-017, 020, 021, 022.

### S17 — Corrupt NuGet candidate discriminator

Use corrupt archive data with: (a) structured root NuSpec entry + available manifest bytes meeting malformed-NuSpec candidate threshold; (b) `.nuspec` ASCII bytes only; (c) root `.nuspec` entry name only without qualifying manifest candidate; (d) `.nupkg` filename only.

Expected: only (a) may become malformed NuGet-package candidate.

Verifies: AC-S02-017, 022.

### S18 — Most-specific hierarchy

Verify broad managed CLI artifact/assembly results are superseded in current classification by directly established compatible reference/satellite/implementation subtype, while absent subtype evidence leaves the broader result.

Verifies: AC-S02-018.

### S19 — Incompatible overlap ambiguity

Construct a controlled fixture where independent incompatible contracts are supportable and verify explicit ambiguity rather than recognizer-order selection.

Verifies: AC-S02-019, 028.

### S20 — Valid-but-unsupported known format

Use valid managed netmodule and another applicable valid known broader format outside deep MVP analysis.

Verifies: AC-S02-020.

### S21 — Unsupported/unrecognized preserved bytes

Use preserved bytes that satisfy no supported/recognized contract or malformed-family discriminator.

Verifies: AC-S02-021, 022.

### S22 — Parser/tool operational failure versus content invalidity

Inject controlled read/parser-adapter/resource failure and compare with deterministic malformed content.

Verifies: AC-S02-023.

### S23 — Cancellation

Cancel classification before successful publication and verify cancellation is not converted into a classification result.

Verifies: AC-S02-023, 029.

### S24 — Stage 02/Stage 03 output boundary

Classify artifacts whose recognizers must inspect metadata/XML/JSON/package structure and verify only classification-bearing state/diagnostics are persisted.

Verifies: AC-S02-024.

### S25 — Stage 01 truth remains unchanged

Verify Artifact identity/provenance/hash/preserved-byte reference/preservation state and Stage 01 diagnostics remain unchanged after classification.

Verifies: AC-S02-025.

### S26 — Durable reopen and historical checkpoint preservation

Classify, commit, remove original supplied source tree, reopen workspace, and verify Stage 02 current state survives while earlier Stage 01 snapshots/checkpoints remain historically unchanged.

Verifies: AC-S02-002, 026.

### S27 — Recognition-order independence

Evaluate the same fixtures under intentionally varied recognizer registration/execution order and verify equivalent substantive outcomes.

Verifies: AC-S02-018, 019, 028.

### S28 — Artifact-processing-order independence

Classify equivalent artifact sets in different processing order and verify per-Artifact substantive classifications remain equivalent.

Verifies: AC-S02-028.

### S29 — Failed durable publication

Force persistence failure during Stage 02 publication and verify no partially successful current Stage 02 checkpoint is published.

Verifies: AC-S02-029.

### S30 — Stage 01 regression

Run DEV-SPEC-001, DEV-SPEC-002, and DEV-SPEC-003 acceptance suites unchanged.

Verifies: AC-S02-030.

---

## Boundary/falsification verification

This section is mandatory under D176 and `16-development-process.md` §13.1.

Every expected outcome below is derived from the canonical recognition/candidate semantics; the matrix does not create new truth conditions.

`N/A` means the listed D176 category is not independently material for that predicate or is covered by a shared cross-cutting matrix row, with the reason stated.

### Family/predicate matrix

| Acceptance / predicate | Boundary category | Controlled condition | Expected governed outcome | Scenario / executable verification |
|---|---|---|---|---|
| AC-S02-004 Managed CLI | Positive / minimum sufficient | valid PE/COFF + CLR header + readable valid CLI metadata root | Established managed CLI artifact | S04 / DEV-SPEC-006 acceptance |
| AC-S02-004 Managed CLI | Adversarial near-match | `MZ`/`.dll`/`.exe` without valid PE+CLI structure | Not managed CLI from weak signal | S03,S04 |
| AC-S02-004 Managed CLI | Required condition invalid | valid PE + CLR header + unreadable CLI metadata | Malformed/corrupt managed candidate | S04 |
| AC-S02-004 Managed CLI | Valid other/broader | valid PE without CLR header | Native/non-managed PE, not malformed managed | S04 |
| AC-S02-005 Assembly | Positive | valid managed CLI artifact + Assembly manifest/table | Established managed CLI assembly | S05 |
| AC-S02-005 Assembly | Adversarial near-match | valid managed CLI metadata without Assembly manifest | Valid module/netmodule; valid-but-unsupported | S05 |
| AC-S02-006 Reference | Positive | exact metadata-resolved assembly-level `System.Runtime.CompilerServices.ReferenceAssemblyAttribute` | Reference assembly | S06 |
| AC-S02-006 Reference | Adversarial near-match | same simple attribute name but different resolved type/namespace | Not reference assembly from weak name | S06 |
| AC-S02-006 Reference | Lookalike | `ref/` path or no method bodies without exact attribute | Not reference assembly from context/body absence | S03,S06 |
| AC-S02-007 Satellite | Positive | non-neutral culture + manifest resource + no executable bodies | Satellite/resource assembly | S07 |
| AC-S02-007 Satellite | Required condition missing | remove culture OR resource OR no-body condition | Retain broader managed/resource-bearing classification; not satellite | S07 |
| AC-S02-007 Satellite | Adversarial near-match | `.resources.dll` or culture directory only | Not satellite from naming/layout | S03,S07 |
| AC-S02-008 Implementation | Positive | non-reference/non-satellite managed assembly + executable managed body/entry point evidence | Managed implementation assembly | S08 |
| AC-S02-008 Implementation | Adversarial near-match | managed assembly lacking direct implementation-bearing evidence | Broad managed CLI assembly | S08 |
| AC-S02-008 Implementation | Lookalike | absence of ReferenceAssemblyAttribute only | Does not establish implementation subtype | S08 |
| AC-S02-009 Native PE | Positive | structurally valid PE without CLR header | Native/non-managed PE | S04 |
| AC-S02-009 Native PE | Adversarial near-match | PE + CLR header + malformed CLI metadata | Malformed managed candidate, not native | S04 |
| AC-S02-010 Config | Positive | well-formed XML, un-namespaced exact `configuration` document element | Established classic .NET XML config | S09 |
| AC-S02-010 Config | Adversarial near-match | exact text prefix `<configurationBackup` | Not configuration candidate | S09 |
| AC-S02-010 Config | Incidental collision | `configuration` only in comment/text/attribute/nested unrelated content | Not configuration candidate | S09 |
| AC-S02-010 Config | Malformed candidate | malformed XML with token-aware exact `configuration` document-element start; namespace condition satisfied where determinable | Malformed configuration candidate | S09 |
| AC-S02-010 Config | Namespace boundary | determinably namespaced `configuration` root | Not classic un-namespaced config | S09 |
| AC-S02-011 Portable PDB | Positive | readable standalone metadata + exact stream-directory `#Pdb` and `#~` | Portable PDB | S10 |
| AC-S02-011 Portable PDB | Adversarial near-match | generic metadata or `.pdb` filename without `#Pdb` structure | Not Portable PDB | S10 |
| AC-S02-011 Portable PDB | Collision | raw `#Pdb` bytes outside structurally identified stream entry | Not Portable-PDB candidate | S10 |
| AC-S02-011 Portable PDB | Malformed candidate | structurally identified `#Pdb` stream entry but required other Portable-PDB stream structure fails | Malformed/corrupt Portable-PDB candidate | S10 |
| AC-S02-012 XML docs | Positive | well-formed XML + exact `doc` root + direct `members` | XML documentation | S11 |
| AC-S02-012 XML docs | Adversarial near-match | `<document` prefix or `.xml` filename | Not XML-documentation candidate | S11 |
| AC-S02-012 XML docs | Collision | `doc` only nested/in comment/text/attribute | Not XML-documentation candidate | S11 |
| AC-S02-012 XML docs | Malformed candidate | malformed XML with exact token-aware `doc` document-element start | Malformed XML-documentation candidate | S11 |
| AC-S02-013 deps | Positive | root object + top-level runtimeTarget/targets/libraries objects + string runtimeTarget.name | Dependency context | S12 |
| AC-S02-013 deps | Adversarial near-match | only two of three family-defining top-level members | Not dependency-context candidate | S12 |
| AC-S02-013 deps | Collision | names only nested or in string values/raw malformed text | Not dependency-context candidate | S12 |
| AC-S02-013 deps | Malformed candidate | root object + all three exact top-level members, with required shapes/value invalid | Malformed dependency-context candidate | S12 |
| AC-S02-014 runtimeconfig | Positive | root object + exact top-level object `runtimeOptions` | Runtime configuration | S13 |
| AC-S02-014 runtimeconfig | Adversarial near-match | `runtimeOptions` nested or inside string value | Not runtime-config candidate | S13 |
| AC-S02-014 runtimeconfig | Collision | malformed raw text containing `"runtimeOptions"` without structural top-level member | Not runtime-config candidate | S13 |
| AC-S02-014 runtimeconfig | Malformed candidate | root object + exact top-level `runtimeOptions` with non-object shape | Malformed runtime-config candidate | S13 |
| AC-S02-015 packages.config | Positive | exact `packages` root + valid direct package entries | packages.config | S14 |
| AC-S02-015 packages.config | Adversarial near-match | `<packagesBackup` or filename-only | Not packages-config candidate | S14 |
| AC-S02-015 packages.config | Collision | nested/incidental `packages` occurrence | Not packages-config candidate | S14 |
| AC-S02-015 packages.config | Malformed candidate | exact `packages` root + invalid required direct package entry | Malformed packages-config candidate | S14 |
| AC-S02-016 NuSpec | Positive | `package` root + direct `metadata` + non-empty id/version | NuSpec | S15 |
| AC-S02-016 NuSpec | Adversarial near-match | generic `package` root without direct `metadata` | Not NuSpec candidate | S15 |
| AC-S02-016 NuSpec | Collision | `.nuspec` name, schema URI, or incidental/nested `metadata` only | Not NuSpec candidate | S15 |
| AC-S02-016 NuSpec | Malformed candidate | exact `package` root + direct `metadata`; id/version missing/invalid | Malformed NuSpec candidate | S15 |
| AC-S02-017 NuGet | Positive | readable ZIP + root NuSpec satisfying successful NuSpec contract | NuGet package | S16 |
| AC-S02-017 NuGet | Adversarial near-match | readable ZIP + root `.nuspec` entry containing generic/unrecognized XML | Valid ZIP/unsupported, not malformed NuGet | S16 |
| AC-S02-017 NuGet | Valid broader format | readable ZIP with no recognized NuSpec | Valid ZIP/unsupported | S16 |
| AC-S02-017 NuGet | Container/subtype | readable ZIP + root malformed-NuSpec candidate | Valid ZIP/unsupported rather than malformed NuGet because successful NuSpec subtype is not established | S16 |
| AC-S02-017 NuGet | Corrupt-container malformed candidate | corrupt ZIP + structured root NuSpec entry + manifest bytes satisfying malformed-NuSpec candidate threshold | Malformed NuGet-package candidate | S17 |
| AC-S02-017 NuGet | Corrupt-container near-match | corrupt ZIP with raw `.nuspec` bytes or entry-name evidence only | No malformed NuGet candidacy from weak evidence | S17 |

### Cross-cutting D176 matrix

| Acceptance / predicate | Boundary category | Controlled condition | Expected governed outcome | Scenario / executable verification |
|---|---|---|---|---|
| AC-S02-003 | Weak-signal falsification | misleading filename/path/extension for every material family | Weak context may route probes but cannot establish successful/malformed family | S03 |
| AC-S02-018 | Container/subtype / hierarchy | broader contract true, subtype condition absent | Broader classification remains current | S05-S08,S18 |
| AC-S02-018 | Order independence | evaluate compatible hierarchy in different recognizer orders | Same most-specific directly established result | S18,S27 |
| AC-S02-019 | Overlap/ambiguity | incompatible independent contracts both supported | `Ambiguous`; candidates preserved | S19 |
| AC-S02-019 | Order independence | reverse recognizer order for ambiguous fixture | Same ambiguity/candidates | S19,S27 |
| AC-S02-020 | Valid other / unsupported | valid netmodule or other known-but-unsupported format | Valid-but-unsupported, not malformed | S05,S20 |
| AC-S02-021 | No predicate satisfied | preserved bytes establish no family/discriminator | Unsupported/unrecognized; bytes retained | S21 |
| AC-S02-022 | Parser rejection near-match | parser throws without family discriminator | No fabricated malformed family | S09-S17,S22 |
| AC-S02-022 | Raw token collision | family token/name only as raw bytes/text | No malformed family candidacy | S09-S17 |
| AC-S02-023 | Operational failure | injected tool/read/resource failure | Operational classification failure, not content invalidity | S22 |
| AC-S02-023 | Cancellation | cancellation before publication | cancellation semantics preserved; no normal classification | S23 |
| AC-S02-024 | Stage boundary | recognizer reads semantic-bearing structure to classify | Persist classification state/diagnostics only | S24 |
| AC-S02-025 | History boundary | successful classification after Stage 01 | Stage 01 Artifact truth unchanged | S25 |
| AC-S02-026 | Replay/history | reopen after source removal | Stage 02 survives; earlier Stage 01 history unchanged | S26 |
| AC-S02-027 | Partial scope | preserved known Artifact + unresolved descendant discovery | classify known Artifact; retain incomplete discovery truth | S02 |
| AC-S02-028 | Execution order | varied recognizer and Artifact order | Equivalent substantive result | S27,S28 |
| AC-S02-029 | Persistence failure | failure during durable Stage 02 publication | no partial successful current checkpoint | S29 |
| AC-S02-030 | Regression | run prior accepted acceptance suites | DEV-SPEC-001/002/003 remain green | S30 |

### D176 applicability notes

- One-required-condition-missing cases are explicitly required where the recognition predicate is conjunctive and the omission can materially change the classification (for example satellite, deps, NuSpec, Portable PDB). They need not be multiplied into meaningless permutations once independent conditions and the governing fallback are demonstrated.
- Parser/tool/resource/cancellation failure is primarily cross-cutting under AC-S02-023; family-specific tests are additionally required when a parser failure risks being misinterpreted as that family's malformed state.
- Recognizer-order independence is cross-cutting under AC-S02-018/019/028 rather than repeated per family, because D172 defines semantic precedence globally. Family-specific order cases may be added where implementation structure creates a credible additional risk.
- Overlap/ambiguity is N/A for a family predicate where the canonical structure cannot independently overlap another family in the controlled fixture model; the global incompatible-overlap scenario still verifies the semantic rule.
- This matrix is deliberately risk-based, not exhaustive byte-fuzzing. Additional adversarial cases should be added during implementation when code structure exposes a credible false-positive path.

---

## Executable verification

### Required DEV-SPEC-006 acceptance suite

Actual location:

- `tests/LegacyRevive.AcceptanceTests/Development/DevSpec006Stage02ArtifactFormatAndFamilyClassificationTests.cs`

The suite must:

- verify AC-S02-001 through AC-S02-030 at observable slice level;
- exercise S01 through S30;
- exercise every acceptance-bearing D176 matrix row above;
- preserve explicit matrix-to-test traceability;
- contain no skipped acceptance-bearing matrix row at completion;
- verify canonical LegacyRevive.NET outcomes rather than merely asserting third-party parser defaults.

Existing DEV-SPEC-005 acceptance tests may be reused mechanically only after they are moved/rewritten/referenced under DEV-SPEC-006 and shown to cover this specification. The historical DEV-SPEC-005 test suite name/result does not satisfy this requirement by itself.

### Boundary matrix coverage record

The matrix contains 70 acceptance-bearing rows: 52 family/predicate rows and 18 cross-cutting rows. All 70 are executable; none is `Not applicable`, skipped, pending, or blocked.

All identifiers below refer to methods in `DevSpec006Stage02ArtifactFormatAndFamilyClassificationTests` unless a supporting suite is named explicitly.

| Matrix rows | Executable mapping |
|---|---|
| AC-S02-004 Managed CLI (4) | `S04_AC004_AC009_AC022_managed_native_and_malformed_managed_boundaries` |
| AC-S02-005 Assembly (2) | `S05_AC005_AC020_assembly_manifest_distinguishes_assembly_from_valid_netmodule` |
| AC-S02-006 Reference (3) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S06_AC006_reference_assembly_requires_exact_metadata_resolved_attribute_identity` |
| AC-S02-007 Satellite (3) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S07_AC007_satellite_requires_culture_resource_and_no_implementation_body` |
| AC-S02-008 Implementation (3) | `S08_AC008_implementation_requires_direct_implementation_bearing_evidence` |
| AC-S02-009 Native PE (2) | `S04_AC004_AC009_AC022_managed_native_and_malformed_managed_boundaries` |
| AC-S02-010 Configuration (5) | `S09_AC010_AC022_configuration_exact_root_namespace_and_collision_boundaries` |
| AC-S02-011 Portable PDB (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S10_AC011_AC022_portable_pdb_requires_structural_pdb_and_table_stream_entries` |
| AC-S02-012 XML documentation (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S11_AC012_AC022_xml_documentation_requires_doc_root_and_direct_members` |
| AC-S02-013 Dependency context (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S12_AC013_AC022_dependency_context_requires_all_exact_top_level_members_and_shapes` |
| AC-S02-014 Runtime configuration (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S13_AC014_AC022_runtime_configuration_requires_exact_top_level_object_member` |
| AC-S02-015 packages.config (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S14_AC015_AC022_packages_config_requires_exact_root_and_valid_direct_entries` |
| AC-S02-016 NuSpec (4) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S15_AC016_AC022_nuspec_requires_package_direct_metadata_identity_and_version` |
| AC-S02-017 NuGet (6) | `S03_AC003_misleading_names_cannot_establish_any_family`; `S16_AC017_AC020_AC021_AC022_readable_zip_requires_successfully_recognized_root_nuspec`; `S17_AC017_AC022_corrupt_nuget_requires_structured_root_entry_and_manifest_candidacy` |
| AC-S02-003 weak signals (1) | `S03_AC003_misleading_names_cannot_establish_any_family` |
| AC-S02-018 hierarchy/order (2) | `S18_AC018_most_specific_directly_established_hierarchy_result_wins`; `S27_AC018_AC019_AC028_recognizer_order_does_not_change_substantive_results` |
| AC-S02-019 ambiguity/order (2) | `S19_AC019_AC028_incompatible_independent_contracts_are_ambiguous_and_order_independent`; `S27_AC018_AC019_AC028_recognizer_order_does_not_change_substantive_results` |
| AC-S02-020 valid unsupported (1) | `S20_AC020_valid_known_but_unsupported_formats_are_not_malformed` |
| AC-S02-021 no predicate (1) | `S21_AC021_AC022_unrecognized_preserved_content_remains_explicitly_unsupported` |
| AC-S02-022 rejection/collision (2) | `S09` through `S17`; focused cases in `StructuralArtifactFormatClassifierTests` |
| AC-S02-023 operational/cancellation (2) | `S22_AC023_operational_failure_is_not_content_invalidity_and_is_artifact_scoped`; `S23_AC023_AC029_cancellation_propagates_without_publication` |
| AC-S02-024 stage boundary (1) | `S24_AC024_stage02_persists_classification_not_stage03_semantic_content` |
| AC-S02-025 history boundary (1) | `S25_AC025_stage01_identity_provenance_hash_and_checkpoint_truth_are_unchanged` |
| AC-S02-026 replay/history (1) | `S26_AC002_AC026_durable_reopen_uses_preserved_bytes_and_retains_history` |
| AC-S02-027 Partial scope (1) | `S02_AC001_AC025_AC027_partial_intake_flows_forward_without_erasing_incompleteness` |
| AC-S02-028 execution order (1) | `S27_AC018_AC019_AC028_recognizer_order_does_not_change_substantive_results`; `S28_AC028_artifact_processing_order_does_not_change_per_artifact_results` |
| AC-S02-029 persistence failure (1) | `S29_AC029_failed_durable_publication_leaves_previous_checkpoint_authoritative`; `Stage02PersistenceTests.Failed_sqlite_stage02_transaction_does_not_publish_checkpoint_or_partial_classification` |
| AC-S02-030 regression (1) | `S30_AC030_prior_accepted_suites_remain_part_of_complete_solution_regression`; complete DEV-SPEC-001/002/003 suite run |

Every positive material family predicate has an adversarial near-match in the mapped method(s). The focused structural suite duplicates high-risk D175/D176 cases for diagnostic isolation; it does not replace the acceptance mapping above.

---

## Supporting verification

Expected supporting verification includes, where the implementation architecture retains comparable responsibilities:

- Domain tests for classification outcome/candidate invariants, existing Artifact identity, provenance/preservation separation, ambiguity/broader-result semantics, and history-safe current-state transitions;
- Application tests for eligibility, Partial flow-forward, failed-preservation exclusion, preserved-byte integrity, orchestration, scoped operational failure, cancellation, and failed publication;
- Structural recognizer/analyzer tests for every successful family predicate and D175 malformed-candidate discriminator, including token/prefix/raw-substring/collision near-misses;
- persistence tests for classification round-trip, accepted Stage 01 schema evolution, prior-history stability, reopen/replay, and transactional rollback;
- deterministic order tests;
- existing DEV-SPEC-001/002/003 acceptance and supporting suites as full Stage 01 regression.

Suggested locations if compatible with current repository structure:

- `tests/LegacyRevive.Domain.Tests/Artifacts/ArtifactClassificationTests.cs`;
- `tests/LegacyRevive.Application.Tests/Classification/ArtifactClassificationServiceTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Supporting/StructuralArtifactFormatClassifierTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Supporting/Stage02PersistenceTests.cs`.

Exact supporting-test filenames may change without semantic effect; actual locations must be recorded before completion.

Actual supporting verification locations:

- `tests/LegacyRevive.Domain.Tests/Artifacts/ArtifactClassificationTests.cs`;
- `tests/LegacyRevive.Application.Tests/Classification/ArtifactClassificationServiceTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Supporting/StructuralArtifactFormatClassifierTests.cs`;
- `tests/LegacyRevive.AcceptanceTests/Supporting/Stage02PersistenceTests.cs`;
- DEV-SPEC-001/002/003 acceptance classes under `tests/LegacyRevive.AcceptanceTests/Development/`.

The starting accepted regression baseline must be established from the current repository before implementation. The historical DEV-SPEC-005 count of 127/127 is not the accepted pre-slice baseline because those uncommitted/rejected Stage 02 changes may still be present locally.

---

## Architecture / tooling constraints

### Domain/Application boundary

- Classification semantics are inward-owned and must not be defined by parser/library exceptions or defaults.
- Application remains the normal gateway for outer-adapter orchestration.
- Concrete byte/PE/XML/JSON/ZIP/metadata inspection remains behind appropriate accepted boundaries.

### Managed metadata

- Use `System.Reflection.Metadata` / `PEReader` as the selected managed metadata baseline where applicable.
- Do not load/execute supplied managed assemblies to inspect them.

### XML / JSON

- Platform XML/JSON APIs remain the baseline, but malformed-candidate detection may require deterministic partial structural/token inspection that satisfies D175.
- Raw substring searches are not acceptable substitutes for structurally scoped candidate discriminators.

### NuGet / ZIP

- Local package inspection may use accepted NuGet/platform capabilities as appropriate.
- Remote feed lookup is outside this slice.
- Tool/library inability to identify a package does not override the canonical D172/D175 outcomes.

### Persistence

- Stage 02 state is later recovery state associated with existing Artifacts.
- It must not rewrite Stage 01 preservation truth/history.
- Physical schema evolution remains implementation discretion if stable identity, append-preserving history, replay, and atomic publication semantics remain intact.

### Security / integrity

- Treat all inspected bytes as untrusted.
- Preserve workspace-owned immutable Original Artifact bytes.
- Do not execute supplied binaries.
- Parser crashes/exceptions must not fabricate successful or malformed classification.
- Preserve cancellation semantics.

---

## Qualifications discovered

No canonical qualification was created or resolved by the DEV-SPEC-006 implementation run.

During the pre-implementation audit, the implementation agent identified wording in `91-design-qualification-register.md` that it considered stale and attempted to create/resolve `DOC-025` while also modifying `91`. That action was outside the accepted implementation-agent boundary and was subsequently rejected by the developer. The authoritative Project-source `91-design-qualification-register.md` was restored. `DOC-025` therefore does **not** form part of accepted LegacyRevive.NET governance and this DEV-SPEC does not rely on it.

The readiness review had specifically rechecked the two categories that invalidated DEV-SPEC-005:

1. malformed/corrupt family candidacy — canonically determined by RMAT-010 / D175 and reflected in AC-S02-010 through 017 and AC-S02-022;
2. boundary/falsification verification — governed by DEV-004 / D176 and operationalized by the matrix above.

Post-implementation review nevertheless established a different failure: the DEV-SPEC-006 verification design did not decompose AC-S02-006 far enough to demonstrate the exact metadata-resolved identity requirement for `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`. This is recorded below as the basis for rejection rather than being silently repaired in the implementation.

---

## Verification status

**Historical verification completed, but the resulting completion judgment was subsequently invalidated.**

The results below are retained as historical evidence of what passed under the then-defined DEV-SPEC-006 verification suite. They no longer establish that Stage 02 classification was accepted or correctly completed.

Readiness verification completed:

- canonical basis reviewed against the current Project source;
- RMAT-009/D172 successful-recognition semantics incorporated;
- RMAT-010/D175 malformed-candidate semantics incorporated;
- DEV-002/D173 semantic-readiness test passed;
- DEV-004/D176 boundary/falsification matrix completed before readiness;
- no unresolved qualification blocks implementation;
- rejected DEV-SPEC-005 code/tests are explicitly treated as unaccepted WIP rather than inherited completion evidence.

Actual verification established before entering `Verification Pending`:

- DEV-SPEC-006 acceptance: 30 passed, 0 failed, 0 skipped;
- D176 matrix: 70/70 rows executable and passing; 0 `Not applicable`; 0 skipped/pending/blocked;
- structural recognizer support: 36 passed, 0 failed, 0 skipped;
- persistence/reopen/history support: 3 passed, 0 failed, 0 skipped;
- Domain project: 18 passed, 0 failed, 0 skipped;
- Application project: 19 passed, 0 failed, 0 skipped;
- DEV-SPEC-001/002/003 acceptance regression: 21 passed (7 / 6 / 8 respectively), 0 failed, 0 skipped;
- complete solution Debug: 174 passed, 0 failed, 0 skipped;
- complete solution Release: 174 passed, 0 failed, 0 skipped;
- acceptance-bearing skips: 0.

Final Definition-of-Done audit:

- at the time of the completion judgment, semantic readiness was recorded as `Pass`; the implementation agent's attempted `DOC-025`/`91` modification was later rejected and is not accepted governance;
- AC-S02-001 through AC-S02-030 and S01 through S30 are represented by passing DEV-SPEC-006 executable acceptance;
- all 70 D176 rows are mapped and passing, including adversarial near-matches for every positive material recognition predicate; 0 rows are `Not applicable`, skipped, pending, or blocked;
- the implementation contains no raw-substring/prefix/property-name/stream-name/package-name shortcut that is treated as a D175 family discriminator;
- hierarchy, broad-format, valid-but-unsupported, unsupported/unrecognized, ambiguity, malformed candidate, operational failure, cancellation, and order-independence boundaries pass;
- Stage 02 persists classification-bearing state and diagnostics only; Stage 03 semantic content is not persisted;
- Stage 01 identity, provenance, hash, preservation state, intake snapshot, diagnostic truth, and prior checkpoint history remain unchanged;
- durable Stage 02 reopen/replay succeeds without the supplied external source tree;
- failed publication and cancellation publish no partial successful current Stage 02 checkpoint;
- DEV-SPEC-001/002/003 regression and complete Debug/Release solution verification pass with zero skips;
- the then-current audit recorded no unresolved blocking qualification.

Historical result recorded at the time: DEV-SPEC-006 was judged to satisfy the canonical and slice-specific Definition of Done. That judgment is **not current**. The rejection disposition below records why it was later found materially unsound.

---

## Definition-of-Done conditions for this slice

DEV-SPEC-006 may transition to `Implemented` only when all applicable canonical Development Process requirements and the following slice-specific conditions are satisfied:

1. semantic readiness remains `Pass`;
2. no implementation-originated material product semantic has bypassed governance;
3. AC-S02-001 through AC-S02-030 are satisfied;
4. S01 through S30 are executable and passing where their behavior is acceptance-bearing;
5. every successful MVP recognition predicate in scope has direct verification;
6. every applicable D175 malformed/corrupt candidate discriminator has direct verification;
7. every positive material recognition predicate has at least one executable adversarial near-match as required by D176;
8. every applicable D176 matrix row is exercised and passing, or is explicitly and defensibly `Not applicable`; no acceptance-bearing required row is silently skipped;
9. readable ZIP + root `.nuspec` content that does not satisfy successful NuSpec recognition is proven to remain broader valid ZIP/unsupported rather than malformed NuGet;
10. raw/prefix/incidental XML/JSON/metadata/package token occurrences are proven insufficient to fabricate malformed family candidacy;
11. hierarchy, broader classification, ambiguity, valid-but-unsupported, unsupported/unrecognized, malformed/corrupt, and operational-failure boundaries are all verified;
12. Stage 02/Stage 03 non-leakage is verified;
13. Stage 01 Artifact identity/provenance/preservation semantics remain unchanged;
14. Stage 01 checkpoint/snapshot/history remains replayable and unrewritten;
15. Stage 02 classification survives durable reopen without the original supplied source tree;
16. failed commit/cancellation does not publish partial successful current Stage 02 state;
17. recognizer order and Artifact processing order do not change substantive results;
18. all DEV-SPEC-001/002/003 acceptance behavior remains passing;
19. complete solution verification passes with no acceptance-bearing skips;
20. actual verification locations/results and matrix-to-test coverage are recorded in this document;
21. aggregate green test counts are not used as a substitute for the D176 boundary coverage audit;
22. any material qualification discovered during implementation has completed the required governance lifecycle before completion is claimed;
23. lifecycle history is updated consistently with `16-development-process.md`.

Source-control commit is not added as a slice-specific completion condition. Repository/source-control practice remains separate from DEV-SPEC lifecycle unless another applicable repository rule explicitly requires it. In the current workflow, the user intends to commit/push the resulting Stage 02 implementation only after DEV-SPEC-006 is legitimately completed.

---

## Rejection disposition

DEV-SPEC-006 is **Rejected** under DEV-003 / D174.

The specification previously reached `Implemented`, and its then-defined executable/supporting verification passed. A later independent post-implementation audit established that the completion judgment was materially unsound and that the Stage 02 implementation must not remain accepted.

### Basis for rejection

The decisive implementation/verification defect is AC-S02-006 — Reference assembly recognition.

AC-S02-006 requires an assembly-level custom attribute whose **metadata-resolved type is exactly** `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`. The DEV-SPEC itself states that simple attribute-name matching is insufficient.

The implemented recognizer, however, established reference-assembly status by comparing the attribute type's namespace and simple name. It did not establish the full metadata-resolved type identity/resolution scope required by the acceptance contract. A different type using the same namespace and simple name could therefore satisfy the implementation even though the canonical predicate was not established.

The D176 verification matrix did not falsify that weaker implementation. Its AC-S02-006 adversarial case used the same simple attribute name with a different type/namespace. That verifies rejection of a simple-name-only implementation, but it does not verify the materially stronger boundary between:

- the genuine metadata-resolved `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`; and
- an unrelated/local/differently-resolved type with the same namespace and simple name.

Consequently, the recorded claims that AC-S02-006 was satisfied, that every material positive predicate had adequate adversarial falsification, and that the DEV-SPEC-006 Definition of Done passed are no longer accepted.

This is not treated as a newly discovered Stage 02 product semantic. The exact metadata-resolved attribute identity requirement was already present in the canonical recognition contract and in DEV-SPEC-006. The failure is therefore retained as evidence of a specification/verification-process weakness rather than repaired as another implementation patch.

### Repository disposition

The developer has rejected all Stage 02 production/test implementation changes produced through DEV-SPEC-004, DEV-SPEC-005, and DEV-SPEC-006 rather than continuing a sequence of fixes to rejected implementations.

The C# production/test changes from those attempts have been reverted/removed. The accepted implementation baseline is restored to the post-DEV-SPEC-003 state:

- DEV-SPEC-001 — accepted implementation baseline;
- DEV-SPEC-002 — accepted implementation baseline;
- DEV-SPEC-003 — accepted implementation baseline;
- DEV-SPEC-004 — rejected historical Stage 02 attempt;
- DEV-SPEC-005 — rejected historical Stage 02 attempt;
- DEV-SPEC-006 — rejected historical Stage 02 attempt.

The historical DEV-SPEC-006 verification results, including the recorded 174/174 Debug and Release runs, remain visible solely as historical evidence about the then-defined implementation and test suite. They provide no acceptance credit to a future Stage 02 implementation.

No DEV-SPEC-006 production/test code is part of the accepted repository implementation baseline.

### Governance disposition

No canonical document is modified by this rejection.

The implementation agent's attempted modification of `91-design-qualification-register.md` and attempted `DOC-025` qualification are explicitly **not accepted**. The authoritative Project-source version of `91-design-qualification-register.md` remains the canonical register baseline.

The rejection itself does not create a new qualification or durable design decision. A separate governed post-mortem may determine whether the three consecutive Stage 02 failures expose material Development Process/specification questions that require qualification and decision work before another Stage 02 DEV-SPEC is created.

### Replacement lineage

`Superseded by` remains `None`.

No replacement Stage 02 DEV-SPEC is authorized by this rejection. Under D174, any future implementation replacing the intended Stage 02 scope must use a **new DEV-SPEC**. Before that occurs, the project intends to review the specification/development process exposed by DEV-SPEC-004, DEV-SPEC-005, and DEV-SPEC-006 rather than immediately attempting another implementation.

---

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| 2026-10-01 | Created → Draft | Created as the new replacement Stage 02 format/family-classification specification after rejected DEV-SPEC-005. Incorporates settled RMAT-009/D172 successful-recognition semantics, RMAT-010/D175 malformed-candidate discriminators, DEV-002/D173 semantic readiness, DEV-003/D174 replacement/rejection history, and DEV-004/D176 boundary/falsification verification. |
| 2026-10-01 | Draft → Ready for Implementation | Canonical-source review completed; full-slice AC-S02-001 through AC-S02-030 re-established; D175 malformed-candidate thresholds incorporated; D176 boundary/falsification matrix completed with adversarial near-matches for every positive material recognition predicate; material semantic choice test passed; no unresolved qualification or canonical blocker remains. |
| 2026-10-01 | Ready for Implementation → In Progress | Required pre-implementation review confirmed semantic readiness remained Pass and the D176 matrix was present. The implementation agent also identified a possible stale-summary issue in `91` and attempted a `DOC-025` governance edit; that attempted canonical change was later rejected and is not part of accepted project governance. Implementation then commenced against DEV-SPEC-006. |
| 2026-10-01 | In Progress → Verification Pending | Complete slice implementation, DEV-SPEC-006 acceptance suite, focused supporting verification, explicit 70-row D176 mapping, prior-slice regression, and Debug/Release solution verification are green; final Definition-of-Done audit commenced. |
| 2026-10-01 | Verification Pending → Implemented | Final audit confirmed semantic readiness, AC-S02-001 through AC-S02-030, S01 through S30, all 70 D176 rows and adversarial near-matches, Stage 01 history/replay, Stage 02 non-leakage, deterministic outcomes, atomic failure/cancellation behavior, prior-slice regression, and complete Debug/Release verification with zero skips. |
| 2026-10-01 | Implemented → Rejected | DEV-003/D174 applied after independent post-implementation review found the prior completion judgment materially unsound. AC-S02-006 required exact metadata-resolved `ReferenceAssemblyAttribute` identity, but the implementation verified only namespace + simple name and the D176 near-match did not falsify an unrelated type with the same namespace/name. The developer rejected the DEV-SPEC-006 implementation rather than patching another rejected Stage 02 attempt; all DEV-SPEC-004/005/006 C# production/test changes were reverted/removed and the accepted repository implementation baseline returned to post-DEV-SPEC-003. Historical verification and the earlier `Implemented` event are preserved. Codex's attempted `91`/`DOC-025` governance modification is not accepted. |

---

## Historical implementation handoff rule

The handoff rules below are retained only as historical context for the rejected DEV-SPEC-006 implementation attempt. They are no longer an active authorization to implement Stage 02 under this specification.

The coding agent was required to read the current canonical sources and this DEV-SPEC before changing production code.

Implementation was permitted to begin from the then-existing uncommitted working tree, while treating DEV-SPEC-005-derived code/tests as unaccepted WIP. No rejected implementation choice was authoritative merely because it already existed.

Any future Stage 02 implementation requires a new DEV-SPEC. Implementation agents must not treat this rejected specification or its historical tests as acceptance authority.
