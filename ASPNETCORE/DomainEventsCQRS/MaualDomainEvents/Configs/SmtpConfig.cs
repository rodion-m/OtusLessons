namespace MaualDomainEvents.Configs;

#pragma warning disable CS8618

public class SmtpConfig : ValidableConfig
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public bool EnableLogging { get; set; }

    public override void ValidateProperties()
    {
        EnsureNotNull(Host);
        EnsureNotNull(UserName);
        EnsureNotNull(Password);
    }
}