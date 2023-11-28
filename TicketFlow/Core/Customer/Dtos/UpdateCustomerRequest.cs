namespace TicketFlow.Core.Customer.Dtos;

public record UpdateCustomerRequest : CreateCustomerRequest
{
    public Guid Id { get; set; }
}