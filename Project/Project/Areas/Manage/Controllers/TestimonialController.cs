using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPustok.Data;
using Org.BouncyCastle.Bcpg.Sig;
using Project.Helpers;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("manage")]
	[Authorize(Roles = "admin, superadmin")]
	public class TestimonialController : Controller {
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

    public TestimonialController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
			var query = _context.Testimonials.AsQueryable();

			var pageData = PaginatedList<Testimonial>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Testimonial testimonial) {
      if (testimonial.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");
      if (testimonial.Order <= _context.Testimonials.Max(s => s.Order)) ModelState.AddModelError("Order", "Order is invalid!");

      if (!ModelState.IsValid)
				return View();

      testimonial.ImageName = FileManager.Save(testimonial.ImageFile, _env.WebRootPath, "img/testimonial");

      _context.Testimonials.Add(testimonial);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var testimonial = _context.Testimonials.Find(id);

			if (testimonial == null)
				return RedirectToAction("Error", "NotFound");

      return View(testimonial);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
    public IActionResult Edit(Testimonial testimonial) {
      if (testimonial.Order <= _context.Testimonials.Max(s => s.Order)) ModelState.AddModelError("Order", "Order is invalid!");
      if (!ModelState.IsValid) return View(testimonial);

      var existTestimonial = _context.Testimonials.Find(testimonial.Id);
      if (existTestimonial == null) return RedirectToAction("Error", "NotFound");

      string? deletedFile = null;

      existTestimonial.Text = testimonial.Text;
      existTestimonial.Author = testimonial.Author;
      existTestimonial.Position = testimonial.Position;
      existTestimonial.Order = testimonial.Order;

      if (testimonial.ImageFile != null) {
        deletedFile = existTestimonial.ImageName;
        existTestimonial.ImageName = FileManager.Save(testimonial.ImageFile, _env.WebRootPath, "img/testimonial");
      }

      _context.Testimonials.Update(existTestimonial);

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/testimonial", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Delete(int id) {
      var existTestimonial = _context.Testimonials.Find(id);
      if (existTestimonial == null) return NotFound();

      _context.Testimonials.Remove(existTestimonial);
      _context.SaveChanges();

			if (existTestimonial.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/testimonial", existTestimonial.ImageName);

      return RedirectToAction("index");
		}
	}
}
