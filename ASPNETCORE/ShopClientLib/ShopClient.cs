using System.Net.Http.Headers;
using Lesson14.Models.Requests;
using Lesson14.Models.Responses;

namespace ShopClientLib;

public class ShopClient : IDisposable
{
    private readonly string _host;
    private readonly HttpClient _httpClient;
    
    // For DI AddHttpClient
    public ShopClient(HttpClient httpClient)
    {
        _host = "https://default/";
        _httpClient = httpClient;
    }

    public ShopClient(string host, HttpClient httpClient)
    {
        _host = host;
        _httpClient = httpClient;
    }

    public bool IsAuthorizationTokenSet { get; private set; }

    public void SetAuthorizationToken(string token)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));
        var header = new AuthenticationHeaderValue("Bearer", token);
        _httpClient.DefaultRequestHeaders.Authorization = header; //SPA - Single Page Application
        IsAuthorizationTokenSet = true;
    }

    public void ResetAuthorizationToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        IsAuthorizationTokenSet = false;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync<RegisterRequest, RegisterResponse>(
            $"{_host}/auth/register", request);

        SetAuthorizationToken(response!.Token);

        return response;
    }

    public async Task<LogInResponse> LogIn(LogInRequest request)
    {
        LogInResponse? response = await _httpClient.PostAsJsonAsync<LogInRequest, LogInResponse>(
            $"{_host}/auth/login", request);
        
        SetAuthorizationToken(response!.Token);
        
        return response;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}