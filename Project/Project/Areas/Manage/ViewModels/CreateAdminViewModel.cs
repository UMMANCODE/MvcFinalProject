using System.ComponentModel.DataAnnotations;

namespace Project.Areas.Manage.ViewModels {
  public class CreateAdminViewModel {
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string UserName { get; set; }
    [MaxLength(50)]
    [MinLength(3)]
    public string? FullName { get; set; }
    [Required]
    [MaxLength(25)]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    [EmailAddress]
    public string Email { get; set; }
  }
}
