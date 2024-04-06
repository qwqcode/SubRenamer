#!/bin/sh

APP_NAME="SubRenamer"
APP_DIR="dist/SubRenamer.app"
CS_PROJ="../SubRenamer/SubRenamer.csproj"

rm -rf $APP_DIR

mkdir -p $APP_DIR/Contents/Resources
cp resources/App.plist $APP_DIR/Contents/Info.plist
cp resources/App.icns $APP_DIR/Contents/Resources/App.icns

mkdir -p $APP_DIR/Contents/MacOS
dotnet publish $CS_PROJ -c Release -r osx-arm64 -o $APP_DIR/Contents/MacOS -p:PublishAot=true -p:PublishTrimmed=true -p:TrimMode=link --self-contained
cd dist
zip ${APP_NAME}_macos_arm64.zip -9 -r ${APP_NAME}.app -x "*/*\.dsym/*"
cd ..

rm -rf $APP_DIR/Contents/MacOS

mkdir -p $APP_DIR/Contents/MacOS
dotnet publish $CS_PROJ -c Release -r osx-x64 -o $APP_DIR/Contents/MacOS -p:PublishAot=true -p:PublishTrimmed=true -p:TrimMode=link --self-contained
zip ${APP_NAME}_macos_amd64.zip -r ${APP_DIR}.app -x "*/*\.dsym/*"
cd ..