using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Destination
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    public bool IsFuelDepo { get; set; }

    public int CargoHoldId { get; set; }
    // lazy loaded to prevent loading large lists
    public virtual CargoHold CargoHold { get; set; } = null!;
    public virtual ICollection<Connection> Connections { get; set; } = null!;
    
    //public string? ImageUrl { get; set; }
}
