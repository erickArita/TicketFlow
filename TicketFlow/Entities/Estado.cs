using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("estados", Schema = "transacctional")]
public class Estado : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("descripcion")]
    [Required]
    [StringLength(30)]
    public string Descripcion { get; set; }
}