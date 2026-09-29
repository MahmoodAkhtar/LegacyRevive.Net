# LegacyRevive.NET — Recovery Process

## Status

**Canonical**

This document defines the authoritative end-to-end LegacyRevive.NET recovery process.

It coordinates the canonical semantics already defined by:

- `03-evidence-model.md`;
- `04-developer-intervention.md`;
- `05-convention-inference.md`;
- `06-recovery-matrix.md`.

It defines recovery stages, iteration, stage outputs, intervention points, reevaluation, reconstruction feedback, build/diagnostic feedback, and validation placement.

It does not define detailed project-reconstruction algorithms, source-reconstruction algorithms, validation metrics, architecture, tooling, or MVP scope.

---

# 1. Purpose

LegacyRevive.NET recovery is a controlled progression from surviving artifacts toward a workable recovered development baseline.

The process must preserve:

- original artifact immutability;
- evidence/inference distinctions;
- provenance;
- intervention history;
- convention scope;
- artifact capability limits;
- dependency-driven reevaluation;
- deterministic replay where practical.

The governing process rule is:

> **Recovery progresses from stronger directly supported information toward weaker inference and reconstruction, while allowing explicit, provenance-preserving iteration whenever new evidence, intervention, diagnostics, or validation changes the current state.**

---

# 2. Canonical process shape

The recovery process is stage-oriented but iterative.

```text
01 Intake and Preservation
        ↓
02 Artifact Inventory and Classification
        ↓
03 Direct Extraction and Normalization
        ↓
04 Relationship and Evidence Integration
        ↓
05 Convention and Inferential Enrichment
        ↓
06 Recovery Planning and Intervention
        ↓
07 Candidate Reconstruction
        ↓
08 Build and Diagnostic Evaluation
        ↓
09 Recovery Validation
        ↓
10 Workable Recovery Baseline

At any stage:
new evidence / correction / invalidation / diagnostic / validation finding
        ↓
dependency-aware reevaluation
        ↓
return to the earliest materially affected stage
```

This is a conceptual process model.

`12-architecture.md` may implement these responsibilities through commands, services, jobs, workflows, or another execution structure without changing their semantic boundaries.

---

# 3. Stage 01 — Intake and Preservation

## Objective

Establish the recovery target and preserve supplied recovery inputs before analysis.

## Required behavior

The stage must:

- discover the supplied recovery-input source sufficiently to identify supplied artifact instances;
- identify the supplied artifact set to the extent actually established by that discovery;
- preserve original artifacts without modification;
- establish stable artifact identity inside the Recovery Workspace only for supplied files actually discovered and admitted as artifact instances;
- record source/path/capture context where available;
- distinguish Recovery Input from Validation Reference;
- record Recovery Configuration separately from Evidence.

Stage 01 distinguishes three responsibilities:

```text
supplied-source discovery
        ↓
Artifact admission
        ↓
artifact preservation
```

A failure in supplied-source discovery is not automatically an Artifact-preservation failure. Where a directory or subtree cannot be enumerated, LegacyRevive.NET must not fabricate Artifact identities or claims about files that were never discovered.

If the supplied root cannot be enumerated sufficiently to establish a trustworthy supplied artifact set, the Stage 01 discovery scope is **Blocked**. A failed root enumeration must not be represented as a successfully identified empty artifact set, and no Recovery Intake Snapshot is produced as though complete discovery had succeeded. A Recovery Run and scoped discovery diagnostic may still be persisted where the workspace/run infrastructure has been established.

If a narrower subtree cannot be enumerated while other files have been safely discovered, trustworthy discovered state may be retained where safe. The failed discovery scope is recorded as Blocked, the overall run may be Partial, and admitted artifacts continue through ordinary preservation. Any Recovery Intake Snapshot produced in this condition must explicitly record that supplied-source discovery was incomplete and the known failed scope or diagnostic context. Such a snapshot describes the artifact instances actually admitted; it does not claim that they are proven to be every file beneath the supplied directory tree.

Positive file discovery is independently trustworthy once the applicable discovery operation has successfully and completely identified the concrete supplied file instance. A later failure while discovering possible child-directory or descendant scopes from the same known directory does not retroactively invalidate those positively discovered files merely because both operations occurred while processing one directory. The files remain eligible for Artifact admission/preservation, while the descendant-discovery failure makes the affected scope incomplete/Blocked and may make the overall run Partial. This applies at the supplied root as well as within a nested directory when a trustworthy supplied Artifact subset has already been positively established.

A root with retained positive file discovery and failed descendant discovery is therefore distinct from a root that cannot be enumerated sufficiently to establish any trustworthy supplied Artifact set. Conversely, successful enumeration that establishes zero direct files does not by itself justify an incomplete empty Recovery Intake Snapshot when descendant discovery subsequently fails; without other trustworthy admitted Artifact state, the intake remains Blocked because possible descendant contents are still unknown.

A file that was individually discovered and admitted but later becomes unreadable or fails byte preservation remains an Artifact-scoped preservation failure. That condition is distinct from inability to discover possible contents beneath an unenumerable source scope.

## Output

A **Recovery Intake Snapshot** describing immutable admitted recovery inputs, discovery completeness, applicable discovery diagnostics/blockers, and run context when a trustworthy snapshot can be established.

A completely enumerated empty directory may therefore produce a valid empty Recovery Intake Snapshot. An unenumerable root must not be treated as semantically equivalent to that known-empty condition.

## Boundary

No project/source reconstruction occurs here.

Stage 01 does not require a richer filesystem-object model merely to represent discovery failure. Directory/subtree scope and diagnostics are sufficient unless later governance establishes a broader capability.

**Governance:** PROC-010 / D170; PROC-011 / D171.

---

# 4. Stage 02 — Artifact Inventory and Classification

## Objective

Determine what artifacts are present and what formats they actually represent.

## Required behavior

For each supplied artifact:

- record path/identity;
- detect artifact format/type;
- distinguish extension from actual format;
- classify the artifact family using `06-recovery-matrix.md`;
- record corrupt, unreadable, unsupported, or ambiguous cases;
- identify possible companion-artifact relationships without silently accepting them.

## Output

Artifact-Instance Profiles.

---

# 5. Stage 03 — Direct Extraction and Normalization

## Objective

Extract the strongest directly available information before applying weaker inference.

## Required behavior

The stage should:

- extract direct Observations permitted by the artifact format;
- normalize direct information deterministically where appropriate;
- create Recovered Facts only where the `03-evidence-model.md` direct-support rule is satisfied;
- retain provenance to source/location;
- preserve parse/extraction diagnostics;
- avoid reconstruction where information is not directly present.

## Output

A provenance-bearing direct evidence state.

---

# 6. Stage 04 — Relationship and Evidence Integration

## Objective

Integrate information across artifacts without collapsing provenance.

## Required behavior

The stage may:

- establish defensible companion-artifact relationships;
- correlate identities across artifacts;
- detect consistency or conflict;
- create cross-artifact Recovered Facts where direct entailment is satisfied;
- create explicit Conflicts where necessary;
- maintain support-dependency relationships.

## Boundary

Correlation does not itself reconstruct original project/source structure.

---

# 7. Stage 05 — Convention and Inferential Enrichment

## Objective

Derive useful patterns and inferences after stronger direct information is established.

## Required behavior

The stage may create:

- Derived Observations;
- Convention Observations;
- Inferences;
- inferential support-strength assessments;
- unresolved recovery questions.

It must preserve:

- scope;
- exceptions;
- support dependencies;
- circularity protections;
- distinction between recurring convention and historical intent.

This implements the stronger-evidence-first principle.

---

# 8. Stage 06 — Recovery Planning and Intervention

## Objective

Determine what can proceed, what remains unresolved, and where deliberate developer participation is useful.

## Required behavior

The stage should expose:

- unresolved recovery questions;
- material conflicts;
- weak/tentative reconstruction choices;
- missing dependencies/configuration;
- intervention opportunities;
- material recovery choices.

Committed interventions follow `04-developer-intervention.md` and become replayable recovery state.

## Output

A **Recovery Plan** describing what reconstruction may proceed from current recovery state.

The Recovery Plan is derived state, not Original-System Evidence.

---

# 9. Stage 07 — Candidate Reconstruction

## Objective

Create candidate development representations from the current recovery state.

Candidate reconstruction may include:

- solution structure;
- project structure;
- project/package/assembly references;
- source structure;
- recovered/decompiled source;
- configuration;
- resources;
- build files.

Detailed project reconstruction belongs to `08-project-reconstruction.md`.

Detailed source reconstruction belongs to `09-source-reconstruction.md`.

## Required behavior

Every material reconstructed choice must retain provenance to its supporting:

- Recovered Facts;
- Inferences;
- Convention Observations;
- Assumptions;
- Developer Decisions;
- other admissible dependencies.

Reconstruction does not become independent Original-System Evidence.

---

# 10. Stage 08 — Build and Diagnostic Evaluation

## Objective

Use buildability and development diagnostics as recovery feedback.

Where the candidate can be evaluated, record:

- build success/failure;
- compiler errors;
- missing references;
- unresolved types/members;
- configuration/resource issues;
- framework/runtime incompatibilities;
- other reproducible diagnostics.

## Epistemic boundary

A build diagnostic is direct information about:

> **the current candidate reconstruction and its environment**

not automatically direct information about:

> **the exact historical original system.**

A diagnostic may support an Inference that recovery state is incomplete.

It does not by itself prove the exact original project declaration.

Build success remains an important milestone, not proof of behavioural equivalence.

---

# 11. Stage 09 — Recovery Validation

## Objective

Evaluate the current recovery result.

Validation may include:

- structural checks;
- dependency checks;
- configuration checks;
- buildability;
- behavioural checks where available;
- controlled Validation References where permitted.

Detailed methodology belongs to `10-validation-strategy.md`.

Detailed metrics belong to `11-recovery-metrics.md`.

## Validation Reference rule

A Validation Reference may evaluate recovery but remains outside Recovery Input.

A validation finding may trigger reevaluation.

The withheld reference itself must not be converted into Evidence or Developer Input to repair the recovery result.

---

# 12. Stage 10 — Workable Recovery Baseline

## Objective

Establish a coherent recovery state suitable for continued engineering work.

A Workable Recovery Baseline may be declared when:

- material current-state dependencies are recorded;
- known unresolved items remain explicit;
- intervention history is persisted;
- reconstructed artifacts are provenance-linked;
- known Invalidated items are not presented as Current;
- relevant build/validation status is recorded.

A workable baseline does not require:

- exact historical reproduction;
- zero unresolved items;
- behavioural equivalence;
- complete source recovery.

---

# 13. Stage outputs and persistence

Material stage outputs should be persisted sufficiently to support:

- provenance;
- replay;
- comparison;
- debugging;
- reevaluation;
- reporting.

Not every transient in-memory object requires persistence.

State that materially influences later recovery conclusions or reconstruction must remain traceable.

---

# 14. Stage progression does not promote epistemic category

Moving information through later stages does not change what kind of information it is.

For example:

```text
Inference
    ↓
used during reconstruction
    ↓
Candidate project choice
```

does not become:

```text
Recovered Fact
```

merely because later stages consumed it.

Process progression is not evidence promotion.

---

# 15. Iteration

Recovery is iterative.

The process may revisit earlier stages when:

- new artifacts appear;
- artifact relationships are corrected;
- new direct information is extracted;
- Assumptions are invalidated;
- Developer Interventions are withdrawn/superseded;
- Convention Observations require reevaluation;
- build diagnostics expose reconstruction defects;
- validation exposes mismatches.

Iteration is expected, not exceptional.

---

# 16. Earliest-affected-stage rule

When recovery state changes, return to the **earliest materially affected stage**.

Do not blindly continue from stale downstream state.

Do not discard unaffected trustworthy work unnecessarily.

Examples:

```text
New PDB added
→ Stage 02 classification
→ Stage 03 extraction
→ downstream reevaluation as required

Developer Decision withdrawn
→ Stage 06 planning/intervention changes
→ affected reconstruction/build/validation reevaluated

Build diagnostic exposes missing reconstructed reference
→ Stage 08 diagnostic
→ return to Stage 06 or 07 depending on the required change
```

---

# 17. Dependency-driven reevaluation

If a material upstream dependency changes:

1. retain historical state;
2. identify dependent current items;
3. mark affected items `Needs Reevaluation`;
4. recompute from the earliest affected stage where practical;
5. mark items `Invalidated` where valid support is lost;
6. create superseding current items rather than rewriting history.

This applies to material:

- Inferences;
- Convention Observations;
- Recovery Plans;
- reconstruction decisions;
- reconstructed artifacts;
- diagnostic/validation conclusions.

---

# 18. New artifact intake after recovery begins

New Original Artifacts added after recovery starts must re-enter through Intake and Inventory/Classification.

This ensures:

- identity is recorded;
- provenance is preserved;
- format is classified;
- direct information is extracted before inference;
- downstream effects are traceable.

Unaffected prior work may remain current.

---

# 19. Developer intervention points

Developer Intervention is not restricted to one fixed stage.

It may occur wherever an explicit recovery question exists, including:

- artifact classification;
- artifact relationship ambiguity;
- evidence conflict;
- convention scope;
- project-boundary alternatives;
- dependency selection;
- source reconstruction ambiguity;
- build repair choices.

Every committed intervention must obey `04-developer-intervention.md`.

Hidden intervention channels are not permitted.

---

# 20. Assumptions in the process

An Active Assumption may allow recovery to proceed where evidence is insufficient.

The process must:

- make it explicit;
- retain scope;
- record dependents;
- reevaluate dependents when it is Superseded or Invalidated.

Stage completion does not convert an Assumption into fact.

---

# 21. Recovery Checkpoints

A **Recovery Checkpoint** is an identified reproducible state of a Recovery Workspace at a meaningful point.

Checkpoints may be useful after:

- direct extraction;
- committed intervention;
- candidate reconstruction;
- successful build;
- validation milestone;
- baseline declaration.

A checkpoint supports replay/comparison but does not replace provenance/history.

---

# 22. Recovery Workspace and Recovery Run

A **Recovery Workspace** is the durable context containing:

- recovery inputs;
- evidence-model state;
- intervention history;
- reconstruction state;
- diagnostics;
- related recovery outputs.

A **Recovery Run** is one execution through some or all applicable stages using a particular:

- workspace state;
- tool version;
- configuration;
- active intervention set.

One workspace may contain many runs.

---

# 23. Deterministic replay

Given the same:

- immutable original input set;
- checkpoint/state;
- tool version;
- configuration;
- active committed interventions;

a Recovery Run should produce the same substantive outputs wherever practical.

Where nondeterminism cannot be avoided, it should be recorded sufficiently for diagnosis.

---

# 24. Partial Recovery

Recovery may be useful even when a stage, capability, scope, or later stage cannot complete.

Examples:

- Stage 01 safely discovers and preserves some supplied files while a narrower supplied subtree cannot be enumerated;
- inventory succeeds but reconstruction does not;
- structure is recovered but source is incomplete;
- reconstruction exists but build fails;
- build succeeds but behavioural validation is unavailable.

Trustworthy completed work remains usable. Partial state must not hide the scope that remains incomplete or Blocked.

---

# 25. Blocked Recovery

A recovery question, stage, capability, or bounded scope is **Blocked** when progress requires unavailable information, intervention, access, or capability.

Blocked state should identify:

- what is blocked;
- why;
- missing dependency/access/capability;
- whether intervention could unblock it;
- unaffected usable recovery state.

A narrower Blocked scope may coexist with a Partial overall Recovery Run. For example, one supplied subtree may be Blocked during Stage 01 discovery while safely discovered sibling artifacts remain usable current state.

Blocked does not prove that the original system is unrecoverable or that undiscovered material exists in any particular form.

---

# 26. Failure handling

Discovery, parser, tool, preservation, build, or validation failure must not automatically invalidate unrelated recovery state.

Failures should be:

- recorded;
- scoped to the narrowest reliably established stage/capability/source/item boundary;
- linked to an Artifact only when an Artifact instance actually exists;
- propagated only where materially relevant.

Where no Artifact identity exists because supplied-source discovery failed before a file was discovered/admitted, the diagnostic remains intake/source/directory/subtree scoped rather than fabricating an Artifact-scoped failure.

Prefer partial progress with explicit diagnostics over all-or-nothing failure where trustworthy state can be retained. Do not preserve partial progress by overstating discovery completeness.

---

# 27. Recovery completion states

LegacyRevive.NET does not define one universal "100% recovered" state.

Canonical process states include:

- **Partial**
- **Blocked**
- **Workable Baseline**
- **Validated Baseline**

A **Validated Baseline** is a Workable Baseline that has undergone applicable validation and records the resulting validation status.

It does not imply perfect historical identity unless a defined validation dimension establishes that.

---

# 28. Relationship to project reconstruction

`08-project-reconstruction.md` participates primarily in Stage 07.

It must consume the recovery state produced by prior stages and preserve:

- provenance;
- current-state dependencies;
- intervention dependencies;
- candidate status;
- reevaluation rules.

---

# 29. Relationship to source reconstruction

`09-source-reconstruction.md` participates primarily in Stage 07 and subsequent build/validation feedback.

It must distinguish directly preserved source-derived information from decompiler-generated, inferred, reconstructed, or developer-adjusted source.

---

# 30. Relationship to validation

`10-validation-strategy.md` and `11-recovery-metrics.md` own detailed validation semantics.

This document owns only:

- validation placement in the process;
- validation-triggered reevaluation;
- Validation Reference separation;
- recording validation state in baseline status.

---

# 31. Qualifications resolved by this document

| Qualification | Resolution |
|---|---|
| `PROC-001` | Recovery is stage-oriented but iterative, using a canonical conceptual sequence from intake through baseline. |
| `PROC-002` | Stage progression does not change epistemic category or promote inference into fact. |
| `PROC-003` | Material state changes trigger dependency-driven reevaluation from the earliest materially affected stage. |
| `PROC-004` | New artifacts enter through intake/classification even after recovery has begun; unaffected state may remain. |
| `PROC-005` | Developer Intervention may occur at multiple explicit process points rather than one fixed intervention stage. |
| `PROC-006` | Build diagnostics are direct information about the candidate/environment and recovery feedback, not direct historical evidence about the original system. |
| `PROC-007` | Recovery distinguishes durable Recovery Workspace, individual Recovery Run, and Recovery Checkpoint. |
| `PROC-008` | Recovery supports Partial, Blocked, Workable Baseline, and Validated Baseline states. |
| `PROC-009` | Validation findings may trigger reevaluation, but Validation References remain outside Recovery Input. |
| `PROC-010` | Stage 01 distinguishes supplied-source discovery from Artifact preservation; incomplete enumeration preserves safe known state without fabricating Artifacts or false completeness. |
| `PROC-011` | Positive supplied-file discovery remains trustworthy when a later descendant-discovery operation for the same directory fails; the later failure narrows completeness rather than erasing established file state. |

---

# 32. Durable design consequences

1. Recovery has a canonical conceptual stage sequence.
2. The process is iterative rather than strictly linear.
3. Stronger direct information precedes weaker inference by default.
4. Stage progression never promotes epistemic category.
5. Dependency changes trigger scoped reevaluation.
6. New artifacts re-enter through intake/classification.
7. Developer Intervention is available at explicit decision points throughout recovery.
8. Build diagnostics feed recovery without becoming historical fact.
9. Recovery Workspaces persist across multiple Recovery Runs.
10. Checkpoints support reproducibility/comparison.
11. Partial progress remains useful.
12. Blocked state is explicit and scoped.
13. Validation feedback is separated from Validation Reference leakage.
14. Stage 01 discovery incompleteness remains explicit: root discovery failure cannot masquerade as a known-empty snapshot, while safe partial subtree progress may be retained without fabricating undiscovered Artifacts.
15. A workable baseline does not imply exact reproduction or behavioural equivalence.

---

# 33. Non-goals

This document does not define:

- exact CLI commands;
- orchestration technology;
- persistence implementation;
- project reconstruction algorithms;
- source decompilation algorithms;
- build-engine integration;
- validation metrics;
- UI;
- MVP stage subset.

---

# 34. Canonical ownership

This document owns:

- recovery stage responsibilities;
- process ordering semantics;
- iteration/reevaluation flow;
- late-artifact re-entry;
- process-level intervention placement;
- build-diagnostic feedback semantics;
- Recovery Workspace / Recovery Run / Recovery Checkpoint;
- partial/blocked/baseline process states;
- Stage 01 supplied-source discovery, discovery-completeness, and discovery-failure semantics.

`03`–`06` own the semantic models consumed by this process.

`08`–`11` own detailed reconstruction and validation behavior.

`12-architecture.md` owns implementation architecture for executing the process.

---

# 35. Governance relationship

This document is constrained by accepted decisions in `90-decisions.md`.

Its qualification history is recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
