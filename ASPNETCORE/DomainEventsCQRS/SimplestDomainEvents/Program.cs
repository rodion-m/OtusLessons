using SimplestDomainEvents;

DomainEvents.Register((OrderSubmitted ev) => Console.WriteLine($"Order submitted for {ev.Order}"));

var order = new Order();
order.Submit();
