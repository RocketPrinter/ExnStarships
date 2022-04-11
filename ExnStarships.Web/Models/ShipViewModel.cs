using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExnStarships.Web.Models;

public class ShipViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ModelId { get; set; }
    public IEnumerable<SelectListItem>? Models { get; set; }

    public double Fuel { get; set; }
    public Ship.ShipState State { get; set; } = Ship.ShipState.Docked;

    public double CargoWeight { get; set; }
    public int CargoHoldId { get; set; }


    // will be null if currently not docked
    public int DestinationId { get; set; }
    public IEnumerable<SelectListItem>? Destinations { get; set; }
    // for displaying in Index and Details
    public string? DestinationName { get; set; }

    public string? ModelName { get; set; }
}
