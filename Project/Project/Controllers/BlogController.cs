using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class BlogController : Controller {
		private readonly AppDbContext _context;
		public BlogController(AppDbContext context) {
			_context = context;
		}
		public IActionResult Index(int page = 1, int? categoryId = null, int? tagId = null) {
			var query = _context.Blogs.AsQueryable();

			if (categoryId != null)
				query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null)
				query = query.Where(c => c.BlogTags.Any(bt => bt.TagId == tagId));

			var pageData = PaginatedList<Blog>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}


		public IActionResult Filter(int? categoryId, int? tagId) {
			var query = _context.Blogs.AsQueryable();
			if (categoryId != null) query = query.Where(c => c.CategoryId == categoryId);
			if (tagId != null) query = query.Where(c => c.BlogTags.Any(ct => ct.TagId == tagId));
			var result = query.ToList();
			return PartialView("_BlogListPartial", result);
		}

		public IActionResult Details(int id) {
			var blog = _context.Blogs
				.Include(b => b.BlogTags)
				.ThenInclude(bt => bt.Tag)
				.FirstOrDefault(e => e.Id == id);
			if (blog == null) return NotFound();
			var model = new BlogDetailsViewModel {
				Blog = blog,
				Categories = _context.Categories
				.Include(c => c.Blogs).ToList(),
				Posts = _context.Posts.ToList(),
				Tags = _context.Tags.ToList()
			};
			return View(model);
		}
	}
}
