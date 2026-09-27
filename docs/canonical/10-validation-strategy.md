# LegacyRevive.NET — Validation Strategy

## Status

**Canonical**

This document defines the canonical validation methodology for LegacyRevive.NET.

It owns the rules for identifying what is being validated, what it is being compared against, which validation dimensions are applicable, how validation findings are interpreted, how blind evaluation avoids contaminating recovery, and what a Validated Baseline means.

Detailed metric formulae, aggregation, applicability, denominators, and reporting measures are canonically defined in:

`11-recovery-metrics.md`

This document is constrained by the accepted decisions in `90-decisions.md` and the resolved validation qualifications `VAL-001` through `VAL-010` in `91-design-qualification-register.md`.

---

# 1. Purpose

LegacyRevive.NET validation answers:

> **What has been established about the quality, fidelity, limitations, and operational usefulness of an identified recovery result?**

Validation does not convert reconstruction into historical fact.

It must preserve the established distinctions:

```text
Recovery result ≠ original repository
Build success ≠ behavioural equivalence
Structural similarity ≠ source identity
Validation reference ≠ recovery input
Validation finding ≠ recovered fact about the original development representation
```

Validation exists to make recovery quality observable without overstating what any individual comparison proves.

---

# 2. Scope and ownership

This document owns:

- Validation Target semantics;
- Validation Reference Snapshot semantics;
- Validation Plan semantics;
- Validation Dimension and Validation Check methodology;
- Validation Finding and Validation Run methodology;
- blind-validation isolation;
- target/reference compatibility requirements;
- validation claim boundaries;
- validation provenance and history;
- reference-derived feedback boundaries;
- Validated Baseline semantics.

This document does **not** own:

- evidence taxonomy or evidence provenance — `03-evidence-model.md`;
- developer intervention semantics — `04-developer-intervention.md`;
- artifact recoverability — `06-recovery-matrix.md`;
- recovery-stage orchestration — `07-recovery-process.md`;
- project reconstruction — `08-project-reconstruction.md`;
- source reconstruction — `09-source-reconstruction.md`;
- detailed recovery metrics — `11-recovery-metrics.md`;
- implementation architecture — `12-architecture.md`.

---

# 3. Validation Target

A **Validation Target** is the identified recovery result or Recovery Checkpoint being evaluated.

Validation never evaluates an unspecified moving workspace state.

A target must identify enough recovery state to allow later interpretation and reproduction, including the applicable:

- Recovery Workspace;
- Recovery Run;
- Recovery Checkpoint or equivalent stable state;
- generated/reconstructed artifacts;
- active developer interventions;
- relevant configuration;
- LegacyRevive.NET/tool version where available.

A materially changed recovery result is a **new Validation Target**.

Changes that can require a new target include:

- changed project boundaries;
- changed source reconstruction;
- changed dependency representation;
- changed developer intervention;
- added or removed recovery artifacts;
- changed reconstruction settings;
- materially different build-repair state.

Historical validation findings remain attached to the target they evaluated.

**Governance:** VAL-001 / D084.

---

# 4. Validation Reference Snapshot

A **Validation Reference Snapshot** is the specific identified reference state used for comparison.

A reference may be:

- an original repository commit;
- a source archive;
- an original project/solution snapshot;
- original build outputs;
- a known release;
- an expected behavior set;
- a controlled test oracle;
- another explicitly identified reference appropriate to the validation question.

A reference snapshot must be stable enough that another evaluator can understand what was compared.

Where the reference is unavailable, validation dimensions that require one may be **Not Evaluated** or **Not Comparable** rather than treated as failures.

---

# 5. Target/reference compatibility

A Validation Target and Validation Reference Snapshot must represent defensibly comparable subjects.

The Validation Run must record material context needed to interpret compatibility, including where relevant:

- application/release version;
- branch/commit/archive identity;
- target framework/runtime;
- build configuration;
- operating-system/runtime environment;
- architecture/platform;
- feature/configuration state;
- external dependency versions;
- database/schema/data assumptions;
- runtime configuration;
- test fixture or scenario version.

Known mismatches do not have to invalidate an entire Validation Run, but they must remain visible and constrain the claims that may be made.

A comparison must not silently report a mismatch caused by incompatible contexts as recovery inaccuracy.

**Governance:** VAL-005 / D088.

---

# 6. Validation Plan

Every Validation Run is governed by an explicit **Validation Plan**.

The plan is established before result interpretation and identifies:

1. Validation Target;
2. Validation Reference Snapshot, where applicable;
3. intended Validation Dimensions;
4. Validation Checks within those dimensions;
5. prerequisites;
6. comparator/method;
7. environment and material configuration;
8. expected applicability;
9. intentionally omitted dimensions/checks;
10. known comparison limitations.

A Validation Plan may be broad or narrow.

A focused plan is valid when it clearly states what it does and does not evaluate.

Validation must not retroactively select only favorable checks after results are known.

**Governance:** VAL-002 / D085.

---

# 7. Validation dimensions

Validation is multidimensional.

Applicable dimensions remain independently visible even when a later report provides summaries.

The canonical dimension families are:

## 7.1 Structural validation

Examines reconstructed development structure, for example:

- Candidate Project boundaries;
- project identities/names where comparable;
- solution composition;
- project kinds;
- target frameworks;
- source organization where comparison is defensible.

Structural validation does not imply source identity or behavioral equivalence.

## 7.2 Dependency validation

Examines dependency relationships and representations, for example:

- dependency existence;
- project-to-project relationships;
- package dependencies;
- framework/runtime dependencies;
- unresolved dependencies;
- dependency direction;
- dependency representation fidelity where historically knowable.

A compiled assembly reference does not itself prove the original project declaration mechanism.

## 7.3 Framework/runtime validation

Examines relevant reconstructed runtime/framework characteristics, for example:

- target framework/runtime;
- host/executable role;
- runtime configuration;
- framework-specific reconstruction.

## 7.4 API/type-system validation

Examines program structure preserved in metadata or source, for example:

- namespaces;
- types;
- inheritance/interface relationships;
- members;
- signatures;
- accessibility;
- generic parameters/constraints;
- attributes.

API equivalence is not implementation or behavioral equivalence.

## 7.5 Resources and configuration validation

Examines recovery of applicable non-code runtime material, for example:

- configuration sections/settings;
- binding/endpoint/service declarations;
- resources;
- localization assets;
- manifests;
- runtime options;
- other configuration-bearing artifacts.

## 7.6 Source validation

Examines recovered source against an appropriate reference while respecting source provenance classes.

Possible comparisons include:

- declarations/signatures;
- syntax/AST structure;
- normalized semantic structure;
- selected implementation equivalence;
- source file identity/layout only where reference evidence makes that comparison meaningful.

Textual similarity is not automatically semantic equivalence.

Decompiler-generated or reconstructed source is not converted into Original Source merely because it resembles or compiles like the reference.

## 7.7 Build validation

Examines operational build properties, for example:

- restore success;
- project build success;
- solution build success;
- warnings/errors;
- required manual intervention;
- build reproducibility.

Build success establishes buildability under the evaluated conditions.

It does **not** establish global behavioral equivalence or historical source identity.

## 7.8 Behavioral validation

Examines behavior under explicitly defined scenarios, for example:

- original tests executed against reconstructed code;
- controlled integration scenarios;
- request/response behavior;
- serialization/deserialization;
- persistence behavior;
- service interactions;
- runtime contracts.

Behavioral success is bounded by the scenarios, data, configuration, environment, and oracles actually evaluated.

Passing evaluated scenarios must not be generalized into untested global equivalence.

**Governance:** VAL-003 / D086; VAL-008 / D091.

---

# 8. Validation Check

A **Validation Check** is one defined comparison or test within a Validation Dimension.

Each check should identify:

- what proposition it evaluates;
- target material;
- reference material, where required;
- comparator or execution method;
- prerequisites;
- result state;
- diagnostics;
- limitations.

A check should be small enough that its result has a clear interpretation.

---

# 9. Validation Finding states

A **Validation Finding** records the outcome and diagnostic basis of a Validation Check.

At minimum, the validation model must distinguish:

- evaluated agreement/pass;
- evaluated divergence/failure;
- **Not Evaluated**;
- **Not Comparable**;
- validation execution/error state.

Exact labels may be refined by `11-recovery-metrics.md` and represented by `12-architecture.md`, provided these semantic distinctions remain.

## 9.1 Not Evaluated

**Not Evaluated** means a potentially applicable check or dimension was intentionally or practically not executed.

Examples:

- original tests were unavailable;
- behavioral validation was outside this plan;
- a required environment was not provisioned.

It is neither success nor failure.

## 9.2 Not Comparable

**Not Comparable** means the available target, reference, or context does not permit a defensible comparison.

Examples:

- reference belongs to a different release;
- historical project metadata is unavailable;
- environment differences make a behavioral comparison uninterpretable.

It is neither success nor failure.

A missing comparison opportunity must not be converted into a negative recovery score merely to make reporting numerically complete.

**Governance:** VAL-004 / D087.

---

# 10. Validation claim boundaries

Different validation dimensions establish different propositions.

Examples:

```text
Build passes
    ⇒ the evaluated reconstruction builds under the evaluated conditions
    ≠ historical source identity
    ≠ behavioral equivalence

Public API matches
    ⇒ the compared API surface is equivalent under the comparator
    ≠ implementation identity
    ≠ runtime equivalence

Source is syntactically/semantically similar
    ⇒ the compared source representation has measured similarity/equivalence
    ≠ proof that it is the original authored source

Behavioral tests pass
    ⇒ evaluated scenarios passed
    ≠ proof of all possible runtime behavior
```

Validation reports must state claims at the level actually supported.

**Governance:** VAL-008 / D091.

---

# 11. Blind validation

Controlled benchmark/evaluation work should support **Blind Validation Runs**.

In a blind run:

1. define the allowed Recovery Input;
2. withhold the Validation Reference from recovery;
3. execute recovery using only allowed recovery inputs and recorded Developer Input;
4. freeze the resulting Validation Target;
5. only then make the Validation Reference Snapshot available to validation;
6. execute the Validation Plan;
7. preserve the blind target and its findings.

This protects the evaluation from reference leakage.

The validation reference must not silently become recovery evidence.

**Governance:** D007, D062, VAL-006 / D089.

---

# 12. Reference-derived feedback

Validation may reveal information that could improve a reconstruction.

When information learned from a previously withheld Validation Reference is used to alter recovery:

- the original blind Validation Target remains unchanged;
- its Validation Run and findings remain historical records;
- the reference-derived change must not be presented as if it had been discovered from the original Recovery Input;
- the revised recovery is a new explicitly **non-blind** recovery lineage/iteration;
- the revised result becomes a new Validation Target if it is subsequently evaluated.

This allows validation to aid research and tool improvement without rewriting the history of what LegacyRevive.NET recovered blindly.

**Governance:** VAL-007 / D090.

---

# 13. Validation Run

A **Validation Run** is one execution of a Validation Plan against an identified Validation Target and, where applicable, Validation Reference Snapshot.

A completed Validation Run retains provenance sufficient to interpret and reproduce its findings.

Where relevant, record:

- Validation Run identity;
- Validation Target identity;
- Validation Reference Snapshot identity;
- Validation Plan/version;
- LegacyRevive.NET/tool version;
- comparator/tool versions;
- environment;
- configuration;
- executed checks;
- diagnostic outputs;
- timestamps;
- result state;
- limitations.

Material changes to target, reference, plan, comparator, or environment that alter interpretation create a new run rather than rewriting a completed historical run.

**Governance:** VAL-009 / D092.

---

# 14. Validation history and immutability

Validation is append-preserving.

Completed runs/findings are historical records of what was evaluated under specific conditions.

Corrections to metadata may be recorded transparently, but materially different validation conditions require a new run.

A newer Validation Run may supersede an older run for current reporting purposes without deleting the older run.

This follows the wider LegacyRevive.NET provenance and history-preservation principles.

---

# 15. Validated Baseline

A **Validated Baseline** is a recovery baseline for which an applicable Validation Plan has been executed and the resulting findings and limitations have been recorded.

Validated Baseline does **not** mean:

- all checks passed;
- all dimensions were evaluated;
- all dimensions were comparable;
- exact reproduction of the original repository;
- original source identity;
- global behavioral equivalence;
- absence of unresolved recovery questions.

A Validated Baseline means the recovery state has an explicit, interpretable validation record suitable for continued engineering decisions.

Where results are partial, divergent, blocked, Not Evaluated, or Not Comparable, those states remain visible.

**Governance:** VAL-010 / D093.

---

# 16. Relationship to the Recovery Process

`07-recovery-process.md` owns the canonical recovery stages.

Validation participates after an identified recovery state is available for evaluation.

Validation may:

- establish properties of the current target;
- expose divergences;
- reveal build/runtime problems;
- inform developer decisions;
- motivate future recovery work.

Validation findings do not silently change historical evidence categories.

Any recovery changes caused by findings follow normal recovery, intervention, provenance, and checkpoint rules.

Withheld-reference information follows the stricter boundary in §12.

---

# 17. Relationship to evidence and reconstruction

Validation material must remain epistemically distinct from original recovery evidence.

A validation comparison can establish:

> "The reconstructed API differs from the chosen reference in these members."

It does not retroactively establish:

> "The original deployed artifacts directly told us those source/project declarations."

Likewise, a successful reconstructed build is evidence about the reconstructed state, not historical evidence proving how the original repository was authored.

---

# 18. Validation corpus use

Open-source or otherwise controlled codebases may be used as validation subjects where licensing and practical constraints permit.

A controlled ground-truth workflow may be:

```text
Known original source/reference
        │
        ├── build/publish
        ▼
Allowed recovery artifacts
        │
        ▼
LegacyRevive.NET recovery
        │
        ▼
Frozen Validation Target
        │
        ▼
Reference revealed to validator
        │
        ▼
Multidimensional Validation Run
```

Research/tuning subjects and held-out validation subjects should be separated when inference rules are being evaluated, so the validation result is not merely measuring rules tuned to the same examples.

Detailed corpus metrics and benchmark aggregation belong to later metric/research work.

---

# 19. Validation strategy invariants

The following invariants are canonical:

1. validation evaluates an identified stable target;
2. every run is governed by an explicit plan;
3. validation remains multidimensional;
4. dimensions remain independently visible;
5. Not Evaluated and Not Comparable are not failures;
6. target/reference compatibility constrains interpretation;
7. blind evaluation freezes recovery before reference access;
8. validation reference does not become recovery input;
9. reference-derived repair creates a new non-blind recovery lineage;
10. build, API/source, and behavior establish different claims;
11. behavioral claims are bounded by evaluated scenarios;
12. validation findings preserve target/reference/plan/comparator/environment provenance;
13. completed validation history is not silently rewritten;
14. a Validated Baseline is not synonymous with perfect recovery.

---

# 20. Governance mapping

This canonical document resolves and implements the following registered qualifications:

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| VAL-001 | D084 | Validation targets an identified recovery checkpoint/state |
| VAL-002 | D085 | Validation uses an explicit Validation Plan |
| VAL-003 | D086 | Validation dimensions remain independently visible |
| VAL-004 | D087 | Not Evaluated / Not Comparable are distinct from failure |
| VAL-005 | D088 | Target/reference context must be compatible |
| VAL-006 | D089 | Blind recovery is frozen before reference access |
| VAL-007 | D090 | Reference-derived repair creates a new lineage |
| VAL-008 | D091 | Different validation dimensions establish different claims |
| VAL-009 | D092 | Runs/findings preserve provenance and history |
| VAL-010 | D093 | Validated Baseline does not mean perfect recovery |

No new qualification is introduced by this revision.

---

# 21. Boundary with `11-recovery-metrics.md`

This document defines **what validation means**.

`11-recovery-metrics.md` defines **how applicable validation outcomes are measured and reported**, including:

- per-dimension measurements;
- denominators and applicability;
- precision/recall-style measures where meaningful;
- treatment of unknown/not-comparable populations;
- developer-intervention measures;
- build/test/behavioral measures;
- confidence-calibration measures where appropriate;
- aggregation rules;
- presentation/reporting.

Metrics must not collapse the multidimensional strategy into one opaque score that replaces the underlying findings.

---

# 22. Governance relationship

This document is constrained by:

- `00-ai-context.md`;
- `01-purpose-and-principles.md`;
- `02-terminology.md`;
- `03-evidence-model.md`;
- `07-recovery-process.md`;
- `08-project-reconstruction.md`;
- `09-source-reconstruction.md`;
- `90-decisions.md`;
- `91-design-qualification-register.md`.

Durable validation decisions are owned by `90-decisions.md`.

Qualification history and status are owned by `91-design-qualification-register.md`.

If later work materially challenges a settled rule here, it must create a new qualification and, where appropriate, a superseding decision rather than silently rewriting governance history.