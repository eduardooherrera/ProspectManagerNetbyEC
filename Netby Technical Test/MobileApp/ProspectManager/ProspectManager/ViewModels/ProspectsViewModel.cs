using ProspectManager.Models;
using ProspectManager.Services;
using ProspectManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProspectManager.ViewModels
{
    public class ProspectsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<ProspectModel> Prospects { get; } = [];

        private ProspectModel _selectedProspect;
        public ProspectModel SelectedProspect
        {
            get => _selectedProspect;
            set
            {
                _selectedProspect = value;
                OnPropertyChanged();
                if (_selectedProspect != null)
                {
                    OpenActivitiesCommand.Execute(null);
                }
            }
        }

        public ICommand LoadProspectsCommand { get; }
        public ICommand OpenActivitiesCommand { get; }

        public ProspectsViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoadProspectsCommand = new Command(async () => await LoadProspectsAsync());
            OpenActivitiesCommand = new Command(async () =>
            {
                if (SelectedProspect != null)
                {
                    await Shell.Current.GoToAsync($"{nameof(ActivitiesPage)}?prospectId={SelectedProspect.Id.ToString()}");
                }
            });
        }

        public void Initialize()
        {
            LoadProspectsCommand.Execute(null);
        }

        public async Task LoadProspectsAsync()
        {
            Prospects.Clear();
            var prospects = await _apiService.GetProspectsAsync();
            foreach (var prospect in prospects)
            {
                Prospects.Add(prospect);
            }
        }
    }
}
