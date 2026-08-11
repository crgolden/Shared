# Shared

Cross-app shared library for the `crgolden` fleet, published as a **private** NuGet package to the `crgolden` GitHub Packages feed. Not intended for public consumption — only ever needed for local development or CI builds of the repos that reference it.

## Contents

- `ServiceCollectionExtensions.AddObservability()` — configures `AspNetCoreTraceInstrumentationOptions` to exclude `/health` requests from tracing.
- `Shared.Domain` — self-validating domain entities (`Church`, `Campus`, `Ministry`, `ServiceSchedule`, `ChurchAttribute`) mirroring the `Directory` church-database schema. Bad input fails fast with a specific `ArgumentException`/`ArgumentOutOfRangeException` instead of surfacing as a raw SQL constraint violation deep inside a write path.

### The builder pattern

Each entity (`Church`, `Campus`, `Ministry`, `ServiceSchedule`, `ChurchAttribute`) follows the same shape: an internal parameterless constructor and `internal init` properties mean the entity can only ever be populated by its matching builder (e.g. `ChurchBuilder` for `Church`) — an external assembly can't construct one via an object initializer and bypass validation.

The builder validates one field at a time. Each `With*` call checks that field immediately and returns the builder for chaining; `Build()` only checks that every required field was set, since each one was already validated the moment it was supplied. The result: a caller gets an immediate, specific exception pointing at exactly the bad field, rather than a raw SQL constraint violation three layers away from the input that caused it.

## Building and testing

```powershell
dotnet build Shared.slnx
dotnet test Shared.Tests.Unit --filter-trait Category=Unit
```

## Versioning and publishing

`.github/workflows/publish.yml` builds and runs unit tests on every push and PR. Publishing then splits into two paths:

- **Automatic preview builds** — every push to `main` computes a version with [GitVersion](https://gitversion.net/) (config: `GitVersion.yml`, tool pinned in `dotnet-tools.json`) and publishes it immediately. The base number is the next semver above the last `v*` release tag: a default patch bump, or `+semver: minor` / `+semver: major` in a commit message to bump further (`+semver: none` to skip entirely). That number gets a `-preview.<N>` suffix, where `<N>` is the count of commits since the last release tag.
- **Manual stable releases** — trigger the workflow manually (`workflow_dispatch`) to cut a real release. It reads the version straight from `Shared/Shared.csproj`'s `<Version>` element (bump that by hand first), publishes that exact number with no pre-release suffix, and pushes a matching `v<version>` git tag. That tag is both the release record and the reset point for the next automatic preview build's commit counter.

Both paths push to the private `crgolden` GitHub Packages feed (`https://nuget.pkg.github.com/crgolden/index.json`, already configured in `NuGet.Config`).
