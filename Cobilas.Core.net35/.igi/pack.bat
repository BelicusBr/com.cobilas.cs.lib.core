echo off

set arg1=%1
set arg2=%2

if "%arg1%"=="" set arg1=Cobilas.Collections
if "%arg2%"=="" set arg2=1.2.0

nugetspec -v
echo:
nugetspec -s -i %arg1% --v %arg2% -t %arg1% -d "Cobilas core" -a Cobilas --t CSharp -p "https://www.nuget.org/packages/%arg1%"
echo:
echo Finalizado
pause