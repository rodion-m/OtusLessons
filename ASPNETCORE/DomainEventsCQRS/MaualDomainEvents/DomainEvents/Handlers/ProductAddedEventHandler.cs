using MaualDomainEvents.Infrastructure.Mailing;
using Polly;

namespace MaualDomainEvents.DomainEvents.Handlers
{
    public class ProductAddedEventHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductAddedEventHandler> _logger;
        private CancellationToken _stoppingToken;

        public ProductAddedEventHandler(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductAddedEventHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            DomainEventsManager.Register<ProductAdded>(
                productAddedEvent =>
                {
                    _ = SendEmailNotification(productAddedEvent);
                });
        }

        private async Task SendEmailNotification(ProductAdded ev)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            Task SendAsync(CancellationToken cancellationToken)
            {
                return emailSender.SendAsync(
                    "Почтальен Печкин",
                    "income@rodion-m.ru",
                    $"В каталог добавили новый товар",
                    $"Добавлен товар: {ev.Product.Name}",
                    cancellationToken: cancellationToken
                );
            }

            var policy = Policy
                .Handle<ConnectionException>() //ретраим только ошибки подключения
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryAttempt, 2)),
                    (exception, retryAttempt) =>
                    {
                        _logger.LogWarning(
                            exception, "There was an error while sending email. Retrying: {Attempt}", retryAttempt);
                    });
            var result = await policy.ExecuteAndCaptureAsync(SendAsync, _stoppingToken);
            if (result.Outcome == OutcomeType.Failure)
                _logger.LogError(result.FinalException, "There was an error while sending email");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            return Task.CompletedTask;
        }
    }
}