using System.Collections.Concurrent;

namespace MaualDomainEvents.DomainEvents;

public static class DomainEventsManager
{
    private static readonly ConcurrentDictionary<Type, List<Delegate>> Handlers = new();

    public static void Register<TEvent>(Action<TEvent> eventHandler) where TEvent : IDomainEvent
    {
        Handlers.AddOrUpdate(typeof(TEvent), 
            addValueFactory: _ => new List<Delegate>() {eventHandler},
            updateValueFactory: (_, delegates) =>
            {
                delegates.Add(eventHandler);
                return delegates;
            });
    }

    public static void Raise<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        foreach (Delegate handler in Handlers[domainEvent.GetType()])
        {
            var action = (Action<TEvent>) handler;
            action(domainEvent);
        }
    }
}