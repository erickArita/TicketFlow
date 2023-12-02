using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicketFlow.Entities;

[Table("tickets_history", Schema = "transacctional")]
public class TiketsHistory : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("descripcion")]
    [Required]
    public string Descripcion { get; set; }
    [Column("usuario_id")]
    [Required]
    public string UsuarioId { get; set; }
    [Column("ticket_id")]
    [Required]
    public string TiketId { get; set; }
    
    public virtual IdentityUser Usuario { get; set; }
    public virtual Ticket Ticket { get; set; }
}