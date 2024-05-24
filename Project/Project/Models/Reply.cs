namespace Project.Models {
  public class Reply : BaseEntity {
    public string? AppUserId { get; set; }
    public int BlogId { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public AppUser? AppUser { get; set; }
    public Blog? Blog { get; set; }
  }
}
