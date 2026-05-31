namespace Tickets_API.Domain.Entities;

public class TicketComment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TicketId { get; set; }

    public Ticket? Ticket { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}