using System.ComponentModel.DataAnnotations;

namespace Project.Attributes.Validation {
	public class Actual : ValidationAttribute {
		private readonly DateTime _dateTime;
		public Actual() {
			_dateTime = DateTime.UtcNow;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

			if (value == null) {
				return new ValidationResult("Date is required");
			}

			if ((DateTime)value < _dateTime) {
				return new ValidationResult("Date must be greater than or equal to today");
			}

			return ValidationResult.Success;
		}
	}
}
