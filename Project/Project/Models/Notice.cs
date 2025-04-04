﻿using System.ComponentModel.DataAnnotations;
using Project.Attributes.Validation;

namespace Project.Models {
	public class Notice : BaseEntity {
		[Required]
		[MaxLength(200)]
		public string Topic { get; set; }
		[Required]
		[Actual]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}
