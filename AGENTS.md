# LegacyRevive.NET — Agent Instructions

## Project purpose

LegacyRevive.NET is a developer-assistance tool for recovering and reconstructing
legacy .NET applications when the original development solution, projects, or
source are missing, incomplete, damaged, inaccessible, or otherwise unusable.

Its objective is to recover enough accurate structure, source, configuration,
dependency information, and supporting evidence to create a workable development
baseline that a developer can understand, build where possible, validate, repair,
and continue maintaining.

LegacyRevive.NET does not claim exact reconstruction of the original repository.

---

## Implementation authority

Do not invent or silently change LegacyRevive.NET product semantics.

Implementation must follow the project's approved architecture, evidence model,
recovery model, MVP scope, tooling decisions, and implementation specifications.

### Canonical source of truth

This file is an implementation-facing summary of important project constraints.

It does not replace or redefine the canonical LegacyRevive.NET design documents,
decision log, or qualification register.

Where this file conflicts with canonical project documentation, the canonical
documentation governs.

Do not infer new canonical semantics from wording in this file.

If implementation exposes a material ambiguity, contradiction, unsupported
assumption, architectural boundary issue, or missing design decision:

1. do not silently choose a new project direction;
2. identify the issue;
3. stop at the affected design boundary where practical;
4. request clarification or governance resolution.

An implementation choice does not itself establish canonical project direction.

---

## Fundamental epistemic rules

The following distinctions must always be preserved:

- Evidence is not inference.
- Inference is not fact.
- Recovered source is not automatically Original Source.
- Recovered structure is not necessarily Original Structure.
- Developer Input is not Original-System Evidence.
- Convention observations do not establish original developer intent.
- Validation Reference is not Recovery Input.
- Reconstructed output is not independent evidence for its own historical premise.
- Build success does not establish historical fidelity.
- Build success does not establish behavioural equivalence.
- Unknown or unresolved information must not be replaced with invented certainty.

Generated reconstruction must never be allowed to corroborate its own premises.

---

## Provenance

Material recovery information must retain sufficient provenance to explain:

- where it originated;
- what supports it;
- how it was derived;
- whether inference was involved;
- whether developer intervention influenced it;
- whether assumptions contributed;
- whether conflicting support exists.

Do not remove provenance simply because doing so would simplify an implementation.

---

## Artifact analysis boundary

Artifact analyzers inspect admissible recovery artifacts and emit direct extraction
results, observations, and diagnostics.

Artifact analyzers must not silently make wider historical conclusions.

For example:

A managed assembly may directly establish an assembly boundary.

It does not directly establish that the assembly corresponded to exactly one
original project.

Inference belongs in explicit inference/recovery logic, not hidden inside artifact
readers.

---

## Reconstruction boundary

Generated projects, solutions, source files, dependency representations, and other
reconstructed artifacts remain reconstructed output.

Do not classify generated material as original merely because it:

- compiles;
- resembles likely original source;
- matches a convention;
- was accepted by a developer;
- was generated from strong supporting evidence.

---

## Developer intervention

Developer Assertions, Developer Decisions, and Developer Corrections are
Developer Input, which is a first-class category of Recovery Input.

Developer Input is not Evidence.

Developer Input must remain distinguishable from Original-System Evidence.

Developer decisions may choose how recovery proceeds without proving what
historically existed.

---

## Validation isolation

Validation references used for controlled evaluation must not enter blind recovery
as Recovery Input.

Do not use ground-truth source, project files, or other withheld validation
material to improve a recovery that is intended to remain blind.

---

## Architecture

LegacyRevive.NET uses a logical modular architecture with a domain-centered core.

Dependencies point inward toward canonical recovery semantics.

Conceptually:

    Host
      ↓
    Application
      ↓
    Domain/Core

Infrastructure and tooling adapters implement inward-facing ports.

Domain/Core must not depend on concrete implementations such as:

- ICSharpCode.Decompiler;
- Microsoft.Build;
- Microsoft.Data.Sqlite;
- NuGet.Protocol;
- IDE APIs;
- filesystem-specific infrastructure.

Concrete external tooling belongs behind explicit ports/adapters.

Do not introduce unnecessary Windows-specific dependencies into Domain/Core.

---

## Technology baseline

Unless an approved implementation specification says otherwise:

- Runtime/application baseline: .NET 10
- Language: C# 14
- Dependency management: NuGet Central Package Management
- Direct managed metadata/Portable PDB: System.Reflection.Metadata
- Decompilation: ICSharpCode.Decompiler
- C# syntax/semantic tooling: Roslyn
- Project model: Microsoft.Build + Microsoft.Build.Locator
- Restore/build execution: out-of-process
- Local package inspection: NuGet.Packaging
- Persistence: Microsoft.Data.Sqlite
- Hosting/DI/logging: Microsoft.Extensions.Hosting
- CLI: System.CommandLine
- Testing: xUnit v3
- Generated GUID identities: UUIDv7 where appropriate
- Artifact fingerprints: SHA-256

Do not replace or introduce a competing implementation for an already-selected
canonical capability without the required governance review.

---

## Dependencies

Keep concrete third-party dependencies out of Domain/Core.

New direct dependencies require deliberate justification.

Do not add packages merely for convenience when the .NET platform already
adequately provides the capability.

Do not automatically upgrade dependencies to the newest version.

Tool/dependency upgrades must not silently change LegacyRevive recovery semantics.

---

## Recovery safety

Original recovery artifacts are immutable inputs.

Never modify supplied original artifacts in place.

Generated/reconstructed output must be stored separately.

Static managed-artifact inspection should not load arbitrary subject assemblies
into the LegacyRevive process merely to inspect metadata.

Prefer non-executing inspection mechanisms.

---

## Determinism

Given equivalent Recovery Input, LegacyRevive version, configuration, dependency
baseline, and committed Developer Interventions, substantive recovery results
should be reproducible wherever practical.

Avoid introducing:

- nondeterministic ordering;
- uncontrolled timestamps in semantic output;
- generating new identities where an existing stable recovery identity should be reused;
- environment-dependent behavior without recording the relevant environment;
- hidden network dependencies.

---

## MVP boundaries

The MVP is:

- managed-.NET recovery first;
- Windows x64 first;
- local/offline by default;
- CLI-first;
- evidence/provenance aware;
- persistent and resumable;
- capable of controlled restore/build attempts.

Do not expand MVP scope simply because a capability is technically possible.

Examples of deferred capability include richer GUI/IDE integration, runtime plugin
loading, cross-platform product support, external enrichment, and generalized
machine-learned recovery inference.

---

## Coding approach

Prefer:

- small cohesive types;
- explicit domain concepts;
- immutable data where appropriate;
- dependency inversion;
- clear provenance-bearing models;
- deterministic algorithms;
- explicit error/diagnostic results;
- tests around semantic boundaries.

Avoid:

- hidden global state;
- reflection-based magic where unnecessary;
- infrastructure leaking into Domain/Core;
- catch-all service classes;
- opaque inference logic;
- silently swallowing unsupported or malformed recovery input.

---

## Tests

Tests must verify both functional behavior and epistemic boundaries.

Examples include ensuring that:

- inference is not represented as direct fact;
- reconstructed material cannot become independent evidence;
- Developer Input remains distinguishable from Evidence;
- validation references cannot leak into blind recovery;
- unsupported information remains Unknown/Unresolved;
- repeated recovery remains substantively deterministic.

Do not weaken epistemic tests merely to make an implementation pass.

---

## Change discipline

Before making a substantial implementation change:

1. identify the capability being implemented;
2. identify its expected architectural layer;
3. identify which external dependencies, if any, it requires;
4. preserve provenance and uncertainty semantics;
5. add or update tests;
6. report any discovered design ambiguity rather than silently resolving it.

Before introducing a new architectural project, abstraction, dependency, persistence
concept, inference rule, recovery state, provenance category, or MVP capability,
verify that it is already supported by canonical design.

If it is not clearly supported, do not establish it through implementation alone.

Prefer the smallest implementation that satisfies the accepted requirement.
