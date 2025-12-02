using Wintakam.ViewModels;

namespace Wintakam.Views;

public partial class PropertyListPage : ContentPage
{
    public PropertyListPage(PropertyListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
