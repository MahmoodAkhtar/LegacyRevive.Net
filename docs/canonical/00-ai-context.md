# LegacyRevive.NET — AI Context

## Status

**Canonical orientation and project index**

This file is the primary orientation document for AI-assisted work on **LegacyRevive.NET**.

It should be read first when starting a new design, specification, research, validation, or implementation discussion related to LegacyRevive.NET.

This document is intentionally concise. It does not own detailed domain definitions. Those belong in the canonical documents identified below.

---

# 1. Product definition

**LegacyRevive.NET** is a developer-assistance tool for recovering and reconstructing legacy .NET applications when the original development solution, projects, or source are missing, incomplete, damaged, inaccessible, or otherwise unusable.

Its primary scenario is that a developer has surviving or deployed artifacts rather than a complete usable development codebase.

Typical inputs may include assemblies, executables, configuration, PDBs, XML documentation, runtime metadata, resources, package/dependency metadata, and other deployment or build artifacts.

The practical objective is:

> **Recover enough accurate structure, source, configuration, dependency information, and supporting evidence to create a workable .NET solution that a developer can understand, build where possible, validate, repair, and continue maintaining.**

LegacyRevive.NET does not promise exact reproduction of the original repository, source code, project layout, or developer intent.

The authoritative product purpose and principles are defined in:

`01-purpose-and-principles.md`

---

# 2. Core project stance

LegacyRevive.NET is a **recovery assistant, not an oracle**.

The project has accepted the following foundational directions:

- workable recovery is more important than pretending to reproduce the original repository exactly;
- evidence and inference must remain distinguishable;
- provenance is a core requirement;
- generated reconstruction must not become independent evidence for its own correctness;
- developer intervention is a first-class part of recovery;
- inferred conventions are derived observations, not guaranteed facts about original developer intent;
- validation reference must remain separate from recovery input;
- validation should remain multidimensional;
- buildability does not prove behavioural equivalence;
- recovery should generally proceed from stronger evidence toward weaker inference;
- original recovery artifacts remain immutable;
- confidence should be explainable rather than expressed as arbitrary numeric precision;
- mature .NET tooling should be reused where appropriate;
- recovery should be deterministic where practical.

The authoritative decision history is maintained in:

`90-decisions.md`

Do not treat this summary as a replacement for the decision log.

---

# 3. Fundamental distinctions

Future work must preserve these established distinctions:

```text
Evidence ≠ inference
Inference ≠ fact
Recovered source ≠ original source
Recovered structure ≠ necessarily original structure
Compilable ≠ behaviourally equivalent
Similarity ≠ equivalence
Convention observation ≠ original developer intent
Developer intervention ≠ original evidence
Validation reference ≠ recovery input
Reconstructed artifact ≠ original artifact
```

Canonical terminology is defined in:

`02-terminology.md`

Where terminology remains qualified or unresolved, the qualification register controls its status.

---

# 4. Governance

All canonical-document work must follow the governance process defined by:

- `90-decisions.md`
- `91-design-qualification-register.md`

The governing documentation rule is:

> **Chats explore; documents consolidate.**

`91-design-qualification-register.md` is the single source of truth for unresolved and resolved design qualifications.

`90-decisions.md` is the source of truth for durable accepted design decisions.

Canonical domain documents are the source of truth for their own subject matter.

Before creating or revising a canonical document:

1. read this file;
2. read earlier canonical dependencies;
3. read `90-decisions.md` for constraining accepted decisions;
4. read `91-design-qualification-register.md` for relevant qualifications;
5. follow the normative governance lifecycle in `91`;
6. audit the resulting document;
7. synchronize any changes across the canonical document, its audit, `90`, and `91`.

No material qualification should disappear merely because work moves to another document or conversation.

---

# 5. Evidence-model status

The formal Evidence Model is now defined in:

`03-evidence-model.md`

It resolves the former `EVID-001` through `EVID-010` qualifications and establishes the canonical semantics for:

- Evidence versus Developer Input;
- Observation versus Recovered Fact;
- inference evolution and supersession;
- conflict representation;
- Provenance and derivation lineage;
- confidence/support strength;
- Assumption lifecycle;
- developer correction/supersession at the evidence-history level;
- circular-evidence protection.

Detailed developer-intervention workflow remains owned by `04-developer-intervention.md`.

Current qualification status remains authoritative in:

`91-design-qualification-register.md`
---

# 6. Developer intervention

The canonical Developer Intervention model is now defined in:

`04-developer-intervention.md`

It establishes:

- Developer Assertion / Decision / Correction workflow semantics;
- explicit scoped Intervention Records;
- Active / Superseded / Withdrawn lifecycle;
- persistence and replay;
- intervention conflict handling;
- withdrawal and downstream reevaluation.

Developer Input remains separate from Evidence under `03-evidence-model.md`.
---

# 7. Convention inference

The canonical Convention Inference model is now defined in:

`05-convention-inference.md`

It establishes:

- Convention Observations as scoped Derived Observations;
- explicit recurrence support and exception reporting;
- scope/generalization rules;
- descriptive support profiles;
- Convention Conflicts;
- history/reevaluation semantics;
- anti-feedback-loop protections for reconstruction.

Convention observations remain distinct from original developer intent.
---

# 8. Recovery matrix

The canonical artifact Recovery Matrix is now defined in:

`06-recovery-matrix.md`

It establishes:

- Direct / Inferable / Heuristic / Not-established contribution classes;
- artifact-type versus artifact-instance capability;
- artifact-specific contribution and limitation profiles;
- missing-artifact semantics;
- companion-artifact relationship rules;
- recovery-ceiling wording.

The canonical Recovery Process in `07-recovery-process.md` consumes these capabilities without overstating what an artifact establishes.

---

# 9. Recovery process

The canonical end-to-end Recovery Process is now defined in:

`07-recovery-process.md`

It establishes:

- stage-oriented but iterative recovery;
- intake → inventory → direct extraction → integration → inference → planning/intervention → reconstruction → build diagnostics → validation → baseline;
- earliest-affected-stage reevaluation;
- Recovery Workspace / Recovery Run / Recovery Checkpoint;
- partial and blocked recovery states;
- Stage 01 supplied-source discovery distinguished from Artifact preservation, including explicit incomplete-enumeration semantics;
- build/validation feedback boundaries.

The process coordinates `03`–`06` without changing their semantic categories.

---

# 10. Project reconstruction

The canonical Project Reconstruction model is now defined in:

`08-project-reconstruction.md`

It establishes:

- Candidate Project / Original Project distinction;
- implementation-assembly project-candidate anchoring;
- Project Boundary support profiles/conflicts;
- explicit merge/split decisions;
- project identity/name/framework/kind reconstruction;
- dependency representation without assuming historical `ProjectReference`/`PackageReference`;
- multiple candidate structures;
- generated project/solution files as Reconstructed Artifacts.

Project reconstruction participates primarily in Stage 07 of the Recovery Process.

---

# 11. Source reconstruction

The canonical Source Reconstruction model is now defined in:

`09-source-reconstruction.md`

It establishes:

- preserved source content versus reconstructed/generated source;
- metadata-reconstructed and decompiler-generated source;
- inferred source organization and Developer-Adjusted Source;
- fine-grained source provenance;
- Source Skeleton and Source Candidate semantics;
- compiler-lowered/generated-code uncertainty;
- source recovery ceilings;
- generated-source anti-feedback protection.

Source reconstruction participates primarily in Stage 07 within Candidate Projects from `08`.

---

# 12. Validation strategy

The canonical Validation Strategy is now defined in:

`10-validation-strategy.md`

It establishes:

- identified Validation Targets and Reference Snapshots;
- Validation Plans and multidimensional validation;
- Not Evaluated / Not Comparable semantics;
- structural, dependency, API, source, build, and behavioral validation boundaries;
- blind validation with recovery frozen before reference access;
- reference-derived feedback as a new recovery lineage;
- validation provenance and reproducibility;
- Validated Baseline semantics.

Detailed measurements belong to `11-recovery-metrics.md`.
---

# 13. Recovery metrics

The canonical Recovery Metrics model is now defined in:

`11-recovery-metrics.md`

It establishes:

- explicit applicability, populations, numerators, and denominators;
- correctness versus coverage separation;
- precision/recall for set-oriented recovery;
- raw historical fidelity versus recoverability-aware effectiveness;
- structural, dependency, API/source, runtime, operational, and epistemic metrics;
- developer-intervention burden measurement;
- transparent aggregation rules with no authoritative global recovery score;
- cross-run/scenario comparability and metric-history rules.

Concrete metric/result storage and execution are implemented through the architecture/tooling documents while preserving these semantics.

---

# 14. Technical architecture

The canonical Technical Architecture is now defined in:

`12-architecture.md`

It establishes:

- a logical modular, domain-centered architecture;
- canonical state partitions for artifacts, knowledge, interventions, reconstruction, checkpoints, validation, and metrics;
- artifact-analyzer and explicit inference-rule boundaries;
- first-class support/provenance graph semantics;
- Recovery Orchestrator responsibilities;
- reconstruction feedback isolation;
- blind-validation reference isolation;
- checkpoint/replay and structured diagnostic semantics;
- extension contracts that preserve canonical epistemic boundaries;
- persistence/history requirements that remain independent of concrete storage technology;
- workspace-owned immutable snapshot preservation of successfully admitted MVP Original Artifact bytes beneath `.legacyrevive`, with original external paths retained as provenance rather than replay dependencies;
- the MVP local-workspace physical boundary in which `.legacyrevive` contains LegacyRevive.NET-owned internal state and the reconstructed development tree is materialized outside it for ordinary developer tools;
- Working-Tree Divergence as operational materialization state that does not by itself alter canonical recovery state.

Concrete tooling/dependency choices belong to `13-tooling-and-dependencies.md`.

---

# 15. Tooling and dependencies

The canonical Tooling and Dependencies model is now defined in:

`13-tooling-and-dependencies.md`

It establishes:

- .NET 10 LTS as the LegacyRevive application/runtime baseline;
- `LegacyRevive.Net.slnx` as the canonical root solution filename for the LegacyRevive.NET repository, using the retained SLNX format and remaining distinct from reconstructed Candidate Solution output;
- central explicit package-version governance;
- System.Reflection.Metadata for direct managed metadata/Portable-PDB extraction;
- ICSharpCode.Decompiler for C# decompilation;
- Roslyn for reconstructed C# syntax/semantic operations;
- Microsoft.Build + Microsoft.Build.Locator for project model access with out-of-process restore/build execution;
- NuGet.Packaging for local package inspection and optional NuGet.Protocol enrichment;
- SQLite/Microsoft.Data.Sqlite for local canonical state persistence;
- Generic Host/Microsoft DI/logging + System.CommandLine for the host/CLI;
- UUIDv7 generated identities plus SHA-256 artifact fingerprints;
- xUnit v3 for automated testing;
- mandatory version/license/security/provenance review for external dependencies.

MVP capability scope remains owned by `14-mvp.md`.

---

# 16. Minimum viable product

The canonical MVP is now defined in:

`14-mvp.md`

It establishes:

- an end-to-end managed-.NET artifact recovery product slice;
- Windows x64 as the first supported execution environment;
- local-directory input with managed binary/config/symbol/document/package analyzers and workspace-owned immutable preservation of successfully admitted supplied artifact bytes;
- ownership classification, Candidate Projects, dependency graph reconstruction and decompiled C#;
- deterministic candidate solution/project/source export from canonical committed recovery state rather than arbitrary working-tree contents;
- explicit developer intervention plus basic recovered context;
- persistent resumable Recovery Workspaces and checkpoints;
- ordinary IDE/editor use of the materialized reconstruction, with explicit adoption of manual source changes as Developer-Adjusted Source or explicit discard/rematerialization;
- export blocking/reporting when material Working-Tree Divergence remains unresolved;
- controlled build attempts without speculative historical fabrication;
- CLI-first recover/status/explain/decide/build/export capabilities;
- blind held-out validation and multidimensional release gates;
- explicit post-MVP exclusions.

Later capability expansion belongs to `15-roadmap.md`.

---

# 17. Post-MVP roadmap

The canonical post-MVP roadmap is now defined in:

`15-roadmap.md`

It establishes:

- evidence/dependency-gated horizons rather than canonical date promises;
- promotion criteria based on recovery impact, intervention reduction, safety, frequency, leverage, cost, and validation readiness;
- core-loop strengthening as the first post-MVP priority;
- later platform/artifact and external-enrichment expansion;
- advanced inference/ranking that remains subordinate to the Evidence Model;
- richer hosts that reuse canonical Application/Domain semantics;
- validation/corpus maturity alongside product growth;
- runtime-plugin and collaborative/enterprise expansion only after dedicated governance;
- native/runtime recovery as a separate research/capability domain.

With this document, the initial `00`–`15` canonical sequence is complete.

---

# 18. Relationship to the wider Legacy tool family

The current accepted distinction is:

- **LegacyRevive.NET** — recover a workable development baseline from surviving/deployed artifacts when the original development representation is missing or unusable;
- **LegacyLens.NET** — help understand and investigate an existing legacy .NET source codebase;
- **LegacySafety.NET** — future concept focused on quickly establishing regression/characterization-test safety around legacy code.

These tools are complementary but have distinct primary responsibilities.

---

# 19. Canonical document set and order

The current canonical document-development order is:

```text
00-ai-context.md
01-purpose-and-principles.md
02-terminology.md
03-evidence-model.md
04-developer-intervention.md
05-convention-inference.md
06-recovery-matrix.md
07-recovery-process.md
08-project-reconstruction.md
09-source-reconstruction.md
10-validation-strategy.md
11-recovery-metrics.md
12-architecture.md
13-tooling-and-dependencies.md
14-mvp.md
15-roadmap.md
16-development-process.md

90-decisions.md
91-design-qualification-register.md
```

The numbered `00`–`16` documents form the canonical domain/design/development-process sequence.

`90` and `91` are cross-cutting governance documents and are updated whenever the governance process requires it.

---

# 20. Canonical ownership

Each major concept should have one primary authoritative home.

Current ownership is:

| Subject | Canonical document |
|---|---|
| Project orientation and document map | `00-ai-context.md` |
| Purpose, goals, non-goals, principles | `01-purpose-and-principles.md` |
| Terminology | `02-terminology.md` |
| Evidence/provenance/confidence model | `03-evidence-model.md` |
| Developer intervention | `04-developer-intervention.md` |
| Convention inference | `05-convention-inference.md` |
| Artifact recoverability | `06-recovery-matrix.md` |
| Recovery stages/process | `07-recovery-process.md` |
| Project/solution reconstruction | `08-project-reconstruction.md` |
| Source reconstruction | `09-source-reconstruction.md` |
| Validation methodology | `10-validation-strategy.md` |
| Recovery metrics | `11-recovery-metrics.md` |
| Technical architecture | `12-architecture.md` |
| Tooling/dependencies | `13-tooling-and-dependencies.md` |
| MVP scope | `14-mvp.md` |
| Later/deferred capabilities | `15-roadmap.md` |
| Implementation development process and Development Slice Specifications | `16-development-process.md` |
| Durable accepted decisions | `90-decisions.md` |
| Qualification state/history and governance lifecycle | `91-design-qualification-register.md` |

If two canonical documents appear to conflict, do not silently reconcile them. Register the material conflict in `91` and resolve it through the governance process.

---

# 21. Current project state

Completed foundational/domain/governance documents:

- `00-ai-context.md`
- `01-purpose-and-principles.md`
- `02-terminology.md`
- `03-evidence-model.md`
- `04-developer-intervention.md`
- `05-convention-inference.md`
- `06-recovery-matrix.md`
- `07-recovery-process.md`
- `08-project-reconstruction.md`
- `09-source-reconstruction.md`
- `10-validation-strategy.md`
- `11-recovery-metrics.md`
- `12-architecture.md`
- `13-tooling-and-dependencies.md`
- `14-mvp.md`
- `15-roadmap.md`
- `90-decisions.md`
- `91-design-qualification-register.md`

The original `00`–`15` domain/design sequence is complete. `16-development-process.md` was subsequently added through DEV-001 / D168 and is now canonical for the implementation-development process and Development Slice Specification model.

As of the whole-set governance audit and subsequent re-audits dated 2026-09-26, `DOC-020` is Resolved. Remote external enrichment is post-MVP capability scope owned by `15-roadmap.md`; `13-tooling-and-dependencies.md` may preselect an adapter for that future capability without making it part of the MVP. The required MVP remains local/offline.

Future material design work should begin from the owning canonical document for the affected subject and use `90-decisions.md` / `91-design-qualification-register.md` for governance rather than assuming a new numbered document is automatically required.

Material implementation work should follow `16-development-process.md`, including use of a Development Slice Specification where that process requires one. Development Slice Specifications remain durable non-canonical implementation artifacts and do not replace the authoritative canonical source for the behavior they implement.
---

# 22. Guidance for future AI-assisted work

When working in this Project:

1. treat Project source documents as authoritative context;
2. read this orientation document first;
3. follow canonical ownership rather than duplicating definitions;
4. preserve established terminology;
5. do not silently override accepted decisions;
6. do not present open qualifications as settled;
7. preserve provenance and uncertainty;
8. distinguish original evidence, inference, developer input, reconstruction, and validation;
9. challenge proposals that risk recursive assumption amplification;
10. keep recovery input separate from validation reference;
11. use the governance lifecycle for every canonical-document change;
12. when a design conclusion matures, update its owning canonical document;
13. for material implementation work, follow `16-development-process.md` and the applicable `DEV-SPEC-*` Development Slice Specification rather than relying on chat context;
14. keep the practical objective in view: help the developer reach a workable recovered .NET development baseline.

---

# 23. Role of this file

`00-ai-context.md` is an **orientation and index**, not a substitute for the rest of the Project source.

It should remain relatively compact and should not become the place where detailed evidence semantics, recovery algorithms, architecture, validation metrics, or MVP requirements are independently defined.

When a detailed statement here conflicts with the canonical owner of that subject, the owning canonical document and governance process must be used to resolve the conflict rather than silently treating this file as authoritative for that detail.