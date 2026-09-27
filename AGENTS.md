# LegacyRevive.NET — Agent Instructions

## Purpose

This file provides implementation-facing instructions for coding agents working in the
LegacyRevive.NET repository.

It is not a canonical product/design document and must not be treated as one.

Canonical LegacyRevive.NET product semantics, architecture, scope, governance, tooling,
and development-process rules are owned by the canonical document set under:

`docs/canonical/`

Development Slice Specifications live under:

`docs/development/specifications/`

Where this file, a Development Slice Specification, tests, code, comments, README content,
or prior conversation conflicts with an accepted canonical source, the canonical source
governs.

Do not infer new canonical semantics from this file.

---

## Project purpose

LegacyRevive.NET is a developer-assistance tool for recovering and reconstructing legacy
.NET applications when the original development solution, projects, or source are missing,
incomplete, damaged, inaccessible, or otherwise unusable.

Its objective is to recover enough accurate structure, source, configuration, dependency
information, and supporting evidence to create a workable development baseline that a
developer can understand, build where possible, validate, repair, and continue maintaining.

LegacyRevive.NET does not claim exact reconstruction of the original repository.

---

## Repository authority and locations

Canonical design and governance documents live under:

`docs/canonical/`

Development Slice Specifications live under:

`docs/development/specifications/`

Production implementation lives under:

`src/`

Verification lives under:

`tests/`

The authority flow is:

```text
canonical documents
    ↓
Development Slice Specification
    ↓
executable acceptance + supporting verification
    ↓
production implementation
```

The important ownership boundaries are:

- `00-ai-context.md` — project orientation and canonical ownership map;
- `12-architecture.md` — technical architecture;
- `13-tooling-and-dependencies.md` — tooling and dependency choices;
- `14-mvp.md` — MVP scope and release acceptance gates;
- `15-roadmap.md` — later/deferred capability;
- `16-development-process.md` — implementation-development process and
  Development Slice Specification model;
- `90-decisions.md` — durable accepted decisions;
- `91-design-qualification-register.md` — qualification state, history, and the
  normative governance lifecycle.

Other canonical subjects are owned by the documents identified in
`docs/canonical/00-ai-context.md`.

Do not duplicate or redefine a concept in another document, specification, test, or code
comment when it already has a canonical owner.

If canonical documents appear to conflict, do not silently reconcile them. Route the
material conflict through the governance process.

---

## Required reading before material implementation

Before beginning or materially changing a Development Slice:

1. read `docs/canonical/00-ai-context.md`;
2. read `docs/canonical/16-development-process.md`;
3. identify and read the applicable `DEV-SPEC-*` under
   `docs/development/specifications/`;
4. read the canonical owner documents and accepted decisions referenced by the DEV-SPEC's
   `Canonical basis`;
5. read any additional canonical owner sections materially affected by the work;
6. check `docs/canonical/91-design-qualification-register.md` for qualifications relevant
   to the work;
7. confirm the slice remains within the accepted scope in
   `docs/canonical/14-mvp.md`;
8. confirm architecture and dependency choices against
   `docs/canonical/12-architecture.md` and
   `docs/canonical/13-tooling-and-dependencies.md`.

Do not rely on memory, prior chat, README summaries, existing code, comments, tests, or this
file instead of reading the applicable canonical sources.

---

## Development Slice discipline

Every material, acceptance-bearing Development Slice requires one Development Slice
Specification before production implementation begins.

A Development Slice Specification is durable implementation history but is non-canonical.

It applies accepted canonical requirements to one bounded implementation increment. It does
not create, replace, weaken, reinterpret, or supersede canonical product/design semantics.

A Development Slice Specification is not required merely for process formality when a change
is demonstrably non-semantic and non-acceptance-bearing, such as:

- formatting-only edits;
- typo-only corrections;
- comment-only corrections that do not alter a contract;
- behavior-preserving internal refactoring already covered by existing acceptance
  verification;
- mechanical repository maintenance that does not alter established behavior or
  architectural boundaries.

If it is materially unclear whether work is exempt, treat it as Development Slice work or
raise the uncertainty through governance rather than silently exempting it.

Do not enlarge a slice merely because adjacent work is technically convenient.

Do not implement adjacent capability unless it is included by the active DEV-SPEC.

Do not promote roadmap work into MVP implementation without the required governance process.

### Acceptance baseline

When a Development Slice Specification reaches `Ready for Implementation`, its acceptance
contract becomes the implementation baseline.

Do not weaken, delete, reinterpret, or bypass an acceptance proposition merely to make the
implementation pass.

Treat the active DEV-SPEC's following sections as the bounded implementation record:

- `Canonical basis`;
- `Scope`;
- `Acceptance contract`;
- `Representative scenarios`;
- `Executable verification`;
- `Supporting verification`;
- `Qualifications discovered`;
- `Verification status`;
- `Lifecycle history`.

A slice is not `Implemented` merely because:

- production code exists;
- the solution builds;
- unit tests pass;
- integration tests pass;
- a storage or architectural choice appears internally consistent.

Completion requires the observable accepted behavior and Definition of Done defined by
`docs/canonical/16-development-process.md`.

---

## Development Slice scope discipline

Implement only the included scope defined by the active Development Slice Specification.

Treat its `Included` and `Excluded` scope as the implementation boundary for that slice.

Do not opportunistically implement adjacent capabilities merely because they are technically
convenient, closely related, or likely to be needed later.

If required work appears to fall outside the active DEV-SPEC:

1. do not silently add it to the implementation;
2. determine whether it is already covered by another accepted Development Slice;
3. determine whether the need exposes a material canonical ambiguity, boundary issue, or
   missing design decision;
4. route material design questions through the canonical governance process;
5. create or use a later DEV-SPEC only in accordance with
   `docs/canonical/16-development-process.md`.

Implement the active slice, not the whole MVP or roadmap.

---

## Governance during implementation

Do not invent or silently change LegacyRevive.NET product semantics.

If implementation exposes a material:

- ambiguity;
- contradiction;
- unsupported assumption;
- architectural boundary issue;
- missing design decision;
- challenge to an accepted decision;
- requirement beyond the DEV-SPEC canonical basis;
- requirement beyond accepted MVP scope;
- conflict between canonical sources;

then:

1. do not silently resolve it in code, tests, comments, or the DEV-SPEC;
2. identify the affected canonical owner and relevant accepted decisions;
3. stop at the affected design boundary where practical;
4. record the issue in the active DEV-SPEC under `Qualifications discovered` where
   appropriate;
5. route the material issue through
   `docs/canonical/91-design-qualification-register.md`;
6. do not treat a proposed resolution as accepted until the required governance lifecycle
   has completed.

`docs/canonical/90-decisions.md` owns durable accepted decisions.

`docs/canonical/91-design-qualification-register.md` owns qualification state, history, and
the normative governance lifecycle.

An implementation choice, test expectation, code comment, or DEV-SPEC statement does not
itself establish canonical direction.

Accepted decisions must not be silently rewritten in place. Follow the canonical
supersession process when durable direction changes.

---

## Fundamental epistemic rules

Preserve the distinctions defined by the canonical Evidence Model and related documents.

Never silently collapse:

- Evidence into inference;
- inference into fact;
- reconstructed output into historical evidence;
- Developer Input or Developer Intervention into Original-System Evidence;
- convention observation into original developer intent;
- Validation Reference into Recovery Input;
- build success into historical fidelity;
- build success into behavioural equivalence;
- recovered structure into guaranteed original structure;
- decompiler-generated source into Original Source;
- external enrichment into supplied Recovery Input;
- unresolved information into invented certainty.

Generated reconstruction must never be allowed to corroborate its own premises.

When information is genuinely unknown or unresolved, preserve that state rather than
fabricating a convenient answer.

---

## Provenance

Material recovery information must retain sufficient provenance to explain, where applicable:

- where it originated;
- what supports it;
- how it was derived;
- whether inference was involved;
- whether developer intervention influenced it;
- whether assumptions contributed;
- whether conflicting support exists.

Do not remove provenance merely because doing so simplifies implementation.

Do not use reconstructed or generated output as independent support for the historical
premise that caused that output to be generated.

---

## Recovery Input and Original Artifact safety

Original recovery artifacts are immutable inputs.

Never modify supplied Original Artifact bytes in place.

Successfully preserved Original Artifacts must follow the accepted preservation semantics
defined by the applicable canonical recovery, architecture, MVP, tooling, and decision
sources.

Do not substitute a content hash for preserved bytes where the canonical model requires
both.

Keep Artifact identity, content hash, supplied path/provenance, and physical storage
location as distinct concepts.

Do not collapse distinct supplied Artifact identities merely because their content hashes
match.

Generated and reconstructed output must remain separate from preserved Original Artifacts.

Static inspection should not execute or load arbitrary subject assemblies merely to inspect
metadata unless an accepted canonical requirement explicitly requires execution.

Prefer non-executing inspection mechanisms.

---

## Validation isolation

Validation Reference material used for controlled evaluation must not enter blind recovery
as Recovery Input.

Do not use withheld ground-truth source, project files, or other Validation Reference
material to improve a recovery intended to remain blind.

Follow `docs/canonical/10-validation-strategy.md` and the canonical Evidence Model for
validation boundaries.

---

## Artifact analysis and inference boundary

Artifact analyzers inspect admissible recovery artifacts and emit direct extraction results,
observations, and diagnostics according to the canonical evidence and recovery models.

Artifact analyzers must not silently make wider historical conclusions.

For example, a managed assembly may directly establish an assembly boundary. That does not
by itself establish that the assembly corresponded to exactly one original project.

Inference belongs in explicit inference/recovery logic, with provenance and uncertainty
preserved, rather than hidden inside artifact readers.

Convention-based observations must not be converted into claims of original developer
intent.

---

## Reconstruction boundary

Generated projects, solutions, source files, dependency representations, and other
reconstructed artifacts remain reconstructed output unless a canonical source explicitly
classifies something otherwise.

Do not classify generated material as original merely because it:

- compiles;
- resembles likely original source;
- matches a convention;
- was accepted by a developer;
- was generated from strong supporting evidence.

Materialized reconstruction must not become evidence for its own historical premises.

Working-tree materialization state, Developer-Adjusted Source, adoption/discard behavior,
and export behavior must follow their canonical owners and accepted decisions rather than
being inferred from filesystem convenience.

---

## Developer intervention

Developer intervention must follow `docs/canonical/04-developer-intervention.md` and the
canonical Evidence Model.

Developer-supplied assertions, decisions, corrections, or adjustments must remain
distinguishable from Original-System Evidence.

A developer decision may choose how recovery proceeds without proving what historically
existed.

Do not silently convert an uncommitted working-tree edit into canonical recovered state,
Evidence, or Developer-Adjusted Source.

---

## Architecture

Follow `docs/canonical/12-architecture.md`.

Dependencies must respect the accepted inward-facing architectural boundaries.

Keep Domain/Core independent of concrete infrastructure, tooling, IDE, filesystem,
persistence, decompiler, package-feed, and build-system implementations unless the canonical
architecture explicitly establishes otherwise.

Concrete external tooling belongs behind the accepted ports/adapters.

Do not create a new architectural project, layer, abstraction, persistence concept, or
cross-cutting mechanism merely because it is convenient.

Verify that architectural changes are already supported by canonical design or route the
material question through governance.

---

## Tooling and dependencies

Follow `docs/canonical/13-tooling-and-dependencies.md`.

Do not duplicate the canonical tooling/dependency matrix in this file.

A Development Slice Specification may narrow how accepted tooling is used for its slice. It
cannot silently replace or override a canonical tooling decision.

Do not introduce a competing implementation for an already-selected canonical capability
without the required governance review.

Keep concrete third-party dependencies out of Domain/Core where required by the canonical
architecture.

New direct dependencies require deliberate justification.

Do not add packages merely for convenience where the platform or already accepted tooling
adequately provides the capability.

Do not automatically upgrade dependencies as part of unrelated work.

Tool or dependency changes must not silently alter LegacyRevive.NET recovery semantics.

---

## Persistence, replay, and determinism

Follow the canonical architecture, recovery process, MVP, tooling, and accepted decisions
for persistent and replayable state.

Recovery workspaces must remain resumable where required by the active canonical scope.

Stable recovery identities must be reused when reopening or replaying existing state rather
than regenerated merely because a process restarted.

Preserved Original Artifact bytes must not depend on the continued availability of their
external supplied path after successful workspace-owned preservation where the canonical
model requires workspace ownership.

Given equivalent Recovery Input, LegacyRevive.NET version, configuration, dependency
baseline, committed Developer Interventions, and applicable checkpoint/state, substantive
results should be reproducible wherever required by the canonical model.

Avoid introducing:

- nondeterministic ordering;
- uncontrolled timestamps into semantic output;
- new identities where existing stable recovery identities should be reused;
- environment-dependent behavior without capturing material environment context;
- hidden network dependencies;
- replay behavior that depends on mutable external source paths after successful
  workspace-owned preservation.

Do not convert implementation convenience into new determinism semantics.

---

## MVP and roadmap boundaries

Follow `docs/canonical/14-mvp.md` for MVP scope.

Follow `docs/canonical/15-roadmap.md` for later/deferred capability.

Do not duplicate the current MVP feature list or roadmap content in this file.

Do not expand MVP scope merely because a capability is technically possible or desirable.

Do not pull roadmap work into an active Development Slice unless it has been promoted through
the normal governance process.

The active DEV-SPEC may intentionally implement only a narrow subset of the overall MVP.

Omitted adjacent MVP work is not permission to implement it in the current slice.

---

## Testing and executable acceptance

Follow `docs/canonical/16-development-process.md` for acceptance discipline.

Follow `docs/canonical/13-tooling-and-dependencies.md` for test technology.

The governing principle is:

**Canonical requirements govern; executable acceptance demonstrates.**

For a material Development Slice:

1. derive acceptance propositions from accepted canonical requirements;
2. verify them through representative scenarios;
3. establish executable acceptance verification before or alongside production
   implementation;
4. add lower-level tests that support diagnosis, design confidence, and failure isolation;
5. keep acceptance verification focused on observable contracts rather than incidental
   implementation detail where practical.

Executable acceptance verification may exercise application-level behavior, public
domain/application contracts, host/CLI behavior where part of the slice contract, and
durable observable state.

Do not couple acceptance unnecessarily to:

- private class structure;
- exact database table layout;
- exact internal directory algorithms;
- incidental serialization details;
- implementation-specific call ordering;

unless those details are themselves canonical observable requirements.

Supporting verification may include:

- domain/unit tests;
- integration tests;
- persistence tests;
- filesystem/storage tests;
- analyzer/component tests;
- determinism tests;
- replay/resume tests;
- negative and failure-path tests.

Lower-level tests do not substitute for slice-level executable acceptance verification.

Tests must verify epistemic boundaries where relevant, including that:

- inference is not represented as direct fact;
- reconstructed material cannot become independent Evidence;
- Developer Input remains distinguishable from Original-System Evidence;
- Validation References cannot leak into blind recovery;
- unsupported or unresolved information remains Unknown/Unresolved where required;
- replay and determinism obligations remain satisfied.

Do not weaken acceptance or epistemic tests merely to make an implementation pass.

---

## Coding approach

Prefer:

- the smallest implementation that satisfies the accepted requirement;
- small cohesive types;
- explicit domain concepts;
- immutable data where appropriate;
- dependency inversion;
- clear provenance-bearing models;
- deterministic algorithms;
- explicit error/diagnostic results;
- explicit boundaries between evidence extraction and inference;
- tests around semantic boundaries;
- infrastructure adapters behind inward-facing ports.

Avoid:

- hidden global state;
- reflection-based magic where unnecessary;
- infrastructure leaking into Domain/Core;
- catch-all service classes;
- opaque inference logic;
- silently swallowing unsupported or malformed recovery input;
- speculative abstractions for capabilities outside the active slice;
- implementing future extensibility before the accepted requirement needs it;
- using generated reconstruction to validate its own premise.

---

## Change discipline for coding agents

Before making a material implementation change:

1. identify the active Development Slice and DEV-SPEC;
2. confirm its lifecycle status and blockers;
3. identify the canonical requirements and decisions being implemented;
4. identify the expected architectural layer;
5. identify which external dependencies, if any, are required;
6. identify the acceptance proposition(s) affected;
7. establish or update the required executable and supporting verification;
8. preserve provenance, uncertainty, replay, integrity, and determinism semantics;
9. implement only the smallest change required by the slice;
10. run the relevant verification;
11. update the DEV-SPEC verification and lifecycle material as required by
    `docs/canonical/16-development-process.md`;
12. report and govern any material design ambiguity rather than silently resolving it.

Before introducing a new:

- architectural project;
- abstraction with semantic significance;
- dependency;
- persistence concept;
- inference rule;
- recovery state;
- provenance category;
- workspace semantic;
- Artifact semantic;
- validation semantic;
- developer-intervention semantic;
- MVP capability;

verify that it is already supported by canonical design.

If it is not clearly supported, do not establish it through implementation alone.

---

## Completion discipline

Do not declare a Development Slice complete solely from code review, build success, or
lower-level test success.

Before treating a slice as `Implemented`, verify the Definition of Done in
`docs/canonical/16-development-process.md`, including as applicable:

- canonical basis remains valid and coherent;
- included/excluded scope remains respected;
- acceptance propositions are satisfied repeatably;
- executable acceptance verification is present and passing;
- supporting verification is passing;
- no unresolved blocking qualification remains;
- architecture, tooling, evidence, provenance, determinism, and MVP boundaries are
  respected;
- actual verification locations are recorded;
- verification status is updated;
- lifecycle history is updated;
- any governance changes exposed by the slice have completed required synchronization.

Code completion alone is not Development Slice completion.
