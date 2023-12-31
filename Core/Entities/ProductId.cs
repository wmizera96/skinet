﻿using StronglyTypedIds;

namespace Core.Entities;

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