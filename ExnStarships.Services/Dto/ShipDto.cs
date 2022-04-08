using ExnStarships.Data.Entities;

namespace ExnStarships.Services.Dto;

public record ShipDto(
    int Id,
    string Name,
    string? Description
    );