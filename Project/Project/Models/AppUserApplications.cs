namespace Project.Models {
	public class AppUserApplications : BaseEntity {
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int ApplicationId { get; set; }
		public Application Application { get; set; }
	}
}
