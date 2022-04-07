using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ExnStarships.Web.Models;

public record RoleViewModel(
    int Id,
    [MaxLength(50)]
    string Name,
    [MaxLength(500)]
    string? Description
    );