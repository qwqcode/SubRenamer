dotnet publish -r win-x64 -c Release
Remove-Item SubRenamer\bin\Release\net8.0\win-x64\publish\SubRenamer.pdb
makensis installer.nsi