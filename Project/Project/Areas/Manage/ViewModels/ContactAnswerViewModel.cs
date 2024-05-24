using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;

namespace Project.Areas.Manage.ViewModels {
  public class ContactAnswerViewModel {
    [Required]
    public int ContactId { get; set; }
    [Actual]
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Required]
    [MaxLength(500)]
    public string Answer { get; set; }
  }
}
