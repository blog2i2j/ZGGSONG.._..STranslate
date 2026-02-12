using STranslate.ViewModels.Pages;

namespace STranslate.Views.Pages;

public partial class PluginMarketPage
{
    public PluginMarketPage(PluginMarketViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;

        InitializeComponent();
    }

    public PluginMarketViewModel ViewModel { get; }
}
