using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations {
	/// <inheritdoc />
	public partial class Init : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
					name: "AspNetRoles",
					columns: table => new {
						Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRoles", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUsers",
					columns: table => new {
						Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
						FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
						PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
						SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
						TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
						LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
						LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
						AccessFailedCount = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUsers", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Categories",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Categories", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Features",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Features", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Icons",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Icons", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Notices",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Topic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
						Date = table.Column<DateTime>(type: "datetime2", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Notices", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Posts",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
						Date = table.Column<DateTime>(type: "datetime2", nullable: false),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_Posts", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Skills",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Percent = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Skills", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Sliders",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
						BtnText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						BtnUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						Order = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Sliders", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Tags",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Tags", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Teachers",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						About = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
						Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
						Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_Teachers", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Testimonials",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Text = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
						Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						Order = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Testimonials", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "AspNetRoleClaims",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
											column: x => x.RoleId,
											principalTable: "AspNetRoles",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserClaims",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetUserClaims_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserLogins",
					columns: table => new {
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
						table.ForeignKey(
											name: "FK_AspNetUserLogins_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserRoles",
					columns: table => new {
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
						table.ForeignKey(
											name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
											column: x => x.RoleId,
											principalTable: "AspNetRoles",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AspNetUserRoles_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserTokens",
					columns: table => new {
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
						table.ForeignKey(
											name: "FK_AspNetUserTokens_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Blogs",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
						Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
						Date = table.Column<DateTime>(type: "datetime2", nullable: false),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						CategoryId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Blogs", x => x.Id);
						table.ForeignKey(
											name: "FK_Blogs_Categories_CategoryId",
											column: x => x.CategoryId,
											principalTable: "Categories",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Courses",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
						About = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
						HowToApply = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
						Certification = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
						StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
						Duration = table.Column<int>(type: "int", nullable: false),
						ClassDuration = table.Column<int>(type: "int", nullable: false),
						SkillLevel = table.Column<int>(type: "int", nullable: false),
						Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
						StudentCount = table.Column<int>(type: "int", nullable: false),
						Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						CategoryId = table.Column<int>(type: "int", nullable: false),
						CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
						ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
						IsDeleted = table.Column<bool>(type: "bit", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Courses", x => x.Id);
						table.ForeignKey(
											name: "FK_Courses_Categories_CategoryId",
											column: x => x.CategoryId,
											principalTable: "Categories",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Events",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
						StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
						EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
						Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
						ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						CategoryId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Events", x => x.Id);
						table.ForeignKey(
											name: "FK_Events_Categories_CategoryId",
											column: x => x.CategoryId,
											principalTable: "Categories",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "TeacherIcons",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						TeacherId = table.Column<int>(type: "int", nullable: false),
						IconId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_TeacherIcons", x => x.Id);
						table.ForeignKey(
											name: "FK_TeacherIcons_Icons_IconId",
											column: x => x.IconId,
											principalTable: "Icons",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_TeacherIcons_Teachers_TeacherId",
											column: x => x.TeacherId,
											principalTable: "Teachers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "TeacherSkills",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						TeacherId = table.Column<int>(type: "int", nullable: false),
						SkillId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_TeacherSkills", x => x.Id);
						table.ForeignKey(
											name: "FK_TeacherSkills_Skills_SkillId",
											column: x => x.SkillId,
											principalTable: "Skills",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_TeacherSkills_Teachers_TeacherId",
											column: x => x.TeacherId,
											principalTable: "Teachers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "BlogTags",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						BlogId = table.Column<int>(type: "int", nullable: false),
						TagId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_BlogTags", x => x.Id);
						table.ForeignKey(
											name: "FK_BlogTags_Blogs_BlogId",
											column: x => x.BlogId,
											principalTable: "Blogs",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_BlogTags_Tags_TagId",
											column: x => x.TagId,
											principalTable: "Tags",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "CourseTags",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						CourseId = table.Column<int>(type: "int", nullable: false),
						TagId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_CourseTags", x => x.Id);
						table.ForeignKey(
											name: "FK_CourseTags_Courses_CourseId",
											column: x => x.CourseId,
											principalTable: "Courses",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_CourseTags_Tags_TagId",
											column: x => x.TagId,
											principalTable: "Tags",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "EventTags",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						EventId = table.Column<int>(type: "int", nullable: false),
						TagId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_EventTags", x => x.Id);
						table.ForeignKey(
											name: "FK_EventTags_Events_EventId",
											column: x => x.EventId,
											principalTable: "Events",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_EventTags_Tags_TagId",
											column: x => x.TagId,
											principalTable: "Tags",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "EventTeachers",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						EventId = table.Column<int>(type: "int", nullable: false),
						TeacherId = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_EventTeachers", x => x.Id);
						table.ForeignKey(
											name: "FK_EventTeachers_Events_EventId",
											column: x => x.EventId,
											principalTable: "Events",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_EventTeachers_Teachers_TeacherId",
											column: x => x.TeacherId,
											principalTable: "Teachers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_AspNetRoleClaims_RoleId",
					table: "AspNetRoleClaims",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "RoleNameIndex",
					table: "AspNetRoles",
					column: "NormalizedName",
					unique: true,
					filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserClaims_UserId",
					table: "AspNetUserClaims",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserLogins_UserId",
					table: "AspNetUserLogins",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserRoles_RoleId",
					table: "AspNetUserRoles",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "EmailIndex",
					table: "AspNetUsers",
					column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
					name: "UserNameIndex",
					table: "AspNetUsers",
					column: "NormalizedUserName",
					unique: true,
					filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_Blogs_CategoryId",
					table: "Blogs",
					column: "CategoryId");

			migrationBuilder.CreateIndex(
					name: "IX_BlogTags_BlogId",
					table: "BlogTags",
					column: "BlogId");

			migrationBuilder.CreateIndex(
					name: "IX_BlogTags_TagId",
					table: "BlogTags",
					column: "TagId");

			migrationBuilder.CreateIndex(
					name: "IX_Courses_CategoryId",
					table: "Courses",
					column: "CategoryId");

			migrationBuilder.CreateIndex(
					name: "IX_CourseTags_CourseId",
					table: "CourseTags",
					column: "CourseId");

			migrationBuilder.CreateIndex(
					name: "IX_CourseTags_TagId",
					table: "CourseTags",
					column: "TagId");

			migrationBuilder.CreateIndex(
					name: "IX_Events_CategoryId",
					table: "Events",
					column: "CategoryId");

			migrationBuilder.CreateIndex(
					name: "IX_EventTags_EventId",
					table: "EventTags",
					column: "EventId");

			migrationBuilder.CreateIndex(
					name: "IX_EventTags_TagId",
					table: "EventTags",
					column: "TagId");

			migrationBuilder.CreateIndex(
					name: "IX_EventTeachers_EventId",
					table: "EventTeachers",
					column: "EventId");

			migrationBuilder.CreateIndex(
					name: "IX_EventTeachers_TeacherId",
					table: "EventTeachers",
					column: "TeacherId");

			migrationBuilder.CreateIndex(
					name: "IX_TeacherIcons_IconId",
					table: "TeacherIcons",
					column: "IconId");

			migrationBuilder.CreateIndex(
					name: "IX_TeacherIcons_TeacherId",
					table: "TeacherIcons",
					column: "TeacherId");

			migrationBuilder.CreateIndex(
					name: "IX_TeacherSkills_SkillId",
					table: "TeacherSkills",
					column: "SkillId");

			migrationBuilder.CreateIndex(
					name: "IX_TeacherSkills_TeacherId",
					table: "TeacherSkills",
					column: "TeacherId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
					name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
					name: "AspNetUserClaims");

			migrationBuilder.DropTable(
					name: "AspNetUserLogins");

			migrationBuilder.DropTable(
					name: "AspNetUserRoles");

			migrationBuilder.DropTable(
					name: "AspNetUserTokens");

			migrationBuilder.DropTable(
					name: "BlogTags");

			migrationBuilder.DropTable(
					name: "CourseTags");

			migrationBuilder.DropTable(
					name: "EventTags");

			migrationBuilder.DropTable(
					name: "EventTeachers");

			migrationBuilder.DropTable(
					name: "Features");

			migrationBuilder.DropTable(
					name: "Notices");

			migrationBuilder.DropTable(
					name: "Posts");

			migrationBuilder.DropTable(
					name: "Sliders");

			migrationBuilder.DropTable(
					name: "TeacherIcons");

			migrationBuilder.DropTable(
					name: "TeacherSkills");

			migrationBuilder.DropTable(
					name: "Testimonials");

			migrationBuilder.DropTable(
					name: "AspNetRoles");

			migrationBuilder.DropTable(
					name: "AspNetUsers");

			migrationBuilder.DropTable(
					name: "Blogs");

			migrationBuilder.DropTable(
					name: "Courses");

			migrationBuilder.DropTable(
					name: "Tags");

			migrationBuilder.DropTable(
					name: "Events");

			migrationBuilder.DropTable(
					name: "Icons");

			migrationBuilder.DropTable(
					name: "Skills");

			migrationBuilder.DropTable(
					name: "Teachers");

			migrationBuilder.DropTable(
					name: "Categories");
		}
	}
}
