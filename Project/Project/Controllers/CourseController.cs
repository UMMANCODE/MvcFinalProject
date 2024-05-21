using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class CourseController : Controller {
		private readonly AppDbContext _context;
		public CourseController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index(int page = 1, int? categoryId = null, int? tagId = null) {
			var query = _context.Courses.AsQueryable();

			if (categoryId != null)
				query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null)
				query = query.Where(c => c.CourseTags.Any(ct => ct.TagId == tagId));

			var pageData = PaginatedList<Course>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}


		public IActionResult Search(string q) {
			var result = _context.Courses
					.Where(c => EF.Functions.Like(c.Name, $"%{q}%"))
					.ToList();
			return PartialView("_CourseListPartial", result);
		}

		public IActionResult Filter(int? categoryId, int? tagId) {
			var query = _context.Courses.AsQueryable();
			if (categoryId != null) query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null) query = query.Where(c => c.CourseTags.Any(ct => ct.TagId == tagId));
			var result = query.ToList();
			return PartialView("_CourseListPartial", result);
		}

		public IActionResult Details(int id) {
			var course = _context.Courses
				.Include(c => c.CourseTags)
				.ThenInclude(ct => ct.Tag)
				.FirstOrDefault(c => c.Id == id);
			if (course == null) return NotFound();
			var model = new CourseDetailsViewModel {
				Course = course,
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
