using ExnStarships.Data.Entities;

namespace ExnStarships.Services.Dto;

public record CrewDto(
    int Id, 
    string FirstName,
    string LastName,

    int shipId,

    DateTime? DateOfBirth,
    string? Email,

    ICollection<Role> Roles
    );