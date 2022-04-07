using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Navigation;

public class DestinationService
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

    public void CreateRole(DestinationDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        repo.Add(mapper.Map<DestinationDto, Destination>(dto));
        unit.SaveChanges();
    }

    public void UpdateRole(DestinationDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var destination = repo.GetById(dto.Id);
        if (destination == null)
            throw new Exception("Cannot update a role which doesn't exist");
        repo.Update(mapper.Map<DestinationDto, Destination>(dto));

        unit.SaveChanges();
    }
}