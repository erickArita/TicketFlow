using AutoMapper;
using TicketFlow.Core.Customer.Dtos;
using TicketFlow.Core.Prioridad.Dtos;
using TicketFlow.Entities;

namespace TicketFlow.Common.Maps;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        MapCustomer();
        MapPrioridad();
    }
    
    private void MapCustomer()
    {
        CreateMap<CreateCustomerRequest, Cliente>();
        CreateMap<Cliente, CustomerResponse>();
    }
    
    private void MapPrioridad()
    {
        CreateMap<CreatePrioridadRequest, Prioridad>();
        CreateMap<Prioridad, PrioridadResponse>();
    }
}