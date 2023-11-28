namespace TicketFlow.Entities;

public interface IAggregateRoot
{
    public DateTime FechaCreacion { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public string ActualizadoPor { get; set; }
}