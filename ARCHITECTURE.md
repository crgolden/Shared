# Architecture

## Bird's-eye view

One class library, `Shared`, shipped as a NuGet package. It has three parts: domain entities a church-directory
service builds and validates before writing them, two extension classes for ASP.NET Core and configuration, and
`Shared.Testing`, the test-data generators and settings helpers test projects use. A single unit-test project checks
all three.

## Codemap

| Part | What it does |
|---|---|
| `Shared/Domain` | `Church`, `Campus`, `Ministry`, `ServiceSchedule` and `ChurchAttribute`, each with a fluent `*Builder`; `StateCode`, the closed USPS set, and `StateCodes.TryParse` |
| `Shared/Extensions` | `ConfigurationExtensions.GetRequired<T>(key)` (throws on a missing key) and `ServiceCollectionExtensions.AddObservability()` (drops `/health` from ASP.NET Core tracing) |
| `Shared/Testing` | `Generated` (every test-data generator), `EnvironmentSetting` (typed environment-variable settings with defaults and bounds), `JsonPayload`, `DisplayPrice` |
| `Shared.Tests.Unit` | the unit tier for all of the above ([TESTING.md](TESTING.md)) |
| `.github/workflows/publish.yml` | build, inspect, test and analyze every change; publish previews from `main` and releases on demand |
| `GitVersion.yml`, `dotnet-tools.json` | version computation, and the pinned GitVersion, coverlet and ReSharper tools |
| `.editorconfig`, `stylecop.json`, `Shared.sln.DotSettings` | analyzer severities, StyleCop settings, ReSharper naming abbreviations |
| `Shared.snk`, `deeprog.png` | strong-name key and package icon |
| `gate.ps1` | the maintainers' local gate (README.md, "Gate") |

## Invariants

- **An entity exists only through its builder.** Each entity has an `internal` constructor and `internal init`
  setters, so code outside this assembly cannot create or fill one directly.
- **Each `With*` call validates its own field immediately** and throws `ArgumentException` or
  `ArgumentOutOfRangeException` naming the parameter. `Build()` checks only that every required field was set. The
  bounds each guard reads are public constants on the builder (for example `ChurchBuilder.MinLatitude`).
- **Latitude and longitude `(0,0)` are valid**: a church not yet geocoded.
- **`State` is the `StateCode` enum, never a string.** `StateCodes.TryParse` is the one conversion from text: it
  accepts letters only, so a numeric string never parses into an enum value, and it refuses any code outside the
  defined set.
- **`Shared.Testing` carries no test-framework dependency.** NuGet dependencies apply to the whole package, so a
  test-framework reference here would reach every production service that references `Shared`.
- **No XML doc comments.** `GenerateDocumentationFile` is on so the package ships its XML file, and the StyleCop
  documentation rules are silenced in `.editorconfig`.

## Boundaries

- The library reads no configuration and calls no network service; callers pass everything in.
- Restoring it needs only nuget.org. Consumers restore the package itself from the `crgolden` GitHub Packages feed.

## Cross-cutting concerns

- **Warnings are errors** in both projects (`TreatWarningsAsErrors`), and CI also fails on any `jb inspectcode`
  result and any open SonarCloud issue.
- **SonarCloud treats `Shared` as production code**, including `Shared.Testing`, because the project references no
  test framework. Its hotspot rule on names containing `password` fires on any constant named that way, so a
  generator's shape constant is named for its content.
- **The assembly is strong-named** (`SignAssembly` with `Shared.snk`) and ships symbols as a `.snupkg`.
