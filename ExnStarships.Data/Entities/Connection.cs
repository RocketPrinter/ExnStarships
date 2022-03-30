namespace ExnStarships.Data.Entities;

public class Connection
{
    public int FirstDestinationId { get; set; }
    public int SecondDestinationId { get; set; }
    public double Length { get; set; }

    public Destination FirstDestination { get; set; } = null!;
    public Destination SecondDestination { get; set; } = null!;
}
