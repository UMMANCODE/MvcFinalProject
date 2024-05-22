using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers {
	public class AuthController : Controller {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly EmailService _emailService;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService) {
			_userManager = userManager;
			_signInManager = signInManager;
			_emailService = emailService;
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
					await _userManager.AddToRoleAsync(user, "user");
					TempData["Email"] = user.Email;
					return RedirectToAction("VerifyEmail", "Auth");
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

		public IActionResult ForgetPassword() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordVM) {
			if (!ModelState.IsValid) return View(forgetPasswordVM);

			AppUser? user = await _userManager.FindByEmailAsync(forgetPasswordVM.Email);

			if (user == null || !await _userManager.IsInRoleAsync(user, "user")) {
				ModelState.AddModelError("", "Account is not exist");
				return View();
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

			var fullName = user.FullName;
			var url = Url.Action("verify", "auth", new { email = forgetPasswordVM.Email, token = resetToken }, Request.Scheme);
			TempData["EmailSent"] = forgetPasswordVM.Email;

			var subject = "Reset Password Link";
			var body = EmailTemplates.GetResetPassword(fullName, url);

			_emailService.Send(user.Email, subject, body);

			return View();
		}

		public async Task<IActionResult> Verify(string email, string token) {
			AppUser? user = _userManager.FindByEmailAsync(email).Result;

			if (user == null || !await _userManager.IsInRoleAsync(user, "user")) {
				return RedirectToAction("notfound", "error");
			}

			if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token)) {
				return RedirectToAction("notfound", "error");
			}

			TempData["email"] = email;
			TempData["token"] = token;

			return RedirectToAction("ResetPassword");
		}

		public IActionResult ResetPassword() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM) {
			TempData["email"] = resetPasswordVM.Email;
			TempData["token"] = resetPasswordVM.Token;

			if (!ModelState.IsValid) return View(resetPasswordVM);

			AppUser? user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);

			if (user == null || !await _userManager.IsInRoleAsync(user, "user")) {
				ModelState.AddModelError("", "Account is not exist");
				return View();
			}

			if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordVM.Token)) {
				ModelState.AddModelError("", "Account is not exist");
				return View();
			}

			var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.NewPassword);

			if (!result.Succeeded) {
				foreach (var item in result.Errors) {
					ModelState.AddModelError("", item.Description);
				}
				return View();
			}

			return RedirectToAction("login");
		}

		public IActionResult VerifyEmail() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> VerifyEmail(string email) {
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) {
				return RedirectToAction("NotFound", "Error");
			}
			var fullName = user.FullName;
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { email, token }, Request.Scheme);

			var subject = "Confirm Your Email";
			var body = EmailTemplates.GetVerifyEmail(fullName, confirmationLink);
			_emailService.Send(email, subject, body);

			TempData["EmailSent"] = email;

			return View();
		}

		public async Task<IActionResult> ConfirmEmail(string email, string token) {
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) {
				return RedirectToAction("NotFound", "Error");
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded) {
				return RedirectToAction("Login");
			}
			else {
				return RedirectToAction("Error");
			}
		}
	}
}
