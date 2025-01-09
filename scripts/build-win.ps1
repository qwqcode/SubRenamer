if (Test-Path dist\SubRenamer) {
    Remove-Item dist\SubRenamer -Recurse -Force
}

if (Test-Path dist\SubRenamer_windows_amd64.zip) {
    Remove-Item dist\SubRenamer_windows_amd64.zip -Force
}

dotnet publish ..\SubRenamer\SubRenamer.csproj -c Release -r win-x64 -o dist\SubRenamer

$files = Get-ChildItem -Path dist\SubRenamer
$files | Where-Object { $_.Name -ne "SubRenamer.exe" } | Remove-Item -Force

$upx = Get-Command upx -ErrorAction SilentlyContinue
if ($upx) {
    & $upx -fq dist\SubRenamer\SubRenamer.exe
}

Compress-Archive -Path dist\SubRenamer -DestinationPath dist\SubRenamer_windows_amd64.zip
