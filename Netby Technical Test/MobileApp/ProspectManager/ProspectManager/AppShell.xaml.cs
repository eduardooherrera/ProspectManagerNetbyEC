using ProspectManager.Views;

namespace ProspectManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProspectsPage), typeof(ProspectsPage));
            Routing.RegisterRoute($"{nameof(ProspectsPage)}/{nameof(ActivitiesPage)}", typeof(ActivitiesPage));
            Routing.RegisterRoute($"{nameof(ProspectsPage)}/{nameof(ActivitiesPage)}/{nameof(ActivityFormPage)}", typeof(ActivityFormPage));
        }
    }
}
