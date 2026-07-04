# Shared

Cross-app shared library for the `crgolden` fleet, published as a **private** NuGet package to the `crgolden` GitHub Packages feed. Not intended for public consumption — only ever needed for local development or CI builds of the repos that reference it.

## Contents

- `ServiceCollectionExtensions.AddObservability()` — configures `AspNetCoreTraceInstrumentationOptions` to exclude `/health` requests from tracing.
- `Shared.Domain` — self-validating domain entities (`Church`, `Campus`, `Ministry`, `ServiceSchedule`, `ChurchAttribute`) mirroring the `Directory` church-database schema. Private constructors plus static `Create(...)` factories enforce every NOT NULL/range invariant at construction, so an invalid instance can never exist in memory — bad input fails fast with a specific `ArgumentException`/`ArgumentOutOfRangeException` instead of surfacing as a raw SQL constraint violation deep inside a write path.

## Building and testing

```powershell
dotnet build Shared.slnx
dotnet test Shared.Tests --filter-trait Category=Unit
```

## Versioning and publishing

`.github/workflows/publish.yml` builds and runs unit tests on every push and PR. Publishing then splits into two paths:

- **Automatic preview builds** — every push to `main` computes a version with [GitVersion](https://gitversion.net/) (config: `GitVersion.yml`, tool pinned in `dotnet-tools.json`) and publishes it immediately. The base number is the next semver above the last `v*` release tag: a default patch bump, or `+semver: minor` / `+semver: major` in a commit message to bump further (`+semver: none` to skip entirely). That number gets a `-preview.<N>` suffix, where `<N>` is the count of commits since the last release tag.
- **Manual stable releases** — trigger the workflow manually (`workflow_dispatch`) to cut a real release. It reads the version straight from `Shared/Shared.csproj`'s `<Version>` element (bump that by hand first), publishes that exact number with no pre-release suffix, and pushes a matching `v<version>` git tag. That tag is both the release record and the reset point for the next automatic preview build's commit counter.

Both paths push to the private `crgolden` GitHub Packages feed (`https://nuget.pkg.github.com/crgolden/index.json`, already configured in `NuGet.Config`).
