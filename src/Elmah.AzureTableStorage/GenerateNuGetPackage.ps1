C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe Elmah.AzureTableStorage.csproj /p:Configuration=Release
D:\Utils\NuGet.exe pack -Prop Configuration=Release
Move-Item *.nupkg D:\NuGet\ -Force