﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Etudiants.Models
{
	public class StudentContext : IdentityDbContext
    {
		public StudentContext(DbContextOptions<StudentContext> options) : base(options)
		{
		}
		public DbSet<Student> Students { get; set; }
		public DbSet<School> Schools { get; set; }
	}

}
