using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ContactFormViewModel {
		[MaxLength(50)]
		public string? Name { get; set; }
		[MaxLength(50)]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		[MaxLength(50)]
		public string Subject { get; set; }
		[Required]
		[MaxLength(500)]
		public string Message { get; set; }
		public string? AppUserId { get; set; }
	}
}
