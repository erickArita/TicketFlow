using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using TicketFlow.Core.Estado.Dtos;

namespace TicketFlow.Entities;

[Table("tickets", Schema = "transacctional")]
public class Ticket : AggregateRoot
{
    [Column("id")] public Guid Id { get; set; }

    [Column("titulo")]
    [Required]
    [StringLength(120)]
    public string Asunto { get; set; }

    [Column("descripcion")]
    [StringLength(500)]
    public string Descripcion { get; set; }

    [Column("cliente_id")] [Required] public Guid ClienteId { get; set; }
    [Column("estado_id")] [Required] public Guid EstadoId { get; set; }
    [Column("prioridad_id")] [Required] public Guid PrioridadId { get; set; }
    [Column("usuario_id")] [Required] public string UsuarioId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Column("fecha_vencimiento")]
    public DateTime FechaVencimiento { get; set; }

    public virtual Estado Estado { get; set; }
    public virtual Prioridad Prioridad { get; set; }
    public virtual Cliente Cliente { get; set; }
    public virtual IdentityUser Usuario { get; set; }
    public virtual ICollection<Respuesta> Respuestas { get; set; }
    public virtual ICollection<ArchivoTicket> ArchivosTickets { get; set; }

    public virtual ICollection<TiketsHistory> TiketsHistories { get; set; }
}