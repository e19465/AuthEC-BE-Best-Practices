using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class EmailConfirmRequest
	{
		[JsonPropertyName("email")]
		public required string Email { get; set; }

		[JsonPropertyName("code")]
		public required string Code { get; set; }
	}
}
