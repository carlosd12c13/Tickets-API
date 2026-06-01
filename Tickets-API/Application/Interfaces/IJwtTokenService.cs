using Tickets_API.Domain.Entities;

namespace Tickets_API.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(User user, DateTime expiresAt);
}