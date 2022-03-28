using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class CargoModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string Serial { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }

    //public string? ImageUrl { get; set; }
}
