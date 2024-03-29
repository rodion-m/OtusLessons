namespace PollyLesson;

public class MyService
{
    private readonly HttpClient _httpClient;

    public MyService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MyClient");
    }
}