using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;

namespace Project.Models {
	public class Contact : BaseEntity {
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
		public AppUser? AppUser { get; set; }
		[Actual]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Actual]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
		[MaxLength(500)]
		public string? Answer { get; set; }
  }
}
