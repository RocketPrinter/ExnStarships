using System.ComponentModel.DataAnnotations;

namespace ExnStarships.Web.Models;

public record DestinationViewModel(
    int Id,
    [MaxLength(50)]
    string Name,
    [MaxLength(500)]
    string? Description,
    bool IsFuelDepo
    );