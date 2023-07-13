namespace Lesson.DI.Infrastructure.Mailing;

public class ConnectionException : Exception
{
    public ConnectionException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}