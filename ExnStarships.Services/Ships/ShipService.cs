using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Ships;

public class ShipService
{
    IRepository<Ship> repo;
    IRepository<CargoHold> cargoHoldRepo;
    IUnitOfWork unit;
    IMapper mapper;

    public ShipService(IRepository<Ship> repo, IRepository<CargoHold> cargoHoldRepo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.cargoHoldRepo = cargoHoldRepo;
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
        var dest = mapper.Map<ShipDto, Ship>(dto);

        var cargoHold = new CargoHold();
        cargoHoldRepo.Add(cargoHold);
        dest.CargoHold = cargoHold;

        repo.Add(dest);
        unit.SaveChanges();
    }

    public void UpdateShip(ShipDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));

        var ship = repo.GetById(dto.Id);
        if (ship == null)
            throw new Exception("Cannot update a ship which doesn't exist");

        // values found in the entity but not in the dto should not be changed by the mapping
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