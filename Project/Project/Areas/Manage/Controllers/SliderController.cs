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
	public class SliderController : Controller {
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

    public SliderController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
			var query = _context.Sliders.AsQueryable();

			var pageData = PaginatedList<Slider>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Slider slider) {
      if (slider.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");
      if (slider.Order <= _context.Sliders.Max(s => s.Order)) ModelState.AddModelError("Order", "Order is invalid!");

      if (!ModelState.IsValid)
				return View();

      slider.ImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath, "img/slider");

      _context.Sliders.Add(slider);
			_context.SaveChanges();

			return RedirectToAction("index");
		}

		public IActionResult Edit(int id) {
			var slider = _context.Sliders.Find(id);

			if (slider == null)
				return RedirectToAction("notfound", "error");

      return View(slider);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
    public IActionResult Edit(Slider slider) {
      if (slider.Order <= _context.Sliders.Max(s => s.Order)) ModelState.AddModelError("Order", "Order is invalid!");
      if (!ModelState.IsValid) return View(slider);

      var existSlider = _context.Sliders.Find(slider.Id);
      if (existSlider == null) return RedirectToAction("notfound", "error");

      string? deletedFile = null;

      existSlider.Title = slider.Title;
      existSlider.Description = slider.Description;
      existSlider.BtnText = slider.BtnText;
      existSlider.BtnUrl = slider.BtnUrl;
      existSlider.Order = slider.Order;

      if (slider.ImageFile != null) {
        deletedFile = existSlider.ImageName;
        existSlider.ImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath, "img/slider");
      }

      _context.Sliders.Update(existSlider);

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/slider", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Delete(int id) {
      var existSlider = _context.Sliders.Find(id);
      if (existSlider == null) return NotFound();

      _context.Sliders.Remove(existSlider);
      _context.SaveChanges();

			if (existSlider.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/slider", existSlider.ImageName);

      return RedirectToAction("index");
		}
	}
}
