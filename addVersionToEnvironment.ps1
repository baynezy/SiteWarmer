$assembly = '.\SiteWarmer\SiteWarmer.App\bin\Release\output\SiteWarmer.exe'
$sharedAssemblyInfo = (Get-Item $assembly).VersionInfo.FileVersion 

Write-Host $sharedAssemblyInfo

