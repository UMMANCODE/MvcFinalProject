using MvcPustok.Attributes.ValidationAttributes;
using Project.Attributes.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Post : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
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
	}
}
