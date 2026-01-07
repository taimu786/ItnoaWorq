using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Domain.Entities.Common;
using ItnoaWorq.Infrastructure.Persistence;

namespace ItnoaWorq.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HrmsDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(HrmsDbContext context) => _context = context;

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            var repo = new Repository<T>(_context);
            _repositories[typeof(T)] = repo;
        }
        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}
