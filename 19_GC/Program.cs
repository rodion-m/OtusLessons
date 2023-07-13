﻿using _19_GC.Configs;
using _19_GC.Mailing;
using MailKit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

{
    for (int i = 0; i < 10_000; i++)
    {
        var sender = new MailKitSmtpEmailSender(Options.Create(new SmtpConfig()), new NullProtocolLogger(),CreateLogger());
    }
}

Console.WriteLine("Hello, World!");


ILogger<MailKitSmtpEmailSender> CreateLogger()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();
        
    using var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddSerilog();
    });
    return loggerFactory.CreateLogger<MailKitSmtpEmailSender>();
}