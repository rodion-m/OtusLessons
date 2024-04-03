namespace MediatRExample.Application.Behaviors;

using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Запускаем таймер
        var stopwatch = Stopwatch.StartNew();

        // Логируем начало выполнения команды/запроса
        _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

        // Выполняем команду/запрос
        var response = await next();

        // Останавливаем таймер и логируем время выполнения
        stopwatch.Stop();
        _logger.LogInformation("Handled {RequestName} in {Elapsed} ms", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);

        return response;
    }
}
