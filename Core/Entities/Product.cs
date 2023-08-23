namespace Core.Entities;

public class Product : BaseEntity<ProductId>
{
    public override ProductId Id { get; set; } = ProductId.New();
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    
    public ProductType ProductType { get; set; }
    public ProductTypeId ProductTypeId { get; set; }
    
    public ProductBrand ProductBrand { get; set; }
    public ProductBrandId ProductBrandId { get; set; }
}