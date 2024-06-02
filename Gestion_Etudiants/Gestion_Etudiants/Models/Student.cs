﻿using System.ComponentModel.DataAnnotations;

namespace Gestion_Etudiants.Models
{
	public class Student
	{
		public int StudentId { get; set; }
		[Required]
		public string StudentName { get; set; }
		[Range(1, 100)]
		public int Age { get; set; }
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
		public int SchoolID { get; set; }
		public School School { get; set; }
	}
}
