using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Crews;

public class CrewService
{
    IRepository<Crew> repo;
    IUnitOfWork unit;
    IMapper mapper;

    public CrewService(IRepository<Crew> repo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.unit = unit;
        this.mapper = mapper;
    }

    public CrewDto? GetCrew(int id)
    {
        var crew = repo.GetById(id);
        return crew == null ? null : mapper.Map<Crew, CrewDto>(crew);
    }

    // if 0 will show everything
    // todo: maybe filter by roles too>
    public List<CrewDto> GetCrews(int shipId = 0)
    {
        List<Crew> crews = shipId==0 
            ? repo.GetAll() 
            : repo.Query(c => c.ShipId == shipId).ToList();
        return crews.Select(c => mapper.Map<Crew,CrewDto>(c)).ToList();
    }

    public void CreateCrew(CrewDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        repo.Add(mapper.Map<CrewDto, Crew>(dto));
        unit.SaveChanges();
    }

    public void UpdateCrew(CrewDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var crew = repo.GetById(dto.Id);
        if (crew == null)
            throw new Exception("Cannot update a crew which doesn't exist");
        repo.Update(mapper.Map<CrewDto, Crew>(dto));

        unit.SaveChanges();
    }
}
