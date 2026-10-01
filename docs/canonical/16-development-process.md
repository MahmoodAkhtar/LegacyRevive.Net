# LegacyRevive.NET — Development Process

## Status

**Canonical**

DEV-001 through DEV-004 have completed the governed lifecycle and are Resolved. D168 establishes the canonical development-process framework, D173 establishes the semantic-readiness gate for implementation delegation, D174 establishes history-preserving rejection of an unsound Implemented completion judgment, and D176 establishes boundary/falsification verification for material recognition/classification slices.

This document is governed by:

- DEV-001 — Canonical Development Process and Development Slice Specifications;
- DEV-002 — Semantic Readiness of a Development Slice;
- DEV-003 — Post-Implemented Rejection of a Development Slice;
- DEV-004 — Recognition Boundary and Falsification Verification;
- D168 — Material Implementation Uses Canonical Development Process and Development Slice Specifications;
- D173 — Development Slices Require Semantic Readiness Before Implementation;
- D174 — Rejected DEV-SPECs Preserve Unsound Implemented Completion Judgments;
- D176 — Recognition/Classifiers Require Boundary-Falsification Verification.

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
- `Superseded`;
- `Rejected`.

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

They remain distinct from blocking and ordinary lifecycle progression. A later replacement may link to a `Rejected` specification for historical lineage, but that does not change the rejected specification's current lifecycle status to `Superseded`; rejection records that its own completion judgment was not accepted as valid.

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

## Readiness assessment

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

# 12A. Semantic readiness

A Development Slice Specification may enter `Ready for Implementation` only when its acceptance-bearing obligations are **semantically determinate enough to delegate to implementation**.

Semantic readiness means the applicable canonical basis, together with the bounded DEV-SPEC, determines the materially significant meaning of the behavior being implemented. The implementer may still choose implementation mechanics, but must not have to invent product/design semantics in order to decide what counts as a correct result.

The readiness review applies the following **material semantic choice test**:

> **Could two implementations make materially different semantic choices and both plausibly claim that the current canonical basis and acceptance wording permit them?**

If the answer is yes because the authoritative semantics do not determine the choice, the slice is not semantically ready. The missing semantic question must be routed through `91-design-qualification-register.md` and resolved by the appropriate canonical owner before the slice proceeds.

Material semantic choices include, where relevant to the slice:

- truth conditions for a classification, state, or accepted outcome;
- evidence or recognition thresholds;
- state meanings and transitions;
- failure-category meanings and boundaries;
- domain invariants;
- precedence, overlap, ambiguity, or conflict rules;
- persistent/history semantics;
- provenance or epistemic categorization;
- externally or developer-visible behavior whose alternatives would change the accepted meaning of the feature;
- another choice that would materially change what the product claims, preserves, reports, accepts, rejects, or treats as `Unknown`/unresolved.

The existence of multiple possible implementations does **not** by itself mean semantic unreadiness. Implementation discretion remains legitimate when the alternatives preserve the same accepted semantics and differ only in mechanics such as:

- private type/helper decomposition;
- internal naming;
- parser or adapter composition;
- buffering versus streaming where no accepted behavior changes;
- local algorithms/data structures where canonical outcomes remain equivalent;
- incidental persistence/schema mechanics not themselves governed;
- other private implementation choices that do not change externally, persistently, architecturally, or epistemically meaningful behavior.

Executable acceptance tests cannot repair an underdetermined canonical requirement by choosing one interpretation and making it executable. Tests demonstrate accepted semantics; they do not supply missing canonical semantics.

## 12A.1 Readiness assessment record

Every material DEV-SPEC must contain a concise `Readiness assessment` section before entering `Ready for Implementation`. It records:

- the canonical semantic basis relied upon for the acceptance contract;
- any material semantic questions identified and their qualification IDs;
- the material implementation discretion intentionally left open;
- the semantic-readiness result: `Pending`, `Blocked`, or `Pass`.

`Pass` may be recorded only when no unresolved material semantic choice must be originated by implementation. A generic statement such as "canonical basis reviewed" is insufficient when the slice depends on material truth conditions, thresholds, state/failure meanings, precedence, ambiguity, or comparable semantics.

Historical `Implemented` DEV-SPECs are not rewritten merely to add this later process section. New specifications, replacement specifications, and existing specifications that have not yet validly entered implementation under the current process must use the current readiness rule.

**Governance:** DEV-002 / D173.

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

## 13.1 Recognition/classification boundary-verification matrix

Where a Development Slice makes material recognition, classification, evidence-threshold, parser-sensitive, or comparable accept/reject claims, representative examples alone are not sufficient. Before the slice may enter `Ready for Implementation`, the DEV-SPEC must contain an explicit **boundary/falsification verification matrix** derived from the already-governed canonical semantics.

The matrix exists to demonstrate both sides of each material recognition predicate:

- what minimum evidence is sufficient; and
- what materially weaker, misleading, malformed, overlapping, or operational condition must remain insufficient or map to a different governed outcome.

For each material recognition predicate, the DEV-SPEC must consider the following categories where applicable:

1. positive / minimum sufficient evidence;
2. each materially independent required condition absent or invalid where that omission can change the outcome;
3. lookalike or misleading content;
4. malformed/corrupt-candidate threshold;
5. valid other or broader format;
6. incidental token/name/prefix collisions or equivalent weak signals;
7. container/subtype boundary;
8. overlap or ambiguity with another independently supportable classification;
9. parser/tool/resource/cancellation failure versus deterministic content invalidity;
10. recognizer-order or equivalent execution-order independence.

At least one **adversarial near-match** is required for every positive material recognition predicate. The near-match must preserve enough superficial similarity to exercise the risk of false recognition while deliberately lacking or violating a canonically required condition. Passing the positive case without such a falsification case is not sufficient completion evidence.

The matrix should normally record, for each row:

- canonical requirement / acceptance proposition;
- boundary category;
- controlled input condition;
- expected governed outcome;
- representative scenario or executable-test reference;
- `Not applicable` rationale where a listed category genuinely does not apply.

This is a **verification discipline, not a source of semantics**. Every expected outcome in the matrix must already be determined by the applicable canonical basis. If the expected outcome cannot be stated without choosing a material semantic interpretation, semantic readiness fails under §12A and the missing question must be governed before implementation. Tests must not make the choice.

The matrix is risk-based rather than combinatorially exhaustive. It does not require enumeration of every possible malformed byte sequence or every permutation of conditions. It requires enough independent boundary cases to demonstrate that each material predicate is not accidentally satisfied by materially weaker evidence or parser/tool convenience.

**Governance:** DEV-004 / D176.

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
- any durable verification-specific notes needed to understand coverage;
- for applicable recognition/classification slices, the executable mapping from the §13.1 boundary/falsification matrix to acceptance verification.

For a slice subject to §13.1, executable verification must exercise the material matrix rows. Supporting/unit tests may provide focused component diagnosis, but they do not replace observable slice-level verification of the governed recognition boundaries. A test that merely reproduces a parser/library default without demonstrating the LegacyRevive.NET governed outcome does not satisfy the boundary-verification obligation.

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

`Withdrawn`, `Superseded`, and `Rejected` are terminal historical outcomes.

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
 ├──→ Superseded
 └──→ Rejected
```

`Withdrawn`, `Superseded`, and `Rejected` are terminal.

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
- the semantic-readiness review in §12A must be `Pass`;
- the `Readiness assessment` must identify the canonical semantic basis, any qualification blockers, and the material implementation discretion intentionally left open;
- representative scenarios must be sufficient to begin;
- executable verification approach must be identified.

Entering this state establishes the acceptance contract as the implementation baseline. A DEV-SPEC is not ready merely because its wording is testable if the tests would themselves have to choose unresolved product semantics.

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

All applicable canonical and slice-specific completion criteria are judged satisfied.

The specification becomes the current accepted implementation/verification record for that slice. Its lifecycle history is durable: a later finding that the completion judgment itself was materially unsound does not erase the `Implemented` transition, but may move the current status to `Rejected` under §17.8.

## 17.6 Withdrawn

The slice will not proceed under this specification.

A meaningful withdrawn specification is retained with a concise reason rather than deleted.

## 17.7 Superseded

A later Development Slice Specification materially replaces a valid implemented specification.

The earlier specification is retained and linked to the replacement.

## 17.8 Rejected

A specification is `Rejected` when it previously reached `Implemented`, but a later deliberate review establishes that the completion/acceptance judgment itself was materially unsound and the implementation is no longer accepted as a valid completed slice.

`Rejected` is not a synonym for failed verification or ordinary abandonment. It preserves the historical `Implemented` transition while making the current disposition explicit. The rejection reason and governing qualification/decision must be recorded in lifecycle history.

---

# 18. Acceptance baseline and mutability

Before `Ready for Implementation`, the specification may be materially refined.

Entering `Ready for Implementation` requires semantic-readiness `Pass` under §12A and creates an implementation baseline. If the material semantic choice test fails, the specification remains `Draft`, the missing design question is registered in `91`, and the canonical owner resolves it before readiness is reconsidered.

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

For a slice subject to §13.1, `Verification status` must also state whether the required boundary/falsification matrix is fully exercised and whether any matrix row remains pending, skipped, blocked, or intentionally `Not applicable`.

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

A Development Slice Specification may transition from `Verification Pending` to `Implemented` only when all applicable completion conditions are satisfied. "Applicable" includes both this canonical Definition of Done and any additional slice-specific completion conditions explicitly stated by the DEV-SPEC and consistent with canonical process.

At minimum:

1. the canonical basis remains valid and coherent;
2. included/excluded scope remains explicit and consistent with canonical boundaries;
3. the semantic-readiness basis remains valid: implementation has not supplied a material product/design semantic that should have been canonically established, and any such issue discovered after readiness has completed governance before completion;
4. every acceptance proposition is satisfied by the required repeatable verification;
5. acceptance verification is automated where reasonably practicable;
6. required supporting tests pass;
7. no unresolved blocking qualification remains;
8. implementation respects applicable architecture, tooling, evidence, MVP, and other canonical boundaries;
9. actual executable-verification locations are recorded;
10. required supporting-verification locations are recorded;
11. verification status is updated to reflect completion;
12. lifecycle history records the transition;
13. any canonical/governance change exposed by the slice has completed its own required synchronization lifecycle.

For any slice subject to §13.1, the final Definition-of-Done audit must additionally confirm that:

- the boundary/falsification matrix remains derived from current canonical semantics;
- every positive material recognition predicate has at least one executable adversarial near-match;
- applicable missing-condition, lookalike, malformed-candidate, broader/other-format, collision, container/subtype, overlap, operational-failure, and order-independence boundaries are exercised or have an explicit justified `Not applicable` entry;
- expected outcomes were not supplied by parser/library behavior or implementation convenience;
- no acceptance-bearing matrix row required for completion is silently skipped; and
- any boundary that exposed underdetermined semantics was routed through governance before completion.

Aggregate green test counts do not by themselves establish completion for a slice subject to §13.1.

Code completion alone does not satisfy Definition of Done. Source-control commit is not a universal canonical lifecycle condition unless an applicable repository rule or the DEV-SPEC explicitly makes it a slice-specific completion condition. Where a DEV-SPEC does state such an additional completion condition, it is binding for that slice.

---

# 23. Historical stability, withdrawal, supersession, and rejection

An `Implemented` Development Slice Specification has durable lifecycle history and is the current accepted implementation/verification record unless later superseded or rejected through the legal lifecycle.

Permitted maintenance includes non-semantic corrections such as:

- typo correction;
- broken-link repair;
- renamed test-file/path reference where meaning is unchanged.

Do not silently rewrite an implemented specification to make later implementation history appear as though it was always part of the original slice.

Where later work materially replaces a valid implemented slice:

1. create a new Development Slice Specification;
2. retain the original specification;
3. set the new specification's `Supersedes` field;
4. set the old specification's `Superseded by` field;
5. transition the old specification to `Superseded`.

Where a specification is abandoned before implementation completion, transition it to `Withdrawn` and record a concise reason.

Where a specification has already reached `Implemented` but later governed review establishes that the completion/acceptance basis itself was materially unsound and the implementation is no longer accepted, transition it to `Rejected`. Append the rejection transition and reason; do not rewrite away the earlier `Implemented` event. This is distinct from ordinary failed verification, withdrawal, and supersession.

Source-control history remains separate from lifecycle semantics. A missing commit is not by itself a universal rejection reason, but failure to satisfy an explicit slice-specific completion condition—including a commit condition where one was deliberately stated—may form part of the basis for rejecting an unsound completion judgment.

A future replacement for a `Rejected` specification uses a new DEV-SPEC. It may use `Supersedes` / `Superseded by` to preserve replacement lineage, but the rejected specification remains `Rejected` because that status records the disposition of its own completion judgment.

Meaningful withdrawn, superseded, and rejected specifications remain part of project history.

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

## Readiness assessment

Canonical semantic basis:
- <canonical sections/decisions that determine material behavior>

Material semantic questions:
- None, or <qualification IDs>

Implementation discretion intentionally left open:
- <private/mechanical choices that do not change accepted semantics>

Semantic readiness: Pending | Blocked | Pass

## Representative scenarios

### Scenario S1 — <Short name>

<Controlled setup and expected acceptance propositions.>

Verifies:
- AC-<AREA>-001

## Boundary/falsification verification

Required when material recognition/classification/evidence-threshold semantics are in scope; otherwise state `Not applicable` with a concise rationale.

| Acceptance / canonical predicate | Boundary category | Controlled condition | Expected governed outcome | Scenario / executable verification |
|---|---|---|---|---|
| AC-<AREA>-001 | Positive / minimum sufficient | ... | ... | ... |
| AC-<AREA>-001 | Adversarial near-match | ... | ... | ... |

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

The template may be extended where a slice genuinely requires additional material. If a specification transitions to `Rejected`, add a concise `Rejection disposition` section recording the governing qualification/decision, why the prior completion judgment is no longer accepted, the disposition of the implementation/repository state, and any future replacement relationship. Historical verification and lifecycle entries remain in place.

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
| DEV-002 | D173 | A material DEV-SPEC must pass an explicit semantic-readiness gate before implementation may begin; unresolved material semantic choices return to governance rather than being delegated to implementation |
| DEV-003 | D174 | A later finding that an `Implemented` completion judgment was materially unsound is preserved by `Implemented → Rejected`; failed verification, withdrawal, supersession, and source-control history remain distinct |
| DEV-004 | D176 | Material recognition/classification slices require a canonically grounded boundary/falsification matrix, adversarial near-matches, and Definition-of-Done matrix-to-test coverage |

DEV-001 through DEV-004 are Resolved. The required canonical-document audits and cross-file governance synchronization have completed.

---

# 31. Canonical ownership

This document owns:

- Development Slice definition;
- Development Slice Specification purpose/status;
- DEV-SPEC identity and repository convention;
- required DEV-SPEC metadata and structure;
- semantic-readiness criteria and readiness-assessment discipline;
- acceptance-contract/scenario relationship;
- recognition/classification boundary-falsification verification discipline;
- executable versus supporting verification relationship;
- DEV-SPEC lifecycle and transition rules;
- acceptance baselining/mutability rules;
- implementation-discovered qualification escalation;
- lifecycle-history requirement;
- Development Slice Definition of Done;
- DEV-SPEC historical stability, withdrawal, supersession, and rejection.

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
