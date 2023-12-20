using System.Collections.Concurrent;

namespace MaualDomainEvents.DomainEvents;

public static class DomainEventsManager
{
    private static readonly ConcurrentDictionary<Type, List<Delegate>> Handlers = new();

    public static void Register<T>(Action<T> eventHandler) where T : IDomainEvent
    {
        Handlers.AddOrUpdate(typeof(T), 
            addValueFactory: _ => new List<Delegate>() {eventHandler},
            updateValueFactory: (_, delegates) =>
            {
                delegates.Add(eventHandler);
                return delegates;
            });
    }

    public static void Raise<T>(T domainEvent) where T : IDomainEvent
    {
        foreach (Delegate handler in Handlers[domainEvent.GetType()])
        {
            var action = (Action<T>) handler;
            action(domainEvent);
        }
    }
}