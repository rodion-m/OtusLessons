using MediatR;

namespace DomainEventsWithMediatR.DomainEvents.Events;

public class IndexPageOpened : INotification
{
    public IndexPageOpened(DateTimeOffset openedAt)
    {
        OpenedAt = openedAt;
    }

    public DateTimeOffset OpenedAt { get; set; }
}