using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class HomeController : Controller {

		private readonly AppDbContext _context;
		public HomeController(AppDbContext context) {
			_context = context;
		}

		public IActionResult Index() {
			HomeViewModel homeViewModel = new() {
				Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
				Features = _context.Features.ToList(),
				Notices = _context.Notices.ToList(),
				Courses = _context.Courses.Take(3).ToList(),
				Events = _context.Events.OrderByDescending(x => x.StartDate).Take(8).ToList(),
				Blogs = _context.Blogs.Take(3).ToList(),
				Testimonials = _context.Testimonials.OrderBy(x => x.Order).ToList()
			};
			ViewBag.Render = "notrender";
			return View(homeViewModel);
		}

		public IActionResult About() {
			AboutViewModel aboutViewModel = new() {
				Teachers = _context.Teachers.Include(x => x.TeacherSkills)
				.ThenInclude(ts => ts.Skill).Include(x => x.TeacherIcons)
				.ThenInclude(ti => ti.Icon)
				.Take(4).ToList(),
				Notices = _context.Notices.ToList(),
			};
			return View(aboutViewModel);
		}
	}
}
