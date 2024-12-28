using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AuthEC.Abstractions.Dto.AccountRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using AuthEC.Entities;

namespace AuthEC.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// service method to get account details
		/// </summary>
		/// <param name="userId">Guid type user id</param>
		/// <returns></returns>
		public async Task<AccountDetailsResponse> GetAccountDetails(Guid userId)
		{
			try
			{
				AppUser? foundUser = await _userManager.FindByIdAsync(userId.ToString());
				if (foundUser == null)
				{
					throw new CustomException(HttpStatusCode.NotFound, "User not found");
				}
				AccountDetailsResponse accountDetails = new AccountDetailsResponse
				{
					Email = foundUser.Email!,
					FullName = foundUser.FullName!,
					Gender = foundUser.Gender,
					Age = (DateTime.Now.Year - foundUser.DateOfBirth!.Value.Year).ToString(),
					LibraryId = foundUser.LibraryId
				};
				return accountDetails;
			}
			catch (Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
