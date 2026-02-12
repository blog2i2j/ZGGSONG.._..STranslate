using CommunityToolkit.Mvvm.ComponentModel;

namespace STranslate.Models;

/// <summary>
/// 插件市场信息模型
/// </summary>
public partial class PluginMarketInfo : ObservableObject
{
    /// <summary>
    /// 插件唯一ID
    /// </summary>
    public string PluginId { get; set; } = string.Empty;

    /// <summary>
    /// 插件名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 插件作者
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 插件类型 (Translate/Ocr/Tts/Vocabulary)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 市场版本号
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 插件描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 项目网址
    /// </summary>
    public string Website { get; set; } = string.Empty;

    /// <summary>
    /// 图标URL
    /// </summary>
    public string IconUrl { get; set; } = string.Empty;

    /// <summary>
    /// 下载URL
    /// </summary>
    public string DownloadUrl { get; set; } = string.Empty;

    /// <summary>
    /// 包名 (STranslate.Plugin.Type.Name)
    /// </summary>
    public string PackageName { get; set; } = string.Empty;

    /// <summary>
    /// 是否已安装
    /// </summary>
    [ObservableProperty]
    public partial bool IsInstalled { get; set; }

    /// <summary>
    /// 是否被选中（多选模式）
    /// </summary>
    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    /// <summary>
    /// 是否下载中
    /// </summary>
    [ObservableProperty]
    public partial bool IsDownloading { get; set; }

    /// <summary>
    /// 下载进度 0-100
    /// </summary>
    [ObservableProperty]
    public partial double DownloadProgress { get; set; }

    /// <summary>
    /// 下载状态文本
    /// </summary>
    [ObservableProperty]
    public partial string? DownloadStatus { get; set; }

    /// <summary>
    /// 是否可以升级
    /// </summary>
    [ObservableProperty]
    public partial bool CanUpgrade { get; set; }

    /// <summary>
    /// 当前安装的版本（用于比较）
    /// </summary>
    public string? InstalledVersion { get; set; }

    /// <summary>
    /// 获取操作状态
    /// </summary>
    public PluginActionStatus ActionStatus
    {
        get
        {
            if (IsDownloading)
                return PluginActionStatus.Downloading;
            if (!IsInstalled)
                return PluginActionStatus.Download;
            if (CanUpgrade)
                return PluginActionStatus.Upgrade;
            return PluginActionStatus.Installed;
        }
    }
}

/// <summary>
/// 插件操作状态
/// </summary>
public enum PluginActionStatus
{
    /// <summary>
    /// 可下载
    /// </summary>
    Download,

    /// <summary>
    /// 已安装最新版本
    /// </summary>
    Installed,

    /// <summary>
    /// 可升级
    /// </summary>
    Upgrade,

    /// <summary>
    /// 下载中
    /// </summary>
    Downloading
}
