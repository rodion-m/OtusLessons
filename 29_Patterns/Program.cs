using _29_Patterns;
using NodaTime;

Meeting(President.Instance);

var newPresident = President.Instance;
newPresident.Name = "Вася";

void Meeting(President president)
{
    Console.WriteLine(president + " is meeting");
}

public sealed class LazySingleton
{
    private static readonly Lazy<LazySingleton> _instance =
        new Lazy<LazySingleton>(() => new LazySingleton());
    LazySingleton() {}
    public static LazySingleton Instance => _instance.Value;
}