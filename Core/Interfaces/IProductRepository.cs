using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync(CancellationToken cancellationToken);
}