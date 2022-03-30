using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Crew
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    public int ShipId { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }

    public Ship Ship { get; set; } = null!;

    public IEnumerable<Role> Roles = null!;

    //public string? ImageUrl { get; set; }
}
