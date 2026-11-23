using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureDemo.Application.DTOs;

// מה שהלקוח שולח כדי ליצור מוצר (אין Id - השרת קובע אותו)
public class CreateProductRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 1_000_000)]
    public decimal Price { get; set; }
}
