using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class CargoModel
{
    public enum CargoType
    {
        None,
        Bulk,
        Parts,
        Equipment,
        Biological,
        Chemical,
        Radioactive,
        Clasified
    }
    
    // --- Info ---
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    // --- Extra info ---
    [MaxLength(50)]
    public string? Serial { get; set; } = null!;
    [MaxLength(500)]
    public string? Description { get; set; }
    public bool IsFragile { get; set; }
    public CargoType Type { get; set; }

    // lazy loaded, since won't be used much
    public virtual ICollection<Cargo> Cargos { get; set; } = null!;
    
    //public string? ImageUrl { get; set; }
}
