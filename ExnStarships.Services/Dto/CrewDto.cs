namespace ExnStarships.Services.Dto;

public record CrewDto(
    int Id, 
    string FirstName,
    string LastName,

    Ship ship,

    DateTime? DateOfBirth;
    string? Email,

    ICollection<Role> Roles
    );