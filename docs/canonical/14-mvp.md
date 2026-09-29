# LegacyRevive.NET — Minimum Viable Product

## Status

**Canonical**

This document defines the first shippable LegacyRevive.NET product slice.

It determines which capabilities from the canonical recovery model, architecture, and tooling baseline are required for the MVP, which are deliberately deferred, and what must be true before the MVP is considered releasable.

Later capability expansion belongs to:

`15-roadmap.md`

This document is constrained by `90-decisions.md` and the resolved `MVP-*` qualifications in `91-design-qualification-register.md`.

---

# 1. MVP objective

The MVP must prove the central LegacyRevive.NET proposition:

> **Given a local set of surviving managed .NET deployment artifacts, LegacyRevive.NET can create an evidence-backed candidate development workspace containing reconstructed C# source, candidate projects/solution structure, explicit provenance/uncertainty, and actionable diagnostics that move a developer materially closer to a workable codebase.**

The MVP is not a demonstration decompiler.

It must exercise the full minimum recovery loop:

```text
artifact intake
    ↓
direct analysis
    ↓
knowledge/provenance
    ↓
candidate project/source reconstruction
    ↓
developer-visible ambiguity/context
    ↓
candidate workspace
    ↓
restore/build attempt where prerequisites exist
    ↓
checkpoint + report
```

**Governance:** MVP-001 / D132.

---

# 2. Primary user journey

The primary MVP user is a developer who has a deployment directory or collected deployment artifacts but does not have a usable original solution/source tree.

The minimum end-to-end journey is:

1. point LegacyRevive.NET at a local artifact directory;
2. create a recovery workspace;
3. inventory and hash all discovered supplied files, with any discovery incompleteness kept explicit;
4. identify and analyze supported managed artifacts and companion files;
5. build the evidence/provenance model;
6. classify/correlate assemblies and dependencies;
7. create Candidate Projects;
8. reconstruct/decompile source;
9. generate a candidate solution/project tree;
10. present uncertainty/conflicts and request developer intervention where needed;
11. attempt restore/build when the required local toolchain/dependencies are available;
12. persist the Recovery Checkpoint;
13. export the candidate codebase and recovery report.

A user must be able to stop after analysis/reconstruction and resume the same workspace later.

---

# 3. Supported execution environment

The MVP is **Windows x64 first**.

Reasons:

- legacy .NET Framework applications are a core target;
- native Windows PDB support may be valuable;
- full-framework reference assemblies/developer packs and Visual Studio/MSBuild toolsets are most naturally available on Windows;
- the architecture/tooling choices remain sufficiently isolated to allow later cross-platform expansion.

Minimum supported host baseline:

```text
Windows 10/11 x64
.NET 10 runtime/SDK
```

Buildability of a recovered application may require additional locally installed SDKs, targeting packs, reference assemblies, Visual Studio Build Tools, or proprietary dependencies.

LegacyRevive.NET must detect/report missing prerequisites rather than treating them as evidence that the recovered reconstruction is wrong.

Cross-platform execution is deferred to the roadmap; the Domain/Core must not intentionally become Windows-only.

**Governance:** MVP-002 / D133.

---

# 4. Recovery Input boundary

The MVP accepts a **local directory tree** as its primary Recovery Input source.

Every discovered file is inventoried even if no dedicated analyzer exists. "Discovered" is literal: failure to enumerate a root or subtree must not be converted into invented file instances or a claim that undiscovered contents are absent.

The MVP must preserve the Stage 01 discovery-completeness semantics defined by `07-recovery-process.md`:

- a completely enumerable empty directory is a known empty supplied set and may produce a valid empty Recovery Intake Snapshot;
- a supplied root that cannot be enumerated sufficiently to establish a trustworthy supplied set is Blocked and must not be represented as a successful empty intake;
- when a narrower subtree cannot be enumerated but other files are safely discovered, the trustworthy discovered files may be retained, the failed discovery scope remains explicit/Blocked, and the run may be Partial;
- no Artifact identity is created for possible files that were never discovered;
- a file discovered before later read/preservation failure follows the ordinary Artifact-scoped preservation-failure model.

The MVP does not require a full filesystem graph, directory-as-Artifact model, filesystem journal capture, or transactional point-in-time directory snapshot to satisfy these semantics.

## 4.1 Required MVP analyzers

The MVP must directly analyze:

- managed `.dll`;
- managed `.exe`;
- `.config`, `web.config`, executable configuration and other relevant XML configuration;
- Portable PDB where present;
- XML documentation files associated with managed assemblies;
- `.deps.json`;
- `.runtimeconfig.json`;
- `packages.config`;
- `.nupkg`;
- `.nuspec`;
- satellite/resource assemblies where recognizable.

## 4.2 Required preservation/correlation

The MVP must:

- retain original relative path and capture context as provenance;
- compute SHA-256;
- snapshot the exact bytes of each successfully preserved supplied artifact into workspace-owned immutable artifact storage beneath `.legacyrevive`;
- verify the preserved bytes/content hash before considering preservation successful;
- use the workspace-owned preserved bytes for subsequent recovery/replay rather than depending on continued availability of the original external path;
- identify artifact kind;
- correlate obvious companion artifacts;
- preserve unsupported but readable supplied files in the immutable artifact store and artifact inventory;
- expose unreadable, unsupported, or preservation-failure conditions through scoped diagnostics;
- treat a failed/incomplete snapshot as not successfully preserved rather than silently proceeding as though the bytes were durable.

Artifact identity, SHA-256 content identity, supplied path/provenance, and physical storage location remain distinct. Equal hashes do not by themselves merge Artifact identities. Large artifact bytes remain filesystem/artifact-store backed rather than SQLite BLOBs. The exact internal artifact-store layout is not an MVP semantic requirement.

## 4.3 Optional-in-MVP artifact support

Native Windows PDB support may ship when the optional adapter is available, but the MVP must not depend on it for basic completion.

## 4.4 Out of scope for MVP deep recovery

The MVP does not promise semantic reconstruction of:

- arbitrary native binaries;
- installers/MSI;
- deployment orchestration platforms;
- database servers;
- live IIS configuration;
- remote package feeds;
- source servers;
- runtime memory/process state.

Such material may still be inventoried and preserved.

**Governance:** MVP-003 / D134; PROC-010 / D170.

---

# 5. Managed assembly analysis scope

For managed assemblies, the MVP must recover direct metadata observations sufficient for practical project/source reconstruction, including where present:

- assembly/module identity;
- target-framework/runtime clues;
- assembly references;
- namespaces;
- types/nested types;
- base types/interfaces;
- generic parameters/constraints;
- constructors;
- methods;
- properties;
- fields;
- events;
- visibility;
- signatures;
- custom attributes;
- embedded resources/manifest clues;
- entry point for executables;
- method-body/IL availability sufficient for decompilation.

Analysis must use the direct-observation boundary from the Evidence Model and Architecture.

A managed assembly analyzer must not silently decide that one assembly equals one original project.

**Governance:** MVP-004 / D135.

---

# 6. Configuration and companion-artifact scope

The MVP must preserve raw configuration/source-like companion content while extracting the high-value structure needed for reconstruction.

At minimum:

- XML configuration remains available verbatim;
- `appSettings` and `connectionStrings` are recognized where applicable;
- runtime/assembly-binding information is indexed;
- `system.serviceModel` is structurally discoverable where present;
- type/assembly-qualified references in configuration can be correlated to known assemblies/types where practical;
- `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, and package contents contribute dependency/runtime observations;
- XML documentation is correlated to member IDs;
- PDB documents/sequence points contribute source-layout context where available.

The MVP does not need a dedicated semantic parser for every legacy framework configuration section.

Uninterpreted preserved content remains visible as such.

---

# 7. Assembly ownership and Candidate Projects

The MVP must classify relevant assemblies using the canonical ownership vocabulary:

- ApplicationOwned;
- InternalDependency;
- ThirdParty;
- Framework;
- Unknown.

The classification may remain inferential/Unknown where evidence is insufficient.

Candidate Project generation must:

- anchor candidates primarily to implementation assemblies;
- preserve evidence/inference basis;
- allow multiple plausible classifications/project structures where material;
- avoid converting every local DLL into an application project automatically;
- expose unresolved ownership/project-boundary questions to the developer.

The MVP need not solve every project merge/split automatically.

**Governance:** MVP-005 / D136.

---

# 8. Dependency reconstruction

The MVP reconstructs a dependency graph before choosing concrete project-file representation.

It must distinguish:

```text
dependency exists
        ≠
historical declaration mechanism is known
```

The MVP may represent a dependency as:

- project reference;
- package reference;
- direct/local assembly reference;
- framework/runtime dependency;
- unresolved dependency.

The chosen representation must preserve its provenance and uncertainty.

Remote NuGet lookup is disabled by default and is not required for MVP completion.

Where the exact historical package/version cannot be established, the tool must not invent it merely to make restore succeed.

**Governance:** MVP-006 / D137.

---

# 9. Source reconstruction

The MVP must generate usable C# source candidates from supported managed implementation assemblies using the selected decompiler.

Required behavior:

- reconstruct declarations and implementations where decompilation permits;
- preserve source provenance as Decompiler-Generated/Reconstructed Source;
- use PDB/source-document information to improve grouping/naming where available;
- otherwise use deterministic generated organization;
- preserve XML documentation when it can be mapped reliably;
- identify compiler-generated/lowered constructs where the tooling exposes enough information to do so;
- preserve unresolved/decompilation failures as diagnostics rather than fabricating code.

The MVP does not claim exact original formatting, comments, local names, file boundaries, or source text.

**Governance:** MVP-007 / D138.

---

# 10. Candidate solution/project generation

The MVP must export a conventional development tree containing:

```text
<output>/
  LegacyRevive.Recovery.json
  LegacyRevive.Report.json
  LegacyRevive.Report.md
  <candidate-solution>.sln or equivalent supported solution representation
  src/
    <CandidateProject>/
      <CandidateProject>.csproj
      ...
```

Generated project files should include, where support exists:

- target framework/runtime;
- output kind;
- assembly name;
- reconstructed source items;
- resources/configuration;
- dependency representations;
- project-to-project relationships.

Project/source generation must be deterministic for the same effective Recovery Checkpoint and tool baseline, aside from explicitly non-semantic timestamps/paths where unavoidable.

The generated solution/project files remain Reconstructed Artifacts.

**Governance:** MVP-008 / D139.

---

# 11. Developer intervention and recovered context

The MVP includes developer participation as a normal recovery capability.

It must support at least:

- Developer Assertion;
- Developer Decision;
- Developer Correction;
- supersession/withdrawal where applicable;
- persisted intervention history;
- downstream reevaluation after a material correction.

When asking for a decision, LegacyRevive.NET should present the strongest available recovered context rather than a context-free prompt.

The MVP should surface basic deterministic Derived Observations useful for decisions, including where applicable:

- assembly/name-prefix patterns;
- namespace patterns;
- dependency-direction patterns;
- recurring type/member naming patterns;
- framework/dependency concentration by scope.

It must report population/scope/exceptions and must not infer named architectural methodology as historical intent.

Machine-learned convention discovery is not part of the MVP.

**Governance:** MVP-009 / D140.

---

# 12. Recovery workspace, persistence, and resume

Every MVP recovery uses a persistent local Recovery Workspace.

The workspace must preserve:

- artifact inventory/hashes;
- Observations/Facts/Derived Observations/Inferences;
- assumptions/conflicts;
- support/provenance relationships;
- interventions;
- Candidate Projects;
- reconstruction metadata;
- diagnostics;
- Recovery Runs/Checkpoints;
- build attempts;
- exported-result metadata.

The user must be able to reopen a workspace without reclassifying already-known historical material merely because the process restarted.

Adding new artifacts or correcting an intervention triggers the canonical earliest-affected-stage reevaluation model.

**Governance:** MVP-010 / D141.

---

# 13. Build and refinement scope

The MVP must be able to attempt restore/build of the generated candidate solution when applicable local prerequisites exist.

The build capability must:

- use the out-of-process build boundary;
- record selected toolset/environment;
- capture structured diagnostics;
- associate diagnostics with projects/files where possible;
- preserve each meaningful build attempt;
- distinguish missing environment/dependency prerequisites from reconstruction defects.

The MVP may perform deterministic or narrowly-scoped automated repairs whose correctness does not depend on pretending an uncertain historical fact is known.

Examples may include:

- correcting generated syntax/project-file defects;
- adding generated compile/resource items already established by recovery state;
- resolving deterministic generated-path issues.

The MVP must not silently perform speculative package-version substitutions, project merging, framework upgrades, or behavioral rewrites merely to reach a green build.

A successful build is a valuable recovery milestone but is **not required for every MVP recovery to be considered a completed recovery run**.

**Governance:** MVP-011 / D142.

---

# 14. Recovery outcomes visible to the user

At the end of a run the MVP must assign/report the applicable canonical recovery state, such as:

- Partial;
- Blocked;
- Workable;
- Validated where a separate applicable validation process has been performed.

The product must make clear:

- what was recovered directly;
- what was inferred;
- what was reconstructed;
- what the developer supplied/decided;
- what remains Unknown;
- what conflicts/blockers remain;
- whether restore/build was attempted and its result.

The CLI exit/result model should distinguish tool execution failure from a legitimate Partial/Blocked recovery.

**Governance:** MVP-012 / D143.

---

# 15. MVP CLI surface

The MVP is command-line first.

The canonical capability surface is:

```text
legacyrevive recover <artifact-directory> --workspace <workspace>
legacyrevive status <workspace>
legacyrevive explain <workspace> <id>
legacyrevive decide <workspace> ...
legacyrevive build <workspace>
legacyrevive export <workspace> --output <directory>
```

Exact option spelling may evolve without a durable decision as long as these capabilities remain available.

## `recover`

Creates/resumes a workspace and runs the supported recovery stages through candidate reconstruction, optionally attempting build according to configuration.

## `status`

Shows current recovery state, unresolved conflicts/decisions, artifact coverage, project/source status, and latest build outcome.

## `explain`

Shows the provenance/support chain for a Fact, Derived Observation, Inference, Candidate Project, dependency representation, source/reconstruction item, or other explainable recovery conclusion.

## `decide`

Commits supported Developer Intervention records.

## `build`

Runs another controlled restore/build attempt against the current reconstruction.

## `export`

Writes the current candidate development tree and machine/human-readable reports.

Interactive UI, web API, IDE extension, and desktop GUI are not MVP requirements.

**Governance:** MVP-013 / D144.

---

# 16. Reporting and explainability

Every exported MVP recovery must include both machine-readable and human-readable reporting.

Minimum report content:

- recovery/workspace/checkpoint identity;
- artifact inventory summary;
- supported/unsupported/unreadable artifact counts;
- recovered application/executable boundaries where identified;
- Candidate Projects and their basis;
- dependency graph and unresolved dependencies;
- source-reconstruction summary;
- intervention summary;
- conflicts/assumptions/Unknowns;
- build attempts/results;
- tool/dependency baseline used;
- major diagnostics;
- output paths.

The `explain` capability must make individual decisions auditable back to source artifacts/evidence/inference rules/interventions.

A report is not allowed to flatten inference/reconstruction into "recovered fact."

---

# 17. Network policy for MVP

The default MVP path is fully local/offline.

Remote NuGet feed access and other external enrichment are **post-MVP capabilities** and are not part of the MVP product path.

The standard MVP acceptance tests run without external enrichment. Post-MVP enrichment validation is governed separately when that roadmap capability is promoted.

`13-tooling-and-dependencies.md` may preselect `NuGet.Protocol` as the adapter for future explicit remote-feed enrichment, but that tooling selection does not expand MVP scope. `15-roadmap.md` owns promotion and sequencing of the capability.

This makes the MVP evidence ceiling reproducible and prevents hidden network knowledge from becoming a prerequisite for useful recovery.

---

# 18. Validation scope in the MVP

A full user-facing historical-reference validation product is not required for the initial user workflow.

However, **controlled blind validation is mandatory for developing and releasing the MVP**.

The held-out validation process must preserve:

```text
known original source/reference
        ↓ build/publish
allowed deployment artifacts
        ↓
blind LegacyRevive recovery
        ↓
frozen Validation Target
        ↓
reference revealed only to validator
        ↓
canonical metrics/report
```

The MVP implementation therefore must contain enough validation/metric infrastructure to execute the release-gate corpus tests, even if the normal user CLI does not expose every validation operation.

**Governance:** MVP-014 / D145.

---

# 19. MVP release corpus

The MVP release gate must include controlled subjects representing at least:

1. a .NET Framework application containing multiple managed assemblies and configuration;
2. a modern .NET application with `.deps.json` / `.runtimeconfig.json`;
3. a scenario with useful companion evidence such as PDB and/or XML documentation;
4. an artifact-starved scenario with managed binaries but no PDB/source documentation;
5. a deliberately incomplete or conflicting deployment that must produce Partial/Blocked/conflict output rather than confident fabrication.

At least one supported .NET Framework subject and one supported modern .NET subject must reach a generated candidate solution/source tree and undergo a build attempt.

The corpus should include held-out subjects not used to tune the applicable inference rules.

---

# 20. MVP acceptance gates

The MVP is releasable only when all of the following hold.

## 20.1 End-to-end functional gate

For the controlled release corpus the tool can:

- create a workspace;
- inventory/hash artifacts;
- analyze supported managed artifacts;
- persist provenance-bearing recovery knowledge;
- generate Candidate Projects;
- generate C# source;
- generate/export a candidate solution/project tree;
- preserve unresolved/unsupported information;
- attempt restore/build where prerequisites exist;
- report a canonical recovery state;
- resume the workspace.

## 20.2 Epistemic-safety gate

No known release-gate test permits:

- reconstructed output to corroborate its own historical premise;
- Validation Reference leakage into blind recovery;
- Developer Decisions to be silently presented as historical facts;
- `Unknown` to be silently replaced by invented historical precision;
- unsupported project/package/source claims to be presented as direct recovery.

A detected violation of these invariants blocks release even if build metrics improve.

## 20.3 Determinism/replay gate

Repeated execution from the same Recovery Input, committed interventions, configuration, and dependency baseline produces substantively equivalent:

- artifact identities/hashes;
- direct observations;
- current conclusions;
- Candidate Project/dependency decisions;
- generated source/project structure;
- reports/metrics,

apart from explicitly non-semantic runtime metadata.

## 20.4 Persistence/history gate

Workspace resume, intervention correction, artifact addition, supersession, and checkpoint history preserve prior state/provenance and trigger appropriate reevaluation.

## 20.5 Recovery-quality gate

The controlled validation report must expose the canonical multidimensional metrics.

The MVP does **not** adopt a single arbitrary minimum "LegacyRevive score."

Dimension-specific regression thresholds may be introduced once a stable held-out corpus establishes defensible baselines.

## 20.6 Developer-usability gate

A developer can determine, without inspecting the SQLite database or internal logs:

- what LegacyRevive produced;
- why material project/dependency decisions were made;
- what remains unresolved;
- what action is needed next;
- whether the current candidate builds.

**Governance:** MVP-015 / D146.

---

# 21. Explicit MVP exclusions

The following are deliberately deferred and must not block the first release:

- exact historical repository/file reproduction;
- native binary decompilation/reconstruction;
- arbitrary installer reverse engineering;
- runtime memory/process analysis;
- automatic database/schema reconstruction from a live database;
- runtime third-party plugin loading;
- web/desktop/IDE UI;
- hosted multi-user service;
- cloud persistence;
- distributed workers;
- remote NuGet/external enrichment capability;
- automatic source-server retrieval;
- generalized ML/LLM inference as an authoritative recovery engine;
- fully automatic resolution of every project/package ambiguity;
- framework modernization/upgrading of the recovered application;
- guaranteed successful build for incomplete deployments;
- global behavioral-equivalence proof;
- one-number recovery score.

These may be considered by `15-roadmap.md`.

**Governance:** MVP-016 / D147.

---

# 22. What the MVP is expected to prove

The MVP succeeds as a product experiment if it demonstrates that LegacyRevive.NET can materially improve the starting condition:

```text
Before
  deployment directory
  DLLs / EXEs / config
  little or no usable source/project structure

After
  inventoried immutable artifacts
  evidence/provenance model
  dependency graph
  Candidate Projects
  reconstructed C# source
  generated solution/project files
  explicit uncertainties/conflicts
  developer decisions/history
  build diagnostics
  resumable recovery workspace
  exportable candidate development baseline
```

The MVP does not have to prove that every deployed .NET application can be fully recovered.

It must prove that the recovery model can produce a trustworthy, inspectable, progressively improvable development baseline on the supported scenario set.

---

# 23. MVP invariants

1. MVP scope is managed-.NET recovery first;
2. execution is Windows x64 first;
3. local/offline recovery is the default;
4. all discovered supplied files are inventoried even when unsupported, while discovery failures remain explicit;
5. managed metadata extraction is direct and non-executing;
6. one assembly is not automatically one original project;
7. dependency existence and historical representation remain separate;
8. generated C# remains reconstructed source;
9. developer intervention is explicit and persisted;
10. basic recovered context accompanies material intervention prompts;
11. workspace resume/checkpoint history is mandatory;
12. build is attempted when practical but is not the sole definition of recovery completion;
13. build repair cannot invent uncertain historical facts merely to compile;
14. CLI is the MVP user surface;
15. explainability/provenance is user-visible;
16. blind controlled validation is a release requirement;
17. no single global recovery score gates release;
18. epistemic-boundary violations block release;
19. unsupported/deferred capabilities remain explicit rather than masquerading as implemented;
20. roadmap expansion must not silently alter these MVP historical decisions.

---

# 24. Governance mapping

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| MVP-001 | D132 | MVP proves the complete minimum managed-artifact recovery loop |
| MVP-002 | D133 | Windows x64 first; architecture remains future cross-platform capable |
| MVP-003 | D134 | Local directory input with defined MVP artifact analyzers/preservation |
| MVP-004 | D135 | Managed assembly direct-analysis scope |
| MVP-005 | D136 | Ownership classification and Candidate Project scope |
| MVP-006 | D137 | Dependency graph first; representation uncertainty preserved |
| MVP-007 | D138 | Decompiler-backed source reconstruction with explicit provenance |
| MVP-008 | D139 | Deterministic candidate solution/project export |
| MVP-009 | D140 | Explicit developer intervention plus basic recovered context |
| MVP-010 | D141 | Persistent resumable Recovery Workspace |
| MVP-011 | D142 | Controlled build attempt/repair without speculative fabrication |
| MVP-012 | D143 | Partial/Blocked/Workable outcomes remain legitimate |
| MVP-013 | D144 | CLI-first command capability surface |
| MVP-014 | D145 | Blind controlled validation is mandatory for MVP release |
| MVP-015 | D146 | Multidimensional functional/epistemic/replay/usability release gates |
| MVP-016 | D147 | Explicit deferred/non-MVP capability boundary |

No open MVP qualification remains after this governed revision.

---

# 25. Boundary with `15-roadmap.md`

This document defines what must ship in the first meaningful LegacyRevive.NET product slice.

`15-roadmap.md` owns prioritization of deferred capabilities after the MVP, including broader artifact support, richer automation, additional hosts/UI, cross-platform support, remote enrichment, deeper behavioral validation, and other later-stage work.

Roadmap work may expand the MVP baseline but must not retrospectively rewrite what the MVP was defined to prove.

Material implementation of MVP capabilities is governed by `16-development-process.md`. Development Slice Specifications may divide MVP implementation into smaller verified increments, but they cannot remove, weaken, or expand the MVP obligations defined here.

---

# 26. Governance relationship

Durable MVP decisions are owned by:

`90-decisions.md`

Qualification history/status are owned by:

`91-design-qualification-register.md`

The implementation-development process and Development Slice Specification discipline are owned by:

`16-development-process.md`

A material change to the MVP scope or release gates must create a qualification and, where appropriate, a superseding decision rather than silently editing the historical commitment. A Development Slice Specification is an implementation artifact and cannot redefine MVP scope.
