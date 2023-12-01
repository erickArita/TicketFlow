using AutoMapper;
using TicketFlow.Core.Customer.Dtos;
using TicketFlow.Entities;

namespace TicketFlow.Common.Maps;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        MapCustomer();
    }
    
    private void MapCustomer()
    {
        CreateMap<CreateCustomerRequest, Cliente>();
        CreateMap<Cliente, CustomerResponse>();
    }
}