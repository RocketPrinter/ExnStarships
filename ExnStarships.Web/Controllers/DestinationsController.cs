using AutoMapper;
using ExnStarships.Services.Dto;
using ExnStarships.Services.Navigation;
using ExnStarships.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExnStarships.Web.Controllers
{
    public class DestinationsController : Controller
    {
        IDestinationService destinationService;
        IMapper mapper;

        public DestinationsController(IDestinationService destinationService, IMapper mapper)
        {
            this.destinationService = destinationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModels = destinationService.GetDestinations()
                .Select(destination => mapper.Map<DestinationDto, DestinationViewModel>(destination));

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] DestinationViewModel viewModel)
        {
            if (viewModel == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
            if (!ModelState.IsValid)
                return View(viewModel);

            destinationService.CreateDestination(mapper.Map<DestinationViewModel, DestinationDto>(viewModel));

            return RedirectToAction("Index", "Destinations");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dto = destinationService.GetDestination(id);
            if (dto == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "Destination cannot be found" });

            return View(mapper.Map<DestinationDto, DestinationViewModel>(dto));
        }

        [HttpPost]
        public IActionResult Edit([FromForm] DestinationViewModel viewModel)
        {
            if (viewModel == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
            if (!ModelState.IsValid)
                return View(viewModel);

            destinationService.UpdateDestination(mapper.Map<DestinationViewModel, DestinationDto>(viewModel));

            return RedirectToAction("Index", "Destinations");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var destination = destinationService.GetDestination(id);
            if (destination == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "Destination cannot be found" });
            return View(mapper.Map<DestinationDto, DestinationViewModel>(destination));
        }

        // not implemented yet
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    roleService.DeleteRole(id);
        //    // todo: put something in the viewbag or something to indicate that the role has been deleted
        //
        //    return RedirectToAction("Index", "Roles");
        //}
    }
}
