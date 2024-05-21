using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

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

		[Authorize(Roles = "superadmin")]
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
	}
}
