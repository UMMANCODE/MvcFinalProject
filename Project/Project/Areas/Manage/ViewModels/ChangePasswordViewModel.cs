using System.ComponentModel.DataAnnotations;

namespace Project.Areas.Manage.ViewModels {
  public class ChangePasswordViewModel {
    [Required]
		public string AppUserId { get; set; }
		[Required]
    [MinLength(8)]
    [MaxLength(25)]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(25)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(25)]
    [DataType(DataType.Password)]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; }
  }
}
