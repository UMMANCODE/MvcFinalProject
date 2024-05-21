using MvcPustok.Attributes.ValidationAttributes;
using Project.Attributes.Validation;
using Project.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Event : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[MaxLength(1000)]
		[Required]
		public string Description { get; set; }
		[Required]
		[Actual]
		public DateTime StartDate { get; set; }
		[Required]
		[Actual]
		public DateTime EndDate { get; set; }
		[Required]
		public string Venue { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
		public List<EventTeachers>? EventTeachers { get; set; } = new();
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public List<EventTags>? EventTags { get; set; } = new();
	}
}
