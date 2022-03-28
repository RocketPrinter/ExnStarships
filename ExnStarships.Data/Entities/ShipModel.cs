using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class ShipModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(50)]
    public string? Manufacturer { get; set; }

    public double MaxFuel { get; set; }
    public int MaxCrewNr { get; set; }
    public double MaxCargoWeight { get; set; }
}
