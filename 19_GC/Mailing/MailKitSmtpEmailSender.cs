using System.Net.Sockets;
using Lesson.DI.Configs;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Lesson.DI.Infrastructure.Mailing;

// http://www.mimekit.net/docs/html/T_MailKit_Net_Smtp_SmtpClient.htm
// https://github.com/jstedfast/MailKit/blob/master/Documentation/Examples/SmtpExamples.cs#L93
/*
* Ограничения SMTP-сервера beget:
* не более 30 писем в минуту
* и не более 1500 писем в час
* лимит одновременных соединений для одного IP: 50
* лимит на суммарное/общее количество коннектов: 3000
* время жизни коннекта: 10 минут
*/
public class MailKitSmtpEmailSender : IEmailSender, IDisposable, IAsyncDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly SmtpConfig _config;
    private bool _disposed;

    public MailKitSmtpEmailSender(
        IOptions<SmtpConfig> options, 
        MELProtocolLogger protocolLogger)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (protocolLogger == null) throw new ArgumentNullException(nameof(protocolLogger));
        options.Value.ValidateProperties();
        _config = options.Value;
        _smtpClient = new SmtpClient(_config.EnableLogging ? protocolLogger : new NullProtocolLogger());
    }
    
    // Dispose и DisposeAsync следуют после конструктора
    public void Dispose() //ликвидация зависимости
    {
        if (_disposed) return;
        if (_smtpClient.IsConnected)
        {
            _smtpClient.Disconnect(true);
        }
        _smtpClient.Dispose();
        _disposed = true;
    }
    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        if (_smtpClient.IsConnected)
        {
            //Есть вероятность эксепшена, тогда часть соединений не закроется никогда
            await _smtpClient.DisconnectAsync(true);
        }
        _smtpClient.Dispose();
    }
    
    public void Send(
        string? senderName, 
        string to, 
        string subject, 
        string htmlBody,
        string? senderEmail, 
        CancellationToken cancellationToken = default)
    {
        if (senderName == null) throw new ArgumentNullException(nameof(senderName));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (subject == null) throw new ArgumentNullException(nameof(subject));
        if (htmlBody == null) throw new ArgumentNullException(nameof(htmlBody));
        
        ThrowIfDisposed();
        
        var fromEmail = senderEmail ?? _config.UserName;
        MimeMessage mimeMessage = CreateMimeMessage(senderName, fromEmail, to, subject, htmlBody);

        try
        {
            //см. список исключений в документации. http://www.mimekit.net/docs/html/T_MailKit_Net_Smtp_SmtpCommandException.htm
            EnsureConnectedAndAuthenticated(cancellationToken);
            _smtpClient.Send(mimeMessage, cancellationToken);
        }
        catch (Exception e) when (
            e is SmtpCommandException or SslHandshakeException or SocketException)
        {
            throw new ConnectionException(e.Message, innerException: e);
        }
    }
    
    public async Task SendAsync(string senderName, string to, string subject, string htmlBody, string? senderEmail = null,
        CancellationToken cancellationToken = default)
    {
        if (senderName == null) throw new ArgumentNullException(nameof(senderName));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (subject == null) throw new ArgumentNullException(nameof(subject));
        if (htmlBody == null) throw new ArgumentNullException(nameof(htmlBody));

        ThrowIfDisposed();
        
        var fromEmail = senderEmail ?? _config.UserName;
        MimeMessage mimeMessage = CreateMimeMessage(senderName, fromEmail, to, subject, htmlBody);

        try
        {
            //см. список исключений в документации. http://www.mimekit.net/docs/html/T_MailKit_Net_Smtp_SmtpCommandException.htm
            await EnsureConnectedAndAuthenticatedAsync(cancellationToken);
            await _smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
        catch (Exception e) when (
            e is SmtpCommandException or SslHandshakeException or SocketException)
        {
            throw new ConnectionException(e.Message, innerException: e);
        }
    }

    private static MimeMessage CreateMimeMessage(
        string fromName, string fromEmail, string to, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        var fromAddress = MailboxAddress.Parse(fromEmail);
        fromAddress.Name = fromName;
        message.From.Add(fromAddress);
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = htmlBody };
        return message;
    }
    
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(_smtpClient));
        }
    }
    
    private void EnsureConnectedAndAuthenticated(CancellationToken cancellationToken)
    {
        if (!_smtpClient.IsConnected)
        {
            _smtpClient.Connect(_config.Host, _config.Port, false, cancellationToken); //_config.SecureSocketOptions
        }
        if (!_smtpClient.IsAuthenticated)
        {
            _smtpClient.Authenticate(_config.UserName, _config.Password, cancellationToken);
        }
    }
    private async Task EnsureConnectedAndAuthenticatedAsync(CancellationToken cancellationToken)
    {
        if (!_smtpClient.IsConnected)
        {
            await _smtpClient.ConnectAsync(_config.Host, _config.Port, false, cancellationToken); //_config.SecureSocketOptions
        }
        if (!_smtpClient.IsAuthenticated)
        {
            await _smtpClient.AuthenticateAsync(_config.UserName, _config.Password, cancellationToken);
        }
    }
}