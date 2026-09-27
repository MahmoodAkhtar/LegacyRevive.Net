# LegacyRevive.NET — Developer Intervention

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET model for deliberate developer participation in recovery.

It builds on the Evidence Model in `03-evidence-model.md`.

In particular:

- Developer Input is first-class Recovery Input but is not formal Evidence;
- Developer Corrections supersede rather than rewrite history;
- materially dependent downstream items may become `Needs Reevaluation` or `Invalidated`;
- provenance must distinguish developer-supplied information from Original-System Evidence.

This document owns the workflow semantics of developer intervention without redefining those evidence semantics.

---

# 1. Purpose

LegacyRevive.NET is a recovery assistant, not an autonomous oracle.

Incomplete artifacts, ambiguity, conflicting information, and irrecoverable historical detail mean that a developer may sometimes need to help recovery progress.

Developer intervention exists so that human knowledge and engineering judgement can be incorporated deliberately without being confused with facts recovered from the original system.

The central rule is:

> **Developer intervention may influence recovery, but it must never silently become original-system evidence or erase the history it changes.**

---

# 2. What counts as Developer Intervention

A **Developer Intervention** is a deliberate human action that changes, supplements, constrains, or directs the recovery process.

The canonical intervention kinds are:

1. **Developer Assertion**
2. **Developer Decision**
3. **Developer Correction**

These terms are defined in `02-terminology.md`.

This document defines how they are used.

---

# 3. Developer Assertion

A **Developer Assertion** supplies a proposition for the recovery process to consider.

Examples may include statements such as:

- a particular deployed assembly belonged to a known subsystem;
- a missing service was historically hosted separately;
- a namespace convention was intentionally used in one area.

A Developer Assertion:

- is Developer Input, not formal Evidence;
- must retain attribution and provenance;
- may support an Inference;
- may help resolve an otherwise Unresolved recovery question;
- cannot by itself create a Recovered Fact;
- must remain distinguishable from observations derived from original artifacts.

An accepted Developer Assertion means:

> **LegacyRevive.NET is permitted to rely on this developer-supplied proposition for recovery.**

It does **not** mean:

> **LegacyRevive.NET independently recovered this proposition from the original system.**

---

# 4. Developer Decision

A **Developer Decision** selects how recovery should proceed when the available material does not uniquely determine one path.

Examples may include:

- choose one project boundary among multiple plausible alternatives;
- select one reconstruction approach;
- continue using an explicit Assumption;
- choose which unresolved dependency to address first.

A Developer Decision is operational.

It answers:

> **What recovery path should we use?**

It does not necessarily answer:

> **What historically existed?**

A Developer Decision therefore may resolve recovery ambiguity without converting the chosen alternative into a Recovered Fact.

---

# 5. Developer Correction

A **Developer Correction** deliberately changes an earlier recovery conclusion, reconstruction decision, Developer Assertion, or Developer Decision.

The evidence-history semantics are fixed by `03-evidence-model.md` and D029:

1. the correction is recorded as new Developer Input;
2. the earlier item remains in history;
3. the corrected item supersedes the earlier item;
4. materially dependent downstream items become `Needs Reevaluation`;
5. an item becomes `Invalidated` where its valid support has been removed.

This document adds the workflow rule:

> **A correction must identify what it corrects.**

A free-standing replacement that does not link to the superseded item is not sufficient for a traceable correction.

---

# 6. Intervention record

Every committed Developer Intervention must be represented as an **Intervention Record**.

An Intervention Record must identify, at minimum:

- intervention identity;
- intervention kind;
- the developer-supplied content or choice;
- scope;
- status;
- origin/attribution sufficient for provenance;
- the recovery item or question it affects, where applicable;
- superseded/superseding relationship, where applicable;
- rationale or note where supplied;
- creation order or equivalent sequencing information.

This is a conceptual contract.

This document does not prescribe a database schema or C# type.

---

# 7. Intervention scope

Every intervention must have an explicit scope.

Scope defines where the intervention is permitted to influence recovery.

The model should support scopes such as:

- recovery-wide;
- project or candidate-project;
- assembly/artifact;
- namespace/type/member;
- configuration area;
- specific recovery question or conclusion.

Exact scope types may be refined by the owning canonical domain documents as concrete recovery entities are defined.

The governing rule is:

> **An intervention must not influence recovery outside its declared scope merely because it is available globally to the tool.**

When scope cannot yet be represented precisely, the intervention should remain narrower rather than being silently generalized.

---

# 8. Intervention lifecycle

A committed Intervention Record has one of these canonical states:

- **Active**
- **Superseded**
- **Withdrawn**

## 8.1 Active

The intervention is current and may influence recovery within its scope.

## 8.2 Superseded

A later intervention explicitly replaces it.

The earlier record remains historically visible but no longer governs current recovery state.

## 8.3 Withdrawn

The developer deliberately removes the intervention from current use without replacing it with another intervention.

Withdrawal does not erase history.

If downstream recovery materially depended on the intervention, those items become `Needs Reevaluation` until recomputed or deliberately reaffirmed.

Where valid support no longer remains, the affected item becomes `Invalidated`.

---

# 9. Draft interaction state is not canonical recovery state

A UI or workflow may eventually allow a developer to compose, preview, or edit an intervention before committing it.

Such transient editing states are implementation concerns.

They are not part of the canonical recovery model until the intervention is committed as an Intervention Record.

This distinction prevents partially entered or abandoned human input from affecting deterministic recovery.

---

# 10. Applying an intervention

An Active intervention may influence recovery only through an explicit dependency relationship.

Examples:

```text
Developer Assertion
    ↓
Inference

Developer Decision
    ↓
Candidate Reconstruction choice

Developer Correction
    ↓
superseding conclusion
```

The intervention must appear in the provenance/derivation path of any material conclusion that relies on it.

An intervention must not operate as hidden global context.

---

# 11. Precedence and conflicts

Developer intervention does not automatically outrank Evidence.

If a Developer Assertion conflicts with Original-System Evidence:

- preserve the Evidence;
- preserve the Developer Assertion;
- represent the incompatibility explicitly;
- do not silently rewrite the Evidence;
- allow the developer to make an operational Developer Decision if recovery must proceed.

If the developer states that an earlier artifact-derived conclusion is wrong, that statement is still Developer Input.

It may trigger a Developer Correction and superseding recovery conclusion, but the original Observation/Evidence remains preserved.

This keeps the distinction between:

- historical evidence;
- current recovery choice;
- developer knowledge.

---

# 12. Intervention and Recovered Facts

Developer Input cannot by itself create a Recovered Fact.

A proposition may become a Recovered Fact only when admissible Evidence directly entails it under the rules in `03-evidence-model.md`.

Therefore:

```text
Developer Assertion
    ↓
may support
Inference
```

but not:

```text
Developer Assertion
    ↓
Recovered Fact
```

If a Developer Assertion later leads the developer to discover independent admissible Evidence, that Evidence may support a new Recovered Fact through the normal Evidence Model.

---

# 13. Intervention and Assumptions

A Developer Decision may authorize recovery to proceed using an Active Assumption.

The decision and the Assumption remain separate records.

This distinction matters because:

- the Developer Decision records the human choice to proceed;
- the Assumption records the unproven proposition being relied upon.

Withdrawing the decision to rely on an Assumption does not prove the Assumption false.

Invalidating an Assumption requires the Assumption lifecycle defined in `03-evidence-model.md`.

---

# 14. Persistence

Committed Intervention Records must be persisted as part of the recoverable recovery state.

They must not exist only in transient chat context, process memory, or an unrecorded developer action.

Persistence is required because interventions may materially influence later recovery results.

A future recovery run using the same:

- original recovery inputs;
- LegacyRevive.NET version;
- configuration;
- committed intervention set;

should be able to reproduce the same substantive intervention-influenced recovery result wherever practical.

This supports D014 — deterministic recovery.

---

# 15. Replay

Committed interventions must be replayable.

Replay means that a later run can reapply the recorded interventions according to:

- their identity;
- scope;
- current status;
- supersession relationships;
- sequencing where order matters.

Replay must not convert Developer Input into Evidence.

The same provenance classification must survive across runs.

---

# 16. Importing and carrying intervention history

Where a recovery workspace or recovery state is reopened, its committed intervention history should remain available.

Only Active interventions affect current recovery.

Superseded and Withdrawn interventions remain available for provenance and historical explanation.

A copied or imported intervention history must preserve identities and relationships sufficiently to avoid making the imported records appear to be newly discovered Evidence.

The exact storage/export format belongs to the architecture/tooling/reporting implementation work governed by `12-architecture.md`, `13-tooling-and-dependencies.md`, and applicable MVP requirements.

---

# 17. Determinism and intervention ordering

Where two interventions affect the same recovery question, the result must not depend on undocumented incidental execution order.

The recovery state must instead make the relationship explicit.

Permitted cases include:

- one intervention supersedes another;
- interventions operate on non-conflicting scopes;
- the combination is explicitly valid.

If two simultaneously Active interventions give incompatible directions for the same scope and neither supersedes the other, the state is a **Developer Intervention Conflict** and recovery must not silently choose one.

---

# 18. Developer Intervention Conflict

A **Developer Intervention Conflict** exists when two or more Active interventions provide incompatible instructions or propositions for overlapping scope.

The conflict must:

- identify the competing interventions;
- remain visible;
- prevent silent precedence;
- be resolved by supersession, withdrawal, scope correction, or another explicit Developer Decision.

A developer-intervention conflict is distinct from conflicting Evidence, though the two may coexist.

---

# 19. Review before commitment

LegacyRevive.NET should make a material intervention inspectable before it becomes committed recovery state.

At minimum, the developer should be able to understand:

- intervention kind;
- scope;
- what recovery item/question it affects;
- the effect it is expected to have;
- whether it supersedes an earlier intervention.

This is a workflow requirement, not a UI specification.

The exact presentation belongs to later implementation/UI design.

---

# 20. Explainability

When an intervention materially influences a recovery conclusion, a developer reviewing that conclusion should be able to determine:

- that developer input influenced it;
- which intervention influenced it;
- what kind of intervention it was;
- whether it remains Active;
- what evidence and assumptions also contributed.

LegacyRevive.NET must not present intervention-influenced conclusions as though they were purely artifact-derived.

---

# 21. Withdrawal

Withdrawal is a deliberate developer action.

A Withdrawn intervention:

- remains in history;
- no longer influences new/current recovery;
- triggers reevaluation of materially dependent downstream items;
- does not imply the intervention was historically false.

Withdrawal is therefore different from correction.

A correction supplies a replacement/superseding intervention or conclusion.

Withdrawal removes current reliance without necessarily supplying a replacement.

---

# 22. Reaffirmation

When an upstream intervention is superseded or withdrawn, downstream items become `Needs Reevaluation`.

A downstream item may return to Current only when it is:

- recomputed from valid current inputs; or
- deliberately reaffirmed because its conclusion remains justified independently.

Reaffirmation must itself be traceable.

It must not simply clear the `Needs Reevaluation` marker without an explanation of why the item remains valid.

---

# 23. Intervention safety rules

Developer intervention must obey these safety rules:

1. **Never become Original-System Evidence.**
2. **Never erase the item it supersedes or corrects.**
3. **Never influence recovery outside declared scope.**
4. **Never operate as hidden context.**
5. **Never silently override conflicting Evidence.**
6. **Never bypass circular-support protections.**
7. **Never depend on unrecorded execution order where interventions conflict.**
8. **Never be lost if it materially affected a persisted recovery result.**

---

# 24. Relationship to convention inference

Convention observations from `05-convention-inference.md` may help a developer make an intervention.

For example, a developer may use a recurring recovered naming pattern to choose a reconstruction convention.

However:

```text
Convention Observation
    ↓
informs
Developer Decision
    ↓
influences
Reconstruction
```

The resulting reconstruction must not later be used as independent evidence that the convention historically applied.

D006 and D030 continue to govern this relationship.

---

# 25. Relationship to recovery process

`07-recovery-process.md` will define where interventions may occur in the end-to-end recovery flow.

This document establishes the invariant semantics that process design must respect:

- intervention is explicit;
- committed intervention is persisted;
- scope is explicit;
- provenance is preserved;
- current state is replayable;
- correction/withdrawal trigger downstream reevaluation.

The recovery process may define multiple intervention opportunities without changing these rules.

---

# 26. Relationship to reconstruction

Project and source reconstruction may consume Active Developer Assertions and Developer Decisions where appropriate.

Those downstream documents must preserve intervention provenance.

They may define domain-specific intervention targets, such as:

- candidate project boundaries;
- dependency choices;
- source reconstruction choices.

They must not redefine the intervention lifecycle established here.

---

# 27. Relationship to validation

Validation may report that a result depended on Developer Input.

Validation must not treat the existence of a Developer Decision as evidence of historical correctness.

Where controlled evaluation uses a Validation Reference, that reference remains outside recovery and cannot be converted into a Developer Assertion merely to improve the recovery result.

---

# 28. Qualifications resolved by this document

This document creates and resolves the following Developer Intervention qualifications:

| Qualification | Resolution |
|---|---|
| `INTV-001` | Developer Intervention has three canonical kinds: Assertion, Decision, Correction. |
| `INTV-002` | Every committed intervention is an explicit scoped Intervention Record with provenance. |
| `INTV-003` | Intervention lifecycle is Active, Superseded, or Withdrawn; draft UI state is outside canonical recovery state. |
| `INTV-004` | Committed interventions are persisted and replayable so intervention-influenced recovery can remain deterministic. |
| `INTV-005` | Developer Input does not automatically outrank Evidence and cannot by itself create a Recovered Fact. |
| `INTV-006` | Overlapping incompatible Active interventions form an explicit Developer Intervention Conflict; no silent precedence is allowed. |
| `INTV-007` | Withdrawal/supersession requires downstream reevaluation, and reaffirmation must be traceable. |

---

# 29. Durable design consequences

The Developer Intervention model establishes these durable rules:

1. Intervention is deliberate, explicit, and provenance-bearing.
2. The canonical kinds are Assertion, Decision, and Correction.
3. Committed interventions are scoped records.
4. Current intervention state is append/history-preserving.
5. Committed interventions persist and can be replayed.
6. Intervention never silently becomes Evidence.
7. Developer Decisions govern recovery choices, not historical truth by default.
8. Conflicting interventions cannot be resolved by incidental ordering.
9. Withdrawal and correction propagate reevaluation to dependent recovery state.
10. Reaffirmation must be explicit and traceable.

---

# 30. Non-goals

This document does not define:

- CLI syntax;
- GUI design;
- interactive prompt wording;
- storage technology;
- serialization format;
- authorization/identity system;
- multi-user collaboration protocol;
- exact domain-specific intervention targets;
- recovery-stage placement;
- reconstruction algorithms.

Those belong to later canonical documents where appropriate.

---

# 31. Canonical ownership

This document owns:

- Developer Intervention kinds;
- intervention-record semantics;
- intervention scope principles;
- intervention lifecycle;
- persistence/replay requirements;
- intervention conflict semantics;
- withdrawal/reaffirmation workflow semantics;
- rules governing how Developer Input affects recovery.

`03-evidence-model.md` remains authoritative for:

- Evidence versus Developer Input;
- provenance;
- recovered facts/inferences;
- correction/supersession history;
- support dependencies and circularity.

`07-recovery-process.md` will own where interventions occur in the recovery flow.

`12-architecture.md` and later implementation work will own the technical representation.

---

# 32. Governance relationship

This document is constrained by the accepted decisions in `90-decisions.md`.

Its design qualifications and resolution history are recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
