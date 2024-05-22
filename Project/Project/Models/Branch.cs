using MvcPustok.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Branch : BaseEntity {
		[Required]
		[MaxLength(20)]
		public string Title { get; set; }
		[Required]
		[MaxLength(25)]
		public string Address1 { get; set; }
		[Required]
		[MaxLength(25)]
		public string Address2 { get; set; }
		[MaxLength(100)]
		public string? ImageName { get; set; }
		[NotMapped]
		[MaxSize(2 * 1024 * 1024)]
		[AllowedFileTypes("image/png", "image/jpeg")]
		public IFormFile? ImageFile { get; set; }
	}
}
