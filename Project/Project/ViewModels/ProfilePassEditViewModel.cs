using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ProfilePassEditViewModel {
		[MaxLength(25)]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string? CurrentPassword { get; set; }
		[MaxLength(25)]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string? NewPassword { get; set; }
		[MaxLength(25)]
		[MinLength(8)]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword))]
		public string? ConfirmNewPassword { get; set; }
		public bool HasPassword { get; set; }
	}
}
