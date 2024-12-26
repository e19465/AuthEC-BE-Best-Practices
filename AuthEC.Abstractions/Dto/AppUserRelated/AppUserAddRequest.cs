using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AuthEC.Abstractions.Enums;
using AuthEC.Entities;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
    public class AppUserAddRequest
    {
        [Required(ErrorMessage = "Full Name is required")]
        [DisplayName("Full Name")]
        [StringLength(100, ErrorMessage = "Maximum length of Full Name is 100 characters")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
		[Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
		public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public required GenderOptions Gender { get; set; }
        public Guid? LibraryId { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public required DateOnly DateOfBirth { get; set; }
        public string? Role { get; set; } = "Student"; // Default role is "Student


		public AppUser ToAppUser()
        {
            return new AppUser
            {
                FullName = FullName,
                Email = Email,
				DateOfBirth = DateOfBirth,
				UserName = Email,
                Gender = Gender.ToString(),
				LibraryId = LibraryId
			};
        }
    }
}
