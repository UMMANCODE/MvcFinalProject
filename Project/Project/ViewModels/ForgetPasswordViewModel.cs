using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ForgetPasswordViewModel {
		[Required]
		[EmailAddress]
		[MaxLength(50)]
		[MinLength(3)]
		public string Email { get; set; }
	}
}
