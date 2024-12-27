using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SignInRequest
	{
		[Required(ErrorMessage = "Email is Required")]
		[JsonPropertyName("email")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public required string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Passowrd is required")]
		[JsonPropertyName("password")]
		public required string Password { get; set; }
	}
}
