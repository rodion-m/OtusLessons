using MaualDomainEvents.Entities;

namespace MaualDomainEvents.DomainEvents;

public class ProductAdded : IDomainEvent
{
    public Product Product { get; }
    
    public ProductAdded(Product product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }
}