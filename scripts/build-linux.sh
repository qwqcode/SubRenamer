#!/bin/sh

mkdir -p dist/SubRenamer
dotnet publish ../SubRenamer/SubRenamer.csproj -c Release -r linux-x64 -o dist/SubRenamer

if command -v upx &> /dev/null; then
  upx -fq dist/SubRenamer/SubRenamer dist/SubRenamer/*.so
fi

chmod u+x dist/SubRenamer/SubRenamer
cp resources/App.desktop.template dist/SubRenamer/SubRenamer.desktop.template
cp resources/App.icns dist/SubRenamer/SubRenamer.icns

cd dist
tar -zcvf SubRenamer_linux_amd64.tar.gz --exclude="*/*.dbg" SubRenamer
