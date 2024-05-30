using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
	[Authorize(Roles = "superadmin, admin")]
	public class ErrorController : Controller {
		public new IActionResult NotFound() {
			return View();
		}

		public new IActionResult Unauthorized() {
			return View();
		}

		public IActionResult Forbidden() {
			return View();
		}

		public IActionResult Unknown() {
			return View();
		}
	}
}
