using CleanArchitectureDemo.Application.Interfaces;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Infrastructure.Repositories;

// שכבת Infrastructure - המימוש הטכני של הממשק שהוגדר ב-Application.
// כאן שומרים ברשימה בזיכרון. בעתיד אפשר להחליף ל-SQL Server (EF Core)
// בלי לשנות אף שורה ב-Application או ב-Api.
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public Product Add(Product product)
    {
        lock (_lock)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }
    }

    public Product? GetById(int id)
    {
        lock (_lock)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}
