using System;
using AuthEC.Abstractions.Dto.AppUserRelated;

namespace AuthEC.Abstractions.Interfaces
{
	public interface IAppUserService
	{
		/// <summary>
		/// This is the method to add a new user
		/// </summary>
		/// <param name="request">AppUserAddRequest type request body</param>
		/// <returns>Success, or bad responses</returns>
		Task<Task> AddUser(AppUserAddRequest request);
	}
}
