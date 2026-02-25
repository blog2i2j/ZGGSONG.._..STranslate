using CommunityToolkit.Mvvm.ComponentModel;
using iNKORE.UI.WPF.Modern.Controls;
using STranslate.Core;

namespace STranslate.Controls;

/// <summary>
/// PluginMarketSettingsDialog.xaml 的交互逻辑
/// </summary>
public partial class PluginMarketSettingsDialog : ContentDialog
{
    private readonly PluginMarketSettingsViewModel _viewModel;

    public PluginMarketSettingsDialog(Settings settings, DataProvider dataProvider)
    {
        InitializeComponent();

        _viewModel = new PluginMarketSettingsViewModel(settings, dataProvider);
        DataContext = _viewModel;
    }

    /// <summary>
    /// 获取或设置一个值，该值指示设置是否已保存
    /// </summary>
    public bool IsSaved { get; private set; }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // 保存设置
        _viewModel.SaveSettings();
        IsSaved = true;
    }
}

/// <summary>
/// 插件市场设置对话框的视图模型
/// </summary>
public partial class PluginMarketSettingsViewModel(Settings settings, DataProvider dataProvider) : ObservableObject
{
    public DataProvider DataProvider { get; } = dataProvider;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsCustomCdnSource))]
    public partial PluginMarketCdnSourceType SelectedCdnSource { get; set; } = settings.PluginMarketCdnSource;

    [ObservableProperty]
    public partial string CustomCdnUrl { get; set; } = settings.CustomPluginMarketCdnUrl;

    /// <summary>
    /// 是否为自定义 CDN 源
    /// </summary>
    public bool IsCustomCdnSource => SelectedCdnSource == PluginMarketCdnSourceType.Custom;

    /// <summary>
    /// 保存设置到 Settings 对象
    /// </summary>
    public void SaveSettings()
    {
        settings.PluginMarketCdnSource = SelectedCdnSource;
        settings.CustomPluginMarketCdnUrl = CustomCdnUrl;
    }
}
