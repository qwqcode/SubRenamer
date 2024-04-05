#!/bin/sh

APP_NAME="SubRenamer"
CS_PROJ="../SubRenamer/SubRenamer.csproj"

mkdir -p dist/$APP_NAME
dotnet publish $CS_PROJ -c Release -r linux-x64 -o dist/$APP_NAME -p:PublishAot=true -p:PublishTrimmed=true -p:TrimMode=link --self-contained

if command -v upx &> /dev/null; then
  upx -fq dist/$APP_NAME/$APP_NAME dist/$APP_NAME/*.so
fi

chmod u+x dist/$APP_NAME/$APP_NAME
cp resources/App.desktop.template dist/$APP_NAME/$APP_NAME.desktop.template
cp resources/App.icns dist/$APP_NAME/$APP_NAME.icns

cd dist
tar -zcvf ${APP_NAME}_linux_amd64.tar.gz --exclude="*/*.dbg" $APP_NAME
