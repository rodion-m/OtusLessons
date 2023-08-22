namespace _26_SynchonizationPrimitives.ReaderWriterLockExample;

using System;
using System.Collections.Generic;
using System.Threading;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}

public class ProductRepository
{
    private readonly Dictionary<int, Product> _products = new();
    private readonly ReaderWriterLockSlim _lock = new();

    public void AddProduct(Product product)
    {
        _lock.EnterWriteLock();
        try
        {
            if (!_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
            else
            {
                throw new InvalidOperationException("Product with the same ID already exists");
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void UpdateProduct(Product product)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
            else
            {
                throw new InvalidOperationException("Product not found");
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public Product GetProduct(int id)
    {
        _lock.EnterReadLock();
        try
        {
            _products.TryGetValue(id, out Product product);
            return product;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public IEnumerable<Product> GetAllProducts()
    {
        _lock.EnterReadLock();
        try
        {
            return new List<Product>(_products.Values);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void RemoveProduct(int id)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_products.ContainsKey(id))
            {
                _products.Remove(id);
            }
            else
            {
                throw new InvalidOperationException("Product not found");
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    // Dispose method to clean up the lock if needed
    public void Dispose()
    {
        _lock.Dispose();
    }
}
