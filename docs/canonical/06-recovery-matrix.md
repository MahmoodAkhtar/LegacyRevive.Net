# LegacyRevive.NET — Recovery Matrix

## Status

**Canonical**

This document defines the authoritative LegacyRevive.NET artifact-recovery matrix.

It describes:

- what categories of recovery information different surviving artifact types can contribute;
- whether that contribution is direct, inferential, heuristic, or not established by the artifact alone;
- important artifact-specific limitations;
- how companion artifacts may strengthen recovery without violating provenance or anti-circularity rules.

It does **not** promise that every instance of an artifact contains every theoretically possible datum.

It also does not define the extraction implementation or tooling used to inspect each artifact.

---

# 1. Purpose

LegacyRevive.NET starts from surviving artifacts whose recoverability varies significantly.

The Recovery Matrix exists to answer:

> **Given this artifact type, what can it contribute to recovery, what may reasonably be inferred from it, and what must not be claimed from it alone?**

The matrix is therefore a capability and evidence-boundary document.

It is not a guarantee of complete recovery.

---

# 2. Matrix contribution classes

Each recovery contribution is classified using one of four categories.

## 2.1 Direct

**Direct** means the relevant information is explicitly present in the artifact and can be read or deterministically decoded without a recovery inference.

A Direct contribution can support an Observation and, where the Evidence Model rules are satisfied, a Recovered Fact.

Examples:

- an assembly identity stored in managed assembly metadata;
- an XML configuration value stored in a configuration file;
- a document path stored in a matching PDB.

## 2.2 Inferable

**Inferable** means the artifact provides meaningful support for a conclusion, but the conclusion goes beyond what the artifact directly states.

The result is an Inference, not a Recovered Fact.

Example:

- assembly references may support an inference about likely project or package dependencies, but they do not directly prove the original project-file declarations.

## 2.3 Heuristic

**Heuristic** means the artifact may contribute a weak or pattern-based signal that is useful only when combined with context, convention observations, or other evidence.

Heuristic contributions must remain explicitly weaker than Direct or strongly supported inferential contributions.

Example:

- namespace clustering may help infer likely project boundaries.

## 2.4 Not established by this artifact alone

This means the artifact does not, by itself, justify the conclusion.

It does **not** mean the information is impossible to recover from the wider artifact set.

For example:

- an assembly alone does not establish the exact original `.csproj` file;
- `.runtimeconfig.json` alone does not establish all original MSBuild properties;
- an XML documentation file alone does not establish method bodies.

---

# 3. Artifact type capability is not artifact instance capability

The matrix describes what an artifact type **may** contain or contribute when the relevant information is present.

A particular artifact instance may be:

- missing data;
- stripped;
- optimized;
- metadata-only;
- generated differently;
- corrupted;
- incompatible with an expected format;
- only one part of a multi-artifact deployment.

Therefore:

> **Artifact-type capability must never be reported as though it were content actually observed in a specific artifact instance.**

LegacyRevive.NET must inspect the actual artifact and produce Observations from what is present.

---

# 4. Absence rule

The absence of an artifact from the supplied recovery set normally establishes only:

> **the artifact is not currently available to this recovery.**

It does not automatically establish:

> **the artifact never existed in the original build or deployment process.**

Examples:

- no `.pdb` supplied does not prove that no PDB was ever produced;
- no XML documentation file supplied does not prove that source contained no XML documentation comments;
- no `.deps.json` supplied does not by itself prove that the original application never generated one.

A stronger historical conclusion about absence requires independent support.

---

# 5. Companion-artifact rule

Artifacts may corroborate one another.

Examples include:

- managed assembly + matching PDB;
- assembly + XML documentation file;
- application assembly + `.deps.json`;
- application assembly + `.runtimeconfig.json`;
- main assembly + satellite resource assemblies;
- assembly + configuration.

However, corroboration requires:

1. a defensible relationship between the artifacts;
2. provenance for that relationship;
3. support-path independence where additional confidence is claimed.

Filename similarity alone is not always sufficient to prove identity or historical pairing.

Where format-supported identities/checksums/metadata relationships are available, they should be preferred.

---

# 6. Managed implementation assemblies — `.dll` / managed `.exe`

This row applies when the artifact is a managed CLI implementation assembly.

## Direct contributions

A managed implementation assembly may directly provide:

- assembly identity;
- assembly version/culture/public-key metadata where present;
- referenced assemblies;
- defined types;
- type names and namespaces;
- visibility;
- base types;
- implemented interfaces;
- members and signatures;
- generic metadata;
- custom attributes;
- manifest resources;
- managed method bodies / IL where present;
- entry-point metadata for executable assemblies where present;
- other CLR metadata encoded in the assembly.

## Inferable contributions

It may support inference about:

- likely project boundaries;
- dependency relationships;
- likely package use;
- source-level type organization;
- source reconstruction;
- application layering;
- likely framework/runtime family in combination with other evidence.

## Heuristic contributions

It may provide weaker signals for:

- original source-file grouping;
- original folder structure;
- architectural conventions;
- project naming;
- subsystem grouping;
- coding conventions visible only indirectly through compiled representation.

## Not established by the assembly alone

A managed assembly does not by itself reliably establish:

- exact original source text;
- comments;
- formatting;
- exact source-file boundaries;
- exact original local-variable names in all builds;
- exact original `.csproj`;
- exact package references as originally declared;
- exact original solution structure;
- original developer intent.

---

# 7. Reference assemblies

A reference assembly must be distinguished from an implementation assembly.

## Direct contributions

A reference assembly may directly provide metadata needed to represent an API surface, including relevant type/member declarations and reference metadata.

## Important limitation

Reference assemblies do not provide usable implementation bodies in the way implementation assemblies do.

Therefore method/source reconstruction from implementation IL must not be claimed merely because a `.dll` exists.

LegacyRevive.NET should identify reference-assembly characteristics where possible before assigning implementation-recovery capabilities.

---

# 8. Native hosts and non-managed executables

A `.exe` filename does not guarantee that the file itself is a managed CLI implementation assembly.

Modern .NET deployment may include native host executables or other native binaries.

For a non-managed executable:

- PE/native information may still be directly inspectable;
- managed metadata/IL recovery rules from §6 do not automatically apply;
- associated managed assemblies and runtime metadata may carry the recoverable application structure.

The matrix therefore classifies by actual artifact format, not extension alone.

---

# 9. Classic XML configuration — `.config`, `app.config`, `web.config`

This category covers surviving XML configuration material associated with .NET Framework or other applications using these formats.

## Direct contributions

Where present, configuration may directly provide:

- raw XML structure;
- named configuration sections;
- application settings;
- connection-string declarations;
- assembly/type names written into configuration;
- endpoint/binding/behavior declarations;
- service/client configuration;
- other configuration values explicitly stored in the file.

## Inferable contributions

Configuration may support inference about:

- runtime wiring;
- external dependencies;
- service topology;
- activated features;
- expected assemblies/types;
- deployment/runtime assumptions.

## Heuristic contributions

Configuration naming/grouping may help infer:

- subsystem boundaries;
- environment-specific conventions;
- likely project relationships.

## Not established by configuration alone

Configuration does not by itself establish:

- that every configured path was executed;
- that every configured type was present in the supplied artifact set;
- exact source implementations;
- exact original project files;
- effective runtime values after external/environment overrides.

---

# 10. WCF configuration

WCF configuration is treated as a specialized configuration contribution because it can carry particularly rich service-model information.

## Direct contributions

Where present, `<system.serviceModel>` configuration may directly provide items such as:

- configured services;
- configured client endpoints;
- endpoint addresses;
- contract/type names written into configuration;
- binding types/configuration names;
- behavior configuration;
- service-hosting configuration;
- other explicit WCF XML settings.

## Inferable contributions

It may support inference about:

- likely service/client relationships;
- service topology;
- expected contracts/implementations;
- hosting structure;
- external communication dependencies.

## Important limitation

Configuration describes configured runtime behavior; it does not by itself reconstruct:

- service implementation code;
- contract source code;
- whether every configuration path was used in production;
- exact original hosting/project organization.

---

# 11. PDB / debugging symbols

PDB capability depends on the PDB format and the information emitted by the compiler/build.

## Direct contributions

A matching PDB may directly provide some combination of:

- source document names/paths;
- method-to-document mappings;
- sequence points;
- source line/column mappings;
- local-variable/scope information where emitted;
- compiler/debug custom information;
- Source Link information where emitted;
- embedded source where emitted.

## Inferable contributions

PDB information may strongly support inference about:

- original source-file boundaries;
- source-tree layout;
- type-to-source-file placement;
- source naming;
- generated versus hand-authored source relationships.

## Important limitations

A PDB does not guarantee:

- complete original source content;
- comments;
- all local names;
- all source documents;
- source availability at referenced paths;
- exact project structure.

Embedded source is a special case: if present, that embedded content is directly recoverable from the PDB rather than inferred.

A PDB should be associated with the intended binary using format-supported identity/matching information where possible.

---

# 12. XML documentation files

A compiler-generated XML documentation file may directly provide:

- documented assembly identity/name;
- member documentation identifiers;
- XML documentation text emitted from source comments;
- references within those comments such as types/members where encoded.

## Inferable contributions

It may help infer:

- API purpose;
- domain terminology;
- intended relationships described by documentation;
- names and signatures to correlate with an assembly.

## Important limitations

An XML documentation file does not by itself provide:

- method bodies;
- private implementation details not documented;
- complete source comments if documentation generation was partial;
- source-file layout;
- original project structure.

The documentation text is historical source-derived material where present, but its descriptive claims should still be distinguished from executable behavior.

---

# 13. `.deps.json`

Where present for an application, `.deps.json` is a dependency manifest produced by the .NET build/runtime toolchain.

## Direct contributions

It may directly provide information about:

- application dependency context;
- compile/runtime libraries represented in the manifest;
- target/runtime information represented in the manifest;
- runtime assets;
- dependency relationships represented by the file;
- package/library identities and versions represented by the file.

## Inferable contributions

It may support inference about:

- likely package references;
- dependency closure;
- runtime deployment expectations;
- likely project dependency needs.

## Important limitations

`.deps.json` does not by itself establish:

- the exact original `.csproj`;
- which dependencies were direct project declarations versus transitive;
- all build-time references that did not flow to the manifest;
- developer intent behind a dependency.

---

# 14. `.runtimeconfig.json`

Where present, `.runtimeconfig.json` records runtime configuration for an application.

## Direct contributions

It may directly provide:

- target framework/runtime framework information represented in the file;
- runtime framework version constraints represented in the file;
- runtime configuration properties explicitly emitted there.

## Inferable contributions

It may support inference about:

- the runtime family required by the application;
- likely SDK/runtime-era characteristics;
- runtime behavior expected from explicit configuration.

## Important limitations

It does not by itself establish:

- the full original project file;
- every original MSBuild property;
- environment variables effective at runtime;
- programmatic runtime switches;
- deployment settings not represented in the file.

---

# 15. Embedded manifest resources

Managed assemblies may contain embedded manifest resources.

## Direct contributions

Where present they may directly provide:

- resource names;
- embedded resource payloads;
- resource manifests/streams that can be decoded according to their format;
- the containing assembly relationship.

## Inferable contributions

They may support inference about:

- UI/text/resource usage;
- embedded templates or configuration;
- localization/resource organization;
- source/project resource ownership.

## Important limitations

The containing assembly establishes packaging ownership, not necessarily the exact original source-file or project-item configuration used to produce the resource.

---

# 16. Satellite assemblies / `.resources.dll`

Satellite assemblies are resource-oriented companion assemblies.

## Direct contributions

Where present they may directly provide:

- assembly/resource identity;
- culture metadata;
- localized resources;
- resource names and payloads.

## Inferable contributions

They may support inference about:

- supported cultures;
- resource organization;
- relationship to a main application/library assembly.

## Important limitation

Satellite assemblies are not evidence of application implementation code merely because they use a `.dll` extension.

They must not be treated like ordinary implementation assemblies for source-code recovery.

---

# 17. NuGet package / package metadata

This category applies where surviving package artifacts or package metadata are actually available.

Examples may include a `.nupkg`, `.nuspec`, package assets, lock/package metadata, or equivalent surviving package information.

## Direct contributions

Depending on the surviving form, package material may directly provide:

- package identity;
- package version;
- declared dependencies;
- target-framework asset groups;
- packaged assemblies/content/build assets;
- package metadata explicitly stored in the artifact.

## Inferable contributions

It may support inference about:

- likely package references required for reconstruction;
- dependency intent at package-author level;
- build/runtime assets needed by a reconstructed project.

## Important limitations

Package metadata found in a deployment does not automatically establish:

- that the package was a direct reference in the original project;
- the exact original PackageReference declaration;
- the exact package-management style used by the original solution.

---

# 18. Build / deployment metadata

This category covers additional surviving build or deployment outputs not given a more specific row in this matrix.

Examples may include:

- publish manifests;
- deployment manifests;
- generated build metadata;
- file inventories;
- platform/runtime markers;
- environment-specific deployment descriptors.

## Direct contributions

Only the content explicitly present in the artifact is Direct.

## Inferable contributions

Such metadata may support inference about:

- deployment topology;
- expected runtime environment;
- build/publish choices;
- file ownership/grouping;
- dependency relationships.

## Important limitation

This is a heterogeneous category.

LegacyRevive.NET must first classify the actual file/format before assigning artifact-specific recovery capability.

---

# 19. Directory and deployment layout

File and directory placement may itself be observed.

Examples include:

- binaries colocated in one deployment directory;
- culture-specific subdirectories;
- plugin folders;
- configuration placed alongside a host executable.

## Direct contributions

The supplied recovery set can directly establish:

- the observed path;
- filename;
- directory relationship at the time the artifact set was captured.

## Inferable / heuristic contributions

Layout may support inference about:

- deployment grouping;
- plugin boundaries;
- resource relationships;
- likely application boundaries.

## Important limitation

Deployment layout is not automatically source-repository layout or original project structure.

---

# 20. Cross-artifact recovery

The strongest recovery often comes from combining independently surviving artifacts.

Examples:

```text
Assembly metadata
+ matching PDB
+ XML documentation
+ .deps.json
+ .runtimeconfig.json
+ configuration
```

may collectively provide much more recovery value than any single artifact.

However, combination must preserve the epistemic category of each conclusion.

Multiple artifacts do not turn an inference into a Recovered Fact unless the direct-support rule in `03-evidence-model.md` is actually satisfied.

---

# 21. Artifact relationship confidence

A relationship between two artifacts may itself be:

- Directly supported;
- Inferred;
- Heuristic;
- Unresolved.

Examples:

- a PDB identity that matches a binary may directly support the pairing;
- identical/similar filenames may only support an inference;
- directory co-location may be heuristic.

LegacyRevive.NET must preserve the provenance and classification of the relationship before using one artifact to interpret another.

---

# 22. What the Recovery Matrix must not do

The matrix must not:

1. claim observed data merely because the artifact type could contain it;
2. treat file extensions as sufficient proof of artifact format;
3. infer historical nonexistence from missing recovery artifacts;
4. treat generated reconstruction as new Original-System Evidence;
5. treat companion artifacts as related without a defensible basis;
6. collapse Direct, Inferable, and Heuristic contributions;
7. claim exact original project/source recovery where only approximate reconstruction is supported;
8. assign one opaque recoverability score that hides different contribution types.

---

# 23. Artifact-instance recovery profile

For an actual recovery run, LegacyRevive.NET should produce an artifact-instance profile derived from the matrix.

That profile should record, at minimum:

- artifact identity/path;
- detected artifact format/type;
- whether parsing/inspection succeeded;
- direct Observations obtained;
- candidate inferential contributions;
- applicable limitations;
- relationships to companion artifacts;
- unresolved/corrupt/unsupported conditions.

The matrix defines the capability model.

The artifact-instance profile records what was actually found.

---

# 24. Recovery ceilings

Some information has a realistic recovery ceiling even when many artifacts survive.

Examples include:

- exact source formatting;
- comments not preserved in surviving source-derived artifacts;
- original developer motivation;
- exact source-control history;
- exact project-file text where no project file survives;
- source constructs erased or transformed by compilation.

A recovery ceiling must be stated as:

> **not established or not reliably recoverable from the available artifact set**

unless the project has sufficient basis to make a stronger impossibility claim.

This avoids turning ordinary information loss into an absolute statement about all possible recovery contexts.

---

# 25. Matrix summary

| Artifact / family | Strongest typical Direct contribution | Common inferential contribution | Key limitation |
|---|---|---|---|
| Managed implementation `.dll` / `.exe` | CLR assembly/type/member metadata; references; IL/resources where present | project/dependency/source reconstruction | not exact original source/project layout |
| Reference assembly | API-oriented metadata | API dependency reconstruction | no usable implementation bodies |
| Native host / non-managed `.exe` | native/host file information | relationship to managed application artifacts | managed metadata rules do not automatically apply |
| `.config` / `web.config` / `app.config` | stored XML configuration | runtime wiring/dependencies | effective runtime behavior may include external overrides |
| WCF configuration | explicit service-model XML | service/client topology | no service implementation source |
| PDB | debugging/source mapping metadata; embedded source if present | source-file/tree reconstruction | content varies by format/build |
| XML documentation | emitted documentation entries/text | API/domain intent clues | no method implementation |
| `.deps.json` | dependency/runtime manifest data | likely project/package dependency needs | not exact original project declarations |
| `.runtimeconfig.json` | runtime framework/configuration data | runtime requirements | not complete project/build configuration |
| Embedded resources | packaged resource names/content | resource usage/ownership | not exact source project-item structure |
| Satellite `.resources.dll` | localized resources/culture | localization relationships | not implementation code |
| NuGet/package metadata | package identity/dependencies/assets where present | package reconstruction | deployment presence ≠ direct project reference |
| Build/deployment metadata | format-specific stored values | build/deployment topology | heterogeneous; classify actual format first |
| Directory/deployment layout | observed placement | grouping/plugin/application boundaries | deployment layout ≠ repository layout |

---

# 26. Qualifications resolved by this document

This document creates and resolves these Recovery Matrix qualifications:

| Qualification | Resolution |
|---|---|
| `RMAT-001` | Matrix contributions are classified as Direct, Inferable, Heuristic, or Not established by this artifact alone. |
| `RMAT-002` | Artifact-type capability does not imply the capability/data is present in a particular artifact instance. |
| `RMAT-003` | File extension is insufficient to classify managed implementation assemblies; reference assemblies and native hosts require distinct capability treatment. |
| `RMAT-004` | Missing artifacts mean unavailable-to-recovery, not historical nonexistence, unless independently established. |
| `RMAT-005` | Companion artifacts may corroborate only when their relationship is defensible and support-path independence is preserved. |
| `RMAT-006` | The matrix records artifact contribution and limitations; an artifact-instance profile records what was actually observed. |
| `RMAT-007` | Irrecoverability statements are scoped to the available artifact set unless a stronger impossibility claim is justified. |
| `RMAT-008` | Cross-artifact combination may strengthen recovery but does not erase evidence/inference distinctions or create one opaque recoverability score. |

---

# 27. Durable design consequences

The Recovery Matrix establishes these durable rules:

1. Artifact capabilities are conditional, not promises about every file.
2. Direct contribution is distinct from inference and heuristic signal.
3. Extension is not enough to determine artifact semantics.
4. Missing evidence is not evidence of historical absence by default.
5. Artifact relationships need provenance.
6. Companion-artifact corroboration must preserve independence.
7. Actual scan output must report observed instance capability, not theoretical type capability.
8. Recovery limitations remain explicit.
9. Recovery ceilings are scoped to the available artifact set.
10. Cross-artifact recovery preserves epistemic categories.

---

# 28. External technical baseline

The artifact-specific technical statements in this document were cross-checked against stable platform documentation where applicable, including:

- .NET assembly/metadata documentation;
- .NET reference-assembly documentation;
- Portable PDB format documentation;
- .NET dependency-context / `.deps.json` documentation;
- .NET runtime configuration documentation;
- .NET resource/satellite-assembly documentation;
- C# XML documentation output documentation.

These technical references inform artifact semantics.

The LegacyRevive.NET recovery classifications, governance rules, and design consequences remain Project decisions owned by this document, `90-decisions.md`, and `91-design-qualification-register.md`.

---

# 29. Non-goals

This document does not define:

- exact extraction APIs;
- decompiler choice;
- PDB-reader implementation;
- NuGet APIs;
- MSBuild APIs;
- binary parsing libraries;
- CLI commands;
- output report format;
- exact recovery stage ordering;
- project reconstruction algorithms;
- source reconstruction algorithms;
- MVP inclusion.

Those belong to later canonical documents.

---

# 30. Canonical ownership

This document owns:

- artifact-family recovery contribution classification;
- artifact-specific recovery limits;
- Direct / Inferable / Heuristic / Not-established matrix semantics;
- artifact-instance versus artifact-type capability distinction;
- missing-artifact semantics;
- companion-artifact relationship rules;
- recovery-ceiling wording.

`03-evidence-model.md` owns the underlying epistemic categories and provenance rules.

`05-convention-inference.md` owns convention-based heuristic pattern inference.

`07-recovery-process.md` will own when artifact discovery/analysis occurs.

`08-project-reconstruction.md` and `09-source-reconstruction.md` will consume matrix contributions without redefining what artifacts establish.

`13-tooling-and-dependencies.md` will own implementation tooling choices.

---

# 31. Governance relationship

This document is constrained by accepted decisions in `90-decisions.md`.

Its qualification history is recorded in `91-design-qualification-register.md`.

Future material changes must follow the normative governance lifecycle in `91`.
