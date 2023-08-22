using StronglyTypedIds;

namespace Core.Entities;

public interface IStronglyTypedId
{
}

public interface IStronglyTypedId<T> : IStronglyTypedId where T: BaseEntity
{
    T CreateEmptyEntity();
}

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct ProductId : IStronglyTypedId<Product>
{
    public Product CreateEmptyEntity()
    {
        var product = new Product
        {
            Id = this
        };
        return product;
    }
}