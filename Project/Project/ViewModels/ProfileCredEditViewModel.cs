using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ProfileCredEditViewModel {
		[MaxLength(25)]
		[MinLength(3)]
		public string? UserName { get; set; }
		[MaxLength(100)]
		[MinLength(3)]
		[EmailAddress]
		public string? Email { get; set; }
		[MaxLength(100)]
		public string? FullName { get; set; }
	}
}
