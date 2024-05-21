using Project.Models;

namespace Project.ViewModels {
	public class HomeViewModel {
		public List<Slider> Sliders { get; set; }
		public List<Feature> Features { get; set; }
		public List<Notice> Notices { get; set; }
		public List<Course> Courses { get; set; }
		public List<Event> Events { get; set; }
		public List<Blog> Blogs { get; set; }
		public List<Testimonial> Testimonials { get; set; }
	}
}
