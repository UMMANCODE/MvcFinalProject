using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class TeacherController : Controller {
		private readonly AppDbContext _context;
		public TeacherController(AppDbContext context) {
			_context = context;
		}
		public IActionResult Index(int page = 1) {
			var query = _context.Teachers
				.Include(t => t.TeacherIcons)
				.ThenInclude(ti => ti.Icon);
			var pageData = PaginatedList<Teacher>.Create(query, page, 4);
			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });
			return View(pageData);
		}

		public IActionResult Details(int id) {
			var teacher = _context.Teachers
				.Include(t => t.TeacherSkills)
				.ThenInclude(tc => tc.Skill)
				.Include(t => t.TeacherIcons)
				.ThenInclude(ti => ti.Icon)
				.FirstOrDefault(t => t.Id == id);
			if (teacher == null) return NotFound();
			return View(teacher);
		}
	}
}
