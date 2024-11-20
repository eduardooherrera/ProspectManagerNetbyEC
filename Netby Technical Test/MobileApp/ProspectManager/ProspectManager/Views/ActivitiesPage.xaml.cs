using ProspectManager.ViewModels;

namespace ProspectManager.Views;

public partial class ActivitiesPage : ContentPage
{
	public ActivitiesPage(ActivitiesViewModel activitiesViewModel)
	{
		InitializeComponent();
		BindingContext = activitiesViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ActivitiesViewModel vm)
        {
            //vm.LoadActivitiesCommand.Execute(null);
        }
    }
}