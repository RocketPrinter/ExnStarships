using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Users.Dto;

namespace ExnStarships.Services.Crew;

public class RoleService
{
    private readonly IRepository<Role> repo;
    private readonly IUnitOfWork unit;

    public RoleService(IRepository<Role> repo, IUnitOfWork unit)
    {
        this.repo = repo;
        this.unit = unit;
    }

    public RoleDto GetRole(int id)
    {
        if (id < 1) 
            throw new ArgumentException(nameof(id));

        var role = repo.GetById(id);
        
        return role == null ? null :
            new RoleDto(
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            );
    }

    public List<RoleDto> GetAllUserse() =>
        repo.GetAll().Select(role => new RoleDto(){
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
        })

        public void CreateRole(RoleDto dto)
        {
            if (dto = null) throw new ArgumentException(nameof("Cannot create role using empty dto"))
            var role = new Role()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
            };
        }
}
  public void UpdateUser(UserDto userDto)
  {
    if (userDto == null) throw new ArgumentNullException(nameof(userDto));
    var user = usersRepository.GetById(userDto.ID);
    if (user == null)
    {
      throw new Exception();
    }
    user.FirstName = userDto.FirstName;
    user.LastName = userDto.LastName;
    user.Email = userDto.Email;
    user.Avatar = userDto.Avatar;
    user.BirthDate = userDto.BirthDate;
    unitOfWork.SaveChanges();
  }
}