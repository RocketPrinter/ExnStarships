using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

/// <summary>
/// Instance of cargo
/// </summary>
public class Cargo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public double Quantity { get; set; }
    public int CargoHoldId { get; set; }
    public int CargoModelId { get; set; }
    public int DestinationId { get; set; }

    public CargoHold CargoHold { get; set; } = null!;
    public CargoModel CargoModel { get; set; } = null!;
    public Destination Destination { get; set; } = null!;
}
