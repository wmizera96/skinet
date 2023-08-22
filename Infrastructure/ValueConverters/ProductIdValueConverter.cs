using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.ValueConverters;

public class ProductIdValueConverter: ValueConverter<ProductId, Guid>
{
    public ProductIdValueConverter() : base(id => id.Value, value => new ProductId(value))
    { }
}