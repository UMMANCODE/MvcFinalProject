using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Icon : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[MaxLength(250)]
		[Required]
		public string Url { get; set; }
	}
}
