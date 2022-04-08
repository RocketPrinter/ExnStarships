namespace ExnStarships.Services.Dto;

public record ShipModelDto(
    int Id,
    string Name,
    string? Manufacturer,

    double MaxFuel,
    int MaxCrewNr,
    double MaxCargoWeight
    );
