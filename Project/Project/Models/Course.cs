using MvcPustok.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;
using Project.Models.Enums;

namespace Project.Models {
	public class Course : AuditEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[MaxLength(500)]
		[Required]
		public string Description { get; set; }
		[MaxLength(500)]
		public string? About { get; set; }
		[MaxLength(500)]
		public string? HowToApply { get; set; }
		[MaxLength(500)]
		public string? Certification { get; set; }
		[Required]
		[Actual]
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }
		[Required]
		public int Duration { get; set; }
		[Required]
		public int ClassDuration { get; set; }
		[Required]
		public SkillLevel SkillLevel { get; set; }
		[Required]
		public string Language { get; set; }
		[Required]
		public int StudentCount { get; set; }
		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public List<CourseTags>? CourseTags { get; set; } = new();
    [NotMapped]
    public List<int>? TagIds { get; set; } = new();
  }
}

