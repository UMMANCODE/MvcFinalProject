namespace Project.Models {
	public abstract class AuditEntity : BaseEntity {
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; }
	}
}
