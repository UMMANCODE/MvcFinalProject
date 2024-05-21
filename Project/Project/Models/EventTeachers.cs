namespace Project.Models {
	public class EventTeachers : BaseEntity {
		public int EventId { get; set; }
		public Event Event { get; set; }
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
	}
}
