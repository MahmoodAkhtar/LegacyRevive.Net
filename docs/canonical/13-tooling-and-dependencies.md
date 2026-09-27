# LegacyRevive.NET — Tooling and Dependencies

## Status

**Canonical**

This document defines the canonical implementation-tooling and dependency choices for LegacyRevive.NET.

It realizes the logical capabilities and boundaries established by:

`12-architecture.md`

without changing the canonical evidence, recovery, reconstruction, validation, or metric semantics.

The concrete MVP scope remains owned by:

`14-mvp.md`

The versions listed here are the **verified implementation baseline as of 2026-09-25**. Package-version changes do not require a new durable design decision when they preserve the selected technology role and all canonical semantics; material technology replacement or semantic impact must follow the governance process.

---

# 1. Purpose

This document answers:

> **Which .NET platform capabilities, libraries, and tools should LegacyRevive.NET use to implement the canonical architecture, and under what dependency-governance rules?**

The tooling strategy follows four priorities:

1. prefer supported .NET platform capabilities where they adequately solve the problem;
2. reuse mature specialist tooling where reimplementation would add risk without product value;
3. place concrete tools behind the Capability Ports defined by `12-architecture.md`;
4. prevent a library's internal model or convenience API from redefining LegacyRevive.NET's epistemic model.

---

# 2. Tooling status vocabulary

Each technology in this document has one of these statuses.

## Selected

The canonical implementation choice for the capability.

A future material replacement requires governed reconsideration.

## Optional adapter

Supported or planned only when a specific artifact/environment requires it.

Its absence must not redefine the general recovery model.

## Deferred

No concrete dependency is selected yet because the capability is outside the current design need or belongs to a later document.

## Platform/BCL

Use the supported .NET platform API directly rather than adding a third-party abstraction by default.

---

# 3. Runtime and SDK baseline

LegacyRevive.NET targets **.NET 10 (`net10.0`)** as its primary application/runtime baseline.

Reasons:

- .NET 10 is the current active LTS release;
- it provides the modern BCL and runtime capabilities required by the tool;
- LegacyRevive.NET can inspect/reconstruct applications targeting older .NET Framework/.NET versions without itself targeting those frameworks;
- a modern host reduces constraints imposed by the legacy applications being recovered.

This does **not** imply that recovered applications are upgraded to .NET 10.

The recovered target framework remains evidence/reconstruction state owned by the recovery model.

The implementation may later add platform-specific target projects where a capability genuinely requires them, but the core/domain remains modern .NET.

**Governance:** TOOL-001 / D117.

## 3.1 LegacyRevive.NET repository solution format

LegacyRevive.NET's own development repository uses **SLNX** as its canonical solution-file format.

The repository solution is therefore:

```text
LegacyRevive.Net.slnx
```

This choice aligns the repository with the .NET 10 SDK baseline selected above. In .NET 10, `dotnet new sln` creates an `.slnx` solution by default; `.sln` requires an explicit format choice. The SLNX format is the modern SDK-supported solution representation and is preferred for LegacyRevive.NET's own development solution.

This repository-format decision is distinct from **reconstructed Candidate Solution output**. LegacyRevive.NET may emit `.sln`, `.slnx`, or another supported solution representation for a recovered application where the Project Reconstruction model and target/tooling context justify that representation. The format used by the LegacyRevive.NET repository must not be projected onto recovered applications as though it were historical evidence about them.

The repository filename is a governed repository convention separate from the SLNX format choice. The current canonical filename is `LegacyRevive.Net.slnx`. A future material change to the solution format or canonical repository filename must be synchronized through the governance process rather than allowed to drift silently.

**Governance:** TOOL-017 / D169; supersedes the repository-solution naming/format direction recorded by TOOL-016 / D162 while retaining SLNX as the canonical format.

---

# 4. Dependency/version governance

LegacyRevive.NET uses **Central Package Management** through `Directory.Packages.props`.

Rules:

- direct package versions are explicitly pinned;
- package upgrades are deliberate changes, not floating restores;
- restore/build output records effective tool/package versions where relevant to replay or validation;
- package-lock/locked restore should be used for CI/reproducible benchmark contexts where practical;
- a newer package is not adopted merely because it has the highest version number;
- target-framework compatibility, security, license, behavior, and deterministic output are checked first;
- transitive packages are not referenced directly from domain code merely because another dependency exposes them.

Package version updates that preserve the selected technology and semantics are maintenance work. A technology replacement or a change that affects canonical behavior requires a qualification/decision.

**Governance:** TOOL-002 / D118.

---

# 5. Verified baseline package set

Verified on 2026-09-25:

| Capability | Selected technology | Verified baseline |
|---|---|---:|
| Application runtime | .NET SDK/runtime | 10.0.12 |
| Hosting/DI/logging | `Microsoft.Extensions.Hosting` | 10.0.12 |
| CLI | `System.CommandLine` | 2.0.12 |
| PE/CLR metadata + Portable PDB | `System.Reflection.Metadata` / `PEReader` | .NET 10 platform |
| C# decompilation | `ICSharpCode.Decompiler` | 11.0.0.9375 |
| C# syntax/semantic analysis | `Microsoft.CodeAnalysis.CSharp` | 5.9.0 |
| MSBuild project API | `Microsoft.Build` | 18.9.6 |
| MSBuild discovery | `Microsoft.Build.Locator` | 1.11.2 |
| Local NuGet package inspection | `NuGet.Packaging` | 7.9.0 |
| NuGet feed access when explicitly enabled | `NuGet.Protocol` | 7.9.0 |
| Local state database | `Microsoft.Data.Sqlite` | 10.0.12 |
| Windows native PDB fallback | `Microsoft.DiaSymReader.Native` | 1.7.0 |
| Testing | `xunit.v3` | 4.0.1 |

This table is not a rule that all packages must remain forever on these exact versions.

It records the governed starting baseline.

---

# 6. PE, CLR metadata, and IL inspection

## Selected: `System.Reflection.Metadata`

`System.Reflection.Metadata`, together with `System.Reflection.PortableExecutable.PEReader`, is the primary direct-extraction technology for managed PE/CLR metadata.

It is used for evidence-oriented extraction such as:

- assembly identity;
- references;
- module/assembly metadata;
- types and members;
- signatures;
- custom attributes;
- generic metadata;
- method bodies/IL where required;
- embedded metadata/resources where exposed;
- Portable PDB metadata.

Why it is primary:

- it is a Microsoft platform API;
- it operates directly over PE/metadata structures;
- it does not require loading the target assembly into the LegacyRevive process;
- it provides a suitable low-level boundary for **Observations** rather than decompiler interpretations.

LegacyRevive.NET should avoid using `Assembly.Load*` as its primary inspection model because loading arbitrary legacy assemblies introduces dependency resolution, execution-context, framework, and isolation problems that are unnecessary for static recovery.

**Governance:** TOOL-003 / D119.

---

# 7. Decompilation

## Selected: `ICSharpCode.Decompiler`

LegacyRevive.NET uses the ILSpy decompilation engine through the `ICSharpCode.Decompiler` package.

It is selected for:

- managed C# decompilation;
- method/type reconstruction;
- high-level C# generation from IL;
- compiler-generated/lowered-code interpretation where supported;
- whole-type/source reconstruction assistance.

The tool must be wrapped by a LegacyRevive Capability Port.

Its generated C# is classified according to `09-source-reconstruction.md`, normally as **Decompiler-Generated Source** or another appropriate reconstructed provenance class.

The decompiler is **not** allowed to define:

- Original Source status;
- historical file identity;
- original project boundary;
- package provenance;
- architectural intent.

Decompiler output may be excellent and buildable while still remaining reconstructed output.

**Governance:** TOOL-004 / D120.

---

# 8. PDB and symbol handling

## Portable PDB

Use `System.Reflection.Metadata` as the primary Portable PDB reader.

Directly recoverable observations may include:

- document names/paths;
- method debug information;
- sequence points;
- local scopes where available;
- source-link/custom debug information where applicable.

## Windows/native PDB

`Microsoft.DiaSymReader.Native` is an **optional Windows-specific adapter** for cases where native Windows PDB support materially improves recovery.

It must remain behind the symbol-reading port because:

- it is platform-specific;
- many recovery scenarios will not require it;
- Portable PDB remains cross-platform through `System.Reflection.Metadata`.

A missing/unreadable PDB is reported as artifact capability/diagnostic state, not as proof that the original build had no symbols.

**Governance:** TOOL-005 / D121.

---

# 9. C# syntax and semantic tooling

## Selected: Roslyn (`Microsoft.CodeAnalysis.CSharp`)

Roslyn is the canonical C# syntax/semantic engine for reconstructed source work.

Uses include:

- parsing decompiled/generated C#;
- syntax-tree transformation;
- formatting/normalization;
- declaration/member analysis;
- generated-source diagnostics;
- compilation-based semantic checks where appropriate;
- source comparison/normalization for validation;
- controlled code generation where syntax APIs are preferable to text concatenation.

Roslyn diagnostics about reconstructed code remain diagnostics about the reconstruction.

They do not become Original-System Evidence.

**Governance:** TOOL-006 / D122.

---

# 10. Project construction, evaluation, and builds

LegacyRevive.NET deliberately separates **project model operations** from **build execution**.

## 10.1 Project construction/evaluation

### Selected: `Microsoft.Build`

Use the public MSBuild object model for:

- creating/editing candidate project files;
- project-property/item representation;
- project evaluation where necessary;
- project graph information where appropriate.

### Selected: `Microsoft.Build.Locator`

Use `Microsoft.Build.Locator` before MSBuild object-model access where an installed MSBuild/.NET SDK toolset is required.

The registered toolset identity becomes part of relevant build/evaluation provenance.

## 10.2 Restore/build execution

Prefer an **out-of-process** adapter invoking the selected installed `dotnet`/MSBuild toolset for restore/build operations rather than treating in-process MSBuild execution as the canonical execution path.

Reasons:

- stronger isolation from the LegacyRevive host process;
- fewer assembly-load/toolset conflicts;
- easier capture of exact command/environment/tool identity;
- closer alignment with how developers/CI actually invoke builds;
- failed/hostile legacy build logic is less able to contaminate the recovery process.

In-process MSBuild APIs remain appropriate for controlled project construction/evaluation.

The currently verified `net10.0`-compatible Microsoft.Build line is used rather than mechanically adopting a package that targets only a newer runtime.

**Governance:** TOOL-007 / D123.

---

# 11. NuGet and package analysis

## 11.1 Local package/package-metadata analysis

### Selected: `NuGet.Packaging`

Use for:

- `.nupkg` inspection;
- `.nuspec` reading;
- package identity/version;
- dependency groups;
- package content/assets;
- package metadata required for recovery.

Local package artifacts are analyzed as supplied Recovery Input.

## 11.2 Remote feed access

### Optional adapter: `NuGet.Protocol`

Remote feed access is **not part of direct original-artifact evidence by default**.

When explicitly enabled, it may enrich recovery by:

- locating package versions;
- querying package metadata;
- downloading candidate packages/assets;
- investigating dependencies.

Externally retrieved feed information must retain provenance as external enrichment and must not be misreported as information recovered from the deployed artifact set.

Network access should therefore be explicit/configurable, especially for controlled blind benchmarks and sensitive enterprise environments.

**Governance:** TOOL-008 / D124.

`DOC-020` is Resolved: this selection is a contingent tooling choice for the post-MVP external-enrichment capability governed by `15-roadmap.md`. Selecting `NuGet.Protocol` here does not place remote-feed enrichment in the MVP or bypass the roadmap promotion/governance process.

---

# 12. Persistence

## Selected: SQLite via `Microsoft.Data.Sqlite`

The default local persistence technology is SQLite accessed through `Microsoft.Data.Sqlite`.

The authoritative persistence model should use an explicit schema/repository layer rather than allowing a generic ORM model to define LegacyRevive domain semantics.

SQLite is suitable for the initial product because it provides:

- single-file local storage;
- transactions;
- indexes and relational integrity;
- portability;
- straightforward backup/copy for recovery workspaces;
- enough relational structure to represent history and support/provenance edges;
- no external database service requirement.

The logical stores from `12-architecture.md` remain distinct in schema/repository boundaries even when physically stored in the same SQLite database.

Large binary Original Artifacts should normally remain content files in the workspace/artifact store, with hashes/metadata/locations persisted in SQLite rather than duplicating every binary into relational rows.

A later persistence replacement is allowed only if it preserves the canonical history/provenance/checkpoint semantics.

**Governance:** TOOL-009 / D125.

---

# 13. Serialization and structured interchange

## Platform/BCL

Use `System.Text.Json` for canonical JSON serialization of:

- portable reports;
- manifests;
- configuration/state exports;
- validation/metric results;
- deterministic test fixtures where suitable.

Use explicit DTO/contracts for durable exported formats.

Do not serialize arbitrary internal object graphs and treat them as a long-term schema.

Where deterministic output matters:

- define stable ordering;
- avoid dependence on dictionary enumeration as semantic ordering;
- version durable export formats;
- preserve IDs/provenance explicitly.

## XML

Use `System.Xml.Linq` / platform XML APIs for raw XML/configuration inspection and preservation where schema-specific higher-level parsing is not required.

Original XML should be preserved separately when exact raw content is recovery evidence.

**Governance:** TOOL-010 / D126.

---

# 14. Hosting, dependency injection, logging, and CLI

## Selected: Generic Host / Microsoft.Extensions

Use:

- `Microsoft.Extensions.Hosting`;
- `Microsoft.Extensions.DependencyInjection`;
- `Microsoft.Extensions.Logging`;
- `Microsoft.Extensions.Configuration` as needed for LegacyRevive's own host configuration.

The composition root belongs in the host layer.

The Domain/Core remains container-agnostic.

## Selected CLI: `System.CommandLine`

`System.CommandLine` is the canonical CLI parser/invocation library.

CLI commands map to Application-layer use cases rather than implementing recovery logic directly.

The CLI is a host, not the domain architecture.

Future UI/API hosts may reuse the same Application/Domain services.

**Governance:** TOOL-011 / D127.

---

# 15. Process execution

## Platform/BCL: `System.Diagnostics.Process`

External tool execution is implemented behind a process-execution Capability Port using `System.Diagnostics.Process`.

Required adapter behavior includes:

- explicit executable/tool identity;
- argument-list construction without unsafe shell-string composition;
- working directory;
- environment-variable control;
- stdout/stderr capture;
- cancellation/timeout support;
- exit-code capture;
- provenance of the effective command/tool version where relevant;
- no shell invocation unless a capability explicitly requires it.

A convenience process library is not selected at this stage because the platform API is sufficient behind the canonical port.

**Governance:** TOOL-012 / D128.

---

# 16. Generated identifiers and content identity

## Generated domain identities

Use .NET's `Guid.CreateVersion7()` for new LegacyRevive domain record identities where a GUID is appropriate.

Reasons include:

- platform support;
- globally unique IDs;
- time-ordered characteristics useful for append-heavy histories;
- no additional ID dependency.

## Artifact content identity

Use SHA-256 hashes for immutable artifact-content identity/checking.

A content hash complements but does not replace the stable Artifact domain identity.

For example:

```text
ArtifactId     = LegacyRevive record identity
SHA-256        = observed content identity
Original path  = provenance/location attribute
```

Generated UUIDv7 IDs must never be mistaken for historical IDs recovered from the subject application.

**Governance:** TOOL-013 / D129.

---

# 17. Testing

## Selected: xUnit v3

Use `xunit.v3` as the primary automated test framework.

The test strategy should include distinct suites for:

- Domain/Core rule tests;
- analyzer golden/corpus tests;
- inference-rule tests;
- support-graph/cycle tests;
- reconstruction tests;
- persistence/replay tests;
- CLI/application tests;
- integration tests with MSBuild/decompiler/package tooling;
- controlled validation-corpus tests;
- determinism/regression tests.

Tests that use known original source/reference must preserve the same recovery-versus-validation isolation rules as the product.

A test helper must not accidentally give the recovery path access to ground truth that production would not have.

`16-development-process.md` owns when material implementation slices require Development Slice Specifications, observable acceptance contracts, executable acceptance verification, and supporting verification. This document continues to own the selected testing framework and related test-tooling choices.

**Governance:** TOOL-014 / D130; DEV-001 / D168 for the development-process boundary.

---

# 18. Dependency security, licensing, and provenance

All direct external dependencies must have a recorded dependency review covering:

- package identity;
- source repository/publisher;
- license;
- supported target frameworks;
- maintenance status;
- relevant security advisories/vulnerabilities;
- reason for inclusion;
- architectural capability/port served;
- version selected;
- replacement/upgrade notes where material.

The selected baseline intentionally favors Microsoft platform packages plus specialist dependencies with permissive licenses.

Current selected third-party/non-BCL licenses include:

- `ICSharpCode.Decompiler` — MIT;
- xUnit — Apache-2.0;
- NuGet client libraries — Apache-2.0;
- Microsoft packages used here — permissive Microsoft/.NET open-source licensing as published by their package/project sources.

Automated vulnerability/dependency checking should be part of CI, but a vulnerability scanner does not automatically decide whether a recovered subject package was historically present; tooling dependency security and recovered-application dependency evidence are different concerns.

**Governance:** TOOL-015 / D131.

---

# 19. Explicitly not selected

The following are intentionally **not canonical dependencies at this stage**.

## 19.1 General-purpose ORM

EF Core is not selected for LegacyRevive's own state store.

This does not prohibit inspecting/reconstructing subject applications that use EF/EF Core.

## 19.2 Graph database

The Support Graph is a domain concept, not a mandate for a graph database.

SQLite relational edge tables are sufficient for the baseline.

## 19.3 Assembly-loading reflection as primary analysis

`Assembly.Load*` is not the canonical static-analysis path.

## 19.4 Runtime plugin framework

MEF or another dynamic plugin framework is not selected merely because the architecture has extension contracts.

Compile-time/DI registration can implement the contracts initially; runtime third-party plugin loading belongs to later scope if justified.

## 19.5 Separate shell/process library

No dependency such as a CLI process wrapper is selected while the platform `Process` API is sufficient behind the port.

## 19.6 Alternative decompiler as parallel authority

LegacyRevive may evaluate alternative engines later, but multiple engines are not treated as independent historical evidence merely because they produce similar C# from the same IL.

---

# 20. Package isolation rules

Concrete tooling packages should be confined to the adapter/capability projects that require them.

Examples:

```text
LegacyRevive.Domain
    X no ICSharpCode.Decompiler
    X no Microsoft.Build
    X no Microsoft.Data.Sqlite
    X no NuGet.Protocol

LegacyRevive.Analysis.Managed
    → System.Reflection.Metadata

LegacyRevive.Decompilation.ILSpy
    → ICSharpCode.Decompiler

LegacyRevive.Infrastructure.Sqlite
    → Microsoft.Data.Sqlite

LegacyRevive.Build.MSBuild
    → Microsoft.Build
    → Microsoft.Build.Locator

LegacyRevive.Packages.NuGet
    → NuGet.Packaging
    → optional NuGet.Protocol
```

The exact project names remain implementation choices, but dependency direction is canonical. In particular, outer adapter/capability projects reference Application-owned ports by default. They should not take a direct Domain/Core project dependency merely for convenience; a direct Domain/Core reference is added only when implementing an inward-owned capability contract genuinely requires Domain/Core-owned types or behaviour. This can include an Application-owned port that exposes Domain/Core types or, where canonically justified, a Domain/Core-owned port. The host/composition root may reference the concrete adapters it composes.

**Governance:** ARCH-013 / D161; host composition remains governed by TOOL-011 / D127.

---

# 21. Upgrade policy

Tooling evolves faster than the LegacyRevive domain model.

Therefore:

1. dependencies are centrally pinned;
2. automated update suggestions may be used;
3. upgrades run unit/integration/corpus regression suites;
4. decompiler upgrades receive special regression testing because generated source may change without historical evidence changing;
5. MSBuild/SDK upgrades record toolset changes because evaluation/build outcomes can vary;
6. persistence migrations preserve prior workspace history;
7. metric/comparator changes that alter benchmark interpretation are versioned according to `11-recovery-metrics.md`;
8. a package update that changes canonical semantics triggers governance review rather than being accepted as routine maintenance.

---

# 22. Network and external-service policy

The baseline recovery workflow is **local/offline capable**.

Network access is capability-specific and explicit.

Examples that may require opt-in network access:

- NuGet feed enrichment;
- package retrieval;
- future symbol/source-server lookup;
- future vulnerability metadata retrieval.

Network-derived material must retain provenance distinct from supplied Recovery Input.

Controlled validation/benchmark runs may disable network access entirely to preserve a known evidence ceiling.

This policy prevents hidden external knowledge from contaminating claims about what was recoverable from the supplied artifacts.

---

# 23. Tool output provenance

Every tool adapter that materially affects recovery should expose enough provenance to identify:

- tool/library name;
- effective version;
- capability;
- input artifact(s)/IDs;
- relevant settings/options;
- execution environment where material;
- diagnostics/failure state.

For deterministic direct extraction, version provenance helps reproduce observations.

For decompilation/build/validation, it is essential because outputs can legitimately change between tool versions.

Tool provenance does not make third-party tool output automatically authoritative; the canonical Evidence Model still determines epistemic category.

---

# 24. Tooling invariants

The following are canonical:

1. LegacyRevive's primary runtime is .NET 10 LTS;
2. package versions are centrally and explicitly managed;
3. `System.Reflection.Metadata` is the primary direct managed PE/metadata/Portable-PDB extraction API;
4. `ICSharpCode.Decompiler` is the selected C# decompilation engine;
5. Windows native PDB reading is an optional adapter rather than a cross-platform core dependency;
6. Roslyn is the selected C# syntax/semantic engine;
7. MSBuild object-model operations use `Microsoft.Build` with `Microsoft.Build.Locator` where applicable;
8. canonical restore/build execution is out-of-process through an explicit tool adapter;
9. local NuGet package inspection uses `NuGet.Packaging`;
10. remote NuGet access is optional enrichment and retains external provenance;
11. SQLite/`Microsoft.Data.Sqlite` is the default local state store;
12. JSON/XML use .NET platform serializers/parsers behind explicit contracts;
13. Generic Host/Microsoft DI/logging and `System.CommandLine` are the baseline host/CLI stack;
14. external process execution uses a controlled `System.Diagnostics.Process` adapter;
15. generated record IDs use platform UUIDv7 where GUID identity is appropriate; artifacts also retain SHA-256 content hashes;
16. xUnit v3 is the baseline test framework;
17. external dependencies require license/security/source review;
18. concrete dependencies remain isolated from the Domain/Core;
19. dependency upgrades cannot silently change recovery semantics;
20. the baseline recovery workflow remains local/offline capable;
21. LegacyRevive.NET's own repository solution uses the `.slnx` format.

---

# 25. Governance mapping

| Qualification | Durable decision | Canonical rule |
|---|---|---|
| TOOL-001 | D117 | .NET 10 LTS is the application/runtime baseline |
| TOOL-002 | D118 | Central explicit dependency/version governance |
| TOOL-003 | D119 | `System.Reflection.Metadata` is primary managed metadata extraction |
| TOOL-004 | D120 | `ICSharpCode.Decompiler` is the selected decompilation engine |
| TOOL-005 | D121 | Portable PDB first; native Windows PDB optional adapter |
| TOOL-006 | D122 | Roslyn is the selected C# syntax/semantic engine |
| TOOL-007 | D123 | MSBuild object model + Locator; builds execute out-of-process |
| TOOL-008 | D124 | NuGet.Packaging local; NuGet.Protocol explicit external enrichment |
| TOOL-009 | D125 | SQLite via Microsoft.Data.Sqlite is default local persistence |
| TOOL-010 | D126 | System.Text.Json/XML platform APIs for structured interchange |
| TOOL-011 | D127 | Generic Host/Microsoft DI/logging + System.CommandLine |
| TOOL-012 | D128 | System.Diagnostics.Process behind controlled execution port |
| TOOL-013 | D129 | UUIDv7 generated identities and SHA-256 artifact hashes |
| TOOL-014 | D130 | xUnit v3 baseline testing stack |
| TOOL-015 | D131 | Dependency license/security/provenance review is mandatory |
| TOOL-016 | D162 (Superseded) | Historical decision selecting `.slnx` and the former `LegacyRevive.slnx` filename |
| TOOL-017 | D169 | LegacyRevive.NET repository retains `.slnx` and uses `LegacyRevive.Net.slnx` as the canonical root solution filename |

No open tooling qualification remains after this governed revision. TOOL-016/D162 remain preserved as superseded historical direction; TOOL-017/D169 define the current repository solution naming state.

---

# 26. Boundary with `14-mvp.md`

This document selects the **technology set and dependency rules**.

It does **not** mean every selected/optional capability must ship in the first MVP.

`14-mvp.md` owns:

- which recovery scenarios ship first;
- which artifact analyzers are MVP-required;
- which reconstruction/validation capabilities are MVP-required;
- CLI command surface;
- which optional adapters are deferred;
- MVP platform/environment support;
- acceptance criteria.

The MVP should use these selected technologies for capabilities it includes unless it creates a governed reason to differ.

---

# 27. Verification sources

The selected baseline was checked against current upstream documentation/package metadata on 2026-09-25, including:

- official .NET support policy;
- Microsoft Learn documentation for the .NET 10 `.slnx` default and `dotnet sln` support;
- Microsoft Learn documentation for System.CommandLine, MSBuild APIs/Locator, dependency injection, and Microsoft.Data.Sqlite;
- official NuGet package metadata for Microsoft.Build, Microsoft.Build.Locator, Microsoft.CodeAnalysis.CSharp, NuGet.Packaging, NuGet.Protocol, Microsoft.Data.Sqlite, System.CommandLine, Microsoft.Extensions.Hosting, Microsoft.DiaSymReader.Native, and xunit.v3;
- the official ILSpy repository and ICSharpCode.Decompiler package metadata.

These sources verify current availability/compatibility/licensing but do not replace LegacyRevive's own regression testing before package upgrades.

---

# 28. Governance relationship

This document is constrained by the earlier canonical document set, especially `12-architecture.md`.

`16-development-process.md` owns the implementation-development process and Development Slice Specification discipline. This document owns the technologies used to implement that verification discipline and must not redefine its lifecycle, acceptance, or completion semantics.

Durable tooling decisions are owned by:

`90-decisions.md`

Qualification history/status are owned by:

`91-design-qualification-register.md`

A future material replacement of a selected technology should create a new qualification and, where appropriate, a superseding decision instead of silently rewriting the historical choice.
