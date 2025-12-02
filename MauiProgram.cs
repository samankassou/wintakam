using Microsoft.Extensions.Logging;
using Supabase;
using Wintakam.Services;
using Wintakam.ViewModels;
using Wintakam.Views;

namespace Wintakam
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

            // Register Configuration Service
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();

            // Register Supabase Client
            builder.Services.AddSingleton(sp =>
            {
                var configService = sp.GetRequiredService<IConfigurationService>();
                var url = configService.GetSupabaseUrl();
                var key = configService.GetSupabaseAnonKey();

                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = false,
                    AutoRefreshToken = true,
                };

                var client = new Supabase.Client(url, key, options);
                Task.Run(async () => await client.InitializeAsync()).Wait();
                return client;
            });

            // Register Authentication Service
            builder.Services.AddSingleton<IAuthenticationService, SupabaseAuthenticationService>();

            // Register Property Service (Supabase backend)
            builder.Services.AddSingleton<IPropertyService, SupabasePropertyService>();

            // Register Views and ViewModels
            builder.Services.AddSingleton<WelcomePage>();
            builder.Services.AddSingleton<WelcomeViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<PropertyListPage>();
            builder.Services.AddTransient<PropertyListViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
