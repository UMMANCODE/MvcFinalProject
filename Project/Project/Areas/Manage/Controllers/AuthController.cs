using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Manage.ViewModels;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
  [Area("Manage")]
  public class AuthController : Controller {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    [Authorize(Roles = "developer")]
    public async Task<IActionResult> CreateRoles() {
      if (!await _roleManager.RoleExistsAsync("superadmin")) {
        IdentityRole role = new() {
          Name = "superadmin"
        };
        await _roleManager.CreateAsync(role);
      }
      if (!await _roleManager.RoleExistsAsync("admin")) {
        IdentityRole role = new() {
          Name = "admin"
        };
        await _roleManager.CreateAsync(role);
      }
      if (!await _roleManager.RoleExistsAsync("user")) {
        IdentityRole role = new() {
          Name = "user"
        };
        await _roleManager.CreateAsync(role);
      }
      return Ok();
    }

    [Authorize(Roles = "developer")]
    public async Task<IActionResult> CreateSuperAdmin() {
      AppUser superadmin = new() {
        UserName = "superadmin",
        Email = "superadmin@eduhome.com"
      };

      var result = await _userManager.CreateAsync(superadmin, "superadmin123");
      await _userManager.AddToRoleAsync(superadmin, "superadmin");

      return Json(result);
    }

    public IActionResult CreateAdmin() {
      return View();
    }

    [HttpPost]
    [Authorize(Roles = "superadmin")]
    public async Task<IActionResult> CreateAdmin(CreateAdminViewModel createAdminVM) {
      if (ModelState.IsValid) {
        var user = new AppUser {
          UserName = createAdminVM.UserName,
          Email = createAdminVM.Email,
          FullName = createAdminVM.FullName,
          ShouldChangePassword = true
        };
        var result = await _userManager.CreateAsync(user, createAdminVM.Password);
        if (result.Succeeded) {
          await _userManager.AddToRoleAsync(user, "admin");
          return RedirectToAction("Admins", "User");
        }
        foreach (var error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
      }
      return View(createAdminVM);
    }

    public IActionResult Login() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel) {
      if (ModelState.IsValid) {
        var user = await _userManager.FindByNameAsync(userLoginViewModel.UserName);
				if (user == null) {
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(userLoginViewModel);
				}
        if (user.ShouldChangePassword == true) {
					return RedirectToAction("ChangePassword", new { id = user.Id });
				}
				var result = await _signInManager.PasswordSignInAsync(user, userLoginViewModel.Password, userLoginViewModel.RememberMe, false);
        if (result.Succeeded) {
          return RedirectToAction("Index", "Dashboard");
        }
        ModelState.AddModelError("", "Invalid login attempt.");
      }
      return View(userLoginViewModel);
    }

    [Authorize(Roles = "admin superadmin")]
    public async Task<IActionResult> Logout() {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Dashboard");
    }

    public async Task<IActionResult> ChangePassword(string id) {
      var user = await _userManager.FindByIdAsync(id);
			if (user == null) {
				return RedirectToAction("notfound", "error");
			}
			ChangePasswordViewModel changePasswordVM = new() {
				AppUserId = user.Id
			};
			return View(changePasswordVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordVM) {
      if (ModelState.IsValid) {
        var user = await _userManager.FindByIdAsync(changePasswordVM.AppUserId);
        var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);
        if (result.Succeeded) {
          user.ShouldChangePassword = false;
          await _userManager.UpdateSecurityStampAsync(user);
          await _signInManager.RefreshSignInAsync(user);
          return RedirectToAction("Index", "Dashboard");
        }
        foreach (var error in result.Errors) {
          ModelState.AddModelError("", error.Description);
        }
      }
      return View(changePasswordVM);
    }


    [Authorize(Roles = "superadmin, admin")]
    public async Task<IActionResult> Profile() {
      var user = await _userManager.GetUserAsync(User);
      var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
      AdminProfileViewModel adminProfileVM = new() {
        AppUser = user,
        Role = role
      };
      return View(adminProfileVM);
    }
  }
}
