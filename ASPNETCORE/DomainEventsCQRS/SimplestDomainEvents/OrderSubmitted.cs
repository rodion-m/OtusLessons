using SimplestDomainEvents;

public class OrderSubmitted : IDomainEvent
{
    public Order Order { get; }
    
    public OrderSubmitted(Order order)
    {
        Order = order;
    }
}
