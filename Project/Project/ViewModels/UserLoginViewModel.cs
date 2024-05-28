using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace Project.ViewModels {
	public class UserLoginViewModel {
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string UserName { get; set; }
		[Required]
		[MinLength(8)]
		[MaxLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
		public IEnumerable<AuthenticationScheme>? Schemes { get; set; }
  }
}
