using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;
using Project.Models.Enums;

namespace Project.Models {
	public class Application : BaseEntity {
		[MaxLength(50)]
		public string? Name { get; set; }
		[MaxLength(50)]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public ApplicationStatus Status { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
		public string? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
		[Actual]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		[Actual]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}
