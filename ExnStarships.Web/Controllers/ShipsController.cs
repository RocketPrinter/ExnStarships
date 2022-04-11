using AutoMapper;
using ExnStarships.Services.Dto;
using ExnStarships.Services.Navigation;
using ExnStarships.Services.Ships;
using ExnStarships.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExnStarships.Web.Controllers;

public class ShipsController : Controller
{
    IShipService shipService;
    IDestinationService destinationService;
    IShipModelService shipModelService;
    IMapper mapper;

    public ShipsController(IShipService shipService, IDestinationService destinationService, IShipModelService shipModelService, IMapper mapper)
    {
        this.shipService = shipService;
        this.destinationService = destinationService;
        this.shipModelService = shipModelService;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModels = shipService.GetShips()
            .Select(ship =>
            {
                var viewModel = mapper.Map<ShipDto, ShipViewModel>(ship);
                viewModel.ModelName = ship.Model?.Name;
                viewModel.DestinationName = ship.Destination?.Name;
                return viewModel;
            });

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ShipViewModel viewModel = new();
        
        // for dropdown
        // these casts are a bit weird 
        viewModel.Destinations = destinationService.GetDestinationsAsSelectList();
        viewModel.Models = shipModelService.GetShipModelsAsSelectList();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create([FromForm] ShipViewModel viewModel)
    {
        if (viewModel == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = mapper.Map<ShipViewModel, ShipDto>(viewModel);
        // todo: for some reason AutoMapper tries to map DestinationId to Destination.Id (but not ModelId to Model.Id???)
        // enabling exact naming convention didn't stop it from happening, even if the docs said it would
        // so we have to do this...
        dto.DestionationId = dto.Destination?.Id;
        dto.Destination = null;

        shipService.CreateShip(mapper.Map<ShipViewModel, ShipDto>(viewModel));

        return RedirectToAction("Index", "Ships");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var ship = shipService.GetShip(id);
        if (ship == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "Ship cannot be found" });

        var viewModel = mapper.Map<ShipDto, ShipViewModel>(ship);
        viewModel.ModelName = ship.Model?.Name;
        viewModel.DestinationName = ship.Destination?.Name;

        return View(viewModel);
    }

    // INCOMPLETE CODE
    // using a tuple as a viewModel. Probably a bad idea but I wanted to try it out
    [HttpGet]
    public IActionResult ChangeDestination(int id)
    {
        var ship = shipService.GetShip(id);
        if (ship == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "Ship cannot be found" });

        (int Id, int? DestinationId) viewModel = (ship.Id, ship.DestionationId);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ChangeDestination((int id, int DestinationId) viewModel)
    {


        return RedirectToAction("Index", "Ships");
    }
}
