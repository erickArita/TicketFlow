using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Customer.Dtos;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.Core.Customer;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerResponse> AddAsync(CreateCustomerRequest createCustomerRequest)
    {
        var customerEntity = _mapper.Map<Cliente>(createCustomerRequest);

        customerEntity.Id = Guid.NewGuid();

        _context.Clientes.Add(customerEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponse>(customerEntity);
    }

    public async Task<CustomerResponse> GetByIdAsync(Guid id)
    {
        var customerEntity = await _context.Clientes.FindAsync(id);

        if (customerEntity == null)
        {
            throw new TicketFlowException("El cliente no existe 😡😡");
        }

        return _mapper.Map<CustomerResponse>(customerEntity);
    }

    public async Task<IReadOnlyCollection<CustomerResponse>> GetAllAsync()
    {
        var customers = await _context.Clientes.ToListAsync();
        return _mapper.Map<IReadOnlyCollection<CustomerResponse>>(customers);
    }

    public async Task UpdateAsync(Guid id, UpdateCustomerRequest updateCustomerRequest)
    {
        var customerEntity = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

        if (customerEntity == null)
        {
            throw new TicketFlowException("El cliente no existe 😡😡");
        }

        _mapper.Map(updateCustomerRequest, customerEntity);

        await _context.SaveChangesAsync();
        
    }

    public Task DeleteAsync(Guid id)
    {
        var customerEntity = _context.Clientes.Find(id);

        if (customerEntity == null)
        {
            throw new TicketFlowException("El cliente no existe 😡😡");
        }

        _context.Clientes.Remove(customerEntity);

        return _context.SaveChangesAsync();
    }
}