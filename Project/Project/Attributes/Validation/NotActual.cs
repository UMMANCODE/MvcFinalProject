using System.ComponentModel.DataAnnotations;

namespace Project.Attributes.Validation {
	public class NotActual : ValidationAttribute {
		private readonly DateTime _dateTime;
		public NotActual() {
			_dateTime = DateTime.UtcNow;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

			if (value == null) {
				return new ValidationResult("Date is required");
			}

			if ((DateTime)value > _dateTime) {
				return new ValidationResult("Date must be smaller than or equal to today");
			}

			return ValidationResult.Success;
		}
	}
}
