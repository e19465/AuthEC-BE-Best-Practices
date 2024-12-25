using System;
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
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public required string ConfirmPassword { get; set; }

        public string? Gender { get; set; }
        public Guid? LibraryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
		public string? Role { get; set; }


        public AppUser ToAppUser()
        {
            return new AppUser
            {
                FullName = FullName,
                Email = Email,
				DateOfBirth = DateOfBirth,
				UserName = Email,
                Gender = Gender,
				LibraryId = LibraryId
			};
        }
    }
}
