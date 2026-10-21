[![Build, test, and publish Shared package](https://github.com/crgolden/Shared/actions/workflows/publish.yml/badge.svg)](https://github.com/crgolden/Shared/actions/workflows/publish.yml)

[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=crgolden_Shared)](https://sonarcloud.io/summary/new_code?id=crgolden_Shared)

# Shared

A .NET class library published as the NuGet package `Shared` to the `crgolden` GitHub Packages feed. It holds
self-validating domain entities for a church directory, two small extension classes, and the test-support code
(`Shared.Testing`) that test projects use to generate their data. The package is public; GitHub Packages still asks
for a token to read any package, public or not.

How the parts fit together, and the rules each one keeps: [ARCHITECTURE.md](ARCHITECTURE.md). How to run the tests:
[TESTING.md](TESTING.md).

## Technologies

- .NET 10 (C# 14), one class library (`Shared/Shared.csproj`) and one unit-test project (`Shared.Tests.Unit`).
- xUnit v3 on Microsoft.Testing.Platform for tests, coverlet for coverage.
- StyleCop.Analyzers, SonarAnalyzer.CSharp and ReSharper's `jb inspectcode`, with warnings treated as errors.
- GitVersion for preview version numbers; SonarCloud for CI analysis.

## Prerequisites

- The .NET 10 SDK: `global.json` pins `10.0.201` and rolls forward to the latest feature band.
- Nothing else for building and testing: every package this repo restores comes from nuget.org (`NuGet.Config` maps
  `*` to nuget.org), so no GitHub token is needed to build it.

## Clone and build

```powershell
git clone https://github.com/crgolden/Shared.git
cd Shared
dotnet tool restore
dotnet build Shared.slnx
```

## Configuration

The library reads no configuration of its own and needs no secrets. `Shared.snk` (the strong-name key) is tracked
on purpose: strong-naming identifies the assembly, it is not a security boundary.

## Run locally

There is no application to run. A consuming project references the package:

```xml
<PackageReference Include="Shared" Version="x.y.z" />
```

and restores it from `https://nuget.pkg.github.com/crgolden/index.json` with a GitHub token that has the
`read:packages` scope, configured as that source's credentials in the consumer's own NuGet settings.

## Tests

```powershell
dotnet test --project Shared.Tests.Unit --configuration Release -- --filter-trait "Category=Unit"
```

The unit tier needs no network, database or secrets. [TESTING.md](TESTING.md) has the CI steps and coverage.

## Gate

`gate.ps1` is the maintainers' gate. It loads a private toolkit from a folder beside this repo, so in a plain clone it
stops at once with `GATE: FAILED`. To gate your own change, run the same checks CI runs, in this order, in your own
script or by hand:

```powershell
dotnet tool restore
dotnet restore Shared.slnx
dotnet build Shared.slnx --no-restore --configuration Release -warnaserror
dotnet jb inspectcode Shared.slnx --no-build -e=WARNING --output=inspectcode.sarif
dotnet test --project Shared.Tests.Unit --no-build --configuration Release -- --filter-trait "Category=Unit"
```

`jb inspectcode` passes only when `inspectcode.sarif` holds no results.

## Versioning and publishing

`.github/workflows/publish.yml` builds, inspects and tests every push and pull request, then publishes on two paths:

- **Every push to `main`** (`publish-preview`): GitVersion (`GitVersion.yml`, tool pinned in `dotnet-tools.json`)
  computes the next version above the last `v*` tag: a patch bump by default, `+semver: minor` or `+semver: major` in a
  commit message to bump further, `+semver: none` to skip. In `ContinuousDeployment` mode the number carries no
  pre-release label, so the package is a plain `X.Y.Z`. The push uses `--skip-duplicate`, so only the first push after
  a release tag publishes a new version.
- **A manual run** (`workflow_dispatch`, `publish-release`): packs the `<Version>` in `Shared/Shared.csproj` as is
  (bump it first) and pushes the tag `v<version>`, which becomes the base for the next computed version.

To predict the next computed version locally: `dotnet tool restore`, then `dotnet dotnet-gitversion /showvariable FullSemVer`.
Both publish jobs need the full history (`fetch-depth: 0`): GitVersion walks back to the last tag, and SonarCloud
reads blame to find new code.
