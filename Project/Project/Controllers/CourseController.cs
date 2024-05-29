using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcPustok.Data;
using Newtonsoft.Json;
using Project.Helpers;
using Project.Models;
using Project.Models.Enums;
using Project.ViewModels;
using Stripe.Checkout;

namespace Project.Controllers {
  public class CourseController : Controller {
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly StripeSettings _stripeSettings;

    public CourseController(AppDbContext context, UserManager<AppUser> userManager, IOptions<StripeSettings> stripeSettings) {
      _context = context;
      _userManager = userManager;
      _stripeSettings = stripeSettings.Value;
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
      if (model == null) return RedirectToAction("notfound", "error");
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Apply(ApplyFormViewModel model) {
      var courseDetails = GetCourseDetails(model.CourseId);
      courseDetails.ApplyFormVM = model;

      if (!ModelState.IsValid) {
        return View("Details", courseDetails);
      }

      TempData["ApplicationDetails"] = JsonConvert.SerializeObject(model);

      var currency = "usd";
      var successUrl = $"{Request.Scheme}://{Request.Host}/Course/Success?id={courseDetails.Course.Id}";
      var cancelUrl = $"{Request.Scheme}://{Request.Host}/Course/Cancel?id={courseDetails.Course.Id}";
      Stripe.StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

      var options = new SessionCreateOptions {
        PaymentMethodTypes = new List<string>
          {
            "card",
        },
        LineItems = new List<SessionLineItemOptions>
          {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = Convert.ToInt32(courseDetails.Course.Price) * 100,
                    Currency = currency,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = courseDetails.Course.Name,
                        Description = courseDetails.Course.Description
                    },
                },
                Quantity = 1,
            },
        },
        Mode = "payment",
        SuccessUrl = successUrl,
        CancelUrl = cancelUrl,
      };

      var service = new SessionService();
      var session = service.Create(options);

      return Redirect(session.Url);
    }

    public IActionResult Success(int id) {
      var applicationDetailsJson = TempData["ApplicationDetails"] as string;
      if (string.IsNullOrEmpty(applicationDetailsJson)) {
        TempData["Result"] = "danger";
        TempData["Message"] = "Application failed! No application details found.";
        return RedirectToAction("Index");
      }

      var model = JsonConvert.DeserializeObject<ApplyFormViewModel>(applicationDetailsJson);

      Application application = new();

      if (User.Identity.IsAuthenticated) {
        var course = _context.Courses.FirstOrDefault(c => c.Id == model.CourseId);
        var user = _userManager.GetUserAsync(User).Result;
        application = new Application {
          CourseId = model.CourseId,
          Course = course,
          AppUserId = user.Id,
          AppUser = user,
          Status = ApplicationStatus.Processing
        };
      }
      else {
        var name = model.Name;
        var email = model.Email;
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)) {
          TempData["Result"] = "danger";
          TempData["Message"] = "Application failed! Name and Email are required.";
          return RedirectToAction("Index");
        }
        var course = _context.Courses.FirstOrDefault(c => c.Id == model.CourseId);
        application = new Application {
          CourseId = model.CourseId,
          Course = course,
          Email = email,
          Name = name,
          Status = ApplicationStatus.Processing
        };
      }
      _context.Applications.Add(application);
      _context.SaveChanges();

      if (application.Id > 0) {
        TempData["Result"] = "success";
        TempData["Message"] = "Application submitted successfully!";
        return RedirectToAction("Details", new { id });
      }
      else {
        TempData["Result"] = "danger";
        TempData["Message"] = "Application failed!";
        return RedirectToAction("Details", new { id });
      }
    }

    public IActionResult Cancel(int id) {
      TempData["Result"] = "danger";
      TempData["Message"] = "Payment failed!";
      return RedirectToAction("Details", new { id });
    }
  }
}
