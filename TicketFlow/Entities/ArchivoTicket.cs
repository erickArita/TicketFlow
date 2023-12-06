using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("archivos_tickets", Schema = "transacctional")]
public class ArchivoTicket : AggregateRoot
{
    [Column("ticket_id")] [Required] public Guid TicketId { get; set; }

    [Column("archivo_adjunto_id")]
    [Required]
    public Guid ArchivoAdjuntoId { get; set; } = Guid.NewGuid();

    public virtual ArchivoAdjunto ArchivoAdjunto { get; set; }
    public virtual Ticket Ticket { get; set; }
}