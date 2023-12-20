namespace _29_Patterns;

class Device
{
    public string Material { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
}

class Notebook : Device
{
    public double Diagonal { get; set; }
    public int Ram { get; set; }
    public int Hdd { get; set; }
}

class Phone : Device
{
    public int Ram { get; set; }
    public int Hdd { get; set; }
}

class Laptop
{
    public Device Device { get; set; }
    public int Ram { get; set; }
    public int Hdd { get; set; }
    
}

// Перехват зависимостей реализуется с помощью декоратора
public class ThrottlingLogSaverDecorator : ILogSaver
{
    private readonly ILogSaver _decoratee;

    //decoratee - декорируемый объект (ElasticsearchLogSaver)
    public ThrottlingLogSaverDecorator(ILogSaver decoratee)
    {
        _decoratee = decoratee;
    }
    public override async Task SaveLogEntry(
        string applicationId, LogEntrylogEntry entry)
    {
        if (!QuotaReached(applicationId))
        {
            IncrementUsedQuota();
            // Сохраняем записи. Обращаемся к декорируемому объекту!
            await _decoratee.SaveLogEntry(applicationId, logEntry);
            return;
        }
        
        // Сохранение невозможно! Лимит приложения исчерпан!
        throw new QuotaReachedException();
    }
    private bool QuotaReached(string applicationId)
    {
        // Проверяем, израсходована ли квота приложения
    }
    private void IncrementUsedQuota()
    {
        //...
    }
}

class EmailNotificatorLogSaverDecorator : ILogSaver
{
    private readonly ILogSaver _decoratee;
    private readonly IEmailNotificator _emailNotificator;

    public EmailNotificatorLogSaverDecorator(
        ILogSaver decoratee, IEmailNotificator emailNotificator)
    {
        _decoratee = decoratee;
        _emailNotificator = emailNotificator;
    }
    public override async Task SaveLogEntry(
        string applicationId, LogEntrylogEntry entry)
    {
        try
        {
            await _decoratee.SaveLogEntry(applicationId, logEntry);
        }
        catch (Exception e)
        {
            await _emailNotificator.Notify(e);
            throw;
        }
    }
}

public interface ILogSaver
{
    Task SaveLogEntry(string applicationId, LogEntrylogEntry entry);
}
public sealed class ElasticsearchLogSaver : ILogSaver
{
    public Task SaveLogEntry(string applicationId, LogEntrylogEntry entry)
    {
        // Сохраняем переданную запись в Elasticsearch
        return Task.FromResult<object>(null);
    }
}

public static class Example
{
    public static void Main()
    {
        ILogSaver logSaver = new ElasticsearchLogSaver();
        logSaver = new ThrottlingLogSaverDecorator(logSaver);
        logSaver = new EmailNotificatorLogSaverDecorator(logSaver, new EmailNotificator());
        // logSaver уже с троттлингом и нотификацией
    }
}

public interface IEmailNotificator
{
    Task Notify(Exception exception);
}