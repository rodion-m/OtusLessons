using System.Runtime.CompilerServices;

namespace Lesson.DI.Configs;

public abstract class ValidableConfig
{
    public abstract void ValidateProperties();

    protected void EnsureNotNull(
        object? property, 
        [CallerArgumentExpression("property")] string? paramName = null)
    {
        if (property is null)
        {
            throw new InvalidPropertyException(paramName!, property);
        }
    }
}