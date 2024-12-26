using System.Net;
using AuthEC.Abstractions.Dto.AppUserRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Services.Helpers;
using AuthEC.WebApi.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthEC.WebApi.Controllers
{
	public class AppUserController : ControllerBase
	{
		private readonly IAppUserService _appUserService;
		private readonly IOptions<AppSettings> _appSettings;

		public AppUserController(IAppUserService appUserService, IOptions<AppSettings> appSettings)
		{
			_appUserService = appUserService;
			_appSettings = appSettings;
		}

		/// <summary>
		/// Controller end pont to add new user to DB
		/// </summary>
		/// <param name="request">AppUserAddRequest typre request body</param>
		/// <returns>HttpResponse</returns>
		[HttpPost]
		[Route("api/user/sign-up")]
		[AllowAnonymous]
		public async Task<IResult> RegisterUser(AppUserAddRequest request)
		{
			try
			{
				await _appUserService.AddUser(request);
				return Results.Ok(new { Message = "Registration Successfull"});
			}
			catch (Exception ex)
			{
				return CustomExceptionsHandler.HandleException(ex);
			}
		}



		/// <summary>
		/// Controller end point to sign in a user
		/// </summary>
		/// <param name="request">SignInRequest type request body</param>
		/// <returns>JWT access and refresh tokens</returns>
		[HttpPost]
		[Route("api/user/sign-in")]
		[AllowAnonymous]
		public async Task<IResult> LoginUser(SignInRequest request)
		{
			try
			{
				string accessTokenSecret = _appSettings.Value.AccessTokenSecret;
				string refreshTokenSecret = _appSettings.Value.RefreshTokenSecret;
				var response = await _appUserService.SignInUser(request, accessTokenSecret, refreshTokenSecret);
				return Results.Ok(response);
			}
			catch (Exception ex)
			{
				return CustomExceptionsHandler.HandleException(ex);
			}
		}



		/// <summary>
		/// Controller end point to refresh tokens
		/// </summary>
		/// <param name="request">RefreshTokenRequest type request body</param>
		/// <returns>JWT access and refresh tokens</returns>
		[HttpPost]
		[Route("api/user/refresh-jwt")]
		[AllowAnonymous]
		public async Task<IResult> RefreshTokens(RefreshTokenRequest request)
		{
			try
			{
				string accessTokenSecret = _appSettings.Value.AccessTokenSecret;
				string refreshTokenSecret = _appSettings.Value.RefreshTokenSecret;
				var response = await _appUserService.RefreshTokens(request, accessTokenSecret, refreshTokenSecret);
				return Results.Ok(response);
			}
			catch (Exception ex)
			{
				return CustomExceptionsHandler.HandleException(ex);
			}
		}
	}
}
