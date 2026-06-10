namespace Tickets.Application.Interfaces;

public interface IAuthTokenSettings
{
    int ExpirationMinutes { get; }
}
