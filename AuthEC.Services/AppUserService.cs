using System.Net;
using System.Web;
using AuthEC.Abstractions.Dto.AppUserRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Entities;
using AuthEC.Services.Helpers;
using AuthEC.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.Helpers;

namespace AuthEC.Services
{
	/// <summary>
	/// This is the Service class for AppUser
	/// </summary>
	public class AppUserService : IAppUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IEmailService _emailService;
		private readonly IOptions<AppSettings> _appSettings;

		public AppUserService(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService, IEmailService emailService, IOptions<AppSettings> appSettings)
		{
			_userManager = userManager;
			_jwtTokenService = jwtTokenService;
			_emailService = emailService;
			_appSettings = appSettings;
		}
		

		private async Task PrepareAndSendRegisterEmail(AppUser user)
		{
			try
			{
				var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var encodedToken = HttpUtility.UrlEncode(emailToken);
				var confirmationLink = $"{_appSettings.Value.FrontEndUrl}/confirm-email?email={user.Email}&code={encodedToken}";
				string subject = "Confirm Email";
				string to = user.Email!;
				string emailBody = RegisterEmailTemplate.GetRegisterEmailTemplate(user.UserName!, confirmationLink);
				bool isHtml = true;
				await _emailService.SendEmailAsync(to, subject, emailBody, isHtml);
			}
			catch(Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		/// <summary>
		/// This service method adds a new user to the database
		/// </summary>
		/// <param name="userAddRequest">AppUserAddRequest type object</param>
		/// <returns>Task.CompletedTask if successfull</returns>
		/// <exception cref="CustomException">throws CustomException is error occurs</exception>
		public async Task<Task> AddUser(AppUserAddRequest userAddRequest)
		{
			try
			{
				ValidationHelper.ValidateModelBinding(userAddRequest);
				AppUser newUser = userAddRequest.ToAppUser();
				IdentityResult result = await _userManager.CreateAsync(newUser, userAddRequest.Password);
				if(result.Succeeded)
				{
					IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, userAddRequest.Role.ToString()!);
					if (roleResult.Succeeded)
					{
						await PrepareAndSendRegisterEmail(newUser);
						return Task.CompletedTask;
					}
					else
					{
						await _userManager.DeleteAsync(newUser);
						var errorMessage = PrepareErrorMessage.PrepareErrorMessageFromIdentityResult(roleResult);
						throw new CustomException(HttpStatusCode.BadRequest, errorMessage);
					}
				}
				else
				{
					var errorMessage = PrepareErrorMessage.PrepareErrorMessageFromIdentityResult(result);
					// Check if the error is due to a duplicate email
					if (result.Errors.Any(e => e.Code == "DuplicateEmail"))
					{
						throw new CustomException(HttpStatusCode.BadRequest, "Email Already Exists");
					}
					throw new CustomException(HttpStatusCode.BadRequest, errorMessage);
				}
			}
			catch (DbUpdateException dbEx)
			{
				// Inspect inner exception to determine if it relates to a specific unique constraint
				if (dbEx.InnerException is SqlException sqlEx) // For SQL Server
				{
					if (sqlEx.Message.Contains("Cannot insert duplicate key row") &&
				sqlEx.Message.Contains("IX_AspNetUsers_LibraryId")) // Check if message contains 'LibraryId' (if that's part of the error message)
					{
						throw new CustomException(HttpStatusCode.BadRequest, "Library ID Already Exists");
					}
				}
				// If it's another unique constraint violation, handle generically
				throw new CustomException(HttpStatusCode.BadRequest, "A unique constraint violation occurred.");
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
			try
			{
				AppUser? foundUser = await _userManager.FindByEmailAsync(request.Email);
				if (foundUser == null)
				{
					throw new CustomException(HttpStatusCode.NotFound, "Invalid Credentials");
				}
				bool isPasswordValid = await _userManager.CheckPasswordAsync(foundUser, request.Password);
				if (!isPasswordValid)
				{
					throw new CustomException(HttpStatusCode.Unauthorized, "Invalid Credentials");
				}

				bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(foundUser);
				if (!isEmailConfirmed)
				{
					throw new CustomException(HttpStatusCode.BadRequest, "Please confirm your email");
				}

				var roles = _userManager.GetRolesAsync(foundUser);
				string role = roles.Result.First().ToString();
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



		/// <summary>
		/// This service method refreshes the access and refresh tokens
		/// </summary>
		/// <param name="request">RefreshTokenRequest type object</param>
		/// <param name="accessTokenSecret">Secret Key to generate JWT Access Token</param>
		/// <param name="refreshTokenSecret">Secret Key to generate JWT Refresh Token</param>
		/// <returns>JWT access & Refresh tokens</returns>
		/// <exception cref="CustomException">throws CustomException if error occur</exception>
		public async Task<SignInResponse> RefreshTokens(RefreshTokenRequest request, string accessTokenSecret, string refreshTokenSecret)
		{
			try
			{
				AppUser? userFromRefreshToken = await _jwtTokenService.ValidateRefreshToken(request.RefreshToken, refreshTokenSecret);
				if (userFromRefreshToken == null)
				{
					throw new CustomException(HttpStatusCode.Unauthorized, "Invalid refresh token");
				}
				var roles = _userManager.GetRolesAsync(userFromRefreshToken);
				string role = roles.Result.First().ToString();
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




		/// <summary>
		/// This service method resends the verification email
		/// </summary>
		/// <param name="email">Email to be verified</param>
		/// <returns></returns>
		/// <exception cref="CustomException"></exception>
		public async Task<Task> ResendVerificationEmail(SendEmailVerificationRequest request)
		{
			try
			{
				AppUser? user = await _userManager.FindByEmailAsync(request.Email);
				if (user == null)
				{
					throw new CustomException(HttpStatusCode.NotFound, "User not found");
				}
				await PrepareAndSendRegisterEmail(user);
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}




		/// <summary>
		/// This service method verifies the email
		/// </summary>
		/// <param name="request">EmailConfirmRequest type request body</param>
		/// <returns>Task.CompletedTask</returns>
		/// <exception cref="CustomException">throws CustomException if something bad happens</exception>
		public async Task<Task> VerifyEmail(EmailConfirmRequest request)
		{
			try
			{
				string email = request.Email;
				string code = request.Code;
				if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code))
				{
					throw new CustomException(HttpStatusCode.BadRequest, "Invalid Request");
				}
				AppUser? user = await _userManager.FindByEmailAsync(email);
				if (user == null)
				{
					throw new CustomException(HttpStatusCode.NotFound, "User not found");
				}
				IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
				if (result.Succeeded)
				{
					return Task.CompletedTask;
				}
				else
				{
					var errorMessage = PrepareErrorMessage.PrepareErrorMessageFromIdentityResult(result);
					throw new CustomException(HttpStatusCode.BadRequest, errorMessage);
				}
			}
			catch(Exception ex)
			{
				throw new CustomException(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
