using System.Text.Json.Serialization;

namespace AuthEC.Abstractions.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RoleOptions
	{
		Admin, Teacher, Student
	}
}
