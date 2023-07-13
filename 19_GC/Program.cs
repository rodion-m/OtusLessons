using _19_GC.Configs;
using _19_GC.Mailing;
using MailKit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

{
    var sender = new MailKitSmtpEmailSender(Options.Create(new SmtpConfig()), new NullProtocolLogger(),CreateLogger());
    sender = null;
}

GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();
GC.WaitForPendingFinalizers();

Thread.Sleep(TimeSpan.FromSeconds(10));

Console.WriteLine("Hello, World!");


ILogger<MailKitSmtpEmailSender> CreateLogger()
{
    using var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Debug);
    });
    return loggerFactory.CreateLogger<MailKitSmtpEmailSender>();
}