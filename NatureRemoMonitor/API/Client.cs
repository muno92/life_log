using NatureRemoMonitor.Exception;

namespace NatureRemoMonitor.API;

public class Client
{
    private readonly HttpClient _httpClient;
    private readonly string _token;

    public Client(HttpClient httpClient, string token)
    {
        if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidArgumentException();
        }

        _httpClient = httpClient;
        _token = token;
    }
}
