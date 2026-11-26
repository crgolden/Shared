@{
    Schema   = 1
    Repo     = 'Shared'
    Workflow = '.github/workflows/publish.yml'
    Sonar    = @{ ProjectKey = 'crgolden_Shared' }
    Probe    = @{ Kind = 'dotnet'; Projects = @('Shared.Tests.Unit') }
    Steps    = @(
        @{ Name = 'Install SonarQube Cloud scanner'; Tool = 'dotnet-sonarscanner'; Inputs = @('*') }
        @{ Name = 'Restore local tools'; Run = @('dotnet', 'tool', 'restore'); Inputs = @('dotnet-tools.json'); Always = $true }
        @{
            Name   = 'Begin Sonar analysis'
            Sonar  = 'begin'
            Needs  = @('Install SonarQube Cloud scanner')
            Inputs = @('*')
            Run    = @(
                'dotnet-sonarscanner', 'begin', '/k:{sonarProject}', '/o:crgolden', '/d:sonar.host.url=https://sonarcloud.io',
                '/d:sonar.cs.opencover.reportsPaths=coverage.opencover.xml', '/d:sonar.exclusions=**/bin/**,**/obj/**,**/*.png',
                '/d:sonar.qualitygate.wait=true', '/d:sonar.scanner.skipJreProvisioning=true',
                '/d:sonar.scm.use.blame.algorithm=GIT_NATIVE_BLAME', '/d:sonar.branch.name={sonarBranch}'
            )
        }
        @{ Name = 'Restore'; Sonar = 'build'; Inputs = @('*'); Run = @('dotnet', 'restore', 'Shared.slnx') }
        @{
            Name   = 'Build'
            Sonar  = 'build'
            Needs  = @('Restore')
            Inputs = @('*')
            Run    = @('dotnet', 'build', 'Shared.slnx', '--no-restore', '--configuration', 'Release', '-warnaserror')
        }
        @{
            Name   = 'ReSharper inspection (jb inspectcode)'
            Needs  = @('Build', 'Restore local tools')
            Inputs = @('*')
            Remove = @('{output}\shared-inspect.sarif')
            Run    = @('dotnet', 'jb', 'inspectcode', 'Shared.slnx', '--no-build', '-e=WARNING', '--caches-home={inspectCache}', '--output={output}\shared-inspect.sarif')
            Check  = @{ Sarif = '{output}\shared-inspect.sarif' }
        }
        @{
            Name   = 'Run unit tests with coverage'
            Needs  = @('Build', 'Restore local tools')
            Inputs = @('*')
            Remove = @('Shared.Tests.Unit\bin\Release\net11.0\TestResults\unit-tests.trx')
            Run    = @(
                'dotnet', 'coverlet', 'Shared.Tests.Unit/bin/Release/net11.0', '--target', 'dotnet',
                '--targetargs', 'test --project Shared.Tests.Unit --no-build --configuration Release -- --filter-trait Category=Unit --report-xunit-trx --report-xunit-trx-filename unit-tests.trx --results-directory=Shared.Tests.Unit/bin/Release/net11.0/TestResults',
                '--format', 'opencover', '--output', 'coverage.opencover.xml', '--skipautoprops',
                '--exclude-by-attribute', 'GeneratedCodeAttribute', '--exclude-by-file', '**/obj/**',
                '--does-not-return-attribute', 'DoesNotReturnAttribute', '--include', '[Shared]*'
            )
            Check  = @{ Trx = 'Shared.Tests.Unit\bin\Release\net11.0\TestResults\unit-tests.trx'; Floor = 1 }
        }
        @{
            Name   = 'End Sonar analysis'
            Sonar  = 'end'
            Needs  = @('Begin Sonar analysis', 'Build')
            Inputs = @('*')
            Run    = @('dotnet-sonarscanner', 'end')
        }
        @{ Name = 'Fail on open Sonar issues'; Sonar = 'issues'; Needs = @('End Sonar analysis'); Inputs = @('*') }
        @{ Name = 'Determine preview version'; Delivery = $true }
        @{ Name = 'Pack'; Delivery = $true }
        @{ Name = 'Push to GitHub Packages'; Delivery = $true }
        @{ Name = 'Read release version from Shared.csproj'; Delivery = $true }
        @{ Name = 'Tag the release'; Delivery = $true }
    )
}
