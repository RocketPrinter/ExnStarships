using System.ComponentModel.DataAnnotations;

namespace ExnStarships.Web.Models;

// boy that's a horible name
public record ShipModelViewModel(
    int Id,
    [MaxLength(50)]
    string Name,
    [MaxLength(50)]
    string Manufacturer,

    double MaxFuel,
    int MaxCrewNr,
    double MaxCargoWeight
    );