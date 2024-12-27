using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class RefreshTokenRequest
	{
		[JsonPropertyName("refreshToken")]
		public required string RefreshToken { get; set; }
	}
}
