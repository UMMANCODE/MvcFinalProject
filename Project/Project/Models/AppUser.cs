using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project.Models {
	public class AppUser : IdentityUser {
		[MaxLength(100)]
		public string FullName { get; set; }
		public bool? ShouldChangePassword { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime LastActive { get; set; } = DateTime.Now;
		public bool IsLoggedIn { get; set; } = false;
	}
}
 