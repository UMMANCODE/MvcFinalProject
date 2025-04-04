﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Data;
using Project.Models;
using Project.Models.Enums;
using Project.ViewModels;
using static System.Net.Mime.MediaTypeNames;

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
				Features = _context.Features.Take(3).ToList(),
				Notices = _context.Notices.Take(10).ToList(),
				Courses = _context.Courses.Take(3).ToList(),
				Events = _context.Events.OrderByDescending(x => x.StartDate).Take(8).ToList(),
				Blogs = _context.Blogs
				.Include(x => x.Replies)
				.Take(3).ToList(),
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
				Notices = _context.Notices.Take(7).ToList(),
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
			var branches = _context.Branches.Take(3).ToList();
			var contactViewModel = new ContactViewModel {
				Branches = branches,
				ContactFormViewModel = contactFormVM
			};

			if (!ModelState.IsValid) {
				return View("Contact", contactViewModel);
			}

			_ = new
			Contact();
			Contact contact;

			if (User.Identity.IsAuthenticated) {
				var user = await _userManager.GetUserAsync(User);
				contact = new Contact {
					Subject = contactFormVM.Subject,
					Message = contactFormVM.Message,
					AppUserId = user.Id,
					AppUser = user
				};
			}
			else {
				var name = contactFormVM.Name;
				var email = contactFormVM.Email;
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email)) {
					ModelState.AddModelError("", "Name and Email are required");
					return View("Contact", contactViewModel);
				}
				contact = new Contact {
					Name = name,
					Email = email,
					Subject = contactFormVM.Subject,
					Message = contactFormVM.Message
				};
			}
			_context.Contacts.Add(contact);
			_context.SaveChanges();

			if (contact.Id > 0) {
				TempData["Result"] = "success";
				TempData["Message"] = "Message sent successfully!";
				return RedirectToAction("Contact", "Home");
			}
			else {
				TempData["Result"] = "danger";
				TempData["Message"] = "Message failed!";
				return View("Contact", contactViewModel);
			}
		}

		[HttpPost]
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
