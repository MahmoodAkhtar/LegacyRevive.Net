# LegacyRevive.NET — Purpose and Principles

## Status

**Canonical**

This document defines the authoritative purpose, intended outcome, boundaries, non-goals, and foundational guiding principles of LegacyRevive.NET.

Detailed terminology, evidence semantics, recovery mechanics, artifact-specific capabilities, architecture, validation metrics, MVP scope, and implementation choices belong in their own canonical documents.

`03-evidence-model.md` now defines the formal evidence semantics that constrain these principles. Detailed evidence mechanics remain owned by that document rather than this purpose/principles document.

---

# 1. Purpose

**LegacyRevive.NET** is a developer-assistance tool for recovering and reconstructing legacy .NET applications when the original development solution, projects, or source are missing, incomplete, damaged, inaccessible, or otherwise unusable.

Its primary scenario is that a developer has surviving or deployed artifacts rather than a complete usable development codebase.

These surviving artifacts may include items such as:

- `.dll` assemblies;
- `.exe` files;
- configuration files;
- PDBs;
- XML documentation files;
- `.deps.json`;
- `.runtimeconfig.json`;
- resource assemblies;
- package/dependency metadata;
- WCF configuration;
- embedded resources;
- other runtime or build artifacts.

LegacyRevive.NET should help the developer progress from those surviving artifacts toward a **workable recovered .NET development baseline**.

---

# 2. Practical objective

The practical objective of LegacyRevive.NET is:

> **Recover enough accurate structure, source, configuration, dependency information, and supporting evidence to create a workable .NET solution that a developer can understand, build where possible, validate, repair, and continue maintaining.**

The goal is not to claim perfect reconstruction of the original repository.

A recovered solution may differ from the original while still being a successful recovery if it provides a sufficiently accurate, explainable, and useful development baseline.

---

# 3. Recovery does not mean exact reproduction

LegacyRevive.NET does **not** promise that it can always recreate:

- the exact original repository;
- the exact original solution or project files;
- the exact original project boundaries;
- the exact original source code;
- original formatting;
- original comments;
- original local-variable names;
- original file layout;
- original developer intent;
- every build-time transformation.

Some information may be directly recoverable from surviving artifacts.

Other information may only be inferable, reconstructable, or irrecoverable.

LegacyRevive.NET should preserve those distinctions rather than presenting all recovered output as equally certain.

---

# 4. Workable recovery is the target outcome

The important outcome is not historical perfection.

The important outcome is that the developer receives enough trustworthy material to move forward with the system.

A useful recovered baseline should help the developer:

- understand what survived;
- understand what was recovered;
- identify what remains uncertain;
- reconstruct enough of the system to continue investigation;
- build where possible;
- repair unresolved areas;
- validate recovered behaviour where possible;
- continue maintaining the application.

A recovery can therefore be valuable even when some original details cannot be recovered.

---

# 5. LegacyRevive.NET is a recovery assistant, not an oracle

LegacyRevive.NET should help a developer make evidence-informed recovery decisions.

It should not hide uncertainty or present inference as certainty merely to produce a more complete-looking result.

The tool should preserve visibility into:

- what is directly supported by surviving evidence;
- what was derived from that evidence;
- what was inferred;
- what was reconstructed;
- what was supplied or decided by the developer;
- what remains unresolved;
- what has been validated.

These are principle-level distinctions. Their formal taxonomy and lifecycle are owned by `03-evidence-model.md`.

An incomplete but honest recovery is preferable to a complete-looking recovery based on hidden assumptions.

---

# 6. Recovery is evidence-driven

Recovery should begin with surviving evidence.

LegacyRevive.NET should distinguish what can be observed directly from what must be inferred.

The detailed evidence taxonomy belongs in the Evidence Model, but the following principle is foundational:

> **Evidence is not the same thing as inference, and inference is not the same thing as fact.**

A plausible reconstruction must not silently become a recovered fact merely because it is useful.

---

# 7. Provenance is fundamental

Important recovered, derived, inferred, reconstructed, and developer-supplied information should retain provenance wherever practical.

The recovery process should make it possible to understand why a conclusion exists and what it depends upon.

Provenance is necessary not only for reporting but also to protect later recovery stages from mistaking reconstructed output for fresh independent evidence from the original system.

The precise formal relationship between provenance and derivation history belongs in `03-evidence-model.md`.

---

# 8. Avoid false-assumption feedback loops

LegacyRevive.NET must avoid recursive assumption amplification.

For example:

1. the tool infers that a project probably existed;
2. it reconstructs that project;
3. a later stage encounters the reconstructed project;
4. that stage must not treat the reconstructed project as new independent evidence that the project originally existed.

Generated or reconstructed material must retain enough provenance and derivation information to distinguish it from original surviving evidence.

This principle applies equally to inferred conventions, recovered structure, and later derived observations.

The exact formal mechanism for detecting and preventing circular support belongs in `03-evidence-model.md`.

---

# 9. Preserve uncertainty

When evidence is incomplete, ambiguous, or contradictory, LegacyRevive.NET should preserve that uncertainty.

It should be acceptable for the tool to report that:

- something is unresolved;
- several interpretations are plausible;
- evidence conflicts;
- more developer input is needed;
- the available evidence is insufficient for a reliable conclusion.

An explicit unknown is preferable to invented certainty.

The formal representation of conflicts, assumptions, and uncertainty belongs in `03-evidence-model.md`.

---

# 10. Developer intervention is first-class

LegacyRevive.NET is not intended to recover every system autonomously.

A developer may need to intervene to help the recovery progress.

Interventions may include resolving ambiguity, supplying missing information, selecting between plausible alternatives, confirming or rejecting an inference, repairing a reconstruction, or choosing a practical recovery path.

Developer intervention should be treated as a normal and valuable part of the process.

Developer-provided information and decisions must remain distinguishable from original surviving evidence.

The detailed intervention model belongs in `04-developer-intervention.md`.

---

# 11. Derived observations may help recovery

LegacyRevive.NET may identify patterns in surviving evidence that help the developer understand how the original system appears to have been organised.

Examples may include recurring naming, namespace, dependency, project, configuration, or coding patterns.

These patterns can be useful when making recovery interventions.

However, they are **derived observations**, not guaranteed facts about the original developers' intent.

They must retain provenance and must not become self-reinforcing evidence merely because later reconstructed material follows the inferred pattern.

The detailed convention-inference model belongs in `05-convention-inference.md`.

---

# 12. Prefer progressive recovery

Recovery should generally progress from stronger evidence toward more inferential reconstruction.

The exact recovery pipeline is not defined in this document.

The governing principle is:

> **Establish what can be supported directly before relying on weaker inference to shape reconstruction.**

The exact recovery stages and execution flow belong in `07-recovery-process.md` and related downstream canonical documents.

---

# 13. Recovery decisions should be explainable

Where LegacyRevive.NET proposes or performs a reconstruction based on inference, the reasoning should be inspectable where practical.

The developer should be able to understand:

- what evidence contributed;
- what assumptions were made;
- what remains uncertain;
- whether developer intervention influenced the result.

Prefer explainable evidence-based recovery over opaque conclusions or unexplained confidence scores.

---

# 14. Confidence should be explainable

LegacyRevive.NET should prefer explainable confidence based on evidence and provenance rather than arbitrary numeric percentages.

The precise confidence vocabulary, attachment points, and rules belong in `03-evidence-model.md`.

The foundational requirement is that confidence should be justified by evidence, not presented as unexplained mathematical precision.

---

# 15. Buildability is important but not sufficient

A recovered solution that builds successfully is an important milestone.

However:

> **Compilable does not mean behaviourally equivalent.**

Compilation alone cannot prove that the recovered system behaves the same as the original.

Buildability should therefore be treated as one important recovery and validation outcome rather than the final proof of correctness.

---

# 16. Validation should be multidimensional

Where recovery can be evaluated against a known original system, the comparison should consider multiple aspects of the recovered result rather than reducing everything to a single opaque score.

The exact validation dimensions and metrics belong in `10-validation-strategy.md` and `11-recovery-metrics.md`.

The governing principle is:

> **Different aspects of a recovered system can have different levels of fidelity, and those differences should remain visible.**

---

# 17. Validation reference must remain separate from recovery input

Open-source or otherwise known original source can be used to evaluate LegacyRevive.NET.

When doing so, the original source must remain a **validation reference**, not recovery input.

A valid evaluation should preserve the separation between:

- artifacts LegacyRevive.NET is permitted to recover from; and
- original source used afterwards to judge the result.

Allowing original source to influence recovery would invalidate the evaluation.

---

# 18. Open-source projects can support research

Open-source .NET projects may also be studied to investigate whether proposed recovery heuristics are generally useful.

This can help evaluate ideas about structure, naming, dependencies, project boundaries, or other recurring patterns.

A heuristic learned from such research remains a heuristic when applied to an individual recovery target.

Research evidence must not be confused with direct evidence from the target being recovered.

---

# 19. Reuse mature .NET capabilities where appropriate

LegacyRevive.NET should prefer to reuse mature .NET ecosystem capabilities when they materially help the recovery problem.

Relevant capability areas include metadata inspection, decompilation, source analysis, package inspection, build tooling, symbol reading, and resource inspection.

Specific library and technology choices are implementation decisions and belong in `13-tooling-and-dependencies.md`.

The principle here is simply:

> **Do not reimplement mature low-level capabilities without a good reason.**

---

# 20. Preserve original artifacts

Original surviving artifacts are recovery evidence and should be treated as immutable inputs.

LegacyRevive.NET should not overwrite or mutate them.

Recovered and reconstructed output should be produced separately so that the original evidence remains available for repeatability, inspection, validation, and comparison.

---

# 21. Prefer deterministic recovery

Given the same recovery inputs, tool version, configuration, and recorded developer interventions, the substantive recovery result should be reproducible wherever practical.

Determinism supports:

- repeatability;
- debugging;
- validation;
- comparison;
- regression testing;
- trust in the recovery process.

---

# 22. Do not confuse recovered conventions with original intent

Patterns may be visible in surviving evidence.

LegacyRevive.NET may surface those patterns because they can help the developer make consistent interventions.

However:

> **Observed convention does not prove original developer intent.**

The tool should describe what the surviving evidence appears to show, not claim knowledge of motivations that the artifacts cannot establish.

---

# 23. Relationship to the wider Legacy tool family

LegacyRevive.NET is conceptually complementary to other legacy-development tools.

## LegacyLens.NET

LegacyLens.NET starts from an existing source codebase and helps developers understand and investigate legacy .NET code.

## LegacyRevive.NET

LegacyRevive.NET starts from surviving/deployed artifacts when the original development representation is missing or unusable and helps reconstruct a workable development baseline.

## LegacySafety.NET

LegacySafety.NET is a related future concept focused on quickly establishing regression or characterization-test protection around legacy code.

These tools may eventually complement one another, but their responsibilities should remain distinct.

---

# 24. Non-goals and non-promises

LegacyRevive.NET should not claim:

- exact recreation of the original repository in every case;
- guaranteed recovery of original source;
- guaranteed recovery of original project boundaries;
- guaranteed reconstruction of original developer intent;
- behavioural equivalence merely because recovered code compiles;
- that inference is equivalent to fact;
- that developer intervention can always be eliminated;
- that uncertainty can always be resolved;
- scientific precision from arbitrary confidence percentages.

The tool should not fill irrecoverable gaps with unlabelled invention.

---

# 25. Principles for evaluating new capabilities

When considering a new LegacyRevive.NET capability, ask:

1. What recovery problem does this solve for the developer?
2. What surviving evidence could support it?
3. What can be established directly?
4. What must be inferred?
5. Can provenance be retained?
6. What uncertainty remains?
7. Could it create a false-assumption feedback loop?
8. Could developer intervention improve the result?
9. How could the result be validated?
10. Does it materially advance the developer toward a workable recovered solution?
11. Does mature .NET tooling already solve part of the problem?
12. Does it belong in the MVP, a later phase, or research?

Technical possibility alone is not sufficient justification for adding a capability.

---

# 26. Guiding principles summary

LegacyRevive.NET should consistently follow these principles:

1. **Recovery is evidence-driven.**
2. **Evidence, inference, reconstruction, and developer input remain distinct.**
3. **Provenance is preserved.**
4. **Uncertainty is explicit.**
5. **Generated reconstruction must not become independent original evidence.**
6. **Developer intervention is expected and supported.**
7. **Derived conventions are observations, not guaranteed intent.**
8. **Recovery progresses from stronger evidence toward weaker inference.**
9. **The target is a workable recovered development baseline, not fictional historical perfection.**
10. **Compilation is an important milestone, not proof of behavioural equivalence.**
11. **Validation should preserve multiple dimensions of fidelity.**
12. **Validation reference remains separate from recovery input.**
13. **Mature ecosystem capabilities should be reused where appropriate.**
14. **Original artifacts remain immutable.**
15. **Recovery should be deterministic where practical.**
16. **Recovery reasoning should be explainable.**
17. **Explicit uncertainty is preferable to invented certainty.**

---

# 27. Canonical ownership

This document owns the authoritative definition of:

- LegacyRevive.NET's purpose;
- target recovery problem;
- practical product objective;
- broad definition of successful recovery;
- non-goals and non-promises;
- foundational recovery principles.

It intentionally does **not** define:

- the formal terminology model;
- evidence classes and confidence rules;
- detailed developer-intervention semantics;
- detailed convention-inference rules;
- artifact-by-artifact recoverability;
- exact recovery stages or pipeline;
- validation metrics;
- technical architecture;
- tooling/library selection;
- MVP feature scope;
- CLI behaviour.

Those belong in their respective canonical documents.


---

# 28. Governance relationship

This document is constrained by the accepted decisions in `90-decisions.md`.

Qualification state and audit history are controlled by `91-design-qualification-register.md`.

The earlier audit findings `DOC-001` through `DOC-006` remain part of the permanent qualification history.

`EVID-001` through `EVID-010` have now been resolved by `03-evidence-model.md`. `EVID-009` establishes the evidence-history semantics for developer correction while `04-developer-intervention.md` retains ownership of detailed intervention workflow.

Future changes to this document must follow the normative governance lifecycle in `91-design-qualification-register.md`.
