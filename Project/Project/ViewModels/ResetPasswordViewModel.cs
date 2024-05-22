using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ResetPasswordViewModel {
		[MaxLength(25)]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string? NewPassword { get; set; }
		[MaxLength(25)]
		[MinLength(8)]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword))]
		public string? ConfirmNewPassword { get; set; }
		[MaxLength(50)]
		[Required]
		public string Email { get; set; }
		[Required]
		public string Token { get; set; }
	}
}
