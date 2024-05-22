using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        // [Authorize(Roles = "superadmin admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
