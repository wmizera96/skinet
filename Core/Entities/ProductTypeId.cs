using StronglyTypedIds;

namespace Core.Entities;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct ProductTypeId : IStronglyTypedId<ProductType>
{
    public ProductType CreateEmptyEntity()
    {
        return new ProductType()
        {
            Id = this
        };
    }
}