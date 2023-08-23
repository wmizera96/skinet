namespace Core.Entities;

public class ProductType : BaseEntity
{
    public ProductTypeId Id { get; set; } = ProductTypeId.New();
    public string Name { get; set; }
}