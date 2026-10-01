# LegacyRevive.NET — Design Qualification Register

## Status

**Canonical project register**

This document is the single source of truth for unresolved and resolved design qualifications discovered while defining LegacyRevive.NET.

It exists to prevent important qualifications, audit findings, ambiguities, assumptions, conflicts, deferred decisions, or unresolved design questions from being lost as work moves between documents and conversations.

This file should remain in the Project source throughout the lifetime of the design effort.

---

# 1. Purpose

A **design qualification** is any material point that prevents a design statement, document, model, or decision from being treated as fully settled, or that required deliberate correction before it could be treated as settled.

A qualification may be:

- an unresolved design question;
- an ambiguity;
- an assumption requiring validation;
- a deferred decision;
- a boundary question;
- an implementation dependency;
- a conflict;
- a validation requirement;
- an audit finding;
- an issue that must be resolved before a document can become fully canonical;
- a previously identified issue that has since been resolved.

This register therefore preserves both:

- **current unresolved qualifications**, and
- **resolved qualification history**.

---

# 2. Governing rule

> **No material qualification should disappear merely because work moves to another document or conversation.**

When a qualification is discovered:

1. create an entry in this register;
2. assign it a stable ID;
3. record where it came from;
4. identify which document or decision owns the resolution;
5. keep it open until deliberately resolved;
6. when resolved, retain the entry and record the resolution rather than deleting it.

A qualification discovered and fixed during the same audit cycle should still be recorded as **Resolved** if it was material enough to affect the canonical document.

---

# 3. Scope

This register covers qualifications arising from any LegacyRevive.NET canonical document or design activity, including:

- purpose and principles;
- terminology;
- evidence model;
- provenance;
- confidence;
- developer intervention;
- convention inference;
- recovery matrix;
- recovery process;
- project reconstruction;
- source reconstruction;
- validation strategy;
- recovery metrics;
- architecture;
- tooling;
- MVP;
- roadmap;
- CLI design;
- artifact/report design;
- implementation specifications;
- document audits.

---

# 4. Status lifecycle

Each qualification must have exactly one current status.

## Open

Known and unresolved.

## In Review

Active design work is currently attempting to resolve it.

## Blocked

Cannot currently be resolved because another decision, experiment, implementation result, or external fact is required.

## Deferred

Intentionally postponed.

A deferred item is still unresolved.

## Resolved

A deliberate resolution has been reached, reflected in the owning canonical document, audited, and recorded.

Resolved items remain in this register permanently for provenance.

## Superseded

No longer independently applicable because another qualification or decision replaced it.

The superseding ID must be recorded.

## Legal status transitions

The normal transitions are:

```text
Open
  ↓
In Review
  ├──→ Blocked
  ├──→ Deferred
  └──→ Resolved

Blocked
  ├──→ In Review
  └──→ Deferred

Deferred
  └──→ In Review

Open / In Review / Blocked / Deferred
  └──→ Superseded
```

Direct `Open → Resolved` is allowed only for a qualification discovered and fully corrected within the same controlled audit/revision cycle, provided all resolution rules are satisfied.

## Reconsidering a resolved qualification

A resolved qualification should not normally be reopened by changing its historical status back to `Open`.

If later work materially invalidates or challenges the previous resolution:

1. keep the original qualification `Resolved`;
2. create a new qualification with a new stable ID;
3. reference the earlier qualification in `Origin` or `Depends on`;
4. explain what changed;
5. proceed through the normal lifecycle.

This preserves historical truth rather than rewriting it.

---

# 5. Qualification types

Use one of the following types where practical.

## Question

A design question requiring a decision.

## Ambiguity

Existing wording or evidence permits more than one interpretation.

## Assumption

A proposition is being relied upon without sufficient validation.

## Deferred Decision

A decision has deliberately been postponed.

## Boundary Issue

It is unclear which canonical document or subsystem should own the concept.

## Conflict

Two established sources or decisions appear inconsistent.

## Validation Need

A claim, heuristic, or design needs empirical or technical validation.

## Audit Finding

A document audit discovered a material issue.

## Dependency

Resolution depends on another design decision or piece of work.

---

# 6. ID scheme

Qualification IDs are stable and must never be reused.

Use a short domain prefix followed by a three-digit sequence.

Examples:

```text
DOC-001    Documentation / audit
DEV-001    Development process
TERM-001   Terminology
EVID-001   Evidence model
INTV-001   Developer intervention
CONV-001   Convention inference
RECV-001   Recovery process/model
MATR-001   Recovery matrix
PROJ-001   Project reconstruction
SRC-001    Source reconstruction
VAL-001    Validation strategy
METR-001   Recovery metrics
ARCH-001   Architecture
TOOL-001   Tooling/dependencies
MVP-001    MVP
CLI-001    CLI
RPT-001    Reports/artifacts
```

The prefix indicates the primary design area, not necessarily the document where the issue was first discovered.

---

# 7. Required entry format

Every qualification should use the following structure.

```text
## <ID> — <Short title>

Status: Open | In Review | Blocked | Deferred | Resolved | Superseded
Type: Question | Ambiguity | Assumption | Deferred Decision | Boundary Issue | Conflict | Validation Need | Audit Finding | Dependency
Origin: <document, audit, conversation, or decision where discovered>
Owner document: <canonical document expected to own the final answer>
Depends on: <qualification IDs or "None">
Blocks: <qualification IDs/documents or "None">

### Qualification

<What is or was unresolved?>

### Why it matters

<Why resolving this matters to the design or implementation.>

### Current understanding

<What is already established without pretending the issue is resolved.>

### Resolution criteria

<What must be decided, demonstrated, corrected, or validated before this can be closed?>

### Resolution

<Empty while unresolved. When resolved, record the agreed answer or correction.>

### Decision reference

<Decision ID from 90-decisions.md if the resolution creates a durable design decision, otherwise "None".>

### Resolved in

<Canonical document/section containing the final definition or correction, otherwise empty.>
```

---

# 8. Normative governance lifecycle

This section is the authoritative end-to-end governance process for material LegacyRevive.NET design issues.

The required lifecycle is:

```text
New document / audit / discussion
        ↓
Material qualification discovered?
        │
        ├── No
        │     ↓
        │   continue work
        │
        └── Yes
              ↓
        Register qualification in 91
              ↓
        Status = Open
              ↓
        When actively addressed:
        Status = In Review
              ↓
        Design work produces a proposed resolution
              ↓
        Is proposed resolution consistent with
        accepted decisions and canonical documents?
              │
              ├── No
              │     ↓
              │   resolve conflict first
              │   and/or create additional qualification
              │
              └── Yes
                    ↓
        Does resolution establish durable
        project direction?
              │
              ├── Yes
              │     ↓
              │   add decision to 90
              │     ↓
              │   link decision ID from this qualification
              │
              └── No
                    ↓
                  no 90 entry required
                    ↓
        Update affected canonical document(s)
                    ↓
        Audit updated canonical document(s)
                    ↓
        Did audit reveal new material qualifications?
              │
              ├── Yes → register them in 91
              │
              └── No / handled
                    ↓
        Perform governance synchronization check
        across 90, 91, canonical docs, and audits
                    ↓
        Record final resolution in 91
                    ↓
        Mark qualification Resolved
                    ↓
        Retain qualification permanently
```

## Governance ownership

`91-design-qualification-register.md` owns:

- qualification lifecycle;
- qualification status;
- qualification history;
- resolution state;
- the normative governance process.

`90-decisions.md` owns:

- durable accepted decisions;
- decision rationale;
- decision supersession history.

Canonical domain documents own:

- the current authoritative design definition for their subject.

Audits verify:

- coverage;
- provenance;
- assumption/hallucination risk;
- document boundaries;
- consistency;
- contradiction;
- qualification resolution.

---

# 9. Resolution rules

A qualification may only be marked **Resolved** when:

1. the unresolved point has been explicitly answered or corrected;
2. the answer/correction is consistent with current canonical material;
3. any affected canonical document has been updated;
4. any durable design decision has been recorded in `90-decisions.md` where appropriate;
5. the resolution is written into this register.

Do not mark a qualification resolved merely because:

- discussion moved on;
- a likely answer exists;
- an implementation happened to choose one option;
- an AI inferred the answer;
- the issue stopped being mentioned.

---

# 10. Relationship to document status

Canonical documents may use these statuses:

```text
Draft
Candidate canonical — unaudited
Candidate canonical — audited with open qualifications
Canonical
```

A document should remain:

**Candidate canonical — audited with open qualifications**

when unresolved qualifications materially affect its definitions.

A document may become **Canonical** when:

- material qualifications are resolved; or
- remaining qualifications are explicitly non-blocking and delegated to later documents.

Where useful, relevant qualification IDs should be listed in the document.

---

# 11. Relationship to 90-decisions.md

This register and `90-decisions.md` serve different purposes.

## This register

Tracks unresolved and resolved qualifications.

It answers:

> What issue existed, what still needs resolution, and what later resolved it?

## `90-decisions.md`

Records durable accepted design decisions.

It answers:

> What did we decide, and why?

A qualification may be resolved without creating a major decision entry, but important semantic or architectural resolutions should normally reference a decision ID.

---

# 12. Review discipline

Before completing any new canonical document:

1. review this register for qualifications owned by that document;
2. attempt to resolve them;
3. add any newly discovered qualifications;
4. update statuses;
5. ensure unresolved items are not silently presented as settled;
6. record new durable decisions where appropriate.

Before declaring a document canonical:

1. run its audit;
2. compare audit findings with this register;
3. ensure every material unresolved finding has a qualification ID;
4. ensure every material resolved finding that affected the canonical result is recorded;
5. ensure no qualification was lost during revision;
6. perform the governance synchronization check across `90`, `91`, affected canonical documents, and audits.

A qualification may only be marked `Resolved` after this synchronization check passes.

---

# 13. Governance synchronization check

A governance change is complete only when all affected governance artifacts agree.

For each qualification being resolved, verify:

## Qualification register

- status and resolution are correct;
- decision reference is present when required;
- `Resolved in` points to the owning canonical document.

## Decision log

If a durable decision was required:

- the decision exists;
- its ID is stable;
- it references the relevant qualification;
- its rationale and consequences are recorded.

## Canonical document

- the accepted resolution is actually reflected in the owning document;
- the document does not still present the question as unresolved;
- related terminology is consistent.

## Audit

- the updated document has been audited;
- the audit confirms the resolution or explicitly records remaining qualifications;
- any new material issue has been registered.

The governance update is not complete until these checks agree.

---

# 14. Register index

| ID | Title | Status | Type | Owner document |
|---|---|---|---|---|
| DOC-001 | Unsupported Scenario Assumptions | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-002 | Premature Feature Commitments | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-003 | Premature Recovery Pipeline Specification | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-004 | Premature Architecture Concepts | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-005 | Artifact-Specific Recoverability Overreach | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-006 | Premature Tooling Commitments | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-007 | Stale Canonical Document Map | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-008 | Governance Process Absent from AI Orientation | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-009 | Open Evidence Model Questions Presented Too Strongly | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-010 | Excessive Downstream Design Detail in Orientation | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-011 | Stale Accepted-Decision Summary | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-012 | Stale Project-State / Next-Step Guidance | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-013 | Minor Formatting Defect in AI Context | Resolved | Audit Finding | `00-ai-context.md` |
| DOC-014 | Pre-governance Purpose Document Status | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-015 | Evidence-Model Boundary Wording in Purpose Document | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-016 | Terminology Document Governance Status | Resolved | Audit Finding | `02-terminology.md` |
| DOC-017 | Provisional Terminology Semantics Signposting | Resolved | Audit Finding | `02-terminology.md` |
| DOC-018 | Stale Evidence-Model Status Wording in Purpose Document | Resolved | Audit Finding | `01-purpose-and-principles.md` |
| DOC-019 | Epistemic Taxonomy and Glossary Synchronization | Resolved | Conflict | `02-terminology.md` / `03-evidence-model.md` |
| DOC-020 | External Enrichment Scope Timing Across Tooling, MVP, and Roadmap | Resolved | Conflict | `14-mvp.md` / `15-roadmap.md` |
| DOC-021 | Stale Validation Result Terminology After Validation Model Finalization | Resolved | Audit Finding | `02-terminology.md` |
| DOC-022 | Stale Qualification Summary Count During Workspace Governance Synchronization | Resolved | Audit Finding | `91-design-qualification-register.md` |
| DOC-023 | Premature Canonical Status During DEV-001 Governance | Resolved | Audit Finding | `00-ai-context.md` / `13-tooling-and-dependencies.md` / `14-mvp.md` / `16-development-process.md` |
| DOC-024 | Stale Qualification Summary After Stage 01 Follow-Up Qualifications | Resolved | Audit Finding | `91-design-qualification-register.md` |
| GOV-001 | Normative End-to-End Governance Lifecycle | Resolved | Audit Finding | `91-design-qualification-register.md` |
| GOV-002 | Qualification Status Transition Rules | Resolved | Audit Finding | `91-design-qualification-register.md` |
| GOV-003 | Decision Replacement Rule | Resolved | Audit Finding | `90-decisions.md` |
| GOV-004 | Governance Synchronization Check | Resolved | Audit Finding | `91-design-qualification-register.md` / `90-decisions.md` |
| EVID-001 | Scope of Evidence | Resolved | Question | `03-evidence-model.md` |
| EVID-002 | Observation vs Recovered Fact | Resolved | Ambiguity | `03-evidence-model.md` |
| EVID-003 | Inference Promotion | Resolved | Question | `03-evidence-model.md` |
| EVID-004 | Conflicting Evidence Representation | Resolved | Question | `03-evidence-model.md` |
| EVID-005 | Provenance vs Lineage | Resolved | Question | `03-evidence-model.md` |
| EVID-006 | Final Confidence Vocabulary | Resolved | Deferred Decision | `03-evidence-model.md` |
| EVID-007 | Confidence Attachment Scope | Resolved | Question | `03-evidence-model.md` |
| EVID-008 | Assumption Lifecycle | Resolved | Question | `03-evidence-model.md` |
| EVID-009 | Developer Correction and Supersession | Resolved | Dependency | `03-evidence-model.md` / `04-developer-intervention.md` |
| EVID-010 | Circular Evidence Protection | Resolved | Question | `03-evidence-model.md` |
| INTV-001 | Canonical Intervention Kinds | Resolved | Question | `04-developer-intervention.md` |
| INTV-002 | Intervention Record and Scope | Resolved | Question | `04-developer-intervention.md` |
| INTV-003 | Intervention Lifecycle | Resolved | Question | `04-developer-intervention.md` |
| INTV-004 | Intervention Persistence and Replay | Resolved | Question | `04-developer-intervention.md` |
| INTV-005 | Intervention Precedence vs Evidence | Resolved | Question | `04-developer-intervention.md` |
| INTV-006 | Conflicting Active Interventions | Resolved | Conflict | `04-developer-intervention.md` |
| INTV-007 | Withdrawal and Downstream Reevaluation | Resolved | Question | `04-developer-intervention.md` |
| CONV-001 | Convention Observation Semantics | Resolved | Question | `05-convention-inference.md` |
| CONV-002 | Eligible Convention Support and Feedback Protection | Resolved | Question | `05-convention-inference.md` |
| CONV-003 | Convention Scope and Generalization | Resolved | Question | `05-convention-inference.md` |
| CONV-004 | Convention Recurrence Threshold | Resolved | Question | `05-convention-inference.md` |
| CONV-005 | Convention Exceptions | Resolved | Question | `05-convention-inference.md` |
| CONV-006 | Convention Support Profile vs Confidence | Resolved | Boundary Issue | `05-convention-inference.md` |
| CONV-007 | Competing Convention Observations | Resolved | Conflict | `05-convention-inference.md` |
| CONV-008 | Convention History and Reevaluation | Resolved | Question | `05-convention-inference.md` |
| RMAT-001 | Recovery Matrix Contribution Classes | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-002 | Artifact Type vs Artifact Instance Capability | Resolved | Boundary Issue | `06-recovery-matrix.md` |
| RMAT-003 | Artifact Classification Beyond Extension | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-004 | Missing Artifact Semantics | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-005 | Companion Artifact Corroboration | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-006 | Matrix vs Artifact-Instance Output | Resolved | Boundary Issue | `06-recovery-matrix.md` |
| RMAT-007 | Recovery Ceiling Wording | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-008 | Cross-Artifact Recovery Semantics | Resolved | Question | `06-recovery-matrix.md` |
| RMAT-009 | Stage 02 Artifact Format and Family Recognition Contract | Resolved | Boundary Issue | `06-recovery-matrix.md` / `07-recovery-process.md` |
| RMAT-010 | Malformed Family-Candidate Evidence Threshold | Resolved | Ambiguity | `06-recovery-matrix.md` |
| PROC-001 | Canonical Recovery Process Shape | Resolved | Question | `07-recovery-process.md` |
| PROC-002 | Stage Progression and Epistemic Category | Resolved | Boundary Issue | `07-recovery-process.md` |
| PROC-003 | Earliest-Affected-Stage Reevaluation | Resolved | Question | `07-recovery-process.md` |
| PROC-004 | Late Artifact Intake | Resolved | Question | `07-recovery-process.md` |
| PROC-005 | Developer Intervention Placement | Resolved | Question | `07-recovery-process.md` |
| PROC-006 | Build Diagnostic Semantics | Resolved | Boundary Issue | `07-recovery-process.md` |
| PROC-007 | Workspace Run Checkpoint Model | Resolved | Question | `07-recovery-process.md` |
| PROC-008 | Partial Blocked and Baseline States | Resolved | Question | `07-recovery-process.md` |
| PROC-009 | Validation Feedback Boundary | Resolved | Boundary Issue | `07-recovery-process.md` |
| PROC-010 | Incomplete Supplied-Directory Enumeration During Intake | Resolved | Boundary Issue | `07-recovery-process.md` |
| PROC-011 | Positive File Discovery Before Later Descendant-Discovery Failure | Resolved | Ambiguity | `07-recovery-process.md` |
| PROJ-001 | Candidate Project vs Original Project | Resolved | Boundary Issue | `08-project-reconstruction.md` |
| PROJ-002 | Assembly Boundary as Project Candidate Anchor | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-003 | Project Boundary Support and Conflict | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-004 | Candidate Project Merge and Split | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-005 | Project Identity and Naming | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-006 | Project Kind and Target Framework | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-007 | Dependency Declaration Reconstruction | Resolved | Boundary Issue | `08-project-reconstruction.md` |
| PROJ-008 | Unresolved Dependencies and Cycles | Resolved | Question | `08-project-reconstruction.md` |
| PROJ-009 | Generated Project/Solution Evidence Boundary | Resolved | Boundary Issue | `08-project-reconstruction.md` |
| PROJ-010 | Multiple Candidate Structures and Selection | Resolved | Question | `08-project-reconstruction.md` |
| SRC-001 | Source Reconstruction Provenance Classes | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| SRC-002 | Source Provenance Granularity | Resolved | Question | `09-source-reconstruction.md` |
| SRC-003 | Recovered Source File Identity and Path | Resolved | Question | `09-source-reconstruction.md` |
| SRC-004 | Directly Preserved Source Content Takes Precedence over Weaker Regeneration | Resolved | Question | `09-source-reconstruction.md` |
| SRC-005 | Source Skeleton Is a Valid Partial Recovery and Unknown Bodies Are Not Fabricated | Resolved | Question | `09-source-reconstruction.md` |
| SRC-006 | Higher-Level Reconstruction of Lowered Code Is Generated Interpretation | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| SRC-007 | Source Recovery Ceilings Remain Explicit and Artifact-Set Scoped | Resolved | Question | `09-source-reconstruction.md` |
| SRC-008 | Source Organization and Candidate Project Ownership Are Reconstruction Choices | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| SRC-009 | Multiple Source Candidates May Coexist; Selection Is Operational | Resolved | Question | `09-source-reconstruction.md` |
| SRC-010 | Build-Driven Source Repair Remains Reconstructed or Developer-Adjusted Source | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| SRC-011 | Generated Source Cannot Independently Corroborate Its Own Reconstruction | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| SRC-012 | Manual Materialized-Source Changes and Developer-Adjusted Source | Resolved | Boundary Issue | `09-source-reconstruction.md` |
| VAL-001 | Validation Targets an Identified Recovery Checkpoint | Resolved | Question | `10-validation-strategy.md` |
| VAL-002 | Validation Is Governed by an Explicit Validation Plan | Resolved | Question | `10-validation-strategy.md` |
| VAL-003 | Validation Dimensions Remain Independently Visible | Resolved | Question | `10-validation-strategy.md` |
| VAL-004 | Not Evaluated and Not Comparable Are Distinct from Validation Failure | Resolved | Boundary Issue | `10-validation-strategy.md` |
| VAL-005 | Validation Requires Compatible Target and Reference Context | Resolved | Question | `10-validation-strategy.md` |
| VAL-006 | Blind Evaluation Freezes Recovery before Validation Reference Access | Resolved | Boundary Issue | `10-validation-strategy.md` |
| VAL-007 | Reference-Derived Repair Creates a New Recovery Lineage | Resolved | Boundary Issue | `10-validation-strategy.md` |
| VAL-008 | Build, API, Source, and Behavioral Validation Establish Different Claims | Resolved | Boundary Issue | `10-validation-strategy.md` |
| VAL-009 | Validation Runs and Findings Preserve Provenance and History | Resolved | Question | `10-validation-strategy.md` |
| VAL-010 | Validated Baseline Does Not Mean Perfect Recovery | Resolved | Boundary Issue | `10-validation-strategy.md` |
| METR-001 | Metric Applicability, Populations, and Denominators | Resolved | Question | `11-recovery-metrics.md` |
| METR-002 | Correctness vs Coverage and Unknowns | Resolved | Boundary Issue | `11-recovery-metrics.md` |
| METR-003 | Set Matching, Precision, and Recall | Resolved | Question | `11-recovery-metrics.md` |
| METR-004 | Recoverability-Aware Measurement | Resolved | Boundary Issue | `11-recovery-metrics.md` |
| METR-005 | Dependency Existence vs Representation Fidelity | Resolved | Boundary Issue | `11-recovery-metrics.md` |
| METR-006 | Source Fidelity Comparison Levels | Resolved | Boundary Issue | `11-recovery-metrics.md` |
| METR-007 | Graduated Operational Recovery Metrics | Resolved | Question | `11-recovery-metrics.md` |
| METR-008 | Developer Intervention Burden | Resolved | Question | `11-recovery-metrics.md` |
| METR-009 | Epistemic Accuracy Metrics | Resolved | Question | `11-recovery-metrics.md` |
| METR-010 | Aggregation and Overall Score | Resolved | Boundary Issue | `11-recovery-metrics.md` |
| METR-011 | Cross-Run and Cross-Scenario Comparability | Resolved | Question | `11-recovery-metrics.md` |
| ARCH-001 | Logical Modular Architecture and Deployment Boundary | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-002 | Canonical State Partitioning | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-003 | Artifact Analyzer Output Boundary | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-004 | Support Graph Architectural Representation | Resolved | Question | `12-architecture.md` |
| ARCH-005 | Inference Rule Architecture | Resolved | Question | `12-architecture.md` |
| ARCH-006 | Recovery Orchestration Boundary | Resolved | Question | `12-architecture.md` |
| ARCH-007 | Reconstruction Feedback Isolation | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-008 | Validation Reference Architectural Isolation | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-009 | Checkpoint and Replay Architecture | Resolved | Question | `12-architecture.md` |
| ARCH-010 | Failure and Diagnostic Architecture | Resolved | Question | `12-architecture.md` |
| ARCH-011 | Extension Boundary | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-012 | Persistence Technology Boundary | Resolved | Deferred Decision | `12-architecture.md` |
| ARCH-013 | Application-Gateway Dependency Policy | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-014 | Local Recovery Workspace Physical Boundary | Resolved | Boundary Issue | `12-architecture.md` |
| ARCH-015 | Original Artifact Preservation Strategy | Resolved | Question | `12-architecture.md` |
| TOOL-001 | Runtime/SDK Baseline | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-002 | Dependency Version Governance | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-003 | Managed Metadata Extraction Technology | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-004 | Decompiler Selection | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-005 | PDB Technology Strategy | Resolved | Boundary Issue | `13-tooling-and-dependencies.md` |
| TOOL-006 | C# Syntax/Semantic Engine | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-007 | MSBuild and Build Execution Model | Resolved | Boundary Issue | `13-tooling-and-dependencies.md` |
| TOOL-008 | NuGet Package and Feed Access | Resolved | Boundary Issue | `13-tooling-and-dependencies.md` |
| TOOL-009 | Persistence Technology | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-010 | Serialization/XML Technology | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-011 | Host DI Logging and CLI | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-012 | External Process Execution | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-013 | Generated Identity and Artifact Hashing | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-014 | Automated Test Framework | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-015 | Dependency Security and Licensing Governance | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-016 | LegacyRevive.NET Repository Solution Format | Resolved | Question | `13-tooling-and-dependencies.md` |
| TOOL-017 | LegacyRevive.NET Repository Solution Filename Rename | Resolved | Question | `13-tooling-and-dependencies.md` |
| MVP-001 | MVP Product Slice | Resolved | Question | `14-mvp.md` |
| MVP-002 | MVP Execution Platform | Resolved | Question | `14-mvp.md` |
| MVP-003 | MVP Input and Artifact Support | Resolved | Question | `14-mvp.md` |
| MVP-004 | Managed Assembly Analysis Depth | Resolved | Question | `14-mvp.md` |
| MVP-005 | MVP Project-Candidate Scope | Resolved | Question | `14-mvp.md` |
| MVP-006 | MVP Dependency Reconstruction | Resolved | Boundary Issue | `14-mvp.md` |
| MVP-007 | MVP Source Reconstruction | Resolved | Question | `14-mvp.md` |
| MVP-008 | MVP Development-Tree Export | Resolved | Question | `14-mvp.md` |
| MVP-009 | MVP Developer Intervention and Context | Resolved | Question | `14-mvp.md` |
| MVP-010 | MVP Workspace Resume | Resolved | Question | `14-mvp.md` |
| MVP-011 | MVP Build and Repair Boundary | Resolved | Boundary Issue | `14-mvp.md` |
| MVP-012 | MVP Outcome Semantics | Resolved | Boundary Issue | `14-mvp.md` |
| MVP-013 | MVP User Surface | Resolved | Question | `14-mvp.md` |
| MVP-014 | MVP Validation Requirement | Resolved | Question | `14-mvp.md` |
| MVP-015 | MVP Release Gates | Resolved | Question | `14-mvp.md` |
| MVP-016 | MVP Exclusions | Resolved | Boundary Issue | `14-mvp.md` |
| MVP-017 | Deterministic Export from Canonical Recovery State | Resolved | Boundary Issue | `14-mvp.md` |
| MVP-018 | Export Handling of Working-Tree Divergence | Resolved | Boundary Issue | `14-mvp.md` |
| ROAD-001 | Roadmap Strategic-vs-Design Boundary | Resolved | Boundary Issue | `15-roadmap.md` |
| ROAD-002 | Evidence-Gated Horizon Model | Resolved | Question | `15-roadmap.md` |
| ROAD-003 | Capability Promotion Criteria | Resolved | Question | `15-roadmap.md` |
| ROAD-004 | First Post-MVP Priority | Resolved | Question | `15-roadmap.md` |
| ROAD-005 | Platform and Artifact Expansion | Resolved | Question | `15-roadmap.md` |
| ROAD-006 | External Enrichment Expansion | Resolved | Boundary Issue | `15-roadmap.md` |
| ROAD-007 | Advanced Inference and Learned Models | Resolved | Boundary Issue | `15-roadmap.md` |
| ROAD-008 | Richer Product Hosts | Resolved | Question | `15-roadmap.md` |
| ROAD-009 | Validation Maturity | Resolved | Question | `15-roadmap.md` |
| ROAD-010 | Runtime Plugin Ecosystem | Resolved | Deferred Decision | `15-roadmap.md` |
| ROAD-011 | Collaborative and Enterprise Scale | Resolved | Deferred Decision | `15-roadmap.md` |
| ROAD-012 | Native and Runtime Recovery | Resolved | Boundary Issue | `15-roadmap.md` |
| DEV-001 | Canonical Development Process and Development Slice Specifications | Resolved | Question | `16-development-process.md` |
| DEV-002 | Semantic Readiness of a Development Slice | Resolved | Boundary Issue | `16-development-process.md` |
| DEV-003 | Post-Implemented Rejection of a Development Slice | Resolved | Ambiguity | `16-development-process.md` |
| DEV-004 | Recognition Boundary and Falsification Verification | Resolved | Validation Need | `16-development-process.md` |

---

# 15. Resolved documentation qualifications

## DOC-001 — Unsupported Scenario Assumptions

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft included plausible but unestablished scenario assumptions, including statements implying that:

- original developers were unavailable;
- build/deployment knowledge had been lost;
- maintenance necessarily had to continue.

These were realistic examples but had not been established as defining product conditions.

### Why it matters

A foundational purpose document must not silently convert plausible legacy-system scenarios into accepted product assumptions.

### Current understanding

The actual established recovery condition is that original development material may be missing, incomplete, damaged, inaccessible, or unusable while surviving/deployed artifacts remain.

### Resolution criteria

Remove scenario-specific assumptions that are not necessary to define the product.

### Resolution

The unsupported scenario assumptions were removed from the revised `01-purpose-and-principles.md`.

The product definition now stays with the established recovery condition rather than adding unagreed contextual claims.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---

## DOC-002 — Premature Feature Commitments

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft implied specific capabilities or outputs as though they were already established requirements, including detailed unresolved-recovery diagnostics and other feature-level behaviour.

### Why it matters

A purpose/principles document should define product direction without silently freezing feature scope that belongs in later MVP, architecture, or recovery-capability documents.

### Current understanding

LegacyRevive.NET should preserve and expose uncertainty, but the exact mechanisms and artifacts for doing so remain design work for later documents.

### Resolution criteria

Remove detailed feature commitments while retaining the foundational principle they were intended to support.

### Resolution

The revised document retains the principles of explainability, visible uncertainty, and developer progress while removing premature implementation/feature commitments.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---

## DOC-003 — Premature Recovery Pipeline Specification

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft included a relatively concrete staged recovery sequence and risked presenting that sequence as the accepted recovery pipeline.

### Why it matters

The discovery conversation established **progressive recovery from stronger evidence toward more inferential reconstruction**, but it had not yet formally agreed the exact recovery pipeline.

### Current understanding

Progressive recovery is established as a principle.

Exact stages and execution flow remain future design work.

### Resolution criteria

Retain the principle while removing the implication that the exact pipeline is already canonical.

### Resolution

The revised document now states only that recovery should progress from stronger evidence toward weaker inference and explicitly delegates exact pipeline design to later canonical documents.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---

## DOC-004 — Premature Architecture Concepts

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft introduced conceptual stages such as:

```text
Observe
→ Interpret
→ Propose
→ Reconstruct
```

and specific recovery layers in a way that could be mistaken for agreed architecture.

### Why it matters

Useful synthesis should not silently become canonical architecture before the Evidence Model, Recovery Matrix, and architecture work have been completed.

### Current understanding

Separation between evidence, inference, reconstruction, intervention, and validation is established.

The exact architecture is not.

### Resolution criteria

Remove architecture-like prescriptions from the purpose document unless they are foundational principles.

### Resolution

The revised document keeps only the high-level distinction between stronger evidence and weaker inference and delegates architecture/pipeline concepts to later documents.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---

## DOC-005 — Artifact-Specific Recoverability Overreach

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft made detailed statements about what particular artifacts could directly recover or infer.

### Why it matters

Artifact-specific recoverability is a technical question that should be validated and owned by the Recovery Matrix rather than embedded in a foundational purpose document.

### Current understanding

The product scenario clearly includes surviving artifacts such as assemblies, configuration, PDBs, documentation, runtime metadata, and resources.

The exact recovery capability of each artifact remains separate design work.

### Resolution criteria

Keep artifact examples only as product-context examples and move detailed recoverability claims out of the document.

### Resolution

The revised `01-purpose-and-principles.md` mentions artifact classes only to describe the product scenario and delegates detailed recoverability to the future Recovery Matrix.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---

## DOC-006 — Premature Tooling Commitments

Status: Resolved  
Type: Audit Finding  
Origin: Initial audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: None  
Blocks: None

### Qualification

The initial draft named specific technologies in a way that risked reading as established implementation commitments.

### Why it matters

The discovery conversation established the principle of leveraging mature .NET/open-source tooling, but final library selection has not yet been made.

### Current understanding

LegacyRevive.NET should investigate and reuse mature tooling where appropriate.

Specific dependency choices belong in later architecture/tooling work.

### Resolution criteria

Preserve the reuse principle while avoiding premature library commitment.

### Resolution

The revised document states the general principle and delegates exact technology choices to future architecture/tooling documents.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md`

---


## DOC-007 — Stale Canonical Document Map

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: D016, D020  
Blocks: None

### Qualification

The AI context still referenced an obsolete canonical document map and obsolete decision filename.

### Why it matters

`00-ai-context.md` is intended to be read first. Incorrect ownership or filenames could redirect all later Project work.

### Current understanding

The agreed domain-document sequence now runs `00` through `15`, with `90-decisions.md` and `91-design-qualification-register.md` as cross-cutting governance documents.

### Resolution criteria

Replace the stale map with the current definitive ordering and ownership.

### Resolution

The revised `00-ai-context.md` now contains the current document-development order and canonical ownership map.

### Decision reference

D016

### Resolved in

`00-ai-context.md` §§10–11

---

## DOC-008 — Governance Process Absent from AI Orientation

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: D020, D021  
Blocks: None

### Qualification

The AI orientation document predated the finalized governance process and did not direct future canonical-document work through `90` and `91`.

### Why it matters

A fresh Project chat could bypass qualification registration, decision logging, audit, and synchronization.

### Current understanding

`91` owns the normative qualification/governance lifecycle and `90` owns durable decisions.

### Resolution criteria

Make `00` explicitly direct future canonical-document work to the governance process without duplicating all governance mechanics.

### Resolution

The revised `00-ai-context.md` contains a dedicated Governance section that points to `90` and `91` and requires their lifecycle for canonical-document changes.

### Decision reference

D020

### Resolved in

`00-ai-context.md` §4

---

## DOC-009 — Open Evidence Model Questions Presented Too Strongly

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: EVID-001, EVID-002, EVID-005, EVID-006, EVID-007  
Blocks: None

### Qualification

The original AI context included concrete evidence-category and confidence-language material while the formal Evidence Model questions remain open.

### Why it matters

Because `00` is read first, provisional terminology could be mistaken for settled Evidence Model semantics.

### Current understanding

The project has accepted high-level evidence principles, but `EVID-001` through `EVID-010` remain open.

### Resolution criteria

Keep only accepted evidence principles in `00` and explicitly defer unresolved semantics to `03-evidence-model.md` and `91`.

### Resolution

The revised `00` lists the open `EVID-*` qualifications and no longer presents their unresolved taxonomy/confidence details as settled.

### Decision reference

D002

### Resolved in

`00-ai-context.md` §5

---

## DOC-010 — Excessive Downstream Design Detail in Orientation

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: D016  
Blocks: None

### Qualification

The original `00` contained substantial detail that belongs to later owners, including pipeline stages, recovery layers, project-reconstruction evidence, tooling candidates, validation metrics, and MVP feature direction.

### Why it matters

An orientation/index document should not compete with canonical domain owners.

### Current understanding

D016 requires each major concept to have one primary canonical owner.

### Resolution criteria

Reduce `00` to orientation, accepted high-level direction, ownership, governance, and current-state information.

### Resolution

The revised `00` removes or reduces downstream design detail and points to the appropriate canonical owners.

### Decision reference

D016

### Resolved in

`00-ai-context.md` throughout, especially §§10–14

---

## DOC-011 — Stale Accepted-Decision Summary

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: D016  
Blocks: None

### Qualification

The original `00` contained a fixed list of 16 accepted decisions while `90-decisions.md` now contains `D001` through `D022`.

### Why it matters

A partial duplicated decision list can become stale and be mistaken for authoritative completeness.

### Current understanding

`90-decisions.md` is the canonical decision log.

### Resolution criteria

Keep only a concise orientation summary and clearly identify `90` as authoritative.

### Resolution

The revised `00` summarizes foundational direction and explicitly directs readers to `90-decisions.md` for the complete decision history.

### Decision reference

D016

### Resolved in

`00-ai-context.md` §2

---

## DOC-012 — Stale Project-State / Next-Step Guidance

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: None  
Blocks: None

### Qualification

The original `00` instructed future work to create `01` and `02`, although both documents already exist.

### Why it matters

The orientation document should accurately identify current state and the next domain document.

### Current understanding

`01`, `02`, `90`, and `91` now exist. `03-evidence-model.md` is next in the agreed sequence.

### Resolution criteria

Update the current-state section and next-step guidance.

### Resolution

The revised `00` records the completed foundation/governance set and identifies `03-evidence-model.md` as the next domain document.

### Decision reference

None

### Resolved in

`00-ai-context.md` §12

---

## DOC-013 — Minor Formatting Defect in AI Context

Status: Resolved  
Type: Audit Finding  
Origin: Governed audit of `00-ai-context.md`  
Owner document: `00-ai-context.md`  
Depends on: None  
Blocks: None

### Qualification

The original introductory list contained a malformed double-hyphen bullet.

### Why it matters

This is not a design issue, but the first-read orientation document should be clean and unambiguous.

### Current understanding

The defect is purely editorial.

### Resolution criteria

Correct the malformed list formatting.

### Resolution

The malformed bullet was removed during the governed revision.

### Decision reference

None

### Resolved in

`00-ai-context.md`

---


## DOC-014 — Pre-governance Purpose Document Status

Status: Resolved  
Type: Audit Finding  
Origin: Governed re-audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: GOV-001, GOV-004  
Blocks: None

### Qualification

The purpose/principles document still carried a pre-governance candidate status based only on its original discovery-conversation audit.

### Why it matters

A foundational document should have an explicit status under the current governance process.

### Current understanding

Its earlier substantive audit was valid, and `DOC-001` through `DOC-006` remain resolved.

### Resolution criteria

Re-audit under the finalized governance process, correct any new material issues, synchronize governance artifacts, and state its canonical status.

### Resolution

The document was governed re-audited, revised, and marked `Canonical`. A governance-relationship section now points to `90` and `91` while preserving the earlier qualification history.

### Decision reference

D020

### Resolved in

`01-purpose-and-principles.md` Status and §28

---

## DOC-015 — Evidence-Model Boundary Wording in Purpose Document

Status: Resolved  
Type: Audit Finding  
Origin: Governed re-audit of `01-purpose-and-principles.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: EVID-001, EVID-004, EVID-005, EVID-006, EVID-007, EVID-008, EVID-010  
Blocks: None

### Qualification

A small number of evidence/provenance/confidence phrases could be read as more formally settled than the still-open Evidence Model permits, particularly use of `lineage` while `EVID-005` remains unresolved.

### Why it matters

The purpose document should constrain the Evidence Model through accepted principles without silently deciding its formal taxonomy or state semantics.

### Current understanding

High-level principles such as provenance, evidence/inference separation, explainable confidence, visible uncertainty, and anti-circularity are already accepted. Their formal representation remains open.

### Resolution criteria

Retain the accepted principles while explicitly delegating unresolved formal semantics to `03-evidence-model.md`.

### Resolution

The revised purpose document uses principle-level wording, replaces unnecessary formal-sounding `lineage` wording with neutral provenance/derivation wording, and explicitly delegates unresolved evidence, confidence, conflict, assumption, and circularity semantics to `03-evidence-model.md`.

### Decision reference

D002

### Resolved in

`01-purpose-and-principles.md` §§5–9, 14, 28

---


## DOC-016 — Terminology Document Governance Status

Status: Resolved  
Type: Audit Finding  
Origin: Governed re-audit of `02-terminology.md`  
Owner document: `02-terminology.md`  
Depends on: GOV-001  
Blocks: None

### Qualification

The terminology document predated the finalized governance lifecycle, and its earlier audit status did not use the formal document-status vocabulary now defined in `91`.

### Why it matters

`EVID-001` through `EVID-010` materially affect several glossary definitions, so the document must not be presented as fully settled.

### Current understanding

The glossary is useful and mostly stable, but formal evidence semantics remain open.

### Resolution criteria

Apply the governed status rule and explicitly identify the open qualifications that materially affect the glossary.

### Resolution

The revised document is marked `Candidate canonical — audited with open qualifications` and explicitly lists `EVID-001` through `EVID-010`.

### Decision reference

D020

### Resolved in

`02-terminology.md` Status and §52

---

## DOC-017 — Provisional Terminology Semantics Signposting

Status: Resolved  
Type: Audit Finding  
Origin: Governed re-audit of `02-terminology.md`  
Owner document: `02-terminology.md`  
Depends on: EVID-001, EVID-002, EVID-004, EVID-005, EVID-006, EVID-007, EVID-008, EVID-009, EVID-010  
Blocks: None

### Qualification

Several useful glossary definitions could be mistaken for finalized formal Evidence Model semantics even though their exact boundaries remain unresolved.

### Why it matters

Terminology should support current work without silently deciding the Evidence Model.

### Current understanding

The project can use provisional glossary meanings while preserving explicit qualification boundaries.

### Resolution criteria

Mark affected definitions as provisional and link them to the controlling `EVID-*` qualifications.

### Resolution

The revised glossary explicitly identifies provisional boundaries for Evidence, Recovered Fact, conflicts, Provenance/Lineage, Confidence, assumptions, Developer Assertion, Developer Correction, and circularity-related concepts.

### Decision reference

D002

### Resolved in

`02-terminology.md` §§4–21, 29–32, 52

---


## DOC-018 — Stale Evidence-Model Status Wording in Purpose Document

Status: Resolved  
Type: Audit Finding  
Origin: Governed generation/audit of `06-recovery-matrix.md`  
Owner document: `01-purpose-and-principles.md`  
Depends on: EVID-001 through EVID-010  
Blocks: None

### Qualification

The opening status text in `01-purpose-and-principles.md` still said the Evidence Model qualifications were open and delegated to `03`, even though the same document later correctly recorded that EVID-001 through EVID-010 had been resolved.

### Why it matters

A canonical foundational document contained contradictory current-state wording.

### Current understanding

`03-evidence-model.md` is canonical and EVID-001 through EVID-010 are resolved.

### Resolution criteria

Remove the stale "open qualifications" wording without moving detailed Evidence Model ownership into `01`.

### Resolution

The stale sentence was replaced with wording that states `03-evidence-model.md` now defines the formal evidence semantics while detailed mechanics remain owned there.

### Decision reference

None

### Resolved in

`01-purpose-and-principles.md` Status

---

## DOC-019 — Epistemic Taxonomy and Glossary Synchronization

Status: Resolved  
Type: Conflict  
Origin: 2026-09-26 whole-set governance, consistency, and completeness audit  
Owner document: `02-terminology.md` / `03-evidence-model.md`  
Depends on: EVID-001, EVID-002, EVID-006  
Blocks: None

### Qualification

The canonical glossary and Evidence Model contained materially inconsistent boundary wording: `03-evidence-model.md` described Validation Reference as one of four Recovery Input categories immediately before placing it outside the Recovery Input graph; `02-terminology.md` defined Observation first as direct extraction from Recovery Input rather than Evidence; `Evidence Source` incorrectly listed a Developer Assertion; and the glossary still described `EVID-006` confidence vocabulary as open. Core Evidence Model terms including Developer Input, Recovery Configuration, Conflict, and Unknown also lacked explicit glossary entries.

### Why it matters

These inconsistencies could permit an implementation to admit Developer Input or Validation Reference into the evidence graph incorrectly, obscure the distinction between supplied artifacts and external contextual evidence, or treat settled support-strength semantics as provisional.

### Current understanding

D007, D023, D027, the resolved `EVID-*` qualifications, and `03-evidence-model.md` already establish the intended boundaries. `15-roadmap.md` and D153 additionally require external enrichment to remain distinct from supplied Recovery Input.

### Resolution criteria

Synchronize `02` and `03` so Recovery Input, Evidence, Developer Input, Recovery Configuration, Validation Reference, External Enrichment, and inferential support labels have one consistent meaning; add missing core glossary terms; and record a durable rule for how explicitly admitted external enrichment participates in the Evidence Model.

### Resolution

`02-terminology.md` and `03-evidence-model.md` were synchronized. Validation Reference is explicitly outside Recovery Input; Observations derive directly from Evidence; Developer Assertions are no longer Evidence Sources; `EVID-006` is recorded as resolved; missing core glossary terms were added; and External Enrichment, when explicitly admitted to recovery, is classified as Independent Contextual Evidence while remaining distinct from Supplied Recovery Input and Original-System Evidence.

### Decision reference

D160

### Resolved in

`02-terminology.md` / `03-evidence-model.md`

---

## DOC-020 — External Enrichment Scope Timing Across Tooling, MVP, and Roadmap

Status: Resolved  
Type: Conflict  
Origin: 2026-09-26 whole-set governance, consistency, and completeness audit  
Owner document: `14-mvp.md` / `15-roadmap.md`  
Depends on: TOOL-008, MVP-003, MVP-006, MVP-016, ROAD-006  
Blocks: activation/ship timing of remote external-enrichment capability

### Qualification

`13-tooling-and-dependencies.md` selects `NuGet.Protocol` as an optional remote-feed enrichment adapter and `14-mvp.md` permits explicitly enabled optional remote lookup/enrichment wording, while `15-roadmap.md` places external enrichment in a post-MVP horizon and ROAD-006 says detailed design should remain deferred until promotion. D124 and D153 do not supersede one another.

### Why it matters

Without an explicit resolution, implementation could either expand the MVP by shipping a roadmap capability early or incorrectly discard an already accepted tooling choice. The conflict also affects release tests, network policy, dependency inclusion, and the meaning of the roadmap promotion gate.

### Current understanding

Required remote enrichment is outside the MVP under D147. All external enrichment must remain explicit, configurable, provenance-distinct, and absent from blind/offline scenarios unless deliberately enabled. What is not settled is whether a remote-feed enrichment capability itself may ship as an optional MVP capability, is strictly post-MVP, or whether D124 is only a contingent technology selection for future promotion.

### Resolution criteria

Choose the intended scope/timing explicitly; identify whether D124, D148, D153, D147, or any MVP decision requires clarification or supersession; synchronize `13`, `14`, and `15`; then audit and perform governance synchronization.

### Resolution

The conflict is resolved by applying the established canonical ownership and scope boundaries already present in the document set:

- `14-mvp.md` owns first-release scope and assigns remote enrichment to post-MVP roadmap work;
- `15-roadmap.md` places external enrichment in the post-MVP capability sequence;
- D124 selects `NuGet.Protocol` only as the explicit adapter when remote-feed enrichment is implemented;
- D147 keeps the MVP local/offline and prevents deferred capabilities from silently becoming MVP requirements;
- D153 establishes external enrichment as a later expansion while preserving provenance separation.

Accordingly, `NuGet.Protocol` is a **contingent preselected adapter for a post-MVP capability**, not an MVP-shippable capability. `13-tooling-and-dependencies.md`, `14-mvp.md`, and `15-roadmap.md` were synchronized to state that boundary explicitly. No existing decision required supersession and no new durable decision was necessary because the resolution clarifies the combined effect of D124, D147, and D153 rather than establishing a new project direction.

### Decision reference

D124, D147, D153

### Resolved in

`13-tooling-and-dependencies.md` §11.2 / `14-mvp.md` §17 and §21 / `15-roadmap.md` §6

---

## DOC-021 — Stale Validation Result Terminology After Validation Model Finalization

Status: Resolved  
Type: Audit Finding  
Origin: 2026-09-26 whole-set governance, consistency, and completeness re-audit  
Owner document: `02-terminology.md`  
Depends on: VAL-002, VAL-003, VAL-004, VAL-009, METR-001  
Blocks: None

### Qualification

`02-terminology.md` retained a standalone `Validation Result` definition saying that its representation and confidence semantics belonged to later validation and Evidence Model work. That wording predated the completed `10-validation-strategy.md` and `11-recovery-metrics.md` models, which now canonically distinguish Validation Findings, Validation Runs, and Recovery Metric results.

### Why it matters

Leaving the older wording in the canonical glossary could encourage implementation of a separate undifferentiated validation-result concept, duplicate ownership already settled in `10`/`11`, or imply that validation semantics remain unfinished.

### Current understanding

The current canonical validation model is already settled: a Validation Finding records the outcome of a Validation Check; a Validation Run records governed execution/history; Recovery Metrics summarize explicitly defined populations and measurements. Validation results do not receive Evidence Model inferential confidence labels.

### Resolution criteria

Synchronize the glossary with the finalized validation/metric model without inventing a new validation entity or changing the established ownership boundaries.

### Resolution

`02-terminology.md` now retains `Validation Result` only as generic umbrella wording and explicitly directs new design/implementation to the canonical `Validation Finding`, `Validation Run`, and `Recovery Metric` result concepts owned by `10-validation-strategy.md` and `11-recovery-metrics.md`. The stale statement that these semantics belong to future work was removed.

### Decision reference

None

### Resolved in

`02-terminology.md` §40

---

## GOV-001 — Normative End-to-End Governance Lifecycle

Status: Resolved  
Type: Audit Finding  
Origin: historical joint audit of `90-decisions.md` and `91-design-qualification-register.md`  
Owner document: `91-design-qualification-register.md`  
Depends on: None  
Blocks: None

### Qualification

The governance lifecycle existed only as distributed rules across `90` and `91`, with no single normative end-to-end process.

### Why it matters

Document generation, audit, qualification resolution, decision creation, and canonical updates need one repeatable process.

### Current understanding

The earlier rules were broadly coherent but fragmented.

### Resolution criteria

Encode one authoritative lifecycle and identify its owning governance document.

### Resolution

The complete lifecycle is now encoded in §8 of `91-design-qualification-register.md`.

`91` explicitly owns qualification lifecycle and governance process.

### Decision reference

D020

### Resolved in

`91-design-qualification-register.md` §8

---

## GOV-002 — Qualification Status Transition Rules

Status: Resolved  
Type: Audit Finding  
Origin: historical joint audit of `90-decisions.md` and `91-design-qualification-register.md`  
Owner document: `91-design-qualification-register.md`  
Depends on: GOV-001  
Blocks: None

### Qualification

Qualification statuses existed, but legal transitions and the treatment of previously resolved qualifications were not explicit.

### Why it matters

Without transition rules, history could be rewritten or statuses changed inconsistently.

### Current understanding

Resolved qualifications should retain historical truth.

### Resolution criteria

Define legal transitions and how later reconsideration of a resolved issue is handled.

### Resolution

§4 now defines normal status transitions.

A previously resolved qualification is not normally reopened; a new linked qualification is created if later work materially challenges the previous resolution.

### Decision reference

D020

### Resolved in

`91-design-qualification-register.md` §4

---

## GOV-003 — Decision Replacement Rule

Status: Resolved  
Type: Audit Finding  
Origin: historical joint audit of `90-decisions.md` and `91-design-qualification-register.md`  
Owner document: `90-decisions.md`  
Depends on: None  
Blocks: None

### Qualification

`90` defined the `Superseded` status but did not explicitly state how an accepted decision must be replaced.

### Why it matters

In-place rewriting would destroy historical governance provenance.

### Current understanding

Decision IDs are intended to be stable.

### Resolution criteria

Define the replacement process and require cross-linking old and new decisions.

### Resolution

`90-decisions.md` §2 now requires that replaced decisions remain in place, are marked `Superseded`, and point to a new decision ID which reciprocally references the old one.

### Decision reference

D022

### Resolved in

`90-decisions.md` §2

---

## GOV-004 — Governance Synchronization Check

Status: Resolved  
Type: Audit Finding  
Origin: historical joint audit of `90-decisions.md` and `91-design-qualification-register.md`  
Owner document: `91-design-qualification-register.md` / `90-decisions.md`  
Depends on: GOV-001  
Blocks: None

### Qualification

Because governance state is distributed across several files, updates could leave `90`, `91`, canonical documents, and audits inconsistent.

### Why it matters

A qualification could appear resolved in one place while still being unresolved or absent elsewhere.

### Current understanding

Governance completion requires cross-file consistency.

### Resolution criteria

Define explicit completion checks across all affected governance artifacts.

### Resolution

`91` now contains the authoritative synchronization checklist, and `90` contains a reciprocal governance synchronization rule.

A qualification may only be marked `Resolved` after the synchronization check passes.

### Decision reference

D021

### Resolved in

`91-design-qualification-register.md` §13; `90-decisions.md` §10

---

# 16. Resolved evidence-model qualifications

## EVID-001 — Scope of Evidence

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: None  
Blocks: None

### Qualification

Should **Evidence** be the broad umbrella containing both artifact-derived evidence and developer assertions, or should developer-supplied information sit alongside evidence as a distinct top-level category?

### Why it matters

This affects the formal taxonomy, provenance model, inference rules, and how later stages distinguish original-system evidence from developer-provided recovery input.

### Current understanding

Developer intervention is first-class recovery input but must remain distinguishable from original surviving evidence.

### Resolution criteria

Define the top-level evidence/recovery-input taxonomy and explain how developer assertions participate in inference without being mistaken for original-artifact evidence.

### Resolution

`Recovery Input` is the top-level input concept. Formal `Evidence` and `Developer Input` are separate categories beneath it. Developer Assertions may influence inference but are not formal Evidence and cannot become Original-System Evidence merely by being accepted or used.

### Decision reference

D023

### Resolved in

`03-evidence-model.md` §2
---

## EVID-002 — Observation vs Recovered Fact

Status: Resolved  
Type: Ambiguity  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-001  
Blocks: None

### Qualification

What is the exact operational distinction between an **Observation** and a **Recovered Fact**?

### Why it matters

Without a clear distinction, different analyzers may classify the same information differently and later inference stages may overstate certainty.

### Current understanding

Observation and Recovered Fact are now formally separated by direct extraction versus direct entailment.

### Resolution criteria

Define whether recovered facts are normalized propositions derived from observations, whether all direct observations can become facts, and what evidence threshold applies.

### Resolution

An Observation is a directly extracted/read item from Evidence. A Recovered Fact is a normalized proposition directly entailed by one or more admissible Observations through deterministic, semantics-preserving interpretation, with no heuristic inference, assumption, Developer Assertion, or reconstructed-output support. Not every Observation needs to become a Recovered Fact.

### Decision reference

D024

### Resolved in

`03-evidence-model.md` §3
---

## EVID-003 — Inference Promotion

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-002  
Blocks: None

### Qualification

If an inference is later supported by new independent evidence, can it become a recovered fact or otherwise change classification?

### Why it matters

Recovery is iterative. New artifacts or developer information may strengthen earlier conclusions while provenance and historical reasoning must remain intact.

### Current understanding

The evidence model now uses append-preserving conclusion history.

### Resolution criteria

Define whether classifications are mutable, versioned, superseded, or represented as new conclusions linked to earlier ones.

### Resolution

Classification is not rewritten in place. If later independent Evidence directly establishes what was previously inferred, the original Inference remains in history and a new Recovered Fact is created that supersedes it for current-state purposes.

### Decision reference

D024

### Resolved in

`03-evidence-model.md` §5
---

## EVID-004 — Conflicting Evidence Representation

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-001  
Blocks: None

### Qualification

How should LegacyRevive.NET formally represent evidence that supports incompatible conclusions?

### Why it matters

The system must preserve uncertainty rather than silently choosing one source.

### Current understanding

Conflict is now an explicit evidence-model record rather than an implicit reporting condition.

### Resolution criteria

Define how conflicts are detected, represented, reported, and linked to any later developer decision or resolution.

### Resolution

A Conflict explicitly links incompatible propositions/conclusions and each support path. Conflict state may be Open, Resolved, or Superseded. Resolution never deletes the competing evidence/history, and a Developer Decision that selects a recovery path does not retroactively prove historical correctness.

### Decision reference

D025

### Resolved in

`03-evidence-model.md` §7
---

## EVID-005 — Provenance vs Lineage

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: None  
Blocks: None

### Qualification

Should **Provenance** and **Lineage** remain separate formal concepts, or should lineage be represented as part of provenance?

### Why it matters

The design needs to track both original source and chains of derivation without unnecessary conceptual duplication.

### Current understanding

Origin and derivation are both required for trustworthy recovery traceability.

### Resolution criteria

Define the minimum model needed to trace every material conclusion back to its origins and derivation steps.

### Resolution

`Provenance` is the umbrella concept. It contains origin provenance and derivation lineage. Lineage remains useful vocabulary for the derivation chain but is not a separate top-level traceability model.

### Decision reference

D026

### Resolved in

`03-evidence-model.md` §6
---

## EVID-006 — Final Confidence Vocabulary

Status: Resolved  
Type: Deferred Decision  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-001, EVID-002, EVID-005  
Blocks: None

### Qualification

What confidence/status vocabulary should LegacyRevive.NET use?

### Why it matters

Confidence labels influence reports, developer interpretation, inference handling, and potentially reconstruction decisions.

### Current understanding

The evidence model now separates inferential support strength from conclusion state and epistemic category.

### Resolution criteria

Choose the smallest useful vocabulary and define each label in terms of evidence/provenance rather than intuition.

### Resolution

Inferential support strength uses `Strongly Supported`, `Plausible`, and `Tentative`. `Unresolved` is a recovery state rather than a confidence label. `Confirmed` is not used as a confidence label because direct support is represented by the Recovered Fact classification itself.

### Decision reference

D027

### Resolved in

`03-evidence-model.md` §9
---

## EVID-007 — Confidence Attachment Scope

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-006  
Blocks: None

### Qualification

What can confidence attach to?

### Why it matters

Confidence applied indiscriminately can become misleading.

### Current understanding

Support strength is now restricted to items whose epistemic uncertainty is inferential.

### Resolution criteria

Define which model entities can carry confidence and why.

### Resolution

Support strength may attach to Inferences, inferential Candidate Reconstructions, and individual inferential reconstruction decisions. It does not attach directly to Evidence Sources, Observations, Recovered Facts, Assumptions, whole reconstructed artifacts as one undifferentiated score, or Validation Results.

### Decision reference

D027

### Resolved in

`03-evidence-model.md` §10
---

## EVID-008 — Assumption Lifecycle

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md`  
Depends on: EVID-003  
Blocks: None

### Qualification

How should assumptions be introduced, scoped, reviewed, replaced, invalidated, or retired?

### Why it matters

Assumptions may be necessary to progress recovery, but hidden or immortal assumptions could contaminate later conclusions.

### Current understanding

Assumptions remain visible and separate from fact.

### Resolution criteria

Define assumption states and how downstream conclusions respond when an assumption changes or is invalidated.

### Resolution

The canonical states are `Active`, `Superseded`, `Invalidated`, and `Retired`. Assumptions never silently become Recovered Facts. Invalidating an assumption requires materially dependent downstream conclusions to be reevaluated before remaining current.

### Decision reference

D028

### Resolved in

`03-evidence-model.md` §8
---

## EVID-009 — Developer Correction and Supersession

Status: Resolved  
Type: Dependency  
Origin: historical audit of `02-terminology.md`  
Owner document: `03-evidence-model.md` / `04-developer-intervention.md`  
Depends on: EVID-003, EVID-005  
Blocks: None

### Qualification

When a developer corrects an earlier recovery conclusion, how should the new conclusion supersede the old one without erasing provenance?

### Why it matters

Developer intervention is first-class, recovery state may change over time, and historical reasoning must remain traceable.

### Current understanding

The evidence-level history and downstream invalidation semantics are now defined; detailed interaction/workflow remains for `04-developer-intervention.md`.

### Resolution criteria

Define how correction, supersession, history, and downstream invalidation are represented.

### Resolution

A Developer Correction is recorded as Developer Input and creates/supports a new conclusion or reconstruction decision that supersedes the earlier item. The earlier item and its support path remain. Materially dependent downstream items become `Needs Reevaluation` until recomputed or reaffirmed and become `Invalidated` where valid support no longer remains. `04-developer-intervention.md` will define how corrections are captured and reviewed, not these evidence-history semantics.

### Decision reference

D029

### Resolved in

`03-evidence-model.md` §11; workflow details delegated to `04-developer-intervention.md`
---

## EVID-010 — Circular Evidence Protection

Status: Resolved  
Type: Question  
Origin: historical audit of `02-terminology.md` and earlier false-assumption feedback-loop discussion  
Owner document: `03-evidence-model.md`  
Depends on: EVID-005  
Blocks: None

### Qualification

What evidence is allowed to contribute to later derived observations and inferences without creating circular support or recursive assumption amplification?

### Why it matters

This is one of the core safety properties of the LegacyRevive.NET recovery model.

### Current understanding

The evidence model now defines dependency-aware support rules.

### Resolution criteria

Define permitted dependency relationships between evidence, derived observations, inference, reconstruction, and later inference, and make circular/self-supporting evidence detectable.

### Resolution

Material support is represented as a directed support-dependency graph. A conclusion cannot gain independent support from itself, descendants, reconstructed output generated from it, or re-expression of the same underlying support path. Derived observations and downstream inferences inherit upstream dependencies, and support edges that create a cycle must be rejected or flagged. Validation References never enter the recovery support graph.

### Decision reference

D030

### Resolved in

`03-evidence-model.md` §§12–13
---


# 17. Developer-intervention qualifications

## INTV-001 — Canonical Intervention Kinds

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D005, D023, D029  
Blocks: None

### Qualification

What distinct kinds of deliberate developer participation should the recovery model recognize?

### Why it matters

Assertions of knowledge, choices about how to proceed, and corrections of existing recovery state have different semantics.

### Current understanding

Developer intervention is first-class and Developer Input is separate from Evidence.

### Resolution criteria

Define the smallest useful set of intervention kinds that preserves these semantic differences.

### Resolution

The canonical kinds are Developer Assertion, Developer Decision, and Developer Correction.

### Decision reference

D031

### Resolved in

`04-developer-intervention.md` §§2–5

---

## INTV-002 — Intervention Record and Scope

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D003, D023  
Blocks: None

### Qualification

How should a committed intervention be represented so it cannot become hidden global recovery context?

### Why it matters

Unscoped human input could silently influence unrelated conclusions and weaken provenance.

### Current understanding

Developer Input must retain provenance and be distinguishable from original evidence.

### Resolution criteria

Require explicit representation, provenance, and scope without prematurely fixing storage technology.

### Resolution

Every committed intervention is an explicit Intervention Record carrying kind, content/choice, scope, status, provenance/attribution, affected recovery target/question where applicable, and supersession relationships where applicable.

### Decision reference

D032

### Resolved in

`04-developer-intervention.md` §§6–7

---

## INTV-003 — Intervention Lifecycle

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D029  
Blocks: None

### Qualification

What lifecycle states should committed intervention records use?

### Why it matters

Current recovery state must distinguish interventions that still apply from those that have been replaced or deliberately removed, without erasing history.

### Current understanding

Correction already uses supersession at the evidence-history level.

### Resolution criteria

Define minimal committed states and keep transient UI editing state outside canonical recovery state.

### Resolution

Committed Intervention Records are Active, Superseded, or Withdrawn. Draft/editing states are implementation concerns and do not affect recovery until committed.

### Decision reference

D033

### Resolved in

`04-developer-intervention.md` §§8–9, 21

---

## INTV-004 — Intervention Persistence and Replay

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D014  
Blocks: None

### Qualification

Must developer interventions persist across recovery runs?

### Why it matters

Material intervention can change recovery output. If it exists only in transient context, the same inputs cannot reproduce the same result.

### Current understanding

D014 defines deterministic recovery using the same recorded developer interventions.

### Resolution criteria

Define whether committed intervention state is durable and replayable without selecting storage technology.

### Resolution

Committed material interventions persist as part of recoverable recovery state and are replayable with identity, scope, status, supersession relationships, and sequencing where order is meaningful.

### Decision reference

D034

### Resolved in

`04-developer-intervention.md` §§14–16

---

## INTV-005 — Intervention Precedence vs Evidence

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D023, D024, D025  
Blocks: None

### Qualification

Does developer input override conflicting Evidence or allow a proposition to become a Recovered Fact?

### Why it matters

Treating developer input as superior historical truth would collapse the Evidence Model distinction.

### Current understanding

Developer Input is separate from Evidence and Recovered Facts require direct admissible Evidence.

### Resolution criteria

Define operational use of developer input without changing the evidence category of the underlying proposition.

### Resolution

Developer Input does not automatically outrank Evidence and cannot by itself create a Recovered Fact. A Developer Decision may choose an operational recovery path while the historical conflict remains preserved.

### Decision reference

D035

### Resolved in

`04-developer-intervention.md` §§10–13

---

## INTV-006 — Conflicting Active Interventions

Status: Resolved  
Type: Conflict  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D014, INTV-002, INTV-003  
Blocks: None

### Qualification

What happens when multiple Active interventions give incompatible directions over overlapping scope?

### Why it matters

Silent last-write-wins or incidental execution order would make recovery nondeterministic and difficult to audit.

### Current understanding

Interventions are scoped and deterministic recovery is required where practical.

### Resolution criteria

Define an explicit conflict state and require deliberate resolution.

### Resolution

Overlapping incompatible Active interventions form a Developer Intervention Conflict. Recovery must not silently choose by execution order. Resolution requires supersession, withdrawal, scope correction, or another explicit Developer Decision.

### Decision reference

D036

### Resolved in

`04-developer-intervention.md` §§17–18

---

## INTV-007 — Withdrawal and Downstream Reevaluation

Status: Resolved  
Type: Question  
Origin: Generation/audit of `04-developer-intervention.md`  
Owner document: `04-developer-intervention.md`  
Depends on: D029, INTV-003  
Blocks: None

### Qualification

How should downstream recovery state respond when a materially relied-upon intervention is withdrawn or superseded?

### Why it matters

A conclusion should not remain current merely because its removed premise existed earlier.

### Current understanding

D029 already requires downstream reevaluation after Developer Correction.

### Resolution criteria

Extend that rule coherently to intervention withdrawal and define how a downstream item can become current again.

### Resolution

Materially dependent items become `Needs Reevaluation`. They return to Current after recomputation or traceable reaffirmation, and become `Invalidated` where valid support no longer remains.

### Decision reference

D037

### Resolved in

`04-developer-intervention.md` §§21–22

---


# 18. Convention-inference qualifications

## CONV-001 — Convention Observation Semantics

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D006  
Blocks: None

### Qualification

What exactly may LegacyRevive.NET claim when it detects a recurring convention?

### Why it matters

Pattern recurrence is useful but can easily be overstated as historical intent or universal architectural policy.

### Current understanding

D006 already requires conventions to remain derived observations rather than facts.

### Resolution criteria

Define the canonical epistemic category and wording boundary.

### Resolution

A Convention Observation is a scoped Derived Observation describing recurrence. It is not a Recovered Fact about original developer intent or proof of a universal historical design rule.

### Decision reference

D038

### Resolved in

`05-convention-inference.md` §§2, 15

---

## CONV-002 — Eligible Convention Support and Feedback Protection

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D030  
Blocks: None

### Qualification

What recovery information may support convention inference without creating self-corroborating feedback?

### Why it matters

A convention used to generate reconstruction could otherwise appear stronger when the generated output is scanned later.

### Current understanding

D030 prohibits descendants and reconstructed output from independently corroborating their ancestors.

### Resolution criteria

Apply the support-dependency rules specifically to convention inference.

### Resolution

Convention inference may use admissible provenance-bearing recovery information. Generated/reconstructed output shaped by a Convention Observation cannot independently support that observation; downstream items inherit the original dependency path.

### Decision reference

D039

### Resolved in

`05-convention-inference.md` §§3–4

---

## CONV-003 — Convention Scope and Generalization

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D006  
Blocks: None

### Qualification

How broadly may a convention inferred in one part of a recovery target be applied?

### Why it matters

Legacy systems may contain different conventions in different regions, and local recurrence must not silently become a global rule.

### Current understanding

Convention observations require provenance but scope had not been formally defined.

### Resolution criteria

Require explicit scope and define how broader generalization occurs.

### Resolution

Every Convention Observation declares observed scope. Broader generalization creates a new supported Derived Observation or Inference with its own provenance and coverage rather than silently widening the original.

### Decision reference

D040

### Resolved in

`05-convention-inference.md` §§6–7

---

## CONV-004 — Convention Recurrence Threshold

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D012  
Blocks: None

### Qualification

What threshold makes a repeated pattern a convention?

### Why it matters

An arbitrary universal threshold could imply unjustified statistical precision across very different convention domains.

### Current understanding

A single occurrence is insufficient, but support requirements vary by domain and scope.

### Resolution criteria

Define recurrence without inventing a universal percentage or count.

### Resolution

A convention requires recurrence, but no universal numeric threshold applies across all domains. Support is explained using domain-appropriate counts, source independence, scope coverage, consistency, and exceptions.

### Decision reference

D041

### Resolved in

`05-convention-inference.md` §§8, 10

---

## CONV-005 — Convention Exceptions

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: CONV-004  
Blocks: None

### Qualification

How should observations that do not match an inferred convention be treated?

### Why it matters

Discarding exceptions would exaggerate pattern strength and may hide scope boundaries or meaningful variation.

### Current understanding

Convention inference must preserve uncertainty and provenance.

### Resolution criteria

Define whether exceptions are retained as part of the observation.

### Resolution

Material exceptions are part of the Convention Observation and must remain visible. Their cause is not inferred without additional support.

### Decision reference

D042

### Resolved in

`05-convention-inference.md` §9

---

## CONV-006 — Convention Support Profile vs Confidence

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D027  
Blocks: None

### Qualification

Should a Convention Observation itself carry the Evidence Model's inferential confidence labels?

### Why it matters

Convention Observation is a Derived Observation; applying inferential labels directly would blur observation versus inference.

### Current understanding

D027 restricts support strength to inferential conclusions.

### Resolution criteria

Define how convention support is communicated without changing its epistemic category.

### Resolution

Convention Observations expose descriptive support profiles: provenance, scope, support counts, independence, coverage where meaningful, and exceptions. `Strongly Supported`, `Plausible`, and `Tentative` apply only to later Inferences based on the convention.

### Decision reference

D043

### Resolved in

`05-convention-inference.md` §§10–12

---

## CONV-007 — Competing Convention Observations

Status: Resolved  
Type: Conflict  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: CONV-003  
Blocks: None

### Qualification

How should incompatible convention observations be handled?

### Why it matters

Multiple conventions may legitimately coexist by scope, while true overlap should not be resolved invisibly.

### Current understanding

Conflict handling elsewhere in the Evidence Model is explicit and history-preserving.

### Resolution criteria

Distinguish scoped coexistence from materially overlapping incompatibility.

### Resolution

Different scoped conventions may coexist. Materially incompatible Convention Observations claiming overlapping scope form a Convention Conflict preserving both observations, scopes, support profiles, provenance, and exceptions.

### Decision reference

D044

### Resolved in

`05-convention-inference.md` §§16–17

---

## CONV-008 — Convention History and Reevaluation

Status: Resolved  
Type: Question  
Origin: Generation/audit of `05-convention-inference.md`  
Owner document: `05-convention-inference.md`  
Depends on: D024, D029, D030  
Blocks: None

### Qualification

What happens when new information or corrections materially change the support for an existing Convention Observation?

### Why it matters

Silently rewriting a convention would lose recovery history; continuing to use stale convention knowledge could contaminate later interventions.

### Current understanding

The Evidence Model uses append-preserving history and dependency-driven reevaluation.

### Resolution criteria

Apply those rules to convention observations.

### Resolution

Updated Convention Observations supersede earlier records rather than rewriting them. Material dependency changes mark the convention `Needs Reevaluation`; it must not guide new recovery decisions until recomputed or deliberately restored to current status.

### Decision reference

D045

### Resolved in

`05-convention-inference.md` §§18–19

---


# 19. Recovery-matrix qualifications

## RMAT-001 — Recovery Matrix Contribution Classes

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D002  
Blocks: None

### Qualification

How should the matrix distinguish information directly present in an artifact from progressively weaker recovery use?

### Why it matters

Without explicit contribution classes, artifact capability descriptions could silently promote inference to fact.

### Current understanding

The Evidence Model already separates direct support, inference, and weaker heuristic reasoning.

### Resolution criteria

Define a compact matrix vocabulary aligned with the Evidence Model.

### Resolution

Matrix contributions use `Direct`, `Inferable`, `Heuristic`, and `Not established by this artifact alone`.

### Decision reference

D046

### Resolved in

`06-recovery-matrix.md` §2

---

## RMAT-002 — Artifact Type vs Artifact Instance Capability

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D002  
Blocks: None

### Qualification

Does a capability listed for an artifact type imply that every artifact instance exposes it?

### Why it matters

Build options, stripping, corruption, and format variants can change recoverable content.

### Current understanding

Observations must come from actual recovery input rather than theoretical capability.

### Resolution criteria

Separate matrix capability from observed instance content.

### Resolution

Artifact-type capability is conditional. A specific capability is claimed for an artifact instance only after inspection confirms the relevant content.

### Decision reference

D047

### Resolved in

`06-recovery-matrix.md` §§3, 23

---

## RMAT-003 — Artifact Classification Beyond Extension

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: RMAT-002  
Blocks: None

### Qualification

Can `.dll` and `.exe` extension alone determine assembly/source recovery capability?

### Why it matters

Managed implementation assemblies, reference assemblies, native hosts, and satellite assemblies can share common extensions while exposing different recoverable information.

### Current understanding

Actual artifact format must be identified first.

### Resolution criteria

Define distinct capability treatment for materially different formats.

### Resolution

Extension alone is insufficient. Managed implementation assemblies, reference assemblies, native/non-managed hosts, satellite assemblies, and other relevant variants receive distinct capability treatment.

### Decision reference

D048

### Resolved in

`06-recovery-matrix.md` §§6–8, 16

---

## RMAT-004 — Missing Artifact Semantics

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D002  
Blocks: None

### Qualification

What may be concluded when an expected artifact type is absent from the supplied recovery set?

### Why it matters

A deployment/capture may omit build-time, documentation, debugging, or other artifacts.

### Current understanding

Missing evidence should not be converted into unsupported historical fact.

### Resolution criteria

Define a conservative absence rule.

### Resolution

Absence normally means unavailable to the current recovery, not historically nonexistent. Stronger absence claims require independent support.

### Decision reference

D049

### Resolved in

`06-recovery-matrix.md` §4

---

## RMAT-005 — Companion Artifact Corroboration

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D003, D030  
Blocks: None

### Qualification

When may one surviving artifact be used to interpret or corroborate another?

### Why it matters

Incorrect pairing could contaminate recovery, while circular support could falsely increase confidence.

### Current understanding

Relationships and support paths require provenance and independence.

### Resolution criteria

Require a defensible artifact relationship and preserve support-path independence.

### Resolution

Companion artifacts may corroborate only when their relationship is defensibly supported. Additional support strength requires independent support paths; filename similarity alone is not automatically sufficient.

### Decision reference

D050

### Resolved in

`06-recovery-matrix.md` §§5, 20–21

---

## RMAT-006 — Matrix vs Artifact-Instance Output

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: RMAT-002  
Blocks: None

### Qualification

Should the Recovery Matrix itself represent what was found in a particular recovery run?

### Why it matters

Mixing theoretical capability with scan results would make the canonical matrix target-specific and misleading.

### Current understanding

The matrix should remain reusable across recovery targets.

### Resolution criteria

Separate canonical capability description from per-artifact scan findings.

### Resolution

The matrix owns artifact-type capabilities/limits. Actual runs produce Artifact-Instance Profiles recording detected format, observations, inferential opportunities, limitations, relationships, and unresolved conditions.

### Decision reference

D051

### Resolved in

`06-recovery-matrix.md` §23

---

## RMAT-007 — Recovery Ceiling Wording

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D001  
Blocks: None

### Qualification

When should the tool say information "cannot be recovered"?

### Why it matters

A later-discovered artifact may contain information absent from the current set, while some historical information genuinely may not survive compilation/deployment.

### Current understanding

LegacyRevive.NET must avoid fictional certainty and overclaiming.

### Resolution criteria

Define conservative wording that remains true as the artifact set changes.

### Resolution

The default statement is `not established` or `not reliably recoverable from the available artifact set`. Stronger universal impossibility language requires a stronger technical basis.

### Decision reference

D052

### Resolved in

`06-recovery-matrix.md` §24

---

## RMAT-008 — Cross-Artifact Recovery Semantics

Status: Resolved  
Type: Question  
Origin: Generation/audit of `06-recovery-matrix.md`  
Owner document: `06-recovery-matrix.md`  
Depends on: D002, D008, D030  
Blocks: None

### Qualification

Does combining multiple artifact types allow LegacyRevive.NET to collapse their contributions into a single recoverability classification/score?

### Why it matters

Different artifacts may strengthen different conclusions without making every conclusion equally certain.

### Current understanding

Validation and recovery support must preserve dimensions and epistemic categories.

### Resolution criteria

Define how cross-artifact combination affects classification.

### Resolution

Cross-artifact combination may strengthen support but preserves Direct/Inference/Heuristic distinctions and conclusion-level provenance. It must not be reduced to one opaque recoverability score.

### Decision reference

D053

### Resolved in

`06-recovery-matrix.md` §§20, 25

---


## RMAT-009 — Stage 02 Artifact Format and Family Recognition Contract

Status: Resolved  
Type: Boundary Issue  
Origin: Post-implementation review of rejected `DEV-SPEC-004-stage-02-artifact-format-and-family-classification.md`, 2026-09-30  
Owner document: `06-recovery-matrix.md` / `07-recovery-process.md`  
Depends on: D002, D047, D048, D051, D054, D055, D134  
Blocks: replacement Stage 02 Artifact format/family classification Development Slice Specification and implementation

### Qualification

What directly observable conditions are required to establish each MVP-relevant Stage 02 Artifact format/family, and how should the canonical model distinguish sufficient recognition, insufficient recognition, malformed/corrupt candidate content, valid-but-unsupported content, ambiguity, conflicting or overlapping format signals, and operational classification failure without crossing improperly into Stage 03 semantic extraction?

Which parts of that recognition contract are owned by the Recovery Matrix as artifact-family semantics, and which parts are owned by the Recovery Process as the Stage 02 / Stage 03 process boundary?

### Why it matters

The existing canonical model requires LegacyRevive.NET to inspect the actual Artifact instance, distinguish actual format from filename extension, classify materially different Artifact families, and preserve corrupt, unsupported, unreadable, and ambiguous conditions explicitly.

However, the rejected DEV-SPEC-004 attempt demonstrated that these broad requirements do not by themselves determine the materially significant recognition rules needed for implementation.

Without a sufficiently explicit canonical recognition contract, an implementation agent must choose product-semantic thresholds itself, such as:

- what structure is sufficient to recognize `.runtimeconfig.json`;
- what structure is sufficient to recognize `.deps.json`;
- what directly establishes a reference assembly;
- what directly establishes a satellite/resource assembly;
- how a structurally valid managed artifact outside the supported family set is represented;
- when candidate content is malformed rather than merely unsupported or unrecognized;
- how overlapping or conflicting recognition signals are handled; and
- how much internal inspection Stage 02 may perform before it becomes Stage 03 extraction.

Those choices affect the truth conditions of Artifact-instance classification and therefore must not arise accidentally from parser implementation.

The rejected implementation is evidence that the design boundary was insufficiently specified. Its chosen rules are not evidence that those rules are correct.

### Current understanding

The following direction is already accepted:

- Artifact-type capability does not establish Artifact-instance content; actual Artifact instances must be inspected.
- Filename extension alone is insufficient to establish actual Artifact format.
- The Recovery Matrix distinguishes materially different Artifact families because they have materially different recovery capabilities and limitations.
- Artifact-instance findings remain separate from Recovery Matrix capability statements.
- Stage 02 detects actual Artifact format/type, distinguishes it from extension, classifies the Artifact family, and preserves corrupt, unreadable, unsupported, and ambiguous conditions.
- Stage 03 performs direct extraction and normalization of artifact content after classification.
- Stage progression does not promote an item's epistemic category.
- The MVP already identifies the Artifact families that require analyzer/classification coverage.

What is not yet established sufficiently is the normative recognition contract connecting direct Artifact-instance observations to a particular Stage 02 format/family conclusion.

The issue does not by itself expand MVP Artifact-family coverage. A valid artifact that lies outside the supported family set may require an explicit truthful outcome without thereby becoming a new MVP deep-analysis capability.

### Resolution criteria

This qualification can be resolved when the project has deliberately established:

1. the canonical ownership boundary between artifact-family recognition semantics in `06-recovery-matrix.md` and Stage 02 / Stage 03 process semantics in `07-recovery-process.md`;
2. the general canonical recognition model applicable to Stage 02 Artifact classification;
3. for each MVP-relevant Artifact family, what kinds of directly observable characteristics may be:
   - required;
   - sufficient;
   - insufficient by themselves;
   - corroborating but non-decisive;
   - conflicting;
4. how the recognition model represents:
   - directly established classification;
   - broader-but-not-more-specific classification;
   - unsupported or unrecognized readable content;
   - structurally valid but unsupported Artifact formats;
   - malformed/corrupt candidate content;
   - ambiguous classification;
   - overlapping recognition signals;
   - operational read/parser/classification failure;
5. how recognition precedence or overlap is handled where one Artifact satisfies characteristics of more than one candidate family;
6. how much structural inspection Stage 02 may perform solely to establish classification without persisting unrelated Stage 03 observations;
7. whether recognition criteria require artifact-specific canonical sections, a common recognition-contract structure, or another owner-consistent representation;
8. how representative positive, negative, boundary, malformed, ambiguous, and adversarial examples should demonstrate the accepted semantics without turning test fixtures into the canonical definition; and
9. what synchronization is required across the Recovery Matrix, Recovery Process, MVP references, decision log, qualification register, and later Development Slice Specification.

### Resolution

RMAT-009 is resolved by establishing a canonical Stage 02 Artifact format/family recognition contract split across the two existing owners rather than introducing a new semantic owner.

`06-recovery-matrix.md` now owns the format/family **recognition truth conditions**. Classification is based on directly inspected preserved bytes/structure, never extension alone. Stage 02 records the most specific directly established family, retains a broader format where subtype evidence is insufficient, and preserves distinct outcomes for valid-but-unsupported, unsupported/unrecognized, ambiguous, malformed/corrupt candidate, and operational classification failure. Malformed-family status requires content-level candidate evidence; filename/extension alone cannot create it. Compatible hierarchical classifications use the most specific directly established subtype, while incompatible independently supported classifications remain ambiguous rather than being resolved by recognizer order.

The Recovery Matrix now defines minimum recognition contracts for the current MVP families: managed CLI artifact/assembly, reference assembly, satellite/resource assembly, managed implementation assembly, native/non-managed PE, classic XML configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, and `.nupkg`. A valid managed CLI module/netmodule without an Assembly manifest is explicitly valid-but-unsupported for current MVP deep analysis rather than malformed.

`07-recovery-process.md` now owns the Stage 02 workflow and Stage 02 / Stage 03 boundary. Stage 02 may inspect internal metadata/container/XML/JSON/resource structure only as far as necessary to decide the classification contract and persists only classification-bearing state/diagnostics. Reusable semantic content remains Stage 03 direct extraction/normalization. Later extraction may trigger normal reevaluation if it reveals that the earlier recognition contract was not actually satisfied; history is not rewritten in place.

`14-mvp.md` is synchronized to make clear that its analyzer list defines MVP coverage, while the recognition truth conditions remain owned by `06`/`07`; valid known formats outside the analyzer set remain truthful preserved state rather than being relabeled malformed.

The technical recognition criteria were checked against the existing external technical baseline used by `06`, including .NET runtime/dependency configuration, reference-assembly, satellite-resource, Portable PDB, PE/CLI, and NuGet format documentation. The governed audit found no conflict with the Evidence Model, architecture analyzer boundary, tooling ownership, or MVP scope.

### Decision reference

D172

### Resolved in

`06-recovery-matrix.md` §5A; `07-recovery-process.md` §4; synchronized in `14-mvp.md` §4.1

---

## RMAT-010 — Malformed Family-Candidate Evidence Threshold

Status: Resolved  
Type: Ambiguity  
Origin: Post-implementation review of rejected `DEV-SPEC-005-stage-02-artifact-format-and-family-classification.md`, 2026-10-01  
Owner document: `06-recovery-matrix.md`  
Depends on: RMAT-009, D172, D173  
Blocks: readiness of the replacement Stage 02 Artifact format/family classification Development Slice Specification

### Qualification

What minimum directly observable structural evidence is required before content that fails full recognition may be represented as a malformed/corrupt candidate of a known Stage 02 Artifact family, rather than remaining unsupported/unrecognized content or a broader valid format?

In particular, when the enclosing XML, JSON, metadata, archive, or comparable structure is itself malformed or only partially readable, what evidence is sufficient to establish that the content is genuinely a candidate of the claimed family without allowing filename, extension, incidental substrings, partial name collisions, parser accidents, or unrelated embedded text to fabricate family membership?

### Why it matters

RMAT-009 / D172 established that malformed-family state requires content-level family-candidate evidence and that extension, filename, generic syntax, parser rejection, and unrelated valid formats are insufficient. It also established family-specific successful-recognition contracts.

Post-implementation review of DEV-SPEC-005 nevertheless exposed a remaining semantic boundary. The implementation used permissive lexical checks such as occurrence of `<configuration`, `<doc`, or JSON property-name text to infer malformed family candidates. Those checks can produce materially different Stage 02 claims from stricter token-aware or structural approaches while each might plausibly be argued to satisfy wording such as "content-level candidate" or "family-specific structure is otherwise established."

The issue is therefore not whether malformed-family claims require direct evidence; that is already settled. The unresolved point is the **minimum admissible candidate-evidence threshold when full structural recognition cannot complete**.

This boundary affects what LegacyRevive.NET claims about supplied Original Artifact bytes. A false malformed-family claim is stronger than preserving the content as unsupported/unrecognized and would collapse uncertainty into an unsupported classification. Conversely, a threshold that is unnecessarily strict could lose directly supportable malformed-family information.

The `.nupkg` rule for a readable ZIP is not reopened by this qualification: RMAT-009/D172 already establishes that a valid ZIP without a recognized NuSpec remains a valid non-NuGet ZIP/unsupported format, not a malformed NuGet package. The qualification concerns the general and family-specific threshold for establishing malformed/corrupt **candidate** evidence when full recognition fails.

### Current understanding

The following remain accepted and are not reopened:

- Stage 02 classification is based on directly inspected preserved bytes/structure, not extension or filename alone.
- Successful family recognition uses the minimum contracts already established by RMAT-009/D172.
- Malformed/corrupt candidate state is distinct from unsupported/unrecognized content, valid-but-unsupported format, ambiguity, and operational classification failure.
- Parser rejection alone does not automatically establish corruption or family membership.
- A valid broader format remains the truthful result when subtype evidence is insufficient.
- Tool/library categories do not define LegacyRevive.NET product semantics.
- Reusable semantic extraction remains Stage 03.

What remains unresolved is whether and how partial structural evidence below the successful-recognition threshold may establish family candidacy strongly enough to support a malformed/corrupt family claim.

### Resolution criteria

This qualification can be resolved when the project has deliberately established:

1. a general rule for the minimum direct evidence required to establish malformed/corrupt family candidacy when full recognition fails;
2. whether that threshold must be structural/token-aware rather than raw substring/name occurrence, and how incidental textual occurrence is excluded;
3. how the rule distinguishes:
   - malformed known-family candidate;
   - unsupported/unrecognized malformed content;
   - valid broader format with unrecognized subtype content;
   - valid other format;
   - operational parser/tool failure;
4. any family-specific candidate thresholds needed for XML configuration, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, `.nupkg`, Portable PDB/metadata, PE/CLI, or other in-scope Stage 02 families;
5. how prefix/name collisions and incidental embedded text are prevented from establishing family evidence;
6. how partial-read or corrupt-container evidence is treated without allowing implementation-specific parser behavior to define the product claim;
7. whether the existing wording in `06-recovery-matrix.md` §5A is sufficient after clarification or requires explicit candidate-recognition clauses per family; and
8. what synchronization is required across D172, `06-recovery-matrix.md`, `07-recovery-process.md`, `16-development-process.md`, the replacement DEV-SPEC, and implementation-facing guidance.

### Resolution

RMAT-010 is resolved by establishing that malformed/corrupt family candidacy is itself an evidence-bearing classification claim and therefore requires a directly observed **family candidate discriminator** at the correct structural scope. The discriminator must be established independently of the condition that failed and must be strong enough to distinguish genuine family candidacy from naming, incidental text, generic outer-format structure, or parser behavior.

The general rule is now canonical in `06-recovery-matrix.md` §5A.5.1:

- filename, extension, supplied path, raw substring occurrence, partial-name/prefix collision, incidental text/string/payload content, generic parser rejection, and tool-specific error categories are insufficient by themselves;
- deterministic partial structural/token inspection is permitted when complete parsing fails, but it must establish exact canonical tokens/records at the required scope rather than search raw text;
- parser/tokenizer mechanics remain implementation discretion only where they preserve the same canonical candidate result;
- if the discriminator is not established, Stage 02 retains a broader valid format where available or preserves unsupported/unrecognized content and diagnostics rather than fabricating malformed family membership.

The Recovery Matrix now defines family-specific thresholds for managed CLI, XML configuration, Portable PDB, XML documentation, `.deps.json`, `.runtimeconfig.json`, `packages.config`, `.nuspec`, and `.nupkg`, while preserving broad managed assembly/subtype fallback semantics. In particular, malformed JSON candidacy requires root/top-level structural member evidence rather than raw property-name text; XML candidacy requires exact document-element/direct-child structural evidence rather than textual prefix matching; Portable-PDB candidacy requires structurally identified `#Pdb` stream evidence; and corrupt-NuGet candidacy requires structured root-NuSpec evidence plus manifest content that independently establishes NuSpec candidacy.

The existing readable-ZIP NuGet rule is unchanged: a valid ZIP without a NuSpec satisfying the successful NuSpec recognition contract remains a valid non-NuGet ZIP/unsupported format, not a malformed NuGet package.

`07-recovery-process.md` Stage 02 is synchronized to require these discriminators before a malformed/corrupt family claim may be emitted. No Stage 03 boundary, MVP analyzer scope, Evidence Model category, architecture boundary, or tooling choice is changed. `16-development-process.md` requires no RMAT-010-specific semantic change because its existing D173 readiness gate already routes unresolved recognition thresholds to governance; the separate verification-process strengthening remains owned by open DEV-004.

A targeted governance audit found no conflict with RMAT-009/D172, D173 semantic readiness, Stage 02/Stage 03 ownership, the Evidence Model, architecture/tooling ownership, or MVP scope. DEV-004 remains Open and is now able to define the separate verification/falsification discipline against this settled semantic basis; RMAT-010 itself no longer blocks the replacement Stage 02 specification once DEV-004 is resolved.

### Decision reference

D175

### Resolved in

`06-recovery-matrix.md` §5A.5.1 and §§26–27; synchronized with `07-recovery-process.md` §4. DEV-004 remains the open development-process verification qualification.

---

# 20. Recovery-process qualifications

## PROC-001 — Canonical Recovery Process Shape

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D010  
Blocks: None

### Qualification

Should recovery be rigidly linear, fully ad hoc, or structured with iteration?

### Resolution

Recovery uses a canonical conceptual stage sequence from intake through baseline while supporting explicit iteration and reevaluation.

### Decision reference

D054

### Resolved in

`07-recovery-process.md` §§2–15

---

## PROC-002 — Stage Progression and Epistemic Category

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D002  
Blocks: None

### Qualification

Does later-stage use make information more certain?

### Resolution

No. Stage progression does not change epistemic category. Stronger classification requires new admissible support under the Evidence Model.

### Decision reference

D055

### Resolved in

`07-recovery-process.md` §14

---

## PROC-003 — Earliest-Affected-Stage Reevaluation

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D029, D037, D045  
Blocks: None

### Qualification

How much recovery should rerun after material upstream change?

### Resolution

Dependent state is reevaluated from the earliest materially affected stage while unaffected valid work remains current.

### Decision reference

D056

### Resolved in

`07-recovery-process.md` §§16–17

---

## PROC-004 — Late Artifact Intake

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D011, D047  
Blocks: None

### Qualification

How do newly discovered Original Artifacts enter an existing recovery?

### Resolution

They re-enter through intake/inventory/classification/direct extraction, after which materially dependent state is reevaluated.

### Decision reference

D057

### Resolved in

`07-recovery-process.md` §18

---

## PROC-005 — Developer Intervention Placement

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D005, D031-D037  
Blocks: None

### Qualification

Is Developer Intervention restricted to one process stage?

### Resolution

No. It may occur at explicit recovery questions throughout the process while obeying `04-developer-intervention.md`.

### Decision reference

D058

### Resolved in

`07-recovery-process.md` §§8, 19

---

## PROC-006 — Build Diagnostic Semantics

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D002, D009  
Blocks: None

### Qualification

What does a build diagnostic establish about the historical original system?

### Resolution

It directly establishes a condition of the current candidate/environment and may support recovery inference, but does not by itself prove the exact historical project declaration.

### Decision reference

D059

### Resolved in

`07-recovery-process.md` §10

---

## PROC-007 — Workspace Run Checkpoint Model

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D014, D034  
Blocks: None

### Qualification

How is durable recovery context distinguished from one execution?

### Resolution

Recovery Workspace is durable context, Recovery Run is one execution, and Recovery Checkpoint is an identified reproducible state.

### Decision reference

D060

### Resolved in

`07-recovery-process.md` §§21–23

---

## PROC-008 — Partial Blocked and Baseline States

Status: Resolved  
Type: Question  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D001  
Blocks: None

### Qualification

Is recovery successful only if all stages complete?

### Resolution

No. Canonical states include Partial, Blocked, Workable Baseline, and Validated Baseline.

### Decision reference

D061

### Resolved in

`07-recovery-process.md` §§24–27

---

## PROC-009 — Validation Feedback Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `07-recovery-process.md`  
Owner document: `07-recovery-process.md`  
Depends on: D007  
Blocks: None

### Qualification

Can validation improve recovery without leaking withheld reference material?

### Resolution

Validation findings may trigger reevaluation, but Validation References remain outside Recovery Input and cannot be repackaged as Evidence or Developer Input.

### Decision reference

D062

### Resolved in

`07-recovery-process.md` §§11, 30

---

## PROC-010 — Incomplete Supplied-Directory Enumeration During Intake

Status: Resolved  
Type: Boundary Issue  
Origin: Post-implementation review of `DEV-SPEC-001 — Workspace + Local Artifact Intake`  
Owner document: `07-recovery-process.md`  
Depends on: D003, D049, D054, D060, D061, D113, D114, D134, D141, D167, D168  
Blocks: Follow-up Stage 01 intake-enumeration Development Slice until resolution

### Qualification

What canonical Stage 01 state must LegacyRevive.NET establish when a supplied local directory tree cannot be completely enumerated, including root-enumeration failure, narrower subtree-enumeration failure, files safely discovered before a later enumeration failure, and the distinction between undiscovered possible contents and an individually discovered file that later fails preservation?

In particular, when is a Recovery Intake Snapshot valid, what does such a snapshot claim about completeness of the supplied artifact set, what state is Partial versus Blocked, and what diagnostic scope applies where no Artifact identity exists?

### Why it matters

Stage 01 is required to identify the supplied artifact set and produce a Recovery Intake Snapshot. Existing canonical rules also require scoped diagnostics, preservation of safe partial progress, persistent/replayable state, and explicit Blocked/Partial conditions.

An enumeration failure can occur before an Artifact identity exists for affected material. Treating that failure as an ordinary Artifact-preservation failure would fabricate an artifact-level scope, while silently treating a partially traversed tree as complete could overstate what intake established. The semantics affect Stage 01 correctness, provenance, replay, diagnostics, MVP local-input behavior, and implementation acceptance.

### Current understanding

Before resolution, the canonical model already established that:

- Stage 01 must identify the supplied artifact set before downstream recovery;
- Recovery Intake Snapshot describes immutable recovery inputs and run context;
- recovery is not universally all-or-nothing;
- safe partial progress and explicit Blocked state are canonical;
- diagnostics should be scoped to the affected capability/item;
- every discovered MVP file is inventoried;
- artifact absence does not establish historical nonexistence;
- Artifact identities apply to admitted artifact instances;
- failed preservation of an already admitted Artifact is distinct from inability to discover possible contents of an unenumerable directory scope.

The canonical documents did not previously state whether incomplete directory enumeration could produce a Recovery Intake Snapshot, how snapshot completeness was represented, or how root-enumeration failure differed from local subtree-enumeration failure.

### Resolution criteria

Resolution required explicit answers for:

1. whether workspace/run state and discovery diagnostics may exist when discovery fails;
2. whether root-enumeration failure may create a Recovery Intake Snapshot;
3. whether safely discovered files may be retained when a narrower subtree cannot be enumerated;
4. how incomplete enumeration is represented as Partial and/or Blocked;
5. the minimum diagnostic scope where no Artifact exists;
6. whether a Recovery Intake Snapshot records discovery completeness;
7. the distinction between undiscovered possible content and an admitted Artifact that later fails preservation;
8. replay/persistence requirements;
9. the MVP and implementation-development consequences.

### Resolution

Stage 01 now canonically distinguishes supplied-source discovery, Artifact admission, and Artifact preservation.

Artifact identity is created only for a file actually discovered and admitted. Enumeration failure never creates placeholder Artifacts or claims about undiscovered file existence.

If the supplied root cannot be enumerated sufficiently to establish a trustworthy supplied artifact set, the discovery scope is Blocked and no Recovery Intake Snapshot is produced as though enumeration succeeded; specifically, failure to enumerate the root is not equivalent to successfully enumerating an empty directory. Workspace/run state and a root-scoped discovery diagnostic may be persisted where infrastructure has already been established.

If a narrower subtree fails while other files have been safely discovered, trustworthy discovered state may be retained. The failed subtree/source scope is Blocked, the overall Recovery Run may be Partial, and admitted files proceed through normal preservation. Any Recovery Intake Snapshot produced in this condition records discovery incompleteness and the applicable failed scope/diagnostic context; it represents admitted artifacts rather than claiming complete knowledge of all files beneath the supplied tree.

A file that was individually discovered/admitted and later becomes unreadable or fails byte preservation remains an Artifact-scoped preservation failure. Where discovery failed before an Artifact existed, the diagnostic remains intake/source/directory/subtree scoped.

The MVP is not expanded into a full filesystem graph or transactional filesystem snapshot model. It must preserve source/root identity, safely discovered files, discovery-completeness state, known failed discovery scopes/diagnostics, and existing Artifact preservation state.

`12-architecture.md` and `14-mvp.md` were synchronized with these semantics. `DEV-SPEC-001` remains the historical record of its implemented acceptance baseline; the newly governed enumeration-failure behavior requires a follow-up Development Slice rather than retroactive rewriting of that baseline.

### Decision reference

D170

### Resolved in

`07-recovery-process.md` §§3, 24–26; `12-architecture.md` §§17–18, 20; `14-mvp.md` §4

---


## PROC-011 — Positive File Discovery Before Later Descendant-Discovery Failure

Status: Resolved  
Type: Ambiguity  
Origin: Post-implementation audit of `DEV-SPEC-002 — Intake Enumeration Failure Handling`  
Owner document: `07-recovery-process.md`  
Depends on: D003, D049, D061, D113, D114, D134, D168, D170; PROC-010  
Blocks: Follow-up Stage 01 positive-discovery/descendant-failure Development Slice until resolution

### Qualification

When Stage 01 successfully establishes the supplied file instances directly discoverable within a known directory, but subsequent discovery of possible child-directory or descendant scopes for that same directory fails, does the successfully established direct-file state remain trustworthy discovered state eligible for ordinary Artifact admission/preservation?

In particular, is successful positive file discovery independently established Stage 01 state, or is it provisional until all discovery operations associated with the containing directory succeed; how does this apply when the containing directory is the supplied root; when is the resulting run Partial versus wholly Blocked; and what diagnostic scope applies without introducing unsupported directory or filesystem entities?

### Why it matters

PROC-010 / D170 established that trustworthy discovered state may survive narrower discovery failure and that failures should not fabricate Artifacts or invalidate unrelated recovery state.

The DEV-SPEC-002 traversal implementation nevertheless materialized direct files and child directories inside one failure boundary and committed the direct-file discoveries only after both operations succeeded. A later child-directory discovery failure could therefore discard file instances whose direct enumeration had already completed successfully. At the supplied root, that implementation behavior could collapse partially established positive discovery into a total root failure with no discovered files.

The canonical documents did not explicitly define whether those two discovery activities form one atomic semantic discovery unit. The assumption affects Artifact admission, Partial/Blocked state, Recovery Intake Snapshot validity, diagnostic scope, persistence, replay, determinism, and MVP intake behavior, so it cannot be settled merely by rearranging filesystem calls or adding a test.

### Current understanding

Before resolution, the following was already established:

- Stage 01 identifies the supplied artifact set only to the extent actually established by discovery.
- Artifact identity is created only for supplied files actually discovered and admitted.
- Trustworthy completed work remains usable where partial recovery is safe.
- Discovery failure should be scoped to the narrowest reliably established boundary and should not automatically invalidate unrelated recovery state.
- A narrower Blocked discovery scope may coexist with a Partial Recovery Run.
- Undiscovered possible contents must not be fabricated as Artifacts or treated as known absent.
- An incomplete Recovery Intake Snapshot represents actually admitted Artifact instances and does not claim complete knowledge of the supplied tree.
- A supplied root that cannot be enumerated sufficiently to establish a trustworthy supplied Artifact set remains Blocked and must not masquerade as known-empty successful discovery.
- No accepted direction requires a filesystem graph, directory identity model, filesystem journal, or transactional point-in-time source snapshot.
- DEV-SPEC-002 is an Implemented, historically stable Development Slice Specification and must not be retroactively rewritten to make this later issue appear part of its original acceptance record.

What remained unsettled was whether a completed positive file enumeration within a directory is independently trustworthy when a later discovery operation for descendant directory scopes from that same directory fails.

### Resolution criteria

Resolution required explicit answers for:

1. when a positively discovered supplied file becomes trustworthy Stage 01 discovered state;
2. whether subsequent descendant-directory discovery failure may invalidate such positive discovery;
3. the semantics for both supplied-root and nested-directory cases;
4. the distinction between partial root discovery and total root discovery failure;
5. when the resulting run is Partial versus wholly Blocked;
6. whether and when an incomplete Recovery Intake Snapshot is valid;
7. the zero-direct-file case where descendant discovery subsequently fails;
8. the narrowest reliable diagnostic/blocking scope;
9. persistence/checkpoint/replay requirements;
10. protection against fabricated descendants or unsupported absence claims;
11. confirmation that no filesystem graph, directory-as-Artifact model, journal, or transactional snapshot is required;
12. the required follow-up Development Slice after governance resolution.

### Resolution

A supplied file becomes trustworthy positive discovery state when Stage 01 has successfully and completely identified that concrete file instance through the applicable discovery operation. That positive state is not provisional merely because another discovery operation for possible child-directory or descendant scopes is performed while processing the same containing directory.

If descendant discovery subsequently fails, the positively discovered files remain trustworthy and remain eligible for ordinary Artifact admission/preservation. The descendant-discovery failure makes discovery incomplete for the affected scope, which is represented as Blocked at the narrowest reliably established non-Artifact source/directory/subtree boundary. When trustworthy discovered Artifact state is retained, the overall Recovery Run may be Partial and an incomplete Recovery Intake Snapshot may represent the actually admitted Artifact set. The snapshot must retain discovery incompleteness and applicable failed-scope diagnostics and must not claim that unknown descendants are absent or that the Artifact set is complete.

This rule applies when the containing directory is the supplied root as well as when it is nested. A root at which positive file discovery succeeded before descendant discovery failed is therefore not semantically identical to a root whose contents could not be enumerated sufficiently to establish any trustworthy supplied Artifact subset. The latter remains wholly Blocked without a Recovery Intake Snapshot.

Successful enumeration that establishes zero direct files does not, by itself, justify an incomplete empty Recovery Intake Snapshot when subsequent descendant discovery fails. The zero-file result establishes only that no direct files were positively discovered by that completed operation; it does not establish the absence of files beneath undiscovered descendants. Unless other trustworthy Artifact state has been established, the intake remains Blocked rather than being represented as an incomplete empty admitted set.

The resolution does not introduce directory Artifact identities, a filesystem graph, filesystem journal state, a transactional source-tree snapshot, hypothetical descendants, or absence claims for undiscovered contents. Internal traversal structures remain implementation details.

`12-architecture.md`, `14-mvp.md`, and the Stage 01 summary in `00-ai-context.md` were synchronized with this refinement. `DEV-SPEC-002` remains unchanged as the historical implementation/verification record. The implementation correction requires a new bounded Development Slice rather than retroactive modification of DEV-SPEC-002.

### Decision reference

D171

### Resolved in

`07-recovery-process.md` §§3, 24–26; `12-architecture.md` §§17–18, 20; `14-mvp.md` §4; `00-ai-context.md` §9

---



# 21. Project-reconstruction qualifications

## PROJ-001 — Candidate Project vs Original Project

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D001, D002  
Blocks: None

### Qualification

Does a reconstructed project represent a recovered historical Original Project or a current recovery construct?

### Resolution

A Candidate Project is a reconstructed current project boundary and remains distinct from Original Project unless direct evidence separately establishes historical identity.

### Decision reference

D063

### Resolved in

`08-project-reconstruction.md` §§2–3

---

## PROJ-002 — Assembly Boundary as Project Candidate Anchor

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D047, D053  
Blocks: None

### Qualification

Should one surviving implementation assembly automatically become one recovered original project?

### Resolution

A distinct managed implementation assembly is a default initial project-candidate anchor, not proof of an original project boundary. Later support may merge, split, or reclassify it.

### Decision reference

D064

### Resolved in

`08-project-reconstruction.md` §5

---

## PROJ-003 — Project Boundary Support and Conflict

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D003, D027  
Blocks: None

### Qualification

How should an inferred Project Boundary explain its basis and competing alternatives?

### Resolution

Every material boundary has a Project Boundary Support Profile. Incompatible materially supported groupings form a Project Boundary Conflict that may remain unresolved, preserve multiple candidates, or be selected operationally through Developer Decision.

### Decision reference

D065

### Resolved in

`08-project-reconstruction.md` §§6–7

---

## PROJ-004 — Candidate Project Merge and Split

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: PROJ-002  
Blocks: None

### Qualification

How may reconstruction depart from initial assembly-anchored project candidates?

### Resolution

Merge and split are explicit reconstruction decisions requiring rationale/provenance. They do not become historical evidence merely because the active reconstruction adopts them.

### Decision reference

D066

### Resolved in

`08-project-reconstruction.md` §8

---

## PROJ-005 — Project Identity and Naming

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D003, D040  
Blocks: None

### Qualification

Does a reconstructed project name define the identity of the recovered project?

### Resolution

Candidate Project identity is stable recovery-state identity independent of its current reconstructed name. Naming uses strongest available support, then conventions/developer decision/fallback with provenance.

### Decision reference

D067

### Resolved in

`08-project-reconstruction.md` §§10–11

---

## PROJ-006 — Project Kind and Target Framework

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D046-D053  
Blocks: None

### Qualification

How should project kind and target framework be represented when surviving artifacts do not uniquely recover the original values?

### Resolution

They are reconstructed properties derived from strongest available support. Practical reconstruction choices remain distinct from historically established values, and multi-target ambiguity remains explicit.

### Decision reference

D068

### Resolved in

`08-project-reconstruction.md` §§12–14

---

## PROJ-007 — Dependency Declaration Reconstruction

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D002, D053  
Blocks: None

### Qualification

Does an observed assembly/runtime dependency establish the exact original project-file declaration mechanism?

### Resolution

No. Candidate `ProjectReference`, package reference, or direct assembly reference representation is a reconstruction choice with provenance unless the declaration mechanism is directly supported independently.

### Decision reference

D069

### Resolved in

`08-project-reconstruction.md` §§15–18

---

## PROJ-008 — Unresolved Dependencies and Cycles

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D056  
Blocks: None

### Qualification

Should unresolved dependencies or Candidate Project cycles be silently repaired to improve buildability?

### Resolution

No. Unresolved dependencies remain explicit. Dependency cycles trigger diagnostics, reevaluation, or Developer Intervention rather than silent structural mutation.

### Decision reference

D070

### Resolved in

`08-project-reconstruction.md` §§19–20

---

## PROJ-009 — Generated Project/Solution Evidence Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D004, D030, D071  
Blocks: None

### Qualification

Can generated project/solution files later serve as independent support that the original project/solution had that structure?

### Resolution

No. Generated `.sln`/project files remain Reconstructed Artifacts and inherit the support dependencies that produced them.

### Decision reference

D071

### Resolved in

`08-project-reconstruction.md` §§26–27

---

## PROJ-010 — Multiple Candidate Structures and Selection

Status: Resolved  
Type: Question  
Origin: Generation/audit of `08-project-reconstruction.md`  
Owner document: `08-project-reconstruction.md`  
Depends on: D005, D012  
Blocks: None

### Qualification

Must project reconstruction always collapse uncertainty to one structure?

### Resolution

No. Multiple materially plausible candidate structures may coexist. Selecting one for active reconstruction is operational and does not make it historical fact.

### Decision reference

D072

### Resolved in

`08-project-reconstruction.md` §§29–30

---

# 22. Source-reconstruction qualifications

## SRC-001 — Source Reconstruction Provenance Classes

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Source material is classified as Preserved Source Content, Metadata-Reconstructed Source, Decompiler-Generated Source, Inferred Source Structure, or Developer-Adjusted Source.

### Decision reference

D073

### Resolved in

`09-source-reconstruction.md`

---

## SRC-002 — Source Provenance Granularity

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Where a source file mixes materially different recovery origins, provenance may attach at finer source-element granularity.

### Decision reference

D074

### Resolved in

`09-source-reconstruction.md`

---

## SRC-003 — Recovered Source File Identity and Path

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Recovered Source File identity remains stable independently of current generated path/name; direct PDB/source paths retain separate provenance.

### Decision reference

D075

### Resolved in

`09-source-reconstruction.md`

---

## SRC-004 — Directly Preserved Source Content Takes Precedence over Weaker Regeneration

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Directly preserved admissible source content is the stronger representation for the same material; generated layout/container choices remain separately reconstructed.

### Decision reference

D076

### Resolved in

`09-source-reconstruction.md`

---

## SRC-005 — Source Skeleton Is a Valid Partial Recovery and Unknown Bodies Are Not Fabricated

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

A Source Skeleton is a legitimate partial recovery, and unavailable behavior remains unresolved or explicit placeholder state rather than invented implementation.

### Decision reference

D077

### Resolved in

`09-source-reconstruction.md`

---

## SRC-006 — Higher-Level Reconstruction of Lowered Code Is Generated Interpretation

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Higher-level C# reconstructed from compiler-lowered/generated forms is generated interpretation of compiled behavior, not proof of exact original syntax.

### Decision reference

D078

### Resolved in

`09-source-reconstruction.md`

---

## SRC-007 — Source Recovery Ceilings Remain Explicit and Artifact-Set Scoped

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Unrecovered local names, comments, formatting, source partition, preprocessor branches, generator inputs, build-time inputs, and exact high-level source forms remain explicit recovery ceilings unless suitable source-derived evidence survives.

### Decision reference

D079

### Resolved in

`09-source-reconstruction.md`

---

## SRC-008 — Source Organization and Candidate Project Ownership Are Reconstruction Choices

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Source file grouping, folders, names, and Candidate Project ownership are provenance-bearing reconstruction choices unless directly supported.

### Decision reference

D080

### Resolved in

`09-source-reconstruction.md`

---

## SRC-009 — Multiple Source Candidates May Coexist; Selection Is Operational

Status: Resolved  
Type: Question  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Multiple materially plausible Source Candidates may coexist; selecting one for active reconstruction does not make it Original Source.

### Decision reference

D081

### Resolved in

`09-source-reconstruction.md`

---

## SRC-010 — Build-Driven Source Repair Remains Reconstructed or Developer-Adjusted Source

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Source changes made in response to build diagnostics remain reconstructed or Developer-Adjusted Source; successful compilation does not establish historical text.

### Decision reference

D082

### Resolved in

`09-source-reconstruction.md`

---

## SRC-011 — Generated Source Cannot Independently Corroborate Its Own Reconstruction

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `09-source-reconstruction.md`  
Owner document: `09-source-reconstruction.md`  
Depends on: Existing canonical evidence/reconstruction decisions  
Blocks: None

### Qualification

What source-reconstruction rule must be established for this area?

### Resolution

Generated recovered source remains a Reconstructed Artifact and cannot independently corroborate the reconstruction decisions that produced it.

### Decision reference

D083

### Resolved in

`09-source-reconstruction.md`

---

# 23. Validation-strategy qualifications

## VAL-001 — Validation Targets an Identified Recovery Checkpoint

Status: Resolved  
Type: Question  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Validation evaluates an identified Validation Target tied to a stable recovery checkpoint/state. A materially changed recovery becomes a new target.

### Decision reference

D084

### Resolved in

`10-validation-strategy.md`

---

## VAL-002 — Validation Is Governed by an Explicit Validation Plan

Status: Resolved  
Type: Question  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Each Validation Run uses a Validation Plan that declares intended dimensions, checks, prerequisites, target/reference context, environment, and intentional non-evaluation before result interpretation.

### Decision reference

D085

### Resolved in

`10-validation-strategy.md`

---

## VAL-003 — Validation Dimensions Remain Independently Visible

Status: Resolved  
Type: Question  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Validation remains multidimensional across applicable structure, dependencies, framework/runtime, API, resources/configuration, source, build, and behavior. No summary may replace underlying dimensions.

### Decision reference

D086

### Resolved in

`10-validation-strategy.md`

---

## VAL-004 — Not Evaluated and Not Comparable Are Distinct from Validation Failure

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

A dimension/check that was not executed or cannot be defensibly compared is not recorded as recovery failure or success; the state and reason remain visible.

### Decision reference

D087

### Resolved in

`10-validation-strategy.md`

---

## VAL-005 — Validation Requires Compatible Target and Reference Context

Status: Resolved  
Type: Question  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Validation Target and Validation Reference Snapshot must represent compatible comparison subjects; known version, framework, configuration, data, or environment mismatches are recorded and constrain interpretation.

### Decision reference

D088

### Resolved in

`10-validation-strategy.md`

---

## VAL-006 — Blind Evaluation Freezes Recovery before Validation Reference Access

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

In controlled blind evaluation, recovery proceeds only from allowed Recovery Input and the Validation Target is frozen before the withheld Validation Reference becomes available to validation.

### Decision reference

D089

### Resolved in

`10-validation-strategy.md`

---

## VAL-007 — Reference-Derived Repair Creates a New Recovery Lineage

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

If information learned from a withheld Validation Reference is used to improve recovery, the original blind target remains preserved and the improved result is a new, explicitly non-blind recovery iteration/target.

### Decision reference

D090

### Resolved in

`10-validation-strategy.md`

---

## VAL-008 — Build, API, Source, and Behavioral Validation Establish Different Claims

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Build, structural/API, source, and behavioral validation establish different propositions. Passing one dimension does not imply equivalence in another, and behavioral success is limited to the scenarios evaluated.

### Decision reference

D091

### Resolved in

`10-validation-strategy.md`

---

## VAL-009 — Validation Runs and Findings Preserve Provenance and History

Status: Resolved  
Type: Question  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Completed Validation Runs/findings retain target, reference, plan, comparator, environment, and diagnostic provenance. Materially changed conditions create a new run rather than rewriting historical findings.

### Decision reference

D092

### Resolved in

`10-validation-strategy.md`

---

## VAL-010 — Validated Baseline Does Not Mean Perfect Recovery

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `10-validation-strategy.md`  
Owner document: `10-validation-strategy.md`  
Depends on: D007, D008, D009, D062 and applicable canonical recovery decisions  
Blocks: None

### Qualification

What validation-strategy rule must be established for this area?

### Resolution

Validated Baseline means an applicable Validation Plan was executed and its results/limitations are recorded; it does not imply all checks passed, all dimensions were comparable, exact reproduction, or global behavioral equivalence.

### Decision reference

D093

### Resolved in

`10-validation-strategy.md`

---

# 24. Recovery-metrics qualifications

## METR-001 — Metric Applicability, Populations, and Denominators

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

What population/applicability/denominator rules must every recovery metric follow?

### Why it matters

Without explicit denominators, high percentages can hide excluded or non-comparable populations.

### Current understanding

Validation already distinguishes Not Evaluated and Not Comparable; metrics must preserve those states rather than flattening them.

### Resolution criteria

Define the required metric metadata and denominator treatment.

### Resolution

Every ratio/rate declares scope, eligible population, matching rule, numerator, denominator, exclusions, and Not Evaluated/Not Comparable treatment. Those populations remain separately visible.

### Decision reference

D094

### Resolved in

`11-recovery-metrics.md`

---

## METR-002 — Correctness vs Coverage and Unknowns

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

How should metrics distinguish correctness from coverage when LegacyRevive.NET can abstain or return Unknown?

### Why it matters

Otherwise the tool could inflate apparent accuracy by answering only easy cases, or be penalized for justified abstention.

### Current understanding

Unknown is a legitimate recovery state, but it must not be counted as correct merely for being cautious.

### Resolution criteria

Define correctness, coverage, and Unknown treatment without rewarding over-abstention.

### Resolution

Correctness among comparable assertions and coverage of the eligible comparable population are separate metrics. Unknown/abstention is neither automatically correct nor automatically wrong and is assessed separately through recoverability/epistemic metrics.

### Decision reference

D095

### Resolved in

`11-recovery-metrics.md`

---

## METR-003 — Set Matching, Precision, and Recall

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

Which standard metric semantics should apply to set-like reconstruction tasks?

### Why it matters

Project/dependency/type/package recovery can produce both false additions and missed reference items.

### Current understanding

Precision/recall makes those error types visible.

### Resolution criteria

Define matching rules and canonical precision/recall usage.

### Resolution

Set-like recovery uses an explicit identity/matching rule and normally reports precision and recall separately; F1 may be secondary only.

### Decision reference

D096

### Resolved in

`11-recovery-metrics.md`

---

## METR-004 — Recoverability-Aware Measurement

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

How should metrics treat historical reference details that the allowed artifact scenario could not establish?

### Why it matters

A raw original-vs-reconstruction comparison can unfairly treat impossible recovery as tool failure, while over-adjustment can excuse real misses.

### Current understanding

The Recovery Matrix already defines artifact-set-scoped recovery ceilings.

### Resolution criteria

Define a measurement approach that preserves both historical fidelity and artifact-scenario fairness.

### Resolution

Raw historical fidelity and recoverability-aware effectiveness are separate views. Not-establishable items remain visible in raw fidelity/recoverability counts but are not silently counted as failures in the recoverability-aware denominator.

### Decision reference

D097

### Resolved in

`11-recovery-metrics.md`

---

## METR-005 — Dependency Existence vs Representation Fidelity

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

Should a dependency relationship and its historical declaration mechanism be scored as one result or separately?

### Why it matters

Compiled dependencies can be recoverable even when the original ProjectReference/PackageReference/binary-reference mechanism is not.

### Current understanding

Project reconstruction already preserves this historical uncertainty.

### Resolution criteria

Define dependency metrics that do not conflate semantic relationship discovery with representation fidelity.

### Resolution

Dependency-edge existence and historical representation/classification are measured separately, with package identity/version/directness likewise separated where applicable.

### Decision reference

D098

### Resolved in

`11-recovery-metrics.md`

---

## METR-006 — Source Fidelity Comparison Levels

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

Which source comparisons should be treated as primary versus diagnostic?

### Why it matters

Textual similarity can understate semantically equivalent decompilation and overstate behaviorally incorrect reconstruction.

### Current understanding

Source Reconstruction distinguishes original/preserved/generated source and explicitly rejects Original Source claims for generated code.

### Resolution criteria

Define source-fidelity metric layers and claim boundaries.

### Resolution

API/declaration, semantic/normalized, compiled-contract, and behavioral comparisons remain distinct. Raw textual similarity is secondary/diagnostic and never proves Original Source identity.

### Decision reference

D099

### Resolved in

`11-recovery-metrics.md`

---

## METR-007 — Graduated Operational Recovery Metrics

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

How should restore/build/run/test recovery progress be measured?

### Why it matters

A final build boolean discards useful information about partial progress and iterative repair.

### Current understanding

Validation treats buildability as one dimension and recovery is iterative.

### Resolution criteria

Define graduated operational milestones and their environment scope.

### Resolution

Operational recovery records restore/build/run/test/refinement milestones and progression under recorded environment/toolchain conditions; build success remains distinct from behavioral equivalence.

### Decision reference

D100

### Resolved in

`11-recovery-metrics.md`

---

## METR-008 — Developer Intervention Burden

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

Should human intervention cost be measured as part of recovery quality?

### Why it matters

LegacyRevive.NET explicitly expects developer participation, and the amount required affects practical usefulness.

### Current understanding

Developer Intervention has explicit kinds, scope, history, and replay semantics.

### Resolution criteria

Define useful intervention-burden measures without treating intervention itself as failure.

### Resolution

Developer-intervention burden is first-class: counts by intervention kind/scope, unresolved decisions/manual corrections, and optionally time-to-milestone with environment/human context.

### Decision reference

D101

### Resolved in

`11-recovery-metrics.md`

---

## METR-009 — Epistemic Accuracy Metrics

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

How should LegacyRevive.NET be measured on uncertainty discipline, support strength, conflicts, and provenance rather than only final answers?

### Why it matters

A tool that is often correct but confidently wrong on unsupported cases can be dangerous in recovery work.

### Current understanding

The Evidence Model defines inferential support labels, conflicts, provenance, and circular-support protection.

### Resolution criteria

Define measurable epistemic-quality signals while preserving non-probabilistic support semantics.

### Resolution

Epistemic metrics include false-assertion rate, empirical correctness by support class, appropriate/missed unknowns where recoverability can be assessed, conflict detection, provenance validity, and circular-support violations. Support labels are not converted into probabilities.

### Decision reference

D102

### Resolved in

`11-recovery-metrics.md`

---

## METR-010 — Aggregation and Overall Score

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

May multidimensional recovery quality be reduced to one canonical overall score?

### Why it matters

A single score can hide severe regression in one dimension and encourages arbitrary weighting.

### Current understanding

D008/D086 require multidimensional validation and independently visible dimensions.

### Resolution criteria

Define whether and how aggregation is allowed.

### Resolution

No authoritative global recovery score replaces dimension metrics. Any future composite indicator is explicitly secondary, formula/weight transparent, and subordinate to underlying results.

### Decision reference

D103

### Resolved in

`11-recovery-metrics.md`

---

## METR-011 — Cross-Run and Cross-Scenario Comparability

Status: Resolved  
Type: Question  
Origin: Generation/audit of `11-recovery-metrics.md`  
Owner document: `11-recovery-metrics.md`  
Depends on: D008, D086, D087, D091 and applicable canonical recovery/evidence decisions  
Blocks: None

### Qualification

When are metric results from different runs/scenarios legitimately comparable?

### Why it matters

Changed artifact sets, references, denominators, matching rules, environments, or intervention policies can create false regression/improvement signals.

### Current understanding

Validation already requires compatible target/reference context and preserves run provenance.

### Resolution criteria

Define the minimum scenario/metric identity needed for benchmark comparison and history.

### Resolution

Cross-run comparison requires materially compatible or explicitly qualified subject/reference, artifact scenario, allowed input, tool/config, metric definition/matching rule, environment, intervention policy, and validation reference. Material metric-definition changes are versioned rather than rewriting historical results.

### Decision reference

D104

### Resolved in

`11-recovery-metrics.md`

---

# 25. Architecture qualifications

## ARCH-001 — Logical Modular Architecture and Deployment Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

What architectural shape should be canonical without prematurely fixing deployment topology?

### Why it matters

Define logical dependency/module boundaries while keeping process/service topology open.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

LegacyRevive.NET uses a logical modular, domain-centered architecture. Logical modules do not imply separate processes/services; deployment topology remains a later hosting/MVP choice.

### Decision reference

D105

### Resolved in

`12-architecture.md`

---

## ARCH-002 — Canonical State Partitioning

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How should original artifacts, knowledge, intervention, reconstruction, checkpoints, and validation state be separated?

### Why it matters

Preserve their different epistemic/lifecycle semantics even if physically co-located.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Original artifacts, recovery knowledge/history, developer intervention, reconstruction workspace, checkpoints, and validation/metric history are logically separate state areas.

### Decision reference

D106

### Resolved in

`12-architecture.md`

---

## ARCH-003 — Artifact Analyzer Output Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

May artifact readers directly emit architectural/historical inferences?

### Why it matters

Protect Observation versus Inference semantics at the analysis boundary.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Artifact analyzers emit direct Observations and diagnostics. Inferential conclusions are produced by explicit inference mechanisms.

### Decision reference

D107

### Resolved in

`12-architecture.md`

---

## ARCH-004 — Support Graph Architectural Representation

Status: Resolved  
Type: Question  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How should provenance/support dependencies be represented so cycles and downstream reevaluation can be enforced?

### Why it matters

Make derivation lineage operational rather than decorative.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Material support/provenance dependencies are first-class architectural data with cycle protection, conflict/supersession links, and downstream traversal.

### Decision reference

D108

### Resolved in

`12-architecture.md`

---

## ARCH-005 — Inference Rule Architecture

Status: Resolved  
Type: Question  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How should inference/heuristics be represented and extended?

### Why it matters

Ensure rules are explainable/testable and cannot bypass support semantics.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Inference uses explicit identifiable/versionable rules with declared inputs, preconditions, outputs, support dependencies, and explanation/support-strength behavior.

### Decision reference

D109

### Resolved in

`12-architecture.md`

---

## ARCH-006 — Recovery Orchestration Boundary

Status: Resolved  
Type: Question  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

Which component coordinates the canonical iterative Recovery Process?

### Why it matters

Coordinate cross-stage lifecycle/reevaluation without embedding artifact-specific logic.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

A Recovery Orchestrator/application service coordinates the canonical process while artifact-specific analysis remains in capability modules.

### Decision reference

D110

### Resolved in

`12-architecture.md`

---

## ARCH-007 — Reconstruction Feedback Isolation

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How is generated reconstruction prevented from becoming independent historical evidence?

### Why it matters

Enforce anti-feedback rules in architecture, not merely documentation.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Reconstruction output is stored/handled as separate Reconstructed Artifacts and cannot independently enter the original-system support graph.

### Decision reference

D111

### Resolved in

`12-architecture.md`

---

## ARCH-008 — Validation Reference Architectural Isolation

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How is blind-recovery reference leakage prevented at component/repository level?

### Why it matters

Process separation is insufficient if services/caches expose reference data.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Blind recovery contexts cannot access Validation Reference content; reference access exists only in validation context until target freeze.

### Decision reference

D112

### Resolved in

`12-architecture.md`

---

## ARCH-009 — Checkpoint and Replay Architecture

Status: Resolved  
Type: Question  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

What architectural semantics must Recovery Checkpoints provide?

### Why it matters

Validation/reproducibility require stable identifiable historical recovery states.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Checkpoints preserve stable history by referencing replay-relevant run/input/current-state/intervention/reconstruction configuration without rewriting earlier checkpoints.

### Decision reference

D113

### Resolved in

`12-architecture.md`

---

## ARCH-010 — Failure and Diagnostic Architecture

Status: Resolved  
Type: Question  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

Should one capability failure terminate a whole Recovery Run?

### Why it matters

Legacy inputs are often partial/broken and useful recovery should continue where safe.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Failures are structured scoped diagnostics; safe partial progress and Partial/Blocked/Workable states remain first-class.

### Decision reference

D114

### Resolved in

`12-architecture.md`

---

## ARCH-011 — Extension Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

How can analyzers/rules/reconstructors/validators/metrics be extensible without bypassing canonical semantics?

### Why it matters

Extensibility must not weaken provenance or isolation rules.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Extensions use explicit contracts and cannot bypass provenance, support-cycle, intervention, reconstruction-feedback, validation-isolation, or metric-accounting rules.

### Decision reference

D115

### Resolved in

`12-architecture.md`

---

## ARCH-012 — Persistence Technology Boundary

Status: Resolved  
Type: Deferred Decision  
Origin: Generation/audit of `12-architecture.md`  
Owner document: `12-architecture.md`  
Depends on: Existing canonical evidence, recovery, reconstruction, validation, and metric decisions  
Blocks: None

### Qualification

Which persistence technology should architecture require?

### Why it matters

Need persistence semantics now without premature database/tool commitment.

### Current understanding

All earlier canonical semantics must be implementable without being silently weakened by technical structure.

### Resolution criteria

Define the minimum durable architectural rule while deferring concrete tool/library/deployment choices where they belong downstream.

### Resolution

Architecture requires stable IDs/history/provenance/checkpoints/etc. but leaves physical persistence technology to `13-tooling-and-dependencies.md`.

### Decision reference

D116

### Resolved in

`12-architecture.md`

---

## ARCH-013 — Application-Gateway Dependency Policy

Status: Resolved  
Type: Boundary Issue  
Origin: Initial physical solution/project graph design discussion  
Owner document: `12-architecture.md`  
Depends on: ARCH-001 / D105; TOOL-011 / D127  
Blocks: None

### Qualification

What dependency policy should govern references from outer adapter/capability projects to Application and Domain/Core in the physical .NET implementation?

### Why it matters

Allowing every adapter to reference Domain/Core by default would weaken the intended inward boundary and encourage implementation coupling. Conversely, forbidding all direct adapter-to-Domain/Core references could force duplicate Application DTOs and mapping layers solely to conceal Domain/Core types that an Application-owned capability contract legitimately exposes. The rule needs to distinguish the normal dependency path from justified exceptions.

### Current understanding

D105 and `12-architecture.md` already require dependencies to point toward canonical recovery semantics, place Application above Domain/Core, and require infrastructure/tool adapters to implement inward-owned ports. D127 places composition in the host/infrastructure boundary and keeps Domain/Core host/container agnostic. The existing architecture did not yet state the default physical project-reference policy for outer adapters.

### Resolution criteria

Define a durable dependency rule that keeps Application as the normal gateway to Domain/Core, preserves legitimate adapter implementations of inward-owned contracts that require Domain/Core types or behaviour, prevents convenience-based Domain/Core coupling, remains compatible with the canonical Capability Port definition, and does not prematurely settle the complete physical project graph.

### Resolution

`LegacyRevive.Application` is the normal gateway to Domain/Core for outer adapter/capability projects. Adapters reference Application-owned Capability Ports by default. A direct adapter-to-Domain/Core dependency is allowed only when implementing an inward-owned capability contract genuinely requires Domain/Core-owned types or behaviour; such a dependency is exceptional, explicit, and must be justified by the contract rather than convenience. This includes an Application-owned port that exposes Domain/Core types and, where the canonical Capability Port model genuinely requires it, a Domain/Core-owned port; Domain/Core ownership must not be introduced merely to bypass the normal Application gateway.

The host/composition root may reference Application and the concrete adapter implementations it composes. This resolution governs dependency direction only; it does not settle the final project count or names.

A focused governed audit was completed across `12-architecture.md` §§4 and 23–27, `13-tooling-and-dependencies.md` §§20 and 24–25, D127, D161, and the canonical Capability Port / Tool Adapter terminology. The audit found the resolution consistent with the existing inward-dependency model, host/composition-root boundary, logical-module semantics, and capability-port ownership model. No contradiction, ownership conflict, unsupported scope expansion, or new material qualification was identified. Governance synchronization across `12`, `13`, `90`, and `91` was then confirmed.

### Decision reference

D161

### Resolved in

`12-architecture.md` §4 and §24–25; synchronized with `13-tooling-and-dependencies.md` §20

---

# 26. Tooling and dependency qualifications

## TOOL-001 — Runtime/SDK Baseline

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which runtime/TFM should the LegacyRevive implementation target?

### Why it matters

Choose a supported modern host without conflating the tool runtime with recovered application TFMs.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

LegacyRevive.NET targets .NET 10/net10.0 as its primary host/application baseline; recovered applications retain their own recovered target-framework state.

### Decision reference

D117

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-002 — Dependency Version Governance

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should package versions and upgrades be controlled?

### Why it matters

Tool versions can alter recovery/build/decompilation output and must remain reproducible.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use central explicit version management, deliberate compatibility/security/license-reviewed upgrades, and version provenance where material.

### Decision reference

D118

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-003 — Managed Metadata Extraction Technology

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which API should perform primary direct managed PE/metadata extraction?

### Why it matters

Direct Observations should not depend on assembly loading or decompiler interpretation.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

System.Reflection.Metadata/PEReader is the primary direct managed metadata/IL/Portable-PDB extraction technology.

### Decision reference

D119

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-004 — Decompiler Selection

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which decompiler engine should LegacyRevive embed rather than reimplement?

### Why it matters

Source reconstruction needs a mature high-quality decompiler while preserving generated-source provenance.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use ICSharpCode.Decompiler behind a capability port; its output remains reconstructed/decompiler-generated source.

### Decision reference

D120

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-005 — PDB Technology Strategy

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should Portable and native Windows PDBs be supported?

### Why it matters

PDB formats differ in portability/platform requirements.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Portable PDB uses System.Reflection.Metadata; native Windows PDB support is an optional Microsoft.DiaSymReader.Native adapter.

### Decision reference

D121

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-006 — C# Syntax/Semantic Engine

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which engine should parse/analyze/transform reconstructed C#?

### Why it matters

A compiler-grade semantic model is required for validation and reconstruction work.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use Microsoft.CodeAnalysis.CSharp (Roslyn).

### Decision reference

D122

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-007 — MSBuild and Build Execution Model

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should project-object-model access and actual build execution be implemented?

### Why it matters

In-process build/toolset loading risks host conflicts while project APIs are valuable.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use Microsoft.Build plus Microsoft.Build.Locator for object-model work; perform canonical restore/build execution out-of-process through the tool adapter.

### Decision reference

D123

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-008 — NuGet Package and Feed Access

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should local package evidence and remote feed lookup differ?

### Why it matters

Remote lookup can contaminate claims about supplied-artifact recoverability.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use NuGet.Packaging for local package artifacts; NuGet.Protocol is explicit optional external enrichment with separate provenance.

### Decision reference

D124

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-009 — Persistence Technology

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which baseline physical store should implement canonical local state/history?

### Why it matters

Architecture deferred the concrete persistence decision while requiring append/provenance/checkpoint semantics.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use SQLite through Microsoft.Data.Sqlite with explicit repository/schema boundaries; preserve logical store separation.

### Decision reference

D125

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-010 — Serialization/XML Technology

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which technologies should represent JSON exports and XML/config parsing?

### Why it matters

Durable interchange needs stable contracts without unnecessary dependencies.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use System.Text.Json plus platform XML APIs; durable exports use explicit/versioned DTO contracts.

### Decision reference

D126

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-011 — Host DI Logging and CLI

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which baseline host/composition/CLI technology should be used?

### Why it matters

Need a maintained .NET composition model without leaking host concerns into Domain/Core.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use Generic Host/Microsoft.Extensions DI/logging/configuration and System.CommandLine for the CLI.

### Decision reference

D127

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-012 — External Process Execution

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Is a third-party process wrapper required?

### Why it matters

Build/tool execution must be structured and provenance-bearing but should minimize dependencies.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use System.Diagnostics.Process behind a controlled execution port; no separate wrapper dependency is selected by default.

### Decision reference

D128

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-013 — Generated Identity and Artifact Hashing

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which baseline identity mechanisms should generated recovery records and artifact bytes use?

### Why it matters

Stable append-oriented IDs and exact-byte artifact identity are required.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use Guid.CreateVersion7 for generated GUID identities and SHA-256 for artifact content fingerprints; keep them semantically distinct.

### Decision reference

D129

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-014 — Automated Test Framework

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Which test framework should underpin unit/integration/corpus regression testing?

### Why it matters

The validation-heavy product needs a stable common test stack.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Use xUnit v3, while preserving validation-reference isolation in test harnesses.

### Decision reference

D130

### Resolved in

`13-tooling-and-dependencies.md`

---

## TOOL-015 — Dependency Security and Licensing Governance

Status: Resolved  
Type: Question  
Origin: Generation/audit of `13-tooling-and-dependencies.md`  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: `12-architecture.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

What review is required before an external dependency becomes canonical?

### Why it matters

Dependencies affect licensing, supply-chain risk, compatibility, and recoverability reproducibility.

### Current understanding

The tooling choice must implement the canonical architecture without changing evidence categories, provenance, reconstruction boundaries, validation isolation, or metric semantics.

### Resolution criteria

Select the smallest suitable maintained technology/approach, record its architectural role and boundary, and avoid converting implementation convenience into domain semantics.

### Resolution

Require identity/source/license/framework/maintenance/security/capability/version review for each direct external dependency.

### Decision reference

D131

### Resolved in

`13-tooling-and-dependencies.md`

---


## TOOL-016 — LegacyRevive.NET Repository Solution Format

Status: Resolved  
Type: Question  
Origin: Prior LegacyRevive.NET implementation discussion; omission rediscovered during initial physical solution/project design on 2026-09-26  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: TOOL-001 / D117 (.NET 10 baseline) and applicable earlier architecture/tooling decisions  
Blocks: Initial physical solution scaffold until resolved

### Qualification

Which solution-file format should the LegacyRevive.NET repository itself use: the traditional `.sln` format or the modern `.slnx` format?

### Why it matters

The physical repository scaffold needs one stable canonical solution representation. Earlier discussion had selected `.slnx`, but that direction was not consolidated into canonical Project source documents, allowing later work to regress to `.sln` when chat context was unavailable. The choice therefore needs durable governance rather than remaining a conversational preference.

The repository format must also remain separate from reconstructed Candidate Solution output so LegacyRevive.NET does not project its own modern development conventions onto recovered legacy applications.

### Current understanding

LegacyRevive.NET already targets .NET 10. Current Microsoft documentation confirms that .NET 10 `dotnet new sln` creates `.slnx` by default, that SDK support began in .NET 9.0.200, and that `.slnx` is supported by major .NET tooling. Existing Project Reconstruction/MVP semantics permit generated candidate solutions to use `.sln` or another supported representation, so selecting `.slnx` for the LegacyRevive.NET repository does not constrain recovered-output history.

### Resolution criteria

1. choose the canonical solution format for the LegacyRevive.NET repository;
2. verify compatibility with the accepted .NET 10/tooling baseline;
3. keep the repository choice distinct from recovered Candidate Solution representation;
4. record the durable decision in `90-decisions.md`;
5. update the canonical tooling owner and AI orientation;
6. audit the changes and complete governance synchronization.

### Resolution

Use `LegacyRevive.slnx` as the canonical solution representation for the LegacyRevive.NET repository. Do not use `.sln` for the repository merely by convention. A different format requires a governed compatibility reason. This decision does not require recovered Candidate Solutions to use `.slnx`; their representation remains a reconstruction/tooling choice under the existing Project Reconstruction and MVP boundaries.

The canonical owner, decision log, qualification register, and AI orientation were synchronized and audited in the same governance cycle. No additional material qualification was identified.

### Decision reference

D162

### Resolved in

`13-tooling-and-dependencies.md` §3.1, §24–25 and §27; summarized in `00-ai-context.md` §15

---


## TOOL-017 — LegacyRevive.NET Repository Solution Filename Rename

Status: Resolved  
Type: Question  
Origin: Developer-confirmed repository rename from `LegacyRevive.slnx` to `LegacyRevive.Net.slnx` on 2026-09-27  
Owner document: `13-tooling-and-dependencies.md`  
Depends on: TOOL-016 / D162  
Blocks: Canonical repository-naming synchronization until resolved

### Qualification

The physical LegacyRevive.NET repository solution has been renamed from `LegacyRevive.slnx` to `LegacyRevive.Net.slnx`, while the canonical tooling owner, accepted decision D162, resolved TOOL-016 qualification, and AI orientation still record the former filename.

Should the rename be treated as incidental repository drift, or should the canonical repository convention be updated through governance while retaining SLNX as the accepted solution-file format?

### Why it matters

D162 made both the SLNX format and the former root solution filename durable project direction. Allowing the physical repository to use a different filename without governance synchronization would leave the canonical documents knowingly inconsistent with implementation reality and would violate the project's decision-supersession and synchronization rules.

At the same time, the filename change does not itself justify reopening the established Candidate Solution boundary or replacing the SLNX format choice. The governance update therefore needs to be narrowly scoped.

### Current understanding

- The repository's root solution has already been deliberately renamed to `LegacyRevive.Net.slnx`.
- The extension remains `.slnx`; no solution-format change has occurred.
- D162 and TOOL-016 must remain historically visible rather than being rewritten to make the old decision appear to have named the new file.
- `13-tooling-and-dependencies.md` owns current repository tooling/naming state; `00-ai-context.md` summarizes that state.
- Reconstructed Candidate Solution output remains a separate recovery/tooling concern and is unaffected by this repository-only rename.

### Resolution criteria

1. preserve TOOL-016 and D162 as historical governance records;
2. create a new durable decision that retains SLNX but establishes `LegacyRevive.Net.slnx` as the current canonical repository solution filename;
3. supersede D162 rather than rewrite its historical decision text;
4. update `13-tooling-and-dependencies.md` and `00-ai-context.md` to the new current filename;
5. update tooling governance mapping and qualification summary/counts;
6. audit all affected canonical documents for stale current-state references, ownership drift, Candidate Solution leakage, and governance inconsistency;
7. complete synchronization across `90`, `91`, the canonical owner, and AI orientation before marking this qualification Resolved.

### Resolution

Retain **SLNX** as the canonical solution-file format for the LegacyRevive.NET repository and establish `LegacyRevive.Net.slnx` as the canonical root solution filename. Record D169 as the current durable decision and mark D162 Superseded by D169. Preserve TOOL-016 unchanged as the historical qualification that selected SLNX and the former filename.

`13-tooling-and-dependencies.md` §3.1 now records `LegacyRevive.Net.slnx` and maps the current convention to TOOL-017 / D169. `00-ai-context.md` §15 now summarizes the same current filename. The repository-only convention remains explicitly separate from reconstructed Candidate Solution output.

A controlled audit of the affected canonical set (`00`, `13`, `90`, `91`) confirmed:

- current-state references use `LegacyRevive.Net.slnx`;
- remaining `LegacyRevive.slnx` references occur only inside preserved superseded/historical records;
- D162 is marked Superseded and links to D169;
- D169 links back to TOOL-017 and supersedes D162;
- TOOL-017 is indexed and resolved with D169 as its decision reference;
- `13-tooling-and-dependencies.md` remains the canonical owner;
- `00-ai-context.md` remains orientation-only;
- Candidate Solution semantics were not altered;
- no additional material qualification was identified.

Governance synchronization across the decision log, qualification register, canonical tooling owner, AI orientation, and audit result is complete.

### Decision reference

D169

### Resolved in

`13-tooling-and-dependencies.md` §3.1, §24–25 and §28; summarized in `00-ai-context.md` §15; governance history in D162/D169 and TOOL-016/TOOL-017

---

# 27. MVP qualifications

## MVP-001 — MVP Product Slice

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

What minimum end-to-end product proposition must the first release prove?

### Why it matters

Prevent an MVP that is merely an analyzer/decompiler demo.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

The MVP covers local artifact intake through evidence/provenance, Candidate Projects/source, intervention/context, workspace export, build attempt where applicable, checkpoint and reporting.

### Decision reference

D132

### Resolved in

`14-mvp.md`

---

## MVP-002 — MVP Execution Platform

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Which execution environment is supported first?

### Why it matters

Legacy .NET Framework recovery/build support and platform constraints require a clear first environment.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Windows x64 is the first supported MVP execution environment; Domain/Core remains future cross-platform capable.

### Decision reference

D133

### Resolved in

`14-mvp.md`

---

## MVP-003 — MVP Input and Artifact Support

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Which Recovery Input source and artifact analyzers are mandatory?

### Why it matters

The MVP needs a bounded input contract and useful companion-artifact coverage.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Primary input is a local directory tree; all files are inventoried and defined managed/config/symbol/documentation/runtime/package/resource artifacts have analyzers.

### Decision reference

D134

### Resolved in

`14-mvp.md`

---

## MVP-004 — Managed Assembly Analysis Depth

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

How much managed metadata must the MVP recover directly?

### Why it matters

Candidate projects/source/dependencies require more than assembly-name inventory.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Recover direct assembly/reference/type/member/signature/attribute/generic/resource/entry-point/IL observations sufficient for reconstruction without assuming original project boundaries.

### Decision reference

D135

### Resolved in

`14-mvp.md`

---

## MVP-005 — MVP Project-Candidate Scope

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

How far must project reconstruction go in the MVP?

### Why it matters

A development baseline requires Candidate Projects but original boundaries can remain uncertain.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Classify ownership and generate evidence-backed Candidate Projects while preserving Unknown/alternatives and avoiding one-project-per-DLL historical claims.

### Decision reference

D136

### Resolved in

`14-mvp.md`

---

## MVP-006 — MVP Dependency Reconstruction

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

How are dependency relationships and representations handled without inventing package history?

### Why it matters

Buildability pressure can otherwise turn guesses into historical facts.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Build the dependency graph first, then choose supported project/package/binary/framework/unresolved representations while preserving uncertainty; remote feed lookup is not required.

### Decision reference

D137

### Resolved in

`14-mvp.md`

---

## MVP-007 — MVP Source Reconstruction

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

What source output is required?

### Why it matters

The product must create usable development source without claiming original source identity.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Decompiler-backed C# source generation is required with PDB/XML-doc assistance, deterministic fallback organization, and explicit reconstructed provenance.

### Decision reference

D138

### Resolved in

`14-mvp.md`

---

## MVP-008 — MVP Development-Tree Export

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

What generated development representation must be exported?

### Why it matters

Developers need conventional projects/solution/source, not only internal state.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Export a deterministic candidate solution/project/source tree plus machine/human-readable recovery manifests/reports.

### Decision reference

D139

### Resolved in

`14-mvp.md`

---

## MVP-009 — MVP Developer Intervention and Context

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Which human-assisted recovery capabilities are first-release requirements?

### Why it matters

Ambiguity is expected and context makes intervention useful.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Support persisted assertions/decisions/corrections plus basic deterministic scoped recovered-context observations; ML convention discovery is deferred.

### Decision reference

D140

### Resolved in

`14-mvp.md`

---

## MVP-010 — MVP Workspace Resume

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Is persistence/resume required in the MVP?

### Why it matters

Recovery is iterative and may span artifact additions and human decisions.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Every recovery uses a persistent resumable workspace with history/checkpoints and earliest-affected-stage reevaluation.

### Decision reference

D141

### Resolved in

`14-mvp.md`

---

## MVP-011 — MVP Build and Repair Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Must every recovery build, and what automatic repair is allowed?

### Why it matters

Compilation pressure must not create speculative historical fabrication.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Attempt controlled restore/build when prerequisites exist; allow only deterministic/narrowly supported repair; a green build is not required for every completed recovery.

### Decision reference

D142

### Resolved in

`14-mvp.md`

---

## MVP-012 — MVP Outcome Semantics

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

How should incomplete but useful recoveries be represented?

### Why it matters

Incomplete deployments are normal and should not be mislabeled as tool failure.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Report canonical Partial/Blocked/Workable/Validated states and distinguish them from execution failure.

### Decision reference

D143

### Resolved in

`14-mvp.md`

---

## MVP-013 — MVP User Surface

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Which user-facing host capabilities are required?

### Why it matters

The MVP needs a coherent minimal operator/developer workflow.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

CLI-first capabilities are recover, status, explain, decide, build and export; richer hosts are deferred.

### Decision reference

D144

### Resolved in

`14-mvp.md`

---

## MVP-014 — MVP Validation Requirement

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

How much canonical validation must ship or gate release?

### Why it matters

Product fidelity needs ground-truth evaluation without forcing full validation UI into the first user workflow.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Blind controlled validation is mandatory for development/release; full user-facing reference-validation commands are not required for normal MVP use.

### Decision reference

D145

### Resolved in

`14-mvp.md`

---

## MVP-015 — MVP Release Gates

Status: Resolved  
Type: Question  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

What must pass before the MVP can ship?

### Why it matters

A single build result or aggregate score is insufficient.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Release requires functional, epistemic-safety, replay/determinism, persistence/history, multidimensional quality and developer-usability gates; no global score.

### Decision reference

D146

### Resolved in

`14-mvp.md`

---

## MVP-016 — MVP Exclusions

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `14-mvp.md`  
Owner document: `14-mvp.md`  
Depends on: Earlier canonical recovery, validation, architecture, and tooling decisions  
Blocks: None

### Qualification

Which capabilities are deliberately deferred?

### Why it matters

Explicit non-goals prevent scope creep and preserve a meaningful first release.

### Current understanding

The MVP must prove the central managed-artifact recovery proposition while preserving canonical provenance, uncertainty, iteration, validation, and tooling boundaries.

### Resolution criteria

Select the smallest shippable capability that proves product value, define explicit acceptance behavior, and move nonessential expansion to the roadmap.

### Resolution

Native/installer/runtime-process/live-database recovery, dynamic plugins, rich/cloud/multi-user hosts, required remote enrichment, generalized ML authority, modernization, universal build success and global behavior proof are deferred.

### Decision reference

D147

### Resolved in

`14-mvp.md`

---

# 28. Roadmap qualifications

## ROAD-001 — Roadmap Strategic-vs-Design Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Does roadmap inclusion itself constitute accepted implementation design?

### Why it matters

The roadmap must not bypass canonical ownership/governance.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Roadmap entries establish strategic sequencing only; detailed design becomes canonical only through the normal qualification/design/audit lifecycle.

### Decision reference

D148

### Resolved in

`15-roadmap.md`

---

## ROAD-002 — Evidence-Gated Horizon Model

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

Should the canonical roadmap be date-driven or dependency/evidence-driven?

### Why it matters

Speculative dates age quickly while product evidence/prerequisites are durable.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Use evidence/dependency horizons; calendar scheduling may be maintained separately.

### Decision reference

D149

### Resolved in

`15-roadmap.md`

---

## ROAD-003 — Capability Promotion Criteria

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

What justifies moving a deferred capability into active design?

### Why it matters

Without promotion criteria, roadmap expansion becomes novelty-driven scope creep.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Promote based on demonstrated recovery impact, intervention reduction, epistemic safety, corpus frequency, prerequisite leverage, validation readiness, and proportionate cost.

### Decision reference

D150

### Resolved in

`15-roadmap.md`

---

## ROAD-004 — First Post-MVP Priority

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

What should the first post-MVP horizon optimize?

### Why it matters

Breadth should not outrun the quality of the core product loop.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Prioritize build/project/source effectiveness, intervention/explainability UX, and reporting within the MVP workflow.

### Decision reference

D151

### Resolved in

`15-roadmap.md`

---

## ROAD-005 — Platform and Artifact Expansion

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

When should broader platform/artifact support be prioritized?

### Why it matters

New evidence sources are valuable after the core loop can use them reliably.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Horizon 2 broadens cross-platform execution and high-value symbol/source-like/schema/service/deployment artifacts.

### Decision reference

D152

### Resolved in

`15-roadmap.md`

---

## ROAD-006 — External Enrichment Expansion

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How may remote/external knowledge expand after MVP?

### Why it matters

It can improve recovery but changes the evidence ceiling.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

External enrichment is allowed later but always provenance-distinct, opt-in/configurable, and recorded in validation context.

### Decision reference

D153

### Resolved in

`15-roadmap.md`

---

## ROAD-007 — Advanced Inference and Learned Models

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

What role may advanced ranking/learned models have?

### Why it matters

Automation must not become an unauditable historical-truth authority.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Advanced inference/learned ranking remains inferential and subordinate to the Evidence Model, with research/held-out validation before promotion.

### Decision reference

D154

### Resolved in

`15-roadmap.md`

---

## ROAD-008 — Richer Product Hosts

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should future desktop/web/IDE/API hosts relate to recovery semantics?

### Why it matters

Multiple surfaces can otherwise fork domain behavior.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

All richer hosts reuse canonical Application/Domain capabilities.

### Decision reference

D155

### Resolved in

`15-roadmap.md`

---

## ROAD-009 — Validation Maturity

Status: Resolved  
Type: Question  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should validation evolve as capability expands?

### Why it matters

Added automation requires corresponding evidence of correctness/safety.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Expand held-out corpus, corruption/starvation scenarios, semantic/behavioral comparators, intervention metrics, and regression reporting alongside product scope.

### Decision reference

D156

### Resolved in

`15-roadmap.md`

---

## ROAD-010 — Runtime Plugin Ecosystem

Status: Resolved  
Type: Deferred Decision  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

When should runtime third-party plugins be introduced?

### Why it matters

Plugin systems add trust, compatibility, loading, provenance, and determinism risk.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Defer runtime plugins until demonstrated need and explicit trust/isolation/versioning design.

### Decision reference

D157

### Resolved in

`15-roadmap.md`

---

## ROAD-011 — Collaborative and Enterprise Scale

Status: Resolved  
Type: Deferred Decision  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should shared/cloud/distributed recovery enter the roadmap?

### Why it matters

Local workspace semantics do not automatically solve multi-user security/concurrency/audit concerns.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Treat collaboration/scale as a separate governed expansion requiring identity, authorization, concurrency, audit, retention, sharing, and distributed determinism.

### Decision reference

D158

### Resolved in

`15-roadmap.md`

---

## ROAD-012 — Native and Runtime Recovery

Status: Resolved  
Type: Boundary Issue  
Origin: Generation/audit of `15-roadmap.md`  
Owner document: `15-roadmap.md`  
Depends on: `14-mvp.md` and applicable earlier canonical decisions  
Blocks: None

### Qualification

How should native/runtime evidence relate to managed recovery?

### Why it matters

Native/dump/runtime artifacts have different information boundaries and cannot inherit managed-source claims.

### Current understanding

The roadmap must extend the canonical MVP without retroactively changing its historical scope or weakening evidence/provenance governance.

### Resolution criteria

Define strategic sequencing/promotion boundaries while deferring detailed design until a capability is actively promoted.

### Resolution

Treat native/runtime recovery as a distinct research/capability domain with its own evidence ceilings and validation strategy.

### Decision reference

D159

### Resolved in

`15-roadmap.md`

---



## DOC-022 — Stale Qualification Summary Count During Workspace Governance Synchronization

Status: Resolved  
Type: Audit Finding  
Origin: Governance synchronization audit for ARCH-014/015, SRC-012, MVP-017/018 on 2026-09-26  
Owner document: `91-design-qualification-register.md`  
Depends on: None  
Blocks: None

### Qualification

The register's prior current-summary counts understated the number of already-tracked qualifications by one before the new workspace/export qualifications were added.

### Why it matters

The qualification register is the single source of truth for qualification status. Its summary must agree with the actual register index rather than presenting a stale count.

### Current understanding

The governed index is authoritative for the set of tracked qualification IDs; summary counts are derived bookkeeping and do not create or remove qualifications.

### Resolution criteria

Recount the register index after all new entries are present and synchronize the current-summary totals without changing any qualification status merely to fit the old summary.

### Resolution

At the DOC-022 synchronization point, the register index was recounted. With DOC-022 and the five workspace/export qualifications included, it contained 172 tracked qualifications: 171 Resolved and 1 Open. The later resolution of ARCH-015 changes only qualification status totals, not the tracked-ID count or the validity of this historical bookkeeping correction.

### Decision reference

None required; this is a register bookkeeping correction and establishes no new durable project direction.

### Resolved in

`91-design-qualification-register.md` current summary.

---


## DOC-023 — Premature Canonical Status During DEV-001 Governance

Status: Resolved  
Type: Audit Finding  
Origin: Step 6 governed audit of DEV-001 / D168 development-process canonicalization, 2026-09-27  
Owner document: `00-ai-context.md` / `13-tooling-and-dependencies.md` / `14-mvp.md` / `16-development-process.md`  
Depends on: D020, D021, D168, DEV-001  
Blocks: None

### Qualification

The first synchronized development-process drafts described `16-development-process.md` as already canonical/completed and some affected canonical documents described its ownership as fully active, while DEV-001 correctly remained `In Review`.

### Why it matters

`91-design-qualification-register.md` requires a document materially affected by an unresolved qualification to remain candidate canonical until the governance lifecycle, audit, and synchronization complete. Presenting `16` as fully canonical before DEV-001 resolution could cause a later Project chat or developer to treat the Development Slice Specification process as finalized prematurely and would contradict the orientation rule not to present open qualifications as settled.

### Current understanding

D168 is already an Accepted durable decision establishing the intended development-process direction and ownership. The remaining issue is governance-stage representation, not the substance of D168 or the DEV-001 design conclusion. The updated documents may contain the accepted candidate definitions while clearly preserving their pre-resolution status.

### Resolution criteria

Represent `16-development-process.md` consistently as **Candidate canonical — audited with open qualifications** while DEV-001 remains `In Review`; make `00`, `13`, and `14` reflect that staged state; preserve D168 unchanged; and avoid beginning governed `DEV-SPEC-*` use as though DEV-001 were already resolved.

### Resolution

`16-development-process.md` now uses the allowed status **Candidate canonical — audited with open qualifications** and explicitly states that DEV-001 remains `In Review`. `00-ai-context.md` now distinguishes the completed `00`–`15` sequence from candidate `16`, marks the ownership entry as candidate, and delays finalized DEV-SPEC guidance until DEV-001 resolution. `13-tooling-and-dependencies.md` and `14-mvp.md` retain their existing ownership while identifying D168's intended `16` boundary and its candidate status. No product, tooling, MVP, or development-process semantics were changed.

### Decision reference

None required. D168 already establishes the durable direction; this finding corrects governance-stage wording only.

### Resolved in

`00-ai-context.md` §§19–22; `13-tooling-and-dependencies.md` §§17, 28; `14-mvp.md` §§25–26; `16-development-process.md` Status, §§30–31.

---

## DOC-024 — Stale Qualification Summary After Stage 01 Follow-Up Qualifications

Status: Resolved  
Type: Audit Finding  
Origin: Registration audit for RMAT-009, DEV-002, and DEV-003, 2026-09-30  
Owner document: `91-design-qualification-register.md`  
Depends on: None  
Blocks: None

### Qualification

The register's current summary states that 175 qualifications are tracked and resolved, while the register index and full entries contain 177 existing resolved qualifications before RMAT-009, DEV-002, and DEV-003 are added. The two unreflected qualifications are PROC-010 and PROC-011.

### Why it matters

The qualification register is the single source of truth for qualification status. Its derived summary counts and domain summaries must agree with the actual governed index; otherwise a later governance action can use an incorrect baseline and compound the bookkeeping error.

### Current understanding

The register index is authoritative for the tracked qualification set. Before the current registration update, it contains 177 unique qualification IDs and each has a corresponding full entry with `Status: Resolved`.

The stale `175` summary and the statement that nine `PROC-*` qualifications are resolved were not synchronized after PROC-010 and PROC-011 were added and resolved. Correcting those derived counts and summary wording does not change either qualification's status or establish new product direction.

### Resolution criteria

Recount the register index, verify one-to-one correspondence between index rows and full qualification entries, update the current-summary totals and Recovery Process qualification count from that authoritative set, and then incorporate the newly registered qualifications using the corrected baseline.

### Resolution

The pre-registration register was recounted and contains 177 unique qualification IDs, all Resolved, with one matching full entry per index row. PROC-010 and PROC-011 account for the two qualifications missing from the previous `175` summary baseline. The current summary and Recovery Process summary are corrected accordingly. After additionally registering RMAT-009, DEV-002, and DEV-003 as Open and this audit finding as Resolved, the register contains 181 tracked qualifications: 178 Resolved and 3 Open.

### Decision reference

None required; this is a register bookkeeping correction and establishes no new durable project direction.

### Resolved in

`91-design-qualification-register.md` current summary.

---

# 29. Workspace materialization and export qualifications

## ARCH-014 — Local Recovery Workspace Physical Boundary

Status: Resolved  
Type: Boundary Issue  
Origin: Workspace/materialization design discussion, 2026-09-26  
Owner document: `12-architecture.md`  
Depends on: D060, D106, D125, D141  
Blocks: None

### Qualification

How should a local Recovery Workspace physically separate LegacyRevive.NET's internal recovery mechanics from the reconstructed development files a developer works with?

### Why it matters

A flat workspace containing databases, recovery stores, candidate source, solution files, and checkpoints together would expose implementation plumbing and make the reconstructed development tree less natural to use with ordinary IDEs/editors.

### Current understanding

The canonical Recovery Workspace is a logical durable context; canonical state partitions remain logically distinct even if physically co-located. SQLite is the MVP persistence baseline, but the workspace directory boundary had not been defined.

### Resolution criteria

Preserve logical state boundaries, provide a stable LegacyRevive.NET-owned internal location, expose reconstructed files as a conventional developer workspace, and avoid deciding unrelated unresolved artifact-preservation details.

### Resolution

The MVP local Recovery Workspace root is developer-facing. LegacyRevive.NET-owned internal state is contained beneath a reserved `.legacyrevive` directory. The current reconstructed solution/project/source representation is materialized outside `.legacyrevive` for ordinary development-tool use. Exact internal sublayout is non-canonical unless separately governed. This does not resolve whether supplied original artifact bytes must be copied into workspace-owned storage or may remain through qualifying immutable references.

### Decision reference

D163

### Resolved in

`12-architecture.md` §27; synchronized with `14-mvp.md` and `02-terminology.md`.

---

## ARCH-015 — Original Artifact Preservation Strategy

Status: Resolved  
Type: Question  
Origin: Workspace/original-artifact preservation discussion, 2026-09-26  
Owner document: `12-architecture.md`  
Depends on: D106, D125, D129, D141, D163  
Blocks: None

### Qualification

Must the MVP snapshot/copy supplied original artifact bytes into workspace-owned immutable storage, or may a Recovery Workspace depend on immutable references to artifacts stored outside the workspace after intake?

### Why it matters

The choice affects resumability, replay, portability, source deletion/mutation resilience, storage duplication, integrity checking, and the meaning of "preserve original artifacts without modification." A recorded hash can detect external mutation but does not guarantee later availability.

### Current understanding

At qualification discovery, `12-architecture.md` permitted "immutable supplied recovery artifacts or immutable references to them." D125 already placed large binaries in an artifact filesystem/store rather than the SQLite database but did not decide whether that store must be workspace-owned. ARCH-014/D163 established the `.legacyrevive` physical boundary without settling artifact-byte ownership, leaving this question open for explicit resolution.

### Resolution criteria

Compare replay/determinism guarantees, portability, integrity/availability semantics, storage/copy cost, failure/atomicity behavior, and developer expectations. Preserve the distinction between integrity detection and guaranteed artifact availability.

### Resolution

For the MVP, successfully preserved supplied Original Artifact bytes are snapshotted into workspace-owned immutable artifact storage beneath `.legacyrevive`. Snapshot completion and SHA-256 verification are required before preservation is considered successful. After successful preservation, subsequent recovery/replay uses the workspace-owned preserved bytes; the original external path/name and capture context remain provenance rather than an availability dependency. A failed/incomplete snapshot remains explicitly not preserved. Preserved bytes are never modified in place, and a later materially different artifact re-enters through intake. Equal hashes do not collapse distinct Artifact identities/provenance; physical deduplication is permitted only as an implementation optimization that preserves those distinctions. Large binaries remain artifact-filesystem/store backed rather than SQLite BLOBs. Qualifying external immutable-reference modes are not part of the MVP decision and require separate governance.

### Decision reference

D167

### Resolved in

`12-architecture.md` §§5.1 and 27; synchronized with `14-mvp.md` and `00-ai-context.md`.

---

## SRC-012 — Manual Materialized-Source Changes and Developer-Adjusted Source

Status: Resolved  
Type: Boundary Issue  
Origin: Developer workflow discussion, 2026-09-26  
Owner document: `09-source-reconstruction.md`  
Depends on: D058, D073, D082, D163  
Blocks: None

### Qualification

When a developer manually changes reconstructed source using an ordinary IDE/editor, when does that filesystem change become canonical Developer-Adjusted Source and how may it be discarded?

### Why it matters

Developers should be able to use normal development tools without every keystroke or experiment silently becoming Developer Intervention or canonical recovery state.

### Current understanding

Developer-Adjusted Source already exists canonically and must result from deliberate Developer Intervention. Generated/reconstructed source remains distinct from Original Source and Original-System Evidence.

### Resolution criteria

Keep incidental filesystem mutation outside canonical recovery state, provide explicit adoption with provenance, provide a safe explicit discard path, preserve source supersession/reevaluation semantics, and remain IDE/editor independent.

### Resolution

Manual changes to materialized recovered source are initially Working-Tree Divergence only. Explicit adoption records the appropriate committed Developer Intervention and makes the adopted material provenance-bearing Developer-Adjusted Source. Explicit discard rematerializes canonical source and the abandoned edit does not enter canonical recovery history merely because it existed on disk.

### Decision reference

D165

### Resolved in

`09-source-reconstruction.md` §32; synchronized with `02-terminology.md`, `12-architecture.md`, and `14-mvp.md`.

---

## MVP-017 — Deterministic Export from Canonical Recovery State

Status: Resolved  
Type: Boundary Issue  
Origin: Export/workspace design discussion, 2026-09-26  
Owner document: `14-mvp.md`  
Depends on: D113, D139, D141, D163  
Blocks: None

### Qualification

What state is authoritative for `export`: the canonical Recovery Workspace/checkpoint state or the incidental current filesystem contents of the developer-facing materialization?

### Why it matters

Without a clear authority boundary, ordinary IDE edits could make export non-reproducible and the distinction between active recovery workspace and standalone development baseline would be unclear.

### Current understanding

MVP export is already required to be deterministic for the same effective Recovery Checkpoint/tool baseline, and Candidate Development Baseline is already canonical terminology.

### Resolution criteria

Preserve deterministic replay, define export as a meaningful operation distinct from copying the workspace, exclude LegacyRevive internal state, and identify the exported baseline with its source recovery state.

### Resolution

`export` deterministically materializes a standalone Candidate Development Baseline from canonical committed Recovery Workspace state/checkpoint plus replay-relevant context. It is not a blind copy of the materialized working tree, and `.legacyrevive` internal state is not part of the exported baseline.

### Decision reference

D164

### Resolved in

`14-mvp.md` §10; synchronized with `12-architecture.md`.

---

## MVP-018 — Export Handling of Working-Tree Divergence

Status: Resolved  
Type: Boundary Issue  
Origin: Export/divergence design discussion, 2026-09-26  
Owner document: `14-mvp.md`  
Depends on: D164, D165  
Blocks: None

### Qualification

What must `export` do when the developer-facing materialized reconstruction contains manual changes that have not been adopted into canonical recovery state?

### Why it matters

Silently copying the changes breaks deterministic export; silently overwriting/discarding them risks developer data loss; interactively adopting them inside export would mix export with mutation/intervention semantics and make automation fragile.

### Current understanding

Manual materialized-source changes remain Working-Tree Divergence until explicitly adopted/discarded. Export is a deterministic materialization from canonical state.

### Resolution criteria

Protect both deterministic export and developer edits, remain automation-safe, and keep adoption/discard as an explicit workflow separate from export.

### Resolution

Before export, LegacyRevive.NET verifies materialized reconstructed files against canonical recovery state. Material unresolved Working-Tree Divergence blocks export. Export reports affected paths/items and performs no implicit adoption, discard, overwrite, or inclusion. The developer resolves divergence explicitly and reruns export.

### Decision reference

D166

### Resolved in

`14-mvp.md` §10; synchronized with `09-source-reconstruction.md`, `12-architecture.md`, and `02-terminology.md`.

---

# 30. Development-process qualifications

## DEV-001 — Canonical Development Process and Development Slice Specifications

Status: Resolved  
Type: Question  
Origin: Implementation-planning and acceptance-verification discussion, 2026-09-27  
Owner document: `16-development-process.md`  
Depends on: D015, D016, D017, D020, D021, D022, D130, D132  
Blocks: `16-development-process.md`; governed use of Development Slice Specifications for implementation work

### Qualification

Should LegacyRevive.NET establish a canonical development process that governs how accepted canonical requirements are translated into bounded implementation slices, observable acceptance contracts, executable verification, supporting tests, qualification escalation, and durable completion records; and should that process be owned by a new canonical `16-development-process.md` using per-slice Development Slice Specification instances?

### Why it matters

The project now has a mature canonical design/governance baseline, but implementation work can still drift if requirements, acceptance behavior, code, and tests are connected only through exploratory chat or developer memory. LegacyRevive.NET has particularly strong semantic requirements around evidence, provenance, uncertainty, replay, deterministic behavior, preservation, and non-fabrication; code can compile and lower-level tests can pass while still violating those canonical semantics.

A repeatable implementation-development process could provide a durable bridge from canonical design to executable software, make implementation scope and acceptance behavior explicit before coding, expose material ambiguities before they become accidental code-level decisions, and preserve why a completed implementation slice was considered conformant. The process must add confidence and traceability without creating a competing governance system or heavyweight documentation that developers stop using.

### Current understanding

The discussion has converged on the following candidate model, but none of it is canonical until this qualification is resolved through the normal governance lifecycle:

- `91-design-qualification-register.md` remains the normative design-governance mechanism and `90-decisions.md` remains the owner of durable accepted decisions; the proposed development process would operate beneath those mechanisms rather than replace them.
- A **Development Slice** is a bounded unit of meaningful implementation work derived from identified canonical requirements.
- A **Development Slice Specification** is a durable, non-canonical working specification for one Development Slice. It is created before production implementation begins and is actively maintained through implementation, verification, and completion.
- Development Slice Specifications should use stable identifiers of the form `DEV-SPEC-<number>` because the durable artifact is the specification; “slice” describes the implementation unit it governs.
- Development Slice Specification instances should live in one predictable repository location separate from the canonical `00`–`16` document set. The current candidate convention is `docs/development/specifications/` with filenames such as `DEV-SPEC-001-workspace-local-artifact-intake.md`.
- A Development Slice Specification should identify its canonical basis and explicitly bound included and excluded scope so that implementation does not silently expand MVP or architectural commitments.
- Acceptance should be expressed first as observable propositions/contract statements derived from canonical requirements; representative scenarios should then be selected to demonstrate those propositions rather than becoming the requirements themselves.
- Executable acceptance verification should constrain implementation rather than merely describe it after the fact. Lower-level domain, integration, persistence, filesystem, component, or other tests support design and diagnosis but do not substitute for slice-level acceptance verification.
- Once a specification reaches **Ready for Implementation**, its acceptance contract becomes the implementation baseline and must not be weakened merely to make the implementation pass. A material semantic ambiguity or conflict discovered during implementation must return to the existing `91` qualification process rather than being silently decided in code.
- The candidate lifecycle is `Draft` → `Ready for Implementation` → `In Progress` → `Verification Pending` → `Implemented`, with `Withdrawn` and `Superseded` as terminal exceptional outcomes and explicit legal backward transitions where review or failed verification requires them.
- Blocking is orthogonal to lifecycle phase. Required metadata should therefore avoid overloading `Status`, with separate fields at least for `Status`, `Blocked by`, `Supersedes`, and `Superseded by`.
- Each specification should keep a lightweight lifecycle history containing only material lifecycle transitions, their date, and concise reason. Ordinary edits remain source-control history; the lifecycle history is not a general change log.
- An `Implemented` specification becomes a historically stable implementation/verification record. Later materially replacing work should normally create a new specification and supersede the earlier one rather than rewriting the earlier implementation history.
- The candidate required structure includes purpose, canonical basis, scope, acceptance contract, representative scenarios, executable verification, supporting verification, qualifications discovered, verification status, and lifecycle history, together with the lifecycle/supersession metadata.
- The first intended application is a `DEV-SPEC-001 — Workspace + Local Artifact Intake` specification for the first meaningful MVP vertical slice. Its acceptance work is expected to include complete file inventory, workspace-owned verified byte preservation, separate Artifact identity for distinct paths with identical bytes, retention of readable unsupported content, and workspace reopening/replay after the original supplied directory is removed. These are candidate implementation acceptance statements derived from already accepted canonical requirements, not new product semantics created by DEV-001.

### Resolution criteria

This qualification can be resolved when the project has explicitly decided and documented, without duplicating existing canonical ownership:

1. whether a canonical implementation-development process is required and whether a new `16-development-process.md` is its appropriate canonical owner;
2. the boundary between design governance (`90`/`91`), canonical product/design documents, the development process, Development Slice Specifications, and executable tests/code;
3. what qualifies as a Development Slice and which implementation changes require a Development Slice Specification, including any lightweight exemptions for trivial/refactoring-only work;
4. the purpose and authoritative/non-authoritative status of Development Slice Specifications;
5. the `DEV-SPEC-*` identifier, filename, repository-location, and stable-reference conventions;
6. the required Development Slice Specification structure, including canonical basis, explicit scope, acceptance contract, scenarios, executable verification, supporting verification, qualification tracking, verification status, metadata, and lifecycle history;
7. the Development Slice Specification lifecycle, legal transitions, mutability expectations, blocking semantics, withdrawal, and supersession behavior;
8. the rule for baselining acceptance at `Ready for Implementation` and the conditions under which substantive acceptance changes require governance rather than accommodation to implementation;
9. how acceptance propositions, representative scenarios, executable acceptance tests, and lower-level diagnostic tests relate without coupling acceptance to incidental implementation details;
10. how material questions discovered during implementation are escalated into `91` and how work resumes after governed resolution;
11. the completion/Definition-of-Done conditions for marking a Development Slice Specification `Implemented`;
12. how the process remains sufficiently lightweight to be used consistently while still producing durable traceability and verification confidence; and
13. the synchronization changes required in `00-ai-context.md` and any other affected canonical documents if `16-development-process.md` is accepted.

### In-review design conclusion

The active review finds the candidate development-process model consistent with the accepted governance, ownership, testing, and MVP decisions and proposes the following resolution for later canonicalization. This subsection is intentionally provisional while DEV-001 remains `In Review`; it is not the final `Resolution` and does not itself establish accepted project direction.

1. **Canonical owner and purpose** — LegacyRevive.NET should add `16-development-process.md` as the canonical owner of the implementation-development process: how accepted canonical requirements are converted into bounded implementation work and objectively verified before that work is considered complete. It must reference rather than redefine the subjects owned by `00`–`15`, `90`, and `91`.
2. **Governance boundary** — `90-decisions.md` and `91-design-qualification-register.md` remain the design-governance system. Canonical domain documents continue to own product/design semantics. `16-development-process.md` governs implementation discipline beneath those layers. Development Slice Specifications and tests cannot create or override canonical product semantics.
3. **Development Slice threshold** — A Development Slice is a bounded unit of meaningful implementation work that implements, changes, or materially verifies canonical product behavior, recovery semantics, persistent/replayable state behavior, user-visible capability, architectural boundary, or another acceptance-bearing obligation. A Development Slice Specification is not required for changes that are demonstrably non-semantic and non-acceptance-bearing, such as formatting, typo-only corrections, or internal refactoring that preserves all established behavior/contracts. If a change's materiality is uncertain, it should be treated as a Development Slice or raised for qualification rather than silently exempted.
4. **Development Slice Specification status** — Each material Development Slice is governed by one durable, non-canonical Development Slice Specification created before production implementation begins. It is subordinate to canonical documents and records how their requirements are scoped, accepted, verified, and completed for that implementation increment.
5. **Stable identity and location** — Development Slice Specifications use stable, never-reused identifiers `DEV-SPEC-<three-digit-number>` and stable filenames `DEV-SPEC-<number>-<short-slug>.md`. They live under `docs/development/specifications/`, separate from the canonical numbered document set, and remain at a stable path through their lifecycle.
6. **Required metadata without status overloading** — Each specification records at least `Status`, `Blocked by`, `Supersedes`, and `Superseded by`. `Status` records lifecycle phase only. `Blocked by` records current impediments independently. Supersession fields record historical replacement independently of blocking.
7. **Required structure** — Each specification contains: Purpose; Canonical basis; Scope with Included and Excluded boundaries; Acceptance contract; Representative scenarios; Executable verification; Supporting verification; Qualifications discovered; Verification status; and Lifecycle history. The specification should reference canonical sources rather than duplicate their definitions.
8. **Acceptance contract** — Acceptance propositions are derived from identified canonical requirements and state observable behavior without binding unnecessarily to incidental implementation details. Each proposition receives a stable identifier within the specification, using `AC-<AREA>-<three-digit-number>` as the normal convention. Representative scenarios are then selected to demonstrate one or more propositions; scenarios are examples used for verification and do not become product requirements merely by appearing in a DEV-SPEC.
9. **Executable and supporting verification** — Slice-level executable acceptance verification should be established before or alongside production implementation so that canonical requirements constrain the implementation rather than tests being derived retrospectively from the code. Acceptance verification should prefer Application/host/publicly observable behavior over internal schema/layout details unless an internal semantic is itself the governed contract. Lower-level domain, integration, persistence, filesystem, component, determinism, or other tests support design confidence and diagnosis but do not replace slice-level acceptance verification. `13-tooling-and-dependencies.md` continues to own the selected test technology.
10. **Lifecycle** — The normal lifecycle is `Draft` → `Ready for Implementation` → `In Progress` → `Verification Pending` → `Implemented`. `Withdrawn` and `Superseded` are terminal historical outcomes. Legal backward transitions are `Ready for Implementation` → `Draft` when specification work must be reopened, and `Verification Pending` → `In Progress` when verification exposes implementation work. Normal forward/terminal transitions are `Draft` → `Ready for Implementation` or `Withdrawn`; `Ready for Implementation` → `In Progress` or `Withdrawn`; `In Progress` → `Verification Pending` or `Withdrawn`; `Verification Pending` → `Implemented`, `In Progress`, or `Withdrawn`; and `Implemented` → `Superseded`. Blocking does not change lifecycle state.
11. **Mutability and acceptance baseline** — `Draft` is freely refinable. Entering `Ready for Implementation` establishes the acceptance contract as the implementation baseline. Non-semantic clarifications may still be made, but the contract must not be weakened or substantively changed merely to accommodate implementation. Before implementation begins, a substantive specification change returns the document to `Draft`. During implementation, a material ambiguity, conflict, unsupported assumption, boundary issue, or challenge to accepted canonical direction is registered in `91`; where it blocks correct implementation the DEV-SPEC records the qualification in `Blocked by` until governed resolution permits work to continue.
12. **Qualification integration** — The `Qualifications discovered` section records references to material qualifications registered in `91`; it is not an alternative unresolved-issue register. Resolution follows the existing D020 lifecycle. If governed resolution changes canonical semantics or the slice's legitimate acceptance obligations, the DEV-SPEC is updated consistently with that resolution rather than silently rewritten to fit code.
13. **Lifecycle history** — Every DEV-SPEC maintains a lightweight lifecycle history containing only the date, lifecycle transition, and concise reason for each material state transition. It is not a general change log; ordinary edits remain source-control history.
14. **Verification records** — The DEV-SPEC records the durable verification definition and locations of executable/supporting tests. Individual CI/build run history remains in CI/test infrastructure rather than being copied continually into the specification. `Verification status` records whether the specification's required verification is pending or satisfied, not an accumulating build log.
15. **Definition of Done** — A specification may transition from `Verification Pending` to `Implemented` only when its canonical basis and scope remain coherent; all acceptance propositions have been satisfied by the required repeatable verification (automated where reasonably practicable); required supporting tests pass; no unresolved blocking qualification remains; implementation respects applicable architecture/tooling/MVP boundaries; actual executable-verification references are recorded; and any canonical/governance changes exposed by the slice have completed their own synchronization lifecycle.
16. **Historical stability and replacement** — An `Implemented` DEV-SPEC becomes a historically stable implementation/verification record. Typographical/link maintenance may be corrected without changing its meaning, but later work that materially replaces its scope or acceptance behavior creates a new DEV-SPEC and links the old/new specifications through `Supersedes` / `Superseded by`. `Withdrawn` specifications are retained with a concise reason rather than deleted where they contain meaningful development history.
17. **Lightweight use** — The process applies to meaningful acceptance-bearing implementation slices, not every edit. One concise DEV-SPEC should normally cover one coherent vertical slice. The artifact exists to improve scope clarity, acceptance quality, qualification discovery, traceability, verification, and completion confidence rather than to duplicate project-management or source-control records.
18. **Synchronization if accepted** — Final acceptance of this model requires a durable decision in `90`, creation/audit of `16-development-process.md`, synchronization of the canonical document map and future-work guidance in `00-ai-context.md`, and targeted cross-reference review of affected documents such as `13-tooling-and-dependencies.md` and `14-mvp.md` without moving their existing ownership into `16`.
19. **First application** — After the process is canonicalized, the first intended instance is `DEV-SPEC-001 — Workspace + Local Artifact Intake`, which should apply the process to the already accepted first MVP intake/preservation slice rather than creating new product semantics.

No conflict with the currently accepted decisions was identified during this review. In particular, the proposed model consolidates implementation conclusions into durable documents (D015), preserves single canonical ownership (D016), routes material unresolved questions through `91` (D017/D020), requires later synchronization before resolution (D021), preserves history through supersession (D022), leaves test-framework ownership with tooling (D130), and strengthens end-to-end MVP acceptance rather than substituting disconnected subsystem tests (D132).

### Resolution


### Decision reference

D168

### Resolved in


---

## DEV-002 — Semantic Readiness of a Development Slice

Status: Resolved  
Type: Boundary Issue  
Origin: Post-implementation review of rejected `DEV-SPEC-004-stage-02-artifact-format-and-family-classification.md`, 2026-09-30  
Owner document: `16-development-process.md`  
Depends on: D016, D017, D020, D168, RMAT-009  
Blocks: readiness of the replacement Stage 02 Artifact format/family classification Development Slice Specification

### Qualification

What level of semantic determinacy is required before an acceptance-bearing Development Slice Specification may enter `Ready for Implementation`, so that implementation is free to choose implementation mechanics but is not forced to originate materially significant product semantics, evidence thresholds, classification truth conditions, domain invariants, failure meanings, or acceptance standards?

### Why it matters

The canonical Development Process already establishes that:

- canonical documents own product/design semantics;
- Development Slice Specifications are subordinate and non-canonical;
- code, tests, and Development Slice Specifications must not silently resolve material design questions;
- material ambiguities discovered during implementation return to the `91` governance process; and
- `Ready for Implementation` requires no material unresolved design question to be silently assumed.

The rejected DEV-SPEC-004 attempt therefore does not demonstrate that the project lacks a rule prohibiting implementation from inventing product semantics.

It exposes a narrower problem.

DEV-SPEC-004 passed a readiness assessment, established an acceptance contract, and proceeded through implementation and verification. Subsequent review nevertheless established that materially significant recognition truth conditions had not been defined. To implement the acceptance contract, the implementation agent necessarily had to choose among materially different semantic interpretations.

The process may therefore need a more explicit readiness test for determining when a specification is semantically determinate enough to delegate to implementation.

Without such a boundary, a DEV-SPEC can appear complete at the acceptance level while still embedding phrases whose truth conditions can only be supplied by implementation choices.

### Current understanding

D168 and `16-development-process.md` already establish the general authority boundary:

- design governance and canonical documents determine product/design semantics;
- a DEV-SPEC operationalizes accepted semantics for one bounded implementation increment;
- implementation and tests cannot create or reinterpret canonical semantics;
- a material ambiguity exposed during implementation must be registered in `91`.

The existing `Ready for Implementation` criteria require:

- identified canonical basis;
- explicit included/excluded scope;
- no silently assumed material unresolved design question;
- a defined acceptance contract;
- representative scenarios sufficient to begin; and
- an identified executable-verification approach.

The open issue is not whether implementation agents are permitted to invent semantics; they are not.

The open issue is whether the readiness criteria provide a sufficiently explicit method for detecting when an apparently concrete acceptance proposition still requires the implementer to decide materially significant semantic meaning.

Implementation mechanics should remain implementation choices where canonical behavior is already determined. Examples may include private decomposition, helper organization, buffering or streaming mechanics, internal naming, parser composition, and other choices that do not change externally or epistemically meaningful behavior.

### Resolution criteria

This qualification can be resolved when the project has deliberately established:

1. how a DEV-SPEC author/readiness review determines whether an acceptance-bearing requirement is semantically determinate enough for implementation;
2. what kinds of unresolved choices indicate that a requirement is still a design question rather than implementation discretion;
3. how the process distinguishes:
   - accepted semantic obligations;
   - acceptance-level specification;
   - implementation mechanics;
4. whether readiness requires explicit treatment of material:
   - truth conditions;
   - classification/evidence thresholds;
   - state meanings;
   - failure categories;
   - domain invariants;
   - precedence/ambiguity rules;
   where those are necessary to satisfy the slice;
5. how a readiness review should react when multiple materially different implementations could all plausibly claim to satisfy the same acceptance wording because the underlying canonical semantics are incomplete;
6. whether a specific semantic-readiness check should be added to the `Ready for Implementation` criteria, readiness assessment, Definition of Done, or another appropriate part of `16-development-process.md`;
7. how the rule remains general and lightweight rather than requiring every implementation detail to be canonically predetermined;
8. how implementation retains legitimate freedom over mechanics after semantic readiness is established; and
9. what synchronization is required in `16-development-process.md`, D168 or a new durable decision, `AGENTS.md`, and other implementation-facing guidance.

### Resolution

The project adopts an explicit semantic-readiness gate for material Development Slice Specifications.

A DEV-SPEC may enter `Ready for Implementation` only when the applicable canonical basis plus the bounded specification determine the materially significant semantic meaning of the acceptance-bearing behavior sufficiently that implementation can choose mechanics without inventing product/design semantics.

The readiness review applies the **material semantic choice test**:

> **Could two implementations make materially different semantic choices and both plausibly claim that the current canonical basis and acceptance wording permit them?**

If yes because the authoritative semantics do not determine the choice, semantic readiness fails. The specification remains or returns to `Draft` before implementation, the missing semantic question is registered in `91`, and the appropriate canonical owner resolves it before readiness is reconsidered. If the issue is discovered during implementation, the existing implementation-discovered qualification/blocking flow applies.

Material semantic choices include, where relevant:

- truth conditions for classifications, states, accepted outcomes, or comparable product claims;
- evidence, recognition, or classification thresholds;
- state meanings/transitions;
- failure categories and boundaries;
- domain invariants;
- precedence, overlap, ambiguity, or conflict rules;
- persistence/history semantics;
- provenance or epistemic categorization; and
- other externally or developer-visible behavior whose alternatives materially change what LegacyRevive.NET claims, preserves, reports, accepts, rejects, or leaves unresolved.

The existence of multiple possible implementations does not itself indicate semantic unreadiness. Private decomposition, internal naming, parser/adapter composition, buffering/streaming, local algorithms/data structures, incidental persistence mechanics, and similar choices remain implementation discretion when all alternatives preserve the same accepted semantics and architectural/tooling constraints.

Each material DEV-SPEC now includes a concise `Readiness assessment` recording the canonical semantic basis, material semantic qualification blockers if any, implementation discretion intentionally left open, and `Pending` / `Blocked` / `Pass`. `Ready for Implementation` requires `Pass`.

Executable tests cannot make an underdetermined requirement semantically ready by selecting one interpretation. Tests demonstrate accepted semantics; they do not create the missing canonical rule.

Definition of Done additionally requires that the semantic-readiness basis still holds and that implementation did not silently originate a material product/design semantic that should have passed through governance.

The rule is deliberately lightweight and does not require canonical predesign of private implementation mechanics. Historical `Implemented` DEV-SPECs are not rewritten merely to add the later readiness-assessment section.

RMAT-009/D172 provides the concrete motivating example: once family-specific Stage 02 recognition truth conditions were canonically defined, a replacement Stage 02 DEV-SPEC can now derive acceptance from those semantics without delegating recognition thresholds to implementation.

### Decision reference

D173

### Resolved in

`16-development-process.md` §§9, 12A, 17.2, 18, 22, 25, 30–31; synchronized with `AGENTS.md` implementation-facing guidance.


---

## DEV-003 — Post-Implemented Rejection of a Development Slice

Status: Resolved  
Type: Ambiguity  
Origin: Post-implementation review and repository rollback of `DEV-SPEC-004-stage-02-artifact-format-and-family-classification.md`, 2026-09-30  
Owner document: `16-development-process.md`  
Depends on: D020, D022, D168, D173  
Blocks: None

### Qualification

How should LegacyRevive.NET represent a Development Slice Specification that has transitioned to `Implemented` after its stated verification passed, but whose entire implementation is subsequently rejected before durable acceptance/commit because later review establishes that the acceptance/design basis itself was materially inadequate?

### Why it matters

The current Development Slice lifecycle defines:

- `Implemented` as a completed, historically stable implementation/verification record;
- `Withdrawn` as a specification whose slice will not proceed under that specification;
- `Superseded` as an implemented specification materially replaced by a later Development Slice Specification.

The current legal transition model permits `Implemented` only to transition to `Superseded`.

The rejected DEV-SPEC-004 attempt does not fit those semantics cleanly:

- the working specification transitioned to `Implemented`;
- its executable and supporting verification passed as then defined;
- later review established that the underlying acceptance/design basis was insufficient;
- the entire production/test implementation was rejected;
- repository implementation was restored to the accepted post-DEV-SPEC-003 baseline;
- the rejected implementation was not retained as accepted implementation history;
- no later Development Slice Specification yet exists to supersede DEV-SPEC-004;
- leaving DEV-SPEC-004 as `Implemented` would misleadingly imply that it remains a valid stable implementation/verification record;
- the existing withdrawal rule is framed around abandonment before implementation completion.

Deleting DEV-SPEC-004 would also lose meaningful development and governance history.

The lifecycle therefore appears not to explicitly represent a completion judgment that is later itself rejected before becoming durable accepted implementation history.

### Current understanding

The following remain established:

- lifecycle history must preserve material historical transitions rather than rewrite them away;
- an `Implemented` specification is intended to be historically stable;
- later materially replacing an accepted implemented slice normally uses a new DEV-SPEC and supersession;
- meaningful withdrawn or superseded specifications remain preserved;
- durable design decisions are superseded rather than silently rewritten;
- ordinary source-control history and DEV-SPEC lifecycle history have different purposes.

The current situation must not be resolved by silently pretending that DEV-SPEC-004 never reached `Implemented`.

It must also not be resolved by leaving `Implemented` as the current state if that would falsely assert that the rejected slice remains a valid implementation/verification record.

No current lifecycle status or transition should be reinterpreted, and no new lifecycle state should be invented, before this qualification is deliberately resolved.

The fact that the rejected implementation was uncommitted is relevant historical context, but the current canonical model has not established whether source-control commit status should be a lifecycle semantic.

### Resolution criteria

This qualification can be resolved when the project has deliberately established:

1. how to represent a prior `Implemented` transition whose completion judgment is subsequently found unsound;
2. whether the correct model should:
   - use an existing lifecycle state with clarified semantics;
   - introduce a new lifecycle state or transition;
   - distinguish implementation completion from durable acceptance;
   - use another explicit history-preserving mechanism;
3. whether and how the lifecycle model distinguishes:
   - failed verification before `Implemented`;
   - implementation abandoned before completion;
   - later replacement of a valid implemented slice;
   - rejection of the completion/acceptance basis itself after `Implemented`;
4. whether source-control commit or another durable repository-acceptance event has any canonical relationship to DEV-SPEC lifecycle, without conflating lifecycle history with ordinary version-control history;
5. how lifecycle history preserves the fact that the specification did reach `Implemented` under the then-current judgment while making the current rejected status equally clear;
6. how a rejected specification is retained as meaningful historical material without implying that its implementation remains accepted;
7. how future replacement work relates to the rejected specification, including whether supersession is appropriate and under what conditions;
8. the correct immediate durable treatment of DEV-SPEC-004 once the qualification is resolved; and
9. what updates are required to lifecycle states, legal transitions, Definition of Done, historical stability rules, the standard DEV-SPEC template, and implementation-facing guidance.

### Resolution

The project introduces a terminal `Rejected` lifecycle status for the narrow case where a Development Slice Specification previously reached `Implemented`, but later deliberate review establishes that the completion/acceptance judgment itself was materially unsound and the implementation is no longer accepted as a valid completed slice.

The legal transition is:

```text
Implemented
    ↓
Rejected
```

The historical `Implemented` transition is not deleted or rewritten. The later rejection is appended to lifecycle history with its reason and governing qualification/decision, preserving both the fact that completion was previously judged satisfied and the later fact that the judgment was invalidated.

`Rejected` is deliberately distinct from:

- failed verification before `Implemented`, which normally returns `Verification Pending → In Progress`;
- abandonment before valid completion, which uses `Withdrawn`;
- later replacement of a valid implemented slice, which uses `Superseded`; and
- ordinary source-control history.

The project does not add a separate durable-acceptance lifecycle phase and does not make source-control commit a universal lifecycle condition. `Implemented` remains the accepted completion judgment after all applicable canonical and slice-specific completion conditions are judged satisfied. Explicit slice-specific completion conditions that are compatible with the canonical process are binding for that slice. A later discovery that such a condition was not actually satisfied may contribute to a governed `Implemented → Rejected` transition.

For DEV-SPEC-004 specifically, the current durable treatment is:

- retain the specification and its full historical lifecycle;
- change current `Status` from `Implemented` to `Rejected`;
- append an `Implemented → Rejected` lifecycle event dated 2026-09-30;
- preserve the historical 77/77 verification result as a statement about verification under the then-defined contract, not as evidence that the completion judgment remains accepted;
- record that post-implementation review established materially inadequate Stage 02 recognition semantics, leading to rejection of the implementation and restoration of the post-DEV-SPEC-003 repository baseline;
- additionally record that DEV-SPEC-004's own stated completion condition requiring the implementation to be committed as a coherent Development Slice had not been satisfied; and
- retain `Supersedes: None` / `Superseded by: None` until a later replacement DEV-SPEC exists.

A later replacement Stage 02 slice must be a new DEV-SPEC. It may use the supersession metadata to preserve replacement lineage to the rejected DEV-SPEC-004, but DEV-SPEC-004 remains `Rejected` rather than becoming `Superseded`, because rejection records the disposition of its own completion judgment.

No new material qualification was exposed by this resolution. DEV-003 therefore closes the post-DEV-SPEC-004 governance set together with RMAT-009/D172 and DEV-002/D173.

### Decision reference

D174

### Resolved in

`16-development-process.md` §§8, 16–17, 22–25, 30–31; synchronized with `AGENTS.md` and the retained `DEV-SPEC-004-stage-02-artifact-format-and-family-classification.md`.

---

## DEV-004 — Recognition Boundary and Falsification Verification

Status: Resolved  
Type: Validation Need  
Origin: Post-implementation review of rejected `DEV-SPEC-005-stage-02-artifact-format-and-family-classification.md`, 2026-10-01  
Owner document: `16-development-process.md`  
Depends on: D168, D173, RMAT-009, RMAT-010, D175  
Blocks: readiness and completion criteria of the replacement Stage 02 Artifact format/family classification Development Slice Specification

### Qualification

What systematic boundary and falsification-oriented verification must an acceptance-bearing recognition/classification Development Slice Specification define before it may enter implementation and later be judged `Implemented`, so that executable tests demonstrate not only representative success cases but also that weaker, misleading, near-match, malformed, overlapping, and parser-sensitive evidence cannot accidentally satisfy or fabricate the governed recognition claim?

### Why it matters

DEV-002 / D173 established the semantic-readiness gate: implementation must not originate material product semantics, and tests cannot repair underdetermined canonical requirements by choosing one interpretation. DEV-SPEC-005 applied that rule, derived a detailed acceptance contract from RMAT-009/D172, required positive and negative recognizer coverage, and completed with 127/127 passing tests.

Post-implementation review nevertheless found material recognition-boundary defects that the executable suite did not expose. The tests demonstrated the representative examples that had been specified, but they did not systematically try to falsify each recognition predicate with adversarial near-matches. For example, a valid ZIP containing a root `.nuspec` filename whose content did not establish a NuSpec family could be misrepresented as malformed NuGet, while loose textual checks could overstate malformed XML/JSON family candidacy.

The Development Process currently requires representative scenarios and says negative/failure scenarios should be included where successful behavior alone cannot demonstrate an important boundary. That direction is sound but may be too discretionary for classifiers whose correctness depends as much on rejecting insufficient evidence as on accepting sufficient evidence.

The open question is therefore procedural rather than a request to move recognition semantics into `16-development-process.md`: once a canonical owner defines a material recognition/evidence boundary, what minimum verification discipline must a DEV-SPEC use to demonstrate that implementation respects both sides of that boundary?

### Current understanding

The following remain accepted and are not reopened:

- canonical documents own product/design semantics; DEV-SPECs operationalize them;
- semantic readiness under D173 remains mandatory;
- executable acceptance tests demonstrate accepted semantics but do not create missing semantics;
- representative scenarios are examples, not canonical definitions;
- negative/failure scenarios are already expected where successful behavior alone cannot demonstrate an important boundary;
- supporting tests do not substitute for slice-level acceptance verification; and
- implementation mechanics remain free where they preserve accepted semantics.

The unresolved point is whether recognition/classification slices require a more explicit, systematic boundary-verification model before `Ready for Implementation` and/or before `Implemented`, and what that model should minimally contain without turning the process into exhaustive combinatorial testing.

### Resolution criteria

This qualification can be resolved when the project has deliberately established:

1. whether material recognition/classification DEV-SPECs require an explicit boundary/falsification verification section or matrix;
2. the minimum categories that must be considered for each material recognition predicate, where applicable, such as:
   - positive/minimum sufficient evidence;
   - one-required-condition-missing cases;
   - lookalike or misleading content;
   - malformed-candidate boundary;
   - valid other/broader format;
   - incidental token/name/prefix collision;
   - container/subtype boundary;
   - overlap/ambiguity;
   - parser/tool/operational failure;
   - recognizer-order independence;
3. whether every positive recognition predicate must include at least one adversarial near-match proving that materially weaker evidence does not satisfy it;
4. how this verification obligation differs from semantic readiness and avoids allowing tests to define missing canonical semantics;
5. how the obligation scales so that it is rigorous for evidence-sensitive classifiers without requiring exhaustive input enumeration;
6. whether the requirement belongs in readiness, representative-scenario guidance, executable verification, Definition of Done, or a combination of those sections;
7. what evidence a final Definition-of-Done audit must inspect before accepting a classifier slice as `Implemented`; and
8. what synchronization is required across `16-development-process.md`, `AGENTS.md`, future DEV-SPEC templates/guidance, and DEV-SPEC-006.


### Resolution

LegacyRevive.NET adopts a mandatory boundary/falsification verification discipline for material recognition/classification Development Slices.

Before `Ready for Implementation`, an applicable DEV-SPEC must contain an explicit boundary/falsification verification matrix derived from the already-governed canonical semantics. For each material recognition predicate it must record the minimum sufficient evidence and, where applicable, materially independent missing-condition cases, lookalike/misleading content, malformed-candidate boundaries, valid broader/other formats, incidental token/name/prefix collisions, container/subtype boundaries, overlap/ambiguity, parser/tool/operational failure, and recognizer-order independence.

Every positive material recognition predicate requires at least one adversarial near-match that preserves superficial similarity while lacking or violating a canonically required condition. This is the minimum falsification obligation; the process does not require exhaustive enumeration of every malformed input or combinatorial permutation. Categories that genuinely do not apply may be marked `Not applicable` only with an explicit rationale.

The matrix is verification structure, not semantic authority. Each expected outcome must be traceable to the canonical basis. If the expected outcome cannot be stated without selecting a material semantic interpretation, the specification fails D173 semantic readiness and the missing question is governed before implementation. Tests cannot choose the missing rule.

Executable acceptance verification must exercise the materially applicable matrix rows. Supporting/unit tests may diagnose individual recognizers, but do not replace slice-level acceptance verification. Parser/library defaults do not count as product semantics unless LegacyRevive.NET explicitly maps them to the governed outcome.

Before `Verification Pending → Implemented`, the Definition-of-Done audit must inspect matrix-to-test coverage, confirm the required adversarial near-match for every positive material predicate, confirm applicable boundary categories are exercised or explicitly justified as `Not applicable`, confirm no acceptance-bearing matrix row is silently skipped, and confirm any semantic gap exposed by boundary design was governed before completion. Aggregate green test counts are insufficient by themselves.

This requirement is deliberately scoped to slices where recognition/classification/evidence-threshold boundaries are material. It does not impose a universal boundary matrix on ordinary implementation slices where those semantics are absent.

A targeted governance audit found no conflict with D168, D173, RMAT-009/D172, RMAT-010/D175, the Evidence Model, tooling ownership, architecture boundaries, or MVP scope. The rule strengthens verification discipline without moving recognition truth conditions into `16-development-process.md`.

### Decision reference

D176

### Resolved in

`16-development-process.md` §§13.1, 14, 20, 25 and applicable Definition-of-Done guidance; synchronized with `AGENTS.md`. DEV-SPEC-006 must apply D176 when created.

---

# 31. Adding new qualifications

When future document audits uncover new unresolved or material resolved points:

1. choose the appropriate domain prefix;
2. assign the next unused ID;
3. add the item to the register index;
4. create the full entry;
5. record the originating document/audit;
6. identify the owner document;
7. record dependencies and blockers;
8. retain resolved items permanently.

Do not create separate ad-hoc unresolved-issue lists without also registering material qualifications here.

Documents may reference qualification IDs, but this register remains the single source of truth for qualification status.

---

# 32. Current summary

As of this version of the register:

```text
Resolved qualifications: 183
Open qualifications:      0
In Review qualifications: 0
Blocked qualifications:   0
Deferred qualifications:  0
Superseded qualifications: 0
Total tracked:            183
```

The resolved qualifications consist of:

- six `DOC-*` findings from the original audit and correction of `01-purpose-and-principles.md`;
- seven `DOC-*` findings from the governed audit and correction of `00-ai-context.md`;
- two `DOC-*` findings from the governed re-audit of `01-purpose-and-principles.md`;
- two `DOC-*` findings from the governed re-audit of `02-terminology.md`;
- four `GOV-*` findings from the joint governance audit of `90-decisions.md` and `91-design-qualification-register.md`.

The ten `EVID-*` qualifications originated from the audit of `02-terminology.md` and were resolved by `03-evidence-model.md`.

Seven `INTV-*` qualifications were identified and resolved during generation/audit of `04-developer-intervention.md`, completing the detailed developer-interaction workflow semantics while preserving the Evidence Model boundary.

Eight `CONV-*` qualifications were identified and resolved during generation/audit of `05-convention-inference.md`, establishing scoped, provenance-bearing convention observations and anti-feedback-loop rules.

Ten `RMAT-*` qualifications are resolved. RMAT-001 through RMAT-008 establish artifact contribution classes, artifact-instance boundaries, companion-artifact rules, and recovery ceilings. RMAT-009/D172 establishes the normative Stage 02 Artifact format/family recognition contract, family-specific minimum recognition conditions, explicit non-success/unsupported outcomes, overlap rules, and the Stage 02 / Stage 03 inspection-output boundary. RMAT-010/D175 establishes the minimum direct structural candidate-discriminator threshold for malformed/corrupt family candidacy and prevents names, raw substrings, parser rejection, or incidental text from fabricating family membership.

Four `DEV-*` qualifications are resolved. DEV-004/D176 adds mandatory risk-based boundary/falsification verification for material recognition/classification Development Slices, including an adversarial near-match for every positive material recognition predicate and Definition-of-Done matrix-to-test coverage review.

`DOC-018` records and resolves a stale Evidence Model status sentence discovered in `01-purpose-and-principles.md` during this cycle.

`DOC-019` records and resolves the whole-set epistemic taxonomy/glossary synchronization issue and is linked to D160. `DOC-020` is now Resolved by synchronizing the existing D124/D147/D153 boundary: remote enrichment is post-MVP capability scope while `NuGet.Protocol` remains the contingent selected adapter. `DOC-021` records and resolves stale validation-result terminology discovered by the subsequent whole-set re-audit. `DOC-022` records and resolves the stale qualification-summary count found during workspace governance synchronization. `DOC-023` records and resolves premature canonical-status wording discovered by the DEV-001/D168 Step 6 audit while preserving DEV-001 itself as `In Review`. `DOC-024` records and resolves the stale qualification-summary baseline discovered during the RMAT-009/DEV-002/DEV-003 registration audit after PROC-010 and PROC-011 had been added without corresponding summary synchronization.

Eleven `PROC-*` qualifications are resolved. PROC-010 and PROC-011 additionally establish the Stage 01 semantics for incomplete supplied-directory enumeration and for preserving trustworthy positive file discovery when later descendant discovery fails.

Ten `PROJ-*` qualifications were identified and resolved during generation/audit of `08-project-reconstruction.md`, establishing Candidate Project boundaries, assembly anchoring, dependency-representation rules, alternative candidate handling, and generated project/solution artifact boundaries.

Twelve `SRC-*` qualifications are resolved. SRC-012 additionally governs manual materialized-source edits, explicit adoption as Developer-Adjusted Source, and discard/rematerialization semantics.

Ten `VAL-*` qualifications were identified and resolved during generation/audit of `10-validation-strategy.md`, establishing target/reference identity, multidimensional plans, non-comparability semantics, blind evaluation, feedback-lineage protection, validation provenance, and Validated Baseline semantics.

Eleven `METR-*` qualifications were identified and resolved during generation/audit of `11-recovery-metrics.md`, establishing explicit denominators, correctness/coverage separation, recoverability-aware measurement, fidelity/operational/epistemic metric families, intervention burden, aggregation limits, and cross-run comparability.

Fifteen `ARCH-*` qualifications are resolved. ARCH-014 establishes the `.legacyrevive` local workspace physical boundary; ARCH-015/D167 establishes workspace-owned immutable byte snapshots for successfully preserved MVP Original Artifacts and removes external source paths as replay dependencies after successful intake.

Seventeen `TOOL-*` qualifications are resolved. TOOL-001 through TOOL-015 selected the .NET runtime/toolchain baseline, metadata/decompiler/PDB/Roslyn/MSBuild/NuGet/persistence/host/test technologies, and dependency security/version-governance rules; TOOL-016 historically selected `.slnx` and the former `LegacyRevive.slnx` repository filename; TOOL-017/D169 retains SLNX while establishing `LegacyRevive.Net.slnx` as the current canonical repository solution filename. Candidate Solution output semantics remain separate.

Eighteen `MVP-*` qualifications are resolved. MVP-017 and MVP-018 additionally define deterministic canonical-state export and the rule that unresolved Working-Tree Divergence blocks export and is reported for explicit resolution.

Twelve `ROAD-*` qualifications were identified and resolved during generation/audit of `15-roadmap.md`, establishing evidence-gated post-MVP horizons, promotion criteria, core-first sequencing, enrichment/inference/host/plugin/scale boundaries, validation growth, and native/runtime research boundaries.

`DEV-001` is Resolved through D168 and canonical `16-development-process.md`. DEV-002 is Resolved through D173: material DEV-SPECs require an explicit semantic-readiness assessment before implementation delegation. DEV-003 is Resolved through D174: an `Implemented` completion judgment that is later deliberately found materially unsound transitions to terminal `Rejected` while preserving the historical `Implemented` event; failed verification, withdrawal, supersession, and source-control history remain distinct. DEV-SPEC-004 is retained as the first historical application of that rule. DOC-023 records the earlier audit-stage correction that prevented `16` from being presented as fully canonical before the DEV-001 synchronization lifecycle completed.

One qualification is currently Open and continues to block the replacement Stage 02 classification slice: DEV-004 requires the Development Process to settle the boundary/falsification verification discipline for evidence-sensitive recognition/classification DEV-SPECs. RMAT-010/D175 is resolved and supplies the malformed/corrupt candidate-evidence semantics that DEV-004 verification must demonstrate.

---

# 33. Closing principle

The LegacyRevive.NET documentation process should treat unresolved design knowledge with the same discipline that LegacyRevive.NET intends to apply to recovered software knowledge:

> **Preserve provenance, preserve uncertainty, record correction history, and never allow an unresolved assumption to silently become accepted fact.**
