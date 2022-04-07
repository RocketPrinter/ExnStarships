using AutoMapper;
using ExnStarships.Services.Crews;
using ExnStarships.Services.Dto;
using ExnStarships.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExnStarships.Web.Controllers
{
    public class RolesController : Controller
    {
        IRoleService roleService;
        IMapper mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roleViews = roleService.GetRoles()
                .Select(role => mapper.Map<RoleDto,RoleViewModel>(role));

            return View(roleViews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] RoleViewModel roleVM)
        {
            if (roleVM == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
            if (ModelState.IsValid)
                return View(roleVM);

            roleService.CreateRole(mapper.Map<RoleViewModel, RoleDto>(roleVM));

            return RedirectToAction("Index", "Roles");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "id is less than 1" });

            var role = roleService.GetRole(id);
            if (role == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "role is null" });

            return View(mapper.Map<RoleDto,RoleViewModel>(role));
        }

        [HttpPost]
        public IActionResult Edit([FromForm] RoleViewModel roleVM)
        {
            if (roleVM == null)
                return RedirectToAction("SomethingWentWrong", "Helpers", new { message = "View model is null" });
            if (ModelState.IsValid)
                return View(roleVM);

            roleService.UpdateRole(mapper.Map<RoleViewModel,RoleDto>(roleVM));

            return RedirectToAction("Index", "Roles");
        }
    }
}
