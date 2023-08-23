using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.ValueConverters;

public class ProductBrandIdValueConverter : ValueConverter<ProductBrandId, Guid>
{
    public ProductBrandIdValueConverter() : base(id => id.Value, value => new ProductBrandId(value))
    { }
}