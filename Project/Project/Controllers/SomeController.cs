using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers {
	[Authorize(Policy = "EmailVerified")]
	public class SomeController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
