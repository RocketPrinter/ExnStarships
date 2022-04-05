using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Crew;

public class RoleService
{
    IRepository<Role> repo;
    IUnitOfWork unit;
    IMapper mapper;

    public RoleService(IRepository<Role> repo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.unit = unit;
        this.mapper = mapper;
    }

    public RoleDto? GetRole(int id)
    {
        if (id < 1) 
            throw new ArgumentException(nameof(id));

        var role = repo.GetById(id);

        return role == null ? null : mapper.Map<Role, RoleDto>(role);
    }

    public List<RoleDto> GetAllUserse() => 
        repo.GetAll()
        .Select(role => mapper.Map<Role,RoleDto>(role))
        .ToList();

    public void CreateRole(RoleDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        repo.Add(mapper.Map<RoleDto, Role>(dto));
        unit.SaveChanges();
    }

    public void UpdateRole(RoleDto dto)
    {
        if (dto == null) 
            throw new ArgumentException(nameof(dto));
        var role = repo.GetById(dto.Id);
        if (role == null)
            throw new Exception("Cannot update a role which doesn't exist");
        repo.Update(mapper.Map<RoleDto,Role>(dto));

        unit.SaveChanges();
    }
}