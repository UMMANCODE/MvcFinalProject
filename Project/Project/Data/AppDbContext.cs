using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace MvcPustok.Data {
	public class AppDbContext : IdentityDbContext {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<BlogTags> BlogTags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseTags> CourseTags { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<EventTags> EventTags { get; set; }
		public DbSet<EventTeachers> EventTeachers { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<Icon> Icons { get; set; }
		public DbSet<Notice> Notices { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Testimonial> Testimonials { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<TeacherIcons> TeacherIcons { get; set; }
		public DbSet<TeacherSkills> TeacherSkills { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Settings> Settings { get; set; }
		public DbSet<Application> Applications { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Subscriber> Subscribers { get; set; }

		override protected void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
		}
	}
}

