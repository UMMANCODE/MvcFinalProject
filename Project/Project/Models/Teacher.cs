using MvcPustok.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Teacher : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string FullName { get; set; }
		[MaxLength(50)]
		[Required]
		public string Position { get; set; }
		[MaxLength(500)]
		public string? About { get; set; }
		public string? Degree { get; set; }
		public string? Experience { get; set; }
		public List<string>? Hobbies { get; set; }
		public string? Faculty { get; set; }
		public string? Mail { get; set; }
		public string? Phone { get; set; }
		public string? Skype { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
		public List<EventTeachers>? EventTeachers { get; set; } = new();
		public List<TeacherIcons>? TeacherIcons { get; set; } = new();
		public List<TeacherSkills>? TeacherSkills { get; set; } = new();
	}
}
