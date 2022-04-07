using System.ComponentModel.DataAnnotations;

namespace ExnStarships.Data.Entities;

public class Connection
{
    public double Length { get; set; }
    
    public int FirstDestinationId { get; set; }
    //public Destination FirstDestination { get; set; } = null!;
    
    public int SecondDestinationId { get; set; }
    //public Destination SecondDestination { get; set; } = null!;
}