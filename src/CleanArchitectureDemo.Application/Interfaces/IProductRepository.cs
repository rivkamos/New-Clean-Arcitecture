using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Interfaces;

// Application מגדירה "מה" היא צריכה (חוזה) - אבל לא "איך" זה ממומש.
// המימוש נמצא בשכבת Infrastructure. זה עקרון היפוך התלויות (Dependency Inversion).
public interface IProductRepository
{
    Product Add(Product product);
    Product? GetById(int id);
}
