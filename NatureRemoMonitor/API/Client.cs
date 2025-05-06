using System.Net.Http.Headers;
using System.Text.Json;
using NatureRemoMonitor.Exception;
using Device = NatureRemoMonitor.API.Resource.Device;
using NotSupportedException = NatureRemoMonitor.Exception.NotSupportedException;

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

        var option = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        };

        try
        {
            return JsonSerializer.Deserialize<IEnumerable<Device>>(responseBody, option) ??
                   throw new NotSupportedException();
        }
        catch (System.Exception e) when (e is System.NotSupportedException or JsonException)
        {
            throw new NotSupportedException("Failed to deserialize.", e);
        }
    }
}
