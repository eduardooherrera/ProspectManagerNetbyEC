
using Newtonsoft.Json;
using ProspectManager.Models;
using ProspectManager.Services;
using ProspectManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProspectManager.ViewModels
{
    [QueryProperty(nameof(ProspectId), "prospectId")]
    public partial class ActivitiesViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private string _prospectId;

        public string ProspectId { set { Initialize(value); } }

        public ObservableCollection<ActivityModel> Activities { get; } = [];
        public ICommand LoadActivitiesCommand { get; }
        public ICommand AddActivityCommand { get; }
        public ICommand EditActivityCommand { get; }
        public ICommand DeleteActivityCommand { get; }
        public ICommand GoBackCommand { get; }

        public ActivitiesViewModel(IApiService apiService)
        {
            _apiService = apiService;

            LoadActivitiesCommand = new Command(async () => await LoadActivitiesAsync());
            AddActivityCommand = new Command(async () => {
                    await Shell.Current.GoToAsync($"{nameof(ActivityFormPage)}?prospectId={_prospectId}");
                }
            );
            EditActivityCommand = new Command<ActivityModel>(async (activity) =>
            {
                string serializedActivity = JsonConvert.SerializeObject(activity);
                await Shell.Current.GoToAsync($"{nameof(ActivityFormPage)}?activity={serializedActivity}");
            });
            DeleteActivityCommand = new Command<ActivityModel>(async (activity) =>
            {
                await _apiService.DeleteActivityAsync(activity.Id);
                await LoadActivitiesAsync();
            });
            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void Initialize(string prospectId)
        {
            _prospectId = prospectId;
            LoadActivitiesCommand.Execute(null);
        }

        private async Task LoadActivitiesAsync()
        {
            Activities.Clear();
            var activities = await _apiService.GetActivitiesByProspectIdAsync(_prospectId);
            foreach (var activity in activities.Activities ?? [])
            {
                activity.ProspectId = Guid.Parse(_prospectId);
                Activities.Add(activity);
            }
        }
    }
}
