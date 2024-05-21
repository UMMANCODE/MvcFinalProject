namespace Project.Models {
	public class EventTags : BaseEntity {
		public int EventId { get; set; }
		public Event Event { get; set; }
		public int TagId { get; set; }
		public Tag Tag { get; set; }
	}
}
