# LegacyRevive.NET — Decision Log

## Status

**Canonical project decision log**

This document records durable LegacyRevive.NET design decisions that have already been accepted through the discovery and documentation process.

It is not a backlog of open questions.

Open, deferred, blocked, or unresolved matters belong in:

`91-design-qualification-register.md`

This document answers:

> **What has LegacyRevive.NET decided, and why?**

---

# 1. Purpose of the decision log

The purpose of this file is to preserve durable project decisions so they are not lost across:

- future chats;
- document revisions;
- architecture work;
- implementation work;
- validation experiments;
- MVP changes.

A decision belongs here when:

- the project has deliberately chosen a direction;
- the decision affects later design or implementation;
- future work should treat it as established unless deliberately revised.

A decision should not be added merely because:

- an idea was discussed;
- a draft document used a term;
- an AI suggested a possibility;
- an issue remains unresolved;
- an implementation happened to choose one option without a design decision.

---

# 2. Decision lifecycle

Each decision may have one of these statuses:

## Accepted

Current project direction.

## Superseded

Replaced by a later decision.

A previously accepted decision must never be rewritten in place merely to make the history look current.

When an accepted decision is reversed or materially replaced:

1. retain the original decision and its ID;
2. mark the original decision `Superseded`;
3. create a new decision using a new `DNNN` ID;
4. set the original decision's `Superseded by` field to the new ID;
5. set the new decision's `Supersedes` field to the old ID;
6. update affected qualifications and canonical documents.

## Deprecated

Still historically relevant but no longer recommended for new work.

## Rejected

A considered direction that was explicitly rejected and is preserved because the rationale is important.

For the initial decision set below, all entries are **Accepted** unless stated otherwise.

---

# 3. Decision format

Each decision uses this structure:

```text
## DNNN — Title

Status: Accepted | Superseded | Deprecated | Rejected
Date: <date accepted or recorded>
Origin: <conversation/document/qualification>
Related qualifications: <IDs or None>

### Decision

<What was decided?>

### Rationale

<Why?>

### Consequences

<What future work must respect?>

### Supersedes

<Decision IDs or None>

### Superseded by

<Decision ID or None>
```

---

# 4. Decision index

| ID | Title | Status |
|---|---|---|
| D001 | Workable Recovery over Exact Repository Reproduction | Accepted |
| D002 | Evidence and Inference Must Remain Distinct | Accepted |
| D003 | Provenance Is a Core Requirement | Accepted |
| D004 | Prevent Recursive Assumption Amplification | Accepted |
| D005 | Developer Intervention Is First-Class | Accepted |
| D006 | Convention Inference Is Derived Observation, Not Fact | Accepted |
| D007 | Validation Reference Must Remain Separate from Recovery Input | Accepted |
| D008 | Validation Must Remain Multidimensional | Accepted |
| D009 | Buildability Does Not Prove Behavioural Equivalence | Accepted |
| D010 | Recovery Should Progress from Stronger Evidence toward Weaker Inference | Accepted |
| D011 | Original Recovery Artifacts Remain Immutable | Accepted |
| D012 | Prefer Explainable Confidence over Arbitrary Numeric Precision | Accepted |
| D013 | Reuse Mature .NET Tooling Where Appropriate | Accepted |
| D014 | Prefer Deterministic Recovery Where Practical | Accepted |
| D015 | Chats Explore; Documents Consolidate | Accepted |
| D016 | Canonical Documents Have Defined Ownership | Accepted |
| D017 | Material Qualifications Must Be Tracked Centrally | Accepted |
| D018 | Resolved Qualifications Remain in the Register | Accepted |
| D019 | LegacyRevive.NET, LegacyLens.NET, and LegacySafety.NET Have Distinct Responsibilities | Accepted |
| D020 | Qualification Lifecycle Is Normative Governance Process | Accepted |
| D021 | Governance Changes Require Cross-File Synchronization | Accepted |
| D022 | Decisions Are Replaced by Supersession, Not In-Place Rewrite | Accepted |
| D023 | Recovery Input Separates Evidence from Developer Input | Accepted |
| D024 | Evidence Conclusions Evolve by Supersession, Not Reclassification In Place | Accepted |
| D025 | Conflicting Evidence Is Explicit and History-Preserving | Accepted |
| D026 | Provenance Includes Derivation Lineage | Accepted |
| D027 | Confidence Is Semantic and Applies Only to Inferential Conclusions | Accepted |
| D028 | Assumptions Have an Explicit Lifecycle | Accepted |
| D029 | Developer Corrections Supersede and Trigger Downstream Reevaluation | Accepted |
| D030 | Support Dependencies Must Prevent Circular Corroboration | Accepted |
| D031 | Developer Intervention Uses Assertion, Decision, and Correction Kinds | Accepted |
| D032 | Committed Interventions Are Explicit Scoped Records | Accepted |
| D033 | Intervention History Uses Active, Superseded, and Withdrawn States | Accepted |
| D034 | Committed Interventions Persist and Are Replayable | Accepted |
| D035 | Developer Input Does Not Override Evidence or Create Facts by Itself | Accepted |
| D036 | Conflicting Active Interventions Require Explicit Resolution | Accepted |
| D037 | Intervention Withdrawal and Supersession Trigger Reevaluation | Accepted |
| D038 | Convention Observations Describe Scoped Recurrence, Not Intent | Accepted |
| D039 | Convention Support Must Exclude Reconstruction Feedback | Accepted |
| D040 | Convention Scope Cannot Be Silently Generalized | Accepted |
| D041 | Convention Recurrence Uses Explainable Domain-Appropriate Support | Accepted |
| D042 | Convention Exceptions Must Remain Visible | Accepted |
| D043 | Convention Observations Use Support Profiles, Not Inferential Confidence | Accepted |
| D044 | Competing Scoped Conventions Require Explicit Conflict Handling | Accepted |
| D045 | Convention History Is Append-Preserving and Reevaluable | Accepted |
| D046 | Recovery Matrix Uses Four Contribution Classes | Accepted |
| D047 | Artifact-Type Capability Does Not Imply Artifact-Instance Content | Accepted |
| D048 | Artifact Format Must Be Classified Beyond File Extension | Accepted |
| D049 | Missing Recovery Artifact Does Not Prove Historical Nonexistence | Accepted |
| D050 | Companion-Artifact Corroboration Requires Defensible Relationship and Independence | Accepted |
| D051 | Matrix Capability and Artifact-Instance Findings Are Separate | Accepted |
| D052 | Recovery Ceilings Are Scoped to the Available Artifact Set | Accepted |
| D053 | Cross-Artifact Recovery Preserves Epistemic Categories | Accepted |
| D054 | Recovery Uses a Stage-Oriented but Iterative Canonical Process | Accepted |
| D055 | Stage Progression Does Not Promote Epistemic Category | Accepted |
| D056 | Recovery Reevaluates from the Earliest Materially Affected Stage | Accepted |
| D057 | Newly Added Artifacts Re-enter through Intake and Classification | Accepted |
| D058 | Developer Intervention Is Available at Explicit Points Throughout Recovery | Accepted |
| D059 | Build Diagnostics Describe Candidate Recovery State, Not Historical Fact | Accepted |
| D060 | Recovery Distinguishes Workspace, Run, and Checkpoint | Accepted |
| D061 | Recovery Supports Partial, Blocked, Workable, and Validated States | Accepted |
| D062 | Validation Feedback Cannot Leak Validation Reference into Recovery | Accepted |
| D063 | Candidate Project Is Distinct from Original Project | Accepted |
| D064 | Implementation Assemblies Anchor Initial Project Candidates without Proving Original Boundaries | Accepted |
| D065 | Project Boundaries Require Explainable Support and Explicit Conflict Handling | Accepted |
| D066 | Candidate Project Merge and Split Are Explicit Reconstruction Decisions | Accepted |
| D067 | Candidate Project Identity Is Separate from Reconstructed Name | Accepted |
| D068 | Project Kind and Target Framework Preserve Historical Uncertainty | Accepted |
| D069 | Compiled Dependency Does Not Establish Original Project Declaration Mechanism | Accepted |
| D070 | Unresolved Dependencies and Project Cycles Remain Explicit | Accepted |
| D071 | Generated Project and Solution Files Remain Reconstructed Artifacts | Accepted |
| D072 | Multiple Project Reconstructions May Coexist; Selection Is Operational | Accepted |
| D073 | Source Reconstruction Uses Explicit Provenance Classes | Accepted |
| D074 | Source Provenance May Attach Below Whole-File Level | Accepted |
| D075 | Recovered Source File Identity Is Separate from Generated Path and Name | Accepted |
| D076 | Directly Preserved Source Content Takes Precedence over Weaker Regeneration | Accepted |
| D077 | Source Skeleton Is a Valid Partial Recovery and Unknown Bodies Are Not Fabricated | Accepted |
| D078 | Higher-Level Reconstruction of Lowered Code Is Generated Interpretation | Accepted |
| D079 | Source Recovery Ceilings Remain Explicit and Artifact-Set Scoped | Accepted |
| D080 | Source Organization and Candidate Project Ownership Are Reconstruction Choices | Accepted |
| D081 | Multiple Source Candidates May Coexist; Selection Is Operational | Accepted |
| D082 | Build-Driven Source Repair Remains Reconstructed or Developer-Adjusted Source | Accepted |
| D083 | Generated Source Cannot Independently Corroborate Its Own Reconstruction | Accepted |
| D084 | Validation Targets an Identified Recovery Checkpoint | Accepted |
| D085 | Validation Is Governed by an Explicit Validation Plan | Accepted |
| D086 | Validation Dimensions Remain Independently Visible | Accepted |
| D087 | Not Evaluated and Not Comparable Are Distinct from Validation Failure | Accepted |
| D088 | Validation Requires Compatible Target and Reference Context | Accepted |
| D089 | Blind Evaluation Freezes Recovery before Validation Reference Access | Accepted |
| D090 | Reference-Derived Repair Creates a New Recovery Lineage | Accepted |
| D091 | Build, API, Source, and Behavioral Validation Establish Different Claims | Accepted |
| D092 | Validation Runs and Findings Preserve Provenance and History | Accepted |
| D093 | Validated Baseline Does Not Mean Perfect Recovery | Accepted |
| D094 | Recovery Metrics Require Explicit Populations and Denominators | Accepted |
| D095 | Correctness and Coverage Are Separate Recovery Measurements | Accepted |
| D096 | Set Reconstruction Uses Explicit Matching with Precision and Recall | Accepted |
| D097 | Historical Fidelity and Recoverability-Aware Effectiveness Remain Separate | Accepted |
| D098 | Dependency Existence and Historical Representation Are Measured Separately | Accepted |
| D099 | Source Fidelity Prioritizes Semantic Claims over Textual Similarity | Accepted |
| D100 | Operational Recovery Is Graduated and Environment-Scoped | Accepted |
| D101 | Developer-Intervention Burden Is a First-Class Metric Family | Accepted |
| D102 | Epistemic Accuracy Is a First-Class Metric Family | Accepted |
| D103 | No Authoritative Global Recovery Score Replaces Dimension Metrics | Accepted |
| D104 | Cross-Run Metric Comparison Requires Compatible Scenario and Definition Context | Accepted |
| D105 | Architecture Is Logically Modular and Domain-Centered | Accepted |
| D106 | Canonical Recovery State Areas Remain Logically Separated | Accepted |
| D107 | Artifact Analyzers Emit Direct Observations and Diagnostics, Not Hidden Inference | Accepted |
| D108 | Support and Provenance Dependencies Are First-Class Architectural Data | Accepted |
| D109 | Inference Uses Explicit Named Rules with Governed Inputs | Accepted |
| D110 | Recovery Orchestrator Coordinates the Canonical Iterative Process | Accepted |
| D111 | Reconstruction Output Is Architecturally Isolated from Historical Evidence | Accepted |
| D112 | Validation Reference Is Architecturally Isolated from Blind Recovery | Accepted |
| D113 | Recovery Checkpoints Preserve Stable Replayable History | Accepted |
| D114 | Diagnostics Support Local Failure and Partial Progress | Accepted |
| D115 | Extension Contracts Cannot Bypass Canonical Recovery Semantics | Accepted |
| D116 | Persistence Technology Is Replaceable but Must Preserve Canonical History and Provenance | Accepted |
| D117 | LegacyRevive.NET Uses .NET 10 LTS as Its Application Runtime Baseline | Accepted |
| D118 | Dependencies Are Centrally Pinned and Deliberately Upgraded | Accepted |
| D119 | System.Reflection.Metadata Is the Primary Managed Metadata Extraction API | Accepted |
| D120 | ICSharpCode.Decompiler Is the Selected C# Decompilation Engine | Accepted |
| D121 | Portable PDB Is Primary and Native Windows PDB Support Is Optional | Accepted |
| D122 | Roslyn Is the Selected C# Syntax and Semantic Engine | Accepted |
| D123 | MSBuild Object Model and Locator Are Selected; Canonical Builds Execute Out of Process | Accepted |
| D124 | NuGet Package Inspection Is Local-First and Remote Feed Access Is Explicit Enrichment | Accepted |
| D125 | SQLite via Microsoft.Data.Sqlite Is the Default Local Persistence | Accepted |
| D126 | Platform JSON and XML APIs Are the Structured Interchange Baseline | Accepted |
| D127 | Generic Host, Microsoft DI/Logging, and System.CommandLine Are the Baseline Host Stack | Accepted |
| D128 | External Tool Execution Uses a Controlled System.Diagnostics.Process Adapter | Accepted |
| D129 | LegacyRevive-Generated IDs Use UUIDv7 and Artifacts Retain SHA-256 Content Identity | Accepted |
| D130 | xUnit v3 Is the Baseline Automated Test Framework | Accepted |
| D131 | External Dependencies Require Security, License, Source, and Capability Review | Accepted |
| D132 | MVP Proves the Complete Minimum Managed-Artifact Recovery Loop | Accepted |
| D133 | MVP Execution Is Windows x64 First | Accepted |
| D134 | MVP Uses Local Directory Recovery Input with Defined Managed Artifact Analyzers | Accepted |
| D135 | MVP Managed Assembly Analysis Recovers the Metadata Needed for Reconstruction | Accepted |
| D136 | MVP Includes Ownership Classification and Evidence-Backed Candidate Projects | Accepted |
| D137 | MVP Reconstructs Dependency Relationships Before Choosing Representation | Accepted |
| D138 | MVP Generates Decompiler-Backed C# with Explicit Source Provenance | Accepted |
| D139 | MVP Exports a Deterministic Candidate Solution and Project Tree | Accepted |
| D140 | MVP Includes Explicit Developer Intervention and Basic Recovered Context | Accepted |
| D141 | MVP Recovery Workspaces Are Persistent and Resumable | Accepted |
| D142 | MVP Attempts Controlled Builds but Does Not Fabricate History to Make Them Green | Accepted |
| D143 | MVP Preserves Partial, Blocked, Workable, and Validated Outcome Semantics | Accepted |
| D144 | MVP Is CLI-First with Recover, Status, Explain, Decide, Build, and Export Capabilities | Accepted |
| D145 | Blind Controlled Validation Is Mandatory for MVP Release | Accepted |
| D146 | MVP Release Uses Multidimensional Functional, Epistemic, Replay, History, Quality, and Usability Gates | Accepted |
| D147 | MVP Explicitly Defers Non-Core Recovery and Product Surfaces | Accepted |
| D148 | Roadmap Sequencing Does Not Constitute Accepted Detailed Design | Accepted |
| D149 | Post-MVP Horizons Are Evidence- and Dependency-Gated Rather Than Date Promises | Accepted |
| D150 | Roadmap Promotion Requires Demonstrated Recovery, Developer, or Safety Value | Accepted |
| D151 | The First Post-MVP Horizon Strengthens the Core Recovery Loop | Accepted |
| D152 | The Second Horizon Broadens Platform and Artifact Evidence Coverage | Accepted |
| D153 | External Enrichment May Expand Post-MVP but Remains Provenance-Distinct | Accepted |
| D154 | Advanced Inference and Learned Ranking Remain Subordinate to the Evidence Model | Accepted |
| D155 | Richer Product Hosts Reuse Canonical Application and Domain Semantics | Accepted |
| D156 | Validation and Held-Out Corpus Maturity Expand Alongside Product Capability | Accepted |
| D157 | Runtime Plugin Ecosystem Is Deferred Until Need and Trust Boundaries Are Demonstrated | Accepted |
| D158 | Collaborative and Enterprise Scale Requires Explicit New Security and Concurrency Design | Accepted |
| D159 | Native and Runtime Recovery Is a Distinct Research and Capability Domain | Accepted |
| D160 | External Enrichment Admitted to Recovery Is Independent Contextual Evidence | Accepted |
| D161 | Application Is the Normal Gateway to Domain/Core for Outer Adapters | Accepted |
| D162 | LegacyRevive.NET Repository Uses SLNX as Its Solution Format | Superseded |
| D163 | Local Recovery Workspace Separates Internal State from the Materialized Reconstruction | Accepted |
| D164 | Export Deterministically Materializes Canonical Recovery State | Accepted |
| D165 | Manual Materialized-Source Changes Require Explicit Adoption | Accepted |
| D166 | Unresolved Working-Tree Divergence Blocks Export | Accepted |
| D167 | MVP Original Artifacts Use Workspace-Owned Immutable Byte Snapshots | Accepted |
| D168 | Material Implementation Uses Canonical Development Process and Development Slice Specifications | Accepted |
| D169 | LegacyRevive.NET Repository Solution Is `LegacyRevive.Net.slnx` | Accepted |
| D170 | Intake Discovery Failure Preserves Known State Without Fabricating Artifacts | Accepted |
| D171 | Positive Stage 01 File Discovery Survives Later Descendant-Discovery Failure | Accepted |
| D172 | Stage 02 Artifact Classification Uses Direct Format-Specific Recognition Contracts | Accepted |
| D173 | Development Slices Require Semantic Readiness Before Implementation | Accepted |
| D174 | Rejected DEV-SPECs Preserve Unsound Implemented Completion Judgments | Accepted |
| D175 | Malformed Family Claims Require Direct Structural Candidate Discriminators | Accepted |
| D176 | Recognition/Classifiers Require Boundary-Falsification Verification | Accepted |

---

# 5. Accepted decisions

## D001 — Workable Recovery over Exact Repository Reproduction

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

LegacyRevive.NET aims to recover a **workable .NET development baseline**, not to guarantee exact reproduction of the original repository.

A successful recovery may differ from the original solution, project layout, source layout, formatting, comments, or other historical details while still being a valid recovery outcome.

### Rationale

Some information cannot always be recovered exactly from surviving/deployed artifacts.

The useful engineering objective is to restore enough accurate structure, source, configuration, dependency information, and supporting evidence for a developer to understand, build where possible, validate, repair, and continue maintaining the system.

### Consequences

Future capability design must distinguish:

- useful recovery;
- exact historical reconstruction.

The tool must not overstate fidelity merely to appear complete.

### Supersedes

None

### Superseded by

None

---

## D002 — Evidence and Inference Must Remain Distinct

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: EVID-001, EVID-002, EVID-003, DOC-009, DOC-015, DOC-017

### Decision

LegacyRevive.NET must preserve the distinction between:

- directly supported evidence;
- derived observations;
- inference;
- assumption;
- reconstruction.

Inference must not silently become fact.

### Rationale

The recovery process inevitably includes uncertainty and inference.

If those categories are collapsed, later stages may overstate what is actually known.

### Consequences

Later evidence-model and architecture work must preserve epistemic category and provenance.

### Supersedes

None

### Superseded by

None

---

## D003 — Provenance Is a Core Requirement

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: EVID-005

### Decision

Important recovered, derived, inferred, reconstructed, and developer-supplied information must retain provenance wherever practical.

### Rationale

A developer must be able to understand where recovery conclusions came from and what they depend upon.

Provenance is also required to prevent reconstructed output from being mistaken for independent original evidence.

### Consequences

Future evidence models, reports, and reconstruction models must support traceability back to origin.

### Supersedes

None

### Superseded by

None

---

## D004 — Prevent Recursive Assumption Amplification

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: EVID-010

### Decision

Generated or reconstructed output must not later be treated as independent evidence that confirms the inference used to generate it.

### Rationale

Without this rule, an initial weak inference could become artificially stronger merely because the recovery process reproduced it in generated output.

### Consequences

Future evidence and reconstruction models must preserve lineage and make circular/self-supporting reasoning detectable.

### Supersedes

None

### Superseded by

None

---

## D005 — Developer Intervention Is First-Class

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: EVID-001, EVID-009

### Decision

Developer intervention is an expected and first-class part of the LegacyRevive.NET recovery process.

The tool is not required to recover every ambiguity autonomously.

### Rationale

Recovery from incomplete artifacts can require human knowledge, judgement, correction, and pragmatic choices.

Treating developer involvement as failure would make the recovery model unrealistic.

### Consequences

Future design must support developer-provided information and decisions while preserving their provenance and distinguishing them from original-system evidence.

### Supersedes

None

### Superseded by

None

---

## D006 — Convention Inference Is Derived Observation, Not Fact

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: EVID-010

### Decision

Conventions inferred from surviving material may be surfaced as useful derived observations, but must not be presented as direct facts or guaranteed original developer intent.

### Rationale

Naming, namespace, project, dependency, configuration, and coding patterns may help a developer make consistent interventions.

However, patterns do not prove historical intent.

### Consequences

Convention inference must retain provenance and must not become self-reinforcing reconstruction evidence.

### Supersedes

None

### Superseded by

None

---

## D007 — Validation Reference Must Remain Separate from Recovery Input

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: None

### Decision

When LegacyRevive.NET is evaluated against a known original codebase, original source used for comparison must remain a **validation reference** and must not be exposed to the recovery process.

### Rationale

Allowing original source to influence recovery would invalidate the evaluation.

### Consequences

Controlled evaluation must maintain a strict separation between:

- recovery input;
- validation reference.

### Supersedes

None

### Superseded by

None

---

## D008 — Validation Must Remain Multidimensional

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

Recovery quality should be evaluated across multiple dimensions rather than hidden behind one opaque overall recovery score.

### Rationale

Different aspects of a recovered system have different realistic recovery ceilings and can be accurate to different degrees.

### Consequences

Later validation design should preserve individual dimensions such as structure, dependencies, APIs, configuration, buildability, and behaviour where measurable.

A single summary score, if ever introduced, must not replace the underlying dimensions.

### Supersedes

None

### Superseded by

None

---

## D009 — Buildability Does Not Prove Behavioural Equivalence

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: None

### Decision

A recovered solution compiling successfully is an important recovery milestone, but it does not prove behavioural equivalence with the original system.

### Rationale

A buildable system may still differ in runtime wiring, configuration, reflection behaviour, persistence, serialization, resources, external dependencies, or other behaviour.

### Consequences

Compilation must be treated as one validation dimension rather than final proof of successful equivalence.

### Supersedes

None

### Superseded by

None

---

## D010 — Recovery Should Progress from Stronger Evidence toward Weaker Inference

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

LegacyRevive.NET should generally establish stronger directly supported information before allowing weaker inference to shape later reconstruction.

### Rationale

This reduces unnecessary speculation and helps preserve explainability.

### Consequences

Future recovery-process design should respect evidence strength, without assuming that this decision alone defines the exact execution pipeline.

### Supersedes

None

### Superseded by

None

---

## D011 — Original Recovery Artifacts Remain Immutable

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

LegacyRevive.NET must not overwrite or mutate original surviving recovery artifacts.

Recovered/reconstructed output should be written separately.

### Rationale

Original artifacts are the primary evidence base and must remain available for repeatability, validation, comparison, and forensic inspection.

### Consequences

All recovery output and transformations must preserve the original input set.

### Supersedes

None

### Superseded by

None

---

## D012 — Prefer Explainable Confidence over Arbitrary Numeric Precision

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`; `02-terminology.md`  
Related qualifications: EVID-006, EVID-007

### Decision

LegacyRevive.NET should prefer explainable evidence-based confidence semantics over arbitrary unexplained numeric percentages.

### Rationale

A numerical percentage can imply statistical precision that the recovery evidence does not justify.

### Consequences

The exact confidence vocabulary remains open, but any confidence representation must be explainable in terms of supporting evidence and provenance.

### Supersedes

None

### Superseded by

None

---

## D013 — Reuse Mature .NET Tooling Where Appropriate

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

LegacyRevive.NET should investigate and reuse mature .NET ecosystem and open-source capabilities rather than unnecessarily reimplementing well-solved low-level functionality.

### Rationale

The recovery problem already has mature supporting technologies in areas such as metadata inspection, decompilation, Roslyn analysis, package inspection, MSBuild, symbol reading, and resource inspection.

### Consequences

Specific dependency choices remain future architecture/tooling decisions and must still be evaluated for licensing, maintenance, correctness, and architectural fit.

### Supersedes

None

### Superseded by

None

---

## D014 — Prefer Deterministic Recovery Where Practical

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery discussion; `01-purpose-and-principles.md`  
Related qualifications: None

### Decision

Given the same recovery inputs, tool version, configuration, and recorded developer interventions, LegacyRevive.NET should produce the same substantive recovery result wherever practical.

### Rationale

Determinism supports debugging, comparison, validation, reproducibility, and regression testing.

### Consequences

Future architecture and implementation should avoid unnecessary nondeterminism.

### Supersedes

None

### Superseded by

None

---

## D015 — Chats Explore; Documents Consolidate

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET Project setup discussion  
Related qualifications: None

### Decision

The LegacyRevive.NET Project will use the rule:

> **Chats explore; documents consolidate.**

### Rationale

Conversation history is useful for exploration but is not a reliable long-term source of truth.

Canonical Project documents should preserve mature conclusions.

### Consequences

Important design decisions should be migrated into canonical documents rather than remaining only in chat history.

### Supersedes

None

### Superseded by

None

---

## D016 — Canonical Documents Have Defined Ownership

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET Project documentation discussion  
Related qualifications: DOC-002, DOC-003, DOC-004, DOC-005, DOC-006, DOC-007, DOC-010, DOC-011

### Decision

Each major concept should have one primary canonical document that owns its authoritative definition.

Other documents should reference that definition rather than silently redefining it.

### Rationale

This reduces duplication, drift, and contradictory definitions.

### Consequences

Examples include:

- purpose/principles → `01-purpose-and-principles.md`;
- terminology → `02-terminology.md`;
- evidence model → `03-evidence-model.md`;
- qualification status → `91-design-qualification-register.md`;
- durable decisions → `90-decisions.md`.

### Supersedes

None

### Superseded by

None

---

## D017 — Material Qualifications Must Be Tracked Centrally

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET audit/governance discussion  
Related qualifications: None

### Decision

All material unresolved qualifications discovered during design or document audits must be recorded in the single central register:

`91-design-qualification-register.md`

### Rationale

Open questions and caveats should not disappear merely because work moves to another document or chat.

### Consequences

Individual documents may reference qualification IDs, but the register is the single source of truth for current qualification status.

### Supersedes

None

### Superseded by

None

---

## D018 — Resolved Qualifications Remain in the Register

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET audit/governance discussion  
Related qualifications: DOC-001, DOC-002, DOC-003, DOC-004, DOC-005, DOC-006

### Decision

When a design qualification is resolved, its register entry should remain permanently with its resolution recorded.

### Rationale

Deleting resolved issues would remove the provenance of how canonical documents were corrected and matured.

### Consequences

`91-design-qualification-register.md` serves as both:

- the current unresolved-qualification register; and
- the historical record of material resolved qualifications.

### Supersedes

None

### Superseded by

None

---

## D019 — LegacyRevive.NET, LegacyLens.NET, and LegacySafety.NET Have Distinct Responsibilities

Status: Accepted  
Date: 2026-09-25  
Origin: LegacyRevive.NET discovery and Project setup discussion  
Related qualifications: None

### Decision

The three tool concepts have distinct primary responsibilities:

- **LegacyRevive.NET** — recover a workable development baseline from surviving/deployed artifacts when the original development representation is missing or unusable;
- **LegacyLens.NET** — help understand and investigate an existing legacy .NET source codebase;
- **LegacySafety.NET** — future concept focused on quickly establishing regression/characterization-test safety around legacy code.

### Rationale

The tools are complementary but solve different developer problems.

### Consequences

Future design should avoid collapsing their responsibilities without an explicit later decision.

### Supersedes

None

### Superseded by

None

---


## D020 — Qualification Lifecycle Is Normative Governance Process

Status: Accepted  
Date: 2026-09-25  
Origin: Joint governance audit of `90-decisions.md` and `91-design-qualification-register.md`  
Related qualifications: GOV-001, GOV-002, DOC-008, DOC-014, DOC-016

### Decision

The end-to-end qualification lifecycle defined in `91-design-qualification-register.md` is the normative governance process for LegacyRevive.NET design work.

### Rationale

The earlier governance rules were coherent but distributed across multiple sections and files. A single normative lifecycle is required so that new documents, audits, qualifications, resolutions, decisions, and canonical updates follow the same repeatable process.

### Consequences

Future document generation and auditing must follow the qualification lifecycle in `91`.

`90-decisions.md` participates in that lifecycle but does not own qualification status.

### Supersedes

None

### Superseded by

None

---

## D021 — Governance Changes Require Cross-File Synchronization

Status: Accepted  
Date: 2026-09-25  
Origin: Joint governance audit of `90-decisions.md` and `91-design-qualification-register.md`  
Related qualifications: GOV-004

### Decision

A governance change is complete only when the relevant decision log, qualification register, canonical document, and audit result are mutually consistent.

### Rationale

Because governance state is distributed across several files, updating only one file can leave the Project in a misleading intermediate state.

### Consequences

Every governance update must perform a final synchronization check across all affected artifacts before a qualification is marked `Resolved`.

### Supersedes

None

### Superseded by

None

---

## D022 — Decisions Are Replaced by Supersession, Not In-Place Rewrite

Status: Accepted  
Date: 2026-09-25  
Origin: Joint governance audit of `90-decisions.md` and `91-design-qualification-register.md`  
Related qualifications: GOV-003

### Decision

When an accepted decision is materially replaced, the original decision must remain in the log and be marked `Superseded`; the replacement must receive a new decision ID.

### Rationale

Rewriting old accepted decisions would destroy governance history and make later reasoning difficult to audit.

### Consequences

Decision IDs are permanent.

Reversals and replacements create new decisions and explicit cross-links rather than rewriting historical entries.

### Supersedes

None

### Superseded by

None

---


## D023 — Recovery Input Separates Evidence from Developer Input

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-001  
Related qualifications: EVID-001

### Decision

`Recovery Input` is the top-level recovery-process input concept.

Formal `Evidence` and `Developer Input` are separate categories beneath it.

Developer Assertions may influence inference but are not formal Evidence and must remain distinguishable from Original-System Evidence.

### Rationale

This preserves first-class developer intervention without allowing human-supplied recovery knowledge to be reported later as something discovered from surviving target artifacts.

### Consequences

Evidence-model, reporting, intervention, and architecture work must preserve source kind and must not collapse developer input into original-system evidence.

### Supersedes

None

### Superseded by

None

---

## D024 — Evidence Conclusions Evolve by Supersession, Not Reclassification In Place

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-002; EVID-003  
Related qualifications: EVID-002, EVID-003

### Decision

An Observation is direct extraction from Evidence.

A Recovered Fact is a normalized proposition directly entailed by admissible observations without inferential leap, assumptions, or developer assertions.

When later evidence strengthens an earlier Inference into a directly supported proposition, the earlier record is retained and a new Recovered Fact supersedes it rather than mutating the original classification in place.

### Rationale

This preserves historical reasoning and prevents later certainty from rewriting what was actually known earlier.

### Consequences

Evidence-model storage and reporting must support stable historical records, current-state selection, and supersession links.

### Supersedes

None

### Superseded by

None

---

## D025 — Conflicting Evidence Is Explicit and History-Preserving

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-004  
Related qualifications: EVID-004

### Decision

Materially incompatible evidence/conclusions must be represented explicitly as a conflict with links to the competing propositions and their support paths.

Resolving a conflict must not delete the original evidence or historical conclusions.

### Rationale

Silently selecting a source would hide uncertainty and make later review impossible.

### Consequences

Downstream recovery must be able to recognize contested conclusions and must preserve any resolution as a separate historical event or decision.

### Supersedes

None

### Superseded by

None

---

## D026 — Provenance Includes Derivation Lineage

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-005  
Related qualifications: EVID-005

### Decision

`Provenance` is the umbrella traceability concept.

Derivation `Lineage` is represented as part of Provenance rather than as a separate top-level concept.

### Rationale

LegacyRevive.NET needs to record both origin and derivation chains without creating two competing traceability models.

### Consequences

Material derived conclusions must remain traceable through provenance to their sources and intervening derivation steps.

### Supersedes

None

### Superseded by

None

---

## D027 — Confidence Is Semantic and Applies Only to Inferential Conclusions

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-006; EVID-007  
Related qualifications: EVID-006, EVID-007

### Decision

Inferential support strength uses the semantic labels:

- Strongly Supported;
- Plausible;
- Tentative.

`Unresolved` is a recovery state, not a confidence label.

Support strength attaches to Inferences and inferential reconstruction decisions, not to raw Evidence Sources, Observations, Recovered Facts, Assumptions, whole reconstructed artifacts, or Validation Results.

### Rationale

This avoids arbitrary pseudo-precision and prevents confidence labels from obscuring the epistemic category of the underlying item.

### Consequences

Every support-strength assessment must be explainable from provenance and uncertainty.

Reports must not replace conclusion-level support with one opaque artifact-level score.

### Supersedes

None

### Superseded by

None

---

## D028 — Assumptions Have an Explicit Lifecycle

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-008  
Related qualifications: EVID-008

### Decision

Recovery Assumptions have the canonical states:

- Active;
- Superseded;
- Invalidated;
- Retired.

Assumptions never silently become Recovered Facts.

### Rationale

Assumptions may be necessary to progress recovery, but hidden or immortal assumptions can contaminate later conclusions.

### Consequences

Assumption dependencies must be traceable.

Invalidating an assumption requires materially dependent downstream conclusions to be reevaluated before remaining current.

### Supersedes

None

### Superseded by

None

---

## D029 — Developer Corrections Supersede and Trigger Downstream Reevaluation

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-009  
Related qualifications: EVID-009

### Decision

A Developer Correction does not rewrite an earlier recovery conclusion in place.

It creates or supports a new conclusion/reconstruction decision that supersedes the earlier item.

Materially dependent downstream items become `Needs Reevaluation` until recomputed or deliberately reaffirmed, and become `Invalidated` where valid support no longer remains.

### Rationale

Developer intervention is first-class, but correction history and downstream effects must remain auditable.

### Consequences

`04-developer-intervention.md` must implement workflow semantics compatible with this evidence-history model while remaining free to define interaction and review mechanics.

### Supersedes

None

### Superseded by

None

---

## D030 — Support Dependencies Must Prevent Circular Corroboration

Status: Accepted  
Date: 2026-09-25  
Origin: `03-evidence-model.md`; EVID-010  
Related qualifications: EVID-010

### Decision

Material recovery support must be represented as a directed support-dependency graph.

A conclusion cannot gain independent support from itself, its descendants, reconstructed output generated from it, or a re-expression of the same underlying support path.

Support edges that create dependency cycles must be rejected or explicitly flagged.

### Rationale

Without dependency-aware support rules, weak inference can recursively amplify itself and appear independently corroborated.

### Consequences

Generated output must retain provenance.

Derived observations and downstream inferences inherit their upstream dependency paths.

Repeated transformations of the same support path must not increase support strength.

### Supersedes

None

### Superseded by

None

---


## D031 — Developer Intervention Uses Assertion, Decision, and Correction Kinds

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-001  
Related qualifications: INTV-001

### Decision

The canonical Developer Intervention kinds are:

- Developer Assertion;
- Developer Decision;
- Developer Correction.

### Rationale

These distinguish supplying human knowledge, choosing a recovery path, and correcting existing recovery state without collapsing all human participation into one ambiguous mechanism.

### Consequences

Later workflow, reporting, and architecture work must preserve intervention kind.

### Supersedes

None

### Superseded by

None

---

## D032 — Committed Interventions Are Explicit Scoped Records

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-002  
Related qualifications: INTV-002

### Decision

Every committed Developer Intervention must be represented as a provenance-bearing Intervention Record with explicit scope and affected recovery target/question where applicable.

### Rationale

Hidden or globally applied human context would weaken provenance and could influence unrelated recovery conclusions.

### Consequences

Intervention-consuming capabilities must apply intervention only within recorded scope and preserve the dependency in provenance.

### Supersedes

None

### Superseded by

None

---

## D033 — Intervention History Uses Active, Superseded, and Withdrawn States

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-003  
Related qualifications: INTV-003

### Decision

Committed Intervention Records use the canonical states:

- Active;
- Superseded;
- Withdrawn.

Transient draft/editing state is outside canonical recovery state.

### Rationale

Recovery needs current-state semantics without erasing intervention history or allowing abandoned UI input to affect recovery.

### Consequences

Only Active interventions influence current recovery. Superseded and Withdrawn records remain historically traceable.

### Supersedes

None

### Superseded by

None

---

## D034 — Committed Interventions Persist and Are Replayable

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-004  
Related qualifications: INTV-004

### Decision

Committed interventions that materially affect recovery must persist as part of recoverable recovery state and be replayable across later runs.

### Rationale

D014 defines deterministic recovery in terms of the same recorded developer interventions. Unrecorded or transient interventions would make results irreproducible.

### Consequences

Architecture must provide a durable representation of intervention identity, scope, state, provenance, and supersession relationships without requiring a particular storage technology.

### Supersedes

None

### Superseded by

None

---

## D035 — Developer Input Does Not Override Evidence or Create Facts by Itself

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-005  
Related qualifications: INTV-005

### Decision

Developer Input does not automatically outrank conflicting Evidence and cannot by itself create a Recovered Fact.

### Rationale

Developer intervention must be useful without becoming indistinguishable from historical evidence.

### Consequences

Evidence conflicts remain preserved. Developer Decisions may choose an operational recovery path while leaving historical certainty unresolved.

### Supersedes

None

### Superseded by

None

---

## D036 — Conflicting Active Interventions Require Explicit Resolution

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-006  
Related qualifications: INTV-006

### Decision

Incompatible Active interventions with overlapping scope form a Developer Intervention Conflict.

LegacyRevive.NET must not resolve that conflict by incidental execution order or hidden precedence.

### Rationale

Undocumented ordering would make intervention-influenced recovery nondeterministic and difficult to audit.

### Consequences

The conflict must be resolved explicitly through supersession, withdrawal, scope correction, or another deliberate developer decision.

### Supersedes

None

### Superseded by

None

---

## D037 — Intervention Withdrawal and Supersession Trigger Reevaluation

Status: Accepted  
Date: 2026-09-25  
Origin: `04-developer-intervention.md`; INTV-007  
Related qualifications: INTV-007

### Decision

When a materially relied-upon intervention is Superseded or Withdrawn, dependent current recovery items become `Needs Reevaluation`.

They may return to Current only after recomputation or traceable reaffirmation, and become `Invalidated` where valid support no longer remains.

### Rationale

Removing a premise without revisiting its downstream results would preserve conclusions whose support has changed.

### Consequences

Recovery-state dependency tracking must propagate intervention-state changes to materially dependent conclusions and reconstruction decisions.

### Supersedes

None

### Superseded by

None

---


## D038 — Convention Observations Describe Scoped Recurrence, Not Intent

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-001  
Related qualifications: CONV-001

### Decision

A Convention Observation is a scoped Derived Observation describing recurrence.

It must not be presented as a Recovered Fact about original developer intent or as proof of a universal historical design rule.

### Rationale

Recurring patterns can be useful to recovery while still being weaker than direct historical evidence about motivation or policy.

### Consequences

Convention reporting must use descriptive language and preserve scope and provenance.

### Supersedes

None

### Superseded by

None

---

## D039 — Convention Support Must Exclude Reconstruction Feedback

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-002  
Related qualifications: CONV-002

### Decision

Generated or reconstructed output that was shaped by a Convention Observation cannot later count as independent support for that Convention Observation.

### Rationale

Otherwise the recovery process could manufacture apparent recurrence from its own reconstruction choices.

### Consequences

Convention support paths must inherit provenance and obey D030 dependency-independence rules.

### Supersedes

None

### Superseded by

None

---

## D040 — Convention Scope Cannot Be Silently Generalized

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-003  
Related qualifications: CONV-003

### Decision

Every Convention Observation has explicit observed scope.

Generalizing to a broader scope creates a new supported derived conclusion rather than modifying or silently widening the original observation.

### Rationale

Legacy systems may contain multiple local conventions, and narrow recurrence does not establish a system-wide pattern.

### Consequences

Convention-consuming reconstruction and intervention must respect scope.

### Supersedes

None

### Superseded by

None

---

## D041 — Convention Recurrence Uses Explainable Domain-Appropriate Support

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-004  
Related qualifications: CONV-004

### Decision

A convention requires recurrence, but LegacyRevive.NET will not impose one universal numeric recurrence threshold across all convention domains.

Convention support must instead be explained using domain-appropriate evidence such as instance counts, independent source coverage, consistency, scope coverage, and exceptions.

### Rationale

A single threshold would imply comparable statistical meaning across fundamentally different pattern types without justification.

### Consequences

Later heuristics may define domain-specific criteria, but they must remain explainable and auditable.

### Supersedes

None

### Superseded by

None

---

## D042 — Convention Exceptions Must Remain Visible

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-005  
Related qualifications: CONV-005

### Decision

Material exceptions are part of a Convention Observation and must not be discarded merely to make the pattern appear stronger or cleaner.

### Rationale

Exceptions may reveal scope boundaries, variation, incomplete evidence, or weakness in the proposed convention.

### Consequences

Convention reporting and heuristics must preserve known exceptions.

### Supersedes

None

### Superseded by

None

---

## D043 — Convention Observations Use Support Profiles, Not Inferential Confidence

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-006  
Related qualifications: CONV-006

### Decision

Convention Observations expose descriptive support profiles including scope, provenance, supporting instances, independence, coverage where meaningful, and exceptions.

The inferential support-strength labels from D027 attach only to later Inferences based on Convention Observations, not to the Convention Observation itself.

### Rationale

This preserves the distinction between observing recurrence and inferring unresolved historical structure from that recurrence.

### Consequences

Reports must not label a convention itself `Strongly Supported`, `Plausible`, or `Tentative` as though it were an Inference.

### Supersedes

None

### Superseded by

None

---

## D044 — Competing Scoped Conventions Require Explicit Conflict Handling

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-007  
Related qualifications: CONV-007

### Decision

Materially incompatible Convention Observations that claim overlapping scope form a Convention Conflict.

They must not be resolved by hidden precedence or by discarding one pattern.

### Rationale

Multiple local conventions may legitimately coexist, while true overlap requires an explicit recovery choice.

### Consequences

Convention conflicts preserve both observations, scopes, support profiles, provenance, and exceptions, and may be resolved operationally through Developer Intervention.

### Supersedes

None

### Superseded by

None

---

## D045 — Convention History Is Append-Preserving and Reevaluable

Status: Accepted  
Date: 2026-09-25  
Origin: `05-convention-inference.md`; CONV-008  
Related qualifications: CONV-008

### Decision

Convention Observations are not rewritten in place when material support changes.

Updated observations supersede earlier records, and a convention whose dependencies materially change becomes `Needs Reevaluation` before it may guide new recovery decisions.

### Rationale

Historical support and earlier reasoning must remain auditable.

### Consequences

Convention inference must participate in dependency-driven reevaluation and preserve prior support profiles.

### Supersedes

None

### Superseded by

None

---


## D046 — Recovery Matrix Uses Four Contribution Classes

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-001  
Related qualifications: RMAT-001

### Decision

Artifact recovery contributions use four classes:

- Direct;
- Inferable;
- Heuristic;
- Not established by this artifact alone.

### Rationale

The matrix must preserve the Evidence Model distinction between directly present information and progressively weaker recovery conclusions.

### Consequences

Artifact documentation and later recovery-process design must not collapse these classes.

### Supersedes

None

### Superseded by

None

---

## D047 — Artifact-Type Capability Does Not Imply Artifact-Instance Content

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-002  
Related qualifications: RMAT-002

### Decision

A capability listed for an artifact type is conditional.

LegacyRevive.NET must inspect the actual artifact before claiming that the capability or data is present.

### Rationale

Build options, formats, stripping, corruption, and artifact variants can materially change available information.

### Consequences

Reports must distinguish theoretical artifact capability from observed artifact-instance findings.

### Supersedes

None

### Superseded by

None

---

## D048 — Artifact Format Must Be Classified Beyond File Extension

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-003  
Related qualifications: RMAT-003

### Decision

File extension alone is insufficient to assign recovery capability.

Managed implementation assemblies, reference assemblies, native hosts, satellite assemblies, and other format variants must be distinguished where relevant.

### Rationale

Different artifacts can share `.dll` or `.exe` extensions while containing materially different recoverable information.

### Consequences

Artifact discovery must classify actual format before applying matrix rules.

### Supersedes

None

### Superseded by

None

---

## D049 — Missing Recovery Artifact Does Not Prove Historical Nonexistence

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-004  
Related qualifications: RMAT-004

### Decision

An artifact missing from the supplied recovery set normally establishes only that it is unavailable to the current recovery.

It does not prove that the artifact never existed historically.

### Rationale

Deployment/capture sets are often incomplete and may omit build-time or debugging artifacts.

### Consequences

Historical absence claims require independent support.

### Supersedes

None

### Superseded by

None

---

## D050 — Companion-Artifact Corroboration Requires Defensible Relationship and Independence

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-005  
Related qualifications: RMAT-005

### Decision

One artifact may corroborate another only when their relationship is defensibly established and any claimed additional support respects dependency independence.

### Rationale

Filename similarity or deployment co-location can be useful signals but are not always sufficient to prove historical pairing.

### Consequences

Companion-artifact relationships themselves must retain provenance and epistemic classification.

### Supersedes

None

### Superseded by

None

---

## D051 — Matrix Capability and Artifact-Instance Findings Are Separate

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-006  
Related qualifications: RMAT-006

### Decision

`06-recovery-matrix.md` defines artifact-type capability and limitations.

Actual recovery runs produce artifact-instance profiles recording what was really observed, inferred, unsupported, or unresolved.

### Rationale

A capability matrix must not become a substitute for inspecting the supplied artifacts.

### Consequences

Later process/report design must preserve both the theoretical matrix and per-instance results.

### Supersedes

None

### Superseded by

None

---

## D052 — Recovery Ceilings Are Scoped to the Available Artifact Set

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-007  
Related qualifications: RMAT-007

### Decision

Statements that information is not recoverable should normally be expressed as not established or not reliably recoverable from the available artifact set unless a stronger impossibility claim is justified.

### Rationale

A later-discovered artifact may contain information unavailable in the current recovery set.

### Consequences

Reports should avoid absolute impossibility language unless the basis supports it.

### Supersedes

None

### Superseded by

None

---

## D053 — Cross-Artifact Recovery Preserves Epistemic Categories

Status: Accepted  
Date: 2026-09-25  
Origin: `06-recovery-matrix.md`; RMAT-008  
Related qualifications: RMAT-008

### Decision

Combining multiple artifacts may strengthen recovery support, but it does not erase the distinction between Direct observations, Inferences, Heuristic signals, assumptions, or reconstruction.

Cross-artifact recovery must not be reduced to one opaque recoverability score.

### Rationale

Different artifact combinations support different conclusions at different epistemic strengths.

### Consequences

Later process, reporting, and validation work must preserve conclusion-level provenance and classification.

### Supersedes

None

### Superseded by

None

---


## D054 — Recovery Uses a Stage-Oriented but Iterative Canonical Process

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-001  
Related qualifications: PROC-001

### Decision

Recovery uses a canonical conceptual stage sequence from intake and direct evidence toward inference, intervention, reconstruction, build diagnostics, validation, and a workable baseline.

The process is iterative rather than strictly linear.

### Rationale

Recovery requires both stronger-evidence-first progression and the ability to react to new evidence or feedback.

### Consequences

Later architecture must preserve stage responsibilities and iteration semantics.

### Supersedes

None

### Superseded by

None

---

## D055 — Stage Progression Does Not Promote Epistemic Category

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-002  
Related qualifications: PROC-002

### Decision

Process progression does not change an item's epistemic category.

An Inference does not become a Recovered Fact because later stages consume it.

### Rationale

Process position is not evidence strength.

### Consequences

All stages must preserve Evidence Model category and provenance.

### Supersedes

None

### Superseded by

None

---

## D056 — Recovery Reevaluates from the Earliest Materially Affected Stage

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-003  
Related qualifications: PROC-003

### Decision

When material recovery state changes, dependent current state is reevaluated from the earliest materially affected stage while unaffected valid work is retained.

### Rationale

This avoids both stale downstream state and unnecessary full reruns.

### Consequences

Architecture must support dependency-aware invalidation and scoped recomputation.

### Supersedes

None

### Superseded by

None

---

## D057 — Newly Added Artifacts Re-enter through Intake and Classification

Status: Accepted  
Date: 2026-09-25

Origin: `07-recovery-process.md`; PROC-004  
Related qualifications: PROC-004

### Decision

New Original Artifacts added after recovery begins must re-enter through intake, inventory/classification, and direct extraction.

### Rationale

Late evidence still requires identity, provenance, format classification, and direct extraction.

### Consequences

Recovery must support incremental artifact intake and downstream reevaluation.

### Supersedes

None

### Superseded by

None

---

## D058 — Developer Intervention Is Available at Explicit Points Throughout Recovery

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-005  
Related qualifications: PROC-005

### Decision

Developer Intervention is not confined to one fixed stage.

It may occur at explicit recovery questions throughout the process while remaining governed by `04-developer-intervention.md`.

### Rationale

Ambiguity can arise during classification, integration, inference, reconstruction, and build repair.

### Consequences

Workflow design must expose deliberate intervention points and forbid hidden intervention channels.

### Supersedes

None

### Superseded by

None

---

## D059 — Build Diagnostics Describe Candidate Recovery State, Not Historical Fact

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-006  
Related qualifications: PROC-006

### Decision

Build/compiler/tooling diagnostics directly describe the current candidate reconstruction and environment.

They may support recovery Inferences but do not by themselves prove the exact historical original project declaration.

### Rationale

Candidate failure and original historical design are related but distinct epistemic questions.

### Consequences

Build feedback must retain candidate-reconstruction provenance.

### Supersedes

None

### Superseded by

None

---

## D060 — Recovery Distinguishes Workspace, Run, and Checkpoint

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-007  
Related qualifications: PROC-007

### Decision

LegacyRevive.NET distinguishes:

- Recovery Workspace — durable context;
- Recovery Run — one execution through applicable stages;
- Recovery Checkpoint — an identified reproducible workspace state.

### Rationale

Iteration, replay, comparison, and deterministic diagnosis require these distinctions.

### Consequences

Later architecture must represent these concepts or equivalent semantics.

### Supersedes

None

### Superseded by

None

---

## D061 — Recovery Supports Partial, Blocked, Workable, and Validated States

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-008  
Related qualifications: PROC-008

### Decision

Recovery is not all-or-nothing.

Canonical process states include Partial, Blocked, Workable Baseline, and Validated Baseline.

### Rationale

Trustworthy recovery can remain useful even when later stages or dimensions are incomplete.

### Consequences

Workflow/reporting must preserve usable partial results and explicit blockers.

### Supersedes

None

### Superseded by

None

---

## D062 — Validation Feedback Cannot Leak Validation Reference into Recovery

Status: Accepted  
Date: 2026-09-25  
Origin: `07-recovery-process.md`; PROC-009  
Related qualifications: PROC-009

### Decision

Validation findings may trigger recovery reevaluation, but a Validation Reference remains outside Recovery Input and cannot be converted into Evidence or Developer Input merely to repair recovery.

### Rationale

Reference leakage would invalidate controlled evaluation.

### Consequences

Validation workflow must distinguish permissible diagnostic feedback from prohibited reference leakage.

### Supersedes

None

### Superseded by

None

---


## D063 — Candidate Project Is Distinct from Original Project

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-001  
Related qualifications: PROJ-001

### Decision

A Candidate Project is a reconstructed current recovery boundary and remains distinct from an Original Project that historically existed.

### Rationale

A workable recovered project structure may differ from the original repository while still being useful.

### Consequences

Project reports and generated files must not imply historical identity merely because a Candidate Project is current.

### Supersedes

None

### Superseded by

None

---

## D064 — Implementation Assemblies Anchor Initial Project Candidates without Proving Original Boundaries

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-002  
Related qualifications: PROJ-002

### Decision

Each distinct managed implementation assembly is treated as an initial project-candidate anchor by default unless stronger recovery state suggests otherwise.

The assembly boundary is not automatically proof of original project boundary.

### Rationale

Assembly boundaries are strong directly observed structural anchors, but historical build transformations and project organization may not be fully preserved.

### Consequences

Project reconstruction must support merge, split, and reclassification without rewriting the assembly evidence.

### Supersedes

None

### Superseded by

None

---

## D065 — Project Boundaries Require Explainable Support and Explicit Conflict Handling

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-003  
Related qualifications: PROJ-003

### Decision

Every material Candidate Project boundary must retain an explainable support profile.

Incompatible supported boundary proposals form a Project Boundary Conflict and may remain unresolved or be selected operationally by Developer Decision.

### Rationale

Project boundaries are often inferred and should not be presented as unquestioned historical facts.

### Consequences

Boundary provenance, contrary signals, and support strength must remain visible.

### Supersedes

None

### Superseded by

None

---

## D066 — Candidate Project Merge and Split Are Explicit Reconstruction Decisions

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-004  
Related qualifications: PROJ-004

### Decision

Merging or splitting initial project candidates is an explicit reconstruction decision with provenance and rationale.

### Rationale

Merge/split can produce a workable representation but changes the reconstructed structure beyond direct assembly boundaries.

### Consequences

Generated project boundaries must identify these transformations and their dependencies.

### Supersedes

None

### Superseded by

None

---

## D067 — Candidate Project Identity Is Separate from Reconstructed Name

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-005  
Related qualifications: PROJ-005

### Decision

Candidate Project identity is stable recovery-state identity independent of current display/file name.

Project names are reconstructed properties with their own provenance.

### Rationale

Names may be inferred or changed without meaning that the underlying candidate boundary is a different historical project.

### Consequences

Renaming must not erase identity/provenance or imply recovery of the original name.

### Supersedes

None

### Superseded by

None

---

## D068 — Project Kind and Target Framework Preserve Historical Uncertainty

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-006  
Related qualifications: PROJ-006

### Decision

Project Kind and target-framework/runtime properties are reconstructed from the strongest available recovery state.

Where exact historical values are not established, a practical reconstruction choice remains explicitly distinct from historical fact.

### Rationale

A workable project may require selecting values that surviving artifacts do not uniquely determine.

### Consequences

Generated project properties must retain provenance and uncertainty.

### Supersedes

None

### Superseded by

None

---

## D069 — Compiled Dependency Does Not Establish Original Project Declaration Mechanism

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-007  
Related qualifications: PROJ-007

### Decision

An observed compiled/runtime dependency does not by itself establish whether the original project declared it through `ProjectReference`, `PackageReference`, direct assembly reference, or another build mechanism.

Dependency representation in a Candidate Project is a provenance-bearing reconstruction choice.

### Rationale

Compiled outputs generally preserve dependency relationships more reliably than the exact source project-file mechanism that produced them.

### Consequences

Generated reference types must not be reported as directly recovered original declarations unless separately supported.

### Supersedes

None

### Superseded by

None

---

## D070 — Unresolved Dependencies and Project Cycles Remain Explicit

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-008  
Related qualifications: PROJ-008

### Decision

Dependencies that cannot yet be resolved remain explicit Unresolved Project Dependencies.

Candidate Project dependency cycles must not be silently broken merely to produce a build.

### Rationale

Both conditions may reveal incorrect boundaries or missing historical build information.

### Consequences

They create diagnostics/reevaluation or intervention opportunities while preserving unaffected project state.

### Supersedes

None

### Superseded by

None

---

## D071 — Generated Project and Solution Files Remain Reconstructed Artifacts

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-009  
Related qualifications: PROJ-009

### Decision

Generated project and solution files are Reconstructed Artifacts.

They do not become independent evidence of original project/solution structure.

### Rationale

Generated files encode recovery conclusions and choices and therefore inherit their dependency lineage.

### Consequences

Rescanning generated `.sln`/project files cannot independently corroborate the structure that generated them.

### Supersedes

None

### Superseded by

None

---

## D072 — Multiple Project Reconstructions May Coexist; Selection Is Operational

Status: Accepted  
Date: 2026-09-25  
Origin: `08-project-reconstruction.md`; PROJ-010  
Related qualifications: PROJ-010

### Decision

When materially different project structures remain plausible, multiple candidate structures may coexist.

Selecting one as the active reconstruction is an operational choice and does not convert it into historical fact.

### Rationale

Forcing one structure would hide ambiguity and reduce developer agency.

### Consequences

Candidate alternatives and active-selection provenance must remain inspectable.

### Supersedes

None

### Superseded by

None

---

## D073 — Source Reconstruction Uses Explicit Provenance Classes

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-001  
Related qualifications: SRC-001

### Decision

Source material is classified as Preserved Source Content, Metadata-Reconstructed Source, Decompiler-Generated Source, Inferred Source Structure, or Developer-Adjusted Source.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D074 — Source Provenance May Attach Below Whole-File Level

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-002  
Related qualifications: SRC-002

### Decision

Where a source file mixes materially different recovery origins, provenance may attach at finer source-element granularity.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D075 — Recovered Source File Identity Is Separate from Generated Path and Name

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-003  
Related qualifications: SRC-003

### Decision

Recovered Source File identity remains stable independently of current generated path/name; direct PDB/source paths retain separate provenance.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D076 — Directly Preserved Source Content Takes Precedence over Weaker Regeneration

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-004  
Related qualifications: SRC-004

### Decision

Directly preserved admissible source content is the stronger representation for the same material; generated layout/container choices remain separately reconstructed.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D077 — Source Skeleton Is a Valid Partial Recovery and Unknown Bodies Are Not Fabricated

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-005  
Related qualifications: SRC-005

### Decision

A Source Skeleton is a legitimate partial recovery, and unavailable behavior remains unresolved or explicit placeholder state rather than invented implementation.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D078 — Higher-Level Reconstruction of Lowered Code Is Generated Interpretation

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-006  
Related qualifications: SRC-006

### Decision

Higher-level C# reconstructed from compiler-lowered/generated forms is generated interpretation of compiled behavior, not proof of exact original syntax.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D079 — Source Recovery Ceilings Remain Explicit and Artifact-Set Scoped

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-007  
Related qualifications: SRC-007

### Decision

Unrecovered local names, comments, formatting, source partition, preprocessor branches, generator inputs, build-time inputs, and exact high-level source forms remain explicit recovery ceilings unless suitable source-derived evidence survives.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D080 — Source Organization and Candidate Project Ownership Are Reconstruction Choices

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-008  
Related qualifications: SRC-008

### Decision

Source file grouping, folders, names, and Candidate Project ownership are provenance-bearing reconstruction choices unless directly supported.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D081 — Multiple Source Candidates May Coexist; Selection Is Operational

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-009  
Related qualifications: SRC-009

### Decision

Multiple materially plausible Source Candidates may coexist; selecting one for active reconstruction does not make it Original Source.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D082 — Build-Driven Source Repair Remains Reconstructed or Developer-Adjusted Source

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-010  
Related qualifications: SRC-010

### Decision

Source changes made in response to build diagnostics remain reconstructed or Developer-Adjusted Source; successful compilation does not establish historical text.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D083 — Generated Source Cannot Independently Corroborate Its Own Reconstruction

Status: Accepted  
Date: 2026-09-25  
Origin: `09-source-reconstruction.md`; SRC-011  
Related qualifications: SRC-011

### Decision

Generated recovered source remains a Reconstructed Artifact and cannot independently corroborate the reconstruction decisions that produced it.

### Rationale

The source reconstruction model must preserve provenance, uncertainty, and recovery ceilings rather than presenting generated C# as Original Source.

### Consequences

Later architecture, tooling, reporting, build repair, and validation must preserve this rule.

### Supersedes

None

### Superseded by

None

---

## D084 — Validation Targets an Identified Recovery Checkpoint

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-001  
Related qualifications: VAL-001

### Decision

Validation evaluates an identified Validation Target tied to a stable recovery checkpoint/state. A materially changed recovery becomes a new target.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D085 — Validation Is Governed by an Explicit Validation Plan

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-002  
Related qualifications: VAL-002

### Decision

Each Validation Run uses a Validation Plan that declares intended dimensions, checks, prerequisites, target/reference context, environment, and intentional non-evaluation before result interpretation.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D086 — Validation Dimensions Remain Independently Visible

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-003  
Related qualifications: VAL-003

### Decision

Validation remains multidimensional across applicable structure, dependencies, framework/runtime, API, resources/configuration, source, build, and behavior. No summary may replace underlying dimensions.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D087 — Not Evaluated and Not Comparable Are Distinct from Validation Failure

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-004  
Related qualifications: VAL-004

### Decision

A dimension/check that was not executed or cannot be defensibly compared is not recorded as recovery failure or success; the state and reason remain visible.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D088 — Validation Requires Compatible Target and Reference Context

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-005  
Related qualifications: VAL-005

### Decision

Validation Target and Validation Reference Snapshot must represent compatible comparison subjects; known version, framework, configuration, data, or environment mismatches are recorded and constrain interpretation.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D089 — Blind Evaluation Freezes Recovery before Validation Reference Access

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-006  
Related qualifications: VAL-006

### Decision

In controlled blind evaluation, recovery proceeds only from allowed Recovery Input and the Validation Target is frozen before the withheld Validation Reference becomes available to validation.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D090 — Reference-Derived Repair Creates a New Recovery Lineage

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-007  
Related qualifications: VAL-007

### Decision

If information learned from a withheld Validation Reference is used to improve recovery, the original blind target remains preserved and the improved result is a new, explicitly non-blind recovery iteration/target.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D091 — Build, API, Source, and Behavioral Validation Establish Different Claims

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-008  
Related qualifications: VAL-008

### Decision

Build, structural/API, source, and behavioral validation establish different propositions. Passing one dimension does not imply equivalence in another, and behavioral success is limited to the scenarios evaluated.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D092 — Validation Runs and Findings Preserve Provenance and History

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-009  
Related qualifications: VAL-009

### Decision

Completed Validation Runs/findings retain target, reference, plan, comparator, environment, and diagnostic provenance. Materially changed conditions create a new run rather than rewriting historical findings.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D093 — Validated Baseline Does Not Mean Perfect Recovery

Status: Accepted  
Date: 2026-09-25  
Origin: `10-validation-strategy.md`; VAL-010  
Related qualifications: VAL-010

### Decision

Validated Baseline means an applicable Validation Plan was executed and its results/limitations are recorded; it does not imply all checks passed, all dimensions were comparable, exact reproduction, or global behavioral equivalence.

### Rationale

Validation must provide trustworthy, interpretable evidence about recovery quality without contaminating recovery or overstating what individual comparisons prove.

### Consequences

Later metrics, architecture, tooling, benchmark, and MVP work must preserve this validation boundary.

### Supersedes

None

### Superseded by

None

---

## D094 — Recovery Metrics Require Explicit Populations and Denominators

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-001  
Related qualifications: METR-001

### Decision

Every ratio/rate used to assess recovery must declare its eligible population, numerator, denominator, applicability/exclusions, and treatment of Not Evaluated and Not Comparable items. Non-evaluated/non-comparable populations remain visible rather than being silently treated as successes or failures.

### Rationale

Recovery percentages are otherwise easy to distort by changing or hiding denominators.

### Consequences

Metrics, benchmark reports, architecture, and UI/reporting must preserve denominator provenance and population accounting.

### Supersedes

None

### Superseded by

None

---

## D095 — Correctness and Coverage Are Separate Recovery Measurements

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-002  
Related qualifications: METR-002

### Decision

Recovery correctness and recovery coverage are separate measurements. Unknown/abstained items are not counted as correct merely because abstention was cautious, and they are not automatically counted as incorrect without regard to applicability/recoverability.

### Rationale

A system could appear highly accurate by answering only easy questions, or appear poor by responsibly abstaining where evidence cannot support an answer.

### Consequences

Metric reports must expose both assertion correctness and eligible-population coverage where abstention is possible.

### Supersedes

None

### Superseded by

None

---

## D096 — Set Reconstruction Uses Explicit Matching with Precision and Recall

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-003  
Related qualifications: METR-003

### Decision

For set-like recovery tasks, LegacyRevive.NET uses an explicit matching rule and normally reports precision and recall separately; F1 may be secondary but cannot replace them.

### Rationale

Project, dependency, package, type, resource, and conflict-detection tasks have asymmetric false-positive and false-negative costs that a single accuracy rate can hide.

### Consequences

Validation Plans/metric definitions must state entity identity/matching semantics before computing set metrics.

### Supersedes

None

### Superseded by

None

---

## D097 — Historical Fidelity and Recoverability-Aware Effectiveness Remain Separate

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-004  
Related qualifications: METR-004

### Decision

When the validation corpus can establish recoverability expectations, raw historical-reference fidelity and recoverability-aware effectiveness are reported as separate views. Items unavailable in principle from the allowed Recovery Input are not silently erased from historical-fidelity reporting and are not silently counted as tool failures in the recoverability-aware view.

### Rationale

The original reference may contain facts that surviving artifacts cannot establish, while the tool must still be accountable for missing information that was recoverable.

### Consequences

Benchmark scenarios must retain allowed-input/recovery-ceiling context and must not use recoverability labels as post-hoc excuses for misses.

### Supersedes

None

### Superseded by

None

---

## D098 — Dependency Existence and Historical Representation Are Measured Separately

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-005  
Related qualifications: METR-005

### Decision

Dependency metrics distinguish whether a relationship was recovered from whether its original declaration/packaging mechanism was recovered correctly.

### Rationale

A dependency can be semantically discovered even when the original ProjectReference/PackageReference/binary-reference mechanism is unknowable, and the inverse can also occur.

### Consequences

Dependency reports must avoid collapsing edge discovery and representation fidelity into one binary result.

### Supersedes

None

### Superseded by

None

---

## D099 — Source Fidelity Prioritizes Semantic Claims over Textual Similarity

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-006  
Related qualifications: METR-006

### Decision

Source-fidelity reporting keeps declaration/API, semantic/normalized, compiled-contract, and behavioral comparisons distinct from raw textual similarity. Textual similarity is secondary/diagnostic and does not establish Original Source identity.

### Rationale

Decompiler output can differ textually while remaining semantically useful, while textually similar output can still be behaviorally wrong.

### Consequences

Source metrics and reports must state the exact comparison level and must not label textual resemblance as source recovery proof.

### Supersedes

None

### Superseded by

None

---

## D100 — Operational Recovery Is Graduated and Environment-Scoped

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-007  
Related qualifications: METR-007

### Decision

Operational recovery is measured through graduated restore/build/run/test/refinement milestones under recorded environment/toolchain conditions rather than one buildable/not-buildable flag.

### Rationale

Compiler diagnostics and successive recovery passes provide valuable information about progress, but build success remains environment-scoped and does not prove behavior.

### Consequences

Operational benchmark results must retain milestone progression and environment context.

### Supersedes

None

### Superseded by

None

---

## D101 — Developer-Intervention Burden Is a First-Class Metric Family

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-008  
Related qualifications: METR-008

### Decision

LegacyRevive.NET measures the burden of developer intervention as a first-class operational metric family, including intervention counts/types/scopes and, where measured, manual repair/time-to-milestone with context.

### Rationale

Human intervention is an intentional part of the product model; the amount required materially affects recovery usefulness.

### Consequences

Reports must not hide intervention cost, while time measures must be treated as environment/human-dependent rather than universal correctness scores.

### Supersedes

None

### Superseded by

None

---

## D102 — Epistemic Accuracy Is a First-Class Metric Family

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-009  
Related qualifications: METR-009

### Decision

LegacyRevive.NET explicitly measures epistemic accuracy, including false assertions, empirical performance by inferential support class, appropriate versus missed unknowns where assessable, conflict detection, provenance validity, and circular-support violations.

### Rationale

A recovery assistant should be evaluated not only on producing answers, but also on whether it distinguishes strong support, uncertainty, conflict, and unsupported conclusions correctly.

### Consequences

Support-strength metrics remain descriptive calibration diagnostics and must not convert semantic support labels into invented probabilities.

### Supersedes

None

### Superseded by

None

---

## D103 — No Authoritative Global Recovery Score Replaces Dimension Metrics

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-010  
Related qualifications: METR-010

### Decision

LegacyRevive.NET has no authoritative single overall recovery score that replaces the underlying multidimensional metrics. Any future composite indicator is secondary, formula/weight transparent, and subordinate to dimension-level results.

### Rationale

A composite score can hide severe regressions in structural, epistemic, runtime, or operational dimensions.

### Consequences

Benchmarking, reporting, architecture, and UI design must preserve constituent metrics and cannot use one score as the canonical statement of recovery quality.

### Supersedes

None

### Superseded by

None

---

## D104 — Cross-Run Metric Comparison Requires Compatible Scenario and Definition Context

Status: Accepted  
Date: 2026-09-25  
Origin: `11-recovery-metrics.md`; METR-011  
Related qualifications: METR-011

### Decision

Metric comparisons across runs require materially compatible or explicitly qualified subject/reference versions, artifact scenarios, allowed Recovery Input, tool/configuration, metric definitions/matching rules, environment, intervention policy, and validation reference context. Material metric-definition changes create a new version rather than rewriting historical results.

### Rationale

Apparent improvements/regressions can otherwise be caused by changed evidence ceilings, denominators, comparators, or environment rather than tool behavior.

### Consequences

Corpus regression reports must preserve scenario/metric provenance and flag non-comparable comparisons.

### Supersedes

None

### Superseded by

None

---

## D105 — Architecture Is Logically Modular and Domain-Centered

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-001  
Related qualifications: ARCH-001

### Decision

LegacyRevive.NET uses a logical modular architecture centered on canonical recovery domain semantics. Logical modules do not imply separate processes/services, and deployment topology remains unconstrained by this decision.

### Rationale

The implementation needs strong responsibility/dependency boundaries without prematurely choosing a deployment model.

### Consequences

Architecture, hosting, MVP, and tooling work must keep core recovery semantics independent of process topology and concrete infrastructure.

### Supersedes

None

### Superseded by

None

---

## D106 — Canonical Recovery State Areas Remain Logically Separated

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-002  
Related qualifications: ARCH-002

### Decision

Original artifacts, recovery knowledge/history, developer intervention, reconstruction workspace, recovery checkpoints, and validation/metric history remain logically distinct state areas even if a later physical persistence implementation co-locates them.

### Rationale

These state classes have different epistemic and lifecycle semantics; collapsing them risks evidence contamination and history loss.

### Consequences

Persistence and architecture implementations must preserve these logical partitions and their provenance boundaries.

### Supersedes

None

### Superseded by

None

---

## D107 — Artifact Analyzers Emit Direct Observations and Diagnostics, Not Hidden Inference

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-003  
Related qualifications: ARCH-003

### Decision

Artifact analyzers are responsible for direct extraction and diagnostics. Historical/architectural conclusions that require inference are produced through explicit inference mechanisms rather than being hidden inside artifact readers.

### Rationale

This preserves Observation versus Inference semantics and makes analyzer output auditable.

### Consequences

Analyzer contracts, tooling adapters, and tests must distinguish direct extraction from inference.

### Supersedes

None

### Superseded by

None

---

## D108 — Support and Provenance Dependencies Are First-Class Architectural Data

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-004  
Related qualifications: ARCH-004

### Decision

Material support/provenance relationships are represented explicitly so upstream dependencies, conflicts, supersession, reevaluation, and circular/self-support detection can be enforced.

### Rationale

Canonical evidence semantics require traceability and protection against recursive corroboration.

### Consequences

Storage and application architecture must support dependency traversal and cycle protection; provenance cannot be a decorative string field.

### Supersedes

None

### Superseded by

None

---

## D109 — Inference Uses Explicit Named Rules with Governed Inputs

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-005  
Related qualifications: ARCH-005

### Decision

Inferential conclusions are produced through identifiable/versionable rules that declare eligible inputs, preconditions, produced conclusions, support dependencies, and explanation/support-strength behavior.

### Rationale

Named rules make heuristics explainable, testable, replaceable, and prevent hidden inference from leaking through other components.

### Consequences

Inference extension points must preserve provenance, assumptions, support-cycle protection, and validation-reference isolation.

### Supersedes

None

### Superseded by

None

---

## D110 — Recovery Orchestrator Coordinates the Canonical Iterative Process

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-006  
Related qualifications: ARCH-006

### Decision

A Recovery Orchestrator/application service coordinates runs, intake, analysis, integration, inference, intervention, reconstruction, build feedback, checkpoints, reevaluation, and validation invocation according to the canonical Recovery Process.

### Rationale

A central coordinator is needed to preserve cross-stage lifecycle rules without forcing artifact-specific logic into one component.

### Consequences

The orchestrator coordinates capabilities but does not own artifact-specific parsing or redefine domain semantics.

### Supersedes

None

### Superseded by

None

---

## D111 — Reconstruction Output Is Architecturally Isolated from Historical Evidence

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-007  
Related qualifications: ARCH-007

### Decision

Reconstruction consumes recovery knowledge and produces Reconstructed Artifacts in a separate reconstruction state area. Generated output cannot independently enter the original-system support graph as evidence for its own premises.

### Rationale

This enforces the anti-feedback rules already established by D004, D030, D071, and D083.

### Consequences

Reconstruction/build tooling and storage must preserve the one-way epistemic boundary.

### Supersedes

None

### Superseded by

None

---

## D112 — Validation Reference Is Architecturally Isolated from Blind Recovery

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-008  
Related qualifications: ARCH-008

### Decision

Blind recovery execution must not have access to Validation Reference content through repositories, analyzers, inference/convention services, shared caches, or equivalent indirect channels before the target is frozen.

### Rationale

Process rules alone are insufficient if architecture permits accidental reference leakage.

### Consequences

Validation/reference adapters and repositories must be exposed only in validation context; reference-derived recovery creates a new non-blind lineage.

### Supersedes

None

### Superseded by

None

---

## D113 — Recovery Checkpoints Preserve Stable Replayable History

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-009  
Related qualifications: ARCH-009

### Decision

Recovery Checkpoints identify stable recovery states by referencing the relevant run, artifact/input identities, current conclusions, active assumptions/interventions, reconstruction selection, and replay-relevant configuration without rewriting earlier checkpoints.

### Rationale

Validation, reproducibility, comparison, and iterative recovery require identifiable historical states.

### Consequences

Checkpoint architecture and persistence must be append/history-preserving and support deterministic replay where practical.

### Supersedes

None

### Superseded by

None

---

## D114 — Diagnostics Support Local Failure and Partial Progress

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-010  
Related qualifications: ARCH-010

### Decision

Analyzer, inference, reconstruction, build, validation, and metric failures are represented as structured scoped diagnostics and do not automatically fail the entire Recovery Run when safe partial progress is possible.

### Rationale

Legacy recovery frequently encounters incomplete or broken inputs; all-or-nothing execution would discard useful recoverable state.

### Consequences

Application architecture must preserve partial/blocked/workable states and associate diagnostics with the affected capability/items.

### Supersedes

None

### Superseded by

None

---

## D115 — Extension Contracts Cannot Bypass Canonical Recovery Semantics

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-011  
Related qualifications: ARCH-011

### Decision

Extensible analyzers, inference rules, reconstructors, validators, metric calculators, and report providers use explicit contracts and may not bypass provenance, support-cycle, developer-input, reconstruction-feedback, validation-isolation, or metric-accounting rules.

### Rationale

Extensibility is valuable only if plugins/capabilities cannot weaken the canonical epistemic model.

### Consequences

Future plugin/discovery/tooling design must enforce canonical contracts at integration boundaries.

### Supersedes

None

### Superseded by

None

---

## D116 — Persistence Technology Is Replaceable but Must Preserve Canonical History and Provenance

Status: Accepted  
Date: 2026-09-25  
Origin: `12-architecture.md`; ARCH-012  
Related qualifications: ARCH-012

### Decision

The architecture requires persistence of stable identities, history, support/provenance edges, supersession/invalidation, checkpoints, intervention records, reconstruction metadata, and validation/metric history, but does not prescribe a physical database/storage technology.

### Rationale

Storage technology should remain an implementation/tooling choice while domain semantics remain stable.

### Consequences

`13-tooling-and-dependencies.md` may evaluate concrete persistence choices only if they can preserve these semantics.

### Supersedes

None

### Superseded by

None

---

## D117 — LegacyRevive.NET Uses .NET 10 LTS as Its Application Runtime Baseline

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-001  
Related qualifications: TOOL-001

### Decision

LegacyRevive.NET targets .NET 10 (`net10.0`) as its primary application/runtime baseline. This does not change or upgrade the target framework of recovered applications.

### Rationale

A current LTS host provides modern platform APIs while allowing LegacyRevive.NET to inspect/reconstruct older .NET Framework and .NET applications.

### Consequences

Host/tool projects should target the selected modern runtime unless a capability has a justified platform-specific requirement; recovered framework identity remains recovery state.

### Supersedes

None

### Superseded by

None

---

## D118 — Dependencies Are Centrally Pinned and Deliberately Upgraded

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-002  
Related qualifications: TOOL-002

### Decision

Direct NuGet dependency versions are managed centrally and explicitly. Upgrades are compatibility/security/license/regression-reviewed rather than floating or automatically adopting the numerically newest package.

### Rationale

Recovery, validation, and benchmark reproducibility depend on knowing the effective toolset, and newest packages may target incompatible runtimes or materially change output.

### Consequences

Use Central Package Management; record effective versions where material; package changes that affect canonical semantics require governance review.

### Supersedes

None

### Superseded by

None

---

## D119 — System.Reflection.Metadata Is the Primary Managed Metadata Extraction API

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-003  
Related qualifications: TOOL-003

### Decision

`System.Reflection.Metadata`/`PEReader` is the primary direct managed PE/CLR metadata and Portable PDB extraction technology.

### Rationale

It provides low-level non-executing inspection suitable for provenance-bearing Observations and avoids loading arbitrary legacy assemblies into the host.

### Consequences

Managed metadata analyzers should use this platform API by default; higher-level tools cannot silently replace direct observations with interpretation.

### Supersedes

None

### Superseded by

None

---

## D120 — ICSharpCode.Decompiler Is the Selected C# Decompilation Engine

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-004  
Related qualifications: TOOL-004

### Decision

LegacyRevive.NET uses `ICSharpCode.Decompiler` as its selected managed C# decompilation engine behind a Capability Port.

### Rationale

ILSpy's engine is mature, embeddable, cross-platform, and avoids reimplementing a sophisticated decompiler.

### Consequences

Its output remains Decompiler-Generated/Reconstructed Source and cannot establish Original Source, original file identity, project boundaries, or intent.

### Supersedes

None

### Superseded by

None

---

## D121 — Portable PDB Is Primary and Native Windows PDB Support Is Optional

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-005  
Related qualifications: TOOL-005

### Decision

Portable PDB reading uses `System.Reflection.Metadata`. Native Windows PDB support may be provided through a platform-specific `Microsoft.DiaSymReader.Native` adapter rather than becoming a core cross-platform dependency.

### Rationale

Symbol formats have different platform requirements and many recoveries will not contain or require native Windows PDBs.

### Consequences

Symbol-reading remains behind a capability port; absence/unreadability is diagnostic/recovery state, not proof of historical absence.

### Supersedes

None

### Superseded by

None

---

## D122 — Roslyn Is the Selected C# Syntax and Semantic Engine

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-006  
Related qualifications: TOOL-006

### Decision

LegacyRevive.NET uses `Microsoft.CodeAnalysis.CSharp` (Roslyn) for reconstructed C# parsing, syntax transformation, semantic/compilation checks, normalization, and source-level validation support.

### Rationale

Roslyn provides the native compiler platform rather than requiring a custom C# parser/semantic model.

### Consequences

Roslyn diagnostics remain about reconstructed code and do not become historical evidence.

### Supersedes

None

### Superseded by

None

---

## D123 — MSBuild Object Model and Locator Are Selected; Canonical Builds Execute Out of Process

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-007  
Related qualifications: TOOL-007

### Decision

Project construction/evaluation uses `Microsoft.Build` and `Microsoft.Build.Locator` where applicable. Restore/build execution is performed through an out-of-process `dotnet`/MSBuild adapter as the canonical path.

### Rationale

This combines the public MSBuild object model with stronger process/toolset isolation and explicit command/environment provenance for actual builds.

### Consequences

The build adapter records toolset context; in-process evaluation cannot leak build results into Original-System Evidence.

### Supersedes

None

### Superseded by

None

---

## D124 — NuGet Package Inspection Is Local-First and Remote Feed Access Is Explicit Enrichment

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-008  
Related qualifications: TOOL-008

### Decision

Local `.nupkg`/`.nuspec` analysis uses `NuGet.Packaging`. `NuGet.Protocol` may be used only through an explicit remote-feed enrichment capability.

### Rationale

Supplied package artifacts and information fetched from external feeds have different provenance and evidence ceilings.

### Consequences

Network access is configurable/disableable; feed-derived information is not reported as if recovered from the supplied artifact set.

### Supersedes

None

### Superseded by

None

---

## D125 — SQLite via Microsoft.Data.Sqlite Is the Default Local Persistence

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-009  
Related qualifications: TOOL-009

### Decision

LegacyRevive.NET uses SQLite through `Microsoft.Data.Sqlite` as the default local workspace/state persistence technology, with explicit repository/schema boundaries preserving the canonical logical stores.

### Rationale

An embedded transactional store fits local recovery workspaces without an external database service and can represent append history and support edges.

### Consequences

Logical store separation remains intact; large binary artifacts normally remain in the artifact filesystem/store; persistence may later be replaced only if canonical semantics are preserved.

### Supersedes

None

### Superseded by

None

---

## D126 — Platform JSON and XML APIs Are the Structured Interchange Baseline

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-010  
Related qualifications: TOOL-010

### Decision

LegacyRevive.NET uses `System.Text.Json` for durable JSON interchange/report data and platform XML APIs such as `System.Xml.Linq` for XML/configuration analysis, with explicit versioned contracts for durable formats.

### Rationale

Platform serializers/parsers avoid unnecessary dependencies while supporting deterministic controlled representations.

### Consequences

Raw original XML/content is preserved separately when exact content is evidence; arbitrary internal object graphs are not treated as durable serialized schemas.

### Supersedes

None

### Superseded by

None

---

## D127 — Generic Host, Microsoft DI/Logging, and System.CommandLine Are the Baseline Host Stack

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-011  
Related qualifications: TOOL-011

### Decision

LegacyRevive.NET uses the .NET Generic Host with Microsoft.Extensions dependency injection/logging/configuration and `System.CommandLine` for the CLI host.

### Rationale

These are maintained .NET ecosystem primitives appropriate for a command-line-first modular application.

### Consequences

The composition root remains in host/infrastructure; Domain/Core is container- and CLI-agnostic.

### Supersedes

None

### Superseded by

None

---

## D128 — External Tool Execution Uses a Controlled System.Diagnostics.Process Adapter

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-012  
Related qualifications: TOOL-012

### Decision

External process execution uses `System.Diagnostics.Process` behind the canonical execution port rather than adding a separate process-wrapper dependency by default.

### Rationale

The platform API is sufficient if argument construction, environment, cancellation, output capture, exit status, and provenance are handled centrally.

### Consequences

Shell-string composition is avoided; tool invocation is structured and auditable.

### Supersedes

None

### Superseded by

None

---

## D129 — LegacyRevive-Generated IDs Use UUIDv7 and Artifacts Retain SHA-256 Content Identity

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-013  
Related qualifications: TOOL-013

### Decision

Where GUID identity is appropriate, LegacyRevive-generated domain records use .NET `Guid.CreateVersion7()`. Original artifact content is fingerprinted with SHA-256 in addition to its stable Artifact record identity.

### Rationale

UUIDv7 provides platform-native ordered unique IDs for append-heavy histories, while content hashes bind analysis to exact bytes.

### Consequences

Generated IDs are never presented as subject-system historical IDs; ArtifactId, content hash, and original path/provenance remain distinct.

### Supersedes

None

### Superseded by

None

---

## D130 — xUnit v3 Is the Baseline Automated Test Framework

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-014  
Related qualifications: TOOL-014

### Decision

LegacyRevive.NET uses `xunit.v3` as its baseline automated test framework across unit, integration, corpus, determinism, reconstruction, and validation-boundary tests.

### Rationale

A mature current .NET testing framework supports the broad controlled test corpus required by the project.

### Consequences

Test infrastructure must preserve recovery-versus-validation reference isolation and must not leak ground truth into blind recovery paths.

### Supersedes

None

### Superseded by

None

---

## D131 — External Dependencies Require Security, License, Source, and Capability Review

Status: Accepted  
Date: 2026-09-25  
Origin: `13-tooling-and-dependencies.md`; TOOL-015  
Related qualifications: TOOL-015

### Decision

Every direct external dependency requires recorded review of identity/source, license, target-framework support, maintenance/security state, selected version, architectural capability served, and upgrade/replacement considerations.

### Rationale

Recovery tooling operates on sensitive and potentially untrusted legacy material and should minimize dependency risk and provenance ambiguity.

### Consequences

Dependency/security CI may inform tool maintenance but cannot change historical conclusions about dependencies recovered from subject applications.

### Supersedes

None

### Superseded by

None

---

## D132 — MVP Proves the Complete Minimum Managed-Artifact Recovery Loop

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-001  
Related qualifications: MVP-001

### Decision

The MVP must implement an end-to-end managed-.NET recovery loop from local artifact intake through evidence-backed reconstruction, developer-visible ambiguity/context, candidate workspace generation, build attempt where applicable, checkpoint, and export/reporting.

### Rationale

The product hypothesis is not proven by isolated metadata inspection or decompilation alone; the developer must receive a usable recovery baseline.

### Consequences

MVP planning and acceptance must preserve an end-to-end user journey rather than treating disconnected subsystems as sufficient.

### Supersedes

None

### Superseded by

None

---

## D133 — MVP Execution Is Windows x64 First

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-002  
Related qualifications: MVP-002

### Decision

The first supported execution environment is Windows x64 with the .NET 10 host baseline. Cross-platform execution is deferred, while Domain/Core and capability boundaries remain future cross-platform capable.

### Rationale

Legacy .NET Framework recovery/build tooling, targeting packs, MSBuild, and native Windows PDB scenarios make Windows the highest-value first environment.

### Consequences

MVP documentation/testing may assume Windows for execution prerequisites but must not embed unnecessary Windows dependencies in the domain model.

### Supersedes

None

### Superseded by

None

---

## D134 — MVP Uses Local Directory Recovery Input with Defined Managed Artifact Analyzers

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-003  
Related qualifications: MVP-003

### Decision

The MVP takes a local directory tree as primary Recovery Input, inventories every supplied file, and provides direct analyzers for managed DLL/EXE plus the defined high-value configuration, symbol, documentation, runtime, package, and resource companion artifacts.

### Rationale

A bounded local input model makes provenance/replay testable while covering the core deployment-artifact scenarios.

### Consequences

Unsupported files remain inventoried/diagnosed; deep native/installer/live-environment recovery is not an MVP requirement.

### Supersedes

None

### Superseded by

None

---

## D135 — MVP Managed Assembly Analysis Recovers the Metadata Needed for Reconstruction

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-004  
Related qualifications: MVP-004

### Decision

The MVP managed-assembly analyzer recovers direct metadata observations for identity, references, types/members/signatures/attributes/generics/resources/entry points and IL availability sufficient for project/source reconstruction without assuming one assembly equals one original project.

### Rationale

This is the minimum evidence substrate for meaningful reconstruction and explainability.

### Consequences

The analyzer remains within the Observation boundary and cannot hide project/ownership inference.

### Supersedes

None

### Superseded by

None

---

## D136 — MVP Includes Ownership Classification and Evidence-Backed Candidate Projects

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-005  
Related qualifications: MVP-005

### Decision

The MVP classifies relevant assemblies using the canonical ownership vocabulary and generates evidence/inference-backed Candidate Projects while preserving Unknown, alternative structures, and unresolved merge/split questions.

### Rationale

Project reconstruction is central product value but original project boundaries are not directly encoded by ordinary binaries.

### Consequences

The MVP must not implement a naïve one-project-per-DLL rule as historical truth.

### Supersedes

None

### Superseded by

None

---

## D137 — MVP Reconstructs Dependency Relationships Before Choosing Representation

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-006  
Related qualifications: MVP-006

### Decision

The MVP builds a dependency graph and then represents relationships as project/package/binary/framework/unresolved dependencies according to available support, preserving uncertainty about historical declaration mechanisms and package versions.

### Rationale

Dependency existence is often recoverable when original project/package representation is not.

### Consequences

Remote feed lookup is not required for completion and package versions/mechanisms must not be invented merely to restore.

### Supersedes

None

### Superseded by

None

---

## D138 — MVP Generates Decompiler-Backed C# with Explicit Source Provenance

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-007  
Related qualifications: MVP-007

### Decision

The MVP reconstructs usable C# source from supported managed implementation assemblies, uses PDB/XML documentation where available, organizes output deterministically when historical layout is unknown, and preserves decompiler/reconstructed provenance.

### Rationale

Source reconstruction is necessary for a development baseline, but generated C# is not the Original Source.

### Consequences

Decompiler failures/unknown layout remain diagnostics rather than fabricated source-history claims.

### Supersedes

None

### Superseded by

None

---

## D139 — MVP Exports a Deterministic Candidate Solution and Project Tree

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-008  
Related qualifications: MVP-008

### Decision

The MVP exports a conventional candidate solution/project/source tree plus recovery manifests/reports, with project metadata/dependencies/resources/configuration populated where support exists and deterministic substantive output for the same effective checkpoint/tool baseline.

### Rationale

A developer needs an ordinary development representation rather than only an internal recovery database.

### Consequences

Generated solution/project/source files remain Reconstructed Artifacts and do not self-corroborate.

### Supersedes

None

### Superseded by

None

---

## D140 — MVP Includes Explicit Developer Intervention and Basic Recovered Context

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-009  
Related qualifications: MVP-009

### Decision

The MVP supports Developer Assertions, Decisions, Corrections, persisted history and downstream reevaluation, and surfaces basic deterministic scoped naming/namespace/dependency/type-pattern context for material decisions.

### Rationale

Human intervention is part of the product model and should be informed by recovered context rather than blind prompts.

### Consequences

Machine-learned convention inference and categorical architecture labels are not required for MVP.

### Supersedes

None

### Superseded by

None

---

## D141 — MVP Recovery Workspaces Are Persistent and Resumable

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-010  
Related qualifications: MVP-010

### Decision

Every MVP recovery uses a persistent local Recovery Workspace that preserves artifact inventory, recovery knowledge/support graph, interventions, candidates, diagnostics, runs/checkpoints, build attempts, and export metadata, and can be resumed without erasing history.

### Rationale

Legacy recovery is iterative and may require later artifacts or developer corrections.

### Consequences

Artifact addition/correction triggers canonical earliest-affected-stage reevaluation rather than full historical rewriting.

### Supersedes

None

### Superseded by

None

---

## D142 — MVP Attempts Controlled Builds but Does Not Fabricate History to Make Them Green

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-011  
Related qualifications: MVP-011

### Decision

The MVP attempts restore/build when local prerequisites exist, captures structured tool/environment diagnostics, may apply deterministic/narrowly justified repairs, and must not perform speculative package/framework/project/behavior changes merely to achieve a successful build.

### Rationale

Build feedback is valuable but green compilation cannot justify unsupported historical assumptions.

### Consequences

Build success is a recovery milestone, not the sole definition of a completed MVP run.

### Supersedes

None

### Superseded by

None

---

## D143 — MVP Preserves Partial, Blocked, Workable, and Validated Outcome Semantics

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-012  
Related qualifications: MVP-012

### Decision

The MVP reports the applicable canonical recovery state and distinguishes tool execution failure from legitimate Partial/Blocked recovery with explicit Unknowns, conflicts, blockers, interventions, and build status.

### Rationale

Incomplete deployments can still yield valuable trustworthy recovery output.

### Consequences

CLI status/exit behavior and reports must not label every non-building recovery as tool failure.

### Supersedes

None

### Superseded by

None

---

## D144 — MVP Is CLI-First with Recover, Status, Explain, Decide, Build, and Export Capabilities

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-013  
Related qualifications: MVP-013

### Decision

The MVP user surface is a CLI providing end-to-end recover, workspace status, provenance explanation, developer intervention, controlled build, and export capabilities. Exact flag spelling may evolve while these capabilities remain.

### Rationale

A CLI is the smallest practical host for repeatable local recovery and automation.

### Consequences

GUI/web/API/IDE hosts are deferred without changing Application/Domain responsibilities.

### Supersedes

None

### Superseded by

None

---

## D145 — Blind Controlled Validation Is Mandatory for MVP Release

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-014  
Related qualifications: MVP-014

### Decision

Although full user-facing historical-reference validation is not required in the normal MVP workflow, the MVP must be developed/released against controlled blind validation where recovery is frozen before the known reference is revealed to the validator.

### Rationale

The product's fidelity and epistemic discipline cannot be assessed solely by self-generated build output.

### Consequences

Release corpus infrastructure must preserve validation-reference isolation and canonical metrics.

### Supersedes

None

### Superseded by

None

---

## D146 — MVP Release Uses Multidimensional Functional, Epistemic, Replay, History, Quality, and Usability Gates

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-015  
Related qualifications: MVP-015

### Decision

MVP release acceptance requires end-to-end functional completion, no known canonical epistemic-boundary violations, deterministic substantive replay, history-preserving persistence/reevaluation, canonical multidimensional recovery reporting, and developer-visible usability; no single global recovery score gates release.

### Rationale

A green build or composite score could hide provenance, uncertainty, replay, or usability failures.

### Consequences

Known self-corroboration/reference-leakage/fabrication violations are release-blocking even when other metrics improve.

### Supersedes

None

### Superseded by

None

---

## D147 — MVP Explicitly Defers Non-Core Recovery and Product Surfaces

Status: Accepted  
Date: 2026-09-25  
Origin: `14-mvp.md`; MVP-016  
Related qualifications: MVP-016

### Decision

Native-code reconstruction, arbitrary installer analysis, runtime process/memory recovery, live-database reconstruction, runtime plugin loading, GUI/web/IDE/multi-user/cloud hosting, required remote enrichment, generalized ML/LLM authority, automatic modernization, universal build success, and global behavioral proof are outside the MVP.

### Rationale

The first release must prove the core managed-artifact recovery proposition without absorbing the entire roadmap.

### Consequences

Deferred capabilities belong to `15-roadmap.md` and cannot silently become MVP requirements.

### Supersedes

None

### Superseded by

None

---

## D148 — Roadmap Sequencing Does Not Constitute Accepted Detailed Design

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-001  
Related qualifications: ROAD-001

### Decision

The roadmap defines strategic capability sequencing and promotion rules, but a roadmap item is not accepted detailed design merely by appearing in `15-roadmap.md`.

### Rationale

Future capabilities need normal qualification/design/audit governance; otherwise the roadmap would bypass the canonical ownership model.

### Consequences

Promoted roadmap items must enter the normal governance lifecycle before implementation details become canonical.

### Supersedes

None

### Superseded by

None

---

## D149 — Post-MVP Horizons Are Evidence- and Dependency-Gated Rather Than Date Promises

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-002  
Related qualifications: ROAD-002

### Decision

Post-MVP capability ordering is expressed through evidence/dependency horizons rather than fixed canonical release dates.

### Rationale

Recovery needs, corpus evidence, intervention burden, platform demand, and prerequisites are more durable planning signals than speculative dates.

### Consequences

Calendar schedules may exist separately, but changing dates does not rewrite the canonical dependency order.

### Supersedes

None

### Superseded by

None

---

## D150 — Roadmap Promotion Requires Demonstrated Recovery, Developer, or Safety Value

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-003  
Related qualifications: ROAD-003

### Decision

A deferred capability should be promoted when product/research evidence shows meaningful recovery impact, developer-effort reduction, epistemic-safety improvement, recurring corpus demand, prerequisite leverage, or comparable justified value.

### Rationale

Technical novelty alone is not sufficient reason to expand scope.

### Consequences

Roadmap prioritization should cite observable need and validation feasibility.

### Supersedes

None

### Superseded by

None

---

## D151 — The First Post-MVP Horizon Strengthens the Core Recovery Loop

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-004  
Related qualifications: ROAD-004

### Decision

The first post-MVP priority is improving build/project/source recovery quality, intervention UX, explainability, and reporting within the proven managed-artifact workflow before pursuing breadth for its own sake.

### Rationale

The highest-value next step is reducing failure and developer burden in the product path already validated by the MVP.

### Consequences

Core-loop regressions and intervention data should guide H1 work.

### Supersedes

None

### Superseded by

None

---

## D152 — The Second Horizon Broadens Platform and Artifact Evidence Coverage

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-005  
Related qualifications: ROAD-005

### Decision

After core-loop stabilization, LegacyRevive.NET may expand cross-platform execution and deeper support for symbols, deployed source-like artifacts, service/schema/model artifacts, and deployment descriptors.

### Rationale

Broader evidence coverage is more useful once the canonical recovery loop is stable enough to exploit it.

### Consequences

Platform-specific capability differences remain explicit and do not change core semantics.

### Supersedes

None

### Superseded by

None

---

## D153 — External Enrichment May Expand Post-MVP but Remains Provenance-Distinct

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-006  
Related qualifications: ROAD-006

### Decision

Remote package, symbol, source-server, Source Link, reference-pack, and similar enrichment may be added later, but externally retrieved material remains distinct from supplied Recovery Input and must be separately attributable.

### Rationale

External knowledge can materially improve recovery while otherwise corrupting evidence-ceiling claims.

### Consequences

Offline mode and validation enrichment-state provenance remain required.

### Supersedes

None

### Superseded by

None

---

## D154 — Advanced Inference and Learned Ranking Remain Subordinate to the Evidence Model

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-007  
Related qualifications: ROAD-007

### Decision

Future richer convention inference, candidate ranking, or learned-model assistance may propose/rank reconstructions but remains inferential, provenance-bearing, and subordinate to canonical Evidence Model rules.

### Rationale

Automation must not turn developer choices or model confidence into historical truth.

### Consequences

New learned/ranking capabilities require research-corpus and held-out validation evidence before governed promotion.

### Supersedes

None

### Superseded by

None

---

## D155 — Richer Product Hosts Reuse Canonical Application and Domain Semantics

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-008  
Related qualifications: ROAD-008

### Decision

Future desktop, local-web, IDE, and API hosts must reuse canonical Application/Domain recovery capabilities rather than creating host-specific recovery semantics.

### Rationale

Multiple UX surfaces should not fragment the recovery model.

### Consequences

Host-specific functionality may vary, but evidence/intervention/reconstruction rules remain shared.

### Supersedes

None

### Superseded by

None

---

## D156 — Validation and Held-Out Corpus Maturity Expand Alongside Product Capability

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-009  
Related qualifications: ROAD-009

### Decision

As LegacyRevive.NET adds capabilities, its held-out validation corpus, starvation/corruption scenarios, semantic comparators, behavioral checks, intervention-cost studies, and regression reporting must expand correspondingly.

### Rationale

More automation without stronger validation increases false-confidence risk.

### Consequences

Research Corpus and held-out Validation Corpus remain distinct.

### Supersedes

None

### Superseded by

None

---

## D157 — Runtime Plugin Ecosystem Is Deferred Until Need and Trust Boundaries Are Demonstrated

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-010  
Related qualifications: ROAD-010

### Decision

Runtime third-party plugin loading is not introduced merely because extension contracts exist; it requires demonstrated ecosystem need plus explicit governance for trust, versioning, isolation, dependency loading, provenance, determinism, failure containment, and permissions.

### Rationale

Compile-time/first-party modularity is sufficient until independently deployed extensions justify the added risk.

### Consequences

Future plugin architecture requires its own qualification/design cycle.

### Supersedes

None

### Superseded by

None

---

## D158 — Collaborative and Enterprise Scale Requires Explicit New Security and Concurrency Design

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-011  
Related qualifications: ROAD-011

### Decision

Shared/cloud/distributed recovery is a distinct expansion requiring identity, authorization, concurrency, intervention ownership, audit, retention, sharing, sensitive-artifact, and distributed-determinism design.

### Rationale

The local workspace architecture must not be mechanically stretched into a multi-user service.

### Consequences

Persistence/hosting may change only through governed design that preserves canonical provenance/history semantics.

### Supersedes

None

### Superseded by

None

---

## D159 — Native and Runtime Recovery Is a Distinct Research and Capability Domain

Status: Accepted  
Date: 2026-09-26  
Origin: `15-roadmap.md`; ROAD-012  
Related qualifications: ROAD-012

### Decision

Native binaries, mixed-mode artifacts, runtime dumps/logs/traces, database snapshots, installers, and similar sources require their own recoverability/evidence models and are not treated as equivalent to managed IL/source reconstruction.

### Rationale

These sources have materially different information boundaries and claim risks.

### Consequences

Any native/runtime recovery capability must define its own evidence ceiling and validation strategy before product claims are accepted.

### Supersedes

None

### Superseded by

None

---


## D160 — External Enrichment Admitted to Recovery Is Independent Contextual Evidence

Status: Accepted  
Date: 2026-09-26  
Origin: Whole-set governance/consistency audit; DOC-019  
Related qualifications: DOC-019

### Decision

Information fetched through External Enrichment is not Supplied Recovery Input. When an explicitly enabled enrichment result is admitted to support recovery conclusions, it is represented as **Independent Contextual Evidence** within Recovery Input, with provenance that preserves its external origin and enrichment state.

Validation Reference remains outside Recovery Input and must not be repurposed as enrichment for the same blind recovery lineage.

### Rationale

The Evidence Model already permits Independent Contextual Evidence, while the roadmap and tooling documents require external enrichment to remain distinct from supplied artifacts. Making the admission rule explicit prevents external information from being misreported as target-surviving evidence without incorrectly excluding legitimate contextual evidence from the support graph.

### Consequences

Reports and metrics must distinguish Supplied Recovery Input from admitted External Enrichment; offline/blind scenarios may exclude enrichment entirely; enrichment cannot create Original-System Evidence provenance; and enabling enrichment changes the recovery/evidence ceiling and must be recorded.

### Supersedes

None

### Superseded by

None

## D161 — Application Is the Normal Gateway to Domain/Core for Outer Adapters

Status: Accepted  
Date: 2026-09-26  
Origin: Initial physical solution/project dependency design; ARCH-013  
Related qualifications: ARCH-013

### Decision

`LegacyRevive.Application` is the normal dependency gateway to `LegacyRevive.Domain` / Domain Core for outer adapter and capability projects.

Outer adapters reference Application-owned Capability Ports by default. A direct adapter-to-Domain/Core dependency is permitted only when implementing an inward-owned capability contract genuinely requires Domain/Core-owned types or behaviour. Such direct dependencies are exceptional, explicit, and justified by the contract being implemented rather than by convenience. They may arise from an Application-owned port that exposes Domain/Core types or, where the canonical Capability Port model genuinely requires it, from a Domain/Core-owned port; Domain/Core ownership must not be introduced merely to bypass the normal Application gateway.

The host/composition root may reference Application and the concrete adapter implementations required to compose the application, consistent with D127.

### Rationale

This preserves a clear inward dependency shape without imposing an artificial prohibition that would force Application-owned DTOs, duplicate representations, or mapping layers solely to hide a genuine Domain/Core type dependency. It also prevents outer adapters from treating Domain/Core as their default implementation surface and keeps Application as the normal boundary through which replaceable capabilities participate in recovery workflows.

### Consequences

- `Application → Domain/Core` is the normal inward dependency.
- Outer adapter/capability projects depend on Application by default.
- Direct adapter/capability references to Domain/Core require a concrete implementation need arising from an inward-owned capability contract and should remain visible and reviewable.
- A direct Domain/Core reference must not be introduced merely to bypass Application orchestration or canonical ports.
- Domain/Core remains independent of Application, hosts, adapters, and concrete tooling packages.
- The CLI/host composition root may reference the concrete adapter implementations it registers.
- This decision constrains dependency direction but does not settle the final physical project count or project names.

### Supersedes

None

### Superseded by

None

---


## D162 — LegacyRevive.NET Repository Uses SLNX as Its Solution Format

Status: Superseded  
Date: 2026-09-26  
Origin: Prior LegacyRevive.NET implementation discussion; omission rediscovered during initial physical solution/project design; TOOL-016  
Related qualifications: TOOL-016

### Decision

LegacyRevive.NET's own development repository uses `LegacyRevive.slnx` as its canonical solution representation.

This decision applies to the LegacyRevive.NET codebase itself. It does not require recovered Candidate Solutions to use `.slnx`; reconstructed solution representation remains governed by Project Reconstruction, MVP/output requirements, and the applicable recovered target/tooling context.

### Rationale

LegacyRevive.NET already targets .NET 10 as its application/runtime baseline. The .NET 10 SDK creates `.slnx` by default for `dotnet new sln`, and current Microsoft tooling treats SLNX as a stable, supported, maintainable solution format. Using `.slnx` therefore aligns the repository with its modern SDK baseline and avoids repeatedly falling back to the older `.sln` convention when conversational context is unavailable.

Recording the choice canonically is also necessary because earlier discussion had established the intended direction but it had not been consolidated into the Project source documents; that omission allowed the physical-design discussion to regress to `.sln`.

### Consequences

- The root development solution is named `LegacyRevive.slnx`.
- New repository scaffolding, documentation, CI examples, and implementation guidance should refer to `LegacyRevive.slnx`, not `LegacyRevive.sln`, unless a specifically governed compatibility exception is required.
- The repository solution format is an implementation/tooling choice and does not constitute evidence or a reconstruction rule for recovered applications.
- Candidate Solution generation remains free to use `.sln`, `.slnx`, or another supported representation where appropriate under the recovery model.
- A future material replacement of `.slnx` requires a new qualification and, where appropriate, a superseding decision rather than silent drift.

### Supersedes

None

### Superseded by

D169

---


## D163 — Local Recovery Workspace Separates Internal State from the Materialized Reconstruction

Status: Accepted  
Date: 2026-09-26  
Origin: Workspace/materialization design discussion; `12-architecture.md`; ARCH-014  
Related qualifications: ARCH-014

### Decision

For the MVP local workspace, the Recovery Workspace root is the developer-facing working location. LegacyRevive.NET-owned internal recovery state is contained beneath a reserved `.legacyrevive` directory, while the root outside `.legacyrevive` contains the current materialized reconstructed development representation and developer-facing outputs.

The `.legacyrevive` boundary is physical organization for the local workspace; it does not collapse the canonical logical state partitions. The exact internal subdirectory/table layout remains an implementation choice unless separately governed.

### Rationale

Developers should be able to open and work with the reconstructed solution using ordinary IDEs/editors without exposing SQLite databases, checkpoint data, provenance stores, and other recovery plumbing as top-level development files. A reserved internal directory gives LegacyRevive.NET a stable ownership boundary while preserving a conventional development tree.

### Consequences

- `.legacyrevive` is reserved for LegacyRevive.NET internal workspace state and supporting internal files.
- Reconstructed solution/project/source files are materialized outside `.legacyrevive` for normal developer use.
- The visible materialization is not itself the authority for canonical recovery state merely because it exists on disk.
- Original-artifact preservation remains subject to ARCH-015; this decision does not decide whether original bytes must be workspace-owned or may be represented by qualifying immutable external references.

### Supersedes

None

### Superseded by

None

---

## D164 — Export Deterministically Materializes Canonical Recovery State

Status: Accepted  
Date: 2026-09-26  
Origin: Export/workspace design discussion; `14-mvp.md`; MVP-017  
Related qualifications: MVP-017

### Decision

`export` materializes a standalone Candidate Development Baseline from the selected/current effective canonical Recovery Workspace state, identified by the applicable Recovery Checkpoint together with replay-relevant tool/configuration/intervention context. It is not a blind copy of whatever files happen to exist in the materialized working tree.

For the same effective canonical recovery state and tool baseline, export must produce the same substantive Candidate Development Baseline apart from explicitly non-semantic metadata where unavoidable.

### Rationale

Export must remain reproducible even when developers use ordinary IDEs/editors against the workspace materialization. Treating canonical recovery state as the export source preserves deterministic replay and gives export a clear purpose: producing an independent development baseline rather than copying LegacyRevive.NET's internal recovery workspace.

### Consequences

- `.legacyrevive` internal state is not part of the exported development baseline.
- Exported solution/project/source/report artifacts are generated/materialized from canonical committed recovery state.
- Export metadata should identify the recovery state/checkpoint from which the baseline was produced.
- A later export from materially changed recovery state may legitimately differ.

### Supersedes

None

### Superseded by

None

---

## D165 — Manual Materialized-Source Changes Require Explicit Adoption

Status: Accepted  
Date: 2026-09-26  
Origin: Developer workflow discussion; `09-source-reconstruction.md`; SRC-012  
Related qualifications: SRC-012

### Decision

A manual change made by an IDE/editor to materialized recovered source does not automatically change canonical recovery state and does not automatically become Developer-Adjusted Source.

LegacyRevive.NET treats the difference first as working-tree divergence. If the developer explicitly adopts the change through a committed Developer Intervention, the adopted source becomes provenance-bearing Developer-Adjusted Source and may supersede the applicable current source reconstruction. If the developer explicitly discards the change, LegacyRevive.NET rematerializes the canonical source representation and the unadopted edit does not enter canonical recovery state.

### Rationale

Developers need freedom to experiment, refactor, format, debug, and edit reconstructed code using normal development tools without every filesystem mutation silently becoming a recovery claim or intervention. Explicit adoption preserves provenance and intent while supporting a natural IDE/editor workflow.

### Consequences

- Filesystem mutation alone is not Developer Input, Evidence, or a committed intervention.
- Adoption must record the appropriate Developer Intervention and source provenance.
- Discard is an explicit working-tree operation and does not reinterpret the abandoned edit as historical evidence.
- LegacyRevive.NET can coexist with arbitrary IDEs/editors without requiring a proprietary source-editing surface.

### Supersedes

None

### Superseded by

None

---

## D166 — Unresolved Working-Tree Divergence Blocks Export

Status: Accepted  
Date: 2026-09-26  
Origin: Export/divergence design discussion; `14-mvp.md`; MVP-018  
Related qualifications: MVP-018

### Decision

Before export, LegacyRevive.NET verifies the materialized reconstructed working tree against the canonical recovery state relevant to that materialization. Material unresolved divergence blocks export.

When divergence is detected, `export` reports the affected paths/items and does not silently adopt, discard, overwrite, or export those divergent working-tree changes. The developer resolves divergence through the explicit adopt/discard workflow and may then rerun export.

### Rationale

An export command should be deterministic and automation-safe rather than an implicit mutation/intervention workflow. Blocking on unresolved divergence protects developer edits from silent loss and protects canonical export from accidental incorporation of uncommitted working changes.

### Consequences

- `export` remains a materialization operation, not an interactive source-adoption mechanism.
- Divergence reporting must be sufficiently actionable for the developer to identify what requires resolution.
- Ordinary workspace operations may detect/report divergence earlier; export is the mandatory safety gate before baseline materialization.
- CI/non-interactive export cannot hang waiting for an adopt/discard prompt.

### Supersedes

None

### Superseded by

None

---

## D167 — MVP Original Artifacts Use Workspace-Owned Immutable Byte Snapshots

Status: Accepted  
Date: 2026-09-27  
Origin: Original-artifact preservation resolution; `12-architecture.md`; ARCH-015  
Related qualifications: ARCH-015

### Decision

For the MVP, every supplied Original Artifact that is successfully preserved during intake has its exact bytes snapshotted into workspace-owned immutable artifact storage beneath the reserved `.legacyrevive` boundary before preservation is considered complete.

After successful preservation, subsequent recovery and replay operate against the preserved workspace-owned bytes rather than depending on continued availability or immutability of the original external path. The original supplied path/name and capture context remain provenance metadata. SHA-256 establishes content identity/integrity and verifies the preserved representation, but a hash does not substitute for possession of the bytes.

A failed or incomplete snapshot is not treated as successfully preserved and must remain explicit through scoped diagnostics/partial or blocked intake as applicable. Preserved Original Artifact bytes are never modified in place by reconstruction. A later materially different supplied artifact enters through normal intake as new recovery state. Equal content hashes do not by themselves merge distinct Artifact identities or supplied provenance.

Large artifact bytes remain filesystem/artifact-store backed rather than SQLite BLOBs. The exact internal artifact-store path/layout, compression, content-addressing, deduplication, reflink/hardlink optimizations, and similar physical optimizations remain implementation choices provided the canonical immutability, availability, identity, and provenance semantics are preserved.

### Rationale

A persistent resumable Recovery Workspace and replayable checkpoints are stronger when the workspace actually possesses the Original Artifact bytes on which recovery depends. External path plus hash can detect mutation but cannot guarantee later availability. Workspace-owned snapshots make recovery resilient to source deletion/mutation, make the workspace portable as a durable recovery context, and align artifact ownership with the `.legacyrevive` internal-state boundary while preserving the logical Original Artifact Store separation.

The storage-duplication cost is accepted for the MVP because it is an operational cost rather than an epistemic/replay weakness. More advanced shared or externally managed immutable stores can be considered later through separate governance if corpus/product evidence justifies them.

### Consequences

- `.legacyrevive` owns the durable preserved-byte representation for successfully admitted MVP Original Artifacts.
- Original external paths remain provenance/capture context after successful intake and are not replay dependencies.
- Intake persistence must not commit a successful-preservation state until byte snapshot and integrity verification have succeeded.
- SHA-256, ArtifactId, supplied path/provenance, and physical storage location remain distinct concepts.
- Physical deduplication may reuse storage for identical bytes only if separate Artifact identities and provenance remain intact.
- Reconstruction/materialization never overwrites preserved Original Artifact bytes.
- A future MVP/post-MVP mode based on qualifying external immutable references requires separate governed design and must not silently weaken this decision.

### Supersedes

None

### Superseded by

None


---

## D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications

Status: Accepted  
Date: 2026-09-27  
Origin: `91-design-qualification-register.md`; DEV-001 in-review design conclusion  
Related qualifications: DEV-001

### Decision

LegacyRevive.NET adopts a canonical implementation-development process, owned by `16-development-process.md`, for translating accepted canonical requirements into bounded implementation work and objective verification before that work is considered complete.

Each material, acceptance-bearing Development Slice is governed by a durable, non-canonical **Development Slice Specification** created before production implementation begins. Development Slice Specifications use stable `DEV-SPEC-<number>` identities and record the slice's canonical basis, included/excluded scope, observable acceptance contract, representative verification scenarios, executable acceptance verification, supporting lower-level verification, implementation-discovered qualifications, lifecycle state, verification state, and durable lifecycle history.

Development Slice Specifications are subordinate to the canonical Project documents: they apply accepted requirements to a particular implementation increment but do not create, replace, or reinterpret canonical product semantics. When implementation exposes a material ambiguity, conflict, unsupported assumption, boundary issue, or challenge to accepted direction, that issue returns to the normative qualification process in `91-design-qualification-register.md` rather than being silently decided in code or tests.

The acceptance contract becomes the implementation baseline when a Development Slice Specification reaches `Ready for Implementation`. It must not be weakened merely to make an implementation pass. A slice reaches `Implemented` only after its required acceptance verification and supporting completion conditions are satisfied. Completed specifications are retained as durable implementation history; materially replacing work uses a new specification linked by supersession rather than rewriting the earlier record.

Non-semantic, non-acceptance-bearing maintenance such as formatting, typo-only correction, or behavior-preserving internal refactoring does not require a Development Slice Specification merely for process formality.

### Rationale

LegacyRevive.NET's canonical model contains strong requirements around evidence boundaries, provenance, uncertainty, replay, immutability, deterministic behavior, developer intervention, and non-fabrication. A codebase can compile and lower-level tests can pass while still violating those semantics.

A durable requirement-to-acceptance development process provides a stable bridge between canonical design and implementation, reduces reliance on exploratory chat or developer memory, forces meaningful scope and acceptance behavior to be explicit before coding, exposes unresolved design questions before they become accidental implementation choices, and preserves why a completed implementation increment was considered conformant.

The process remains subordinate to the existing design-governance system so it strengthens implementation discipline without creating a competing source of canonical truth.

### Consequences

- `16-development-process.md` becomes the canonical owner of the implementation-development process and the normative Development Slice Specification model; `00-ai-context.md` must be synchronized to include that ownership once the DEV-001 governance lifecycle completes.
- `90-decisions.md` and `91-design-qualification-register.md` remain the owners of durable decisions and qualification governance respectively; Development Slice Specifications do not replace them.
- Material Development Slice Specifications use stable `DEV-SPEC-<three-digit-number>` identities and live in the dedicated non-canonical repository area defined by `16-development-process.md`.
- Slice-level acceptance propositions are derived from canonical requirements and should constrain implementation through executable verification established before or alongside production implementation.
- Lower-level unit/integration/persistence/filesystem/component/determinism tests support design confidence and diagnosis but do not substitute for slice-level acceptance verification.
- `13-tooling-and-dependencies.md` continues to own testing technology choices; this decision governs development discipline, not test-framework selection.
- Blocking, lifecycle phase, and supersession remain distinct concerns in Development Slice Specification metadata rather than being collapsed into one status field.
- Each Development Slice Specification retains a lightweight lifecycle history of material state transitions and reasons; ordinary edit history remains source-control history.
- An `Implemented` specification is historically stable. Material replacement creates a later specification and records supersession rather than rewriting prior implementation history.
- The first intended application after canonicalization is `DEV-SPEC-001 — Workspace + Local Artifact Intake`, applying already accepted intake/preservation requirements rather than creating new recovery semantics.

### Supersedes

None

### Superseded by

None

---


## D169 — LegacyRevive.NET Repository Solution Is `LegacyRevive.Net.slnx`

Status: Accepted  
Date: 2026-09-27  
Origin: Repository solution rename completed by the developer; TOOL-017 governance synchronization  
Related qualifications: TOOL-017

### Decision

LegacyRevive.NET retains **SLNX** as the canonical solution-file format for its own development repository, and the canonical root solution filename is now:

```text
LegacyRevive.Net.slnx
```

This repository convention applies only to the LegacyRevive.NET codebase. It does not require recovered Candidate Solutions to use `.slnx` or the LegacyRevive.NET repository naming convention; reconstructed solution representation remains governed by Project Reconstruction, MVP/output requirements, and the applicable recovered target/tooling context.

### Rationale

The physical repository solution has been deliberately renamed from `LegacyRevive.slnx` to `LegacyRevive.Net.slnx`. The canonical documentation must describe the actual current repository convention rather than retain a filename that no longer exists.

The rename does not challenge the substantive SLNX format choice established by D162. This decision therefore carries that format direction forward while replacing the obsolete filename and preserving the superseded decision as historical governance record.

### Consequences

- The root development solution is named `LegacyRevive.Net.slnx`.
- Repository scaffolding, documentation, CI examples, and implementation guidance should use `LegacyRevive.Net.slnx` when referring to LegacyRevive.NET's own solution.
- SLNX remains the canonical repository solution-file format.
- Candidate Solution generation remains independent and may use `.sln`, `.slnx`, or another supported representation where justified by the recovery model and target/tooling context.
- D162 remains preserved as superseded history; its former `LegacyRevive.slnx` filename must not be treated as current canonical state.
- A future material change to either the repository solution format or canonical root solution filename must be governed and synchronized rather than introduced as silent documentation drift.

### Supersedes

D162

### Superseded by

None

---

## D170 — Intake Discovery Failure Preserves Known State Without Fabricating Artifacts

Status: Accepted  
Date: 2026-09-28  
Origin: `91-design-qualification-register.md`; PROC-010 governance resolution  
Related qualifications: PROC-010

### Decision

Stage 01 distinguishes supplied-source discovery from preservation of an already discovered Artifact.

LegacyRevive.NET creates an Artifact identity only for a supplied file that has actually been discovered and admitted as an artifact instance. Failure to enumerate a supplied directory or subtree must not fabricate Artifact identities or file-existence claims for contents that were never discovered.

When the supplied root cannot be enumerated sufficiently to identify a trustworthy supplied artifact set, the Stage 01 discovery scope is Blocked and no Recovery Intake Snapshot is produced as though the source had been successfully enumerated. A failed root enumeration must not be represented as a successfully identified empty artifact set. A Recovery Run and scoped discovery diagnostic may still be persisted where the workspace/run infrastructure has been established.

When a narrower subtree cannot be enumerated but other files have been safely discovered, LegacyRevive.NET retains the trustworthy discovered state where safe. The affected discovery scope is represented as Blocked through a structured intake/discovery diagnostic, while the overall Recovery Run may be Partial.

A Recovery Intake Snapshot produced from incomplete directory discovery represents the artifact instances actually admitted to that snapshot and must explicitly retain that supplied-source discovery was incomplete together with the applicable failed scope/diagnostic context. It must not imply that the admitted Artifact set is proven to be the complete contents of the supplied directory tree.

A file that was individually discovered and admitted but later becomes unreadable or fails byte preservation remains an Artifact-scoped preservation failure under the ordinary preservation model. That condition is distinct from inability to discover possible material beneath an unenumerable source scope.

### Rationale

LegacyRevive.NET must preserve safe partial recovery without converting missing discovery information into invented artifacts or false completeness.

The distinction applies the established Partial/Blocked and scoped-diagnostic model to Stage 01 while preserving provenance, replayability, and the epistemic boundary between what was actually discovered and what remains unknown. It also prevents a root-enumeration failure from becoming observationally equivalent to a successfully enumerated empty supplied set.

### Consequences

- supplied-source discovery requires structured diagnostics independent of Artifact identity;
- Stage 01 state must record whether supplied-directory discovery completed whenever a Recovery Intake Snapshot is produced;
- failed root enumeration cannot masquerade as a successful empty intake;
- safe sibling/subtree progress may be retained when another subtree fails;
- incomplete Recovery Intake Snapshots expose their discovery incompleteness persistently and replayably;
- no Artifact identity is generated for undiscovered possible content;
- already-discovered files continue through the existing Artifact admission/preservation model;
- diagnostics use the narrowest reliably established source/directory/subtree scope when no Artifact exists;
- MVP implementation must cover root and subtree discovery failure without requiring a richer filesystem-capture ontology;
- D134's requirement to inventory every supplied file is interpreted at the discovery boundary as every supplied file actually discovered by intake; inability to enumerate possible additional contents remains explicit rather than being treated as either inventory success or historical absence.

### Supersedes

None

### Superseded by

None

---


## D171 — Positive Stage 01 File Discovery Survives Later Descendant-Discovery Failure

Status: Accepted  
Date: 2026-09-29  
Origin: `91-design-qualification-register.md`; PROC-011 governance resolution  
Related qualifications: PROC-011

### Decision

When Stage 01 has successfully and completely identified one or more supplied files within a known directory, those positive file discoveries remain trustworthy discovered state even if a subsequent discovery operation for possible child-directory or descendant scopes from that same directory fails.

A later descendant-discovery failure does not retroactively invalidate positively established file discoveries merely because the implementation performed both operations while processing the same directory. The discovered files remain eligible for ordinary Artifact admission and preservation.

The failed descendant-discovery operation makes supplied-source discovery incomplete for the affected scope. The failure is represented at the narrowest reliably established non-Artifact source/directory/subtree scope, the affected scope is Blocked, and the overall Recovery Run may be Partial when trustworthy discovered state is retained. Any Recovery Intake Snapshot represents only the actually admitted Artifact instances and explicitly records incomplete discovery; it does not claim that unknown descendants are absent or that the admitted set is complete.

This rule also applies when the known directory being processed is the supplied root, provided positive file discovery has already established a trustworthy supplied Artifact subset. This condition is distinct from total root discovery failure in which the root cannot be enumerated sufficiently to establish any trustworthy supplied Artifact set.

Successful enumeration that establishes no direct files does not by itself establish a trustworthy non-empty or empty supplied Artifact subset when subsequent descendant discovery fails. An incomplete empty snapshot is therefore not justified solely by a zero-file direct enumeration; the state remains Blocked unless other trustworthy discovered Artifact state has been established.

The decision does not require directory Artifact identities, a canonical filesystem graph, filesystem journaling, transactional source snapshots, or claims about the number, names, types, or historical existence of undiscovered descendants.

### Rationale

Stage 01 discovery is literal and evidence-disciplined. A completed positive discovery establishes concrete supplied file instances. A later failure to discover different possible descendant scopes establishes uncertainty about those descendants, not a reason to erase already established positive state.

Treating both operations as one atomic semantic unit would make canonical recovery state depend on incidental traversal/API grouping and would discard trustworthy partial progress without a canonical justification. Retaining positive state while marking descendant discovery incomplete applies D170's partial-progress rule at the narrowest reliable boundary without fabricating Artifacts or overstating completeness.

The zero-direct-file case remains distinct because successful observation of zero direct files does not establish whether undiscovered descendant files exist; without another positively discovered Artifact subset there is no trustworthy admitted set from which to form a Partial intake snapshot.

### Consequences

- Stage 01 implementations must commit positively established file discoveries independently of later descendant-discovery success for the same known directory.
- A later child-directory/descendant enumeration failure must not discard those discovered files.
- When such positive state survives, the run may be Partial and an incomplete Recovery Intake Snapshot may be produced for the admitted Artifact set.
- Total root discovery failure remains Blocked without a Recovery Intake Snapshot when no trustworthy supplied Artifact subset has been established.
- Diagnostic scope must describe the failed descendant-discovery boundary rather than falsely implying that successfully discovered files failed discovery.
- Persistence/checkpoint/replay must retain the admitted Artifact set, incomplete-discovery state, and applicable failed-scope diagnostics.
- Implementation details may use internal traversal structures, but they do not become canonical filesystem entities.
- The post-implementation issue discovered after DEV-SPEC-002 requires a new bounded Development Slice; DEV-SPEC-002 remains unchanged as historical implementation/verification record.

### Supersedes

None

### Superseded by

None

---

## D172 — Stage 02 Artifact Classification Uses Direct Format-Specific Recognition Contracts

Status: Accepted  
Date: 2026-09-30  
Origin: `91-design-qualification-register.md`; RMAT-009 governance resolution  
Related qualifications: RMAT-009

### Decision

Stage 02 Artifact format/family classification is governed by canonical, directly observable recognition contracts owned by `06-recovery-matrix.md`, with process/output boundaries owned by `07-recovery-process.md`.

A classification may be asserted only when the preserved Artifact instance satisfies the required structural conditions for that format/family. Filename, extension, relative path, and deployment position may select probes or corroborate a result, but they do not by themselves establish actual format/family.

Stage 02 records the most specific directly established classification. Where only a broader format is established, the broader result remains current. A valid known format outside current MVP deep-analysis support is `valid-but-unsupported`, not malformed. Malformed/corrupt status requires content-level evidence of a known-format candidate plus failure of that format's required structure; operational/tool/resource/cancellation failure remains distinct from content invalidity.

Where directly established classifications form a compatible hierarchy, the most specific established subtype wins. Where materially incompatible family contracts remain independently satisfied, the result remains explicit ambiguity rather than being selected by recognizer order, extension, or convenience.

Stage 02 may inspect internal Artifact structure only as far as necessary to evaluate the recognition contract and persists only classification-bearing state and classification diagnostics. Reusable direct semantic content is extracted and normalized in Stage 03.

The MVP-relevant minimum recognition contracts are defined in `06-recovery-matrix.md` §5A, including managed CLI artifacts/assemblies, reference assemblies, satellite/resource assemblies, managed implementation assemblies, native/non-managed PE, classic XML configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, and `.nupkg`.

### Rationale

D047 and D048 already require Artifact-instance inspection and prohibit extension-only classification, while D054/D055 establish the Stage-oriented process and preserve epistemic categories. Those rules did not determine the format-specific truth conditions needed to classify real artifacts. The rejected DEV-SPEC-004 attempt demonstrated that leaving those thresholds implicit delegates product semantics to implementation.

Direct recognition contracts make the classification claim falsifiable, deterministic, testable, and consistent with the Evidence Model. They also prevent two opposite errors: forcing valid but unsupported artifacts into a malformed state, and accepting weak names/property-presence signals as though they established a specific format.

Separating Stage 02 structural inspection from Stage 03 semantic extraction permits whatever inspection is technically necessary to identify the format without allowing parser convenience to blur canonical stage outputs.

### Consequences

- `06-recovery-matrix.md` owns family-specific recognition truth conditions in addition to artifact-family capability/limits.
- `07-recovery-process.md` owns Stage 02 classification workflow and the Stage 02 / Stage 03 inspection-output boundary.
- Stage 02 implementations must expose broader established, valid-but-unsupported, unsupported/unrecognized, ambiguous, malformed/corrupt-candidate, and operational-failure outcomes where applicable.
- A valid managed CLI module/netmodule without an Assembly manifest is valid-but-unsupported for the MVP rather than malformed.
- Reference-assembly recognition requires the fully qualified assembly-level `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`; simple attribute name matching is insufficient.
- Satellite/resource classification requires resource-oriented managed-assembly structure and non-neutral culture; culture or `.resources.dll` naming alone is insufficient.
- `.runtimeconfig.json` recognition requires a JSON object whose `runtimeOptions` member is itself an object.
- `.deps.json` recognition requires the required dependency-context root sections with the canonical container shapes, including string `runtimeTarget.name`.
- Portable PDB recognition requires standalone Portable-PDB metadata structure including `#Pdb` and `#~` streams rather than `.pdb` naming or a generic metadata signature alone.
- NuGet container/manifest recognition is based on container/XML structure rather than extension.
- Tests verify these contracts but do not define them.
- The replacement Stage 02 Development Slice Specification must derive its acceptance propositions from these canonical semantics rather than recreate them.

### Supersedes

None

### Superseded by

None

---


## D173 — Development Slices Require Semantic Readiness Before Implementation

Status: Accepted  
Date: 2026-09-30  
Origin: `91-design-qualification-register.md`; DEV-002 governance resolution  
Related qualifications: DEV-002

### Decision

A material Development Slice Specification may enter `Ready for Implementation` only when its acceptance-bearing obligations are semantically determinate enough that implementation can choose mechanics without having to originate materially significant product/design semantics.

The readiness review applies a material semantic choice test: if two implementations could make materially different semantic choices and both plausibly claim that the current canonical basis and acceptance wording permit them because the authoritative semantics do not determine the choice, the slice is not ready. The missing semantic issue must be registered in `91-design-qualification-register.md` and resolved by the applicable canonical owner before implementation proceeds.

Material semantic choices include, where relevant, truth conditions, classification/evidence thresholds, state meanings, failure-category boundaries, domain invariants, precedence/ambiguity/conflict rules, persistence/history semantics, provenance or epistemic categorization, and other externally or developer-visible behavior whose alternatives would change what the product claims, preserves, reports, accepts, rejects, or leaves unresolved.

Multiple implementation approaches remain permitted when they preserve the same accepted semantics and differ only in implementation mechanics. Tests and DEV-SPEC acceptance wording demonstrate or operationalize accepted semantics; they do not create missing canonical semantics.

Every material DEV-SPEC records a concise readiness assessment before entering `Ready for Implementation`, including the canonical semantic basis, any material semantic qualification blockers, implementation discretion intentionally left open, and a readiness result of `Pending`, `Blocked`, or `Pass`.

### Rationale

D168 already established that canonical documents own product/design meaning and that implementation, tests, and DEV-SPECs cannot silently redefine it. The rejected DEV-SPEC-004 attempt showed that this authority boundary alone was not a sufficient readiness detection mechanism: a specification could appear concrete, testable, and complete while still leaving materially significant truth conditions to implementation choice.

RMAT-009/D172 supplied the concrete example. Stage 02 classification wording such as “most specific directly supportable family” was not enough until the canonical recognition contracts defined what observations actually establish each family, what remains broader or unsupported, and how malformed, ambiguous, overlapping, and operational-failure states differ.

A semantic-readiness gate therefore checks whether the design is sufficiently determined before delegation, while preserving legitimate implementation freedom and avoiding unnecessary canonicalization of private mechanics.

### Consequences

- `16-development-process.md` owns the semantic-readiness gate and readiness-assessment discipline.
- `Ready for Implementation` requires semantic-readiness `Pass`; a merely testable acceptance contract is insufficient if tests would have to choose unresolved semantics.
- A material semantic choice discovered before implementation keeps/returns the DEV-SPEC to `Draft` and is routed through `91`.
- A material semantic choice discovered during implementation follows the existing qualification/blocking flow and must complete governance before the slice can be completed.
- Definition of Done requires confirmation that implementation did not silently supply canonical semantics that should have been governed first.
- `AGENTS.md` mirrors the rule for coding agents but does not own it.
- Private decomposition, helper design, parser composition, buffering/streaming, internal naming, local algorithms/data structures, and incidental storage mechanics remain implementation choices when they do not change accepted semantics.
- Historical implemented DEV-SPECs are not rewritten merely to add the later readiness-assessment section.

### Supersedes

None

### Superseded by

None

---

## D174 — Rejected DEV-SPECs Preserve Unsound Implemented Completion Judgments

Status: Accepted  
Date: 2026-09-30  
Origin: `91-design-qualification-register.md`; DEV-003 governance resolution  
Related qualifications: DEV-003

### Decision

The Development Slice Specification lifecycle includes a terminal `Rejected` status for the narrow case where a specification previously reached `Implemented`, but later deliberate review establishes that the completion/acceptance judgment itself was materially unsound and the implementation is no longer accepted as a valid completed slice.

The legal transition is `Implemented → Rejected`. The earlier `Implemented` transition remains in lifecycle history; rejection is appended with its reason and governing qualification/decision rather than rewriting history.

`Rejected` remains distinct from:

- failed verification before `Implemented`, which normally returns `Verification Pending → In Progress`;
- abandonment before valid completion, which uses `Withdrawn`;
- later replacement of a valid implemented slice, which uses `Superseded`; and
- ordinary source-control history.

Source-control commit is not a universal canonical lifecycle gate. However, all explicit slice-specific completion conditions that are consistent with the canonical process are binding for that slice. If a specification is later found to have reached `Implemented` without satisfying such a condition, that may contribute to a governed rejection of the completion judgment.

A future implementation that replaces the intended scope of a `Rejected` specification uses a new DEV-SPEC. Supersession fields may preserve replacement lineage, but the earlier specification remains `Rejected` because that status records the disposition of its own completion judgment.

### Rationale

DEV-SPEC-004 demonstrated a lifecycle condition not represented by D168's original model. Its tests and stated verification passed and the document transitioned to `Implemented`, but subsequent review established that its acceptance/design basis had delegated materially significant Stage 02 recognition semantics to implementation. The implementation was rejected and the repository was restored to the accepted post-DEV-SPEC-003 baseline. In addition, DEV-SPEC-004's own completion conditions required the implementation to be committed as a coherent Development Slice, which had not occurred.

Treating the specification as still `Implemented` would falsely present the rejected implementation as a current valid completion record. Deleting or rewriting the historical `Implemented` transition would destroy development history. `Withdrawn` would conflate post-completion invalidation with abandonment, while `Superseded` would imply that a valid implementation was merely replaced.

A dedicated rejection transition preserves both historical facts: the completion judgment occurred, and that judgment was later deliberately invalidated.

### Consequences

- `16-development-process.md` adds `Rejected` as a terminal DEV-SPEC lifecycle status and permits `Implemented → Rejected`.
- Lifecycle history must preserve both the earlier `Implemented` transition and the later rejection.
- Rejection requires a deliberate, recorded basis; it must not be used merely because a later implementation is preferred.
- Definition of Done applies both canonical completion conditions and explicit compatible slice-specific completion conditions.
- Source-control commit remains separate from lifecycle by default and does not become a universal completion gate.
- DEV-SPEC-004 is retained with current status `Rejected`; its historical verification and lifecycle events remain visible, while its implementation is not presented as accepted.
- A replacement Stage 02 slice must be a new DEV-SPEC and derive its semantics from RMAT-009/D172 and semantic readiness from DEV-002/D173.

### Supersedes

None

### Superseded by

None

---

## D175 — Malformed Family Claims Require Direct Structural Candidate Discriminators

Status: Accepted  
Date: 2026-10-01  
Origin: `91-design-qualification-register.md`; RMAT-010 governance resolution  
Related qualifications: RMAT-010

### Decision

A Stage 02 `Malformed/corrupt candidate` claim for a known Artifact family requires direct structural evidence of a family-specific **candidate discriminator** that can be established independently of the structural condition that failed. The discriminator must be an exact format-level token, record, relationship, or structure observed at the canonical scope required by that family; it cannot be supplied by filename/path, raw substring occurrence, partial name collision, incidental embedded text, generic parser rejection, or tool-specific error categorization.

Deterministic partial structural/token inspection is permitted when complete parsing cannot succeed, provided it establishes the canonical discriminator rather than relying on parser convenience. Where the discriminator is not directly established, Stage 02 retains a broader valid format if one is established, otherwise preserves unsupported/unrecognized content and diagnostics rather than fabricating family membership.

`06-recovery-matrix.md` §5A.5.1 defines the current MVP family-specific candidate thresholds. In particular:

- managed CLI candidacy requires valid PE/COFF structure plus an established CLR/CLI header before malformed CLI metadata can support malformed managed state;
- malformed XML family candidacy requires token-aware evidence at document-element/direct-child scope as applicable, not textual prefix matching;
- malformed JSON family candidacy requires token-aware root/top-level member structure, not raw property-name occurrence;
- Portable-PDB candidacy requires structurally identified Portable-PDB-specific metadata-stream evidence, not generic metadata or raw stream-name bytes;
- NuSpec candidacy requires `package` document structure plus direct `metadata`; generic package XML or `.nuspec` naming is insufficient; and
- malformed NuGet-package candidacy for a corrupt container requires structured evidence of a root NuSpec entry plus manifest bytes that independently establish NuSpec candidacy. A readable ZIP without a successfully recognized NuSpec remains valid non-NuGet ZIP/unsupported under D172.

### Rationale

D172 established successful recognition contracts and required content-level family evidence for malformed/corrupt outcomes, but post-implementation review of rejected DEV-SPEC-005 showed that phrases such as `content-level candidate` and `family-specific structure is otherwise established` still permitted materially different thresholds. Loose substring checks could overstate malformed-family membership even while passing representative tests.

Because malformed-family status is itself a product claim about Original Artifact bytes, it requires the same evidence discipline as successful recognition. A direct structural discriminator preserves useful malformed-family information where it is genuinely observable while preventing weak names, incidental text, parser behavior, or corrupt-container guesses from being promoted into unsupported classification claims.

### Consequences

- `06-recovery-matrix.md` owns both successful family-recognition truth conditions and malformed/corrupt family-candidate evidence thresholds.
- `07-recovery-process.md` permits deterministic partial structural inspection only to establish those governed candidate discriminators.
- Implementations may choose parsers, tokenizers, buffering, and helper decomposition, but equivalent structural evidence must map to equivalent canonical candidate outcomes.
- Raw substring/prefix searches are not sufficient evidence for malformed XML/JSON/metadata/package family claims.
- Parser rejection remains diagnostic unless the family discriminator and deterministic content invalidity are independently established.
- A broader valid format takes precedence over a weaker malformed-subtype guess when subtype candidacy is not directly established.
- DEV-004 may now define verification discipline against these settled semantic thresholds; tests must demonstrate D175 rather than invent a threshold.
- The replacement Stage 02 DEV-SPEC remains blocked by DEV-004 until its separate development-process qualification is resolved.

### Supersedes

None

### Superseded by

None

---

# 6. How to add future decisions

A new decision should be added when a material open qualification is deliberately resolved or when a new durable design direction is accepted.

When adding a decision:

1. assign the next unused `DNNN` ID;
2. record its origin;
3. link relevant qualification IDs;
4. describe the decision;
5. record why it was chosen;
6. state the consequences;
7. update the qualification register if the decision resolves an open qualification;
8. update affected canonical documents.

Do not renumber existing decision IDs.

---

# 7. Relationship to audits

Audits may identify:

- unsupported assumptions;
- document-boundary problems;
- unresolved design questions;
- contradictions;
- premature commitments.

Not every audit correction requires a durable decision entry.

A decision belongs here only when the resolution establishes a project rule or direction that future work should continue to respect.

Resolved `DOC-*` audit findings that require only document correction remain recorded in `91-design-qualification-register.md`; their individual corrections are preserved there rather than duplicated as durable decisions.

Where those corrections exposed broader durable rules, those rules are represented here through decisions such as:

- D016 — Canonical Documents Have Defined Ownership;
- D017 — Material Qualifications Must Be Tracked Centrally;
- D018 — Resolved Qualifications Remain in the Register.

---

# 8. Relationship with evidence-model qualifications

`EVID-001` through `EVID-010` were resolved by `03-evidence-model.md`.

Their durable semantic consequences are recorded in:

- D023 — Recovery Input Separates Evidence from Developer Input;
- D024 — Evidence Conclusions Evolve by Supersession, Not Reclassification In Place;
- D025 — Conflicting Evidence Is Explicit and History-Preserving;
- D026 — Provenance Includes Derivation Lineage;
- D027 — Confidence Is Semantic and Applies Only to Inferential Conclusions;
- D028 — Assumptions Have an Explicit Lifecycle;
- D029 — Developer Corrections Supersede and Trigger Downstream Reevaluation;
- D030 — Support Dependencies Must Prevent Circular Corroboration.

The resolved qualification history remains authoritative in `91-design-qualification-register.md`.

Future work must not reinterpret those resolutions silently. A material challenge must create a new qualification and, where necessary, a superseding decision.
---

# 9. Governance lifecycle participation

`90-decisions.md` participates in the normative governance lifecycle owned by `91-design-qualification-register.md`.

A new durable decision should normally arise only after a material qualification or design issue has been explicitly considered.

The required sequence is:

```text
material issue discovered
        ↓
qualification registered in 91
        ↓
design work produces proposed resolution
        ↓
does resolution establish durable project direction?
        │
        ├── No
        │     ↓
        │   no decision entry required
        │
        └── Yes
              ↓
        add new decision to 90
              ↓
        link decision ID from qualification in 91
              ↓
        update affected canonical document(s)
              ↓
        audit updated canonical document(s)
              ↓
        when governance consistency is confirmed,
        record final resolution in 91
        and mark qualification Resolved
```

This file does not own qualification status transitions.

`91-design-qualification-register.md` is the single source of truth for qualification lifecycle and status.

---

# 10. Governance synchronization rule

A governance change is not complete merely because a decision was added to this file.

Where a decision resolves or materially affects a qualification, the following artifacts must agree before the change is considered complete:

```text
90-decisions.md
↔
91-design-qualification-register.md
↔
affected canonical document(s)
↔
audit result(s)
```

At minimum:

1. `90` must contain the durable decision where one is required;
2. `91` must reference that decision from the relevant qualification;
3. the affected canonical document must contain the accepted design definition;
4. the audit must confirm consistency;
5. only then may the qualification be marked `Resolved`.

Temporary inconsistency may exist while edits are being made, but the governance update is not complete until this synchronization check passes.

---

# 11. Closing principle

The decision log should preserve deliberate project choices without pretending that unresolved design questions have already been answered.

> **Qualifications record what still needs to be settled. Decisions record what has actually been settled.**

---

## D176 — Recognition/Classifiers Require Boundary-Falsification Verification

Status: Accepted  
Date: 2026-10-01  
Origin: `91-design-qualification-register.md`; DEV-004 governance resolution  
Related qualifications: DEV-004

### Decision

Material Development Slice Specifications whose correctness depends on recognition, classification, evidence thresholds, parser-sensitive family identification, or comparable accept/reject predicates must define an explicit boundary/falsification verification matrix before entering `Ready for Implementation`.

For each material positive recognition predicate, the matrix must identify the governed sufficient evidence and include at least one adversarial near-match demonstrating that materially weaker or misleading evidence does not satisfy the predicate. Where applicable, the matrix must also cover independently material missing-condition, lookalike, malformed-candidate, broader/other-format, incidental-token/name collision, container/subtype, overlap/ambiguity, operational-failure, and recognizer-order boundaries.

The matrix derives expected outcomes from canonical semantics; it does not create them. If a required boundary outcome cannot be determined from the canonical basis without choosing a material semantic interpretation, the DEV-SPEC fails semantic readiness under D173 and the question must be governed before implementation.

Completion is risk-based rather than combinatorially exhaustive. `Implemented` requires executable verification of the materially applicable matrix rows, including the required adversarial near-match for every positive material predicate, with any `Not applicable` category explicitly justified. Supporting tests may diagnose components but cannot substitute for acceptance-level demonstration of the governed boundary.

### Rationale

DEV-SPEC-005 demonstrated that a semantically ready specification and a fully green executable suite can still miss material false-positive recognition behavior when tests exercise representative examples but do not systematically attempt to falsify the recognition predicates. This is especially important in LegacyRevive.NET because an incorrect classifier can overstate what preserved Original Artifact bytes actually establish.

D173 answers whether the meaning of the expected result is governed. D176 answers whether implementation verification has demonstrated both the sufficient and insufficient sides of that governed result. Keeping those concerns separate prevents tests from inventing semantics while making boundary coverage deliberate rather than discretionary.

### Consequences

- `16-development-process.md` owns the boundary/falsification verification discipline for applicable Development Slices.
- Applicable DEV-SPECs include the matrix before `Ready for Implementation`; it is part of the implementation baseline.
- Every positive material recognition predicate requires at least one adversarial near-match.
- Boundary categories are considered where applicable, with explicit rationale when a category is genuinely not applicable.
- Final Definition-of-Done review inspects matrix-to-test coverage and cannot infer completion merely from aggregate green test counts.
- Parser/library defaults may implement mechanics but do not define LegacyRevive.NET recognition outcomes.
- The obligation is deliberately risk-based and does not require exhaustive malformed-input enumeration.
- DEV-SPEC-006 must apply this rule to the full replacement Stage 02 classification slice.
- `AGENTS.md` is synchronized so coding agents check applicable boundary/falsification coverage before implementation and completion.

### Supersedes

None

### Superseded by

None

