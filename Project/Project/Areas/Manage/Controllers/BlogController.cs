using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Helpers;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
  [Area("manage")]
  [Authorize(Roles = "superadmin, admin")]
  public class BlogController : Controller {
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BlogController(AppDbContext context, IWebHostEnvironment env) {
      _context = context;
      _env = env;
    }

    public IActionResult Index(int page = 1) {
      var query = _context.Blogs.AsQueryable();

      var pageData = PaginatedList<Blog>.Create(query, page, 3);

      if (pageData.TotalPages < page && pageData.TotalPages >= 1)
        return RedirectToAction("index", new { page = pageData.TotalPages });

      return View(pageData);
    }

    public IActionResult Create() {
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Blog blog) {
      if (blog.ImageFile == null) ModelState.AddModelError("ImageFile", "ImageFile is required!");

      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        return View(blog);
      }

      if (!_context.Categories.Any(x => x.Id == blog.CategoryId))
        return RedirectToAction("notfound", "error");

      foreach (var tagId in blog.TagIds) {
        if (!_context.Tags.Any(x => x.Id == tagId))
          return RedirectToAction("notfound", "error");

        BlogTags blogTag = new() {
          TagId = tagId
        };
        blog.BlogTags.Add(blogTag);
      }

      blog.ImageName = FileManager.Save(blog.ImageFile, _env.WebRootPath, "img/blog");

      _context.Blogs.Add(blog);
      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Edit(int id) {
      var blog = _context.Blogs
        .Include(x => x.BlogTags)
        .FirstOrDefault(x => x.Id == id);

      if (blog == null)
        return RedirectToAction("notfound", "error");
      ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
      ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
      blog.TagIds = blog.BlogTags.Select(x => x.TagId).ToList();
      return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Blog blog) {
      if (!ModelState.IsValid) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Tags = new SelectList(_context.Tags, "Id", "Name");
        blog.TagIds = blog.BlogTags.Select(x => x.TagId).ToList();
        return View(blog);
      }

      var existBlog = _context.Blogs
        .Include(x => x.BlogTags)
        .FirstOrDefault(x => x.Id == blog.Id);

      if (existBlog == null) return RedirectToAction("notfound", "error");

      if (blog.CategoryId != existBlog.CategoryId && !_context.Categories.Any(x => x.Id == blog.CategoryId))
        return RedirectToAction("notfound", "error");

      existBlog.BlogTags.RemoveAll(x => !blog.TagIds.Contains(x.TagId));

      foreach (var tagId in blog.TagIds.FindAll(x => !existBlog.BlogTags.Any(bt => bt.TagId == x))) {
        if (!_context.Tags.Any(x => x.Id == tagId)) return RedirectToAction("notfound", "error");

        BlogTags blogTag = new() {
          TagId = tagId,
        };
        existBlog.BlogTags.Add(blogTag);
      }

      string? deletedFile = null;

      existBlog.Name = blog.Name;
      existBlog.Description = blog.Description;
      existBlog.Author = blog.Author;
      existBlog.Date = blog.Date;
      existBlog.CategoryId = blog.CategoryId;

      if (blog.ImageFile != null) {
        deletedFile = existBlog.ImageName;
        existBlog.ImageName = FileManager.Save(blog.ImageFile, _env.WebRootPath, "img/blog");
      }

      _context.Blogs.Update(existBlog);

      if (deletedFile != null) {
        FileManager.Delete(_env.WebRootPath, "img/blog", deletedFile);
      }

      _context.SaveChanges();

      return RedirectToAction("index");
    }

    public IActionResult Delete(int id) {
      var existBlog = _context.Blogs.Find(id);
      if (existBlog == null) return NotFound();

      _context.Blogs.Remove(existBlog);
      _context.SaveChanges();

      if (existBlog.ImageName != null)
        FileManager.Delete(_env.WebRootPath, "img/blog", existBlog.ImageName);

      return RedirectToAction("index");
    }
  }
}
