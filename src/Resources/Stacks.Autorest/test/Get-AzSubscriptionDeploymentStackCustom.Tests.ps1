if(($null -eq $TestName) -or ($TestName -contains 'Get-AzSubscriptionDeploymentStackCustom'))
{
  $loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
  if (-Not (Test-Path -Path $loadEnvPath)) {
      $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
  }
  . ($loadEnvPath)
  $TestRecordingFile = Join-Path $PSScriptRoot 'Get-AzSubscriptionDeploymentStackCustom.Recording.json'
  $currentPath = $PSScriptRoot
  while(-not $mockingPath) {
      $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
      $currentPath = Split-Path -Path $currentPath -Parent
  }
  . ($mockingPath | Select-Object -First 1).FullName
}

Describe 'Get-AzSubscriptionDeploymentStackCustom' {

    BeforeAll {
        $rname = "testPSStack-" + (Get-Random 1000000)
        $location = "westus2"
    
        # Prepare 
        $deployment = New-AzSubscriptionDeploymentStack -Name $rname -Location $location -TemplateFile templates\StacksSubTemplate.json -TemplateParameterFile templates\StacksSubTemplateParams.json -DenySettingsMode None -Force
        $deployment | Should -Not -Be $null
    }

    It 'ListDeploymentStacks' {
        {
            $listBySubscription = Get-AzSubscriptionDeploymentStackCustom
            $listBySubscription.Count | Should -Not -Be 0
            $listBySubscription.name.contains($rname) | Should -Be $true
        } | Should -Not -Throw
    }

    It 'GetByNameParameterSetname' {
        { 
            # Success 
		    $getByName = Get-AzSubscriptionDeploymentStackCustom -StackName $rname 
		    $getByName | Should -Not -Be $null

            # Failure - NotFound
            $badStackName = "badstack1928273615"
            $exceptionMessage = "'$badStackName' was not found."
            { Get-AzSubscriptionDeploymentStackCustom -StackName $badStackName } | Should -Throw $exceptionMessage            
        } | Should -Not -Throw
    }

    It 'GetByResourceIdParameterSetName' {
        { 
            # Success
            $getByResourceId = Get-AzSubscriptionDeploymentStack -ResourceId ("/subscriptions/" + $env.SubscriptionId + "/providers/Microsoft.Resources/deploymentStacks/$rname")
            $getByResourceId | Should -Not -Be $null

            # TODO: Need to Fix Id Validator
            # Failure - Bad Id Form
            # $badId = "a/bad/id"
            # $exceptionMessage = "Provided Id '$badId' is not in correct form. Should be in form /subscriptions/<subid>/providers/Microsoft.Resources/deploymentStacks/<stackname>"
            # Assert-Throws { Get-AzSubscriptionDeploymentStack -ResourceId $badId } | Should -Throw $exceptionMessage
        } | Should -Not -Throw
    }

    AfterAll {
        Remove-AzSubscriptionDeploymentStack -Name $rname -DeleteAll -Force
    }    
}
