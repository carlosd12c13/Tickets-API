using Tickets.Domain.Entities;

namespace Tickets.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetActiveByEmailAsync(string email);
}
