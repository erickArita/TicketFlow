﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Estado;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Core.Ticket.Extensions;
using TicketFlow.Core.User;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;
using TicketFlow.Entities.Enums;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Core.Ticket;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IEstadoService _estadoService;
    private readonly ISigningService _signingService;


    public TicketService(
        ApplicationDbContext dbContext,
        IMapper mapper,
        IUserService userService,
        IEstadoService estadoService,
        ISigningService signingService
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userService = userService;
        _estadoService = estadoService;
        _signingService = signingService;
    }

    public async Task<IReadOnlyCollection<TicketResponse>> GetAllAsync()
    {
        var tickets = await _dbContext.Tickets
            .Include(ticket => ticket.Cliente)
            .Include(ticket => ticket.Prioridad)
            .Include(ticket => ticket.Usuario)
            .Include(ticket => ticket.Estado)
            .Include(p => p.ArchivosTickets)
            .ThenInclude(p => p.ArchivoAdjunto)
            .ToListAsync();

        var ticketResponses = _mapper.Map<IReadOnlyCollection<TicketResponse>>(tickets);
        foreach (var ticketResponse in ticketResponses)
        {
            await ticketResponse.SignFiles(_signingService);
        }

        return ticketResponses;
    }

    public async Task<TicketWithResponses> GetByIdAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets
            .Include(c => c.Respuestas)
            .Include(ticket => ticket.Cliente)
            .Include(ticket => ticket.Prioridad)
            .Include(ticket => ticket.Usuario)
            .Include(ticket => ticket.Estado)
            .Include(p => p.ArchivosTickets)
            .ThenInclude(p => p.ArchivoAdjunto)
            .FirstOrDefaultAsync(ticket => ticket.Id == id);

        if (ticket is null)
        {
            throw new NotFoundException($"Ticket con id {id} no existe  😪");
        }

        var ticketResponse = _mapper.Map<TicketWithResponses>(ticket);

        // Cargar respuestas planas sin relaciones anidadas
        var respuestasDb = await _dbContext.Respuestas
            .Where(r => r.TicketId == id)
            .ToListAsync();

        // Organizar respuestas en una estructura jerárquica en memoria
        ticketResponse.Respuestas = BuildRespuestasHierarchy(respuestasDb, null);
        await ticketResponse.SignFiles(_signingService);

        return ticketResponse;
    }

    private List<RespuestaResponse> BuildRespuestasHierarchy(List<Respuesta> respuestasDb, Guid? respuestaPadreId)
    {
        var respuestasDto = _mapper.Map<List<RespuestaResponse>>(respuestasDb
            .Where(r => r.RespuestaPadreId == respuestaPadreId)
            .ToList());

        foreach (var respuestaDto in respuestasDto)
        {
            respuestaDto.RespuestasHijas = BuildRespuestasHierarchy(respuestasDb, respuestaDto.Id);
        }

        return respuestasDto;
    }


    public async Task<TicketResponse> AddAsync(CreateTicketRequest ticketCreationDto)
    {
        var ticket = _mapper.Map<Entities.Ticket>(ticketCreationDto);
        ticket.Id = Guid.NewGuid();
        if (ticket.ArchivosTickets.Any())
        {
            ticket.ArchivosTickets = ticket.ArchivosTickets.Select(archivoTicket => new ArchivoTicket
            {
                ArchivoAdjuntoId = archivoTicket.ArchivoAdjuntoId,
                TicketId = ticket.Id
            }).ToList();
        }

        // Validamos las relaciones con otras tablas
        await ticket.ValidateAsync(_dbContext);

        ticket.UsuarioId = (await _userService.GetUserInSessionAsync()).Id;

        //El estado por defecto es pendiente
        var estado = await _estadoService.GetByNameAsync(Estados.Pendiente);
        ticket.EstadoId = estado.Id;

        _dbContext.Tickets.Add(ticket);

        await _dbContext.SaveChangesAsync();

        var newTicket = await _dbContext.Tickets
            .Include(p => p.Cliente)
            .Include(p => p.Prioridad)
            .Include(p => p.ArchivosTickets)
            .ThenInclude(p => p.ArchivoAdjunto)
            .FirstOrDefaultAsync(t => t.Id == ticket.Id);

        var ticketResponse = _mapper.Map<TicketResponse>(newTicket);

        await ticketResponse.SignFiles(_signingService);

        return ticketResponse;
    }


    public async Task<TicketResponse> UpdateAsync(Guid id, UpdateTicketRequest ticketUpdateDto)
    {
        var ticket = await _dbContext.Tickets
            .Include(ticket => ticket.Cliente)
            .Include(ticket => ticket.Prioridad)
            .Include(ticket => ticket.Usuario)
            .Include(ticket => ticket.Estado)
            .Include(p => p.ArchivosTickets)
            .ThenInclude(p => p.ArchivoAdjunto)
            .FirstOrDefaultAsync(ticket => ticket.Id == id);

        if (ticket is null) throw new NotFoundException($"Ticket con id {id} no existe 😪");
        //validamos las mismas relaciones de la creacion y una mas que es el estado
        await ticket.ValidateUpdateAsync(_dbContext);

        _mapper.Map(ticketUpdateDto, ticket);

        await _dbContext.SaveChangesAsync();

        var updatedTicket = _mapper.Map<TicketResponse>(ticket);

        await updatedTicket.SignFiles(_signingService);

        return updatedTicket;
    }

    public async Task DeleteAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == id);

        if (ticket is null)
        {
            throw new NotFoundException($"Ticket con id {id} no existe 😪");
        }

        _dbContext.Tickets.Remove(ticket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TicketResponse> AddUser2TicketAsync(Guid ticketId, Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId.ToString());
        var ticket = await _dbContext.Tickets.Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(u => u.Estado)
            .FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        if (ticket.UsuarioId == userId.ToString())
            throw new TicketFlowException(
                $"Usuario {user.UserName} ya esta asignado al ticket {ticket.Asunto} debe seleccionar otro usuario 😡 ");
        if (user is null) throw new NotFoundException($"Usuario con id {userId} no existe ??");

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticketId} no existe ??");

        ticket.UsuarioId = user.Id;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TicketResponse>(ticket);
    }

    public async Task<TicketResponse> UpdateStatusAsync(Guid ticketId, Guid estadoId)
    {
        var ticket = await _dbContext.Tickets
            .Include(c => c.Cliente)
            .Include(p => p.Prioridad)
            .Include(u => u.Usuario)
            .Include(u => u.Estado)
            .Include(p => p.ArchivosTickets)
            .ThenInclude(p => p.ArchivoAdjunto)
            .FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        var estado = await _dbContext.Estados.FirstOrDefaultAsync(estado => estado.Id == estadoId);

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticketId} no existe ??");

        if (estado is null) throw new NotFoundException($"Estado con id {estadoId} no existe ??");
        if (ticket.EstadoId == estado.Id)
            throw new TicketFlowException(
                $"Estado {estado.Descripcion} ya esta asignado al ticket {ticket.Asunto} debe seleccionar otro estado 😅 ");

        ticket.EstadoId = estado.Id;
        await _dbContext.SaveChangesAsync();

        var updatedTicket = _mapper.Map<TicketResponse>(ticket);

        await updatedTicket.SignFiles(_signingService);

        return updatedTicket;
    }

    public async Task<TicketResponse> UpdatePriorityAsync(Guid ticketId, Guid prioridadId)
    {
        var ticket = await _dbContext.Tickets
            .Include(c => c.Cliente)
            .Include(p => p.Estado)
            .Include(u => u.Usuario).FirstOrDefaultAsync(ticket => ticket.Id == ticketId);
        var prioridad = await _dbContext.Prioridades.FirstOrDefaultAsync(prioridad => prioridad.Id == prioridadId);

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticketId} no existe ??");

        if (prioridad is null) throw new NotFoundException($"Prioridad con id {prioridadId} no existe ??");

        if (ticket.PrioridadId == prioridad.Id)
            throw new TicketFlowException(
                $"Prioridad {prioridad.Descripcion} ya esta asignado al ticket {ticket.Asunto} debe seleccionar otra prioridad 😅 ");

        ticket.PrioridadId = prioridad.Id;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TicketResponse>(ticket);
    }

    public async Task<RespuestaResponse> AddResponseAsync(Guid ticketId, CreateResponseRequest respuestaCreationDto)
    {
        var ticket = await GetByIdAsync(ticketId);
        var user = await _userService.GetUserInSessionAsync();

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticketId} no existe 😪");

        var respuesta = _mapper.Map<Respuesta>(respuestaCreationDto);

        respuesta.Id = Guid.NewGuid();
        respuesta.UsuarioId = user.Id;
        respuesta.TicketId = ticket.Id;

        var entity = await _dbContext.Respuestas.AddAsync(respuesta);
        await _dbContext.SaveChangesAsync();

        var respuestaResponse = _mapper.Map<RespuestaResponse>(entity.Entity);

        respuestaResponse.modificado = respuestaResponse.FechaModificacion.HasValue;

        return respuestaResponse;
    }

    public async Task<RespuestaResponse> UpdateResponseAsync(Guid respuestaId, UpdateResponseRequest respuestaUpdateDto)
    {
        var respuesta = await _dbContext.Respuestas
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(respuesta => respuesta.Id == respuestaId);

        if (respuesta is null) throw new NotFoundException($"Respuesta con id {respuestaId} no existe 😪");

        respuesta.Comentario = respuestaUpdateDto.Comentario;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<RespuestaResponse>(respuesta);
    }
}