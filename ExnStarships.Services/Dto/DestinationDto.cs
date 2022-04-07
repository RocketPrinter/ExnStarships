namespace ExnStarships.Services.Dto;

public record DestinationDto(
    int Id,
    string Name,
    string? Description,
    bool IsFuelDepo
    );