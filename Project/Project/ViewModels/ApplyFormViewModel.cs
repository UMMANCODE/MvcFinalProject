using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels {
	public class ApplyFormViewModel {
		[MaxLength(50)]
		public string? Name { get; set; }
		[MaxLength(50)]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public int CourseId { get; set; }

	}
}
