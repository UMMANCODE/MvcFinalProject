using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace Project.Helpers.Handlers {
	public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement> {
		private readonly UserManager<AppUser> _userManager;

		public EmailVerifiedHandler(UserManager<AppUser> userManager) {
			_userManager = userManager;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement) {
			var user = await _userManager.GetUserAsync(context.User);
			if (user != null && await _userManager.IsEmailConfirmedAsync(user)) {
				context.Succeed(requirement);
			}
			else {
				context.Fail();
			}
		}
	}
}
