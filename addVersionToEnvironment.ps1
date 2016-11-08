$sharedAssemblyInfo = Get-Content .\SiteWarmer\SharedAssemblyInfo.cs

Write-Host $sharedAssemblyInfo


$RegularExpression = [regex] 'AssemblyVersion\(\"(.*)\"\)'

