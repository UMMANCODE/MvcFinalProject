using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers {
	public class ErrorController : Controller {
		public new IActionResult NotFound() {
			return View();
		}

		public IActionResult Unknown() {
			return View();
		}

		public IActionResult Forbidden() {
			return View();
		}

		public new IActionResult Unauthorized() {
			return View();
		}
	}
}
