using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SignInResponse
	{
		[JsonPropertyName("accessToken")]
		public required string AccessToken { get; set; }

		[JsonPropertyName("refeshToken")]
		public required string RefreshToken { get; set; }
	}
}
