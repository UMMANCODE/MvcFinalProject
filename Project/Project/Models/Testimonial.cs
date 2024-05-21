using MvcPustok.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Testimonial : BaseEntity {
		[MaxLength(250)]
		[Required]
		public string Text { get; set; }
		[MaxLength(50)]
		public string? Author { get; set; }
		[MaxLength(50)]
		public string? Position { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
		public int Order { get; set; }
	}
}
