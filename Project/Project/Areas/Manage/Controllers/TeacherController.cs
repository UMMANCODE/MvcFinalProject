﻿using Microsoft.AspNetCore.Authorization;
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
  public class TeacherController : Controller {
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TeacherController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
      var query = _context.Teachers.AsQueryable();

      var pageData = PaginatedList<Teacher>.Create(query, page, 3);

      if (pageData.TotalPages < page && pageData.TotalPages >= 1)
        return RedirectToAction("index", new { page = pageData.TotalPages });

      return View(pageData);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Teacher teacher, List<Skill> Skills, List<Icon> Icons) {
      if (teacher.ImageFile == null) {
        ModelState.AddModelError("ImageFile", "ImageFile is required!");
      }

      if (!ModelState.IsValid) {
        return View(teacher);
      }

      teacher.ImageName = FileManager.Save(teacher.ImageFile, _env.WebRootPath, "img/teacher");

      _context.Teachers.Add(teacher);
      _context.SaveChanges();

      foreach (var skill in Skills) {
        var teacherSkill = new TeacherSkills {
          TeacherId = teacher.Id,
          Skill = skill
        };
        _context.TeacherSkills.Add(teacherSkill);
      }

      foreach (var icon in Icons) {
        var teacherIcon = new TeacherIcons {
          TeacherId = teacher.Id,
          Icon = icon
        };
        _context.TeacherIcons.Add(teacherIcon);
      }

      _context.SaveChanges();

      return RedirectToAction("Index");
    }

    public IActionResult Edit(int id) {
      var teacher = _context.Teachers
        .Include(x => x.EventTeachers)
        .Include(x => x.TeacherIcons)
        .Include(x => x.TeacherSkills)
        .FirstOrDefault(x => x.Id == id);

      if (teacher == null)
        return RedirectToAction("notfound", "error");

      return View(teacher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Teacher teacher, List<TeacherSkills> TeacherSkills, List<TeacherIcons> TeacherIcons) {
      if (!ModelState.IsValid) {
        return View(teacher);
      }

      var existTeacher = _context.Teachers
          .Include(x => x.EventTeachers)
          .Include(x => x.TeacherIcons).ThenInclude(ti => ti.Icon)
          .Include(x => x.TeacherSkills).ThenInclude(ts => ts.Skill)
          .FirstOrDefault(x => x.Id == teacher.Id);

      if (existTeacher == null) return RedirectToAction("notfound", "error");

      string? deletedFile = null;

      existTeacher.FullName = teacher.FullName;
      existTeacher.Position = teacher.Position;
      existTeacher.About = teacher.About;
      existTeacher.Degree = teacher.Degree;
      existTeacher.Experience = teacher.Experience;
      existTeacher.Hobbies = teacher.Hobbies;
      existTeacher.Faculty = teacher.Faculty;
      existTeacher.Mail = teacher.Mail;
      existTeacher.Phone = teacher.Phone;
      existTeacher.Skype = teacher.Skype;

      if (teacher.ImageFile != null) {
        deletedFile = existTeacher.ImageName;
        existTeacher.ImageName = FileManager.Save(teacher.ImageFile, _env.WebRootPath, "img/teacher");
      }

      _context.Teachers.Update(existTeacher);

      var existingSkills = _context.TeacherSkills.Where(ts => ts.TeacherId == teacher.Id).ToList();
      _context.TeacherSkills.RemoveRange(existingSkills);

      foreach (var skill in TeacherSkills) {
        skill.TeacherId = teacher.Id;
        _context.TeacherSkills.Add(skill);
      }

      var existingIcons = _context.TeacherIcons.Where(ti => ti.TeacherId == teacher.Id).ToList();
      _context.TeacherIcons.RemoveRange(existingIcons);

      foreach (var icon in TeacherIcons) {
        icon.TeacherId = teacher.Id;
        _context.TeacherIcons.Add(icon);
      }

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/teacher", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("Index");
    }

    public IActionResult Delete(int id) {
      var existTeacher = _context.Teachers.Find(id);
      if (existTeacher == null) return NotFound();

      _context.Teachers.Remove(existTeacher);
      _context.SaveChanges();

      if (existTeacher.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/teacher", existTeacher.ImageName);

      return RedirectToAction("index");
    }
  }
}
