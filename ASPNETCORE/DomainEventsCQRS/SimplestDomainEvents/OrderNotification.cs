namespace SimplestDomainEvents;

public static class OrderNotification
{
    static OrderNotification()
    {
        DomainEvents.Register<OrderSubmitted>(ev => ProcessSubmittedOrder(ev));
    }

    private static void ProcessSubmittedOrder(OrderSubmitted ev)
    {
        /* Use ev.Order.Lines to compose an email and send it to ev.Order.Customer */
    }
}