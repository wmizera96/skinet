using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;
    private readonly IProductRepository _productRepository;

    public ProductsController(StoreContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductsAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductByIdAsync(new ProductId(id), cancellationToken));
    }


    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromQuery] string name)
    {
        _context.Products.Add(new Product { Name = name });
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
    
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyCollection<ProductBrand>>> GetProductBrands(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductBrandsAsync(cancellationToken));
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyCollection<ProductType>>> GetProductTypes(CancellationToken cancellationToken)
    {
        return Ok(await _productRepository.GetProductTypesAsync(cancellationToken));
    }
}