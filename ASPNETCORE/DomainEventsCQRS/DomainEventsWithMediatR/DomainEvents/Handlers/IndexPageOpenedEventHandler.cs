using DomainEventsWithMediatR.DomainEvents.Events;
using MediatR;

namespace DomainEventsWithMediatR.DomainEvents.Handlers;

public class IndexPageOpenedEventHandler : INotificationHandler<IndexPageOpened>, IDisposable
{
    private readonly ILogger<IndexPageOpenedEventHandler> _logger;
    private readonly ScopedDependency _scopedDependency;

    public IndexPageOpenedEventHandler(
        ILogger<IndexPageOpenedEventHandler> logger, 
        ScopedDependency scopedDependency,
        IServiceProvider serviceProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _scopedDependency = scopedDependency ?? throw new ArgumentNullException(nameof(scopedDependency));
        _logger.LogInformation("Service provider Id: {Id}", serviceProvider.GetHashCode());
        _logger.LogInformation("Created");
    }

    public async Task Handle(IndexPageOpened notification, CancellationToken cancellationToken)
    {
        // Если сервер остановится, то cancellationToken НЕ сработает.
        _logger.LogInformation("Handling IndexPageOpened event");
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        _logger.LogInformation("Handled IndexPageOpened event");
    }

    public void Dispose()
    {
        _logger.LogWarning("Disposing");
    }
}