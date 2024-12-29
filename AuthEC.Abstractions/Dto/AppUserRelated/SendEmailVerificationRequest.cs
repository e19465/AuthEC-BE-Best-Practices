using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SendEmailVerificationRequest
	{
		[JsonPropertyName("email")]
		public required string Email { get; set; }
	}
}
