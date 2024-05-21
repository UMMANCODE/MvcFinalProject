namespace Project.Models {
	public class TeacherIcons : BaseEntity {
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int IconId { get; set; }
		public Icon Icon { get; set; }
	}
}
