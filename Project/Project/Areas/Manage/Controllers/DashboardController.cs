using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Project.Areas.Manage.ViewModels;
using Project.Models.Enums;

namespace Project.Areas.Manage.Controllers {
	[Area("Manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class DashboardController : Controller {
		private readonly AppDbContext _context;
		public DashboardController(AppDbContext context) {
			_context = context;
		}
		public IActionResult Index() {
			var dashboardViewModel = new DashboardViewModel {
				TotalUsers = _context.AppUsers.Count(),
				TotalEarnings = _context.Applications
				.Where(x =>x.Status == ApplicationStatus.Approved)
				.Sum(x => x.Course.Price),
				TotalApplications = _context.Applications.Count(),
				UsersOnline = _context.AppUsers.Count(x => x.IsLoggedIn)
			};
			return View(dashboardViewModel);
		}
	}
}
