$project = "<Project>`n" +
"  <PropertyGroup>`n" +
"    <Product>Coderanger.ImageInfo</Product>`n" +
"    <Authors>CodeRanger</Authors>`n" +
"    <Company>CodeRanger</Company>`n" +
"    <Copyright>Copyright (c) CodeRanger. All rights reserved.</Copyright>`n" +
"    <Version>$($env:GITVERSION_SEMVER)</Version>`n" +
"    <FileVersion>$($env:GITVERSION_SEMVER)</FileVersion>`n" +
"    <InformationalVersion>$($env:GITVERSION_SEMVER)</InformationalVersion>`n" +
"  </PropertyGroup>`n" +
"</Project>"

$project | Out-File -FilePath './Directory.Build.props';
Write-Host "Outputting new Build.props:"
Write-Host $project
