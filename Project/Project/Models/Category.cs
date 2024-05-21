using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Category : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		public List<Course>? Courses { get; set; } = new();
		public List<Event>? Events { get; set; } = new();
		public List<Blog>? Blogs { get; set; } = new();
	}
}
