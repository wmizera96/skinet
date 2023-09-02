using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<T?> GetByIdAsync(IStronglyTypedId<T> id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);

    Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);

}
