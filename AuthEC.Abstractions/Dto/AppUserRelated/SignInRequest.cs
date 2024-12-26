using System.ComponentModel.DataAnnotations;

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
