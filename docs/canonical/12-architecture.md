# LegacyRevive.NET — Technical Architecture

## Status

**Canonical**

This document defines the canonical logical technical architecture for LegacyRevive.NET.

It translates the established evidence, recovery, reconstruction, intervention, validation, and metric semantics into implementable component boundaries without selecting concrete third-party libraries, databases, hosting frameworks, or deployment topology.

Concrete tooling and dependency choices belong to:

`13-tooling-and-dependencies.md`

This document is constrained by `90-decisions.md` and the resolved `ARCH-*` qualifications in `91-design-qualification-register.md`.

---

# 1. Purpose

The architecture answers:

> **How should LegacyRevive.NET be structured so that its implementation preserves the canonical recovery semantics, provenance boundaries, iteration model, validation isolation, and explainability requirements?**

The architecture must make invalid semantic shortcuts difficult.

In particular, it must not allow:

```text
Analyzer output → silently treated as historical fact
Reconstructed output → fed back as independent evidence
Developer choice → silently promoted to original-system evidence
Validation reference → exposed to blind recovery
Build success → promoted to behavioral equivalence
Mutable current state → erase historical provenance
External tool behavior → redefine domain semantics
```

---

# 2. Architectural stance

LegacyRevive.NET uses a **logical modular architecture with a domain-centered core**.

The logical modules may initially execute in one process or be hosted differently later. The canonical architecture therefore defines dependency and responsibility boundaries, not microservice/process boundaries.

The core principle is:

```text
External artifacts / developer input / tool outputs
                    │
                    ▼
              Ports / adapters
                    │
                    ▼
        Canonical recovery application
                    │
                    ▼
           Domain recovery model
                    │
                    ▼
      Reconstruction / validation outputs
```

The domain model must remain independent of specific decompiler, MSBuild, PDB, package, database, UI, or host implementations.

**Governance:** ARCH-001 / D105.

---

# 3. Top-level logical architecture

```text
┌───────────────────────────────────────────────────────────────┐
│ Hosts / User Interaction                                     │
│ CLI • future UI/API • automation host                        │
└──────────────────────────────┬────────────────────────────────┘
                               │ commands/queries
                               ▼
┌───────────────────────────────────────────────────────────────┐
│ Recovery Application Layer                                   │
│ Recovery Orchestrator • use cases • checkpoint coordination  │
│ intervention workflow • reconstruction/validation commands   │
└──────────────────────────────┬────────────────────────────────┘
                               │
          ┌────────────────────┼────────────────────┐
          ▼                    ▼                    ▼
┌─────────────────┐  ┌────────────────────┐  ┌──────────────────┐
│ Domain Core      │  │ Capability Modules │  │ State/History     │
│ evidence model   │  │ artifact analyzers │  │ artifact catalog  │
│ inference rules  │  │ reconstructors     │  │ knowledge store   │
│ project/source   │  │ build adapters     │  │ intervention log  │
│ validation model │  │ validators         │  │ checkpoints       │
│ metric semantics │  │ reporters          │  │ validation history │
└────────┬─────────┘  └──────────┬─────────┘  └─────────┬────────┘
         │                       │                      │
         └───────────────────────┼──────────────────────┘
                                 ▼
┌───────────────────────────────────────────────────────────────┐
│ Infrastructure Adapters                                      │
│ filesystem • metadata/PDB • decompiler • packages • MSBuild  │
│ persistence • process execution • clocks/ids • report output │
└───────────────────────────────────────────────────────────────┘
```

This diagram is logical. A box does not imply a separate process, package, assembly, or service.

---

# 4. Dependency rule

Dependencies point toward canonical recovery semantics.

A practical source-level direction is:

```text
Host
  ↓
Application
  ↓
Domain/Core

Infrastructure / tool adapters
  └──────────────→ implement ports defined inward
```

The Domain/Core must not depend on concrete external-tool packages.

Application is the normal gateway to Domain/Core for outer adapter/capability projects. Adapter projects reference Application-owned capability contracts by default. A direct adapter-to-Domain/Core dependency is permitted only when implementing an inward-owned capability contract genuinely requires Domain/Core-owned types or behaviour; such a dependency is exceptional and should remain explicit and justifiable rather than arising from convenience.

This rule does not require Application-owned DTOs or duplicate representations merely to conceal a genuine Domain/Core dependency. A justified direct dependency may arise because an Application-owned port intentionally exposes Domain/Core types, or because a capability contract is itself correctly owned by Domain/Core under the canonical Capability Port model. The latter must not be introduced merely to bypass the normal Application gateway.

Capability modules and adapters must not bypass canonical state/provenance rules. The host/composition root may reference Application and the concrete adapter implementations required to compose the application, consistent with the host boundary defined by D127.

This architecture is compatible with ordinary .NET dependency injection, but does not select a specific DI container here.

**Governance:** ARCH-013 / D161.

---

# 5. Canonical state partitions

LegacyRevive.NET has several semantically distinct state areas. They may share one physical persistence technology later, but the architecture must preserve their logical separation.

## 5.1 Original Artifact Store

For the MVP, contains workspace-owned immutable snapshots of successfully preserved supplied Original Artifact bytes. The preserved bytes are held beneath the LegacyRevive.NET-owned `.legacyrevive` boundary and become the authoritative byte representation used by subsequent recovery/replay.

Responsibilities:

- stable Artifact identity;
- SHA-256 content hash;
- original supplied path/name and capture context as provenance;
- workspace-owned preserved-byte availability;
- artifact classification state;
- preservation/integrity diagnostics.

Successful intake preservation requires the exact supplied bytes to be copied/snapshotted into the workspace-owned artifact store and verified before the artifact is considered preserved. A recorded hash can establish content identity/integrity but does not substitute for possession of the preserved bytes. After successful preservation, the original external location is provenance rather than an operational dependency for replay.

Original-artifact bytes are never modified in place by reconstruction or later recovery activity. A later materially different supplied artifact enters through normal intake as new recovery state rather than mutating the preserved snapshot. Equal content hashes do not by themselves collapse distinct Artifact identities or supplied-path provenance. Physical deduplication may be an implementation optimization only if those logical distinctions remain intact. Large artifact bytes remain filesystem/artifact-store backed rather than SQLite BLOBs.

Support for externally managed immutable artifact references is not part of the MVP preservation contract and would require separate governed design.

## 5.2 Recovery Knowledge Store

Contains canonical recovery knowledge/history such as:

- Observations;
- Recovered Facts;
- Derived Observations;
- Inferences;
- Assumptions;
- Conflicts;
- support/provenance edges;
- supersession/current-state links.

It is append-preserving for material historical conclusions.

## 5.3 Developer Intervention Journal

Contains committed:

- Developer Assertions;
- Developer Decisions;
- Developer Corrections;
- scope;
- status;
- attribution/provenance;
- supersession/withdrawal relationships.

It is logically separate from original-system Evidence.

## 5.4 Reconstruction Workspace

Contains:

- Candidate Projects;
- Source Candidates;
- generated project/solution files;
- reconstructed source;
- build-repair outputs;
- active reconstruction selection;
- unresolved reconstruction decisions.

Reconstructed artifacts are not stored as independent Original-System Evidence.

## 5.5 Recovery Checkpoint Store

Records stable recovery checkpoints sufficient to identify/replay a Validation Target or later recovery state.

A checkpoint references the relevant state/history; it does not rewrite prior conclusions.

## 5.6 Validation and Metrics Store

Contains:

- Validation Plans;
- Validation Targets;
- Validation Reference Snapshot identifiers;
- Validation Runs;
- Validation Findings;
- Recovery Metric definitions/versions;
- metric results;
- environment/comparator context.

Validation history remains separate from the recovery support graph.

**Governance:** ARCH-002 / D106.

---

# 6. Stable identity and provenance

Domain records require stable identities independent of mutable display names, generated paths, or reconstruction locations.

Examples include:

```text
ArtifactId
ObservationId
FactId
DerivedObservationId
InferenceId
AssumptionId
ConflictId
InterventionId
ProjectCandidateId
RecoveredSourceFileId
SourceCandidateId
RecoveryRunId
RecoveryCheckpointId
ValidationRunId
MetricDefinitionId
MetricResultId
```

The exact ID representation belongs to implementation/tooling work.

Canonical requirements are:

- identity remains stable through renaming/path changes;
- provenance references identities, not only display strings;
- supersession does not reuse an old identity for a materially new conclusion;
- generated path/name is not the identity of recovered historical material.

---

# 7. Artifact analysis boundary

An **Artifact Analyzer** inspects one or more admissible artifacts and produces direct extraction results.

Analyzer responsibilities may include:

- metadata reading;
- configuration parsing;
- PDB/document inspection;
- resource discovery;
- package/runtime metadata inspection;
- decompiler-accessible low-level implementation representation;
- diagnostics about unreadable/unsupported content.

Analyzers do **not** directly declare historical architecture conclusions such as:

> "This DLL was definitely an original project."

Instead, analyzers emit provenance-bearing **Observations** and diagnostics.

Deterministic semantics-preserving normalization may convert admissible observations into Recovered Facts through the canonical Evidence Model.

Inference belongs to inference/rule components, not hidden inside artifact readers.

**Governance:** ARCH-003 / D107.

---

# 8. Knowledge integration and support graph

The architecture represents material support relationships as explicit edges.

Conceptually:

```text
Artifact
   ↓ produces/supports
Observation
   ↓ directly entails
Recovered Fact
   ↓
Derived Observation
   ↓
Inference
   ↓
Reconstruction Decision
```

Additional links include:

```text
supports
depends-on
corroborates
conflicts-with
supersedes
invalidates
needs-reevaluation
derived-from
```

The support graph must preserve upstream dependencies so that:

- circular corroboration can be rejected/detected;
- downstream items can be found when support changes;
- developer corrections can trigger reevaluation;
- generated reconstruction cannot recursively strengthen its own premise.

**Governance:** ARCH-004 / D108.

---

# 9. Inference engine and rule model

Inference is executed by explicit, named inference rules.

An inference rule should expose:

- rule identity/version;
- eligible input proposition types;
- preconditions;
- produced proposition/reconstruction candidate;
- support dependencies;
- support-strength classification logic;
- diagnostics/explanation.

Example shape:

```text
Rule:
  LocalAssemblyProjectCandidate

Inputs:
  assembly A references B
  B artifact is present
  B classified application-owned/internal candidate

Produces:
  inference that B may anchor a Candidate Project

Support:
  explicit Fact/DerivedObservation IDs
```

Rules must not:

- consume their own descendants as independent support;
- consume Validation Reference material during blind recovery;
- convert Developer Decisions into historical facts;
- hide an assumption required for the rule to fire.

Inference-rule registration is extensible, but rule execution remains governed by the central support/provenance model.

**Governance:** ARCH-005 / D109.

---

# 10. Convention analysis

Convention analysis is a specialized derived-observation capability.

It consumes eligible historical evidence/recovered facts and produces scoped Convention Observations with:

- scope;
- population;
- recurrence/support profile;
- exceptions;
- provenance;
- conflict state where applicable.

It must not consume:

- generated project layout as evidence for original project convention;
- reconstructed source layout as evidence for original layout;
- Developer Choices as evidence of original convention;
- Validation Reference material as blind-recovery evidence.

Convention results may inform later inference rules only through explicit provenance-bearing dependencies.

---

# 11. Recovery Orchestrator

The **Recovery Orchestrator** coordinates the canonical Recovery Process from `07-recovery-process.md`.

Responsibilities include:

- start/resume Recovery Run;
- intake newly added artifacts;
- invoke eligible analyzers;
- integrate/normalize findings;
- schedule derived observation and inference evaluation;
- identify conflicts/ambiguities;
- request or apply committed Developer Intervention;
- plan/execute reconstruction;
- invoke build diagnostics;
- create Recovery Checkpoints;
- invoke validation only against an identified target;
- trigger earliest-affected-stage reevaluation when dependencies change.

The orchestrator coordinates semantics; it does not own artifact-specific analysis algorithms.

**Governance:** ARCH-006 / D110.

---

# 12. Developer intervention architecture

Developer intervention enters recovery only through explicit application use cases that create or change Intervention Records.

The implementation must avoid hidden mutable "developer hints" that bypass provenance.

Conceptually:

```text
Developer input
    ↓
Intervention command
    ↓
validate scope/conflicts
    ↓
append Intervention Record
    ↓
determine affected recovery dependencies
    ↓
mark/recompute downstream state
```

Replay of the same artifact set, configuration, and committed intervention journal should reproduce the same substantive recovery state wherever practical.

---

# 13. Reconstruction boundary

Reconstruction consumes current recovery knowledge, inference, assumptions, and Developer Decisions to produce **Reconstructed Artifacts**.

Reconstruction components include logical capabilities for:

- project/solution reconstruction;
- dependency representation;
- source reconstruction;
- configuration/resource placement;
- source/project candidate selection;
- build-driven repair.

The architecture enforces a one-way epistemic boundary:

```text
Recovery Knowledge
      ↓
Reconstruction
      ↓
Generated/Reconstructed Artifacts

Generated artifacts ──X──> independent historical evidence
```

A reconstructed artifact may be inspected for build/validation purposes, but its mere existence cannot independently corroborate the decisions that generated it.

**Governance:** ARCH-007 / D111.

---

# 14. Build execution boundary

Build/restore/run operations are external execution capabilities accessed through ports.

They return structured operational results such as:

- process/tool identity/version;
- environment;
- command/effective inputs;
- exit result;
- compiler/build diagnostics;
- produced artifacts;
- timing where measured.

Build diagnostics describe the candidate reconstruction state.

They may trigger build-repair workflows but do not become Original-System Evidence merely because they were produced by a compiler/MSBuild invocation against reconstructed output.

The concrete build technology belongs to `13-tooling-and-dependencies.md`.

---

# 15. Validation isolation

Validation is architecturally isolated from blind recovery.

A blind recovery execution context must not expose Validation Reference content through:

- recovery repositories;
- analyzer ports;
- inference services;
- convention analysis;
- developer-context services;
- shared caches that leak reference-derived data.

Conceptually:

```text
Blind Recovery Context
   ├── Recovery Input
   ├── Developer Input
   └── Recovery services

Validation Context
   ├── frozen Validation Target
   └── Validation Reference Snapshot
```

Only after the target is frozen may validation compare it with the reference.

If reference-derived information later influences recovery, a new non-blind lineage is created as required by D090.

**Governance:** ARCH-008 / D112.

---

# 16. Validation and metric execution architecture

The Validation Engine executes a Validation Plan using registered comparators/checks.

It produces Validation Findings, not recovery facts.

The Metrics Engine consumes:

- identified Validation Run/findings;
- metric definition/version;
- declared populations/matching rules;
- comparison/environment context.

It produces metric results preserving:

- numerator/denominator;
- exclusions;
- Not Evaluated/Not Comparable populations;
- metric definition identity/version;
- provenance to validation findings/reference context.

Metric computation does not write into the recovery support graph.

---

# 17. Checkpoints, history, and replay

Recovery state is iterative and history-preserving.

A **Recovery Checkpoint** identifies a stable state suitable for:

- validation;
- comparison;
- rollback/reference;
- reproducibility;
- audit.

A checkpoint should identify, directly or indirectly:

- Recovery Run;
- available artifact set/version identities;
- current evidence/conclusion state;
- active assumptions;
- active intervention records;
- selected reconstruction candidates;
- tool/configuration identity necessary for replay where applicable;
- Stage 01 supplied-source discovery completeness and scoped discovery diagnostics/blockers where the checkpoint depends on an intake snapshot produced from incomplete discovery.

A later checkpoint does not rewrite an earlier one.

Materially changed recovery inputs or interventions cause reevaluation from the earliest materially affected stage.

**Governance:** ARCH-009 / D113.

---

# 18. Diagnostics and partial progress

Errors are stage/capability outcomes, not necessarily whole-run failures.

The architecture must support structured diagnostics for:

- supplied-source/root/directory/subtree discovery or enumeration failure;
- unsupported artifact;
- unreadable/corrupt artifact;
- analyzer failure;
- unresolved dependency;
- conflicting evidence;
- inference blocked by missing precondition;
- reconstruction failure;
- build failure;
- validation execution error;
- metric non-applicability.

A Recovery Run may continue where safe and produce Partial/Blocked/Workable states already defined by the Recovery Process.

Diagnostics must identify the affected capability/item or, where no Artifact instance exists, the narrowest reliably established source/directory/subtree scope, and preserve enough context for developer action.

An intake discovery diagnostic must not require or fabricate an Artifact identity for material that was never discovered. A narrower Blocked discovery scope may coexist with a Partial Recovery Run when unaffected discovered artifacts remain trustworthy.

When positive file discovery in a known directory has completed successfully, later failure to discover possible child-directory/descendant scopes from that same directory does not invalidate those discovered files. Diagnostics must describe the failed descendant-discovery boundary rather than retrospectively converting the positively discovered files into undiscovered state. At the supplied root, this partial-discovery condition must remain distinguishable from total root discovery failure where no trustworthy supplied Artifact subset was established.

**Governance:** ARCH-010 / D114; PROC-010 / D170; PROC-011 / D171.

---

# 19. Extension architecture

LegacyRevive.NET is extensible through explicit capability contracts rather than ad-hoc hooks.

Likely extension categories include:

- Artifact Analyzer;
- Observation normalizer;
- Derived-observation/convention provider;
- Inference Rule;
- Project/source reconstructor;
- Build/tool adapter;
- Validation Comparator/Check provider;
- Recovery Metric calculator;
- report renderer.

An extension must use canonical domain contracts and cannot bypass:

- provenance requirements;
- support-cycle protection;
- developer-input distinction;
- reconstruction feedback protection;
- validation-reference isolation;
- metric denominator/accounting rules.

Discovery/registration technology is deferred to `13-tooling-and-dependencies.md`.

**Governance:** ARCH-011 / D115.

---

# 20. Persistence boundary

The canonical architecture requires persistence/replay semantics but does not prescribe a database or serialization technology.

Persistence must be capable of representing:

- stable IDs;
- append-preserving history;
- provenance/support edges;
- supersession/invalidations;
- checkpoint state;
- Stage 01 incomplete-discovery state that can distinguish retained positive file discovery plus a later descendant-discovery failure from total root discovery failure;
- intervention journal;
- reconstruction artifact metadata;
- validation/metric history;
- Stage 01 supplied-source discovery completeness and discovery-failure scope/diagnostics when applicable to a persisted intake snapshot or blocked intake run.

Persistence must distinguish a successfully enumerated empty supplied tree from a supplied root whose contents could not be enumerated.

Physical implementations may use files, embedded databases, relational stores, graph-oriented representations, or combinations if they preserve the canonical semantics.

Persistence technology is a tooling/implementation decision, not a domain-semantic decision.

**Governance:** ARCH-012 / D116.

---

# 21. Concurrency and determinism

Parallel execution may be used for independent analysis work, but externally observable recovery semantics should remain deterministic where practical.

If parallel analyzers complete in different orders, the integrated substantive result should not depend on incidental scheduling.

Where ordering matters, it must be explicit and reproducible.

Stable ordering is especially important for:

- reports;
- generated project/source output;
- inference evaluation ties;
- diagnostics;
- metric results.

This implements D014 without prescribing a concurrency library.

---

# 22. Security and sensitive material

LegacyRevive.NET may encounter configuration, connection strings, credentials, private symbols, proprietary binaries, or internal package metadata.

Architecture should therefore allow:

- sensitive-value redaction in reports/logs;
- separation between raw artifact access and rendered diagnostics;
- deliberate export of reconstructed outputs;
- avoidance of accidental secret copying into validation/benchmark artifacts.

Detailed security policy and concrete secret-handling tooling may be refined later without changing the evidence model.

---

# 23. Suggested .NET solution-level module shape

The following is a logical implementation shape, not a requirement that every box become exactly one assembly:

```text
LegacyRevive.Domain
  canonical entities/value types/rules
  evidence/provenance model
  recovery/reconstruction/validation semantics

LegacyRevive.Application
  use cases
  orchestration
  commands/queries
  checkpoint/reevaluation coordination

LegacyRevive.Analysis.*
  artifact-specific analyzers
  normalizers

LegacyRevive.Inference
  derived observations
  convention analysis
  inference rules

LegacyRevive.Reconstruction
  project/source reconstruction
  candidate generation/selection

LegacyRevive.Validation
  validation plans/checks/comparators

LegacyRevive.Metrics
  metric definitions/calculators

LegacyRevive.Infrastructure.*
  filesystem/persistence/process/tool adapters

LegacyRevive.Cli / future Hosts
  user interaction and composition root
```

Exact project count/names remain implementation choices. The required architectural rule is responsibility/dependency separation, not these literal names.

---

# 24. Architecture invariants

The following are canonical:

1. architecture is logically modular and domain-centered;
2. deployment/process topology is not fixed by this document;
3. canonical domain semantics are independent of concrete external tools;
4. original artifacts, recovery knowledge, interventions, reconstruction, and validation history remain logically distinct;
5. material domain records use stable identity independent of generated path/name;
6. artifact analyzers emit direct observations/diagnostics rather than hidden architectural inference;
7. inference uses explicit named rules and provenance-bearing support dependencies;
8. support cycles/self-corroboration are rejected or exposed;
9. the Recovery Orchestrator coordinates the canonical iterative process;
10. developer intervention enters through explicit replayable records;
11. reconstruction output cannot independently corroborate its own premises;
12. build diagnostics describe candidate state, not historical fact;
13. blind validation is isolated from Validation Reference leakage;
14. validation/metric output does not enter the recovery support graph;
15. checkpoints are stable history-preserving recovery-state identifiers;
16. failures can remain local and partial progress is first-class;
17. extensions cannot bypass canonical epistemic boundaries;
18. persistence technology is replaceable provided canonical history/provenance semantics survive;
19. deterministic substantive output is preferred even when work is parallelized;
20. concrete tool/library selection belongs to `13-tooling-and-dependencies.md`;
21. Application is the normal gateway to Domain/Core for outer adapters; adapters reference Application by default and reference Domain/Core directly only when an inward-owned capability contract genuinely requires Domain/Core-owned types or behaviour.
22. the MVP local workspace reserves `.legacyrevive` for LegacyRevive.NET-owned internal state;
23. the developer-facing reconstruction is materialized outside `.legacyrevive` for use by ordinary development tools;
24. incidental filesystem mutation of the materialized reconstruction does not itself alter canonical recovery state;
25. successfully preserved MVP Original Artifacts use workspace-owned immutable byte snapshots beneath `.legacyrevive`; hashes verify identity/integrity but do not replace preservation;
26. after successful preservation, original external artifact paths remain provenance rather than replay dependencies.

---

# 25. Governance mapping

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| ARCH-001 | D105 | Logical modular domain-centered architecture; deployment topology not fixed |
| ARCH-002 | D106 | Canonical state areas remain logically separated |
| ARCH-003 | D107 | Artifact analyzers emit observations/diagnostics, not hidden inference |
| ARCH-004 | D108 | Support/provenance graph is first-class and cycle-protected |
| ARCH-005 | D109 | Inference uses explicit named rules and governed inputs |
| ARCH-006 | D110 | Recovery Orchestrator coordinates canonical iterative process |
| ARCH-007 | D111 | Reconstruction output is isolated from historical evidence |
| ARCH-008 | D112 | Validation Reference is architecturally isolated from blind recovery |
| ARCH-009 | D113 | Recovery Checkpoints preserve stable replayable history |
| ARCH-010 | D114 | Diagnostics support local failure and partial progress |
| ARCH-011 | D115 | Extension contracts cannot bypass canonical semantics |
| ARCH-012 | D116 | Persistence technology is replaceable but must preserve canonical history/provenance |
| ARCH-013 | D161 | Application is the normal gateway to Domain/Core for outer adapters; direct adapter-to-Domain/Core references are exceptional and justified by inward-owned contracts |
| ARCH-014 | D163 | Local workspace reserves `.legacyrevive` for internal recovery state and materializes developer-facing reconstruction outside it |
| ARCH-015 | D167 | MVP Original Artifact bytes are preserved as workspace-owned immutable snapshots beneath `.legacyrevive`; external source paths remain provenance after successful intake |

All architecture qualifications in this revision are resolved.

---

# 26. Boundary with `13-tooling-and-dependencies.md`

This document defines **what architectural capabilities and boundaries must exist**.

`13-tooling-and-dependencies.md` should decide or evaluate concrete technology choices for, where required:

- assembly metadata/IL inspection;
- decompilation;
- PDB reading;
- MSBuild/project model;
- Roslyn;
- NuGet/package metadata;
- XML/config parsing;
- persistence;
- serialization;
- CLI/hosting;
- DI/composition;
- process execution;
- testing;
- report generation.

A tooling choice must conform to this architecture rather than silently redefine it.

---


# 27. Local Recovery Workspace physical model

The logical Recovery Workspace remains the durable recovery context defined by the canonical process and terminology. For the MVP local filesystem representation, that logical context is exposed through a developer-facing workspace root with a reserved LegacyRevive.NET internal subtree:

```text
<workspace>/
  .legacyrevive/        # LegacyRevive.NET-owned internal state
  <candidate-solution>  # current materialized reconstruction
  src/                  # current materialized reconstructed source
  ...                   # other developer-facing reconstructed/report outputs
```

`.legacyrevive` may contain SQLite persistence, checkpoint/run state, provenance/support data, intervention history, reconstruction metadata, materialization metadata, diagnostics, and other LegacyRevive.NET-owned internal files. Its exact internal layout is not canonical unless separately governed.

The developer-facing files outside `.legacyrevive` are the **Materialized Reconstruction**: a filesystem realization of current canonical reconstruction state intended to work with ordinary IDEs, editors, build tools, and developer workflows. Their existence or mutation on disk does not by itself alter canonical recovery state.

A material difference between the expected materialization and the current developer-facing files is **Working-Tree Divergence**. Divergence is operational workspace state, not Original-System Evidence, not a Developer Intervention, and not Developer-Adjusted Source until an explicit governed adoption occurs.

The local physical boundary does not collapse the logical state partitions in §5. For the MVP, successfully preserved supplied Original Artifact bytes are owned by the Recovery Workspace and stored immutably beneath `.legacyrevive`; the exact internal artifact-store subpath/encoding is non-canonical. The original external location remains recorded provenance after successful preservation and is not required to remain available for replay.

**Governance:** ARCH-014 / D163; ARCH-015 / D167.

---

# 28. Governance relationship

This document is constrained by all earlier canonical domain documents, especially:

- `03-evidence-model.md`;
- `04-developer-intervention.md`;
- `05-convention-inference.md`;
- `06-recovery-matrix.md`;
- `07-recovery-process.md`;
- `08-project-reconstruction.md`;
- `09-source-reconstruction.md`;
- `10-validation-strategy.md`;
- `11-recovery-metrics.md`;
- `90-decisions.md`;
- `91-design-qualification-register.md`.

Durable architecture decisions are owned by `90-decisions.md`.

Qualification history/status are owned by `91-design-qualification-register.md`.

Later architectural changes that materially challenge these rules require a new qualification and, where appropriate, a superseding decision rather than silent in-place historical rewriting.
