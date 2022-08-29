@echo off

:: Place GoFX at the root of the data directory, to make it accessible to FiveM's path
echo Building GoFX for C...
gcc .\go\src\gofx\gofx.c -shared -o ..\..\..\..\GoFX.dll

set GO111MODULE=auto

if exist ..\Client\client.go.dll del ..\Client\client.go.dll
if exist ..\Client\client.go.h del ..\Client\client.go.h
echo Building client Go resource...
::govvv build -buildmode=c-shared -ldflags="-s -w" -o ..\Client\client.go.dll .\go\src\client
go build -buildmode=c-shared -ldflags="-s -w" -o ..\Client\client.go.dll .\go\src\client

if exist ..\Server\server.go.dll del ..\Server\server.go.dll
if exist ..\Server\server.go.h del ..\Server\server.go.h
echo Building server Go resource...
::govvv build -buildmode=c-shared -ldflags="-s -w" -o ..\Server\server.go.dll .\go\src\server
go build -buildmode=c-shared -ldflags="-s -w" -o ..\Server\server.go.dll .\go\src\server

echo Building client GoFX for C#...
pushd Client
dotnet publish -c Release
popd

echo Building server GoFX for C#...
pushd Server
dotnet publish -c Release
popd

echo Finalizing GoFX for C#...
copy /y fxmanifest.lua ..\
xcopy /y /e Client\bin\Release\net452\publish ..\Client\
xcopy /y /e Server\bin\Release\netstandard2.0\publish ..\Server\

echo GoFX build complete!