if(($null -eq $TestName) -or ($TestName -contains 'Set-AzSubscriptionDeploymentStackCustom'))
{
  $loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
  if (-Not (Test-Path -Path $loadEnvPath)) {
      $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
  }
  . ($loadEnvPath)
  $TestRecordingFile = Join-Path $PSScriptRoot 'Set-AzSubscriptionDeploymentStackCustom.Recording.json'
  $currentPath = $PSScriptRoot
  while(-not $mockingPath) {
      $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
      $currentPath = Split-Path -Path $currentPath -Parent
  }
  . ($mockingPath | Select-Object -First 1).FullName
}

Describe 'Set-AzSubscriptionDeploymentStackCustom' {
    It 'ByTemplateFileWithNoParameters' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateFileWithParameterFile' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateFileWithParameterUri' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateFileWithParameterObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateSpecWithParameterObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateUriWithParameterObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateSpecWithParameterUri' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateUriWithParameterUri' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateUriWithParameterFile' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateUriWithNoParameters' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateSpecWithNoParameters' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByTemplateSpecWithParameterFile' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }
}
