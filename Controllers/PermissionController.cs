using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
