$assembly = '.\SiteWarmer\SiteWarmer.App\bin\Release\output\SiteWarmer.exe'
$assemblyName = (Get-Item $assembly).VersionInfo.FileVersion 

Write-Host "Assembly Version"
Write-Host $assemblyName

Set-AppveyorBuildVariable -Name "AssemblyVersion" -Value $assemblyName