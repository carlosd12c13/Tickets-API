using Microsoft.EntityFrameworkCore;
using Tickets.Application.Interfaces;
using Tickets.Domain.Entities;

namespace Tickets.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetActiveByEmailAsync(string email)
    {
        return _context.Users
            .FirstOrDefaultAsync(x => x.Email == email && x.IsActive);
    }
}
