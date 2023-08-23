using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<Product> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.ProductType)
            .Include(x => x.ProductBrand)
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

        if (product is null)
            throw new Exception("not found product with Id: " + productId);

        return product;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.ProductType)
            .Include(x => x.ProductBrand)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductBrands.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductTypes.ToListAsync(cancellationToken);
    }
}