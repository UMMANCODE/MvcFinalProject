using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project.Models {
	public class AppUser : IdentityUser {
		[MaxLength(100)]
		public string FullName { get; set; }
		public bool? ShouldChangePassword { get; set; }
  }
}
