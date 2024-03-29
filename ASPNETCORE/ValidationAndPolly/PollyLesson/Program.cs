var builder = WebApplication.CreateBuilder(args);

IHttpClientBuilder httpClientBuilder = builder.Services.AddHttpClient("MyClient");

// More info: https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience
// from Microsoft.Extensions.Http.Resilience
httpClientBuilder.AddStandardResilienceHandler();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();