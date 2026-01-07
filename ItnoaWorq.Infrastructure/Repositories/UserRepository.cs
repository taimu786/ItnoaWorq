using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ItnoaWorq.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
    }
}
