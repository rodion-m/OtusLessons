using MimeKit;

namespace MaualDomainEvents.Wrong;

public interface IEmailSender1
{
    void Send(MimeMessage message);
}