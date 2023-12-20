namespace MaualDomainEvents;

public class MyRandom
{
    public static MyRandom Instance { get; } = new MyRandom();

    public int Next() => Random.Shared.Next();
}

public class Usage
{
    public Usage()
    {
        int rnd = MyRandom.Instance.Next();
    }
}