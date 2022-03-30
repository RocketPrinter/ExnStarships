using ExnStarships.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ExnStarships.Data;

public class ExnStarshipContext : DbContext
{
    public DbSet<Cargo> Cargos { get; set; } = null!;
    public DbSet<CargoHold> CargoHolds { get; set; } = null!;
    public DbSet<CargoModel> CargoModels { get; set; } = null!;
    public DbSet<Connection> Connections { get; set; } = null!;
    public DbSet<Crew> Crews { get; set; } = null!;
    public DbSet<CrewRole> CrewRoles { get; set; } = null!;
    public DbSet<Destination> Destinations { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Ship> Ships { get; set; } = null!;
    public DbSet<ShipModel> ShipModels { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // postgres is cool and supports enums
        // https://www.npgsql.org/efcore/mapping/enum.html?tabs=tabid-1
        modelBuilder.HasPostgresEnum<Ship.ShipState>();
        modelBuilder.HasPostgresEnum<CargoModel.CargoType>();
    }

    static ExnStarshipContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Ship.ShipState>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoModel.CargoType>();
    }
}
