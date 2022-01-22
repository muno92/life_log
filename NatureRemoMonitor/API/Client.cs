using System.Net.Http.Headers;
using System.Text.Json;
using NatureRemoMonitor.API.Resource;
using NatureRemoMonitor.Exception;

namespace NatureRemoMonitor.API;

public class Client
{
    private readonly HttpClient _httpClient;

    public Client(HttpClient httpClient, string token)
    {
        if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidArgumentException();
        }

        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}");
    }

    public async Task<IEnumerable<Device>> FetchNewestSensorValue()
    {
        var response = await _httpClient.GetAsync("https://api.nature.global/1/devices");
        var responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonSerializer.Deserialize<IEnumerable<Device>>(responseBody) ??
                   throw new Exception.NotSupportedException();
        }
        catch (System.Exception e) when (e is System.NotSupportedException or JsonException)
        {
            throw new Exception.NotSupportedException("Failed to deserialize.", e);
        }
    }
}
