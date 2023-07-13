
var products = new Product[]
{
    new Product { Id = 1, Name = "Milk", Price = 1.20m, IsDiscounted = false },
    new Product { Id = 2, Name = "Bread", Price = 0.80m, IsDiscounted = true },
    new Product { Id = 3, Name = "Butter", Price = 1.50m, IsDiscounted = false },
    new Product { Id = 4, Name = "Cheese", Price = 2.00m, IsDiscounted = true },
    new Product { Id = 5, Name = "Ham", Price = 1.80m, IsDiscounted = false },
    new Product { Id = 6, Name = "Eggs", Price = 1.00m, IsDiscounted = false },
    new Product { Id = 7, Name = "Water", Price = 0.50m, IsDiscounted = false },
    new Product { Id = 8, Name = "Juice", Price = 1.20m, IsDiscounted = false },
    new Product { Id = 9, Name = "Coke", Price = 1.00m, IsDiscounted = false },
    new Product { Id = 10, Name = "Beer", Price = 1.80m, IsDiscounted = true }
};

// (product) => product.Name.Contains('a') - лямбда-выражение (анонимная ф-я)
List<Product> filteredProducts = Filter(
    products, 
    LeaveOnlyDiscountedProducts,
    Console.WriteLine
);

bool LeaveOnlyDiscountedProducts(Product product)
{
    return product.IsDiscounted;
}

Console.WriteLine(string.Join(Environment.NewLine, filteredProducts));
return;

// Func<Product, bool> - Product - передаем в делегат из Filter, bool проверяем внутри метода Filter
// Action<int> - по сути void Func (ничего не возвращает)
// Predicate - по сути Func с заранее заданным типом bool

List<Product> Filter(IReadOnlyList<Product> products, ProductFilterCondition condition, Action<int>? progress = null)
{
    var list = new List<Product>();
    for (var index = 0; index < products.Count; index++)
    {
        var product = products[index];
        Thread.Sleep(1000);
        progress?.Invoke(index);
        if (condition(product))
        {
            list.Add(product);
        }
    }

    return list;
}

// Func<Product, bool>
public delegate bool ProductFilterCondition(Product product);

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public bool IsDiscounted { get; set; }
    public DateTimeOffset? ProductedAt { get; set; }

    public override string ToString() => $"{Id} - {Name} - {Price} - {IsDiscounted}";
}