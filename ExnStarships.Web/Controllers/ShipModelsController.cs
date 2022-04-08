using AutoMapper;
using ExnStarships.Services.Dto;
using ExnStarships.Services.Ships;
using ExnStarships.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExnStarships.Web.Controllers;

public class ShipModelsController : Controller
{
    IShipModelService shipModelService;
    IMapper mapper;

    public ShipModelsController(IShipModelService shipModelService, IMapper mapper)
    {
        this.shipModelService = shipModelService;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModels = shipModelService.GetShipModels()
            .Select(shipModel => mapper.Map<ShipModelDto, ShipModelViewModel>(shipModel));

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] ShipModelViewModel viewModel)
    {
        if (viewModel == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
        if (!ModelState.IsValid)
            return View(viewModel);

        shipModelService.CreateShipModel(mapper.Map<ShipModelViewModel, ShipModelDto>(viewModel));

        return RedirectToAction("Index", "ShipModels");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var shipModel = shipModelService.GetShipModel(id);
        if (shipModel == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "Ship Model cannot be found" });

        return View(mapper.Map<ShipModelDto, ShipModelViewModel>(shipModel));
    }

    [HttpPost]
    public IActionResult Edit([FromForm] ShipModelViewModel viewModel)
    {
        if (viewModel == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
        if (!ModelState.IsValid)
            return View(viewModel);

        shipModelService.UpdateShipModel(mapper.Map<ShipModelViewModel, ShipModelDto>(viewModel));

        return RedirectToAction("Index", "ShipModels");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var shipModel = shipModelService.GetShipModel(id);
        if (shipModel == null)
            return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "ShipModel cannot be found" });
        return View(mapper.Map<ShipModelDto, ShipModelViewModel>(shipModel));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        shipModelService.DeleteShipModel(id);
        // todo: put something in the viewbag or something to indicate that the shipModel has been deleted

        return RedirectToAction("Index", "ShipModels");
    }
}
