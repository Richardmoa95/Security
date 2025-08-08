using Microsoft.EntityFrameworkCore;
using UserDataService.Core.Entities;
using UserDataService.Core.Interfaces;
using UserDataService.Infrastructure.Data;

namespace UserDataService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
    }
}
