using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.ValueConverters;

public class ProductTypeIdValueConverter: ValueConverter<ProductTypeId, Guid>
{
    public ProductTypeIdValueConverter() : base(id => id.Value, value => new ProductTypeId(value))
    { }
}