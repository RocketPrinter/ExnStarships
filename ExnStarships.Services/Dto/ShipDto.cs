using ExnStarships.Data.Entities;

namespace ExnStarships.Services.Dto;

public class ShipDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ModelId { get; set; }
    public ShipModelDto? Model { get; set; }

    public double Fuel { get; set; }
    public Ship.ShipState State { get; set; } = Ship.ShipState.Docked;
    // will be null if currently not docked
    public int? DestionationId { get; set; }
    public DestinationDto? Destination { get; set; }

    public double CargoWeight { get; set; }
    public int CargoHoldId { get; set; }
}