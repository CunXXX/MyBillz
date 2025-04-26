using System.Net.Http.Json;

namespace MyBillz.Services;

public class AppSettingsService
{
    private readonly HttpClient _httpClient;
    public Dictionary<string, object>? Settings { get; private set; }

    public AppSettingsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task LoadAsync()
    {
        Settings = await _httpClient.GetFromJsonAsync<Dictionary<string, object>>("appsettings.json");
    }

    public T Get<T>(string key)
    {
        if (Settings == null)
        {
            throw new InvalidOperationException("Settings have not been loaded yet.");
        }

        if (Settings.TryGetValue(key, out var value))
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        throw new KeyNotFoundException($"Key '{key}' not found in appsettings.json.");
    }
}
