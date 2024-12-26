using System.Net;
using AuthEC.Abstractions.Dto.AppUserRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Entities;
using AuthEC.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Services.Helpers;

namespace AuthEC.Services
{
	/// <summary>
	/// This is the Service class for AppUser
	/// </summary>
	public class AppUserService : IAppUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly JwtTokenService _jwtTokenService;

		public AppUserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
			_jwtTokenService = new JwtTokenService(userManager);
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
					var errorMessage = string.Join("; ", result.Errors.Select(result => result.Description));
					throw new CustomException(HttpStatusCode.BadRequest, errorMessage);
				}
			}
			catch (CustomException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		/// <summary>
		/// App User sign in method
		/// </summary>
		/// <param name="request">request containing Email and Password</param>
		/// <returns>Object containing access and refresh tokens</returns>
		public async Task<SignInResponse> SignInUser(SignInRequest request, string accessTokenSecret, string refreshTokenSecret)
		{
			string role = "Student";
			try
			{
				AppUser? foundUser = await _userManager.FindByEmailAsync(request.Email);
				if (foundUser == null)
				{
					throw new CustomException(HttpStatusCode.NotFound, "User not found");
				}
				string accessToken = _jwtTokenService.GenerateAccessToken(foundUser, role, accessTokenSecret);
				string refreshToken = _jwtTokenService.GenerateRefreshToken(foundUser, refreshTokenSecret);
				return new SignInResponse
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken
				};
			}
			catch (CustomException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		public async Task<SignInResponse> RefreshTokens(RefreshTokenRequest request, string accessTokenSecret, string refreshTokenSecret)
		{
			try
			{
				AppUser? userFromRefreshToken = await _jwtTokenService.ValidateRefreshToken(request.RefreshToken, refreshTokenSecret);
				if (userFromRefreshToken == null)
				{
					throw new CustomException(HttpStatusCode.Unauthorized, "Invalid refresh token");
				}
				string role = "Student";
				string accessToken = _jwtTokenService.GenerateAccessToken(userFromRefreshToken, role, accessTokenSecret);
				string refreshToken = _jwtTokenService.GenerateRefreshToken(userFromRefreshToken, refreshTokenSecret);

				return new SignInResponse
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken
				};
			}
			catch (CustomException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
