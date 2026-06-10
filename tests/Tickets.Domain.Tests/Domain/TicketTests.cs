using Tickets.Domain.Entities;
using Tickets.Domain.Enums;
using Tickets.Domain.Exceptions;

namespace Tickets.Domain.Tests.Domain;

public class TicketTests
{
    [Fact]
    public void AssignTo_WhenTicketIsOpen_ShouldAssignAgentAndChangeStatusToInProgress()
    {
        // Arrange
        var ticket = new Ticket
        {
            Title = "Test ticket",
            Description = "Test description",
            CreatedByUserId = Guid.NewGuid()
        };

        var agentId = Guid.NewGuid();

        // Act
        ticket.AssignTo(agentId);

        // Assert
        Assert.Equal(agentId, ticket.AssignedToUserId);
        Assert.Equal(TicketStatus.InProgress, ticket.Status);
    }

    [Fact]
    public void ChangeStatus_WhenTicketIsClosed_ShouldThrowDomainException()
    {
        // Arrange
        var ticket = new Ticket
        {
            Title = "Test ticket",
            Description = "Test description",
            CreatedByUserId = Guid.NewGuid()
        };

        ticket.ChangeStatus(TicketStatus.Closed);

        // Act
        var action = () => ticket.ChangeStatus(TicketStatus.InProgress);

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void AddComment_WhenTicketIsClosed_ShouldThrowDomainException()
    {
        // Arrange
        var ticket = new Ticket
        {
            Title = "Test ticket",
            Description = "Test description",
            CreatedByUserId = Guid.NewGuid()
        };

        ticket.ChangeStatus(TicketStatus.Closed);

        // Act
        var action = () => ticket.AddComment(Guid.NewGuid(), "Any comment");

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void AddComment_WhenMessageIsEmpty_ShouldThrowDomainException()
    {
        // Arrange
        var ticket = new Ticket
        {
            Title = "Test ticket",
            Description = "Test description",
            CreatedByUserId = Guid.NewGuid()
        };

        // Act
        var action = () => ticket.AddComment(Guid.NewGuid(), "");

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void AddComment_WhenTicketIsOpen_ShouldAddComment()
    {
        // Arrange
        var ticket = new Ticket
        {
            Title = "Test ticket",
            Description = "Test description",
            CreatedByUserId = Guid.NewGuid()
        };

        var userId = Guid.NewGuid();

        // Act
        ticket.AddComment(userId, "This is a comment");

        // Assert
        Assert.Single(ticket.Comments);
        Assert.Equal(userId, ticket.Comments.First().UserId);
        Assert.Equal("This is a comment", ticket.Comments.First().Message);
    }
}
