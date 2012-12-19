@echo off

%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe app/SocialSense.sln /t:Clean,Rebuild /p:Configuration=Release

if not exist Download\Package\lib\net40 mkdir Download\Package\lib\net40

copy app\src\SocialSense\bin\Release\SocialSense.dll Download\Package\lib\net40

.\.nuget\NuGet.exe update -self
.\.nuget\NuGet.exe pack SocialSense.nuspec -BasePath Download\Package -Output Download