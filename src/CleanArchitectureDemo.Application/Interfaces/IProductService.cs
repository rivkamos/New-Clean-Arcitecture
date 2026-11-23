using CleanArchitectureDemo.Application.DTOs;

namespace CleanArchitectureDemo.Application.Interfaces;

public interface IProductService
{
    ProductDto Add(CreateProductRequest request);
    ProductDto? GetById(int id);
}
