using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("archivos_adjuntos", Schema = "transacctional")]
public class ArchivoAdjunto : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("object_id")]
    [Required]
    public string ObjectId { get; set; }
    
    public virtual ICollection<ArchivoRespuesta> ArchivosRespuestas { get; set; }
    public virtual ICollection<ArchivoTicket> ArchivosTickets { get; set; }
}