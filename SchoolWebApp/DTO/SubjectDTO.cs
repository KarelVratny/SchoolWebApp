﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolWebApp.DTO {
	public class SubjectDTO {
		public int Id { get; set; }
		[MaxLength(25)]
		[MinLength(4, ErrorMessage = "The subject name is too short, the minimum length is 4")]
		[DisplayName("Subject name")]
		public string Name { get; set; }
	}
}
