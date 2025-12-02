using System.Text.Json;

namespace Wintakam.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly string _supabaseUrl;
    private readonly string _supabaseAnonKey;

    public ConfigurationService()
    {
        // Try to load from appsettings.json
        var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (File.Exists(appSettingsPath))
        {
            try
            {
                var json = File.ReadAllText(appSettingsPath);
                var config = JsonSerializer.Deserialize<AppSettings>(json);

                _supabaseUrl = config?.Supabase?.Url ?? string.Empty;
                _supabaseAnonKey = config?.Supabase?.AnonKey ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading appsettings.json: {ex.Message}", ex);
            }
        }
        else
        {
            // Fallback to environment variables
            _supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? string.Empty;
            _supabaseAnonKey = Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY") ?? string.Empty;
        }

        if (string.IsNullOrEmpty(_supabaseUrl))
            throw new InvalidOperationException(
                "SUPABASE_URL not configured. Please create appsettings.json file. " +
                "Copy appsettings.example.json and fill in your Supabase credentials.");

        if (string.IsNullOrEmpty(_supabaseAnonKey))
            throw new InvalidOperationException(
                "SUPABASE_ANON_KEY not configured. Please create appsettings.json file. " +
                "Copy appsettings.example.json and fill in your Supabase credentials.");
    }

    public string GetSupabaseUrl() => _supabaseUrl;
    public string GetSupabaseAnonKey() => _supabaseAnonKey;

    private class AppSettings
    {
        public SupabaseSettings? Supabase { get; set; }
    }

    private class SupabaseSettings
    {
        public string? Url { get; set; }
        public string? AnonKey { get; set; }
    }
}
