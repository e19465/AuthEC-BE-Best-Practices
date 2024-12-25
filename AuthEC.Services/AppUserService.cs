using System;
using System.Net;
using AuthEC.Abstractions.Dto.AppUserRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.DataAccess.Repository.IRepository;
using AuthEC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Helpers;

namespace AuthEC.Services
{
	public class AppUserService : IAppUserService
	{
		private readonly UserManager<AppUser> _userManager;

		public AppUserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Task> AddUser(AppUserAddRequest userAddRequest)
		{
			try
			{
				ValidationHelper.ValidateModelBinding(userAddRequest);
				AppUser newUser = userAddRequest.ToAppUser();
				var result = await _userManager.CreateAsync(newUser, userAddRequest.Password);
				if(result.Succeeded)
				{
					return Task.CompletedTask;
				}
				else
				{
					return Task.FromException(new Exception("User creation failed"));
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
