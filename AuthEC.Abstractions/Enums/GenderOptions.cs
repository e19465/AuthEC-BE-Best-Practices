using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum GenderOptions
	{
		Male, Female, Other
	}
}
