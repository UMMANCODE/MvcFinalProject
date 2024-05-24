using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("Manage")]
	public class TagController : Controller {
		private readonly AppDbContext _context;

		public TagController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index(int page = 1) {
			var query = _context.Tags.AsQueryable();

			var pageData = PaginatedList<Tag>.Create(query, page, 5);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Tag tag) {
			if (!ModelState.IsValid)
				return View();

			_context.Tags.Add(tag);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var tag = _context.Tags.Find(id);

			if (tag == null)
				return NotFound();

			return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Tag tag) {
			if (!ModelState.IsValid)
				return View();

			_context.Tags.Update(tag);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Delete(int id) {
			var tag = _context.Tags.Find(id);

			if (tag == null)
				return NotFound();

			_context.Tags.Remove(tag);
			_context.SaveChanges();

			return RedirectToAction("index");
		}
	}
}
