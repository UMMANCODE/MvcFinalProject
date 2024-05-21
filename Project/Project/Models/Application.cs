using System.ComponentModel.DataAnnotations;
using Project.Models.Enums;

namespace Project.Models {
	public class Application : BaseEntity {
		[Required][MaxLength(50)]
		public string Name { get; set; }
		[Required][MaxLength(50)]
		public string Email { get; set; }
		[Required]
		public ApplicationType Type { get; set; }
		public List<AppUserApplications>? AppUserApplications { get; set; } = new();
	}
}
