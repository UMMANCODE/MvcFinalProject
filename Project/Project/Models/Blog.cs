using MvcPustok.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;

namespace Project.Models {
	public class Blog : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[MaxLength(1000)]
		[Required]
		public string Description { get; set; }
		[MaxLength(20)]
		[Required]
		public string Author { get; set; }
		[Required]
		[NotActual]
		public DateTime Date { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public List<BlogTags>? BlogTags { get; set; } = new();
	}
}
