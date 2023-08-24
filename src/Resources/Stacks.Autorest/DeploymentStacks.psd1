@{
  GUID = 'e5c8fb3f-5c66-433c-865f-0dd14addbd90'
  RootModule = './DeploymentStacks.psm1'
  ModuleVersion = '0.1.0'
  CompatiblePSEditions = 'Core', 'Desktop'
  Author = ''
  CompanyName = ''
  Copyright = ''
  Description = ''
  PowerShellVersion = '5.1'
  DotNetFrameworkVersion = '4.7.2'
  RequiredAssemblies = './bin/DeploymentStacks.private.dll'
  FormatsToProcess = './DeploymentStacks.format.ps1xml'
  FunctionsToExport = 'Export-DeploymentStackTemplate', 'Get-DeploymentStack', 'New-DeploymentStack', 'Remove-DeploymentStack', 'Set-DeploymentStack', '*'
  AliasesToExport = '*'
  PrivateData = @{
    PSData = @{
      Tags = ''
      LicenseUri = ''
      ProjectUri = ''
      ReleaseNotes = ''
    }
  }
}
