using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Data;
using Project.Models;
using Project.Models.Enums;
using Project.ViewModels;

namespace Project.Controllers {
	public class HomeController : Controller {

		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		public HomeController(AppDbContext context, UserManager<AppUser> userManager) {
			_context = context;
			_userManager = userManager;
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
				Testimonials = _context.Testimonials.OrderBy(x => x.Order).ToList()
			};
			return View(aboutViewModel);
		}

		public IActionResult Contact() {
			List<Branch> branches = _context.Branches.Take(3).ToList();
			ContactFormViewModel contacts = new();
			ContactViewModel contactViewModel = new() {
				Branches = branches,
				ContactFormViewModel = contacts
			};
			return View(contactViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SendMessage(ContactFormViewModel contactFormVM) {
			if (!ModelState.IsValid) {
				var branches = _context.Branches.Take(3).ToList();
				var contactViewModel = new ContactViewModel {
					Branches = branches,
					ContactFormViewModel = contactFormVM
				};
				return View("Contact", contactViewModel);
			}
			if (User.Identity.IsAuthenticated) {
				var user = await _userManager.GetUserAsync(User);
				var contact = new Contact {
					Subject = contactFormVM.Subject,
					Message = contactFormVM.Message,
					AppUserId = user.Id,
					AppUser = user
				};
				_context.Contacts.Add(contact);
				await _context.SaveChangesAsync();
				TempData["success"] = "Message sent successfully";
				return RedirectToAction("Contact");
			}
			else {
				var name = contactFormVM.Name;
				var email = contactFormVM.Email;
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email)) {
					ModelState.AddModelError("", "Name and Email are required");
					var branches = _context.Branches.Take(3).ToList();
					var contactViewModel = new ContactViewModel {
						Branches = branches,
						ContactFormViewModel = contactFormVM
					};
					return View("Contact", contactViewModel);
				}
				var contact = new Contact {
					Name = name,
					Email = email,
					Subject = contactFormVM.Subject,
					Message = contactFormVM.Message
				};
				_context.Contacts.Add(contact);
				await _context.SaveChangesAsync();
				TempData["success"] = "Message sent successfully";
				return RedirectToAction("Contact");
			}
		}

		public async Task<IActionResult> Subscribe(string email) {
			if (string.IsNullOrEmpty(email)) {
				return Json(new { success = false, message = "Email is required" });
			}
			var isExist = await _context.Subscribers.AnyAsync(x => x.Email == email);
			if (isExist) {
				return Json(new { success = false, message = "This email is already subscribed" });
			}
			var subscriber = new Subscriber {
				Email = email
			};
			_context.Subscribers.Add(subscriber);
			await _context.SaveChangesAsync();
			var isSubscribed = await _context.Subscribers.AnyAsync(x => x.Email == email);
			return Json(new { success = isSubscribed, message = isSubscribed ? "Subscription successful" : "Subscription failed" });
		}

		public IActionResult Cancel(int id) {
			var message = _context.Applications.Find(id);
			if (message == null)
				return RedirectToAction("notfound", "error");

			message.Status = ApplicationStatus.Cancelled;
			message.UpdatedAt = DateTime.Now;
			_context.SaveChanges();

			return RedirectToAction("profile", "auth", new {tab = "applications"});
		}

		public IActionResult SearchAutocomplete(string? query) {
			var courses = _context.Courses.Where(x => x.Name.Contains(query)).Take(3).ToList();
			var events = _context.Events.Where(x => x.Name.Contains(query)).Take(3).ToList();
			var blogs = _context.Blogs.Where(x => x.Name.Contains(query)).Take(3).ToList();
			var searchResult = new {
				courses,
				events,
				blogs
			};
			return Json(searchResult);
		}
	}
}
