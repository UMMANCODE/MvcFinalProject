using System.Data;
using ClosedXML.Excel;
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
	[Authorize(Roles = "superadmin, admin")]
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

		public FileResult ExportContactsInExcel() {
			var contacts = _context.Contacts
				.Include(x => x.AppUser)
				.ToList();
			var fileName = "contacts.xlsx";
			return GenerateExcel(fileName, contacts);
		}

		private FileResult GenerateExcel(string fileName, List<Contact> data) {
			DataTable dataTable = new("Contacts");
			dataTable.Columns.AddRange(
			[
				new("Id"),
				new("Name"),
				new("Email"),
				new("Subject"),
				new("Message"),
				new("Answer"),
				new("User"),
				new("CreatedAt"),
				new("UpdatedAt")
			]);

			foreach (var item in data) {
				dataTable.Rows.Add(item.Id, item.Name ?? "N/A", item.Email ?? "N/A", item.Subject, item.Message, item.Answer ?? "N/A", item.AppUser?.UserName ?? "N/A", item.CreatedAt, item.UpdatedAt);
			}

			using XLWorkbook wb = new();
			wb.Worksheets.Add(dataTable);
			using MemoryStream stream = new();
			wb.SaveAs(stream);

			return File(stream.ToArray(),
					"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
					fileName);
		}
	}
}
