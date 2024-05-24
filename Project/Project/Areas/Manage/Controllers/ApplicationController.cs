using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models.Enums;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;
using Project.Services;
using Project.Data;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class ApplicationController : Controller {

		private readonly AppDbContext _context;
    private readonly EmailService _emailService;
    public ApplicationController(AppDbContext context, EmailService emailService) {
			_context = context;
      _emailService = emailService;
    }

		public IActionResult Index(int page = 1) {
			var query = _context.Applications
				.Include(x => x.AppUser)
        .Include(x => x.Course)
        .Where(x => x.Status == ApplicationStatus.Processing)
        .AsQueryable();

			var pageData = PaginatedList<Application>.Create(query, page, 10);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Approve(int id) {
      var message = _context.Applications.Find(id);
      if (message == null)
        return RedirectToAction("notfound", "error");

      message.Status = ApplicationStatus.Approved;
			message.UpdatedAt = DateTime.Now;
			_context.SaveChanges();
      var body = EmailTemplates.GetCourseEnrollmentEmail(message.AppUser.FullName, message.Course.Name, "approved");
      _emailService.Send(message.AppUser.Email, "Application Approved", body);

      return RedirectToAction("index");
    }

    public IActionResult Reject(int id) {
      var message = _context.Applications.Find(id);
      if (message == null)
        return RedirectToAction("notfound", "error");

      message.Status = ApplicationStatus.Rejected;
      message.UpdatedAt = DateTime.Now;
			_context.SaveChanges();
      var body = EmailTemplates.GetCourseEnrollmentEmail(message.AppUser.FullName, message.Course.Name, "rejected");
      _emailService.Send(message.AppUser.Email, "Application Rejected", body);

      return RedirectToAction("index");
    }
  }
}
