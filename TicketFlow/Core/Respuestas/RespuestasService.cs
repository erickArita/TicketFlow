using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Respuestas.Extensions;
using TicketFlow.Core.Ticket;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Core.TicketHistory;
using TicketFlow.Core.User;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Core.Respuestas;

public class RespuestasService : IRespuestasService
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ITicketService _ticketService;
    private readonly ITicketHistoryService _ticketHistoryService;
    private readonly ISigningService _signingService;

    public RespuestasService(IUserService userService,
        ApplicationDbContext dbContext,
        IMapper mapper,
        ITicketService ticketService,
        ITicketHistoryService ticketHistoryService,
        ISigningService signingService)
    {
        _userService = userService;
        _dbContext = dbContext;
        _mapper = mapper;
        _ticketService = ticketService;
        _ticketHistoryService = ticketHistoryService;
        _signingService = signingService;
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
            .Include(r => r.ArchivoRespuestas)
            .ThenInclude(r => r.ArchivoAdjunto)
            .FirstOrDefaultAsync(respuesta => respuesta.Id == respuestaId);

        if (respuesta is null) throw new NotFoundException($"Respuesta con id {respuestaId} no existe 😪");
        
        respuesta.Comentario = respuestaUpdateDto.Comentario;

        if (respuestaUpdateDto.FilesIds is not null)
        {
            respuesta.ArchivoRespuestas = respuestaUpdateDto.FilesIds.Select(id => new ArchivoRespuesta
            {
                ArchivoAdjuntoId = id,
                RespuestaId = respuesta.Id,
            }).ToList();
        }
        _dbContext.Respuestas.Update(respuesta);
        await _dbContext.SaveChangesAsync();
        
        var newResponse = await _dbContext.Respuestas
            .Include(r => r.Usuario)
            .Include(r => r.ArchivoRespuestas)
            .ThenInclude(r => r.ArchivoAdjunto)
            .FirstOrDefaultAsync(respuesta => respuesta.Id == respuestaId); 

        await _ticketHistoryService.AddTicketHistoryAsync($"Se actualizo una respuesta del ticket", respuesta.TicketId);
        var respuestaResponse = _mapper.Map<RespuestaResponse>(newResponse);
        await respuestaResponse.SignFiles(_signingService);
        return respuestaResponse;
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
        await respuestaResponse.SignFiles(_signingService);
        respuestaResponse.modificado = respuestaResponse.FechaModificacion.HasValue;

        return respuestaResponse;
    }

    private async Task<RespuestaResponse> AsignarRespuestaATicket(CreateResponseRequest respuestaCreationDto)
    {
        var ticket = await _ticketService.GetByIdAsync(respuestaCreationDto.TicketId);

        var user = await _userService.GetUserInSessionAsync();

        if (ticket is null) throw new NotFoundException($"Ticket con id {ticket.Id} no existe 😪");
        
        var respuesta = new Respuesta();
        respuesta.Id = Guid.NewGuid();
        await ValidarExistenArchivos(respuestaCreationDto.FilesIds);
        respuesta = _mapper.Map<Respuesta>(respuestaCreationDto);
        
        respuesta.UsuarioId = user.Id;
        respuesta.TicketId = ticket.Id;

        var entity = await _dbContext.Respuestas.AddAsync(respuesta);
        await _dbContext.SaveChangesAsync();

        await _ticketHistoryService.AddTicketHistoryAsync($"Se agrego una respuesta al ticket", ticket.Id);

        var respuestaResponse = _mapper.Map<RespuestaResponse>(entity.Entity);
        await respuestaResponse.SignFiles(_signingService);
        respuestaResponse.modificado = respuestaResponse.FechaModificacion.HasValue;

        return respuestaResponse;
    }

    private async Task ValidarExistenArchivos(List<Guid>? filesIds)
    {
        if (filesIds is null) return;

        var archivos = await _dbContext.ArchivosAdjuntos.Where(archivo => filesIds.Contains(archivo.Id)).ToListAsync();

        if (archivos.Count != filesIds.Count)
        {
            throw new NotFoundException($"Uno o varios archivos no existen 😪");
        }
    }
}