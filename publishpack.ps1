
dotnet pack src

$packfile=get-item src/bin/Release/JunziQianSdk.1.3.0.nupkg
nuget add  $packfile -source d:/code/localpackages
dotnet nuget push $packfile --api-key $env:NugetKey --source $env:NugetSource