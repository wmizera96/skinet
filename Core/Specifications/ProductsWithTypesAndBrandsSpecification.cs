using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(products => products.ProductType);
        AddInclude(products => products.ProductBrand);
    }

    public ProductsWithTypesAndBrandsSpecification(ProductId id) : base(x => x.Id == id)
    {
        AddInclude(products => products.ProductType);
        AddInclude(products => products.ProductBrand);
    }
}
