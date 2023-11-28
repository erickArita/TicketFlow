using TicketFlow.Core.Customer.Dtos;

namespace TicketFlow.Core.Customer;

public interface ICustomerService
{
    Task<CustomerResponse> AddAsync(CreateCustomerRequest createCustomerRequest);

    Task<CustomerResponse> GetByIdAsync(Guid id);

    Task<IReadOnlyCollection<CustomerResponse>> GetAllAsync();

    Task UpdateAsync(UpdateCustomerRequest updateCustomerRequest);

    Task DeleteAsync(Guid id);
}