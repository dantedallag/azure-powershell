if(($null -eq $TestName) -or ($TestName -contains 'Save-AzSubscriptionDeploymentStackCustom'))
{
  $loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
  if (-Not (Test-Path -Path $loadEnvPath)) {
      $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
  }
  . ($loadEnvPath)
  $TestRecordingFile = Join-Path $PSScriptRoot 'Save-AzSubscriptionDeploymentStackCustom.Recording.json'
  $currentPath = $PSScriptRoot
  while(-not $mockingPath) {
      $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
      $currentPath = Split-Path -Path $currentPath -Parent
  }
  . ($mockingPath | Select-Object -First 1).FullName
}

Describe 'Save-AzSubscriptionDeploymentStackCustom' {
    It 'SaveByName' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'SaveByResourceId' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'SaveByStackObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }
}
