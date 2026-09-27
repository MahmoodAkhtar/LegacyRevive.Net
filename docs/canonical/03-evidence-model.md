# LegacyRevive.NET — Evidence Model

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET model for:

- recovery evidence;
- observations;
- recovered facts;
- inferences;
- assumptions;
- provenance and derivation lineage;
- confidence/support semantics;
- conflicting evidence;
- developer-supplied recovery information;
- correction and supersession;
- circular-evidence protection.

It resolves the Evidence Model qualifications `EVID-001` through `EVID-010`.

Detailed developer-intervention workflow and interaction semantics remain owned by `04-developer-intervention.md`.

---

# 1. Purpose

LegacyRevive.NET reconstructs development information from incomplete surviving material.

That process inevitably combines:

- information directly available from the target system;
- deterministic extraction and normalization;
- inference;
- assumptions;
- developer input;
- reconstruction.

The Evidence Model exists so those categories do not collapse into one another.

Its central rule is:

> **Every material recovery conclusion must remain explainable in terms of what supports it, where that support came from, and what transformations or decisions produced it.**

The model must make it possible to answer questions such as:

- Is this conclusion directly supported or inferred?
- Which original artifacts support it?
- Did developer input influence it?
- Does it depend on an assumption?
- Was it superseded by a later conclusion?
- Is it affected by conflicting evidence?
- Is any apparent corroboration actually circular?
- What support strength is being claimed, and why?

---

# 2. Top-level recovery-input taxonomy

The recovery boundary distinguishes **Recovery Input** from **Validation Reference**.

Recovery Input contains three distinct top-level categories:

1. **Evidence**
2. **Developer Input**
3. **Recovery Configuration**

**Validation Reference** is deliberately outside Recovery Input and outside the recovery support graph.

The important distinction is:

```text
Recovery Input
├── Evidence
│   ├── Original-System Evidence
│   └── Independent Contextual Evidence
├── Developer Input
└── Recovery Configuration

Validation Reference
└── outside the recovery process
```

## 2.1 Evidence

**Evidence** is non-generated information permitted to support a conclusion about the recovery target.

Evidence has two important source kinds.

### Original-System Evidence

Information surviving from the target system itself.

Examples may include:

- assemblies;
- executables;
- configuration;
- PDBs;
- XML documentation;
- resources;
- runtime metadata;
- package metadata preserved with the deployed system.

### Independent Contextual Evidence

Information obtained independently of the recovery process that may legitimately help interpret the target.

An example could be independently retrieved package metadata for a package identity observed in an original artifact.

Independent contextual evidence must remain distinguishable from Original-System Evidence.

External Enrichment that is explicitly enabled and admitted to recovery is represented as Independent Contextual Evidence. It remains distinct from **Supplied Recovery Input**, and its source/enrichment state must remain explicit.

It must never be described as though it survived from the target when it did not.

## 2.2 Developer Input

Developer-provided recovery information is **not Evidence** in the formal LegacyRevive.NET taxonomy.

It is a separate top-level recovery-input category.

Developer Input may include:

- Developer Assertions;
- Developer Decisions;
- Developer Corrections.

Developer Input may influence recovery conclusions, but its provenance must identify it as human-supplied rather than original-system evidence.

This separation prevents a developer assertion from later being reported as something discovered from the target artifacts.

## 2.3 Recovery Configuration

Recovery Configuration controls how a recovery run is performed.

Examples may eventually include selected targets, allowed evidence sources, or recovery options.

Configuration may influence processing but is not evidence about what historically existed unless some separately represented evidence supports the same proposition.

## 2.4 Validation Reference

A Validation Reference is outside the recovery-input graph.

It may be used after recovery to evaluate results, but it must not contribute support to recovery conclusions.

This preserves controlled evaluation.

---

# 3. Evidence sources, observations, and recovered facts

## 3.1 Evidence Source

An Evidence Source identifies the origin from which evidence-bearing information is obtained.

At minimum its provenance must distinguish:

- source identity;
- source kind;
- whether it is Original-System Evidence or Independent Contextual Evidence;
- relevant location within the source where practical.

## 3.2 Observation

An **Observation** is a directly extracted or read item from Evidence.

An Observation records **what was observed**, not a wider conclusion about what probably existed.

Examples:

- assembly metadata contains type `A.B.CustomerService`;
- configuration contains an endpoint with binding `netTcpBinding`;
- a PDB maps a method token to a source document path.

An Observation may normalize representation where that normalization is deterministic and semantics-preserving.

An Observation does not depend on:

- a recovery assumption;
- a heuristic inference;
- a developer assertion;
- a reconstructed artifact.

## 3.3 Recovered Fact

A **Recovered Fact** is a normalized proposition that is directly entailed by one or more admissible Observations.

A Recovered Fact may combine multiple observations where the combination is deterministic and introduces no inferential leap.

A proposition is **not** a Recovered Fact if accepting it requires:

- choosing between plausible explanations;
- applying a heuristic;
- assuming missing information;
- relying on a Developer Assertion;
- inferring historical intent;
- treating generated reconstruction as independent support.

Therefore:

```text
Evidence Source
    ↓
Observation
    ↓ deterministic / semantics-preserving normalization
Recovered Fact
```

Not every Observation needs to become a Recovered Fact.

Observations may remain useful in their raw form.

A Recovered Fact means **directly supported**, not **globally unquestionable**. If another directly supported proposition conflicts with it, both are retained and the conflict is represented explicitly.

---

# 4. Derived observations and inferences

## 4.1 Derived Observation

A **Derived Observation** is a deterministic or descriptive aggregation over earlier evidence-model items that does not itself assert a new unsupported historical conclusion.

Examples may include:

- repeated namespace prefixes;
- a count of references to a package;
- a recurring naming pattern;
- a dependency pattern calculated from observed metadata.

Derived Observations must retain dependency links to the items from which they were derived.

They may support later inference, subject to the anti-circularity rules in this document.

## 4.2 Inference

An **Inference** is a conclusion that goes beyond what is directly entailed by the available Observations.

An inference may depend on:

- Recovered Facts;
- Observations;
- Derived Observations;
- Developer Input;
- Assumptions;
- Recovery Heuristics;
- earlier Inferences, where dependency rules allow it.

An Inference must record enough provenance to explain its support path.

Inference is not failure.

It is an expected part of recovery, but it must remain distinguishable from direct support.

---

# 5. Conclusion identity, evolution, and promotion

Evidence-model records are historically stable.

LegacyRevive.NET should not mutate an earlier Inference into a Recovered Fact in place.

If new independent evidence later establishes directly what had previously only been inferred:

1. retain the original Inference;
2. create a new Recovered Fact;
3. link the new conclusion to the earlier one using supersession/history metadata;
4. mark the earlier conclusion as superseded for current-state purposes;
5. preserve the original support path and the new support path separately.

This yields:

```text
Inference v1
    │
    │ later independent evidence appears
    ↓
Recovered Fact v2
    └── supersedes → Inference v1
```

The historical record therefore answers both:

- what the system believed earlier; and
- why the later classification became stronger.

Classification changes are represented by **new conclusions plus supersession**, not by rewriting historical records.

---

# 6. Provenance and lineage

**Provenance** is the umbrella concept for traceability.

It contains both:

1. **origin provenance** — where an item came from; and
2. **derivation lineage** — how it was produced from earlier items.

Lineage is therefore **part of Provenance**, not a separate top-level evidence concept.

A material evidence-model item should be traceable through provenance to the relevant origins.

For derived items, provenance should include dependency relationships such as:

```text
Evidence Source
    ↓
Observation
    ↓
Recovered Fact
    ↓
Inference
    ↓
Candidate Reconstruction
```

Developer Input and Assumptions appear explicitly in that graph where they contributed.

The provenance model must never disguise:

- generated output as original evidence;
- developer input as artifact-derived evidence;
- validation reference as recovery input.

---

# 7. Conflicting evidence

Conflicting evidence is represented explicitly rather than resolved by silent source preference.

A **Conflict** records:

- the incompatible propositions or conclusions;
- the support/provenance paths for each;
- the current conflict state;
- any resolution or developer decision that later affects it.

A conflict may be:

- **Open** — no resolution has been adopted;
- **Resolved** — a deliberate resolution has been recorded;
- **Superseded** — a later conflict record replaces the earlier framing.

Resolving a conflict does not delete the losing or superseded evidence.

The original evidence and conclusions remain in history.

Where conflicting Recovered Facts exist, downstream work must treat them as contested until the conflict has been deliberately handled.

A conflict may be resolved by:

- new independent evidence;
- correction of an earlier extraction or interpretation;
- a developer decision where recovery must proceed despite remaining ambiguity;
- recognition that the propositions are not actually incompatible.

A developer decision that selects one recovery path does not retroactively prove that the selected path was historically correct.

---

# 8. Assumptions

An **Assumption** is an explicit proposition temporarily accepted so recovery can proceed without sufficient evidence to establish it as a Recovered Fact.

An Assumption must record:

- its proposition;
- why it is needed;
- its scope;
- the items that depend on it;
- its current state.

The canonical Assumption states are:

- **Active** — currently permitted to influence recovery;
- **Superseded** — replaced by a later assumption or conclusion;
- **Invalidated** — evidence or correction shows it should no longer be relied upon;
- **Retired** — no longer required, without implying it was disproved.

An Assumption never silently becomes a Recovered Fact.

If later evidence directly establishes the proposition:

- create the new Recovered Fact;
- retire or supersede the Assumption;
- preserve both records and their relationship.

If an Assumption becomes Invalidated, downstream conclusions that materially depend on it must be marked for reevaluation and must not continue to be presented as current without review.

---

# 9. Confidence and support strength

LegacyRevive.NET does not assign confidence percentages merely to create an appearance of precision.

The model uses a small semantic vocabulary for **inferential support strength**:

- **Strongly Supported**
- **Plausible**
- **Tentative**

These labels apply to inferential conclusions, including inferential reconstruction choices.

They do not apply to raw Evidence Sources or Observations.

## 9.1 Strongly Supported

Use when the inference has substantial support and no known material conflict undermines the adopted conclusion.

Typical reasons may include:

- multiple independent support paths;
- one highly specific support path;
- convergence of different evidence kinds;
- absence of critical unsupported assumptions.

## 9.2 Plausible

Use when there is meaningful support for the inference but important uncertainty remains.

Typical reasons may include:

- limited evidence;
- credible alternatives;
- dependence on non-critical assumptions;
- incomplete corroboration.

## 9.3 Tentative

Use when the inference is useful enough to surface or provisionally use but support is weak, indirect, assumption-heavy, or materially contested.

## 9.4 Unresolved is not a confidence level

**Unresolved** is a recovery state, not a confidence label.

Where no conclusion should currently be adopted, the system should remain Unresolved rather than assigning a low-confidence answer merely to appear complete.

## 9.5 Mandatory explanation

A support-strength label is incomplete without a rationale that identifies the relevant support and uncertainty.

The label is shorthand for a traceable assessment, not a substitute for provenance.

---

# 10. Where confidence may attach

Support strength may attach to:

- Inferences;
- inferential Candidate Reconstructions;
- individual reconstruction decisions that depend on inference.

Support strength does **not** attach directly to:

- Evidence Sources;
- Observations;
- Recovered Facts;
- Assumptions;
- original artifacts;
- whole reconstructed artifacts as a single undifferentiated score;
- Validation Results.

Why:

- Evidence and Observations describe origin/direct extraction rather than inferential support.
- Recovered Fact already means the proposition meets the direct-support rule.
- Assumptions are assumptions, not confidence-bearing conclusions.
- A whole reconstructed artifact can contain decisions with different support strengths.
- Validation Results should expose their own measured result/dimensions rather than reuse recovery confidence.

A report may summarize the support profile of a larger reconstructed artifact, but that summary must remain derived from the underlying conclusion-level assessments and must not replace them.

---

# 11. Developer input, correction, and supersession

Developer Input is first-class recovery input but remains formally separate from Evidence.

## 11.1 Developer Assertion

A Developer Assertion may support an Inference or recovery decision.

It cannot by itself make a proposition a Recovered Fact.

Its provenance must identify the developer-supplied origin.

## 11.2 Developer Decision

A Developer Decision records how recovery should proceed where evidence does not uniquely determine the path.

A Developer Decision may resolve an operational recovery choice without claiming historical certainty.

## 11.3 Developer Correction

A Developer Correction that changes an existing recovery conclusion does not rewrite history in place.

Instead:

1. record the correction as Developer Input;
2. create the corrected conclusion or reconstruction decision;
3. link it as superseding the earlier item;
4. retain the earlier item and its support path;
5. identify downstream items that depended materially on the superseded item;
6. mark those downstream items **Needs Reevaluation** until recomputed or deliberately reaffirmed;
7. mark an item **Invalidated** where its support has been removed and no valid support remains.

This resolves the evidence-history semantics of developer correction.

`04-developer-intervention.md` still owns:

- how a developer supplies a correction;
- interaction/workflow design;
- intervention persistence UX;
- authorization or review workflow;
- presentation of intervention choices.

---

# 12. Circular-evidence protection

Circular/self-supporting recovery reasoning is prohibited.

LegacyRevive.NET must maintain a directed **support-dependency graph** for material derived conclusions.

The graph records which earlier items support later items.

## 12.1 Core rule

A node must not obtain independent support from:

- itself;
- one of its descendants;
- a reconstructed artifact generated from itself or its descendants;
- a re-expression of the same underlying support path.

## 12.2 Reconstructed output

Reconstructed output may be analyzed for operational purposes, but it must retain its generated provenance.

If a reconstructed project was created because of inference `I1`, later scanning that project cannot create independent evidence supporting `I1`.

At most it can restate or propagate the same dependency.

## 12.3 Derived observations

A Derived Observation may support a later Inference only if its dependency path does not already depend on that Inference or a descendant of it.

## 12.4 Earlier inferences

An earlier Inference may be used as a premise for a later Inference.

However:

- the downstream inference inherits the upstream dependency path;
- reuse does not create an additional independent corroboration path;
- support strength must not increase merely because the same evidence has been transformed multiple times.

## 12.5 Developer input

Developer Input may contribute to an Inference, but it remains a separate support kind.

It must not be counted as independent original-system evidence.

## 12.6 Validation reference

Validation Reference nodes never enter the recovery support graph.

## 12.7 Cycle detection

The material support graph must reject or flag a support edge that would create a dependency cycle.

The model must therefore make recursive assumption amplification detectable rather than relying only on reviewer discipline.

---

# 13. Independence of support

Two support paths are **independent** only when one is not derived from the other and they do not merely restate the same underlying source chain.

For example:

```text
Assembly metadata
    ↓
Observation A
    ↓
Inference X

Generated project from X
    ↓
Observation B
```

Observation B is **not** independent corroboration of X.

By contrast, an independently surviving configuration file that directly supports the same proposition may form an additional independent support path.

Support-strength reasoning must consider dependency independence, not merely count the number of supporting nodes.

---

# 14. Current-state and historical-state semantics

Evidence-model history is append-preserving.

Material historical items should not be silently rewritten merely to make the current model look clean.

Where a conclusion changes:

- retain the old item;
- record the new item;
- connect them using supersession/correction relationships;
- distinguish historical state from current adopted state.

Useful current-state markers include:

- **Current**
- **Superseded**
- **Needs Reevaluation**
- **Invalidated**

These state markers describe whether an item should currently influence recovery.

They do not erase its historical existence.

---

# 15. Reconstruction relationship

A reconstruction may depend on:

- Recovered Facts;
- Inferences;
- Developer Decisions;
- Active Assumptions.

The reconstruction must retain provenance to those dependencies.

The reconstruction does not become new original-system evidence.

Where a reconstruction contains multiple inferential choices, support should remain visible at the decision/conclusion level rather than collapsed into one global confidence score.

Detailed project and source reconstruction mechanics belong in:

- `08-project-reconstruction.md`;
- `09-source-reconstruction.md`.

---

# 16. Qualification resolutions

This document resolves the Evidence Model qualifications as follows.

| Qualification | Resolution |
|---|---|
| `EVID-001` | Evidence and Developer Input are separate top-level categories under Recovery Input. Developer assertions may influence inference but are not formal Evidence. |
| `EVID-002` | Observation is direct extraction from Evidence; Recovered Fact is a normalized proposition directly entailed by admissible observations without inference, assumptions, or developer assertions. |
| `EVID-003` | Earlier classifications are not rewritten. New stronger conclusions are created and linked by supersession; an earlier Inference can be superseded by a later Recovered Fact. |
| `EVID-004` | Conflicts are explicit records linking incompatible propositions and their support paths. Resolution preserves all original evidence/history. |
| `EVID-005` | Provenance is the umbrella concept; derivation lineage is part of Provenance. |
| `EVID-006` | Inferential support uses `Strongly Supported`, `Plausible`, and `Tentative`. `Unresolved` is a state, not a confidence label. |
| `EVID-007` | Support strength attaches to Inferences and inferential reconstruction decisions, not raw evidence, observations, facts, assumptions, whole artifacts, or validation results. |
| `EVID-008` | Assumptions have explicit `Active`, `Superseded`, `Invalidated`, and `Retired` states with downstream reevaluation when invalidated. |
| `EVID-009` | Developer corrections create superseding conclusions/decisions; history is retained and materially dependent downstream items become `Needs Reevaluation` or `Invalidated`. |
| `EVID-010` | A directed support-dependency graph preserves provenance, rejects cycles, prevents descendants/generated output from corroborating ancestors, and preserves support-path independence. |

---

# 17. Durable design consequences

The Evidence Model establishes these durable rules:

1. Recovery Input is broader than Evidence.
2. Developer Input is not formal Evidence.
3. Direct extraction and direct entailment are distinct from inference.
4. Recovery history is append-preserving rather than rewritten in place.
5. Provenance includes derivation lineage.
6. Conflicts remain explicit.
7. Confidence is semantic, explainable, and limited to inferential conclusions.
8. Assumptions have an explicit lifecycle.
9. Developer corrections supersede rather than erase.
10. Support dependencies must make circularity detectable and prevent self-corroboration.

These rules constrain architecture, reporting, intervention, reconstruction, and validation design.

---

# 18. Non-goals of this document

This document does not define:

- exact persistence schema;
- exact C# types;
- database technology;
- graph-storage technology;
- report formatting;
- CLI behavior;
- exact developer-intervention UI/workflow;
- project-reconstruction heuristics;
- source-decompilation strategy;
- validation metrics.

Those belong to their owning canonical documents.

---

# 19. Canonical ownership

This document owns the authoritative LegacyRevive.NET definition of:

- recovery evidence taxonomy;
- observation/fact/inference distinction;
- provenance and derivation lineage semantics;
- conflict representation principles;
- assumption lifecycle;
- confidence/support-strength semantics;
- evidence-level correction/supersession;
- anti-circular support rules.

`02-terminology.md` owns glossary wording and must remain synchronized with these definitions.

`04-developer-intervention.md` owns detailed intervention workflow while respecting the evidence semantics defined here.

`90-decisions.md` owns the durable decisions created from these resolutions.

`91-design-qualification-register.md` owns the qualification history showing how these questions were resolved.
