using System.ComponentModel.DataAnnotations;

namespace Gestion_Etudiants.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		[Display(Name = "Role")]

		public string RoleName { get; set; }
	}
}
