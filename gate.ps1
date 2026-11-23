param([string]$Goal, [string]$Only)

$ErrorActionPreference = 'Continue'
$gateCommon = $env:GATE_LIBRARY
if (-not $gateCommon -or -not (Test-Path -LiteralPath $gateCommon -PathType Leaf)) {
    Write-Host 'GATE: FAILED (GATE_LIBRARY names no gate library script; README.md, section Gate, says how to plug one in or run the steps without one)'
    exit 1
}
. $gateCommon
$gateOutput = if (Get-Command Get-GateOutputDirectory -ErrorAction SilentlyContinue) {
    Get-GateOutputDirectory (Split-Path -Leaf $PSScriptRoot)
} else {
    (New-Item -ItemType Directory -Force -Path (Join-Path ([IO.Path]::GetTempPath()) "claude\crgolden-gates\$(Split-Path -Leaf $PSScriptRoot)")).FullName
}

Register-GateStep @('Restore local tools', 'Begin Sonar analysis', 'Restore', 'Build', 'jb inspectcode',
    'Run unit tests with coverage', 'End Sonar analysis', 'Fail on open Sonar issues')
Register-StepInput 'Restore local tools' @('dotnet-tools.json')
Register-StepInput 'Begin Sonar analysis' @('*')
Register-StepInput 'Restore' @('*')
Register-StepInput 'Build' @('*')
Register-StepInput 'jb inspectcode' @('*')
Register-StepInput 'Run unit tests with coverage' @('*')
Register-StepInput 'End Sonar analysis' @('*')
Register-StepInput 'Fail on open Sonar issues' @('*')
$repo = $PSScriptRoot
$sarif = Join-Path $gateOutput 'shared-inspect.sarif'
$unitTrx = Join-Path $repo 'Shared.Tests.Unit\bin\Release\net11.0\TestResults\unit-tests.trx'
$sonarBranch = Get-SonarBranchName
$beginSonar = "Begin Sonar analysis (branch $sonarBranch)"
$restore = 'Restore (dotnet restore Shared.slnx)'
$build = 'Build (dotnet build Shared.slnx Release)'
$endSonar = 'End Sonar analysis (quality gate waited)'
$sonarIssues = 'Fail on open Sonar issues'
$unitStep = 'Run unit tests with coverage (Category=Unit)'
$env:TZ = 'UTC'
if ($env:TZ -ne 'UTC') { Write-Host 'GATE: FAILED (TZ pin)'; exit 1 }
Set-Location $repo
$probeProjects = @('Shared.Tests.Unit')
if ($Only) {
    $probeFile, $probeTest = $Only -split '::', 2
    $probeFile = "$probeFile".Trim().Replace('\', '/')
    $probeTest = "$probeTest".Trim()
    $probeProject = $probeFile.Split('/')[0]
    if (-not $probeTest -or $probeTest.Contains(',') -or $probeProject -notin $probeProjects -or
            -not (Test-Path -LiteralPath (Join-Path $repo $probeFile) -PathType Leaf)) {
        Write-Host "PROBE: FAILED (a probe runs exactly one test, named by the file it lives in: -Only '<a test file under $($probeProjects -join ' | ')>::<test method>')"
        exit 1
    }
    $probeClass = [IO.Path]::GetFileNameWithoutExtension($probeFile)
    $probeResults = Join-Path $gateOutput 'probe-results'
    $probeTrx = Join-Path $probeResults 'probe.trx'
    if (Test-Path -LiteralPath $probeTrx) { Remove-Item -LiteralPath $probeTrx -Force }
    Write-Host "PROBE: running one test, '$probeTest' of $probeClass in $probeProject; no gate record or verdict is written"
    $global:LASTEXITCODE = $null
    dotnet test --project $probeProject --configuration Release -- --filter-query "/*/*/$probeClass/$probeTest" --report-xunit-trx --report-xunit-trx-filename probe.trx --results-directory=$probeResults
    $probeExit = $global:LASTEXITCODE
    if (-not (Test-Path -LiteralPath $probeTrx)) { Write-Host "PROBE: FAILED (no TRX, exit $probeExit)"; exit 1 }
    [xml]$probeDoc = Get-Content -LiteralPath $probeTrx -Raw
    $counters = $probeDoc.TestRun.ResultSummary.Counters
    if ($probeExit -ne 0 -or [int]$counters.executed -ne 1 -or [int]$counters.failed -ne 0) {
        Write-Host "PROBE: FAILED (a probe runs one test, and '$Only' executed $($counters.executed), failed $($counters.failed), exit $probeExit)"
        exit 1
    }
    Write-Host "PROBE: PASSED for the one test named, against the last build's binaries; no verdict was saved"
    exit 0
}
exit (Invoke-Gate {
Initialize-GateState 'Shared' $repo
Get-OwedStep | Register-OwedStep
Invoke-GateLibraryStep

$toolRestore = 'Restore local tools (dotnet tool restore)'
Invoke-GateStep -Name $toolRestore -Body {
    $global:LASTEXITCODE = $null
    dotnet tool restore
    Assert-StepExit $toolRestore
}

$sonarCarried = Test-StepCarried $sonarIssues
if ($sonarCarried) {
    $null = Test-StepCarried $beginSonar
    $null = Test-StepCarried $restore
    $null = Test-StepCarried $build
    $null = Test-StepCarried $endSonar
}
else {
    $sonarStartedAt = [DateTimeOffset]::UtcNow
    Invoke-GateStep -Name $beginSonar -Always -Body {
        $env:JAVA_HOME = Get-SonarScannerJavaHome $beginSonar
        $global:LASTEXITCODE = $null
        dotnet-sonarscanner begin /k:"crgolden_Shared" /o:"crgolden" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.cs.opencover.reportsPaths="coverage.opencover.xml" /d:sonar.exclusions="**/bin/**,**/obj/**,**/*.png" /d:sonar.qualitygate.wait=true /d:sonar.scanner.skipJreProvisioning=true /d:sonar.scm.use.blame.algorithm=GIT_NATIVE_BLAME /d:sonar.branch.name="$sonarBranch"
        Assert-StepExit $beginSonar
    }

    Invoke-GateStep -Name $restore -Always -Body {
        $global:LASTEXITCODE = $null
        dotnet restore Shared.slnx
        Assert-StepExit $restore
    }

    Invoke-GateStep -Name $build -Needs @($restore) -Always -Body {
        $global:LASTEXITCODE = $null
        dotnet build Shared.slnx --no-restore --configuration Release -warnaserror
        Assert-StepExit $build
    }
}

Invoke-GateStep -Name 'jb inspectcode' -Needs @($build, $toolRestore) -Body {
    if (Test-Path $sarif) { Remove-Item $sarif -Force }
    dotnet jb inspectcode "$repo\Shared.slnx" --no-build -e=WARNING --caches-home="$(New-InspectCodeCache $gateOutput)" --output="$sarif"
    Test-Sarif $sarif
}

Invoke-GateStep -Name $unitStep -Needs @($build, $toolRestore) -Body {
    if (Test-Path $unitTrx) { Remove-Item $unitTrx -Force }
    $global:LASTEXITCODE = $null
    dotnet coverlet Shared.Tests.Unit\bin\Release\net11.0 `
        --target "dotnet" `
        --targetargs "test --project Shared.Tests.Unit --no-build --configuration Release -- --filter-trait Category=Unit --report-xunit-trx --report-xunit-trx-filename unit-tests.trx --results-directory=Shared.Tests.Unit/bin/Release/net11.0/TestResults" `
        --format opencover --output "coverage.opencover.xml" `
        --skipautoprops --exclude-by-attribute GeneratedCodeAttribute --exclude-by-file "**/obj/**" `
        --does-not-return-attribute DoesNotReturnAttribute --include "[Shared]*"
    Test-Trx $unitStep $unitTrx $global:LASTEXITCODE -floor 1
}

if (-not $sonarCarried) {
    Invoke-GateStep -Name $endSonar -Needs @($beginSonar, $build, $unitStep) -Always -Body {
        $global:LASTEXITCODE = $null
        dotnet-sonarscanner end
        Assert-StepExit $endSonar
    }
    Invoke-GateStep -Name $sonarIssues -Needs @($endSonar) -Always -Body {
        Assert-NoOpenSonarIssue $sonarIssues 'crgolden_Shared' $sonarBranch $sonarStartedAt
    }
}

Write-Row 'Upload test results / pack / push / tag' 'NOT RUN' 'delivery steps, not checks'
Complete-Gate
})
