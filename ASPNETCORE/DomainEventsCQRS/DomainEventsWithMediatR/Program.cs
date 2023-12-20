using DomainEventsWithMediatR;
using DomainEventsWithMediatR.DomainEvents.Events;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ScopedDependency>();
builder.Services.AddMediatR(cfg =>
{
    //Обычно все обработчики событий находятся в слое Application
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});

var app = builder.Build();
app.MapGet("/", async (IMediator mediator, ILogger<Program> logger, IServiceProvider serviceProvider) =>
{
    logger.LogInformation("Index page opened");
    logger.LogInformation("Service provider Id: {Id}", serviceProvider.GetHashCode());
    
    await mediator.Publish(new IndexPageOpened(DateTimeOffset.Now));
    return "Hello World!";
});

app.Run();