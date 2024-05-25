using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Data;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers {
	public class AuthController : Controller {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly EmailService _emailService;
		private readonly AppDbContext _context;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService, AppDbContext context) {
			_userManager = userManager;
			_signInManager = signInManager;
			_emailService = emailService;
			_context = context;
		}
		public IActionResult Login() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel, string? returnUrl) {
			if (ModelState.IsValid) {
				var user = await _userManager.FindByNameAsync(userLoginViewModel.UserName);
				if (user != null && await _userManager.IsInRoleAsync(user, "user")
						&& !await _userManager.IsEmailConfirmedAsync(user)
						&& (await _userManager.CheckPasswordAsync(user, userLoginViewModel.Password))) {
					return RedirectToAction("VerifyEmailGet", "Auth", new { email = user.Email });
				}
				var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.UserName, userLoginViewModel.Password, userLoginViewModel.RememberMe, false);
				if (result.Succeeded) {
					var fullNameClaim = new Claim("Custom:FullName", user.FullName);
					var emailClaim = new Claim("Custom:Email", user.Email);

					var claims = new List<Claim> { fullNameClaim, emailClaim };

					var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);

					if (!addClaimsResult.Succeeded) {
						ModelState.AddModelError("", "Claims could not be added");
						return View(userLoginViewModel);
					}
					return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("index", "home");
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

		[HttpGet]
		public async Task<IActionResult> VerifyEmailGet(string email) {
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

			return View("VerifyEmail");
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

		[Authorize(Roles = "user")]
		public async Task<IActionResult> Profile(string tab = "dashboard") {
			AppUser? user = await _userManager.GetUserAsync(User);

			if (user == null)
				return RedirectToAction("login", "auth");

			ProfileViewModel profileVM = new() {
				ProfilePassEditVM = new() {
					CurrentPassword = "",
					NewPassword = "",
					ConfirmNewPassword = ""
				},
				ProfileCredEditVM = new() {
					FullName = user.FullName,
					Email = user.Email,
					UserName = user.UserName
				},
				Applications = await _context.Applications.Where(a => a.AppUserId == user.Id).ToListAsync(),
				Contacts = await _context.Contacts.Where(c => c.AppUserId == user.Id).ToListAsync()
			};

			ViewBag.Tab = tab;

			return View(profileVM);
		}

		[Authorize(Roles = "user")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(ProfileCredEditViewModel? profileCredEditVM, ProfilePassEditViewModel? profilePassEditVM, string tab = "profile") {
			ViewBag.Tab = tab;
			ProfileViewModel profileVM = new();
			profileVM.ProfileCredEditVM = profileCredEditVM;
			profileVM.ProfilePassEditVM = profilePassEditVM;

			if (!ModelState.IsValid) return View(profileVM);

			AppUser? user = await _userManager.GetUserAsync(User);

			if (user == null) return RedirectToAction("login", "auth");

			if (!string.IsNullOrWhiteSpace(profileCredEditVM.UserName)) user.UserName = profileCredEditVM.UserName;
			if (!string.IsNullOrWhiteSpace(profileCredEditVM.Email)) user.UserName = profileCredEditVM.Email;
			if (!string.IsNullOrWhiteSpace(profileCredEditVM.FullName)) user.UserName = profileCredEditVM.FullName;

			if (!string.IsNullOrWhiteSpace(profilePassEditVM.NewPassword)) {
				var passwordResult = await _userManager.ChangePasswordAsync(user, profilePassEditVM.CurrentPassword, profilePassEditVM.NewPassword);

				if (!passwordResult.Succeeded) {
					foreach (var err in passwordResult.Errors)
						ModelState.AddModelError("", err.Description);

					return View(profileVM);
				}
			}

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded) {
				foreach (var err in result.Errors) {
					if (err.Code == "DuplicateUserName")
						ModelState.AddModelError("UserName", "UserName is already taken");
					else if (err.Code == "DuplicateEmail")
						ModelState.AddModelError("Email", "Email is already taken");
					else ModelState.AddModelError("", err.Description);
				}
				return View(profileVM);
			}

			await _signInManager.SignInAsync(user, false);

			return View(profileVM);
		}

    [HttpGet("~/auth/google")]
    public IActionResult GoogleLogin() {
      var redirectUrl = Url.Action("GoogleResponse", "Auth");
      var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
      return Challenge(properties, "Google");
    }

    [HttpGet("~/auth/google-response")]
    public async Task<IActionResult> GoogleResponse() {
      var info = await _signInManager.GetExternalLoginInfoAsync();
      if (info == null) {
        return RedirectToAction(nameof(Login));
      }

      var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
      if (signInResult.Succeeded) {
        return RedirectToAction("Index", "Home");
      }
      else {
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (email != null) {
          var user = await _userManager.FindByEmailAsync(email);
          if (user == null) {
            user = new AppUser {
              UserName = email,
              Email = email,
              FullName = info.Principal.FindFirstValue(ClaimTypes.Name)
            };
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "user");
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: false);
          }
          else {
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: false);
          }
          return RedirectToAction("Index", "Home");
        }
        return RedirectToAction(nameof(Login));
      }
    }
  }
}
