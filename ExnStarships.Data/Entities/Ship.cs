﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExnStarships.Data.Entities;

public class Ship
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public int ModelId { get; set; }
    
    public double Fuel { get; set; }
    public int CrewNr { get; set; }
    public double CargoWeight { get; set; }
    
    //public string? ImageUrl { get; set; }
}
