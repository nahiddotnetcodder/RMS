using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static RMS.Models.ApplicationConstants;

namespace RMS.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _role;
        public RoleController(IRoleService role)
        {
            _role = role;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _role.GetAll();
            return View(roles);
        }
        public async Task<IActionResult> Create()
        {
            var statuList = Enum.GetValues(typeof(Status)).Cast<Status>().Select(v => new NameIdPair
            {
                Name = EnumUtility.GetDescriptionFromEnumValue(v),
                Id = (int)v
            }).ToList();
            ViewData["Status"] = statuList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _role.CreateRole(model);
                if (result.Success)
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _role.GetRoleById(id);

            if (model == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var statuList = Enum.GetValues(typeof(Status)).Cast<Status>().Select(v => new NameIdPair
            {
                Name = EnumUtility.GetDescriptionFromEnumValue(v),
                Id = (int)v
            }).ToList();
            ViewData["Status"] = statuList;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _role.UpdateRole(model);

                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
