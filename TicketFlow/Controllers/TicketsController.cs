using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Respuestas;
using TicketFlow.Core.Ticket;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Entities.Enums;

namespace TicketFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IRespuestasService _respuestasService;

    public TicketsController(ITicketService ticketService, IRespuestasService respuestasService)
    {
        _ticketService = ticketService;
        _respuestasService = respuestasService;
    }

    /// <summary>
    ///     Get all tickets
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<AplicationResponse<IEnumerable<TicketResponse>>>> Get()
    {
        var tickets = await _ticketService.GetAllAsync();
        return Ok(new AplicationResponse<IEnumerable<TicketResponse>>
        {
            Data = tickets,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AplicationResponse<TicketWithResponses>>> Get(Guid id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);
        return Ok(new AplicationResponse<TicketWithResponses>
        {
            Data = ticket,
        });
    }

    /// <summary>
    ///   Crea un nuevo ticket con sus respectivos archivos, si los tiene, este metodo es solo para el rol de admin
    /// </summary>
    /// <param name="ticketCreationDto">
    ///    <see cref="CreateTicketRequest"/>
    /// </param>
    /// <param name="Archivos">
    ///   <see cref="IFormFile"/>
    /// </param>
    /// <returns>
    ///  <see cref="TicketResponse"/>
    /// </returns>
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> Post(
        CreateTicketRequest ticketCreationDto)
    {
        var ticket = await _ticketService.AddAsync(ticketCreationDto);
        return StatusCode(StatusCodes.Status201Created,
            new AplicationResponse<TicketResponse>
        {
            Data = ticket,
        });
    }

    /// <summary>
    /// Actualiza todos los campos de un ticket 
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="ticketUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{ticketId}")]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> Put(
        Guid ticketId,
        UpdateTicketRequest ticketUpdateDto)
    {
        var ticket = await _ticketService.UpdateAsync(ticketId, ticketUpdateDto);
        return Ok(new AplicationResponse<TicketResponse>
        {
            Data = ticket,
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> Delete(Guid id)
    {
        await _ticketService.DeleteAsync(id);
        return Ok(new AplicationResponse<TicketResponse>());
    }

    /// <summary>
    ///     Reemplaza al usuaruio encargado del ticket, solo para el rol de admin
    /// </summary>
    /// <param name="ticketId">
    /// Guid
    /// </param>
    /// <param name="userId">
    /// Guid
    /// </param>
    /// <returns>
    /// <see cref="TicketResponse"/>
    /// </returns>
    [HttpPost("{ticketId}/users/{userId}")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> AddUser2Ticket(Guid ticketId, Guid userId)
    {
        var ticket = await _ticketService.AddUser2TicketAsync(ticketId, userId);
        return Ok(new AplicationResponse<TicketResponse>
        {
            Data = ticket,
        });
    }

    /// <summary>
    /// Actualiza el estado del ticket
    /// </summary>
    [HttpPut("{ticketId}/status/{estadoId}")]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> UpdateStatus(Guid ticketId, Guid estadoId)
    {
        var ticket = await _ticketService.UpdateStatusAsync(ticketId, estadoId);
        return Ok(new AplicationResponse<TicketResponse>
        {
            Data = ticket,
        });
    }

    /// <summary>
    /// Actualiza la prioridad del ticket
    /// </summary>
    [HttpPut("{ticketId}/priority/{prioridadId}")]
    public async Task<ActionResult<AplicationResponse<TicketResponse>>> UpdatePriority(Guid ticketId, Guid prioridadId)
    {
        var ticket = await _ticketService.UpdatePriorityAsync(ticketId, prioridadId);
        return Ok(new AplicationResponse<TicketResponse>
        {
            Data = ticket,
        });
    }

    /// <summary>
    ///     Agrega una respuesta a un ticket
    /// </summary>
    [HttpPost("/responses")]
    public async Task<ActionResult<AplicationResponse<RespuestaResponse>>> AddResponse(CreateResponseRequest respuestaCreationDto)
    {
        var respuesta = await _respuestasService.AddResponseAsync(respuestaCreationDto);
        return StatusCode(StatusCodes.Status201Created,
            new AplicationResponse<RespuestaResponse>
        {
            Data = respuesta,
        });
    }

    /// <summary>
    ///     Actualiza una respuesta a un ticket
    /// </summary>
    [HttpPut("responses/{respuestaId}")]
    public async Task<ActionResult<AplicationResponse<RespuestaResponse>>> UpdateResponse(
        Guid respuestaId,
        UpdateResponseRequest respuestaUpdateDto)
    {
        var respuesta = await _respuestasService.UpdateResponseAsync(respuestaId, respuestaUpdateDto);
        return Ok(new AplicationResponse<RespuestaResponse>
        {
            Data = respuesta,
        });
    }
}