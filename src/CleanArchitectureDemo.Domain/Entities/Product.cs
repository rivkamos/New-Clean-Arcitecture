namespace CleanArchitectureDemo.Domain.Entities;

// שכבת Domain - הלב של המערכת.
// מכילה רק את הישויות העסקיות. לא תלויה באף שכבה אחרת ובאף ספרייה חיצונית.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
