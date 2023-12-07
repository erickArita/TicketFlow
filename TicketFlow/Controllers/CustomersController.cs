using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Customer;
using TicketFlow.Core.Customer.Dtos;

namespace TicketFlow.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<AplicationResponse<IReadOnlyCollection<CustomerResponse>>>> Get()
    {
        var customers = await _customerService.GetAllAsync();

        return Ok(
            new AplicationResponse<ICollection<CustomerResponse>>
            {
                Data = customers.ToList()
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AplicationResponse<CustomerResponse>>> Get(Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);

        return Ok(
            new AplicationResponse<CustomerResponse>
            {
                Data = customer
            }
        );
    }

    [HttpPost]
    public async Task<ActionResult<AplicationResponse<CustomerResponse>>> Post(CreateCustomerRequest createCustomer)
    {
        var customerResponse = await _customerService.AddAsync(createCustomer);

        return StatusCode(StatusCodes.Status201Created,
            new AplicationResponse<CustomerResponse>
            {
                Data = customerResponse
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AplicationResponse<string>>> Put(Guid id, UpdateCustomerRequest createCustomer)
    {
        await _customerService.UpdateAsync(id, createCustomer);

        return Ok(
            new AplicationResponse<string>
            {
                Message = "Cliente actualizado correctamente 🤩🤩"
            }
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _customerService.DeleteAsync(id);

        return Ok(
            new AplicationResponse<string>
            {
                Message = "Cliente eliminado correctamente 🤩🤩"
            }
        );
    }
}