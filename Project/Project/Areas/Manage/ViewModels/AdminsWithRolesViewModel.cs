using Project.Models;

namespace Project.Areas.Manage.ViewModels {
	public class AdminsWithRolesViewModel {
		public AppUser Admin { get; set; }
		public IList<string> Roles { get; set; }
	}
}
