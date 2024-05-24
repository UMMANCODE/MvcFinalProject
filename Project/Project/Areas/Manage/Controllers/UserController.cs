using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Manage.ViewModels;
using Project.Models;
using Project.ViewModels;

namespace Project.Areas.Manage.Controllers {
	[Area("Manage")]
	public class UserController : Controller {
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) {
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[Authorize(Roles = "superadmin, admin")]
		public async Task<IActionResult> Users(int page = 1) {
			var roleName = "user";

			var role = await _roleManager.FindByNameAsync(roleName);
			if (role == null) {
				return RedirectToAction("notfound", "error");
			}

			var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
			var query = usersInRole.AsQueryable();

			var pageData = PaginatedList<AppUser>.Create(query, page, 3);

			if (pageData.TotalPages < page && pageData.TotalPages >= 1)
				return RedirectToAction("index", new { page = pageData.TotalPages });

			return View(pageData);
		}

		[Authorize(Roles = "superadmin")]
		public async Task<IActionResult> Admins(int page = 1) {
			var roleName1 = "admin";
			var roleName2 = "superadmin";

			var role1 = await _roleManager.FindByNameAsync(roleName1);
			var role2 = await _roleManager.FindByNameAsync(roleName2);
			if (role1 == null && role2 == null) {
				return RedirectToAction("notfound", "error");
			}

			var usersInRole1 = await _userManager.GetUsersInRoleAsync(roleName1);
			var usersInRole2 = await _userManager.GetUsersInRoleAsync(roleName2);
			var totalUsersInRole = usersInRole1.Concat(usersInRole2).Distinct().AsQueryable();

			int pageSize = 3;
			var paginatedUsers = PaginatedList<AppUser>.Create(totalUsersInRole, page, pageSize);

			var usersWithRoles = new List<AdminsWithRolesViewModel>();
			foreach (var admin in paginatedUsers.Items) {
				var roles = await _userManager.GetRolesAsync(admin);
				usersWithRoles.Add(new AdminsWithRolesViewModel {
					Admin = admin,
					Roles = roles
				});
			}

			var paginatedUsersWithRoles = new PaginatedList<AdminsWithRolesViewModel>(
					usersWithRoles, paginatedUsers.TotalPages, page, pageSize);

			if (page > paginatedUsersWithRoles.TotalPages && paginatedUsersWithRoles.TotalPages >= 1) {
				return RedirectToAction("Admins", new { page = paginatedUsersWithRoles.TotalPages });
			}

			return View(paginatedUsersWithRoles);
		}
		/*
		[Authorize(Roles = "superadmin, admin")]
		public async Task<IActionResult> DeleteUser(string id) {
			if (id == null) {
				return RedirectToAction("notfound", "error");
			}

			var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
			if (user == null) {
				return RedirectToAction("notfound", "error");
			}

			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded) {
				return RedirectToAction("users", "user");
			}
			return RedirectToAction("unknown", "error");
		}

		[Authorize(Roles = "superamin")]
		public async Task<IActionResult> DeleteAdmin(string id) {
			if (id == null) {
				return RedirectToAction("notfound", "error");
			}

			var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
			if (user == null) {
				return RedirectToAction("notfound", "error");
			}

			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded) {
				return RedirectToAction("admins", "user");
			}
			return RedirectToAction("unknown", "error");
		}
		*/
	}
}
