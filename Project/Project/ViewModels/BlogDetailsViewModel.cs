using Project.Models;

namespace Project.ViewModels {
	public class BlogDetailsViewModel {
		public Blog Blog { get; set; }
		public List<Category> Categories { get; set; }
		public List<Post> Posts { get; set; }
		public List<Tag> Tags { get; set; }
	}
}
