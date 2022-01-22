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

        var devices = JsonSerializer.Deserialize<IEnumerable<Device>>(responseBody);
        if (devices == null)
        {
            throw new Exception.NotSupportedException();
        }

        return devices;
    }
}
