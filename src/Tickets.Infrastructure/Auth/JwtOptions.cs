using Tickets.Application.Interfaces;

namespace Tickets.Infrastructure.Auth;

public class JwtOptions : IAuthTokenSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; }
}
