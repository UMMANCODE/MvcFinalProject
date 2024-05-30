using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
	[Authorize(Roles = "admin, superadmin")]
	public class SettingsController : Controller {
		private readonly StaticService _staticService;
		public SettingsController(StaticService staticService) {
			_staticService = staticService;
		}

		public IActionResult Index() {
			var settings = _staticService.GetSettings();
			return View(settings);
		}

		public IActionResult Details(string key) {
			var setting = _staticService.GetSetting(key);
			if (setting == null) {
				return RedirectToAction("notfound", "error");
			}
			return View(setting);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Settings setting) {
			if (ModelState.IsValid) {
				_staticService.CreateSetting(setting);
				return RedirectToAction(nameof(Index));
			}
			return View(setting);
		}

		public IActionResult Edit(string key) {
			var setting = _staticService.GetSetting(key);
			if (setting == null) {
				return RedirectToAction("notfound", "error");
			}
			return View(setting);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Settings setting) {
			if (ModelState.IsValid) {
				_staticService.UpdateSetting(setting);
				return RedirectToAction(nameof(Index));
			}
			return View(setting);
		}

		public IActionResult Delete(string key) {
			var setting = _staticService.GetSetting(key);
			if (setting == null) {
				return RedirectToAction("notfound", "error");
			}
			return View(setting);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string key) {
			_staticService.DeleteSetting(key);
			return RedirectToAction(nameof(Index));
		}
	}
}
