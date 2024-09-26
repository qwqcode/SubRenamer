<p align="center"><img src="https://github.com/qwqcode/SubRenamer/assets/22412567/3a49c011-ce41-4bc3-ab85-5237a6e9acd7"></p>

# SubRenamer

<img src="https://github.com/qwqcode/SubRenamer/assets/22412567/ef9b38b0-d1c6-4f1f-9f7e-f7b67a36d9b5" width="150" align="right" />

🎞 字幕文件批量改名工具

A Tool for Batch Rename Subtitle Files to Match Video Names with One Click.

(Read this in other languages: [English](README.en.md))

**原因？** 如果视频和字幕文件名一致，任何视频播放器都可以自动载入字幕。

**目的？** 重命名外挂字幕文件名，使之与视频文件名对应。

[![](https://img.shields.io/github/release/qwqcode/SubRenamer.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/releases/latest) [![](https://img.shields.io/github/downloads/qwqcode/SubRenamer/total.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/releases) [![](https://img.shields.io/github/issues/qwqcode/SubRenamer.svg?style=flat-square)](https://github.com/qwqcode/SubRenamer/issues)

## 对比普通批量改名软件，有什么区别？

SubRenamer 专注于字幕文件改名，简单易用。

对于大多数视频与字幕文件，您仅需将其拖入程序，即可被自动精准地识别，一键改名，省去了普通改名软件较为繁杂的设置操作。

## 如何拥有 SubRenamer?

点击以下链接下载最新版本：

| [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/2772a99b-f10f-48cd-aed7-58488e7a726e">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_windows_amd64.zip) | [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/0aef7104-b7bc-4bde-94c3-3f9df044d66b">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_macos_arm64.zip) | [<img width="32" src="https://github.com/qwqcode/SubRenamer/assets/22412567/8b41fffd-2eb3-4a78-b1bd-8751a09c36c5">](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_linux_amd64.tar.gz) |
|-|-|-|
| [Windows (x86)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_windows_amd64.zip) | [macOS (M1)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_macos_arm64.zip) | [Linux (x86)](https://github.com/qwqcode/SubRenamer/releases/latest/download/SubRenamer_linux_amd64.tar.gz) |


可在 [Release](https://github.com/qwqcode/SubRenamer/releases) 页面找到历史版本和更新日志。

如果下载速度慢，可以尝试网盘下载：[蓝奏云](https://wwi.lanzoui.com/s/sub-renamer) (密码: 233)

## 使用教程

[【B站】「追番神器」真 · 番剧字幕一键重命名 自动化字幕文件批量改名工具程序](https://www.bilibili.com/video/BV1e7411y7rG)

[【小众软件】SubRenamer – 字幕批量重命名，自动匹配视频文件与字幕文件[Windows]](https://www.appinn.com/subrenamer-for-windows)

## 特性

- **自动匹配**：自动识别算法，一键匹配
- **拖拽导入**：拖拽快速导入文件及文件夹
- **多语言匹配**：支持视频字幕多语言匹配（一对多映射）
- **多语言筛选**：导入前自动检测并筛选指定语言的字幕
- **多匹配规则**：对于复杂的文件名格式，支持手动匹配
- **手动匹配编辑器**：自定义规则，支持简单通配符
- **正则表达式编辑器**：包含正则表达式匹配测试工具
- **匹配微调**：支持对匹配结果进行微调
- **改名命令**：右键快速复制 Linux 改名命令到剪贴板
- **字幕备份**：改名前自动备份字幕文件
- **追加后缀**：支持在文件扩展名前添加自定义后缀
- **文件识别**：通过文件扩展名自动区分视频和字幕，支持自定义
- **快捷键**：支持快捷键操作，提高效率
- **夜间模式**：支持夜间模式，跟随系统切换
- **窗口置顶**：支持窗口置顶，方便操作
- **跨平台**：支持 Windows、macOS、Linux
- **体积小**：仅 15MB 左右

> [!IMPORTANT]\
> 重制说明：SubRenamer 第一版于 2019 年发布，当时使用 WinForm 进行开发，仅支持 Windows 平台。2024 年 SubRenamer 完成重制发布 v2.0 版本，采用全新技术栈 AvaloniaUI + .NET 8 开发，支持跨平台，可以在 Windows、macOS、Linux 上原生运行（不是 Electron.js）。

<img width="800" src="https://github.com/qwqcode/SubRenamer/assets/22412567/9b620a47-61cb-418a-b6d3-3dd2e0140f69">

| 匹配编辑 | 匹配规则自定义 |
|-|-|
| <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/2976022a-2545-4e0e-8202-bd3e00708e4a"> | <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/7dd80067-74c8-4c73-939f-fd7b01cb3d2b"> |

| 手动匹配规则编辑器 | 正则表达式规则编辑器 |
|-|-|
| <img width="822" src="https://github.com/qwqcode/SubRenamer/assets/22412567/ec201431-0bbc-4ca2-8963-f7ec1ce46e32"> | <img width="612" src="https://github.com/qwqcode/SubRenamer/assets/22412567/9f67d09d-4f6d-4675-834d-f7e03540d09d"> |

| 夜间模式 | 字幕语言筛选 |
|-|-|
| <img width="600" src="https://github.com/qwqcode/SubRenamer/assets/22412567/fa46d20a-3c95-440f-90a1-f50df192c876"> |  <img width="512" src="https://github.com/qwqcode/SubRenamer/assets/22412567/59e1b56f-14d9-4414-adcc-7f259b138a35"> |

| 右键菜单 | 快捷键支持 | 设置界面 |
|-|-|-|
| <img width="224" src="https://github.com/qwqcode/SubRenamer/assets/22412567/e890b761-149f-4902-90ea-6f7ff7b91699"> | <img width="224" src="https://github.com/qwqcode/SubRenamer/assets/22412567/b06126e1-4541-442e-b76f-5de792c7db81"> | <img width="412" src="https://github.com/user-attachments/assets/84d5c217-1bf1-4d0d-b137-899189b44553"> |

**拖拽导入文件**

[拖拽导入视频演示](https://github.com/qwqcode/SubRenamer/assets/22412567/9de8fa00-6010-4b3a-83a6-2c976dc97090)

## 改名说明

拖入的视频和字幕文件如果处于**不同**的文件夹内，执行改名只会将字幕文件**复制**到视频所在的文件夹中，**不会改动**原始字幕文件，所以无需备份。

反之，如果视频和字幕文件名处于**相同**的文件夹内，执行改名会直接修改字幕文件名（设置可以勾选启用备份，将原始字幕文件备份到 SubBackup 目录内）。

改名后的字幕文件名将与视频文件名一致。

## 算法原理

### 自动匹配模式

自动匹配模式通过比对文件名之间的差异部分 (diff) 来确定集数 (extract)，并根据集数自动关联视频文件和字幕文件 (mapping)，以实现自动匹配。

为实施自动匹配，需导入至少两个文件名格式一致的视频文件和两个字幕文件。

- 算法代码：[SubRenamer.Core](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Core)（入口函数位于 [Matcher.cs](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Core/Matcher.cs) 文件内）
- 单元测试代码：[SubRenamer.Tests](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Tests)
- 测试用例数据：[TopLevelTests.json](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/TopLevelTests.json)（**其中包含了自动匹配算法的示例数据**）

### 手动匹配模式

自动匹配模式可能在复杂的文件名格式下失效，此时可切换至手动匹配模式，手动模式允许你自定义规则（支持简单通配符和正则表达式）。程序提供一个简单的编辑器，以便你可以快速地编写匹配规则。

## 用户故事

<details>

<summary>请听 ABCDE 的故事（🌫️</summary>

> (缩减版) 小A下载了一部新更的生肉番，又从字幕网站下载到了一套字幕文件，生肉番的 视频文件名 常常和 字幕文件名 不一致，看番时需要手动选定字幕，下次打开又得重新选定。小A拥有了 **SubRenamer**，从此改名交给他来做，终于可以安安心心看番啦。

> (探究版) 小B今天下载了一部番剧，小B因不会他国语言从而需要找寻一套字幕。小B下载到了字幕，但因 字幕文件名 与 视频文件名 不相对应，播放器无法自动载入字幕文件，小B因每次都要手动选择字幕文件而烦恼万分。最终，小B实在受不鸟了，毅然决然决定修改文件名...... 所以问题来了，小B如何才能快速地修改字幕文件名，而不是一个一个慢慢地手动修改呢？？？

> (激情版) 小C热爱学习，小C下载了一套100000000集的学习视频，提升自我人生价值的大好机会到了，准备今天晚上就开淦(darkbubi)，可到了晚上，小C打开下载好的视频时却突然想起，自己什么也(bing)听(bu)不(xiang)懂(xue)（此刻的小C对于学习的热情瞬间熄灭）。可是突然！小C发现了 **SubRenamer**，下载一套字幕后，修改按钮一敲，100000000集的学习视频字幕文件顺利加载，小C对于学习的热情死(bu)灰(ke)复(neng)燃(di)

> (慵懒版) 小D拥有了 **SubRenamer** 后，字幕文件改名的操作全交给 **SubRenamer**，省去了大量时间可以留给睡觉。

> (蜜汁版) 小E . .o. 0。.O . 。o.

> (稽智版) 小F选择重新下载内挂字幕的番剧 lol

</details>

## FAQ

**macOS 无法打开，提示已损坏**

网上可以找到很多解决的方法，这里提供一个方法，在终端中输入以下命令：

```bash
sudo xattr -d com.apple.quarantine /Applications/SubRenamer.app
```

原因是没有经过苹果开发者签名，macOS 会提示已损坏，而注册开发者账号需要 99 美元/年。

**在 Linux Wayland 桌面环境没有缩放？**

这是一个上游问题，AvaloniaUI 在 Wayland 桌面环境下缩放比例可能不准确，导致文字很小，可以在启动时设置环境变量手动设置缩放比例。

```bash
AVALONIA_SCREEN_SCALE_FACTORS="eDP-1=2;" ./SubRenamer
```

- https://github.com/AvaloniaUI/Avalonia/issues/9390
- https://github.com/AvaloniaUI/Avalonia/wiki/Configuring-X11-per-monitor-DPI

## 多语言翻译 (I18n)

SubRenamer 支持多语言切换，目前支持的语言有：

- English
- 简体中文 (Simplified Chinese)
- 繁體中文 (Traditional Chinese)
- 日本語 (Japanese)

语言文件位于：[SubRenamer/Assets/Lang](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer/Assets/Lang) 目录内，可以通过编辑 XAML 文件来添加或完善语言翻译。我们期待你的参与，欢迎提交 PR 添加更多语言翻译。

## 有 BUG?

可在 [issues 页](https://github.com/qwqcode/SubRenamer/issues) 反馈。

## Stargazers over time

[![Stargazers over time](https://starchart.cc/qwqcode/SubRenamer.svg)](https://starchart.cc/qwqcode/SubRenamer)

## 编译说明

建议使用 JetBrains Rider 或 Visual Studio 2022 打开项目。

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

### 单元测试

```bash
dotnet test SubRenamer.Tests --verbosity normal
```

单元测试代码位于 [SubRenamer.Tests](https://github.com/qwqcode/SubRenamer/tree/main/SubRenamer.Tests) 目录内，推荐使用 Rider 内置的可视化工具执行测试和查看测试结果。

<img width="1432" src="https://github.com/user-attachments/assets/4e922f6b-08f0-4e72-9d8d-90db8358e46c">

**测试数据**

[TopLevelTests.json](https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/TopLevelTests.json) 文件存放了测试用例数据，包含各种各样的字幕和视频文件名列表用于测试匹配算法，欢迎提交 PR 添加更多测试用例，修改文件后执行单元测试命令即可查看测试结果。

每次代码提交将通过 GitHub Actions 自动执行单元测试，确保代码质量。

### 构建单文件

在 Win 平台，为了构建出单个包含静态链接依赖库的 exe 文件（无额外的动态链接 dll 依赖库文件），需要手动把 [这几个 dll 文件](https://github.com/qwqcode/qwqcode/releases/tag/dotnet-lib) 下载放到 `native` 目录内。然后添加环境变量 `ENABLE_NATIVE_LIBS=true` 再执行编译。

- https://github.com/qwqcode/SubRenamer/blob/main/.github/workflows/dotnet-desktop.yml
- https://github.com/AvaloniaUI/Avalonia/issues/9503
- https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer/SubRenamer.csproj

### Publish with NativeAOT

```bash
dotnet publish -r <RID> -c Release

# Build for Windows example
dotnet publish -r win-x64 -c Release
```

### Builder the installer with NSIS

NSIS installer `~13MB size`

```bash
pwsh ./publish.ps1
```

> if you builder the installer with nsis, you can ignore upx compression, so you can get better startup performance.

## 技术实现

- AOT 编译，单文件发布
- 多平台打包及分发
- 跨平台适配处理
- IoC 容器，依赖注入，MVVM，LINQ
- JSON 源生成器
- 多线程，纤程
- 全局异常捕获
- 错误日志反馈
- JSON 配置管理
- 版本管理，升级检查
- 用量统计
- GitHub API
- GitHub Actions CI/CD
- 单元测试
- 多语言，国际化
- HiDPI 支持

## 开源协议

本项目采用 GPL-2.0 协议开源，详见 [LICENSE](./LICENSE) 文件。
