# CLAUDE.md

本文档为 Claude Code (claude.ai/code) 在处理本仓库代码时提供指导。

## 语言规则
- 所有解释、推理和评论必须用简体中文书写。
- 除非是代码、标识符或不可避免的技术术语，否则不得使用英语。
- 错误解释和总结必须用中文。

## 快速构建与调试

```powershell
# 构建并运行（最常用）
dotnet run --project STranslate/STranslate.csproj

# 或构建后运行
dotnet build STranslate.slnx --configuration Debug
./.artifacts/Debug/STranslate.exe
```

更多构建选项参见 [项目概述](docs/overview.md)。

## 多语言支持

本项目支持以下五种语言，语言文件位于 [`STranslate/Languages/`](STranslate/Languages/) 目录：

| 语言 | 文件 | 说明 |
|------|------|------|
| 简体中文 | `zh-cn.xaml` | 默认语言 |
| 繁体中文 | `zh-tw.xaml` | 台湾/香港地区 |
| English | `en.xaml` | 英语 |
| 日本語 | `ja.xaml` | 日语 |
| 한국어 | `ko.xaml` | 韩语 |

### 添加或修改国际化字符串

1. 在所有语言文件中添加相同的键值（参考现有字符串格式）
2. 使用 `_i18n.GetTranslation("KeyName")` 在代码中获取翻译
3. 使用 `Internationalization.GetString("KeyName")` 在 XAML 中绑定

### 开发注意事项

- 新增用户可见的提示信息时，必须添加对应的国际化字符串
- 建议按模块分类组织语言文件（参考现有文件的注释分组）
- 所有语言文件必须保持键的一致性（相同的键存在于所有语言文件中）

## 文档导航

本文档已按功能模块拆分为以下子文档：

### 快速开始
- [**项目概述**](docs/overview.md) - STranslate 项目简介、主要功能、构建命令

### 架构设计
- [**架构设计**](docs/architecture.md) - 核心架构说明
  - 启动流程 - 应用程序启动过程
  - 插件系统 - 插件加载与管理
  - 服务管理 - Service 与 Plugin 的关系
  - 关键接口 - IPlugin、IPluginContext 等接口定义
  - 数据流 - 翻译功能的数据流示例

### 功能特性
- [**功能特性**](docs/features.md) - 热键系统、剪贴板监听、历史记录

### 存储与配置
- [**存储与配置**](docs/storage.md) - 设置架构、存储位置

### 插件开发
- [**插件开发指南**](docs/plugin.md) - 插件开发、包格式、社区插件开发

### 开发参考
- [**参考信息**](docs/reference.md) - 关键文件索引、修改核心服务/UI、技术栈与依赖项

## 快速参考

### 按任务类型查找

| 任务类型 | 关键文件/位置 | 详细文档 |
|---------|-------------|---------|
| **UI 页面开发** | `STranslate/Views/Pages/*.xaml` | [参考信息](docs/reference.md) |
| **ViewModel 逻辑** | `STranslate/ViewModels/Pages/*.cs` | [架构设计](docs/architecture.md) |
| **添加新服务** | `STranslate/ViewModels/Preference/Services/*.cs` | [架构设计](docs/architecture.md#服务管理) |
| **开发插件** | `Plugins/` 目录 | [插件开发指南](docs/plugin.md) |
| **修改翻译服务** | `STranslate/ViewModels/Preference/Services/TranslateServiceViewModel.cs` | - |
| **修改 OCR 服务** | `STranslate/ViewModels/Preference/Services/OCRServiceViewModel.cs` | - |
| **修改 TTS 服务** | `STranslate/ViewModels/Preference/Services/TTSServiceViewModel.cs` | - |
| **热键功能** | `STranslate/Core/HotkeySettings.cs` | [功能特性](docs/features.md) |
| **剪贴板监听** | `STranslate/Helpers/ClipboardMonitor.cs` | [功能特性](docs/features.md) |
| **历史记录** | `STranslate/Core/SqlService.cs` | [功能特性](docs/features.md) |
| **添加国际化** | `STranslate/Languages/*.xaml` | [多语言支持](#多语言支持) |
| **存储配置** | `STranslate/Models/ConfigModel.cs` | [存储与配置](docs/storage.md) |

### 常用文件速查

```
主窗口              -> STranslate/Views/MainWindow.xaml
设置窗口            -> STranslate/Views/SettingsWindow.xaml
翻译输入控件        -> STranslate/Controls/InputControl.xaml
翻译输出控件        -> STranslate/Controls/OutputControl.xaml
头部控件            -> STranslate/Controls/HeaderControl.xaml

插件页面            -> STranslate/Views/Pages/PluginPage.xaml
插件市场页面        -> STranslate/Views/Pages/PluginMarketPage.xaml
常规设置页面        -> STranslate/Views/Pages/GeneralPage.xaml
热键设置页面        -> STranslate/Views/Pages/HotkeyPage.xaml

核心翻译服务        -> STranslate/Core/TranslationService.cs
HTTP 服务           -> STranslate/Core/HttpService.cs
设置管理            -> STranslate/Core/Settings.cs
```

### 常见修改场景

| 想实现的功能 | 参考实现位置 |
|------------|-------------|
| 添加新的设置项 | `STranslate/Models/ConfigModel.cs` + `STranslate/Views/Pages/GeneralPage.xaml` |
| 添加页面到设置窗口 | `STranslate/Views/SettingsWindow.xaml` 的导航菜单 |
| 添加新的图标按钮 | 参考 `PluginPage.xaml` 中的 `ActionIconStyle` |
| 添加带状态的按钮 | 参考 `PluginMarketPage.xaml` 的多状态图标样式 |
| 添加进度显示 | 参考 `PluginMarketPage.xaml` 的下载进度条 |
| 添加拖放功能 | 参考 `PluginPage.xaml` 的 `AllowDrop` 实现 |

## 最近更新 (2026-02-14)

### 插件市场 UI 优化
- **卡片布局统一**：插件市场页面 (`PluginMarketPage.xaml`) 和已安装插件页面 (`PluginPage.xaml`) 统一采用卡片网格布局
- **图标按钮**：将文本按钮替换为图标按钮，使用 Fluent System Icons 字体图标
- **居中显示**：WrapPanel 设置 `HorizontalAlignment="Center"`，使不完整行居中显示
- **新增项目主页按钮**：插件市场卡片添加项目主页跳转链接

### 关键文件变更
- `STranslate/Views/Pages/PluginMarketPage.xaml` - 卡片布局、图标按钮、主页链接
- `STranslate/Views/Pages/PluginPage.xaml` - 统一卡片样式、添加 ToolTip
- `STranslate/Converters/PluginMarketConverters.cs` - 添加状态到可见性转换器
- `STranslate/ViewModels/Pages/PluginMarketViewModel.cs` - 添加 `OpenOfficialLinkCommand`
- `STranslate/Languages/*.xaml` - 新增国际化字符串

