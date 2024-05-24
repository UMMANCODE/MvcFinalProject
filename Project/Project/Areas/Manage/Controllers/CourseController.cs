using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Helpers;
using Project.Models;
using Project.Models.Enums;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
  [Area("manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class CourseController : Controller {
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CourseController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
      var query = _context.Courses.AsQueryable();

      var pageData = PaginatedList<Course>.Create(query, page, 3);

      if (pageData.TotalPages < page && pageData.TotalPages >= 1)
        return RedirectToAction("index", new { page = pageData.TotalPages });

      return View(pageData);
    }

    public IActionResult Create() {
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.SkillLevels = Enum.GetValues(typeof(SkillLevel))
                                .Cast<SkillLevel>()
                                .Select(x => new SelectListItem {
                                  Value = ((int)x).ToString(),
                                  Text = x.ToString()
                                });
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Course course) {
      if (course.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");

      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.SkillLevels = Enum.GetValues(typeof(SkillLevel))
                          .Cast<SkillLevel>()
                          .Select(x => new SelectListItem {
                            Value = ((int)x).ToString(),
                            Text = x.ToString()
                          });
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        return View(course);
      }

      if (!_context.Categories.Any(x => x.Id == course.CategoryId))
        return RedirectToAction("notfound", "error");

      foreach (var tagId in course.TagIds) {
        if (!_context.Tags.Any(x => x.Id == tagId)) 
          return RedirectToAction("notfound", "error");

        CourseTags courseTag = new() {
          TagId = tagId
        };
        course.CourseTags.Add(courseTag);
      }

      course.ImageName = FileManager.Save(course.ImageFile, _env.WebRootPath, "img/course");

      _context.Courses.Add(course);
      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Edit(int id) {
      var course = _context.Courses
        .Include(x => x.CourseTags)
        .FirstOrDefault(x => x.Id == id);

      if (course == null)
        return RedirectToAction("notfound", "error");
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.SkillLevels = Enum.GetValues(typeof(SkillLevel))
                                .Cast<SkillLevel>()
                                .Select(x => new SelectListItem {
                                  Value = ((int)x).ToString(),
                                  Text = x.ToString()
                                });
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      course.TagIds = course.CourseTags.Select(x => x.TagId).ToList();
      return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Course course) {
      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.SkillLevels = Enum.GetValues(typeof(SkillLevel))
                                  .Cast<SkillLevel>()
                                  .Select(x => new SelectListItem {
                                    Value = ((int)x).ToString(),
                                    Text = x.ToString()
                                  });
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        course.TagIds = course.CourseTags.Select(x => x.TagId).ToList();
        return View(course); 
      }

      var existCourse = _context.Courses
        .Include(x => x.CourseTags)
        .FirstOrDefault(x => x.Id == course.Id);

      if (existCourse == null) return RedirectToAction("notfound", "error");

      if (course.CategoryId != existCourse.CategoryId && !_context.Categories.Any(x => x.Id == course.CategoryId))
        return RedirectToAction("notfound", "error");

      existCourse.CourseTags.RemoveAll(x => !course.TagIds.Contains(x.TagId));

      foreach (var tagId in course.TagIds.FindAll(x => !existCourse.CourseTags.Any(ct => ct.TagId == x))) {
        if (!_context.Tags.Any(x => x.Id == tagId)) return RedirectToAction("notfound", "error");

        CourseTags courseTag = new() {
          TagId = tagId,
        };
        existCourse.CourseTags.Add(courseTag);
      }

      string? deletedFile = null;

      existCourse.Name = course.Name;
      existCourse.Description = course.Description;
      existCourse.About = course.About;
      existCourse.HowToApply = course.HowToApply;
      existCourse.Certification = course.Certification;
      existCourse.StartDate = course.StartDate;
      existCourse.Duration = course.Duration;
      existCourse.ClassDuration = course.ClassDuration;
      existCourse.SkillLevel = course.SkillLevel;
      existCourse.Language = course.Language;
      existCourse.StudentCount = course.StudentCount;
      existCourse.Price = course.Price;
      existCourse.CategoryId = course.CategoryId;

      if (course.ImageFile != null) {
        deletedFile = existCourse.ImageName;
        existCourse.ImageName = FileManager.Save(course.ImageFile, _env.WebRootPath, "img/course");
      }

      _context.Courses.Update(existCourse);

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/course", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Delete(int id) {
      var existCourse = _context.Courses.Find(id);
      if (existCourse == null) return NotFound();

      _context.Courses.Remove(existCourse);
      _context.SaveChanges();

      if (existCourse.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/course", existCourse.ImageName);

      return RedirectToAction("index");
    }
  }
}
