using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class EventController : Controller {
		private readonly AppDbContext _context;
		public EventController(AppDbContext context) {
			_context = context;
		}
		public IActionResult Index(int page = 1, int? categoryId = null, int? tagId = null) {
			var query = _context.Events.AsQueryable();

			if (categoryId != null)
				query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null)
				query = query.Where(c => c.EventTags.Any(et => et.TagId == tagId));

			var pageData = PaginatedList<Event>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}


		public IActionResult Filter(int? categoryId, int? tagId) {
			var query = _context.Events.AsQueryable();
			if (categoryId != null) query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null) query = query.Where(c => c.EventTags.Any(ct => ct.TagId == tagId));
			var result = query.ToList();
			return PartialView("_EventListPartial", result);
		}

		public IActionResult Details(int id) {
			var evnt = _context.Events
				.Include(e => e.EventTags)
				.ThenInclude(et => et.Tag)
				.Include(e => e.EventTeachers)
				.ThenInclude(et => et.Teacher)
				.FirstOrDefault(e => e.Id == id);
			if (evnt == null) return NotFound();
			var model = new EventDetailsViewModel {
				Event = evnt,
				Categories = _context.Categories
				.Include(c => c.Courses)
				.ToList(),
				Posts = _context.Posts.ToList(),
				Tags = _context.Tags.ToList()
			};
			return View(model);
		}
	}
}
