using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class BlogController : Controller {
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		public BlogController(AppDbContext context, UserManager<AppUser> userManager) {
			_context = context;
			_userManager = userManager;
		}

		public BlogDetailsViewModel GetBlogDetails(int id) {
			var blog = _context.Blogs
				.Include(b => b.Replies.Take(5)).ThenInclude(r => r.AppUser)
				.Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
				.FirstOrDefault(b => b.Id == id);
			if (blog == null) return null;
			var model = new BlogDetailsViewModel {
				Blog = blog,
				Categories = _context.Categories
				.Include(c => c.Courses)
				.ToList(),
				Posts = _context.Posts.ToList(),
				Tags = _context.Tags.ToList(),
				Reply = new Reply {
					BlogId = blog.Id,
					Blog = blog
				},
				TotalReplies = _context.Replies.Count(x => x.BlogId == id)
			};

			return model;
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
			var model = GetBlogDetails(id);
			if (model == null) return RedirectToAction("notfound", "error");
			return View(model);
		}

		public IActionResult LoadMore(int id, int skip) {
			var result = new {
				replyCount = _context.Replies.Count(x => x.BlogId == id),
				replies = _context.Replies
													.Include(x => x.AppUser)
													.Where(x => x.BlogId == id)
													.OrderBy(x => x.CreatedAt)
													.Skip(skip)
													.Take(5)
													.Select(x => new {
														x.Message,
														x.AppUser.FullName,
														x.CreatedAt
													}).ToList()
			};
			return Json(result);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Reply(Reply reply) {
			AppUser? user = await _userManager.GetUserAsync(User);
			if (user == null || !await _userManager.IsInRoleAsync(user, "user"))
				return RedirectToAction("login", "auth", new { returnUrl = Url.Action("details", "blog", new { id = reply.BlogId }) });

			if (!_context.Blogs.Any(x => x.Id == reply.BlogId))
				return RedirectToAction("notfound", "error");

			if (!ModelState.IsValid) {
				var vm = GetBlogDetails(reply.BlogId);
				vm.Reply = reply;
				return View("details", vm);
			}

			reply.AppUserId = user.Id;
			reply.AppUser = user;
			reply.Blog = _context.Blogs.Find(reply.BlogId);
			reply.Message = reply.Message?.Trim();
			reply.CreatedAt = DateTime.Now;

			_context.Replies.Add(reply);
			_context.SaveChanges();

			return RedirectToAction("details", "blog", new { id = reply.BlogId });
		}
	}
}
