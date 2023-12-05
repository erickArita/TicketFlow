using AutoMapper;
using TicketFlow.Core.Customer.Dtos;
using TicketFlow.Core.Estado.Dtos;
using TicketFlow.Core.Prioridad.Dtos;
using TicketFlow.Entities;

namespace TicketFlow.Common.Maps;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        MapCustomer();
        MapPrioridad();
        MapEstado();
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
    
    private void MapEstado()
    {
        CreateMap<CreateEstadoRequest, Estado>();
        CreateMap<Estado, EstadoResponse>();
    }
}