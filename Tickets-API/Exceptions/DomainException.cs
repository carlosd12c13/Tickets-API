namespace Tickets_API.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}