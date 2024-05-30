using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
	[Authorize(Roles = "admin, superadmin")]
	public class CategoryController : Controller {
		private readonly AppDbContext _context;

		public CategoryController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index(int page = 1) {
			var query = _context.Categories.AsQueryable();

			var pageData = PaginatedList<Category>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category category) {
			if (!ModelState.IsValid)
				return View();

			_context.Categories.Add(category);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var category = _context.Categories.Find(id);

			if (category == null)
				return NotFound();

			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category category) {
			if (!ModelState.IsValid)
				return View();

			_context.Categories.Update(category);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Delete(int id) {
			var category = _context.Categories.Find(id);

			if (category == null)
				return NotFound();

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return RedirectToAction("index");
		}
	}
}
