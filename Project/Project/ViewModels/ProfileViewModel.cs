using Project.Models;

namespace Project.ViewModels {
	public class ProfileViewModel {
		public ProfileCredEditViewModel? ProfileCredEditVM { get; set; }
		public ProfilePassEditViewModel? ProfilePassEditVM { get; set; }
		public List<Application>? Applications { get; set; } = new();
		public List<Contact>? Contacts { get; set; } = new();
	}
}
