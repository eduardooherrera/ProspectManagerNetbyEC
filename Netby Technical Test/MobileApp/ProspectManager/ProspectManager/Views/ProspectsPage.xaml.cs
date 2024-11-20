using ProspectManager.ViewModels;

namespace ProspectManager.Views;

public partial class ProspectsPage : ContentPage
{
	public ProspectsPage(ProspectsViewModel prospectsViewModel)
	{
		InitializeComponent();
		BindingContext = prospectsViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await (BindingContext as ProspectsViewModel)?.LoadProspectsAsync();
    }
}