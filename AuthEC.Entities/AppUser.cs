using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthEC.Entities
{
	public class AppUser : IdentityUser
	{
		[PersonalData]
		[Column(TypeName = "nvarchar(100)")]
		public string? FullName { get; set; }

		[PersonalData]
		[Column(TypeName = "nvarchar(10)")]
		public string? Gender { get; set; }

		[PersonalData]
		public DateOnly? DateOfBirth { get; set; }

		[PersonalData]
		public Guid? LibraryId { get; set; }
	}
}
