using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("Manage")]
	public class FeatureController : Controller {
		private readonly AppDbContext _context;

		public FeatureController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index(int page = 1) {
			var query = _context.Features.AsQueryable();

			var pageData = PaginatedList<Feature>.Create(query, page, 5);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Feature feature) {
			if (!ModelState.IsValid)
				return View();

			_context.Features.Add(feature);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var feature = _context.Features.Find(id);

			if (feature == null)
				return NotFound();

			return View(feature);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Feature feature) {
			if (!ModelState.IsValid)
				return View();

			_context.Features.Update(feature);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Delete(int id) {
			var feature = _context.Features.Find(id);

			if (feature == null)
				return NotFound();

			_context.Features.Remove(feature);
			_context.SaveChanges();

			return RedirectToAction("index");
		}
	}
}
