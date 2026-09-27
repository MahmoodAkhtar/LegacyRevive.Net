# LegacyRevive.NET — Convention Inference

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET model for identifying and surfacing recurring conventions from surviving recovery information.

It builds on:

- `03-evidence-model.md`;
- `04-developer-intervention.md`;
- D006 — Convention Inference Is Derived Observation, Not Fact;
- D030 — Support Dependencies Must Prevent Circular Corroboration.

The purpose of convention inference is to help a recovering developer make better-informed interventions and reconstruction choices without converting recurring patterns into historical facts or claims about original developer intent.

---

# 1. Purpose

Surviving .NET artifacts often contain recurring patterns.

Examples may include:

- namespace organization;
- type naming;
- assembly naming;
- dependency direction;
- interface usage;
- configuration structure;
- service naming;
- project-like grouping signals;
- repeated coding or metadata patterns.

These patterns may help a developer choose consistent recovery interventions.

However:

> **A recurring pattern is evidence of recurrence, not proof of original developer intent or universal design policy.**

Convention inference therefore exists to surface useful patterns while preserving:

- provenance;
- scope;
- exceptions;
- uncertainty;
- evidence/inference boundaries;
- anti-feedback-loop protections.

---

# 2. Convention Observation

A **Convention Observation** is a Derived Observation describing a recurring pattern in admissible recovery information.

A Convention Observation:

- is not a Recovered Fact about original developer intent;
- is not automatically an Inference about why the pattern exists;
- must retain provenance to the items from which the pattern was derived;
- must declare the scope in which recurrence was observed;
- must preserve material exceptions;
- may inform later Inferences or Developer Decisions.

Example:

```text
Observed:
14 surviving types under namespace A.B.Services
use an "I<Name>Service" / "<Name>Service" pairing.

Not observed:
The original developers intended this as a universal architectural rule.
```

---

# 3. Eligible inputs

Convention inference may use provenance-bearing recovery information that is admissible under the Evidence Model.

Typical eligible inputs include:

- Observations from Original-System Evidence;
- Recovered Facts;
- Derived Observations whose dependencies are admissible;
- independently supported structural relationships.

Convention inference may also use an earlier Inference as a dependency where the Evidence Model permits inference chaining, but the Convention Observation must inherit that inferential dependency and must not be presented as purely evidence-derived.

Developer Input may influence what patterns are queried or inspected, but it must remain visible as Developer Input.

Validation References never contribute to recovery convention inference.

---

# 4. Generated reconstruction is not independent convention evidence

Reconstructed output may reflect a Convention Observation because a developer or recovery process deliberately followed it.

That reconstructed output must not then be counted as new independent support for the same convention.

For example:

```text
Original artifacts
    ↓
Convention Observation C1
    ↓
Developer Decision uses C1
    ↓
Generated project structure
    ↓
rescan generated structure
```

The rescan does **not** independently strengthen C1.

It remains downstream of C1.

This rule follows D030.

---

# 5. Convention domains

LegacyRevive.NET may surface conventions in domains such as:

- naming;
- namespaces;
- assembly organization;
- dependency relationships;
- interface/implementation pairing;
- configuration;
- service/end-point naming;
- resource organization;
- likely project grouping;
- repeated metadata characteristics.

This list is illustrative rather than exhaustive.

A convention domain becoming technically detectable does not automatically mean it is valuable enough for MVP inclusion.

MVP inclusion is owned by `14-mvp.md`.

---

# 6. Scope

Every Convention Observation must declare its observed scope.

Possible scopes may include:

- recovery target;
- candidate solution;
- candidate project;
- assembly;
- namespace;
- subsystem;
- configuration area;
- another explicitly identified recovery region.

A pattern observed in a narrow scope must not be silently generalized to the entire recovery target.

Example:

```text
Supported:
"Within assemblies X and Y, service interfaces commonly use prefix I."

Unsupported generalization:
"The whole application used interface-prefix naming."
```

---

# 7. Scope hierarchy and generalization

A broader convention may be proposed from narrower observations only when the wider scope itself has adequate supporting coverage.

The existence of the same pattern in two local scopes does not automatically prove a recovery-wide convention.

Broader-scope convention inference must consider:

- how much of the wider scope was observable;
- whether relevant areas are missing;
- whether known exceptions exist;
- whether the samples are independent;
- whether one scope was reconstructed from another.

Generalization is therefore a new Derived Observation or Inference with its own provenance, not a silent widening of an existing Convention Observation.

---

# 8. Recurrence

A convention requires recurrence.

A single occurrence is not a convention.

The model does not establish one universal numeric recurrence threshold for all convention domains.

Instead, a Convention Observation must record enough support information to explain why recurrence is meaningful for that domain and scope.

Relevant support may include:

- number of supporting instances;
- number of independently observed source areas;
- coverage of the relevant scope;
- consistency;
- material exceptions;
- dependency independence.

This avoids arbitrary thresholds that would falsely imply the same statistical meaning across naming, dependencies, configuration, and other domains.

---

# 9. Exceptions

Material exceptions are part of the Convention Observation.

They must not be discarded merely to make a convention look cleaner.

A convention may still be useful when exceptions exist.

For example:

```text
12 of 15 directly observed service implementations
follow pattern P.

3 directly observed exceptions:
A, B, C.
```

The observation should expose both the pattern and the exceptions.

An exception may indicate:

- a genuine local variation;
- an older/newer convention;
- a subsystem boundary;
- incomplete evidence;
- an incorrect proposed convention.

The Convention Inference model does not infer which explanation is correct without further support.

---

# 10. Convention support profile

A Convention Observation should carry an explainable support profile.

The support profile should identify, where applicable:

- supporting item count;
- independently supported source count;
- known exception count;
- observed scope;
- estimated observable coverage where meaningful;
- whether support includes inferential dependencies;
- whether Developer Input influenced discovery or scoping.

This profile is descriptive.

It must not be collapsed into an unexplained probability.

---

# 11. Support strength

Convention Observations are Derived Observations, not Inferences, so the inferential confidence labels from `03-evidence-model.md` do not attach directly to them.

Where LegacyRevive.NET makes an **Inference based on a Convention Observation**, that Inference may carry:

- Strongly Supported;
- Plausible;
- Tentative;

according to the Evidence Model.

The Convention Observation itself instead exposes its support profile, scope, provenance, and exceptions.

This preserves the distinction between:

```text
"We observed this recurring pattern."
```

and:

```text
"We infer that an unresolved artifact probably follows this pattern."
```

---

# 12. Convention-based inference

A Convention Observation may support an Inference about unresolved recovery state.

Example:

```text
Convention Observation:
Most directly observed assemblies in subsystem S
use namespace root Company.Product.S.

Inference:
An unresolved type associated with subsystem S
may plausibly have belonged under that namespace root.
```

The Inference must retain:

- the Convention Observation as a support dependency;
- any other support;
- the applicable scope;
- its inferential support strength.

The inferred target does not become part of the convention's independent supporting sample merely because the inference was adopted.

---

# 13. Convention and Developer Intervention

Convention Observations are especially useful as inputs to Developer Decisions.

Example:

```text
Convention Observation
    ↓
Developer Decision
    ↓
Reconstruction
```

The Developer Decision must preserve that dependency.

A developer may:

- accept the convention as useful guidance;
- reject it for a particular intervention;
- choose an exception;
- narrow the applicable scope;
- supply additional Developer Input.

The convention does not automatically control recovery.

---

# 14. Convention adoption does not prove history

If a developer chooses to follow a Convention Observation during reconstruction, the resulting reconstruction shows:

> the recovery followed that convention.

It does not show:

> the original system necessarily followed that convention in the unresolved area.

Reports and provenance must preserve that difference.

---

# 15. Original developer intent

Convention inference must not claim historical motivation merely from recurrence.

Statements such as:

- "the developers intended...";
- "the architecture required...";
- "this was their design rule...";

require suitable evidence beyond recurrence itself.

Where such evidence does not exist, LegacyRevive.NET should use descriptive wording such as:

- "observed pattern";
- "recurring convention";
- "common within the observed scope";
- "consistent with the observed convention".

---

# 16. Multiple conventions in one recovery target

A recovery target may contain different conventions in different scopes.

LegacyRevive.NET must not assume that one target has one universal style.

For example:

```text
Subsystem A → convention C1
Subsystem B → convention C2
```

Both may be valid Convention Observations.

A broader convention should not suppress meaningful local conventions.

This is particularly important in long-lived legacy systems that may contain code from different eras, teams, acquisitions, or migrations; however, the model does not infer those historical causes unless supported.

---

# 17. Competing convention observations

Two Convention Observations may compete or overlap.

For example:

- one pattern dominates at solution scope;
- a different pattern dominates in a subsystem.

This is not automatically a contradiction.

The model should first examine scope.

A **Convention Conflict** exists when incompatible Convention Observations claim materially overlapping scope such that both cannot guide the same unresolved recovery decision without further choice.

A Convention Conflict must preserve:

- both convention observations;
- their scopes;
- their support profiles;
- their provenance;
- known exceptions.

It may remain unresolved or be handled through a Developer Decision.

---

# 18. Convention lifecycle

Convention Observations are historical evidence-model items and are not silently rewritten.

When new information changes the pattern:

- retain the earlier Convention Observation;
- create an updated observation;
- link it as superseding the earlier one for current use;
- preserve the earlier provenance and support profile.

A convention may become:

- **Current**
- **Superseded**
- **Needs Reevaluation**
- **Invalidated**

These current-state markers follow the evidence-history principles in `03-evidence-model.md`.

---

# 19. Reevaluation

Convention Observations must be reevaluated when material dependencies change.

Examples include:

- supporting evidence is corrected;
- an assumption supporting a derived input is invalidated;
- a developer correction changes a material upstream classification;
- a support path is discovered to be circular;
- new original evidence materially changes the observed pattern.

A Convention Observation marked `Needs Reevaluation` must not continue to guide new recovery decisions as though it were current.

---

# 20. Determinism

Given the same:

- admissible inputs;
- tool version;
- convention rules;
- configuration;
- committed developer interventions;

convention inference should produce the same substantive Convention Observations wherever practical.

This follows D014.

Determinism does not require hiding uncertainty.

The same evidence may deterministically produce:

- a pattern;
- its exceptions;
- its scope;
- its unresolved limitations.

---

# 21. Heuristics

Convention detection may use Recovery Heuristics.

A heuristic may identify candidate recurrence, grouping, or pattern structure.

However:

- heuristic output is not fact merely because the heuristic is reusable;
- the resulting Convention Observation must expose its actual support;
- heuristics must not replace provenance;
- heuristic performance on other systems is research evidence, not target-system evidence.

Exact heuristic algorithms belong to later implementation/tooling design.

---

# 22. Open-source heuristic research

Open-source codebases may be studied to test whether convention-detection heuristics are useful.

This research may help answer questions such as:

- which recurring patterns are reliably detectable;
- which pattern types commonly produce false generalization;
- what forms of exception reporting are useful.

But when a heuristic is applied to a specific LegacyRevive.NET recovery target:

> **the target's own recovery information must support the Convention Observation.**

Research corpus behavior is not direct evidence about the target.

---

# 23. Reporting requirements

When surfaced to a developer, a material Convention Observation should make clear:

- what pattern was observed;
- where it was observed;
- what supports it;
- known exceptions;
- whether support includes inferential dependencies;
- whether Developer Input influenced its discovery or scope;
- whether the observation is Current or requires reevaluation.

The report should avoid language that implies intent or certainty beyond the support.

---

# 24. Convention Observation as guidance

A Convention Observation is guidance-capable knowledge.

It may help a developer:

- choose between plausible project groupings;
- choose naming for reconstructed artifacts;
- understand likely local organization;
- choose where to inspect next;
- make interventions that remain consistent with observed material.

It should not force a reconstruction where stronger contrary evidence exists.

Stronger directly supported information takes precedence over convention-based inference.

---

# 25. Convention inference and recovery ordering

Convention inference should generally consume already-established observations and recovered structure rather than being used to override stronger direct information.

It therefore follows the broader principle:

> **Establish stronger directly supported information before allowing weaker convention-based inference to shape reconstruction.**

The exact stage placement belongs in `07-recovery-process.md`.

---

# 26. Qualifications resolved by this document

This document creates and resolves the following Convention Inference qualifications:

| Qualification | Resolution |
|---|---|
| `CONV-001` | A Convention Observation is a scoped Derived Observation describing recurrence; it is not a fact about original developer intent. |
| `CONV-002` | Convention inference may use admissible provenance-bearing recovery information, but generated reconstruction cannot independently support the convention that shaped it. |
| `CONV-003` | Convention Observations require explicit scope; widening scope creates a new supported generalization rather than silently expanding the original. |
| `CONV-004` | Convention recurrence has no single universal numeric threshold; support must be explained through counts, coverage, independence, consistency, and exceptions appropriate to the domain. |
| `CONV-005` | Material exceptions are preserved as part of the Convention Observation and may not be discarded to strengthen the apparent pattern. |
| `CONV-006` | Convention Observations use descriptive support profiles; inferential confidence labels apply only to later Inferences based on them. |
| `CONV-007` | Overlapping incompatible Convention Observations form a Convention Conflict rather than being resolved by hidden precedence. |
| `CONV-008` | Convention Observations preserve historical versions and are reevaluated when material dependencies change. |

---

# 27. Durable design consequences

Convention inference establishes these durable rules:

1. Convention observations describe recurrence, not intent.
2. Scope is mandatory.
3. Generalization requires its own support.
4. A single occurrence is not a convention.
5. No universal numeric recurrence threshold is imposed across domains.
6. Material exceptions remain visible.
7. Convention support is descriptive rather than pseudo-statistical.
8. Confidence belongs to convention-based Inferences, not the Convention Observation itself.
9. Generated reconstruction cannot corroborate the convention that shaped it.
10. Competing scoped conventions remain explicit.
11. Convention history is append-preserving.
12. Dependency changes trigger reevaluation.

---

# 28. Non-goals

This document does not define:

- exact pattern-detection algorithms;
- numerical heuristic weights;
- machine-learning models;
- project-boundary algorithms;
- source reconstruction algorithms;
- CLI syntax;
- report layout;
- storage technology;
- MVP inclusion of individual convention domains.

Those belong to later canonical documents where appropriate.

---

# 29. Canonical ownership

This document owns:

- Convention Observation semantics;
- eligible convention-inference inputs;
- scope/generalization rules;
- recurrence and exception principles;
- support profiles;
- convention conflict semantics;
- convention lifecycle/reevaluation;
- anti-feedback-loop rules specific to conventions.

`03-evidence-model.md` remains authoritative for the underlying Evidence, Derived Observation, Inference, Provenance, confidence, and support-dependency semantics.

`04-developer-intervention.md` remains authoritative for how a developer uses convention knowledge in a committed intervention.

`07-recovery-process.md` will own when convention inference occurs in the end-to-end recovery flow.

`08-project-reconstruction.md` and `09-source-reconstruction.md` may define domain-specific uses of Convention Observations without redefining this model.

---

# 30. Governance relationship

This document is constrained by accepted decisions in `90-decisions.md`.

Its qualification history is recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
