namespace TicketFlow.Core.Prioridad.Dtos;

public record PrioridadResponse
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; }
}