# LegacyRevive.NET — Development Process

## Status

**Canonical**

DEV-001 has completed the governed lifecycle and is Resolved. D168 establishes the durable development-process direction owned by this document.

This document is governed by:

- DEV-001 — Canonical Development Process and Development Slice Specifications;
- D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications.

---

# 1. Purpose

This document defines how accepted LegacyRevive.NET canonical requirements are converted into bounded implementation work and objectively verified before that work is considered complete.

It owns the implementation-development process for:

- defining a Development Slice;
- deciding when a Development Slice Specification is required;
- creating and maintaining Development Slice Specifications;
- tracing slice work to canonical requirements;
- defining observable acceptance contracts;
- selecting representative verification scenarios;
- establishing executable acceptance verification;
- relating acceptance verification to lower-level tests;
- escalating implementation-discovered material design questions into the canonical governance process;
- managing Development Slice Specification lifecycle, blocking, withdrawal, supersession, and completion;
- preserving durable implementation/verification history.

The objective is to provide a durable bridge:

```text
canonical requirement
        ↓
bounded Development Slice
        ↓
Development Slice Specification
        ↓
observable acceptance contract
        ↓
representative scenario(s)
        ↓
executable acceptance verification
        ↓
implementation + supporting verification
        ↓
acceptance satisfied
        ↓
implemented slice with durable history
```

This process exists to reduce reliance on exploratory chat, developer memory, or code-first interpretation when implementing semantically important LegacyRevive.NET behavior.

---

# 2. Canonical ownership boundary

This document does not replace existing governance or canonical owners.

The ownership model is:

```text
90-decisions.md
    durable accepted decisions

91-design-qualification-register.md
    qualification lifecycle and governance

00–15 canonical domain/design documents
    authoritative product/design semantics

16-development-process.md
    implementation-development discipline

DEV-SPEC-* documents
    per-slice implementation specifications

tests + production code
    executable verification and implementation
```

The following boundaries are mandatory:

- `90-decisions.md` owns durable accepted decisions.
- `91-design-qualification-register.md` owns qualification state, history, and the normative governance lifecycle.
- Canonical domain/design documents own the authoritative meaning of their respective subjects.
- `13-tooling-and-dependencies.md` owns testing framework and test-tool technology choices.
- `14-mvp.md` owns MVP scope and release acceptance gates.
- This document owns the process for translating accepted requirements into verified implementation work.
- Development Slice Specifications are durable but **non-canonical** implementation artifacts.
- Tests and implementation cannot create, replace, weaken, or reinterpret canonical product semantics.

Where implementation reveals a material question about canonical meaning, the question returns to the governance process in `91` rather than being decided implicitly in code, tests, or a Development Slice Specification.

---

# 3. Core development principle

A material implementation slice is not complete merely because:

- production code exists;
- the solution builds;
- lower-level unit or integration tests pass;
- a particular storage or architecture choice appears internally consistent.

A material implementation slice is complete only when the observable canonical behavior it was intended to implement has been specified and satisfactorily verified.

The governing principle is:

> **Canonical requirements govern; executable acceptance demonstrates.**

Executable acceptance is evidence that an implementation conforms to the accepted requirement for the bounded slice. It does not change the requirement itself.

---

# 4. Development Slice

A **Development Slice** is a bounded unit of meaningful implementation work that implements, changes, or materially verifies one coherent set of acceptance-bearing obligations.

A Development Slice normally applies where work affects one or more of:

- canonical product behavior;
- recovery semantics;
- evidence/provenance boundaries;
- persistent or replayable state behavior;
- user-visible capability;
- externally observable application behavior;
- architectural responsibility or dependency boundaries;
- deterministic behavior;
- preservation/integrity semantics;
- developer-intervention behavior;
- another material acceptance-bearing obligation.

A Development Slice should be small enough that its purpose, scope, acceptance contract, and verification can be understood as one coherent implementation increment.

A single slice should not be enlarged merely because adjacent work is technically convenient.

---

# 5. When a Development Slice Specification is required

Every material Development Slice requires one Development Slice Specification.

A Development Slice Specification is not required merely for process formality when a change is demonstrably non-semantic and non-acceptance-bearing, for example:

- formatting-only edits;
- typo-only corrections;
- comment-only corrections that do not alter a contract;
- behavior-preserving internal refactoring already covered by existing acceptance verification;
- mechanical repository maintenance that does not alter established behavior or boundaries.

If it is materially unclear whether a proposed change is exempt, the work should be treated as a Development Slice or the uncertainty should be raised through the qualification process rather than silently exempted.

---

# 6. Development Slice Specification

A **Development Slice Specification** is the durable working specification for one Development Slice.

It is:

- created before production implementation begins;
- actively used during implementation and verification;
- maintained through its lifecycle;
- subordinate to canonical Project documents;
- retained after completion as durable implementation history.

It records how accepted canonical requirements are:

- selected for the slice;
- bounded;
- translated into observable acceptance propositions;
- demonstrated through representative scenarios;
- verified executably;
- supported by lower-level tests;
- affected by discovered qualifications;
- judged complete.

A Development Slice Specification does not become a new canonical owner for the requirements it references.

If a Development Slice Specification conflicts with an accepted canonical source, the canonical source governs and the conflict must be handled through the normal qualification process.

---

# 7. Stable identity and repository location

Development Slice Specifications use stable identifiers:

```text
DEV-SPEC-<three-digit-number>
```

Examples:

```text
DEV-SPEC-001
DEV-SPEC-002
DEV-SPEC-003
```

Identifiers:

- are allocated sequentially;
- are never reused;
- remain stable throughout the document lifecycle.

The normal filename convention is:

```text
DEV-SPEC-<number>-<short-slug>.md
```

Example:

```text
DEV-SPEC-001-workspace-local-artifact-intake.md
```

Development Slice Specifications live under:

```text
docs/development/specifications/
```

This location is deliberately separate from the canonical numbered document set.

A specification remains at a stable path through its lifecycle. Lifecycle state is recorded inside the document rather than represented by moving the file between status-specific directories.

---

# 8. Required metadata

Each Development Slice Specification records at least:

```text
Status:
Blocked by:
Supersedes:
Superseded by:
```

These fields describe distinct concerns and must not be collapsed into one status value.

## 8.1 Status

`Status` records lifecycle phase only.

Allowed values are:

- `Draft`;
- `Ready for Implementation`;
- `In Progress`;
- `Verification Pending`;
- `Implemented`;
- `Withdrawn`;
- `Superseded`.

## 8.2 Blocked by

`Blocked by` records current impediments independently of lifecycle phase.

Typical values are:

```text
None
DEV-002
ARCH-016
<other qualification or explicit blocker>
```

A blocked specification retains its lifecycle status.

Example:

```text
Status: In Progress
Blocked by: DEV-002
```

`Blocked` is therefore not a Development Slice Specification lifecycle status.

## 8.3 Supersedes / Superseded by

These fields record durable replacement relationships between Development Slice Specifications.

They remain distinct from blocking and ordinary lifecycle progression.

---

# 9. Required document structure

Every Development Slice Specification contains the following sections.

```text
# DEV-SPEC-xxx — <Title>

Status:
Blocked by:
Supersedes:
Superseded by:

## Purpose

## Canonical basis

## Scope

### Included

### Excluded

## Acceptance contract

## Representative scenarios

## Executable verification

## Supporting verification

## Qualifications discovered

## Verification status

## Lifecycle history
```

Additional sections may be added when they materially improve the specification, but the required structure should remain recognizable across slices.

The specification should reference canonical sources rather than copy large portions of their definitions.

---

# 10. Canonical basis

The `Canonical basis` section identifies the accepted requirements that justify and constrain the Development Slice.

It may reference:

- canonical documents and sections;
- durable decision IDs;
- resolved qualification IDs where useful for historical traceability.

Example:

```text
## Canonical basis

- 07-recovery-process.md §...
- 12-architecture.md §...
- 14-mvp.md §...
- D134
- D141
- D167
```

The purpose is traceability, not duplication.

A Development Slice Specification must not silently expand beyond its canonical basis.

Where proposed implementation work requires new product/design semantics, those semantics must first pass through the normal governance process.

---

# 11. Scope

Every Development Slice Specification explicitly records included and excluded work.

Example:

```text
## Scope

### Included

- Recovery Workspace creation/opening
- local artifact enumeration
- immutable artifact preservation
- SHA-256 verification
- inventory persistence
- workspace reopen/replay

### Excluded

- managed metadata extraction
- dependency inference
- Candidate Project reconstruction
- source reconstruction
- build
```

Explicit exclusions are important because a technically convenient adjacent capability is not automatically part of the slice.

Scope must remain consistent with the owning canonical documents, especially MVP boundaries.

---

# 12. Acceptance contract

The `Acceptance contract` defines the observable propositions that the slice must satisfy.

Acceptance propositions:

- derive from identified canonical requirements;
- describe what must be true;
- avoid unnecessary dependence on incidental implementation detail;
- are stable references within the specification;
- constrain implementation.

The normal identifier convention is:

```text
AC-<AREA>-<three-digit-number>
```

Example:

```text
AC-INTAKE-001
AC-INTAKE-002
AC-INTAKE-003
```

An acceptance proposition is not the same thing as a test case.

Example:

```text
AC-INTAKE-003

Distinct supplied artifact instances remain distinct Artifact
identities even when their content hashes are equal.
```

That proposition may be verified by one or more scenarios.

Acceptance propositions do not create new canonical product semantics. They operationalize already accepted requirements for the bounded Development Slice.

---

# 13. Representative scenarios

Representative scenarios are controlled examples selected to demonstrate one or more acceptance propositions.

The sequence is:

```text
canonical requirement
        ↓
acceptance proposition
        ↓
representative scenario(s)
        ↓
executable verification
```

The scenario must not be mistaken for the requirement itself.

For example, a scenario containing two files with identical bytes may demonstrate a requirement that equal content hashes do not collapse distinct Artifact identity. The exact filenames or directory structure used by the scenario do not thereby become product requirements.

A scenario should normally state which acceptance propositions it exercises.

Small representative scenarios are preferred where they demonstrate the required behavior without introducing unrelated complexity.

Negative/failure scenarios should be included where successful behavior alone cannot demonstrate an important boundary.

---

# 14. Executable acceptance verification

Each material Development Slice has executable verification for its acceptance contract where reasonably practicable.

Executable acceptance verification should be established before or alongside production implementation so that the accepted contract constrains the implementation rather than merely documenting whatever was built.

Verification should prefer:

- Application-level behavior;
- host/CLI behavior where the host itself is part of the accepted contract;
- durable observable state through supported application abstractions;
- public domain/application contracts;

over unnecessary coupling to:

- database table names;
- internal directory algorithms;
- private class structure;
- incidental serialization layout;
- other implementation details not governed by the acceptance contract.

Internal details may be asserted where that internal semantic is itself part of the governed requirement.

The Development Slice Specification records:

- the acceptance test suite/location;
- the acceptance proposition IDs covered;
- any durable verification-specific notes needed to understand coverage.

It does not become a CI build-history log.

---

# 15. Supporting verification

Lower-level tests support implementation confidence and failure diagnosis.

Depending on the slice, supporting verification may include:

- domain/unit tests;
- integration tests;
- persistence round-trip tests;
- filesystem/storage tests;
- analyzer tests;
- component tests;
- determinism tests;
- replay/resume tests;
- failure-path tests;
- other focused verification.

Supporting tests answer questions such as:

> Which component or semantic boundary failed?

Slice-level acceptance verification answers:

> Does this implementation satisfy the observable canonical behavior for the slice?

Supporting tests do not substitute for acceptance verification.

Testing technology is owned by `13-tooling-and-dependencies.md`, not this document.

---

# 16. Development Slice Specification lifecycle

The normal lifecycle is:

```text
Draft
  ↓
Ready for Implementation
  ↓
In Progress
  ↓
Verification Pending
  ↓
Implemented
```

`Withdrawn` and `Superseded` are terminal historical outcomes.

## 16.1 Legal transitions

The normal legal transitions are:

```text
Draft
 ├──→ Ready for Implementation
 └──→ Withdrawn

Ready for Implementation
 ├──→ In Progress
 ├──→ Draft
 └──→ Withdrawn

In Progress
 ├──→ Verification Pending
 └──→ Withdrawn

Verification Pending
 ├──→ Implemented
 ├──→ In Progress
 └──→ Withdrawn

Implemented
 └──→ Superseded
```

`Withdrawn` and `Superseded` are terminal.

Blocking does not change lifecycle state.

---

# 17. Lifecycle state meaning

## 17.1 Draft

The specification exists but is not yet an implementation baseline.

During `Draft`:

- canonical basis is identified;
- scope is bounded;
- acceptance propositions are developed;
- representative scenarios are designed;
- verification approach is planned;
- material questions may still be raised.

The specification is freely refinable.

## 17.2 Ready for Implementation

The specification is sufficiently defined for production implementation to proceed.

Before entering `Ready for Implementation`:

- canonical basis must be identified;
- included/excluded scope must be explicit;
- no material unresolved design question may be silently assumed;
- the acceptance contract must be defined;
- representative scenarios must be sufficient to begin;
- executable verification approach must be identified.

Entering this state establishes the acceptance contract as the implementation baseline.

## 17.3 In Progress

Production implementation is actively proceeding against the accepted baseline.

During this state:

- executable acceptance verification is implemented/refined;
- production code is implemented;
- supporting verification is added;
- discovered qualifications are recorded;
- actual verification locations replace planned placeholders.

## 17.4 Verification Pending

The slice implementation is believed complete, but required acceptance verification has not yet been fully satisfied.

A failure at this state normally returns the specification to `In Progress`.

## 17.5 Implemented

All completion criteria defined by this process are satisfied.

The specification becomes a durable, historically stable implementation/verification record.

## 17.6 Withdrawn

The slice will not proceed under this specification.

A meaningful withdrawn specification is retained with a concise reason rather than deleted.

## 17.7 Superseded

A later Development Slice Specification materially replaces an implemented specification.

The earlier specification is retained and linked to the replacement.

---

# 18. Acceptance baseline and mutability

Before `Ready for Implementation`, the specification may be materially refined.

Entering `Ready for Implementation` creates an implementation baseline.

After that point:

- non-semantic clarifications may be made;
- test paths and supporting-verification references may be filled in;
- implementation discoveries may be recorded;
- lifecycle and verification status may change;

but the acceptance contract must not be weakened or substantively rewritten merely to make implementation pass.

If a substantive specification problem is identified before implementation begins, the specification returns:

```text
Ready for Implementation
        ↓
Draft
```

If a substantive issue is discovered during implementation and it reflects a material canonical ambiguity, conflict, unsupported assumption, boundary question, or challenge to accepted direction, it must be registered through `91`.

The specification may record that qualification in `Blocked by` where it prevents correct progress.

If governed resolution legitimately changes canonical semantics or slice obligations, the Development Slice Specification is then updated to match the governed result.

---

# 19. Implementation-discovered qualifications

The `Qualifications discovered` section records references to material issues registered in `91-design-qualification-register.md`.

It is not an independent unresolved-issue register.

The process is:

```text
implementation exposes material question
        ↓
register qualification in 91
        ↓
record qualification reference in DEV-SPEC
        ↓
if necessary:
Blocked by: <qualification>
        ↓
resolve through normative governance
        ↓
update canonical owner(s) where required
        ↓
update DEV-SPEC consistently
        ↓
resume implementation
```

Code, tests, and Development Slice Specifications must not silently resolve material design questions merely because an implementation choice is convenient.

---

# 20. Verification status

The `Verification status` section records the durable state of the specification's required verification.

It should state whether:

- acceptance verification is pending;
- required acceptance propositions are satisfied;
- any required supporting verification remains outstanding;
- verification is blocked by a referenced qualification or implementation defect.

It should not accumulate individual CI/build-run history.

CI/test infrastructure owns transient run history.

The Development Slice Specification owns the durable definition of what must be verified and where that verification lives.

---

# 21. Lightweight lifecycle history

Every Development Slice Specification maintains a lightweight lifecycle history.

The required fields are:

```text
Date | Transition | Reason
```

Example:

```text
| Date | Transition | Reason |
|---|---|---|
| 2026-09-27 | Created → Draft | Initial specification |
| 2026-09-28 | Draft → Ready for Implementation | Acceptance contract established |
| 2026-09-29 | Ready for Implementation → In Progress | Implementation commenced |
| 2026-10-02 | In Progress → Verification Pending | Implementation believed complete |
| 2026-10-02 | Verification Pending → In Progress | Acceptance verification exposed replay defect |
| 2026-10-03 | In Progress → Verification Pending | Defect corrected |
| 2026-10-03 | Verification Pending → Implemented | Acceptance contract satisfied |
```

This is not a general change log.

Do not record ordinary events such as:

- typo fixes;
- class renames;
- refactoring steps;
- individual commits;
- every test addition.

Source control already records ordinary edit history.

Lifecycle history records **when and why the specification changed lifecycle phase**.

---

# 22. Definition of Done

A Development Slice Specification may transition from `Verification Pending` to `Implemented` only when all applicable completion conditions are satisfied.

At minimum:

1. the canonical basis remains valid and coherent;
2. included/excluded scope remains explicit and consistent with canonical boundaries;
3. every acceptance proposition is satisfied by the required repeatable verification;
4. acceptance verification is automated where reasonably practicable;
5. required supporting tests pass;
6. no unresolved blocking qualification remains;
7. implementation respects applicable architecture, tooling, evidence, MVP, and other canonical boundaries;
8. actual executable-verification locations are recorded;
9. required supporting-verification locations are recorded;
10. verification status is updated to reflect completion;
11. lifecycle history records the transition;
12. any canonical/governance change exposed by the slice has completed its own required synchronization lifecycle.

Code completion alone does not satisfy Definition of Done.

---

# 23. Historical stability, withdrawal, and supersession

An `Implemented` Development Slice Specification is historically stable.

Permitted maintenance includes non-semantic corrections such as:

- typo correction;
- broken-link repair;
- renamed test-file/path reference where meaning is unchanged.

Do not silently rewrite an implemented specification to make later implementation history appear as though it was always part of the original slice.

Where later work materially replaces an implemented slice:

1. create a new Development Slice Specification;
2. retain the original specification;
3. set the new specification's `Supersedes` field;
4. set the old specification's `Superseded by` field;
5. transition the old specification to `Superseded`.

Where a specification is abandoned before implementation completion, transition it to `Withdrawn` and record a concise reason.

Meaningful withdrawn/superseded specifications remain part of project history.

---

# 24. Lightweight-use rule

The process exists to improve:

- scope clarity;
- acceptance quality;
- requirements traceability;
- qualification discovery;
- verification design;
- diagnostic quality;
- completion confidence;
- durable implementation history.

It must not become a substitute for:

- source control;
- issue tracking;
- CI run history;
- canonical design documents;
- `90`/`91` governance;
- ordinary code comments.

One concise Development Slice Specification should normally cover one coherent vertical slice.

The process should be rigorous where semantics matter and deliberately lightweight where they do not.

---

# 25. Standard Development Slice Specification template

A new Development Slice Specification should normally begin from this structure:

```text
# DEV-SPEC-xxx — <Title>

Status: Draft
Blocked by: None
Supersedes: None
Superseded by: None

## Purpose

<What bounded implementation increment is being delivered?>

## Canonical basis

- <canonical document/section>
- <decision IDs>

## Scope

### Included

- ...

### Excluded

- ...

## Acceptance contract

### AC-<AREA>-001 — <Short name>

<Observable proposition.>

## Representative scenarios

### Scenario S1 — <Short name>

<Controlled setup and expected acceptance propositions.>

Verifies:
- AC-<AREA>-001

## Executable verification

Planned:
- <test project/path/suite>

## Supporting verification

- <domain/unit verification>
- <integration verification>
- <persistence/filesystem/etc.>

## Qualifications discovered

None.

## Verification status

Pending.

## Lifecycle history

| Date | Transition | Reason |
|---|---|---|
| <date> | Created → Draft | Initial specification |
```

The template may be extended where a slice genuinely requires additional material.

---

# 26. First intended application

The first intended instance of this process is:

```text
DEV-SPEC-001 — Workspace + Local Artifact Intake
```

That specification is intended to apply the already accepted Recovery Workspace and artifact-intake/preservation requirements to the first meaningful MVP implementation slice.

Its purpose is to operationalize existing canonical requirements, not create new Recovery Input, preservation, Artifact identity, workspace, or replay semantics.

The specification should be created only after this development-process governance cycle has completed.

---

# 27. Relationship to MVP development

This process does not expand the MVP.

When a Development Slice implements MVP work:

- `14-mvp.md` remains authoritative for what is required in the MVP;
- this document defines how that requirement is converted into a verified implementation slice;
- a DEV-SPEC may narrow implementation work into a smaller vertical increment but cannot remove an MVP obligation;
- roadmap capabilities are not promoted into a slice merely because they are technically adjacent.

A material scope question returns to governance rather than being decided in a Development Slice Specification.

---

# 28. Relationship to testing/tooling

This document defines **verification responsibility and development discipline**, not test technology.

`13-tooling-and-dependencies.md` remains authoritative for:

- baseline automated test framework;
- testing dependencies;
- related tooling/version choices.

This document remains valid if the selected test technology later changes through normal governance.

---

# 29. Relationship to design governance

This development process is subordinate to the normative governance lifecycle in `91-design-qualification-register.md`.

A Development Slice Specification may expose a material qualification, but it cannot resolve one independently.

The required boundary is:

```text
design question
    ↓
91 qualification lifecycle
    ↓
90 decision if durable direction is established
    ↓
canonical owner updated
    ↓
DEV-SPEC applies the accepted result
```

This prevents implementation pressure from becoming an implicit design-authority mechanism.

---

# 30. Governance mapping

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| DEV-001 | D168 | Material implementation uses the canonical development process and durable Development Slice Specifications |

DEV-001 is Resolved. The required canonical-document audit and cross-file governance synchronization have completed.

---

# 31. Canonical ownership

This document owns:

- Development Slice definition;
- Development Slice Specification purpose/status;
- DEV-SPEC identity and repository convention;
- required DEV-SPEC metadata and structure;
- acceptance-contract/scenario relationship;
- executable versus supporting verification relationship;
- DEV-SPEC lifecycle and transition rules;
- acceptance baselining/mutability rules;
- implementation-discovered qualification escalation;
- lifecycle-history requirement;
- Development Slice Definition of Done;
- DEV-SPEC historical stability, withdrawal, and supersession.

This document does not own:

- qualification governance;
- durable decision semantics;
- evidence/recovery semantics;
- recovery-stage semantics;
- architecture;
- tooling/dependency selection;
- MVP scope;
- roadmap scope;
- validation methodology;
- recovery metrics.

Those remain with their existing canonical owners.

---

# 32. Closing principle

LegacyRevive.NET development should preserve a visible chain from accepted design to demonstrably conforming implementation:

```text
canonical meaning
        ↓
explicit slice scope
        ↓
observable acceptance
        ↓
executable proof
        ↓
implementation
        ↓
durable completion history
```

The process should make it difficult for implementation convenience, exploratory chat, or incidental test structure to silently redefine what the product was canonically intended to do.
