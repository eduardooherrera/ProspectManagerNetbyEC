using ProspectManager.ViewModels;

namespace ProspectManager.Views;

public partial class ActivityFormPage : ContentPage
{
	public ActivityFormPage(ActivityFormViewModel activityFormViewModel)
	{
		InitializeComponent();
		BindingContext = activityFormViewModel;
	}
}