using ExnStarships.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ExnStarships.Data;

public class MainContext : DbContext
{
    public static bool dbCreated = false;

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

        // using a mix of auto include and lazy loading 
        // this approach should be performant and doesn't require implementing repostiories for every entity
        // I may have no idea what I'm doing.

        // ships
        var ship = modelBuilder.Entity<Ship>();
        ship.Navigation(s => s.Crews).AutoInclude();
        ship.Navigation(s => s.Destination).AutoInclude();
        ship.Navigation(s => s.Model).AutoInclude();

        // destination & connection
        var con = modelBuilder.Entity<Connection>();
        con.HasKey(c => new { c.FirstDestinationId, c.SecondDestinationId });
        //con.Navigation(c => c.FirstDestination).AutoInclude();
        //con.Navigation(c => c.SecondDestination).AutoInclude();
        
        // cargo
        modelBuilder.Entity<Cargo>()
            .Navigation(x => x.CargoHold)
            .AutoInclude();
        
        modelBuilder.Entity<CargoHold>()
            .Navigation(x => x.Cargos)
            .AutoInclude();

        // crew & roles
        var crew = modelBuilder.Entity<Crew>();
        crew.HasMany(c => c.Roles)
            .WithMany(r => r.Crews)
            .UsingEntity<CrewRole>();

        crew.Navigation(c => c.Ship)
            .AutoInclude();
    }

    static MainContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Ship.ShipState>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoModel.CargoType>();
    }

    public MainContext(DbContextOptions options) : base(options)
    {
        //todo: for testing only!!!
        if (!dbCreated)
            Database.EnsureCreated();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseLazyLoadingProxies();
    }
}
