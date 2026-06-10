using Tickets.Domain.Entities;

namespace Tickets.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(User user, DateTime expiresAt);
}