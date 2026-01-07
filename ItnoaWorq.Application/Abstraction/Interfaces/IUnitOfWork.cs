using ItnoaWorq.Domain.Entities.Common;

namespace ItnoaWorq.Application.Abstraction.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
