using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Ship
{
    public enum ShipState
    {
        Docked,
        Disabled,
        Transit
    }

    // --- Info ---
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int ModelId { get; set; }
    public ShipModel Model { get; set; } = null!;

    // --- Navigation ---
    public double Fuel { get; set; }
    public ShipState State { get; set; } = ShipState.Docked;

    // will be null if currently not docked
    public int? DestinationId { get; set; }
    public Destination? Destination { get; set; }

    // --- Crew ---
    public List<Crew> Crews { get; set; } = null!;

    // --- Cargo ---
    public double CargoWeight { get; set; }
    public int CargoHoldId { get; set; }
    // lazy loaded to prevent loading large lists
    public virtual CargoHold CargoHold { get; set; } = null!;


    //public string? ImageUrl { get; set; }
}
