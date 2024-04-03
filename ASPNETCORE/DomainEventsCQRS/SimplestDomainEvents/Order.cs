namespace SimplestDomainEvents;

public class Order
{
    public void Submit()
    {
        // Do some work to submit the order
        DomainEvents.Raise(new OrderSubmitted(this));
    }
}