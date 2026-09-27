# LegacyRevive.NET — Project Reconstruction

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET model for reconstructing candidate .NET solution/project structure from governed recovery state.

It owns:

- Candidate Project semantics;
- Project Boundary reconstruction;
- candidate solution grouping;
- project identity and naming reconstruction;
- target-framework/runtime project properties;
- project dependency representation;
- project/package/assembly-reference reconstruction choices;
- configuration/resource ownership at project level;
- project-file generation semantics;
- project-reconstruction reevaluation.

It participates primarily in Stage 07 — Candidate Reconstruction from `07-recovery-process.md`.

It does not define detailed source reconstruction, validation metrics, architecture, tooling libraries, or MVP scope.

---

# 1. Purpose

LegacyRevive.NET may recover enough information to construct a workable `.NET` development representation even when the original `.sln`, project files, or repository structure do not survive.

The purpose of Project Reconstruction is:

> **Create an explainable candidate solution/project structure that faithfully expresses the strongest supported recovery state while clearly distinguishing reconstructed engineering choices from historically recovered facts.**

A reconstructed project should help the developer:

- organize recovered assemblies/source/resources/configuration;
- represent dependencies;
- select appropriate framework/runtime characteristics;
- produce buildable project files where possible;
- understand why the proposed structure exists;
- revise the structure safely when new evidence appears.

The goal is a workable recovered structure, not fictional certainty about the exact original repository layout.

---

# 2. Candidate Project

A **Candidate Project** is a reconstructed .NET project boundary proposed for the current recovery state.

A Candidate Project may be based on:

- one or more surviving implementation assemblies;
- directly recovered project/build metadata where available;
- dependency relationships;
- target-framework/runtime information;
- PDB/source-path information;
- configuration;
- resource ownership;
- Convention Observations;
- Developer Decisions;
- other admissible recovery state.

A Candidate Project is a Candidate Reconstruction.

It is not automatically an Original Project.

---

# 3. Original Project versus Candidate Project

An **Original Project** means a project that actually existed in the original development codebase.

A **Candidate Project** is LegacyRevive.NET's current reconstructed representation.

The following must remain distinct:

```text
Original Project
≠
Candidate Project
```

A Candidate Project may correspond closely to an Original Project.

It may also:

- combine material originally spread across multiple projects;
- split material that originally belonged to one project;
- use a different project-file format;
- use a different name;
- represent dependencies differently;
- omit historical build details that cannot be recovered.

A useful reconstruction therefore does not prove exact historical identity.

---

# 4. Project Boundary

A **Project Boundary** determines which recovered items belong to one Candidate Project.

Project Boundary is often inferential.

Potential support includes:

- implementation assembly boundaries;
- assembly references;
- target-framework/runtime metadata;
- package/dependency metadata;
- namespaces;
- friend/internals relationships;
- configuration;
- resource ownership;
- PDB document/source paths;
- Convention Observations;
- deployment layout;
- Developer Input.

Every material boundary decision must retain provenance.

---

# 5. Assembly boundary as reconstruction anchor

A surviving managed implementation assembly is a strong reconstruction anchor because it directly establishes a compiled assembly boundary.

However:

> **Assembly boundary is not automatically proof of original project boundary.**

A compiled assembly may not preserve every historical project/build distinction.

Therefore the default reconstruction rule is:

1. treat each distinct managed implementation assembly as an independent **initial project candidate anchor** unless stronger recovery state suggests otherwise;
2. keep the boundary inferential unless direct project metadata establishes it;
3. allow later merge/split/reclassification through explicit reconstruction decisions.

This is a reconstruction starting point, not a historical claim.

Reference assemblies, satellite resource assemblies, native hosts, and other non-implementation artifacts do not automatically create ordinary implementation Candidate Projects.

---

# 6. Boundary support profile

Every Candidate Project boundary should expose a **Project Boundary Support Profile** describing the reasons for the boundary.

Relevant support may include:

- assembly identity/boundary;
- direct project metadata if surviving;
- reference relationships;
- target-framework compatibility;
- namespace concentration;
- source/PDB path clustering;
- resource ownership;
- configuration references;
- deployment grouping;
- Convention Observations;
- Developer Decisions;
- known contradictory signals.

The profile is descriptive.

Where the boundary itself is inferential, the boundary decision may carry the Evidence Model support-strength label:

- Strongly Supported;
- Plausible;
- Tentative.

---

# 7. Project boundary conflicts

A **Project Boundary Conflict** exists when materially supported recovery signals favor incompatible project groupings.

Examples may include:

- one assembly boundary suggesting one Candidate Project while surviving project metadata suggests a different grouping;
- PDB/source layout clustering that conflicts with deployment grouping;
- two plausible dependency structures requiring different project partitions.

A conflict must preserve all competing support.

LegacyRevive.NET may:

- leave the boundary Unresolved;
- produce multiple Candidate Project structures;
- use a Developer Decision to select an operational path.

A selected operational path does not retroactively prove historical correctness.

---

# 8. Merge and split reconstruction

Project reconstruction must support explicit **merge** and **split** decisions.

## Merge

A merge combines recovery material from multiple initial project candidate anchors into one Candidate Project.

A merge requires explicit rationale/provenance.

Possible reasons may include:

- direct surviving project metadata;
- Developer Decision;
- evidence that one recovered assembly is generated/auxiliary rather than a project output;
- practical reconstruction constraints.

## Split

A split divides one initial project candidate anchor into multiple Candidate Projects.

A split is more inferential and requires explicit support/rationale.

Possible support may include:

- surviving project/build metadata;
- source/PDB path evidence;
- distinct dependency/resource/configuration groupings;
- Developer Decision.

Neither merge nor split becomes evidence about the original structure merely because reconstruction adopts it.

---

# 9. Candidate Solution

A **Candidate Solution** is a reconstructed organizational container for Candidate Projects.

A solution file is not required to prove that an identical original `.sln` existed.

The Candidate Solution may be created when it provides a useful development representation for:

- multiple related Candidate Projects;
- project dependencies;
- build organization;
- developer navigation.

The Candidate Solution itself is a reconstructed artifact.

Its project membership and organization must remain provenance-linked.

---

# 10. Project identity versus project name

Candidate Project identity must be stable independently of its current display/file name.

A project name may be:

- directly supported by surviving project/build metadata;
- inferred from assembly identity;
- inferred from namespace/configuration/deployment conventions;
- chosen by Developer Decision.

Therefore:

> **Project identity is recovery-state identity; project name is a reconstructive property with its own provenance.**

Renaming a Candidate Project does not create a new historical project identity unless the reconstruction semantics require a new candidate.

---

# 11. Naming reconstruction

Naming should prefer stronger support before weaker convention.

A typical evidence order is:

1. surviving direct project/build metadata;
2. assembly identity/name;
3. configuration/resource relationships;
4. Convention Observations;
5. Developer Decision;
6. generated fallback name.

Generated fallback names must be clearly reconstruction-generated.

They must not later be reported as recovered original names.

---

# 12. Project kind

A Candidate Project may need a reconstructive **Project Kind**, such as a library, executable/host, web-hosting project, test-like project, or another .NET project role.

Project Kind should be derived from the strongest available recovery state.

Examples of support may include:

- managed entry-point metadata;
- deployment/runtime metadata;
- hosting/configuration artifacts;
- references;
- framework/runtime characteristics;
- Developer Input.

Project Kind is inferential unless direct surviving project metadata establishes it.

The exact project-type template or SDK/tooling choice belongs to `12-architecture.md` and `13-tooling-and-dependencies.md`.

---

# 13. Target framework and runtime reconstruction

A Candidate Project should record target-framework/runtime characteristics required to create a workable development representation.

Potential support includes:

- assembly metadata;
- `.runtimeconfig.json`;
- `.deps.json`;
- package asset groups;
- configuration;
- runtime/deployment metadata;
- direct surviving project/build metadata.

The project model must distinguish:

- directly observed framework/runtime facts;
- inferred target framework;
- Developer Decision used to select a practical reconstruction target.

If exact original targeting cannot be established, LegacyRevive.NET may choose a workable reconstruction target while preserving that it is a reconstruction decision.

---

# 14. Multi-targeting

Multiple surviving binaries that appear related must not automatically be treated as separate Original Projects.

They may represent:

- separate projects;
- multi-target outputs;
- different deployments/builds;
- framework-specific builds;
- unrelated assemblies with similar naming.

When multi-targeting is plausible, the reconstruction must preserve the ambiguity until support justifies:

- one multi-target Candidate Project;
- multiple Candidate Projects;
- Developer Decision.

The exact original target-framework list must not be invented.

---

# 15. Dependency reconstruction

Project reconstruction must represent dependencies needed by Candidate Projects.

A dependency may be reconstructed as:

- Candidate Project dependency;
- package dependency;
- direct assembly/file dependency;
- framework/runtime dependency;
- unresolved external dependency.

The recovery model must distinguish:

> **compiled/runtime dependency observed**

from:

> **original project-file declaration mechanism**

These are not the same proposition.

---

# 16. ProjectReference reconstruction

A surviving assembly reference directly establishes a compiled assembly dependency when the Evidence Model direct-support rule is satisfied.

It does not by itself prove that the original project used a `ProjectReference`.

A Candidate `ProjectReference` may be reconstructed when:

- both dependency endpoints are represented as Candidate Projects; and
- the relationship is an appropriate workable reconstruction choice.

The generated `ProjectReference` must retain provenance to the underlying assembly/dependency evidence and reconstructive decision.

It is a project-representation choice, not automatically a recovered historical fact.

---

# 17. PackageReference reconstruction

Package/dependency metadata may support a package dependency.

However, deployment/runtime package evidence does not by itself prove:

- the package was directly referenced by the original project;
- the exact original package-management style;
- the exact original `PackageReference` item.

A Candidate package reference should preserve:

- package identity/version support;
- whether directness is observed or inferred;
- source provenance;
- reconstruction rationale.

Where direct versus transitive origin cannot be established, the uncertainty remains explicit.

---

# 18. Direct assembly/file references

Some recovered dependencies may need to remain direct assembly/file references in the Candidate Project because available recovery state does not justify a package or Candidate Project representation.

This is legitimate.

LegacyRevive.NET should prefer an honest direct dependency representation over inventing unsupported package/project history.

A later discovery may supersede that representation.

---

# 19. Unresolved external dependencies

If a referenced dependency cannot be resolved to a Candidate Project, package, framework component, or available assembly, represent it as an **Unresolved Project Dependency**.

The record should preserve:

- observed identity/reference;
- requesting Candidate Project;
- available version/public-key/culture metadata where relevant;
- current resolution attempts;
- blocker/diagnostic state.

An unresolved dependency may block build without invalidating the rest of the Candidate Project.

---

# 20. Dependency cycles

If reconstructed Candidate Project dependencies create a cycle, LegacyRevive.NET must not silently break or rewrite the cycle merely to make generated projects build.

The cycle may indicate:

- incorrect Candidate Project boundaries;
- an incorrect dependency representation;
- generated/merged build behavior not yet understood;
- missing historical build mechanism;
- another unresolved recovery issue.

The cycle should trigger reevaluation or Developer Intervention.

---

# 21. Source ownership boundary

`08-project-reconstruction.md` determines which recovered source representation belongs to which Candidate Project.

It does not define how method/source code is reconstructed.

Source ownership may be supported by:

- assembly/type ownership;
- PDB documents;
- source paths;
- namespaces;
- resource/configuration relationships;
- Convention Observations;
- Developer Decisions.

Detailed source creation belongs to `09-source-reconstruction.md`.

---

# 22. Configuration ownership

Configuration should be assigned to Candidate Projects only when the relationship is supportable.

Possible support includes:

- filename/deployment association;
- executable/application relationship;
- explicit configured types/assemblies;
- hosting metadata;
- Developer Decision.

A single configuration artifact may influence more than one Candidate Project without proving that it originally belonged textually to each project's source tree.

Ownership for reconstruction and influence on runtime behavior are separate concepts.

---

# 23. Resource ownership

Embedded resources directly belong to their containing assembly at the compiled-artifact level.

Satellite resources may have a relationship to a main assembly.

Project reconstruction may use those relationships to place recovered resources into Candidate Projects.

That placement is reconstruction of project ownership and may not preserve the exact original project-item/include metadata.

---

# 24. Namespace and folder reconstruction

Namespaces may support project grouping and recovered source organization.

However:

```text
Namespace
≠
Project
≠
Folder
```

A namespace prefix does not by itself prove:

- project boundary;
- source directory;
- repository layout.

Convention Observations may inform folder/project organization, but generated folders remain reconstruction choices unless directly supported.

---

# 25. PDB/source-path contribution

PDB source document paths may provide particularly useful support for:

- source-file names;
- source-tree grouping;
- possible project-root boundaries;
- generated-source locations.

But a PDB path does not automatically prove:

- the current developer machine/repository path;
- exact project membership without supporting context;
- that all source files are represented.

Project reconstruction should preserve the path as direct PDB-derived information and any project-boundary conclusion as a separate derived/inferential item.

---

# 26. Project file generation

A Candidate Project may be materialized as a generated project file once enough reconstruction choices exist.

The generated project file should express the current Candidate Project state, including where applicable:

- project identity/name;
- project kind;
- target framework/runtime choice;
- Candidate Project references;
- package dependencies;
- assembly/file dependencies;
- source/resource/configuration items;
- required reconstruction properties.

The generated project file is a Reconstructed Artifact.

It must not be treated as independent evidence of the original project file.

---

# 27. Original project-file fidelity

Project reconstruction should preserve a distinction between:

- **semantic recovery** — the recovered development representation expresses the dependencies/build characteristics needed to continue engineering; and
- **textual/historical fidelity** — the generated project file matches the original project's exact XML/text, item ordering, imports, build targets, conditions, or style.

LegacyRevive.NET prioritizes workable semantic recovery.

Exact project-file fidelity should only be claimed where directly supported.

---

# 28. Build-driven project refinement

Build diagnostics may reveal defects in Candidate Project reconstruction.

Examples:

- missing reference;
- incompatible target framework;
- duplicate compile/resource inclusion;
- unresolved generated source;
- missing build property.

Build feedback may trigger project-reconstruction reevaluation.

It does not directly prove the original historical declaration that would have avoided the diagnostic.

Changes made in response remain reconstruction decisions with provenance.

---

# 29. Multiple candidate structures

Where materially different project structures remain plausible, LegacyRevive.NET may preserve multiple Candidate Project structures rather than forcing one answer.

Candidates may differ in:

- boundaries;
- dependency representation;
- target frameworks;
- names;
- solution grouping.

Each candidate must preserve its support and assumptions.

A Developer Decision may select one for active reconstruction.

Unselected candidates may remain historical alternatives.

---

# 30. Candidate selection

Selecting a Candidate Project structure for current use does not change it into a Recovered Fact.

Selection means:

> **this is the project structure LegacyRevive.NET will currently reconstruct against.**

It does not mean:

> **this is proven to be the exact original project structure.**

Selection should be provenance-linked to:

- support profile;
- assumptions;
- Developer Decision where applicable.

---

# 31. Project-reconstruction reevaluation

A Candidate Project or project property becomes `Needs Reevaluation` when a material dependency changes.

Triggers may include:

- new artifact/project metadata;
- corrected assembly identity;
- changed artifact relationship;
- invalidated Assumption;
- superseded Convention Observation;
- Developer Correction/withdrawal;
- changed dependency resolution;
- build diagnostic;
- source reconstruction evidence;
- validation finding.

Reevaluation follows `07-recovery-process.md` and creates superseding current reconstruction state rather than silently rewriting history.

---

# 32. Workable project reconstruction

A Candidate Project can be useful even when some historical properties remain unresolved.

A **Workable Candidate Project** should, where applicable:

- have a coherent boundary;
- expose unresolved boundary/dependency questions;
- represent required dependencies;
- contain or reference recoverable source/resources/configuration;
- record framework/runtime reconstruction;
- retain provenance for inferential choices;
- participate in build/diagnostic evaluation where feasible.

Workable does not mean historically exact.

---

# 33. Qualifications resolved by this document

| Qualification | Resolution |
|---|---|
| `PROJ-001` | Candidate Project is a reconstructed project boundary and remains distinct from Original Project. |
| `PROJ-002` | Managed implementation assembly boundaries are default initial project-candidate anchors, not proof of original project boundaries. |
| `PROJ-003` | Project boundaries expose support profiles and may remain unresolved, produce alternatives, or be selected by Developer Decision. |
| `PROJ-004` | Merge and split of initial project candidates are explicit reconstruction decisions with provenance. |
| `PROJ-005` | Candidate Project identity is stable independently of reconstructed project name; names use strongest available support then explicit fallback. |
| `PROJ-006` | Target framework/project kind are reconstructed properties whose historical certainty is preserved separately from practical reconstruction choice. |
| `PROJ-007` | Observed assembly dependency does not by itself establish original `ProjectReference`/`PackageReference` declaration; dependency representation is a provenance-bearing reconstruction choice. |
| `PROJ-008` | Unresolved dependencies and dependency cycles remain explicit and trigger reevaluation rather than silent repair. |
| `PROJ-009` | Generated project/solution files are Reconstructed Artifacts and cannot become independent evidence of original project structure. |
| `PROJ-010` | Multiple plausible project structures may coexist as candidates; selecting one for active reconstruction does not make it historical fact. |

---

# 34. Durable design consequences

1. Candidate Project and Original Project remain distinct.
2. Implementation assemblies anchor initial reconstruction but do not prove original project boundaries.
3. Boundary decisions retain support profiles.
4. Merge/split decisions are explicit and provenance-bearing.
5. Stable candidate identity is separate from reconstructed names.
6. Framework/project-kind reconstruction preserves uncertainty.
7. Compiled dependency and original declaration mechanism remain distinct.
8. Unresolved dependencies remain explicit.
9. Cycles trigger reevaluation rather than silent mutation.
10. Generated `.sln`/project files remain reconstructed artifacts.
11. Multiple candidate structures may coexist.
12. Active candidate selection is operational, not historical proof.
13. Build feedback refines candidate reconstruction but does not rewrite history.

---

# 35. Non-goals

This document does not define:

- exact `.sln` file syntax;
- exact `.csproj` XML generation algorithm;
- SDK-style versus non-SDK-style implementation policy;
- decompiler/source-generation algorithm;
- NuGet/MSBuild APIs;
- build engine integration;
- project-template library;
- validation metrics;
- CLI commands;
- MVP subset.

Those belong to later canonical documents.

---

# 36. Canonical ownership

This document owns:

- Candidate Project / Candidate Solution semantics;
- Project Boundary reconstruction;
- initial assembly-to-project anchoring;
- merge/split reconstruction;
- project naming/identity semantics;
- project kind/framework reconstruction semantics;
- project dependency representation semantics;
- source/configuration/resource project ownership;
- generated project/solution artifact semantics;
- candidate alternatives and selection;
- project-reconstruction reevaluation.

`09-source-reconstruction.md` owns detailed source recovery.

`10`–`11` own validation.

`12` owns architecture.

`13` owns implementation tooling.

---

# 37. Governance relationship

This document is constrained by accepted decisions in `90-decisions.md`.

Its qualification history is recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
