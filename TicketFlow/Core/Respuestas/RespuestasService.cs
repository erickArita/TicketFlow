using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Ticket;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Core.TicketHistory;
using TicketFlow.Core.User;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.Core.Respuestas;

public class RespuestasService : IRespuestasService
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ITicketService _ticketService;
    private readonly ITicketHistoryService _ticketHistoryService;

    public RespuestasService(IUserService userService,
        ApplicationDbContext dbContext,
        IMapper mapper,
        ITicketService ticketService,
        ITicketHistoryService ticketHistoryService)
    {
        _userService = userService;
        _dbContext = dbContext;
        _mapper = mapper;
        _ticketService = ticketService;
        _ticketHistoryService = ticketHistoryService;
    }

    public async Task<RespuestaResponse> AddResponseAsync(CreateResponseRequest respuestaCreationDto)
    {
        if (respuestaCreationDto.TicketId is null && respuestaCreationDto.RespuestaPadreId is null)
        {
            throw new TicketFlowException("La respusesta debe de tener un ticket o ser hija de una respuesta 😡😡");
        }

        if (respuestaCreationDto.TicketId is null)
        {
            return await AsignarRespuestaHija(respuestaCreationDto);
        }
        
        return await AsignarRespuestaATicket(respuestaCreationDto);
    }

    public async Task<RespuestaResponse> UpdateResponseAsync(Guid respuestaId, UpdateResponseRequest respuestaUpdateDto)
    {
        var respuesta = await _dbContext.Respuestas
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(respuesta => respuesta.Id == respuestaId);

        if (respuesta is null) throw new NotFoundException($"Respuesta con id {respuestaId} no existe 😪");

        respuesta.Comentario = respuestaUpdateDto.Comentario;
        await _dbContext.SaveChangesAsync();

        await _ticketHistoryService.AddTicketHistoryAsync($"Se actualizo una respuesta del ticket", respuesta.TicketId);

        return _mapper.Map<RespuestaResponse>(respuesta);
    }

    private async Task<RespuestaResponse> AsignarRespuestaHija(CreateResponseRequest respuestaCreationDto)
    {
        var respuestaPadre = await _dbContext.Respuestas
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(respuesta => respuesta.Id == respuestaCreationDto.RespuestaPadreId);

        if (respuestaPadre is null)
            throw new NotFoundException($"Respuesta con id {respuestaCreationDto.RespuestaPadreId} no existe 😪");

        var user = await _userService.GetUserInSessionAsync();

        var respuesta = _mapper.Map<Respuesta>(respuestaCreationDto);

        respuesta.Id = Guid.NewGuid();
        respuesta.UsuarioId = user.Id;
        respuesta.TicketId = respuestaPadre.TicketId;

        var entity = await _dbContext.Respuestas.AddAsync(respuesta);
        await _dbContext.SaveChangesAsync();

        await _ticketHistoryService.AddTicketHistoryAsync($"Se agrego una respuesta al ticket", respuesta.TicketId);

        var respuestaResponse = _mapper.Map<RespuestaResponse>(entity.Entity);

        respuestaResponse.modificado = respuestaResponse.FechaModificacion.HasValue;

        return respuestaResponse;
    }

    private async Task<RespuestaResponse> AsignarRespuestaATicket(CreateResponseRequest respuestaCreationDto)
    {
        var ticket = await _ticketService.GetByIdAsync(respuestaCreationDto.TicketId);

        var user = await _userService.GetUserInSessionAsync();

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticket.Id} no existe 😪");

        var respuesta = _mapper.Map<Respuesta>(respuestaCreationDto);

        respuesta.Id = Guid.NewGuid();
        respuesta.UsuarioId = user.Id;
        respuesta.TicketId = ticket.Id;

        var entity = await _dbContext.Respuestas.AddAsync(respuesta);
        await _dbContext.SaveChangesAsync();

        await _ticketHistoryService.AddTicketHistoryAsync($"Se agrego una respuesta al ticket", ticket.Id);

        var respuestaResponse = _mapper.Map<RespuestaResponse>(entity.Entity);

        respuestaResponse.modificado = respuestaResponse.FechaModificacion.HasValue;

        return respuestaResponse;
    }
}