namespace Core.Entities;

public interface IStronglyTypedId
{
    Guid Value { get; }
}

public interface IStronglyTypedId<T> : IStronglyTypedId where T: BaseEntity
{
    T CreateEmptyEntity();
}