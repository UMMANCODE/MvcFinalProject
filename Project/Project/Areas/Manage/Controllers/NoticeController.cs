using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
	[Authorize(Roles = "admin, superadmin")]
	public class NoticeController : Controller {
		private readonly AppDbContext _context;

		public NoticeController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index(int page = 1) {
			var query = _context.Notices.AsQueryable();

			var pageData = PaginatedList<Notice>.Create(query, page, 5);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Notice notice) {
			if (!ModelState.IsValid)
				return View();

			_context.Notices.Add(notice);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var notice = _context.Notices.Find(id);

			if (notice == null)
				return NotFound();

			return View(notice);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Notice notice) {
			if (!ModelState.IsValid)
				return View();

			_context.Notices.Update(notice);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Delete(int id) {
			var notice = _context.Notices.Find(id);

			if (notice == null)
				return NotFound();

			_context.Notices.Remove(notice);
			_context.SaveChanges();

			return RedirectToAction("index");
		}
	}
}
