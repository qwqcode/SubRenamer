# SubRenamer

Next-Gen SubRenamer, a subtitle renaming tool powered by dotnet and avalonia (cross-platform).

## Prerequisites

Windows

```bash
Visual Studio 2022, including .NET 8 & Desktop development with C++ workload.
```

Ubuntu (20.04+)

```bash
sudo apt-get install dotnet-sdk-8.0 libicu-dev cmake zlib1g-dev -y
```

## Publish with NativeAOT

```bash
dotnet publish -r <RID> -c Release

# Build for Windows example
dotnet publish -r win-x64 -c Release
```

## Builder the installer with NSIS

NSIS installer `~13MB size`

```bash
pwsh ./publish.ps1
```

> if you builder the installer with nsis, you can ignore upx compression, so you can get better startup performance.
