using Microsoft.Extensions.Logging;
using ProspectManager.Services;
using ProspectManager.ViewModels;
using ProspectManager.Views;

namespace ProspectManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //builder.Services.AddHttpClient("api", httpClient => httpClient.BaseAddress = new Uri("http://localhost:39257/api/Prospects"));
            builder.Services.AddHttpClient();

;           builder.Services.AddSingleton<IApiService, ApiService>();

            builder.Services.AddTransient<ProspectsViewModel>();
            builder.Services.AddTransient<ProspectsPage>();

            builder.Services.AddTransient<ActivityFormViewModel>();
            builder.Services.AddTransient<ActivityFormPage>();

            builder.Services.AddTransient<ActivitiesViewModel>();
            builder.Services.AddTransient<ActivitiesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
