using Newtonsoft.Json;
using ProspectManager.Models;
using ProspectManager.Services;
using ProspectManager.Views;
using System.Globalization;
using System.Windows.Input;

namespace ProspectManager.ViewModels
{
    [QueryProperty("Activity", "activity")]
    [QueryProperty("ProspectId", "prospectId")]
    public class ActivityFormViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private string? _prospectId;
        private string? _activity;
        private int? _activityId;

        public string ProspectId { set { Initialize(value); } }
        public string Activity { set { InitializeActivity(value); } }

        public string? Description { get; set; }
        public string? Type { get; set; }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        public int Rating { get; set; }

        public List<string> ActivityTypes { get; } = ["llamada", "mensaje", "correo"];
        public ICommand SaveActivityCommand { get; }
        public ICommand GoBackCommand { get; }

        public ActivityFormViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Date = DateTime.Today;
            SaveActivityCommand = new Command(async () => await SaveActivityAsync());
            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(ProspectsPage)}/{nameof(ActivitiesPage)}?prospectId={_prospectId}"));
        }

        public void Initialize(string prospectId)
        {
            _prospectId = prospectId;

        }

        public void InitializeActivity(string activity)
        {
            _activity = activity;

            if (_activity != null)
            {
                LoadActivityAsync(_activity);
            }
        }

        private void LoadActivityAsync(string activityId)
        {
            var activity =  JsonConvert.DeserializeObject<ActivityModel>(activityId);

            string format = "dd/MM/yyyy HH:mm:ss";
            _activityId = activity?.Id;
            _prospectId = activity?.ProspectId.ToString();
            Description = activity?.Description ?? "";
            Type = activity?.Type ?? "";
            Date = DateTime.ParseExact(activity?.Date ?? DateTime.Now.ToString(), format, CultureInfo.InvariantCulture); ;
            Rating = activity?.Rating ?? 1;
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Rating));
        }

        private async Task SaveActivityAsync()
        {
            if (Rating >= 1 && Rating <= 5)
            {
                if(!String.IsNullOrEmpty(_prospectId))
                {
                    var activity = new ActivityModelInput
                    {
                        Id = _activityId ?? 0,
                        ProspectId = Guid.Parse(_prospectId ?? ""),
                        Description = Description ?? "",
                        Type = Type ?? "",
                        Date = Date,
                        Rating = Rating
                    };


                    if (_activityId.HasValue)
                    {
                        await _apiService.UpdateActivityAsync(activity);
                    }
                    else
                    {
                        await _apiService.CreateActivityAsync(activity);
                    }
                }
                await Shell.Current.GoToAsync($"../../");
            }
            else
            {
                await Application.Current?.MainPage?.DisplayAlert("Error", "La calificación debe ser un número entre 1 y 5.", "OK");
            }
        }
    }
}
