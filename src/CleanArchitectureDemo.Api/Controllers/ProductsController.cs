using CleanArchitectureDemo.Application.DTOs;
using CleanArchitectureDemo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Api.Controllers;

// שכבת Api - רק מקבלת בקשת HTTP, מעבירה ל-Service ומחזירה תשובה.
// אין כאן לוגיקה עסקית ואין גישה ישירה לנתונים.
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET api/products/1
    [HttpGet("{id:int}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _productService.GetById(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    // POST api/products
    [HttpPost]
    public ActionResult<ProductDto> Add(CreateProductRequest request)
    {
        var created = _productService.Add(request);

        // 201 Created + כתובת למשאב החדש (Location header)
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
