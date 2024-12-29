using AuthEC.Entities;

namespace AuthEC.Abstractions.Interfaces
{
	public interface IJwtTokenService
	{
		/// <summary>
		/// This method generates an access token for the user
		/// </summary>
		/// <param name="user">AppUser type user</param>
		/// <param name="userRole">Role of the user</param>
		/// <param name="accessTokenSecret">Secret key to create access token</param>
		/// <returns>New access token</returns>
		string GenerateAccessToken(AppUser user, string userRole, string accessTokenSecret);


		/// <summary>
		/// This method generates a refresh token for the user
		/// </summary>
		/// <param name="user">AppUser type user</param>
		/// <param name="refreshTokenSecret">Secret key to create refresh token</param>
		/// <returns>New Refresh token</returns>
		string GenerateRefreshToken(AppUser user, string refreshTokenSecret);


		/// <summary>
		/// This method validates the refresh token
		/// </summary>
		/// <param name="refreshToken">Refresh token to valaidate</param>
		/// <param name="refreshTokenSecret">Secret key used to create refresh token</param>
		/// <returns>AppUser type user or null</returns>
		Task<AppUser?> ValidateRefreshToken(string refreshToken, string refreshTokenSecret);
	}
}
