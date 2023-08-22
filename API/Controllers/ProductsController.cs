using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var productId = new ProductId(id);
        var product = await _context.Products.FindAsync(productId, cancellationToken);
        return Ok(product);
    }


    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromQuery] string name)
    {
        _context.Products.Add(new Product(name));
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var productId = new ProductId(id);
        _context.Products.SoftDelete(productId);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok();
    }
}