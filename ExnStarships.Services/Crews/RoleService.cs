using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Crews;

public interface IRoleService
{
    void CreateRole(RoleDto dto);
    RoleDto? GetRole(int id);
    List<RoleDto> GetRoles();
    void UpdateRole(RoleDto dto);
    void DeleteRole(int id);
}

public class RoleService : IRoleService
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
        var role = repo.GetById(id);
        return role == null ? null : mapper.Map<Role, RoleDto>(role);
    }

    public List<RoleDto> GetRoles() =>
        repo.GetAll()
        .Select(role => mapper.Map<Role, RoleDto>(role))
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
        repo.Update(mapper.Map<RoleDto, Role>(dto));

        unit.SaveChanges();
    }

    // delete role
    public void DeleteRole(int id)
    {
        var role = repo.GetById(id);
        if (role == null)
            throw new Exception("Cannot delete a role which doesn't exist");
        if (role.Crews?.Count > 0)
            // todo: find a better way to communicate this
            throw new Exception("Cannot delete role as crew is asigned to it.");
        repo.Delete(role);
        unit.SaveChanges();
    }
}