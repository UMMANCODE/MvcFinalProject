using System.ComponentModel.DataAnnotations;

namespace Project.Models {
	public class Skill : BaseEntity {
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[Required]
		[Range(0, 100)]
		public int Percent { get; set; }
	}
}
