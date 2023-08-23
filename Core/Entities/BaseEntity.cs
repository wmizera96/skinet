namespace Core.Entities;

public class BaseEntity
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

public abstract class BaseEntity<T> : BaseEntity where T: IStronglyTypedId
{
    public abstract T Id { get; set; }
}