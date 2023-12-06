using AutoMapper;
using TicketFlow.Controllers;
using TicketFlow.Core.Customer.Dtos;
using TicketFlow.Core.Estado.Dtos;
using TicketFlow.Core.Prioridad.Dtos;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Entities;

namespace TicketFlow.Common.Maps;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        MapCustomer();
        MapPrioridad();
        MapEstado();
        MapTicket();
        ResponseMaps();
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

    private void MapTicket()
    {
        CreateMap<CreateTicketRequest, Ticket>()
            .ForMember(ticket => ticket.UsuarioId,
                opt => opt.MapFrom(ticketCreationDto => ticketCreationDto.UsuarioAsignadoId))
            .ForMember(opt => opt.ArchivosTickets,
                opt => opt.MapFrom(ticketCreationDto => ticketCreationDto.ArchivosIds.Select(id => new ArchivoTicket
                {
                    ArchivoAdjuntoId = id
                })));
        CreateMap<Ticket, TicketResponse>()
            .ForMember(ticketResponse => ticketResponse.ClienteNombre,
                opt => opt.MapFrom(ticket => ticket.Cliente.Nombre))
            .ForMember(ticketResponse => ticketResponse.PrioridadDescripcion,
                opt => opt.MapFrom(ticket => ticket.Prioridad.Descripcion))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoNombre,
                opt => opt.MapFrom(ticket => ticket.Usuario.UserName))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoId,
                opt => opt.MapFrom(ticket => ticket.Usuario.Id))
            .ForMember(ticketResponse => ticketResponse.EstadoDescripcion,
                opt => opt.MapFrom(ticket => ticket.Estado.Descripcion));


        CreateMap<UpdateTicketRequest, Ticket>();
        CreateMap<Ticket, TicketWithResponses>()
            .ForMember(des => des.Respuestas, opt => opt.MapFrom(c => c.Respuestas));

        CreateMap<Ticket, TicketWithResponses>()
            .ForMember(ticketResponse => ticketResponse.ClienteNombre,
                opt => opt.MapFrom(ticket => ticket.Cliente.Nombre))
            .ForMember(ticketResponse => ticketResponse.PrioridadDescripcion,
                opt => opt.MapFrom(ticket => ticket.Prioridad.Descripcion))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoNombre,
                opt => opt.MapFrom(ticket => ticket.Usuario.UserName))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoId,
                opt => opt.MapFrom(ticket => ticket.Usuario.Id))
            .ForMember(ticketResponse => ticketResponse.EstadoDescripcion,
                opt => opt.MapFrom(ticket => ticket.Estado.Descripcion))
            .ForMember(ticketResponse => ticketResponse.Respuestas,
                opt => opt.MapFrom(ticket => ticket.Respuestas));
    }

    private void ResponseMaps()
    {
        CreateMap<CreateResponseRequest, Respuesta>();
        CreateMap<Respuesta, RespuestaResponse>()
            .ForMember(respuestaResponse => respuestaResponse.UsuarioNombre,
                opt => opt.MapFrom(respuesta => respuesta.Usuario.UserName));

        CreateMap<Cliente, CustomerResponse>();
        CreateMap<Prioridad, PrioridadResponse>();
        CreateMap<Estado, EstadoResponse>();
        CreateMap<Ticket, TicketResponse>()
            .ForMember(ticketResponse => ticketResponse.ClienteNombre,
                opt => opt.MapFrom(ticket => ticket.Cliente.Nombre))
            .ForMember(ticketResponse => ticketResponse.PrioridadDescripcion,
                opt => opt.MapFrom(ticket => ticket.Prioridad.Descripcion))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoNombre,
                opt => opt.MapFrom(ticket => ticket.Usuario.UserName))
            .ForMember(ticketResponse => ticketResponse.UsuarioAsignadoId,
                opt => opt.MapFrom(ticket => ticket.Usuario.Id))
            .ForMember(ticketResponse => ticketResponse.EstadoDescripcion,
                opt => opt.MapFrom(ticket => ticket.Estado.Descripcion));
    }
}