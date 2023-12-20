namespace MaualDomainEvents.Configs;

public class InvalidPropertyException : Exception
{
    public InvalidPropertyException(string propertyName, object? property) 
        : base($"Property {propertyName} is not set. Actual value was: {property}")
    {
    }
}