using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketFlow.Entities;

[Table("clientes", Schema = "transacctional")]
public class Cliente : AggregateRoot
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("nombre")]
    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }
    [Column("apellido")]
    [Required]
    [StringLength(70)]
    public string Apellido { get; set; }
    [Column("correo")]
    [Required]
    [StringLength(70)]
    public string Correo { get; set; }
    [Column("telefono")]
    [Required]
    [StringLength(20)]
    public string Telefono { get; set; }
}