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
    
    public int CargoModelId { get; set; }
    public CargoModel CargoModel { get; set; } = null!;
    
    public int CargoHoldId { get; set; }
    // lazy loaded, since probably won't be used
    public virtual CargoHold CargoHold { get; set; } = null!;
    
    public int DestinationId { get; set; }
    // lazy loaded, since most of the time we'll just compare IDs
    public virtual Destination Destination { get; set; } = null!;
}
