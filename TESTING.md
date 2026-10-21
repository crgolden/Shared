# Testing

## Test tiers

One tier: unit tests in `Shared.Tests.Unit`, an xUnit v3 project on Microsoft.Testing.Platform (`global.json` selects
the runner). Every test class carries `[Trait("Category", "Unit")]`. The tests need no network, database, service or
secret: they build entities through the builders, call the extension methods against in-memory configuration, and
check the `Shared.Testing` generators.

| Folder | Covers |
|---|---|
| `Shared.Tests.Unit/Domain` | each entity's builder: every `With*` guard, `Build()` refusing a missing required field, `StateCodes.TryParse` |
| `Shared.Tests.Unit/Extensions` | `GetRequired<T>` on a present and a missing key |
| `Shared.Tests.Unit/Testing` | `EnvironmentSetting` resolution and bounds, and the shape and range of every `Generated` member |

## Running tests locally

From the repo root:

```powershell
dotnet build Shared.slnx --configuration Release
dotnet test --project Shared.Tests.Unit --no-build --configuration Release -- --filter-trait "Category=Unit"
```

With coverage, as CI measures it (`coverlet.console` is pinned in `dotnet-tools.json`):

```powershell
dotnet tool restore
dotnet coverlet Shared.Tests.Unit/bin/Release/net10.0 `
  --target "dotnet" `
  --targetargs "test --project Shared.Tests.Unit --no-build --configuration Release -- --filter-trait Category=Unit --stop-on-fail on" `
  --format opencover --output "coverage.opencover.xml" `
  --skipautoprops --exclude-by-attribute GeneratedCodeAttribute --exclude-by-file "**/obj/**" `
  --does-not-return-attribute DoesNotReturnAttribute --include "[Shared]*"
```

Coverage counts only the `Shared` assembly (`--include "[Shared]*"`), never the test project.

## CI pipeline

The `build` job in `.github/workflows/publish.yml` runs on every push and pull request: restore local tools, begin
SonarCloud analysis, restore, Release build with `-warnaserror`, `dotnet jb inspectcode` (any result in the SARIF fails
the job), the unit tier under coverlet with `--stop-on-fail on`, upload of the test results, end of the analysis with
the quality gate waited, and a final step that fails while SonarCloud reports any open issue. The SonarCloud steps
skip Dependabot's pull requests, which get no secrets.

## Local SonarCloud analysis

The analysis needs the repo's SonarCloud token and project, which only maintainers have. Without them, the build with
`-warnaserror`, `jb inspectcode` and the unit tier above are the checks you can run.
