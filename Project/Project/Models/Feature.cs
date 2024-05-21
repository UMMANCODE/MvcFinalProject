using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Feature : BaseEntity {
		[Required]
		[MaxLength(50)]
		public string Title { get; set; }
		[Required]
		[MaxLength(500)]
		public string Description { get; set; }
	}
}
