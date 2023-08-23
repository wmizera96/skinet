using StronglyTypedIds;

namespace Core.Entities;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct ProductBrandId : IStronglyTypedId<ProductBrand>
{
    public ProductBrand CreateEmptyEntity()
    {
        return new ProductBrand
        {
            Id = this
        };
    }
}