using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Cargo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int CargoModelId { get; set; }
    public int DestinationId { get; set; } 
}
