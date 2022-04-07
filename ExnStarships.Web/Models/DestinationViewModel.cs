namespace ExnStarships.Web.Models;

public record DestinationViewModel(
    int Id,
    string Name,
    string? Description,
    bool IsFuelDepo
    );