using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models.Enums;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;
using Project.Services;
using Project.Data;
using Microsoft.AspNetCore.SignalR;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class ApplicationController : Controller {

		private readonly AppDbContext _context;
		private readonly EmailService _emailService;
		private readonly IHubContext<ToastHub> _hubContext;

		public ApplicationController(AppDbContext context, EmailService emailService, IHubContext<ToastHub> hubContext) {
			_context = context;
			_emailService = emailService;
			_hubContext = hubContext;
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
      var message = _context.Applications
				.Include(x => x.AppUser)
				.Include(x => x.Course)
				.FirstOrDefault(x => x.Id == id);

			if (message == null)
        return RedirectToAction("notfound", "error");

      message.Status = ApplicationStatus.Approved;
			message.UpdatedAt = DateTime.Now;
			_context.SaveChanges();

			string body;
			if (message.AppUser != null) {
				body = EmailTemplates.GetCourseEnrollmentEmail(message.AppUser.FullName, message.Course.Name, "approved");
				_emailService.Send(message.AppUser.Email, "Application Approved", body);
			}
			else {
				body = EmailTemplates.GetCourseEnrollmentEmail(message.Name, message.Course.Name, "approved");
				_emailService.Send(message.AppUser.Email, "Application Approved", body);
			}

			if (message.AppUserId != null)
				_hubContext.Clients.User(message.AppUserId).SendAsync("ReceiveMessage", "Your application has been approved.");

			return RedirectToAction("index");
    }

    public IActionResult Reject(int id) {
			var message = _context.Applications
				.Include(x => x.AppUser)
				.Include(x => x.Course)
				.FirstOrDefault(x => x.Id == id);

			if (message == null)
        return RedirectToAction("notfound", "error");

      message.Status = ApplicationStatus.Rejected;
      message.UpdatedAt = DateTime.Now;
			_context.SaveChanges();

			string body;
			if (message.AppUser != null) {
				body = EmailTemplates.GetCourseEnrollmentEmail(message.AppUser.FullName, message.Course.Name, "rejected");
				_emailService.Send(message.AppUser.Email, "Application Rejected", body);
			}
			else {
				body = EmailTemplates.GetCourseEnrollmentEmail(message.Name, message.Course.Name, "rejected");
				_emailService.Send(message.AppUser.Email, "Application Rejected", body);
			}

			if (message.AppUserId != null)
				_hubContext.Clients.User(message.AppUserId).SendAsync("ReceiveMessage", "Your application has been rejected.");

			return RedirectToAction("index");
    }
  }
}
