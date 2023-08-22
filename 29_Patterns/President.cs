namespace _29_Patterns;

//Антипаттерн:
//1. Нарушает SRP
//2. Нарушает DIP-SOLI(D)
public class President : ICloneable
{
    public static President Instance
    {
        get;
    } = new President("Петя", DateTimeOffset.Now);

    public string Name { get; set; }
    public DateTimeOffset Birhday { get; set; }
    
    // Private constructor
    private President(string name, DateTimeOffset birhday)
    {
        Name = name;
        Birhday = birhday;
    }

    public President ClonePresident()
    {
        return new President(Name, Birhday);
    }

    public object Clone() => ClonePresident();
}