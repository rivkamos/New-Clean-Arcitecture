using CleanArchitectureDemo.Application.DTOs;
using CleanArchitectureDemo.Application.Interfaces;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Services;

// כאן נמצאת הלוגיקה העסקית (Use Cases).
// ה-Service מכיר רק את הממשק IProductRepository - הוא לא יודע אם הנתונים בזיכרון, ב-SQL או בקובץ.
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public ProductDto Add(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name.Trim(),
            Price = request.Price
        };

        var saved = _repository.Add(product);
        return ToDto(saved);
    }

    public ProductDto? GetById(int id)
    {
        var product = _repository.GetById(id);
        return product is null ? null : ToDto(product);
    }

    // המרה מ-Entity ל-DTO
    private static ProductDto ToDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };
}
