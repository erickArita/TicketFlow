using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("prioridades", Schema = "transacctional")]
public class Prioridad : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("descripcion")]
    [Required]
    [StringLength(30)]
    public string Descripcion { get; set; }
}