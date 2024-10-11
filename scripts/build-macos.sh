#!/bin/sh

CS_PROJ="../SubRenamer/SubRenamer.csproj"

rm -rf dist/SubRenamer.app

mkdir -p dist/SubRenamer.app/Contents/Resources
cp resources/App.plist dist/SubRenamer.app/Contents/Info.plist
cp resources/App.icns dist/SubRenamer.app/Contents/Resources/App.icns

mkdir -p dist/SubRenamer.app/Contents/MacOS
dotnet publish $CS_PROJ -c Release -r osx-arm64 -o dist/SubRenamer.app/Contents/MacOS -p:PublishAot=true -p:PublishTrimmed=true -p:TrimMode=link --self-contained
cd dist
zip SubRenamer_macos_arm64.zip -9 -r SubRenamer.app
cd ..

rm -rf dist/SubRenamer.app/Contents/MacOS

mkdir -p dist/SubRenamer.app/Contents/MacOS
dotnet publish $CS_PROJ -c Release -r osx-x64 -o dist/SubRenamer.app/Contents/MacOS -p:PublishAot=true -p:PublishTrimmed=true -p:TrimMode=link --self-contained
cd dist
zip SubRenamer_macos_amd64.zip -9 -r SubRenamer.app
cd ..