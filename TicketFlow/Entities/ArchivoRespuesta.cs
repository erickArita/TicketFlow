using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("archivos_respuestas", Schema = "transacctional")]
public class ArchivoRespuesta : AggregateRoot
{
    [Column("respuesta_id")]
    [Required]
    public Guid RespuestaId { get; set; }
    [Column("archivo_adjunto_id")]
    [Required]
    public Guid ArchivoAdjuntoId { get; set; }
    
    public virtual ArchivoAdjunto ArchivoAdjunto { get; set; }
    public virtual Respuesta Respuesta { get; set; }
    
}