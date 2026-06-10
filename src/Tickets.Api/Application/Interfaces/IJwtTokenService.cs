using Tickets.Api.Domain.Entities;

namespace Tickets.Api.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(User user, DateTime expiresAt);
}