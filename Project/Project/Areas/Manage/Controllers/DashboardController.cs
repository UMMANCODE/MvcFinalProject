using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;

namespace Project.Areas.Manage.Controllers {
	[Area("Manage")]
  // [Authorize(Roles = "superadmin, admin")]
  public class DashboardController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
