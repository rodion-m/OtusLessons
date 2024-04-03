namespace SimplestDomainEvents;

public static class DomainEvents
{
    private static readonly Dictionary<Type, List<Delegate>> Handlers = new();

    public static void Register<T>(Action<T> eventHandler)
        where T : IDomainEvent
    {
        if (!Handlers.ContainsKey(typeof(T)))
        {
            Handlers.Add(typeof(T), new List<Delegate>());
        }

        Handlers[typeof(T)].Add(eventHandler);
    }

    public static void Raise<T>(T domainEvent)
        where T : IDomainEvent
    {
        foreach (Delegate handler in Handlers[domainEvent.GetType()])
        {
            var action = (Action<T>)handler;
            action(domainEvent);
        }
    }
}