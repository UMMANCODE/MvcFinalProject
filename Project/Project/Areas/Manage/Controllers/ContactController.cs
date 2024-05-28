using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Areas.Manage.ViewModels;
using Project.Data;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
  // [Authorize(Roles = "superadmin,admin")]
  public class ContactController : Controller {
		private readonly AppDbContext _context;
    private readonly EmailService _emailService;
		private readonly IHubContext<ToastHub> _hubContext;

		public ContactController(AppDbContext context, EmailService emailService, IHubContext<ToastHub> hubContext) {
      _context = context;
      _emailService = emailService;
			_hubContext = hubContext;
		}

    public IActionResult Index(int page = 1) {
      var query = _context.Contacts
        .Include(x => x.AppUser)
        .Where(x => x.Answer == null)
        .AsQueryable();

      var pageData = PaginatedList<Contact>.Create(query, page, 10);

      if (pageData.TotalPages < page && pageData.TotalPages >= 1)
        return RedirectToAction("index", new { page = pageData.TotalPages });

      return View(pageData);
    }

    public IActionResult Answer(int id) {
      var contact = _context.Contacts
				.Include(x => x.AppUser)
				.FirstOrDefault(x => x.Id == id);

			if (contact == null)
        return RedirectToAction("notfound", "error");

      var contactAnswerVM = new ContactAnswerViewModel {
        ContactId = contact.Id,
        UpdatedAt = DateTime.Now,
        Answer = contact.Answer
      };

      return View(contactAnswerVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Answer(ContactAnswerViewModel contactAnswerVM) {
      var contact = _context.Contacts
        .Include(x => x.AppUser)
        .FirstOrDefault(x => x.Id == contactAnswerVM.ContactId);

      if (contact == null)
        return RedirectToAction("notfound", "error");

      contact.Answer = contactAnswerVM.Answer;
      contact.UpdatedAt = contactAnswerVM.UpdatedAt;
      _context.SaveChanges();


			string body;
			if (contact.AppUser != null) {
				body = EmailTemplates.GetContactAnswerEmail(contact.AppUser.FullName, contact.Message, contact.Answer);
				_emailService.Send(contact.AppUser.Email, "Answer", body);
			}
			else {
				body = EmailTemplates.GetContactAnswerEmail(contact.Name, contact.Message, contact.Answer);
				_emailService.Send(contact.Email, "Answer", body);
			}

			if (contact.AppUserId != null)
				_hubContext.Clients.User(contact.AppUserId).SendAsync("ReceiveMessage", "Your question has been answered");

			return RedirectToAction("index");
    }
  }
}
