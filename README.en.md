<p align="center"><img src="https://github.com/qwqcode/SubRenamer/assets/22412567/3a49c011-ce41-4bc3-ab85-5237a6e9acd7"></p>

# SubRenamer

<img src="https://github.com/qwqcode/SubRenamer/assets/22412567/ef9b38b0-d1c6-4f1f-9f7e-f7b67a36d9b5" width="150" align="right" />

🎞 One-click batch subtitle renaming tool

A Tool for Batch Rename Subtitle Files to Match Video Names with One Click.

**Why?** If the video and subtitle filenames match, any video player can automatically load the subtitles.

**Goal?** Rename external subtitle files to match the video filenames.

[![](https://img.shields.io/github/release/qwqcode/SubRenamer.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/releases/latest) [![](https://img.shields.io/github/downloads/qwqcode/SubRenamer/total.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/releases) [![](https://img.shields.io/github/issues/qwqcode/SubRenamer.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/issues)

## What sets SubRenamer apart from regular batch renaming software?

SubRenamer specializes in renaming subtitle files and is straightforward to use.

For most video and subtitle files, simply drag them into the program for automatic and precise recognition, allowing one-click renaming without the complex settings of typical renaming software.

## How to get SubRenamer?

Click the links below to download the latest version:

| [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/2772a99b-f10f-48cd-aed7-58488e7a726e">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_windows_amd64.zip) | [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/0aef7104-b7bc-4bde-94c3-3f9df044d66b">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_macos_arm64.zip) | [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/8b41fffd-2eb3-4a78-b1bd-8751a09c36c5">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_linux_amd64.tar.gz) |
|-|-|-|
| [Windows (x86)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_windows_amd64.zip) | [macOS (M1)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_macos_arm64.zip) | [Linux (x86)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_linux_amd64.tar.gz) |

You can find historical versions and changelogs on the [Release](https://github.com/qwqcode/SubRenamer/releases) page.

## Features

- **Automatic Matching**: One-click matching with automatic recognition algorithms.
- **Drag & Drop Import**: Quickly import files and folders via drag and drop.
- **Multi-language Matching**: Supports multi-language subtitle matching (one-to-many mapping).
- **Language Filtering**: Automatically detects and filters subtitles of specified languages before import.
- **Multiple Matching Rules**: Supports manual matching for complex filename formats.
- **Manual Matching Editor**: Customizable rules with simple wildcards.
- **Regex Editor**: Includes a regex matching test tool.
- **Match Fine-tuning**: Allows fine-tuning of matching results.
- **Auto Subtitle Sync**: Supports automatic alignment of subtitle timelines with video/audio (integrates [FFsubsync](https://github.com/qwqcode/ffsubsync-bin) + [FFmpeg](https://www.ffmpeg.org/))
- **Rename Commands**: Quickly copy Linux rename commands to clipboard via right-click.
- **Subtitle Backup**: Automatically backs up subtitle files before renaming.
- **Append Suffix**: Supports adding custom suffixes before file extensions.
- **File Recognition**: Automatically distinguishes between video and subtitle files by extension, with customization support.
- **Shortcuts**: Supports keyboard shortcuts for efficiency.
- **Dark Mode**: Follows system settings for dark mode.
- **Always on Top**: Keeps the window on top for easy operation.
- **I18n**: Supports multiple languages, including Chinese and English.
- **Cross-platform**: Supports Windows, macOS, and Linux.
- **Small Size**: Around 15MB.

> [!IMPORTANT]\
> Rewrite Note: The first version of SubRenamer was released in 2019, developed with WinForm and only supported Windows. In 2024, SubRenamer was rewritten and released as v2.0, using AvaloniaUI + .NET 8, now supporting cross-platform functionality on Windows, macOS, and Linux (not using Electron.js).

<img width="800" src="https://github.com/qwqcode/SubRenamer/assets/22412567/9b620a47-61cb-418a-b6d3-3dd2e0140f69">

| Match Editor | Custom Matching Rules |
|-|-|
| <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/2976022a-2545-4e0e-8202-bd3e00708e4a"> | <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/7dd80067-74c8-4c73-939f-fd7b01cb3d2b"> |

| Manual Matching Rule Editor | Regex Rule Editor |
|-|-|
| <img width="822" src="https://github.com/qwqcode/SubRenamer/assets/22412567/ec201431-0bbc-4ca2-8963-f7ec1ce46e32"> | <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/9f67d09d-4f6d-4675-834d-f7e03540d09d"> |

| Dark Mode | Subtitle Language Filtering |
|-|-|
| <img width="600" src="https://github.com/qwqcode/SubRenamer/assets/22412567/fa46d20a-3c95-440f-90a1-f50df192c876"> |  <img width="512" src="https://github.com/qwqcode/SubRenamer/assets/22412567/59e1b56f-14d9-4414-adcc-7f259b138a35"> |

| Right-click Menu | Shortcut Support | Settings Interface |
|-|-|-|
| <img width="224" src="https://github.com/qwqcode/SubRenamer/assets/22412567/e890b761-149f-4902-90ea-6f7ff7b91699"> | <img width="224" src="https://github.com/qwqcode/SubRenamer/assets/22412567/b06126e1-4541-442e-b76f-5de792c7db81"> | <img width="412" src="https://github.com/user-attachments/assets/84d5c217-1bf1-4d0d-b137-899189b44553"> |

**Drag & Drop Import**

[Drag & Drop Import Video Demo](https://github.com/qwqcode/SubRenamer/assets/22412567/9de8fa00-6010-4b3a-83a6-2c976dc97090)

## Renaming Instructions

If the video and subtitle files are in **different** folders, the renaming process will **copy** the subtitle files to the video folder without altering the original subtitle files, so no backup is needed.

Conversely, if the video and subtitle files are in the **same** folder, the renaming process will directly modify the subtitle filenames (you can enable backup in settings to save the original subtitle files in the SubBackup directory).

The renamed subtitle filenames will match the video filenames.

## Algorithm Principle

### Automatic Matching Mode

The automatic matching mode determines the episode (extract) by comparing the differences (diff) between filenames and automatically associates video and subtitle files (mapping) to achieve automatic matching.

To perform automatic matching, you need to import at least two video files and two subtitle files with consistent naming formats.

- Algorithm Code: [SubRenamer.Core](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Core) (entry function in [Matcher.cs](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Core/Matcher.cs))
- Unit Test Code: [SubRenamer.Tests](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Tests)
- Test Case Data: [TopLevelTests.json](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/TopLevelTests.json) (**contains example data for the automatic matching algorithm**)

### Manual Matching Mode

The automatic matching mode may fail with complex filename formats, in which case you can switch to manual matching mode. Manual mode allows you to define rules (supporting simple wildcards and regular expressions). The program provides a simple editor for quickly writing matching rules.

## Auto Sync Subtitles

SubRenamer supports automatic subtitle timeline adjustment based on [FFsubsync](https://github.com/qwqcode/ffsubsync-bin) + FFmpeg (automatically aligning subtitle timelines with video/audio).

You need to download the FFsubsync program separately. You can automatically install it by clicking the download button in the settings interface:

<img width="420" alt="image" src="https://github.com/user-attachments/assets/d0184502-4bc8-4c0f-bcc6-431be02612ed">

The sync can be executed right after renaming. Simply check the “Sync” option and click “Rename All” to execute.

You can also import video and subtitle files, then right-click on the list to select “Execute Subtitle Auto Sync Program” to perform the sync alone, without renaming.

<img width="300" alt="image" src="https://github.com/user-attachments/assets/6e26540b-ff5f-44a4-aefd-d76d8f6df21f">

If you encounter network issues and are unable to download, you can try [manual download](https://github.com/qwqcode/ffsubsync-bin) and rename the downloaded file to `ffsubsync_bin.exe`, then place it in the same directory as the `SubRenamer.exe` program.

## FAQ

**macOS can't open, says it's damaged**

There are many solutions online. Here's one method: open the terminal and enter the following command:

```bash
sudo xattr -d com.apple.quarantine /Applications/SubRenamer.app
```

The reason is it isn't signed by an Apple developer, which requires a $99/year developer account.

**No scaling on Linux Wayland desktop environment?**

This is an upstream issue. AvaloniaUI may not scale correctly on Wayland, resulting in small text. You can set the environment variable to manually set the scaling factor at startup.

```bash
AVALONIA_SCREEN_SCALE_FACTORS="eDP-1=2;" ./SubRenamer
```

- https://github.com/AvaloniaUI/Avalonia/issues/9390
- https://github.com/AvaloniaUI/Avalonia/wiki/Configuring-X11-per-monitor-DPI

## Multi-Language Translation (I18n)

SubRenamer supports multiple languages. Currently available languages include:

- English
- 简体中文 (Simplified Chinese)
- 繁體中文 (Traditional Chinese)
- 日本語 (Japanese)

Language files are located in the [SubRenamer/Assets/Lang](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer/Assets/Lang) directory. You can add or improve translations by editing the XAML files. Contributions are welcome, feel free to submit a PR to add more language translations.

## Found a BUG?

Report it on the [issues page](https://github.com/qwqcode/SubRenamer/issues).

## Stargazers over time

[![Stargazers over time](https://starchart.cc/qwqcode/SubRenamer.svg)](https://starchart.cc/qwqcode/SubRenamer)

## Compilation Instructions

It is recommended to use JetBrains Rider or Visual Studio 2022 to open the project.

### Prerequisites

**Windows**

- Visual Studio 2022, including .NET 8 & Desktop development with C++ workload.
- Alternatively, you can install JetBrains Rider to build the project. (Recommended).

**Fedora (36+)**

```bash
sudo dnf group install "C Development Tools and Libraries" "Development Tools"

sudo dnf install dotnet-sdk-8.0 libicu-devel cmake zlib-devel -y
```

**Ubuntu (20.04+)**

```bash
sudo apt-get install dotnet-sdk-8.0 libicu-dev cmake zlib1g-dev -y
```

**macOS (12+)**

```bash
# Install Homebrew
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install xcode command line tools
xcode-select --install

# Install dependencies
brew install dotnet-sdk8 icu4c cmake zlib
```

****

### Unit Testing

```bash
dotnet test SubRenamer.Tests --verbosity normal
```

Unit test code is in the [SubRenamer.Tests](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Tests) directory. It is recommended to use Rider's built-in visual tool to run tests and view results.

<img width="1432" src="https://github.com/user-attachments/assets/4e922f6b-08f0-4e72-9d8d-90db8358e46c">

**Test Data**

The [TopLevelTests.json](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/TopLevelTests.json) file contains test case data, including various subtitle and video filename lists for testing the matching algorithm. Feel free to submit a PR to add more test cases. After modifying the file, run the unit test command to view the results.

Each code submission will trigger unit tests via GitHub Actions to ensure code quality.

### Building a Single File

On Windows, to build a single exe file with statically linked dependencies (without additional dynamic link DLL dependencies), download [these DLL files](https://github.com/qwqcode/qwqcode/releases/tag/dotnet-lib) and place them in the `native` directory. Then add the environment variable `ENABLE_NATIVE_LIBS=true` before compiling.

- https://github.com/qwqcode/SubRenamer/blob/main/.github/workflows/dotnet-desktop.yml
- https://github.com/AvaloniaUI/Avalonia/issues/9503
- https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer/SubRenamer.csproj

### Publish with NativeAOT

```bash
dotnet publish -r <RID> -c Release

# Build for Windows example
dotnet publish -r win-x64 -c Release
```

### Build the installer with NSIS

NSIS installer `~13MB size`

```bash
pwsh ./publish.ps1
```

> If you build the installer with NSIS, you can ignore UPX compression for better startup performance.

## Technical Implementation

- AOT compilation, single file publishing
- Multi-platform packaging and distribution
- Cross-platform adaptation handling
- IoC container, dependency injection, MVVM, LINQ
- JSON source generator
- Multithreading, coroutines
- Global exception handling
- Error log reporting
- JSON configuration management
- Version management, update checking
- Usage statistics
- GitHub API
- GitHub Actions CI/CD
- Unit testing
- Multi-language, internationalization
- HiDPI support

## License

This project is open-sourced under the GPL-2.0 license. See the [LICENSE](./LICENSE) file for details.
