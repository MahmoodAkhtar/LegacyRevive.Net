# LegacyRevive.NET — Post-MVP Roadmap

## Status

**Canonical**

This document defines the canonical post-MVP capability roadmap for LegacyRevive.NET.

It organizes capabilities deliberately deferred by `14-mvp.md` into evidence-gated development horizons without turning roadmap ideas into unconditional implementation commitments.

A roadmap item does not supersede an earlier decision merely because it appears here.

---

# 1. Purpose

The roadmap answers:

> **After the MVP proves the core managed-artifact recovery proposition, what should LegacyRevive.NET expand next, in what dependency order, and under what evidence should a deferred capability be promoted into implementation scope?**

The roadmap preserves:

```text
roadmap item ≠ accepted detailed design
future possibility ≠ promised feature
```

Each material capability still passes through the normal qualification, decision, design, audit, and synchronization process before becoming canonical implementation scope.

**Governance:** ROAD-001 / D148.

---

# 2. Planning model

LegacyRevive.NET uses **evidence-gated capability horizons**, not fixed release dates.

```text
MVP evidence
    ↓
Horizon 1 — strengthen the core recovery loop
    ↓
Horizon 2 — broaden recoverability and developer reach
    ↓
Horizon 3 — deepen automation and product experience
    ↓
Horizon 4 — ecosystem, scale, and advanced research
```

Movement between horizons is informed by validation results, intervention burden, recurring blockers, artifact frequency, false-confidence patterns, reliability data, platform demand, maintenance cost, and architectural prerequisites.

Calendar dates may be added to project planning later, but they are not the canonical dependency model.

**Governance:** ROAD-002 / D149.

---

# 3. Promotion criteria

A roadmap capability should be promoted into active design/implementation only when there is a defensible reason.

Useful signals include:

- recurring Blocked/Partial recoveries;
- repeated developer intervention;
- strong unused evidence in common artifacts;
- false-confidence or recoverable-miss patterns;
- meaningful reduction in time to a Workable baseline;
- demonstrated corpus frequency;
- prerequisite value for several later capabilities;
- acceptable security/maintenance cost;
- objective validation feasibility.

A feature should not be promoted solely because it is technically interesting.

**Governance:** ROAD-003 / D150.

---

# 4. Horizon 1 — strengthen the core recovery loop

The first post-MVP priority is improving the reliability and effectiveness of the proven workflow before maximizing artifact breadth.

Priorities include:

## 4.1 Build-recovery refinement

- targeting-pack/toolset detection;
- missing-prerequisite classification;
- deterministic reference-resolution repair;
- project-property/resource/configuration refinement;
- framework-specific diagnostics;
- dependency-resolution explanations.

Build repair may improve the candidate representation but cannot invent historical facts merely to compile.

## 4.2 Project-boundary refinement

Improve ownership classification, project merge/split candidates, application-boundary detection, and project/package/binary ambiguity handling from validation evidence.

## 4.3 Source reconstruction quality

Improve PDB-guided grouping, XML-documentation restoration, compiler-generated-code handling, deterministic layout, resource relationships, and decompiler-failure diagnostics.

## 4.4 Intervention and explainability UX

Improve decision summaries, candidate comparison, `explain` output, impact previews, and safe batched decisions.

## 4.5 Recovery-report maturity

Improve blocker summaries, dependency/project visualization, artifact contribution, recovery-ceiling reporting, intervention burden, and build progression.

**Governance:** ROAD-004 / D151.

---

# 5. Horizon 2 — broaden recoverability and platform reach

After the core loop is stable, broaden evidence and execution coverage.

Priorities may include:

- validated Linux/macOS execution for capabilities that do not require Windows-only toolchains;
- mature native Windows PDB support;
- deployed source-like artifacts such as `.cshtml`, `.aspx`, `.ascx`, `.master`, `.svc`, and `Global.asax`;
- deeper `.edmx`, `.wsdl`, `.xsd`, WCF/service/model recovery context;
- Web Deploy, Windows Service, IIS, container, Octopus, and other deployment descriptors where corpus evidence justifies them.

Cross-platform support means the same canonical recovery semantics with explicit capability differences; it does not imply every recovered .NET Framework application can build on every host.

**Governance:** ROAD-005 / D152.

---

# 6. Horizon 2 — external enrichment

External enrichment may later include:

- remote NuGet lookup/package retrieval;
- symbol servers;
- Source Link/source-server retrieval;
- reference-pack discovery;
- vulnerability/package-lifecycle metadata.

Canonical rule:

```text
supplied Recovery Input ≠ external enrichment
```

Reports must distinguish what supplied artifacts established from what external sources added. Offline operation remains supported, and controlled validation records whether enrichment was enabled because it changes the evidence ceiling.

`DOC-020` is Resolved: external enrichment remains post-MVP capability scope. The `NuGet.Protocol` selection in `13-tooling-and-dependencies.md` is a contingent adapter choice for this roadmap capability and does not itself promote the capability into MVP scope.

**Governance:** ROAD-006 / D153.

---

# 7. Horizon 3 — deeper convention and inference automation

Later work may expand deterministic Derived Observations and inference research.

Possible areas:

- version/framework-generation clusters;
- richer dependency-direction patterns;
- implementation/interface pairing;
- persistence/DI/framework usage patterns;
- application-role patterns;
- candidate project/dependency/source-organization ranking.

Repeated real-world interventions may suggest candidate inference rules, but the lifecycle remains:

```text
real interventions
    ↓
research hypothesis
    ↓
Research Corpus
    ↓
held-out Validation Corpus
    ↓
governed inference rule
```

A developer choice is never automatically oracle truth.

A future learned model may assist candidate ranking, but learned output remains inferential and subordinate to the authoritative Evidence Model.

**Governance:** ROAD-007 / D154.

---

# 8. Horizon 3 — richer developer experience

Potential hosts include:

- desktop/workbench UI;
- local web UI;
- Visual Studio/Rider integration;
- stable automation/API surface.

They may support evidence-graph navigation, candidate comparison, conflict/intervention workflows, build progression, and recovery diagnostics.

All hosts must reuse canonical Application/Domain capabilities rather than reimplement recovery semantics.

**Governance:** ROAD-008 / D155.

---

# 9. Horizon 3 — validation and benchmark maturity

Validation should grow alongside product capability.

Possible expansion:

- larger held-out multi-framework corpus;
- historical tagged releases;
- artifact-starvation matrices;
- mixed-release/corrupt-deployment scenarios;
- richer semantic comparators;
- original-test-suite reuse;
- behavior-level validation where safe;
- intervention-cost studies;
- time-to-workable measurements;
- support-strength calibration;
- regression dashboards.

Research Corpus and held-out Validation Corpus remain distinct.

Validation should prioritize finding confident errors, provenance violations, and recoverable misses—not merely increasing a headline score.

**Governance:** ROAD-009 / D156.

---

# 10. Horizon 4 — extension ecosystem

Runtime third-party plugin loading should only be introduced when there is demonstrated need for independently deployed capabilities.

Potential extensions include proprietary analyzers, organization-specific resolvers/rules, validation comparators, and report exporters.

Before runtime plugin loading is accepted, governance must resolve trust/security, API compatibility, dependency loading, isolation, provenance attribution, determinism, failure containment, and capability permissions.

Compile-time/first-party modularity remains sufficient until these needs are demonstrated.

**Governance:** ROAD-010 / D157.

---

# 11. Horizon 4 — scale and collaborative recovery

Potential later features include shared workspaces, multi-user intervention history, approvals, central corpus/report storage, distributed workers, enterprise artifact repositories, organization indexes, and CI recovery pipelines.

These require new design work for identity, authorization, concurrency, conflict resolution, immutable audit history, retention, sensitive artifacts, workspace sharing, and distributed determinism.

A local SQLite workspace is not automatically promoted into a cloud architecture merely because collaboration is desired.

**Governance:** ROAD-011 / D158.

---

# 12. Horizon 4 / research — native and runtime recovery

Native/runtime recovery is a distinct expansion domain.

Potential research areas:

- native import/export analysis;
- mixed-mode assemblies;
- COM/interoperability clues;
- native dependency graphs;
- crash dumps;
- runtime logs/traces;
- database/schema snapshots;
- installer/package manifests.

Each requires its own evidence/recoverability model.

LegacyRevive.NET must not imply that native binary reconstruction has the same source-recovery properties as managed IL decompilation.

**Governance:** ROAD-012 / D159.

---

# 13. Capabilities deliberately not promised

The roadmap does not promise exact historical source text, exact original solution/project reproduction, guaranteed file/folder recovery, guaranteed buildability, universal package identification, universal native-source recovery, autonomous modernization, speculative auto-fix acceptance, or an AI system that determines historical truth without provenance.

Future claim boundaries must continue to match surviving evidence.

---

# 14. Prioritization model

Roadmap candidates should be compared transparently across:

| Dimension | Question |
|---|---|
| Recovery impact | How many Blocked/Partial cases could this improve? |
| Intervention reduction | How much repeated developer work could it remove? |
| Evidence value | Does it expose strong previously unused evidence? |
| Epistemic safety | Does it reduce unsupported/confidently-wrong conclusions? |
| Corpus frequency | How common is the scenario/artifact? |
| Dependency leverage | Does it unblock several later capabilities? |
| Implementation cost | What engineering/maintenance burden is introduced? |
| Platform/security risk | Does it expand attack surface or fragility? |
| Validation readiness | Can the capability be objectively tested? |

This is an explanatory matrix, not a pseudo-precise universal priority score.

---

# 15. Feedback from real recoveries

Post-MVP evidence should feed roadmap refinement.

Useful signals include unsupported artifact kinds, common blockers, unresolved dependencies, frequent intervention categories, abandoned stages, build failure categories, time/steps to Workable, high-confidence inference failures, common Unknowns later resolved by developers, and requested workflow features.

```text
observed product/research evidence
    ↓
roadmap hypothesis
    ↓
qualification/design
    ↓
implementation
    ↓
validation
    ↓
roadmap reprioritization
```

Product telemetry or user choices must never silently become Original-System Evidence inside a recovery.

---

# 16. Roadmap governance

Roadmap status categories are:

- **Planned horizon** — canonical strategic location, not accepted detailed design;
- **Candidate** — worthwhile but evidence/order remains contingent;
- **Research** — feasibility/validation work required;
- **Deferred** — known capability intentionally not prioritized.

When a roadmap item becomes active design work:

1. register material qualifications in `91`;
2. create/update its owning canonical design document;
3. record durable decisions in `90` where required;
4. audit;
5. synchronize;
6. only then treat the detailed design as accepted.

**Governance:** ROAD-001 / D148.

---

# 17. Concise horizon view

| Horizon | Primary objective | Representative capabilities |
|---|---|---|
| MVP | Prove trustworthy managed-artifact recovery loop | Canonical `14-mvp.md` scope |
| H1 | Improve core recovery effectiveness | build/project/source/intervention/report refinement |
| H2 | Broaden evidence/platform coverage | cross-platform, PDB, web/service/schema/deployment artifacts, enrichment |
| H3 | Reduce human effort and deepen UX | advanced inference, ranking, richer hosts, validation maturity |
| H4 | Ecosystem and scale | runtime plugins, collaboration, distributed/enterprise workflows |
| Research | Explore qualitatively different evidence | native binaries, runtime evidence, learned ranking |

This ordering expresses dependencies, not release dates.

---

# 18. Roadmap invariants

1. roadmap entries are not automatically accepted implementation designs;
2. roadmap ordering is evidence/dependency gated rather than calendar-defined;
3. MVP historical scope is not retroactively expanded;
4. core recovery effectiveness is prioritized before breadth for its own sake;
5. external enrichment remains provenance-distinct;
6. cross-platform support preserves canonical semantics while allowing capability differences;
7. advanced inference remains subordinate to the Evidence Model;
8. developer choices are not automatically training truth;
9. learned models may rank candidates but do not become epistemic authority;
10. richer hosts reuse Application/Domain semantics;
11. runtime plugin loading requires explicit trust/isolation governance;
12. collaborative/cloud scale requires new concurrency/security/audit design;
13. native/runtime evidence requires its own recovery/claim boundaries;
14. validation and held-out corpora grow alongside capability;
15. priority is explained transparently rather than through opaque pseudo-precision;
16. product feedback may reprioritize the roadmap without becoming recovery evidence.

---

# 19. Governance mapping

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| ROAD-001 | D148 | Roadmap is strategic sequencing, not automatic detailed-design acceptance |
| ROAD-002 | D149 | Capability horizons are evidence/dependency gated rather than date promises |
| ROAD-003 | D150 | Promotion requires demonstrated recovery/developer/safety value |
| ROAD-004 | D151 | First post-MVP horizon strengthens the core recovery loop |
| ROAD-005 | D152 | Second horizon broadens platform/artifact evidence coverage |
| ROAD-006 | D153 | External enrichment expands later but remains provenance-distinct |
| ROAD-007 | D154 | Advanced inference/learned ranking stays subordinate to Evidence Model |
| ROAD-008 | D155 | Richer hosts reuse canonical Application/Domain capabilities |
| ROAD-009 | D156 | Validation/corpus maturity expands alongside product capability |
| ROAD-010 | D157 | Runtime plugin ecosystem waits for demonstrated need and trust design |
| ROAD-011 | D158 | Collaborative/enterprise scale requires explicit new governance |
| ROAD-012 | D159 | Native/runtime recovery is a distinct research/capability domain |

No open roadmap qualification remains after this governed revision.

---

# 20. Completion of the initial canonical sequence

With this document governed, the initial `00`–`15` LegacyRevive.NET document sequence is complete.

That does not mean the design can never change. It means future work now has an authoritative context, stable terminology, evidence/provenance rules, intervention/convention models, artifact/recovery/reconstruction boundaries, validation/metrics, architecture/tooling, a bounded MVP, roadmap, and governance history.

Future material changes extend or supersede this baseline through the same governance lifecycle.

---

# 21. Governance relationship

Durable roadmap decisions are owned by `90-decisions.md`.

Qualification state/history are owned by `91-design-qualification-register.md`.

Roadmap reprioritization that does not establish durable direction may update this document without a new decision. Material changes to capability boundaries, claim semantics, or accepted sequencing principles require qualification/decision governance.
