namespace TicketFlow.Core.Estado.Dtos;

public record EstadoResponse
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; }
}