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

# 5A. Stage 02 artifact format/family recognition contract

Stage 02 classification is a direct, artifact-instance conclusion about **what format/family the preserved bytes actually establish**. It is not filename classification, later semantic extraction, or inference about historical ownership/use.

## 5A.1 General recognition rule

A format/family classification is established only when the Artifact instance satisfies the directly observable structural conditions defined here for that family.

Filename, extension, relative path, and deployment position may:

- select efficient recognizers/probes;
- be retained as provenance/context;
- corroborate a content-established classification;
- contribute to a later companion-artifact relationship assessment.

They are **not sufficient by themselves** to establish actual format/family.

Stage 02 records the **most specific directly established classification**. If direct inspection establishes only a broader format, the broader classification remains current; LegacyRevive.NET must not manufacture a subtype merely because the subtype is common or because a filename suggests it.

## 5A.2 Canonical classification outcomes

Stage 02 must distinguish these outcomes where applicable:

1. **Established** — the required recognition conditions for a supported family are satisfied.
2. **Broader format established** — a broader valid format is established but a more specific family/subtype is not.
3. **Valid but unsupported** — a valid known format is directly established but the MVP does not provide the corresponding deep analyzer/capability. This is not corruption.
4. **Unsupported/unrecognized preserved content** — the bytes remain valid preserved input, but no supported or otherwise recognized format/family is established.
5. **Ambiguous** — two or more materially incompatible classifications remain directly supportable after permitted Stage 02 inspection. Candidate classifications and the reason for ambiguity remain explicit.
6. **Malformed/corrupt candidate** — content-level format evidence establishes a known-format candidate, but required structural conditions for that format fail. Filename/extension alone is insufficient to create this outcome.
7. **Operational classification failure** — classification could not complete because of an operational/tool/resource/cancellation failure rather than because the bytes failed a format rule. No successful or malformed classification is fabricated from that failure.

Parser rejection alone is not automatically evidence of corruption. The diagnostic must preserve whether rejection was caused by deterministic content invalidity, unsupported format/version, or an operational failure where that distinction can be established.

## 5A.3 Overlap and precedence

Recognition precedence is semantic rather than recognizer-order based:

- where one classification is a directly established specialization of another, retain the most specific directly established classification;
- where the broader format is established but subtype conditions are not, retain the broader format;
- where two materially incompatible family contracts are independently satisfied and neither canonically subsumes the other, preserve `Ambiguous` rather than selecting by recognizer order, extension, or convenience;
- recognition order must not change the substantive result for equivalent bytes and relevant configuration.

## 5A.4 Stage 02 / Stage 03 inspection boundary

Stage 02 may inspect internal structure **only as far as required to evaluate a recognition contract**. Such inspection may read metadata records, container entries, XML/JSON shape, resource presence, or other structural fields necessary to answer the classification question.

Stage 02 persists only classification-bearing state and classification diagnostics. It does not persist unrelated semantic content merely because the recognizer encountered it. Reusable direct content such as dependency entries, type/member metadata, configuration values, PDB documents/sequence points, package dependencies, or resource payloads belongs to Stage 03 direct extraction/normalization.

A later Stage 03 analyzer may reread the same preserved bytes. Avoiding duplicate reads is an implementation optimization and must not collapse the semantic boundary between classification and direct extraction.

## 5A.5 MVP-relevant recognition contracts

The following conditions define the minimum direct recognition contract for the current MVP families. They define classification truth conditions, not implementation APIs.

| Family / format | Directly established when | Insufficient / corroborating only | Key malformed / unsupported boundary |
|---|---|---|---|
| Managed CLI artifact | PE/COFF is structurally valid, contains a CLR/CLI header, and the referenced CLI metadata root is readable as valid CLI metadata. | `.dll` / `.exe` name; `MZ` alone. | A valid PE with a CLR header whose required CLI metadata cannot be structurally read is a malformed/corrupt managed candidate, not a native PE. |
| Managed CLI assembly (broad) | Managed CLI artifact plus a valid Assembly manifest/table establishes an assembly. | Presence of CLR metadata alone does not establish assembly rather than module. | A valid managed CLI module with no Assembly manifest is a valid managed module/netmodule; for MVP purposes it is valid-but-unsupported rather than malformed. |
| Reference assembly | Managed CLI assembly plus an assembly-level custom attribute whose metadata-resolved type is exactly `System.Runtime.CompilerServices.ReferenceAssemblyAttribute`. | Simple attribute type name alone; absence of method bodies alone; filename/path such as `ref/`. | Absence of the attribute does not by itself prove implementation-assembly status. |
| Satellite/resource assembly | Managed CLI assembly with non-neutral assembly culture, one or more manifest resources, and no executable implementation method bodies. | `.resources.dll` filename and culture-named directory are strong corroborating deployment signals but not sufficient alone; culture alone is insufficient. | Resource-bearing assemblies that do not satisfy the satellite conditions remain broader managed/resource-bearing assemblies; a companion-main-assembly relationship is Stage 04. |
| Managed implementation assembly | Managed CLI assembly that is not directly established as a reference or satellite assembly and contains direct implementation-bearing evidence such as one or more executable managed method bodies or an executable entry point backed by implementation metadata. | Merely being a managed assembly; absence of `ReferenceAssemblyAttribute`; `.dll` / `.exe` name. | A valid managed assembly lacking enough direct subtype evidence remains `Managed CLI assembly (broad)` rather than being forced into implementation/reference/satellite. |
| Native/non-managed PE | PE/COFF is structurally valid and no CLR/CLI header establishes a managed CLI artifact. | `.exe` / `.dll` extension. | A PE containing a CLR header with malformed CLI metadata is not reclassified as native merely because managed parsing failed. |
| Classic .NET XML configuration | XML is well-formed and the document element is un-namespaced `configuration`. | `.config`, `app.config`, `web.config` filename; XML syntax alone. | A content-level `<configuration` candidate that is not well-formed XML may be malformed configuration; a misleading filename without content-level family evidence remains unrecognized/unsupported. Section semantics are Stage 03. |
| Portable PDB | Standalone ECMA-335-style metadata is structurally readable and contains both required `#Pdb` and `#~` streams for standalone Portable PDB debugging metadata. | `.pdb` filename; metadata-root signature alone. | A valid other metadata blob is not a malformed Portable PDB. Content that establishes the Portable-PDB metadata shape but fails required stream structure is malformed/corrupt. Native Windows PDB remains optional MVP support under `14-mvp.md`. |
| XML documentation | XML is well-formed, the document element is `doc`, and a direct `members` child is present. | `.xml` filename; an `assembly/name` element is corroborating but not required for family recognition. | Member/documentation semantics and correlation to an assembly are Stage 03/04. A content-level `<doc` candidate that cannot be parsed may be malformed XML documentation. |
| `.deps.json` dependency context | JSON is well-formed; the root is an object; required top-level `runtimeTarget`, `targets`, and `libraries` members are objects; and `runtimeTarget.name` is a string. | `.deps.json` filename; isolated property names; arbitrary valid JSON. | Wrong required member shapes are malformed dependency-context candidates when the family-specific structure is otherwise established. Entry/cross-reference semantics are Stage 03. |
| `.runtimeconfig.json` | JSON is well-formed; the root is an object; `runtimeOptions` exists and is an object. | `.runtimeconfig.json` filename; presence of a `runtimeOptions` property with a non-object value. | A family-specific `runtimeOptions` member with invalid required container shape is a malformed runtime-configuration candidate. Individual runtime-option semantics are Stage 03. |
| `packages.config` | XML is well-formed with document element `packages`; each `package` entry used by the manifest has non-empty `id` and `version` attributes. | Filename alone; generic XML. | A `packages` manifest with structurally invalid package entries is a malformed packages-config candidate; dependency interpretation is Stage 03. |
| `.nuspec` | XML is well-formed with document element `package`, a direct `metadata` element, and non-empty package `id` and `version`. | `.nuspec` filename; generic `<package>` XML without NuGet metadata structure; namespace/schema URI is corroborating/version context rather than the sole family discriminator. | Missing/invalid NuGet manifest structure after family-specific content is established is malformed NuSpec; full package metadata/dependency extraction and schema-version validation are Stage 03. |
| `.nupkg` | The bytes form a readable ZIP package container and contain a root package `.nuspec` manifest that itself satisfies the NuSpec recognition contract. | `.nupkg` filename; ZIP signature/container alone. | A valid ZIP without a recognized NuSpec is a valid non-NuGet ZIP/unsupported format, not a malformed NuGet package. Corrupt ZIP structure with content-level package evidence may be a malformed NuGet-package candidate. |

These are **minimum recognition contracts**. Stage 03 may apply richer format validation while extracting semantic content, but later extraction failure does not retroactively rewrite a Stage 02 classification unless it reveals that the Stage 02 recognition contract itself was not actually satisfied; such a correction follows the normal append-preserving reevaluation/supersession rules.

### 5A.5.1 Malformed/corrupt family-candidate evidence thresholds

A `Malformed/corrupt candidate` claim is itself a family-classification claim and therefore requires direct structural support. It may be made only when Stage 02 can directly establish a **family candidate discriminator** independently of the structural condition that failed.

A family candidate discriminator is an exact structural fact, observed at the format-specific scope required by the family contract, that is sufficient to establish that the bytes are genuinely attempting or representing that known family even though the full successful-recognition contract cannot be completed. The discriminator must be established from preserved bytes/structure and must not be inferred from the failure itself.

The following are insufficient by themselves to establish malformed/corrupt family candidacy:

- filename, extension, supplied path, or directory placement;
- a raw substring, byte sequence, partial name/prefix collision, or incidental occurrence inside text, comments, string values, payloads, or unrelated embedded content;
- a parser exception or generic parser rejection;
- a generic outer-format marker where the claimed subtype/family requires stronger evidence;
- a tool/library-specific error category whose semantic meaning is not independently mapped to this contract.

Deterministic partial structural inspection is permitted when complete parsing cannot succeed. Such inspection must establish exact tokens/records and their structural scope—for example document element, top-level JSON member, metadata-stream directory entry, or archive entry—rather than searching raw text. Parser/tokenizer choice remains implementation discretion provided equivalent bytes produce the same canonical candidate result. If the required discriminator cannot be established, Stage 02 retains the broader valid format where one is directly established, otherwise preserves unsupported/unrecognized content plus diagnostics; it does not fabricate a malformed family claim.

For the current MVP families, the minimum malformed/corrupt candidate thresholds are:

| Family / format | Minimum candidate discriminator when full recognition fails | Boundary that must remain distinct |
|---|---|---|
| Managed CLI artifact | Structurally valid PE/COFF plus a directly established CLR/CLI header. Failure to structurally read the referenced CLI metadata may then support malformed/corrupt managed candidacy. | `MZ`, PE naming, or CLR-like bytes outside a valid PE/CLI structural relationship do not establish a managed candidate. |
| Managed CLI assembly/subtypes | A valid managed CLI artifact plus the relevant directly readable assembly/subtype structure required before the failing condition. Where subtype evidence is merely absent or insufficient, retain the broader managed CLI assembly/module result rather than creating malformed subtype state. | Failure to establish reference/satellite/implementation subtype conditions normally means broader classification, not malformed subtype candidacy. |
| Classic .NET XML configuration | Token-aware partial XML structure establishes the **document-element** start tag with exact local name `configuration`; where namespace binding is structurally determinable it must be un-namespaced. | `<configurationBackup`, occurrences in comments/text/attributes, or unrelated nested elements do not establish a configuration candidate. If namespace status cannot be structurally established, it must not be assumed. |
| Portable PDB | Standalone ECMA-335-style metadata structure is directly established far enough to identify an exact `#Pdb` stream-directory entry; failure of other required Portable-PDB stream structure, including required `#~`, may then support malformed/corrupt Portable-PDB candidacy. | Generic metadata, a metadata signature alone, `#~` alone, `.pdb` naming, or raw `#Pdb` byte occurrence outside a structurally identified stream entry is insufficient. |
| XML documentation | Token-aware partial XML structure establishes the **document-element** start tag with exact local name `doc`. | `<document`, comments/text/attributes, or unrelated nested `doc` text/elements do not establish document-family candidacy. |
| `.deps.json` dependency context | Token-aware partial JSON structure establishes a root object and exact **top-level** members `runtimeTarget`, `targets`, and `libraries`. Their required value/container shapes may then fail and support malformed dependency-context candidacy. | Isolated property-name text, names inside string values/nested objects, a subset of the three family-defining top-level members, or filename alone is insufficient. |
| `.runtimeconfig.json` | Token-aware partial JSON structure establishes a root object and an exact **top-level** `runtimeOptions` member. Its required object/container shape may then fail and support malformed runtime-configuration candidacy. | `runtimeOptions` text inside a string/nested object, raw textual occurrence, or filename alone is insufficient. |
| `packages.config` | Token-aware partial XML structure establishes the document element with exact local name `packages`. Invalid/malformed direct `package` entries may then support malformed packages-config candidacy. | Filename, generic XML, prefix/name collisions, or nested/incidental `packages` occurrences are insufficient. |
| `.nuspec` | Token-aware partial XML structure establishes document element `package` **and** a direct child `metadata` element. Required package `id`/`version` structure may then fail and support malformed NuSpec candidacy. | A generic `package` root alone, `.nuspec` naming, namespace/schema URI alone, or incidental `metadata` text is insufficient. |
| `.nupkg` | For a corrupt/unreadable ZIP container, partial archive structure must directly establish a **root** `.nuspec` entry and available manifest bytes must independently satisfy the NuSpec malformed-family candidate threshold above. | A readable ZIP without a NuSpec that satisfies the successful NuSpec recognition contract remains valid non-NuGet ZIP/unsupported. A root entry name ending `.nuspec`, raw `.nuspec` bytes, ZIP signature, or filename alone does not establish malformed NuGet-package candidacy. |

This candidate threshold is deliberately weaker than successful recognition only where the family identity can still be directly established independently of the failed structural condition. It is not permission to infer family membership from likelihood, naming, parser behavior, or implementation convenience.

**Governance:** RMAT-010 / D175.

## 5A.6 Representative verification boundary

Tests and fixtures must demonstrate the canonical contracts rather than define them. For each implemented recognizer, verification should include, where applicable:

- positive canonical examples;
- misleading-extension examples;
- broader-valid-format examples;
- valid-but-unsupported examples;
- malformed candidates with content-level family evidence;
- unsupported/unrecognized preserved content;
- overlapping/ambiguous cases;
- operational-failure mapping; and
- invariance to recognizer execution order.

**Governance:** RMAT-009 / D172; RMAT-010 / D175.

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
| `RMAT-009` | Stage 02 classification uses canonical direct format/family recognition contracts, preserves broad/unsupported/ambiguous/malformed/operational outcomes distinctly, and keeps reusable semantic extraction in Stage 03. |
| `RMAT-010` | Malformed/corrupt family candidacy requires a directly established family-specific structural discriminator; raw substrings, names, parser rejection, and incidental text cannot fabricate family membership. |

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
11. Stage 02 family classification is governed by direct format-specific recognition contracts rather than extension or recognizer order, and valid-but-unsupported artifacts remain distinct from malformed content.
12. Malformed/corrupt family candidacy requires a direct family-specific structural discriminator at the correct scope; partial structural inspection may establish that discriminator, but raw substrings, name collisions, parser rejection, and generic outer-format evidence cannot.

---

# 28. External technical baseline

The artifact-specific technical statements in this document were cross-checked against stable platform documentation where applicable, including:

- .NET assembly/metadata documentation;
- .NET reference-assembly documentation;
- Portable PDB format documentation;
- .NET dependency-context / `.deps.json` documentation;
- .NET runtime configuration documentation;
- .NET resource/satellite-assembly documentation;
- C# XML documentation output documentation;
- NuGet package, NuSpec, and `packages.config` format documentation;
- PE/COFF and CLI metadata format documentation.

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
- recovery-ceiling wording;
- artifact format/family recognition truth conditions used by Stage 02.

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
