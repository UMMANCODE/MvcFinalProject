using MvcPustok.Data;

namespace Project.Services {
	public class StaticService {
		private readonly AppDbContext _context;
		public StaticService(AppDbContext context) {
			_context = context;
		}

		public Dictionary<string, string> GetSettings() {
			return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
		}

		public List<string> GetCruds()
		{
			return new List<string> { "Course", "Event", "Blog", "Slider", "Testimonial", "Teacher", "Category", "Tag", "Notice", "Feature" };
        }
    }
}
