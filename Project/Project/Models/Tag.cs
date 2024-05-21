using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Tag : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		public List<CourseTags>? CourseTags { get; set; } = new();
		public List<EventTags>? EventTags { get; set; } = new();
		public List<BlogTags>? BlogTags { get; set; } = new();
	}
}
