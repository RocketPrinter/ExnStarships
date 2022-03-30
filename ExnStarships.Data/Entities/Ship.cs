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
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int ModelId { get; set; }

    public ShipState State { get; set; } = ShipState.Docked;
    // can be null
    public int? DestionationId { get; set; }
    
    public double Fuel { get; set; }
    public int CrewNr { get; set; }
    public double CargoWeight { get; set; }

    public Destination? Destination { get; set; }

    public List<Crew> Crews { get; set; } = null!;

    //public string? ImageUrl { get; set; }
}
