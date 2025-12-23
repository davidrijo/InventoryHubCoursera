using Microsoft.AspNetCore.Mvc;
using InventoryHub.Shared;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    // Mock data (In a real app, this would be a Database)
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Quantity = 10, Price = 1200.00m, Category = "Electronics" },
        new Product { Id = 2, Name = "Mouse", Quantity = 50, Price = 25.50m, Category = "Accessories" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts() => Ok(_products);

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }
}