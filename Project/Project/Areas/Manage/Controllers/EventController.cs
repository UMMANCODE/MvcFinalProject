using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcPustok.Data;
using Project.Helpers;
using Project.Models;
using Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Project.Areas.Manage.Controllers {
  [Area("manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class EventController : Controller {
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public EventController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
      var query = _context.Events.AsQueryable();

      var pageData = PaginatedList<Event>.Create(query, page, 3);

      if (pageData.TotalPages < page && pageData.TotalPages >= 1)
        return RedirectToAction("index", new { page = pageData.TotalPages });

      return View(pageData);
    }

    public IActionResult Create() {
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName");
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Event evnt) {
      if (evnt.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");

      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName");
        return View(evnt);
      }

      if (!_context.Categories.Any(x => x.Id == evnt.CategoryId))
        return RedirectToAction("notfound", "error");

      foreach (var tagId in evnt.TagIds) {
        if (!_context.Tags.Any(x => x.Id == tagId))
          return RedirectToAction("notfound", "error");

        EventTags eventTag = new() {
          TagId = tagId
        };
        evnt.EventTags.Add(eventTag);
      }

      foreach (var teacherId in evnt.TeacherIds) {
        if (!_context.Teachers.Any(x => x.Id == teacherId))
          return RedirectToAction("notfound", "error");

        EventTeachers eventTeacher = new() {
          TeacherId = teacherId
        };
        evnt.EventTeachers.Add(eventTeacher);
      }

      evnt.ImageName = FileManager.Save(evnt.ImageFile, _env.WebRootPath, "img/event");

      _context.Events.Add(evnt);
      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Edit(int id) {
      var evnt = _context.Events
        .Include(x => x.EventTags)
        .Include(x => x.EventTeachers)
        .FirstOrDefault(x => x.Id == id);

      if (evnt == null)
        return RedirectToAction("notfound", "error");
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName");
      evnt.TagIds = evnt.EventTags.Select(x => x.TagId).ToList();
      evnt.TeacherIds = evnt.EventTeachers.Select(x => x.TeacherId).ToList();
      return View(evnt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Event evnt) {
      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName");
        evnt.TagIds = evnt.EventTags.Select(x => x.TagId).ToList();
        evnt.TeacherIds = evnt.EventTeachers.Select(x => x.TeacherId).ToList();
        return View(evnt);
      }

      var existEvent = _context.Events
        .Include(x => x.EventTags)
        .Include(x => x.EventTeachers)
        .FirstOrDefault(x => x.Id == evnt.Id);

      if (existEvent == null) return RedirectToAction("notfound", "error");

      if (evnt.CategoryId != existEvent.CategoryId && !_context.Categories.Any(x => x.Id == evnt.CategoryId))
        return RedirectToAction("notfound", "error");

      existEvent.EventTags.RemoveAll(x => !evnt.TagIds.Contains(x.TagId));

      foreach (var tagId in evnt.TagIds.FindAll(x => !existEvent.EventTags.Any(et => et.TagId == x))) {
        if (!_context.Tags.Any(x => x.Id == tagId)) return RedirectToAction("notfound", "error");

        EventTags eventTag = new() {
          TagId = tagId,
        };
        existEvent.EventTags.Add(eventTag);
      }

      existEvent.EventTeachers.RemoveAll(x => !evnt.TeacherIds.Contains(x.TeacherId));

      foreach (var teacherId in evnt.TeacherIds.FindAll(x => !existEvent.EventTeachers.Any(et => et.TeacherId == x))) {
        if (!_context.TeacherIcons.Any(x => x.Id == teacherId)) return RedirectToAction("notfound", "error");

        EventTeachers eventTeacher = new() {
          TeacherId = teacherId,
        };
        existEvent.EventTeachers.Add(eventTeacher);
      }

      string? deletedFile = null;

      existEvent.Name = evnt.Name;
      existEvent.Description = evnt.Description;
      existEvent.StartDate = evnt.StartDate;
      existEvent.EndDate = evnt.EndDate;
      existEvent.Venue = evnt.Venue;
      existEvent.StartDate = evnt.StartDate;
      existEvent.CategoryId = evnt.CategoryId;

      if (evnt.ImageFile != null) {
        deletedFile = existEvent.ImageName;
        existEvent.ImageName = FileManager.Save(evnt.ImageFile, _env.WebRootPath, "img/event");
      }

      _context.Events.Update(existEvent);

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/event", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Delete(int id) {
      var existEvent = _context.Events.Find(id);
      if (existEvent == null) return NotFound();

      _context.Events.Remove(existEvent);
      _context.SaveChanges();

      if (existEvent.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/event", existEvent.ImageName);

      return RedirectToAction("index");
    }
  }
}
