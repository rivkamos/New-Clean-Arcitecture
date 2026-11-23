namespace CleanArchitectureDemo.Application.DTOs;

// מה שהשרת מחזיר ללקוח. לא חושפים את ה-Entity ישירות.
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
