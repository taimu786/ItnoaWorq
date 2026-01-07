using ItnoaWorq.Domain.Entities.Identity;

namespace ItnoaWorq.Application.Abstraction.Interfaces;

public interface IUserRepository
{
    Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken);
}
