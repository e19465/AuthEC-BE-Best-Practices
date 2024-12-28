using System.Text.Json.Serialization;


namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SignInResponse
	{
		[JsonPropertyName("accessToken")]
		public required string AccessToken { get; set; }

		[JsonPropertyName("refreshToken")]
		public required string RefreshToken { get; set; }
	}
}
