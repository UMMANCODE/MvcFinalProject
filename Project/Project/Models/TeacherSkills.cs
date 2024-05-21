namespace Project.Models {
	public class TeacherSkills : BaseEntity {
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int SkillId { get; set; }
		public Skill Skill { get; set; }
	}
}
