using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExnStarships.Services.Navigation;

public interface IDestinationService
{
    void CreateDestination(DestinationDto dto);
    DestinationDto? GetDestination(int id);
    List<DestinationDto> GetDestinations();
    void UpdateDestination(DestinationDto dto);

    IEnumerable<SelectListItem> GetDestinationsAsSelectList();
}

public class DestinationService : IDestinationService
{
    IRepository<Destination> repo;
    IRepository<CargoHold> cargoHoldRepo;
    IUnitOfWork unit;
    IMapper mapper;

    public DestinationService(IRepository<Destination> repo, IRepository<CargoHold> cargoHoldRepo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.cargoHoldRepo = cargoHoldRepo;
        this.unit = unit;
        this.mapper = mapper;
    }

    public DestinationDto? GetDestination(int id)
    {
        var destination = repo.GetById(id);
        return destination == null ? null : mapper.Map<Destination, DestinationDto>(destination);
    }

    public List<DestinationDto> GetDestinations() =>
        repo.GetAll()
        .Select(destination => mapper.Map<Destination, DestinationDto>(destination))
        .ToList();

    public void CreateDestination(DestinationDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var dest = mapper.Map<DestinationDto, Destination>(dto);
        
        var cargoHold = new CargoHold();
        cargoHoldRepo.Add(cargoHold);
        dest.CargoHold = cargoHold;

        repo.Add(dest);
        unit.SaveChanges();
    }

    public void UpdateDestination(DestinationDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));

        var destination = repo.GetById(dto.Id);
        if (destination == null)
            throw new Exception("Cannot update a destination which doesn't exist");

        // values found in the entity but not in the dto should not be changed by the mapping
        repo.Update(mapper.Map(dto,destination));
        unit.SaveChanges();
    }

    // todo: implement
    // warning! Don't delete if has cargo
    // warning! Make sure to delete CargoHold too
    //public void DeleteDestination(int id)
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

    public IEnumerable<SelectListItem> GetDestinationsAsSelectList()
    {
        var shipModels = repo.GetAll();
        if (shipModels.Count == 0)
            return Enumerable.Repeat(new SelectListItem() { Value = null, Text = "No destinations found!" }, 1);
        return shipModels.Select(sm => new SelectListItem
        {
            Value = sm.Id.ToString(),
            Text = sm.Name
        });
    }
}