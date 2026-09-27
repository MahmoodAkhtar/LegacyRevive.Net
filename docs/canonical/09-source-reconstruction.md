# LegacyRevive.NET — Source Reconstruction

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET model for recovering and reconstructing source representations inside Candidate Projects.

It participates primarily in Stage 07 — Candidate Reconstruction from `07-recovery-process.md`.

It owns source-reconstruction provenance classes, source-file identity and ownership, metadata/signature reconstruction, decompiler-generated source semantics, directly preserved source-derived material, inferred source organization, compiler-generated/lowered construct handling, developer-adjusted source, alternative source candidates, source reevaluation, and source-level recovery ceilings.

---

# 1. Purpose

LegacyRevive.NET may need to recreate source code when original source files are missing or incomplete.

Its source-reconstruction objective is:

> **Produce a workable, explainable source representation that preserves as much directly recoverable program structure and behavior as possible while clearly distinguishing historical source-derived information from generated, inferred, repaired, or developer-adjusted code.**

Recovered source is not automatically Original Source.

---

# 2. Source representation classes

Every material source element should be classifiable by provenance.

The canonical classes are:

1. **Preserved Source Content**
2. **Metadata-Reconstructed Source**
3. **Decompiler-Generated Source**
4. **Inferred Source Structure**
5. **Developer-Adjusted Source**

A generated `.cs` file may contain more than one class.

Therefore provenance may need to attach below whole-file level where material distinctions matter.

---

# 3. Preserved Source Content

**Preserved Source Content** is source or source-derived text that survives directly in an admissible Original Artifact.

Examples may include:

- embedded source present in matching symbol artifacts;
- XML documentation text emitted from original source comments;
- source snippets embedded in surviving artifacts where provenance is direct.

Preserved Source Content is not automatically a complete Original Source File.

Where complete embedded source survives, that content may be recovered directly according to its provenance.

---

# 4. Metadata-Reconstructed Source

**Metadata-Reconstructed Source** is generated source syntax representing directly recoverable program metadata.

It may represent:

- namespaces;
- type names and visibility;
- base types/interfaces;
- generic metadata;
- fields/properties/events;
- methods/constructors;
- member signatures;
- attributes.

The underlying metadata may directly establish declaration facts.

The emitted C# syntax remains a reconstructed representation.

---

# 5. Decompiler-Generated Source

**Decompiler-Generated Source** is source-language code synthesized from compiled implementation information.

It may be a useful behavioral representation, but is not Original Source merely because it compiles or resembles likely original C#.

It may differ from original source in:

- expression/control-flow form;
- local names;
- temporaries;
- syntactic sugar;
- ordering;
- formatting;
- helper constructs;
- language-version choices;
- representation of compiler-lowered constructs.

Its provenance must identify it as generated.

---

# 6. Inferred Source Structure

**Inferred Source Structure** is reconstructed organization not directly preserved as complete source layout.

Examples include:

- assigning a type to a generated `.cs` file;
- file/folder naming;
- grouping or splitting types across files;
- namespace/file layout.

Potential support includes PDB paths, type ownership, namespaces, conventions, Candidate Project boundaries, and Developer Decisions.

It must not be presented as original source layout unless independently supported.

---

# 7. Developer-Adjusted Source

**Developer-Adjusted Source** is recovered/reconstructed source deliberately changed through Developer Intervention.

Examples include correcting decompiler artifacts, selecting among plausible source forms, repairing source for buildability, or restoring developer knowledge unavailable from artifacts.

Developer-adjusted code remains distinct from Original Source and from purely generated output.

---

# 8. Source element provenance

Source provenance should be available at the smallest practical material unit.

A source element may be a:

- file;
- type declaration;
- member declaration;
- method body;
- documentation block;
- statement/expression region;
- generated placeholder.

Whole-file provenance is insufficient when materially different provenance classes are mixed.

This document does not prescribe the storage technology.

---

# 9. Recovered Source File identity

A **Recovered Source File** has stable recovery identity separate from its current path/name.

Its path/name may be:

- directly preserved by PDB/source information;
- inferred from type/namespace/project structure;
- convention-guided;
- selected by Developer Decision;
- generated as fallback.

Renaming/reorganizing it must not erase provenance or identity.

---

# 10. PDB/source-path contribution

Where matching symbol information directly provides a source document path, that path is direct source-derived information.

It does not by itself prove current repository layout, complete source coverage, or Candidate Project ownership without supporting context.

File/project assignment remains a separate reconstruction conclusion.

---

# 11. Embedded source precedence

Where admissible artifacts contain embedded source for material that could otherwise be decompiled or inferred, directly preserved source content is the stronger representation for that material.

Generated container/path/project placement may still remain reconstructed.

---

# 12. XML documentation contribution

XML documentation may provide preserved source-derived documentation text and member relationships.

It may be merged into reconstructed source where appropriate.

But XML documentation does not establish complete original comment layout, method implementation, or absence of comments for undocumented members.

---

# 13. Source Skeleton

A **Source Skeleton** is a partial reconstructed source representation containing declarations/signatures without claiming complete implementations.

It is a valid partial recovery when implementation is unavailable or intentionally deferred.

Missing method behavior must remain unresolved or explicit placeholder state rather than being fabricated for completeness.

---

# 14. Method-body reconstruction

Where implementation information is available, LegacyRevive.NET may reconstruct method bodies.

Each method body must distinguish preserved, decompiler-generated, inferred-placeholder, and developer-adjusted material where relevant.

If behavior cannot be reconstructed reliably, the implementation remains unresolved rather than invented.

---

# 15. Compiler-generated and lowered constructs

Compiled artifacts may contain compiler-generated/lowered structures such as helper types, state-machine representations, closures, backing fields, or other transformed forms.

LegacyRevive.NET may present a higher-level C# reconstruction where tooling supports it.

However:

> **A reconstructed higher-level form is a generated interpretation of compiled behavior, not proof of the exact original source construct.**

When reversal is uncertain, preserving a lower-level/generated representation is preferable to inventing source intent.

---

# 16. Generated source and build-time transformations

Compiled output may include code created by source generators or other build-time transformations.

Without surviving inputs/build state, LegacyRevive.NET may recover compiled result behavior but may not recover:

- original generator input;
- generator configuration;
- exact generated source text;
- generation ordering;
- full build pipeline.

Generated behavior remains distinguishable from hand-authored Original Source.

---

# 17. Preprocessor and conditional compilation

Compiled output represents the branch that participated in that build.

It does not by itself establish the full original `#if`/`#else` structure or excluded branches.

LegacyRevive.NET must not invent missing preprocessor structure.

---

# 18. Local names and debug information

Original local-variable names/scopes may survive to different degrees in debug/symbol information.

Where names are not directly preserved, generated names are reconstruction output and must not be presented as original identifiers.

---

# 19. Comments, formatting, and style

Compiled implementation does not by itself recover original comments, whitespace, formatting, file ordering, or style.

Where these survive through admissible source-derived artifacts they may be preserved directly.

Otherwise emitted formatting/style is generated reconstruction.

Convention Observations may guide usability but do not establish historical style.

---

# 20. Language-version and syntax choices

Equivalent compiled behavior may be expressible with different C# forms.

LegacyRevive.NET may select a practical representation appropriate to the Candidate Project.

Unless directly supported, the selected syntax/language-version form remains reconstruction rather than exact original syntax.

---

# 21. File grouping and partial types

Compiled type boundaries do not automatically establish original source-file grouping.

PDB/source document evidence should be preferred where available.

Otherwise file grouping is reconstructive.

A compiled type may have originated from multiple partial declarations; absent direct source-derived evidence, LegacyRevive.NET should not invent historical partial-file boundaries merely because they are plausible.

---

# 22. Candidate Project ownership

Every Recovered Source File must have a current Candidate Project ownership relationship or remain explicitly unassigned.

Ownership follows `08-project-reconstruction.md`.

Source organization alone must not silently move source between Candidate Projects.

---

# 23. Source Candidates

A **Source Candidate** is one materially plausible source reconstruction among alternatives.

Candidates may differ in:

- high-level reconstruction of lowered code;
- file grouping;
- syntax form;
- unresolved semantic choices.

Each candidate retains provenance/support.

Selecting one for active reconstruction is operational and does not make it Original Source.

---

# 24. Source candidate selection

Selection should prefer, in order:

1. directly preserved source content;
2. directly supported metadata/signature representation;
3. decompiler-generated representation with stronger behavioral support;
4. convention/inference-based organization;
5. Developer Decision;
6. explicit unresolved/placeholder representation where evidence is insufficient.

This is recovery precedence, not a claim about historical aesthetics.

---

# 25. Build-driven source repair

Build diagnostics may expose defects in reconstructed source.

Repairs may correct generated syntax, reconstructed signatures, ownership, or unresolved stubs.

A build repair remains reconstructed or Developer-Adjusted Source.

Successful compilation does not establish that repaired text matches Original Source.

---

# 26. Behavioral caution

A decompiled/reconstructed method that compiles is not automatically behaviorally equivalent to the original application in all contexts.

Detailed behavioral validation belongs to `10-validation-strategy.md`.

Buildability is not proof of source fidelity.

---

# 27. Source fidelity dimensions

Source reconstruction may differ across dimensions such as:

- declaration/signature fidelity;
- control-flow/behavioral representation;
- identifier fidelity;
- file organization;
- documentation/comment fidelity;
- formatting/style;
- compiler/source-language form.

These must not be collapsed into one source-fidelity claim here.

Detailed measurement belongs to `11-recovery-metrics.md`.

---

# 28. Source recovery ceilings

Unless suitable source-derived artifacts survive, the following may remain not established or not reliably recoverable from the available artifact set:

- exact original comments;
- exact formatting/whitespace;
- exact local names;
- exact source-file partition;
- full preprocessor structure;
- source-generator inputs;
- build-time transformation inputs;
- exact high-level source form behind compiler lowering;
- developer intent behind source structure.

New artifacts may raise these ceilings.

---

# 29. Generated source anti-feedback rule

Any source emitted by LegacyRevive.NET remains a Reconstructed Artifact.

Rescanning it must not independently corroborate:

- metadata/signature reconstruction;
- inferred source organization;
- reconstructed control flow;
- inferred naming;
- Candidate Project ownership.

Dependency lineage must prevent self-corroboration.

---

# 30. Source reconstruction reevaluation

Recovered source becomes `Needs Reevaluation` when material dependencies change, including:

- new PDB/embedded source;
- corrected metadata;
- changed Candidate Project ownership;
- invalidated Assumption;
- superseded Convention Observation;
- Developer Correction/withdrawal;
- dependency resolution;
- build diagnostic;
- validation finding.

Updated source supersedes earlier reconstruction rather than silently rewriting history.

---

# 31. Workable Recovered Source

**Workable Recovered Source** is source reconstruction coherent enough for continued engineering use while retaining provenance and unresolved limitations.

It may combine preserved content, reconstructed declarations, decompiled bodies, generated organization, unresolved sections, and developer repairs.

Workable does not mean Original Source.

---


# 32. Manual edits to materialized recovered source

Recovered source may be materialized into the developer-facing filesystem workspace so that ordinary IDEs/editors can inspect, build, debug, refactor, or otherwise work with it.

A direct filesystem edit to that materialized source is initially **Working-Tree Divergence** only. The edit does not automatically become:

- Developer Input;
- a committed Developer Intervention;
- Developer-Adjusted Source;
- Original Source;
- Original-System Evidence.

LegacyRevive.NET must preserve the distinction between an incidental/experimental working-tree edit and a deliberate recovery change.

When a developer explicitly **adopts** a divergent source change, LegacyRevive.NET records the appropriate committed Developer Intervention and the adopted material becomes **Developer-Adjusted Source** with provenance linking it to the prior recovered source and the intervention. Where the adopted change replaces current reconstructed material, normal supersession/reevaluation rules apply.

When a developer explicitly **discards** an unadopted divergent change, LegacyRevive.NET rematerializes the canonical source representation. The discarded working edit does not enter canonical recovery history merely because it temporarily existed on disk.

This workflow is editor/IDE independent. LegacyRevive.NET does not require source changes to be authored through a LegacyRevive-specific editor or UI.

**Governance:** SRC-012 / D165.

---

# 33. Qualifications resolved by this document

| Qualification | Resolution |
|---|---|
| `SRC-001` | Source material is classified as Preserved Source Content, Metadata-Reconstructed Source, Decompiler-Generated Source, Inferred Source Structure, or Developer-Adjusted Source. |
| `SRC-002` | Source provenance may attach below whole-file level where materially different provenance classes are mixed. |
| `SRC-003` | Recovered Source File identity is stable independently of generated path/name; direct PDB/source paths retain separate provenance. |
| `SRC-004` | Directly preserved source content outranks weaker regeneration for the same material while generated container/layout remains separately reconstructed. |
| `SRC-005` | Source Skeleton is a valid partial recovery and unknown behavior is not fabricated. |
| `SRC-006` | High-level reconstruction of compiler-lowered code is generated interpretation, not proof of exact original syntax. |
| `SRC-007` | Local names, comments, formatting, preprocessor structure, generator inputs, file partition, and exact high-level source form remain explicit artifact-set-scoped recovery ceilings unless directly supported. |
| `SRC-008` | Source organization and Candidate Project ownership are provenance-bearing reconstruction choices; namespaces/types do not alone prove original files/folders. |
| `SRC-009` | Multiple Source Candidates may coexist; selection is operational and does not make one Original Source. |
| `SRC-010` | Build-driven source repair remains reconstructed or Developer-Adjusted Source and does not prove historical text. |
| `SRC-011` | Generated source remains a Reconstructed Artifact and cannot independently corroborate the recovery decisions that produced it. |
| `SRC-012` | Manual edits to materialized recovered source remain Working-Tree Divergence until explicitly adopted; adoption creates provenance-bearing Developer-Adjusted Source and discard rematerializes canonical source. |

---

# 34. Durable design consequences

1. Recovered source is not one epistemic category.
2. Provenance may need member/region granularity.
3. Direct source-derived content is preserved distinctly.
4. Metadata facts are distinct from emitted C# syntax.
5. Decompiled C# is generated representation, not Original Source.
6. Source Skeleton is a legitimate partial result.
7. Unknown method behavior is not invented for completeness.
8. Compiler-lowered/generated-code reconstruction preserves uncertainty.
9. Generated local names/style/layout are not original identifiers/style/layout.
10. Source organization and Candidate Project ownership remain provenance-bearing choices.
11. Multiple Source Candidates may coexist.
12. Build repair changes reconstruction, not history.
13. Generated source cannot self-corroborate.
14. Recovery ceilings remain explicit and artifact-set scoped.
15. Materialized-source edits remain outside canonical recovery state until explicitly adopted; unadopted divergence may be discarded without becoming recovery history.

---

# 35. Non-goals

This document does not define:

- decompiler/library selection;
- decompiler configuration;
- exact formatting rules;
- Roslyn API design;
- exact placeholder syntax;
- source-control history reconstruction;
- source-generator execution;
- project-file generation;
- validation metrics;
- CLI commands;
- MVP subset.

---

# 36. Canonical ownership

This document owns source-reconstruction provenance classes, source-file identity/provenance, metadata/skeleton/decompiler semantics, inferred source organization, compiler-lowered/generated-code boundaries, source alternatives/selection, build-driven repair semantics, manual materialized-source adoption/discard semantics, source recovery ceilings, and source reevaluation.

`08-project-reconstruction.md` owns Candidate Project boundaries and source ownership targets.

`10`–`11` own validation and metrics.

`12`–`13` own architecture and tooling.

---

# 37. Governance relationship

This document is constrained by accepted decisions in `90-decisions.md`.

Its qualification history is recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
