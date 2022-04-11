using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Ships;

public interface IShipService
{
    void CreateShip(ShipDto dto);
    ShipDto? GetShip(int id);
    List<ShipDto> GetShips();
    void UpdateShip(ShipDto dto);
}

public class ShipService : IShipService
{
    IRepository<Ship> repo;
    IRepository<CargoHold> cargoHoldRepo;
    IRepository<Destination> destinationRepo;
    IUnitOfWork unit;
    IMapper mapper;

    public ShipService(IRepository<Ship> repo, IRepository<CargoHold> cargoHoldRepo, IRepository<Destination> destinationRepo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.cargoHoldRepo = cargoHoldRepo;
        this.destinationRepo = destinationRepo;
        this.unit = unit;
        this.mapper = mapper;
    }

    public ShipDto? GetShip(int id)
    {
        var ship = repo.GetById(id);
        return ship == null ? null : mapper.Map<Ship, ShipDto>(ship);
    }

    public List<ShipDto> GetShips() =>
        repo.GetAll()
        .Select(ship => mapper.Map<Ship, ShipDto>(ship))
        .ToList();

    public void CreateShip(ShipDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var ship = mapper.Map<ShipDto, Ship>(dto);
        // todo: same AutoMapper issue as in controller
        ship.DestinationId = ship.Destination?.Id;
        ship.Destination = null;
        ship.Model = null;

        if (ship.DestinationId == null || !destinationRepo.Exists(ship.DestinationId.Value))
            throw new Exception("Ship must havea valid destination!");

        var cargoHold = new CargoHold();
        cargoHoldRepo.Add(cargoHold);
        ship.CargoHold = cargoHold;

        repo.Add(ship);
        unit.SaveChanges();
    }

    public void UpdateShip(ShipDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));

        var ship = repo.GetById(dto.Id);
        if (ship == null)
            throw new Exception("Cannot update a ship which doesn't exist");

        // todo: will be replaced with the navigation system. You won't be able to just edit the destination
        if (ship.DestinationId == null || !destinationRepo.Exists(ship.DestinationId.Value))
            throw new Exception("Ship must havea valid destination!");

        repo.Update(mapper.Map(dto, ship));
        unit.SaveChanges();
    }

    // todo: implement
    // warning! Don't delete if has cargo or crew
    // warning! Make sure to delete CargoHold too
    //public void DeleteShip(int id)
    //{
    //    var role = repo.GetById(id);
    //    if (role == null)
    //        throw new Exception("Cannot delete a role which doesn't exist");
    //    if (role.Crews?.Count > 0)
    //        // todo: find a better way to communicate this
    //        throw new Exception("Cannot delete role as crew is asigned to it.");
    //    repo.Delete(role);
    //    unit.SaveChanges();
    //}
}