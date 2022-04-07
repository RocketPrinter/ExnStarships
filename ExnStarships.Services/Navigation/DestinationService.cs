using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Navigation;

public interface IDestinationService
{
    void CreateDestination(DestinationDto dto);
    DestinationDto? GetDestination(int id);
    List<DestinationDto> GetDestinations();
    void UpdateDestination(DestinationDto dto);
}

public class DestinationService : IDestinationService
{
    IRepository<Destination> repo;
    IUnitOfWork unit;
    IMapper mapper;

    public DestinationService(IRepository<Destination> repo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
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
        repo.Add(mapper.Map<DestinationDto, Destination>(dto));
        unit.SaveChanges();
    }

    public void UpdateDestination(DestinationDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var destination = repo.GetById(dto.Id);
        if (destination == null)
            throw new Exception("Cannot update a destination which doesn't exist");
        repo.Update(mapper.Map<DestinationDto, Destination>(dto));

        unit.SaveChanges();
    }

    // todo: implement
    //public void DeleteRole(int id)
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