using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Crew
{
    // --- Info ---
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    
    // -- Location ---
    public int ShipId { get; set; }
    public Ship Ship { get; set; } = null!;

    // --- Extra info ---
    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }

    // --- Roles ---
    // lazy loaded
    public virtual ICollection<Role> Roles { get; set; } = null!;

    //public string? ImageUrl { get; set; }
}
