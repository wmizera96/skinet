namespace Core.Entities;

public class ProductBrand : BaseEntity<ProductBrandId>
{
    public override ProductBrandId Id { get; set; } = ProductBrandId.New();
    public string Name { get; set; }
}