using ExnStarships.Data.Entities;

namespace ExnStarships.Services.Dto;

public record RoleDto(
    int Id, 
    string Name, 
    string? Description
    );