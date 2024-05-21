using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers {
	public class AuthController : Controller {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Login() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel) {
			if (ModelState.IsValid) {
				var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.UserName, userLoginViewModel.Password, userLoginViewModel.RememberMe, false);
				if (result.Succeeded) {
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Invalid login attempt.");
			}
			return View(userLoginViewModel);
		}

		public IActionResult Register() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserRegisterViewModel userRegisterViewModel) {
			if (ModelState.IsValid) {
				var user = new AppUser { 
					UserName = userRegisterViewModel.UserName,
					Email = userRegisterViewModel.EmailAddress,
					FullName = userRegisterViewModel.FullName
				};
				var result = await _userManager.CreateAsync(user, userRegisterViewModel.Password);
				if (result.Succeeded) {
					await _signInManager.SignInAsync(user, isPersistent: false);
					await _userManager.AddToRoleAsync(user, "user");
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors) {
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(userRegisterViewModel);
		}

		[Authorize(Roles = "user")]
		public async Task<IActionResult> Logout() {
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
