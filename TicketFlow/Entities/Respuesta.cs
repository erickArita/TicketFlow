using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicketFlow.Entities;

[Table("respuestas", Schema = "transacctional")]
public class Respuesta : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("ticket_id")]
    [Required]
    public Guid TicketId { get; set; }
    [Column("usuario_id")]
    [Required]
    public string UsuarioId { get; set; }
    [Column("respuesta")]
    [Required]
    public string Comentario { get; set; }
    [Column("respuesta_padre_id")]
    public Guid? RespuestaPadreId { get; set; }
    
    public virtual Ticket Ticket { get; set; }
    public virtual IdentityUser Usuario { get; set; }
    public virtual Respuesta? RespuestaPadre { get; set; }
    public virtual ICollection<Respuesta> RespuestasHijas { get; set; }
    public virtual ICollection<ArchivoRespuesta> ArchivoRespuestas { get; set; }
}