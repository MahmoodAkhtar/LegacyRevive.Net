# LegacyRevive.NET — Recovery Metrics

## Status

**Canonical**

This document defines the canonical measurement and reporting model for LegacyRevive.NET recovery quality.

It operationalizes the multidimensional validation methodology defined in:

`10-validation-strategy.md`

It owns:

- metric applicability and denominator rules;
- correctness versus coverage semantics;
- set-oriented precision/recall conventions;
- recoverability-aware versus raw historical-fidelity views;
- metric families for structural, dependency, source, runtime, epistemic, and operational recovery;
- developer-intervention burden measures;
- aggregation and reporting rules;
- cross-run and cross-scenario comparability requirements.

It does not replace Validation Findings or collapse them into one opaque recovery score.

This document is constrained by `90-decisions.md` and the resolved `METR-*` qualifications in `91-design-qualification-register.md`.

---

# 1. Purpose

LegacyRevive.NET metrics answer:

> **How well did an identified recovery result perform on the applicable validation questions, how much of the recoverable/reference space did it cover, how much human intervention was required, and how trustworthy was its handling of uncertainty?**

Metrics are descriptive measurements over a declared Validation Target, Validation Plan, comparison population, and environment.

A metric result is not historical evidence about how the original repository was authored.

The measurement model must preserve these distinctions:

```text
Correctness ≠ coverage
Coverage ≠ recoverability
Recoverability ≠ historical fidelity
Buildability ≠ behavioural equivalence
Similarity ≠ semantic equivalence
Unknown ≠ correct
Unknown ≠ automatically wrong
Not Evaluated ≠ failure
Not Comparable ≠ failure
Operational usefulness ≠ historical exactness
Support strength ≠ numeric probability
```

---

# 2. Scope and ownership

This document owns the metric semantics used to summarize Validation Runs and recovery benchmarks.

It does **not** own:

- validation-target/reference/run semantics — `10-validation-strategy.md`;
- evidence categories, support strength, provenance, conflicts, or assumptions — `03-evidence-model.md`;
- developer-intervention lifecycle — `04-developer-intervention.md`;
- artifact recoverability classes — `06-recovery-matrix.md`;
- recovery-stage orchestration — `07-recovery-process.md`;
- Candidate Project semantics — `08-project-reconstruction.md`;
- source provenance/reconstruction semantics — `09-source-reconstruction.md`;
- implementation/storage architecture — `12-architecture.md`.

Metrics consume those models without redefining them.

---

# 3. Recovery Metric

A **Recovery Metric** is a named measurement computed over an explicitly defined validation/recovery population.

Every metric definition must state, where applicable:

1. **question** — what the metric is intended to measure;
2. **scope** — Validation Target, dimension, scenario, project, artifact group, or other bounded subject;
3. **eligible population** — which items may contribute;
4. **matching/comparison rule** — how target and reference items correspond;
5. **numerator**;
6. **denominator**;
7. **excluded/non-applicable population**;
8. **Not Evaluated / Not Comparable treatment**;
9. **Unknown/abstention treatment**;
10. **units**;
11. **provenance** — Validation Run/findings and reference context from which the result was derived;
12. **limitations**.

A percentage without a declared population and denominator is not an acceptable canonical recovery metric.

**Governance:** METR-001 / D094.

---

# 4. Applicability and population accounting

Metric results must expose enough population accounting to prevent denominator distortion.

A useful generic accounting model is:

```text
Reference / candidate population
        │
        ├── Eligible and comparable
        │       ├── evaluated / attempted
        │       ├── unknown / abstained where applicable
        │       └── execution error where applicable
        │
        ├── Not Evaluated
        ├── Not Comparable
        └── Not Applicable
```

`Not Evaluated` and `Not Comparable` do not silently enter accuracy denominators as failures or successes.

They must nevertheless be reported as counts/rates because a nominally high correctness percentage over a very small comparable subset may be misleading.

Example:

```text
Comparable reference dependencies: 80
Correctly represented:              76
Incorrectly represented:             4
Not Comparable:                     15
Not Evaluated:                       5
```

The relevant representation accuracy is:

```text
76 / 80 = 95%
```

but the report must also show that 20 additional items were not part of that accuracy denominator.

**Governance:** METR-001 / D094; VAL-004 / D087.

---

# 5. Correctness and coverage are separate

LegacyRevive.NET must not make a system look more accurate merely by declining to answer difficult questions.

For metrics where abstention/unknown is possible, report at least two independent ideas:

## 5.1 Correctness

Of the items for which LegacyRevive.NET made a comparable assertion/reconstruction decision, how many were correct under the defined comparator?

Generic form:

```text
Correctness = Correct comparable assertions / Comparable assertions made
```

## 5.2 Coverage

Of the eligible/comparable reference population, how much did LegacyRevive.NET attempt or establish?

Generic form:

```text
Coverage = Comparable assertions made / Eligible comparable population
```

An `Unknown` is not counted as a correct answer merely because abstaining was cautious.

But Unknown is also not automatically a defect. Its quality is assessed separately through recoverability/epistemic metrics.

Example:

```text
Eligible comparable items: 100
Assertions made:            80
Correct assertions:         78
Unknown/abstained:           20

Correctness: 78 / 80 = 97.5%
Coverage:    80 / 100 = 80%
```

Both numbers are necessary.

**Governance:** METR-002 / D095.

---

# 6. Set reconstruction: precision and recall

Where recovery produces or identifies a set of entities or relationships, precision and recall are preferred to an undifferentiated accuracy percentage.

For a reference set `R` and reconstructed set `C`, under a declared matching rule:

```text
TP = items in C that correctly correspond to items in R
FP = items in C with no valid reference counterpart
FN = items in R that should have been recovered but were not

Precision = TP / (TP + FP)
Recall    = TP / (TP + FN)
```

`F1` may be reported as a secondary summary when useful:

```text
F1 = 2 × Precision × Recall / (Precision + Recall)
```

F1 must not replace the underlying precision and recall values.

Typical set-oriented metrics include:

- application assembly classification;
- Candidate Project discovery;
- dependency-edge discovery;
- package identity recovery;
- type/member recovery;
- resource/configuration item recovery;
- conflict detection in controlled adversarial scenarios.

The matching rule must be explicit. Name equality alone is not automatically sufficient where identity can differ legitimately.

**Governance:** METR-003 / D096.

---

# 7. Raw historical fidelity versus recoverability-aware effectiveness

A known original/reference may contain historical details that the allowed recovery artifacts could not establish.

LegacyRevive.NET must therefore keep two views separate where recoverability classification is available.

## 7.1 Raw historical fidelity

Asks:

> How closely does the reconstructed result match the chosen historical reference across the comparable reference population?

This view does not pretend that every mismatch was avoidable.

## 7.2 Recoverability-aware effectiveness

Asks:

> How well did LegacyRevive.NET recover information that the allowed artifact scenario could reasonably establish under the canonical recovery model?

Items assessed as not establishable from the allowed Recovery Input are not silently counted as tool failures in this view.

However, those items remain visible in the raw historical-fidelity view and in the recoverability classification counts.

A recoverability-aware denominator must be traceable to:

- the actual allowed Recovery Input;
- `06-recovery-matrix.md` contribution/ceiling semantics;
- any scenario-specific validation rule used to determine whether an item was Direct, Inferable, Heuristic, or Not-established.

This prevents two opposite distortions:

- penalizing LegacyRevive.NET for information unavailable in principle from the scenario;
- excusing missed recoverable information by labeling it unknown after the fact.

**Governance:** METR-004 / D097.

---

# 8. Discovery and structural metrics

## 8.1 Application-assembly classification

Where ground truth exists:

- application-owned assembly precision;
- application-owned assembly recall;
- third-party/framework classification accuracy on comparable classified assemblies;
- unknown classification count/rate.

## 8.2 Candidate Project discovery

For deployable/reference projects that have a defensible correspondence to supplied artifacts:

```text
Project precision = Correct project candidates / Generated project candidates
Project recall    = Correct project candidates / Eligible reference projects
```

The Validation Plan must declare how merged/split candidates are matched.

A one-project-per-DLL reconstruction is not automatically considered structurally correct merely because every DLL received a project.

## 8.3 Project metadata

For matched Candidate Projects, report field-level accuracy/coverage for comparable properties such as:

- target framework/runtime;
- output/project kind;
- assembly name;
- root namespace where historically comparable;
- platform target;
- signing;
- resource/application characteristics.

Properties that were historically unknowable or not comparable remain explicitly classified rather than forced into the correctness denominator.

---

# 9. Dependency and package metrics

Dependency validation separates **relationship existence** from **historical representation/mechanism**.

For an edge `A → B`, independently measure:

1. whether the dependency relationship was recovered;
2. whether its dependency category was correct;
3. whether the historical declaration mechanism was recovered where comparable.

Example:

```text
Original: A references B via ProjectReference
Recovered: A depends on B, represented as binary reference

Dependency existence: correct
Historical representation: divergent
```

Recommended metrics include:

- dependency-edge precision;
- dependency-edge recall;
- dependency-category accuracy;
- unresolved-dependency count;
- package identity precision/recall;
- exact package-version accuracy where comparable;
- direct-versus-transitive classification accuracy where knowable;
- project/package/binary-reference mechanism accuracy where historical reference establishes it.

A correct dependency edge must not be scored as entirely wrong merely because the historical packaging mechanism was unknowable, and a correct mechanism guess must not hide a missing dependency edge.

**Governance:** METR-005 / D098.

---

# 10. API and type-system metrics

Metadata-rich .NET artifacts make API/type-system recovery a high-value metric family.

Where comparison is applicable, measure set precision/recall or exact-match rates for:

- namespaces;
- top-level and nested types;
- base types/interfaces;
- generic parameters and constraints;
- constructors;
- methods;
- properties;
- fields;
- events;
- accessibility;
- attributes;
- signatures.

For public API recovery, a report may include:

```text
Reference public members:  18,497
Recovered matches:         18,491
False additions:                2
Missed members:                 6
```

The precise identity/matching rules must account for declaring type, member kind, name, generic arity, parameter types, return type, and other signature components relevant to the comparator.

---

# 11. Source-fidelity metrics

Source validation must not privilege textual resemblance over semantic usefulness.

Recommended ordering of evidence strength for measurement is generally:

1. declaration/API equivalence;
2. semantic/normalized representation equivalence where technically defensible;
3. normalized IL or compiled-contract comparison where applicable;
4. behavioral validation;
5. textual/syntactic similarity as a diagnostic/secondary measure.

Textual similarity may be useful for:

- investigating decompiler output;
- comparing formatting-independent syntax;
- measuring file/layout recovery where PDB/reference data exists.

It must not be reported as proof of original source recovery.

Possible metrics include:

- declaration coverage;
- signature exact-match rate;
- method semantic-comparison coverage;
- normalized-IL match rate where the comparison is meaningful;
- source-file/path match rate only where historical file identity is known;
- XML-documentation member mapping/recovery rate when XML docs exist.

Exact source text, comments, formatting, local variable names, compiler-generated transformations, and original file organization may have different recovery ceilings and must not be conflated with executable semantics.

**Governance:** METR-006 / D099.

---

# 12. Resources and configuration metrics

Where a reliable reference exists, report precision/recall and value accuracy for applicable runtime material.

Examples:

- configuration section presence;
- setting/key presence;
- value equality/normalization;
- assembly binding redirects;
- WCF services;
- endpoints;
- contracts;
- bindings;
- behaviors;
- client endpoints;
- connection-string presence/value where safe and comparable;
- embedded-resource identity;
- satellite-resource culture coverage;
- manifest/runtime configuration values.

Presence recovery and value/semantic accuracy should be separate when possible.

Example:

```text
Endpoints present:          37 / 37
Binding type correct:       37 / 37
Comparable binding values:  34 / 36
Not Comparable values:       1
Not Evaluated values:        1
```

---

# 13. Build and operational validation metrics

Operational recovery is graduated rather than binary.

Report applicable milestones independently:

- solution/project restore success;
- projects restored / eligible projects;
- projects compiled / eligible projects;
- solution build status;
- compiler error count;
- compiler warning count where useful;
- executable/host startup status;
- runnable scenarios;
- number of automated repair/refinement passes;
- number of remaining blocking issues.

Build progression should be retained across meaningful refinement checkpoints, for example:

```text
Pass 1: 247 errors
Pass 2:  38 errors
Pass 3:   4 errors
Pass 4:   0 errors
```

This measures the recovery/refinement engine rather than treating only the final boolean build result as informative.

Build metrics must record enough environment/toolchain context for interpretation.

**Governance:** METR-007 / D100.

---

# 14. Behavioral metrics

Behavioral metrics are bounded by the evaluated scenario set.

Where an original/authoritative test suite can be executed against reconstructed production code, report at least:

- tests/scenarios discovered;
- applicable tests/scenarios;
- executed;
- passed;
- failed;
- skipped/Not Evaluated;
- Not Comparable;
- infrastructure/execution errors.

A basic scenario pass rate is:

```text
Pass rate = Passed / Executed comparable scenarios
```

But the report must also expose scenario coverage.

Failures should be diagnosable where practical into categories such as:

- reconstruction divergence;
- missing resource;
- configuration difference;
- unavailable external dependency;
- environment mismatch;
- test harness/build assumption;
- unknown.

Passing 914 of 927 executed tests is strong evidence about those tests, not a claim of global behavioral equivalence.

---

# 15. Developer-intervention burden

Developer intervention is an expected part of LegacyRevive.NET and therefore its cost is a first-class recovery metric, not an embarrassment to hide.

Recommended measures include:

- number of Developer Assertions committed;
- number of Developer Decisions committed;
- number of Developer Corrections committed;
- number of conflicts requiring intervention;
- number of unresolved decisions remaining;
- number of manual reference/package/configuration corrections;
- number of files manually edited outside automated reconstruction, where tracked;
- number of recovery iterations triggered by intervention.

Counts should be grouped by kind and scope where useful rather than reduced to one intervention number.

## 15.1 Time measures

Time-to-milestone may also be recorded:

- time to inventory;
- time to first candidate solution;
- time to first build attempt;
- time to first successful build;
- time to Workable Baseline;
- time to Validated Baseline.

Time is highly environment- and human-dependent. It must therefore be accompanied by benchmark/environment context and should not be treated as a universal correctness measure.

**Governance:** METR-008 / D101.

---

# 16. Epistemic accuracy metrics

LegacyRevive.NET is not only evaluated on whether it reconstructs the correct answer. It is also evaluated on whether it knows when its evidence does and does not justify an answer.

This is a first-class metric family.

## 16.1 False assertion rate

For comparable assertions made:

```text
False assertion rate = Incorrect comparable assertions / Comparable assertions made
```

A particularly important variant is the false assertion rate for **Strongly Supported** inferential conclusions.

The objective is not to assign numeric probability to the support label; it is to detect whether supposedly stronger support is producing an unacceptable number of incorrect conclusions.

## 16.2 Support-strength calibration profile

For each inferential support class:

```text
Strongly Supported
Plausible
Tentative
```

report:

- number of comparable inferences;
- correct;
- incorrect;
- unknown/Not Comparable;
- empirical correctness rate among comparable items.

The expected qualitative ordering is that stronger support should not systematically perform worse than weaker support.

This is a calibration diagnostic, not a conversion of the support vocabulary into probabilities.

## 16.3 Appropriate unknown / abstention

Where the validation corpus can establish recoverability expectations, distinguish:

- **justified unknown** — available input did not support a defensible conclusion;
- **missed recoverable** — sufficient applicable input existed but the tool failed to establish/use it;
- **unsupported assertion** — the tool asserted a conclusion despite insufficient support.

This prevents both reckless guessing and trivial success through excessive abstention.

## 16.4 Conflict detection

In controlled scenarios with known conflicts:

- conflict-detection precision;
- conflict-detection recall;
- false conflict count;
- missed conflict count;
- correct deferral/decision behavior.

## 16.5 Provenance validity

For audited conclusions/reconstructions, measure whether the declared provenance/support path actually supports the claimed conclusion under the Evidence Model.

Possible measures:

```text
Valid provenance paths / Audited comparable paths
Circular/self-support violations
Broken/missing provenance links
```

This directly tests explainability rather than merely checking that a provenance field is populated.

**Governance:** METR-009 / D102.

---

# 17. Operational recovery metrics

Historical fidelity and developer usefulness are related but not identical.

Operational metrics answer questions such as:

- can the recovered solution be opened/restored/built?;
- how many projects compile?;
- how much remains blocked?;
- can key hosts start?;
- how many tests/scenarios execute?;
- how much developer intervention remains?;
- how many automated refinement passes were required?;
- how long did it take under the recorded benchmark conditions?;

A recovery may have high operational usefulness despite unavoidable differences from the original repository.

Conversely, high structural similarity alone does not guarantee a workable development baseline.

Raw historical-fidelity metrics and operational-recovery metrics therefore remain separate dimensions.

---

# 18. Metric aggregation

## 18.1 Dimension summaries

Metrics may be grouped into named dimension summaries for reporting convenience.

A summary must retain access to its constituent metrics and populations.

## 18.2 No authoritative global recovery score

LegacyRevive.NET must not reduce recovery quality to one authoritative opaque percentage such as:

```text
LegacyRevive score: 94 / 100
```

Such a number would hide differences among:

- discovery;
- structure;
- dependencies;
- source/API fidelity;
- runtime/configuration;
- epistemic accuracy;
- operational recovery;
- developer intervention burden.

If a future UI introduces a composite indicator for a narrow presentation purpose, it must be explicitly secondary, disclose its formula/weights, and never replace the underlying canonical dimension metrics.

**Governance:** METR-010 / D103; D008 / D086.

---

# 19. Recommended top-level reporting dimensions

The metric report should normally preserve at least these top-level families when applicable:

| Dimension | Core measurement question |
|---|---|
| Discovery | Did LegacyRevive correctly understand/classify the supplied artifact set? |
| Structural Fidelity | Did it reconstruct project/dependency/framework structure accurately? |
| Source/API Fidelity | Did it reconstruct declarations, contracts, and executable semantics accurately? |
| Runtime Fidelity | Did it preserve configuration, resources, services, and evaluated runtime behavior? |
| Epistemic Accuracy | Were assertions, uncertainty, conflicts, support strength, and provenance handled correctly? |
| Operational Recovery | How close is the result to a workable development baseline, and at what intervention cost? |

These are reporting families, not a license to force every metric into one of six numeric scores.

---

# 20. Cross-run and cross-scenario comparability

Metrics are comparable across runs only when the compared conditions are materially compatible or the differences are explicitly normalized/explained.

A benchmark comparison should identify at least the relevant:

- subject/reference version;
- artifact scenario;
- allowed Recovery Input;
- LegacyRevive.NET version/configuration;
- Validation Plan/metric definitions;
- matching rules;
- environment/toolchain;
- developer intervention policy;
- validation reference snapshot.

Results from different artifact-starvation scenarios must not be presented as direct tool regressions without accounting for the changed evidence ceiling.

Likewise, changing a metric denominator/matching rule creates a new metric definition/version rather than silently rewriting historical benchmark results.

**Governance:** METR-011 / D104.

---

# 21. Artifact-starvation and marginal-value reporting

Controlled corpus evaluation may run the same reference subject under progressively reduced artifact sets, for example:

```text
A  DLL + EXE + config + PDB + XML docs
B  DLL + EXE + config + PDB
C  DLL + EXE + config
D  DLL + EXE
E  DLL only
```

For each scenario, report the same applicable metric families where defensible.

The change between scenarios can estimate the marginal recovery value of an artifact class, for example:

```text
Δ project-boundary recall after adding PDBs
Δ documentation recovery after adding XML docs
Δ package-version accuracy after adding deps.json/packages.config
```

The delta is scenario/reference specific and must not be generalized into a universal artifact-value percentage without broader evidence.

---

# 22. Adversarial/mixed-deployment metrics

Mutation scenarios can evaluate whether LegacyRevive.NET becomes confidently wrong when inputs are inconsistent.

Examples include:

- missing DLL;
- unrelated DLL injected;
- duplicate dependency versions;
- stale config;
- Debug/Release mixture;
- artifacts from different releases;
- renamed deployment files;
- missing PDB/resource companion;
- conflicting framework/runtime evidence.

Useful metrics include:

- true conflict detection;
- false conflict rate;
- inappropriate auto-resolution count;
- appropriate deferral count;
- false strongly-supported assertion count;
- mixed-release detection;
- downstream invalidation/reevaluation correctness.

These scenarios primarily test epistemic safety rather than ordinary happy-path reconstruction fidelity.

---

# 23. Example benchmark report shape

A benchmark should make the underlying dimensions visible.

Example:

```text
LegacyRevive.NET Validation Report

Subject
  ContosoOrders v3.8
  .NET Framework 4.7.2
  12 deployable reference projects
  38 third-party dependencies

Artifact scenario
  DLL + EXE + config
  no PDB
  no XML documentation

Discovery
  application assemblies       12 / 12
  third-party classification   37 / 38

Structural Fidelity
  project precision            12 / 12
  project recall               12 / 12
  dependency edges             31 / 32
  target frameworks            12 / 12 comparable

Source/API Fidelity
  types                        2,842 / 2,842
  members                     18,491 / 18,497
  public API                   reported separately

Runtime Fidelity
  comparable configs             9 / 9
  WCF services                  14 / 14
  endpoints                     37 / 37
  resources                    103 / 105

Epistemic Accuracy
  false strongly-supported assertions   1
  appropriate unknowns                  17 / 18 assessed
  detected seeded conflicts              4 / 4

Operational Recovery
  projects restored             12 / 12
  projects compiled             11 / 12
  tests passed                 914 / 927 executed
  automated refinement passes    3
  developer decisions             2

Final recovery state
  Workable with intervention
```

The report does not need one aggregate score to be interpretable.

---

# 24. Corpus regression use

The canonical metric model enables per-commit regression testing:

```text
LegacyRevive commit N
        ↓
held-out validation corpus
        ↓
metric report
        ↓
compare with commit N+1
```

Regression analysis should preserve metric-level changes, for example:

```text
Package identity recall      +3.2 pp
Project precision            -1.1 pp
Strongly-supported error      +2 cases
Developer decisions           -4
Build success                 unchanged
```

This prevents a positive aggregate number from hiding a serious epistemic or structural regression.

---

# 25. Metric invariants

The following invariants are canonical:

1. every ratio has an explicit eligible population and denominator;
2. Not Evaluated and Not Comparable remain visible outside correctness denominators;
3. correctness and coverage are separate;
4. Unknown/abstention is not counted as correct merely for being cautious;
5. set reconstruction uses explicit matching rules and normally exposes precision and recall;
6. raw historical fidelity and recoverability-aware effectiveness remain separate where both are available;
7. dependency existence and historical dependency representation are measured separately;
8. semantic/API/behavioral source measures outrank raw textual similarity as claims of executable fidelity;
9. build progress is graduated and does not prove behavior;
10. developer intervention burden is first-class;
11. epistemic accuracy is first-class;
12. support-strength calibration is descriptive and does not turn support labels into probabilities;
13. no authoritative global score replaces the underlying dimensions;
14. cross-run comparison requires compatible or explicitly qualified scenarios and metric definitions;
15. metric history is not silently rewritten when definitions or conditions change.

---

# 26. Governance mapping

This document resolves and implements the following qualifications:

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| METR-001 | D094 | Metric applicability/populations and denominators are explicit |
| METR-002 | D095 | Correctness and coverage are separate; abstention is not correctness |
| METR-003 | D096 | Set reconstruction uses explicit matching plus precision/recall |
| METR-004 | D097 | Raw historical fidelity and recoverability-aware effectiveness remain separate |
| METR-005 | D098 | Dependency existence and historical representation are measured separately |
| METR-006 | D099 | Semantic/API/behavioral source fidelity is distinct from textual similarity |
| METR-007 | D100 | Build/operational recovery is graduated and environment-scoped |
| METR-008 | D101 | Developer-intervention burden is a first-class metric family |
| METR-009 | D102 | Epistemic accuracy is a first-class metric family |
| METR-010 | D103 | No authoritative global score may replace dimension metrics |
| METR-011 | D104 | Cross-run comparison requires compatible/qualified scenarios and stable metric definitions |

---

# 27. Relationship to architecture

`12-architecture.md` may choose concrete representations for:

- metric definitions;
- metric result records;
- population counters;
- comparator adapters;
- benchmark manifests;
- report serialization;
- metric-version identifiers.

Those implementation choices must preserve the semantics defined here.

---

# 28. Governance relationship

This document is constrained by:

- `00-ai-context.md`;
- `01-purpose-and-principles.md`;
- `02-terminology.md`;
- `03-evidence-model.md`;
- `04-developer-intervention.md`;
- `06-recovery-matrix.md`;
- `07-recovery-process.md`;
- `08-project-reconstruction.md`;
- `09-source-reconstruction.md`;
- `10-validation-strategy.md`;
- `90-decisions.md`;
- `91-design-qualification-register.md`.

Durable metric decisions are owned by `90-decisions.md`.

Qualification status/history is owned by `91-design-qualification-register.md`.

If later work materially challenges a settled metric rule, governance requires a new qualification and, where appropriate, a superseding decision rather than silent reinterpretation.
