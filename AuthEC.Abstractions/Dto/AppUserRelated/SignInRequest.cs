using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SignInRequest
	{
		[Required(ErrorMessage = "Email is Required")]
		public required string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Passowrd is required")]
		public required string Password { get; set; }
	}
}
