dotnet restore .\ClientProject\WindowsClient.csproj && dotnet build .\WindowsSolution.sln -c Release /clp:ErrorsOnly -p WarningLevel=0 -maxcpucount:1
pause