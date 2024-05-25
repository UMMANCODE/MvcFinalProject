using System.Collections.Generic;
using System.Linq;
using MvcPustok.Data;
using Project.Models;

namespace Project.Services {
	public class StaticService {
		private readonly AppDbContext _context;
		public StaticService(AppDbContext context) {
			_context = context;
		}

		public Dictionary<string, string> GetSettings() {
			return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
		}

		public Settings GetSetting(string key) {
			return _context.Settings.FirstOrDefault(x => x.Key == key);
		}

		public void CreateSetting(Settings setting) {
			_context.Settings.Add(setting);
			_context.SaveChanges();
		}

		public void UpdateSetting(Settings setting) {
			var existingSetting = _context.Settings.FirstOrDefault(x => x.Key == setting.Key);
			if (existingSetting != null) {
				existingSetting.Value = setting.Value;
				_context.SaveChanges();
			}
		}

		public void DeleteSetting(string key) {
			var setting = _context.Settings.FirstOrDefault(x => x.Key == key);
			if (setting != null) {
				_context.Settings.Remove(setting);
				_context.SaveChanges();
			}
		}

		public List<string> GetCruds() {
			return ["Course", "Event", "Blog", "Slider", "Testimonial", "Teacher", "Category", "Tag", "Notice", "Feature", "Settings"];
		}
	}
}
