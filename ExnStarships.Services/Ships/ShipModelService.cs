using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;

namespace ExnStarships.Services.Ships;

public interface IShipModelService
{
    void CreateShipModel(ShipModelDto dto);
    void DeleteShipModel(int id);
    ShipModelDto? GetShipModel(int id);
    List<ShipModelDto> GetShipModels();
    void UpdateShipModel(ShipModelDto dto);
}

public class ShipModelService : IShipModelService
{
    IRepository<ShipModel> repo;
    IUnitOfWork unit;
    IMapper mapper;

    public ShipModelService(IRepository<ShipModel> repo, IUnitOfWork unit, IMapper mapper)
    {
        this.repo = repo;
        this.unit = unit;
        this.mapper = mapper;
    }

    public ShipModelDto? GetShipModel(int id)
    {
        var shipModel = repo.GetById(id);
        return shipModel == null ? null : mapper.Map<ShipModel, ShipModelDto>(shipModel);
    }

    public List<ShipModelDto> GetShipModels() =>
        repo.GetAll()
        .Select(shipModel => mapper.Map<ShipModel, ShipModelDto>(shipModel))
        .ToList();

    public void CreateShipModel(ShipModelDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));

        repo.Add(mapper.Map<ShipModelDto, ShipModel>(dto));
        unit.SaveChanges();
    }

    public void UpdateShipModel(ShipModelDto dto)
    {
        if (dto == null)
            throw new ArgumentException(nameof(dto));
        var shipModel = repo.GetById(dto.Id);
        if (shipModel == null)
            throw new Exception("Cannot update a shipModel which doesn't exist");

        repo.Update(mapper.Map(dto, shipModel));
        unit.SaveChanges();
    }

    public void DeleteShipModel(int id)
    {
        var shipModel = repo.GetById(id);

        if (shipModel == null)
            throw new Exception("Cannot delete a shipModel which doesn't exist");
        if (shipModel.Ships?.Count > 0)
            // todo: find a better way to communicate this
            throw new Exception("Cannot delete shipModel as ships are asigned to it.");

        repo.Delete(shipModel);
        unit.SaveChanges();
    }
}