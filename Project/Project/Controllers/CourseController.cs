using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.Models.Enums;
using Project.ViewModels;

namespace Project.Controllers {
	public class CourseController : Controller {
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		public CourseController(AppDbContext context, UserManager<AppUser> userManager) {
			_context = context;
			_userManager = userManager;
		}

		public CourseDetailsViewModel GetCourseDetails(int id) {
			var course = _context.Courses
				.Include(c => c.CourseTags)
				.ThenInclude(ct => ct.Tag)
				.FirstOrDefault(c => c.Id == id);
			if (course == null) return null;
			var model = new CourseDetailsViewModel {
				Course = course,
				Categories = _context.Categories
				.Include(c => c.Courses)
				.ToList(),
				Posts = _context.Posts.ToList(),
				Tags = _context.Tags.ToList(),
				ApplyFormVM = new ApplyFormViewModel {
					CourseId = course.Id,
				}
			};
			return model;
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
			var model = GetCourseDetails(id);
			if (model == null) return NotFound();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Apply(ApplyFormViewModel model) {
			if (!ModelState.IsValid) {
				var courseDetails = GetCourseDetails(model.CourseId);
				courseDetails.ApplyFormVM = model;
				return View("Details", courseDetails);
			}
			if (User.Identity.IsAuthenticated) {
				var course = _context.Courses.FirstOrDefault(c => c.Id == model.CourseId);
				var user = await _userManager.GetUserAsync(User);
				var application = new Application {
					CourseId = model.CourseId,
					Course = course,
					AppUserId = user.Id,
					AppUser = user,
					Status = ApplicationStatus.Processing
				};
				_context.Applications.Add(application);
				_context.SaveChanges();
				TempData["Success"] = "Your application has been sent successfully";
				return RedirectToAction("details", new { id = model.CourseId });
			}
			else {
				var name = model.Name;
				var email = model.Email;
				if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)) {
					ModelState.AddModelError("", "Name and Email are required");
					var courseDetails = GetCourseDetails(model.CourseId);
					courseDetails.ApplyFormVM = model;
					return View("Details", courseDetails);
				}
				var course = _context.Courses.FirstOrDefault(c => c.Id == model.CourseId);
				var application = new Application {
					CourseId = model.CourseId,
					Course = course,
					Email = email,
					Name = name,
					Status = ApplicationStatus.Processing
				};
				_context.Applications.Add(application);
				_context.SaveChanges();
				TempData["Success"] = "Your application has been sent successfully";
				//return RedirectToAction("details", new { id = model.CourseId });
				return Json(new { success = true });
			}
		}
	}
}
