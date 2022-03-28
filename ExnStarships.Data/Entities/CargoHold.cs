using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

/// <summary>
/// Acts like an "interface" for storing cargo.
/// Ships and destinations have cargo holds.
/// </summary>
public class CargoHold
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
