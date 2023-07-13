namespace _19_GC.Mailing;

public interface IEmailSender
{
    /// <exception cref="ConnectionException">This method has to throw this exception due to connection errors</exception>
    void Send(string senderName,
        string to,
        string subject,
        string htmlBody,
        string? senderEmail = null,
        CancellationToken cancellationToken = default
    );
    
    /// <exception cref="ConnectionException">This method has to throw this exception due to connection errors</exception>
    Task SendAsync(string senderName,
        string to,
        string subject,
        string htmlBody,
        string? senderEmail = null,
        CancellationToken cancellationToken = default
    );
}