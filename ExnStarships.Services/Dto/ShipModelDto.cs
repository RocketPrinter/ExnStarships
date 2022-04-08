namespace ExnStarships.Services.Dto;

public class ShipModelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Manufacturer { get; set; }

    public double MaxFuel { get; set; }
    public int MaxCrewNr { get; set; }
    public double MaxCargoWeight { get; set; }
}