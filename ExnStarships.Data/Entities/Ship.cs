using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Ship
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public ShipModel Model { get; set; } = null!;
    
    public double Fuel { get; set; }
    public int CrewNr { get; set; }
    public int PassengersNr { get; set; }
    public double CargoQuantity { get; set; }

    //public string? ImageUrl { get; set; }
}
