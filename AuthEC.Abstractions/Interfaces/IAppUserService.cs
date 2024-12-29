using System;
using AuthEC.Abstractions.Dto.AppUserRelated;

namespace AuthEC.Abstractions.Interfaces
{
	/// <summary>
	/// This is the interface for AppUserService, all the methods related to AppUser will be declared here
	/// </summary>
	public interface IAppUserService
	{
		/// <summary>
		/// This is the method to add a new user
		/// </summary>
		/// <param name="request">AppUserAddRequest type request body</param>
		/// <returns>Success, or bad responses</returns>
		Task<Task> AddUser(AppUserAddRequest request);

		/// <summary>
		/// This is the method to sign in a user
		/// </summary>
		/// <param name="request">Request body containing email and password</param>
		/// <returns>JWT access and refresh toknes</returns>
		Task<SignInResponse> SignInUser(SignInRequest request, string accessTokenSecret, string refreshTokenSecret);

		/// <summary>
		/// This is the method to refresh tokens
		/// </summary>
		/// <param name="request">RefreshTokenRequest type request body</param>
		/// <param name="accessTokenSecret">Secret to create access token</param>
		/// <param name="refreshTokenSecret">Secret to create refresh token</param>
		/// <returns>JWT access and refresh toknes</returns>
		Task<SignInResponse> RefreshTokens(RefreshTokenRequest request, string accessTokenSecret, string refreshTokenSecret);



		/// <summary>
		/// This is the method to verify the email
		/// </summary>
		/// <param name="email">Email for sending verification email</param>
		/// <returns></returns>
		Task<Task> ResendVerificationEmail(SendEmailVerificationRequest request);



		/// <summary>
		/// This is the method to verify the email
		/// </summary>
		/// <param name="request">EmailConfirmRequest type request body</param>
		/// <returns></returns>
		Task<Task> VerifyEmail(EmailConfirmRequest request);

	}
}
