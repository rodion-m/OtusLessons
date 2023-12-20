namespace DomainEventsWithMediatR;

public class ScopedDependency : IDisposable
{
    private readonly ILogger<ScopedDependency> _logger;

    public ScopedDependency(ILogger<ScopedDependency> logger)
    {
        _logger = logger;
    }

    public void Dispose()
    {
        _logger.LogWarning("Disposing");
    }
}