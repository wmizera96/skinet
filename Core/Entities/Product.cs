namespace Core.Entities;

public class Product : BaseEntity
{
    public ProductId Id { get; set; } = ProductId.New();
    public string Name { get; set; }

    internal Product()
    {
        // parameterless ctor for Entity Framework
        Name = string.Empty;
    }
    
    public Product(string name)
    {
        Name = name;
    }
}