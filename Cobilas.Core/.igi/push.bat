echo off

set arg1=%1
set arg2=%2

if "%arg1%"=="" set arg1=Cobilas.Collections
if "%arg2%"=="" set arg2=1.2.0

nuget push %arg1%.%arg2%.nupkg oy2e67ycvppcx2k7gl2qe74qgol3fjyevbc5j3kxwhcxpi -Source https://api.nuget.org/v3/index.json
pause