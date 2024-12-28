using System;
using AuthEC.Abstractions.Dto.AccountRelated;

namespace AuthEC.Abstractions.Interfaces
{
	public interface IAccountService
	{
		/// <summary>
		/// This is the method to get account details
		/// </summary>
		/// <param name="userId">Guid type user id of the user</param>
		/// <returns></returns>
		Task<AccountDetailsResponse> GetAccountDetails(Guid userId);
	}
}
