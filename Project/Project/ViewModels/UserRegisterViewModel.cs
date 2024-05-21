using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class UserRegisterViewModel {
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string FullName { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string UserName { get; set; }
		[EmailAddress]
		[Required]
		[MinLength(5)]
		[MaxLength(50)]
		public string EmailAddress { get; set; }
		[Required]
		[MinLength(8)]
		[MaxLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Compare("Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
