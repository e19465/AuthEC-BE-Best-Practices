using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Dto.AccountRelated
{
	public class AccountDetailsResponse
	{
		[JsonPropertyName("email")]
		public required string Email { get; set; }

		[JsonPropertyName("fullName")]
		public required string FullName { get; set; }

		[JsonPropertyName("gender")]
		public string? Gender { get; set; }

		[JsonPropertyName("libraryId")]
		public Guid? LibraryId { get; set; }

		[JsonPropertyName("age")]
		public string? Age { get; set; }
	}
}
