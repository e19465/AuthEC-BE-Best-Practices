using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthEC.Entities;
using AuthEC.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthEC.Services
{
	public class JwtTokenService
	{
		private readonly UserManager<AppUser> _userManager;


		public JwtTokenService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}



		/// <summary>
		/// This method generates an access token for the user
		/// </summary>
		/// <param name="user">AppUser type user</param>
		/// <param name="userRole">User's Role</param>
		/// <param name="accessTokenSecret">Secret Key to generate access token</param>
		/// <returns></returns>
		public string GenerateAccessToken(AppUser user, string userRole, string accessTokenSecret)
		{
			var accessSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));

			ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
			{
				new Claim(JwtClaimTypes.UserId, user.Id),
				new Claim(JwtClaimTypes.Gender, user.Gender!),
				new Claim(JwtClaimTypes.Age, (DateTime.Now.Year - user.DateOfBirth!.Value.Year).ToString()),
				new Claim(JwtClaimTypes.Role, userRole),
				new Claim(JwtClaimTypes.TokenType, "access")
			});

			if (user.LibraryId != null) 
			{ 
				claims.AddClaim(new Claim(JwtClaimTypes.LibraryId, user.LibraryId.ToString()!));
			}

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claims,
				Expires = DateTime.UtcNow.AddMinutes(5), // token expires in 5 minutes
				SigningCredentials = new SigningCredentials(accessSecret, SecurityAlgorithms.HmacSha256Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);
			return token;
		}



		/// <summary>
		/// This method generates a refresh token for the user
		/// </summary>
		/// <param name="user">AppUser type user</param>
		/// <param name="refreshTokenSecret">Secret key to generate refresh token</param>
		/// <returns>JWT access and refresh tokens</returns>
		public string GenerateRefreshToken(AppUser user, string refreshTokenSecret)
		{
			var refreshSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));

			ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
			{
				new Claim(JwtClaimTypes.UserId, user.Id),
				new Claim(JwtClaimTypes.TokenType, "refresh")
			});

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claims,
				Expires = DateTime.UtcNow.AddDays(7), // token expires in 1 week
				SigningCredentials = new SigningCredentials(refreshSecret, SecurityAlgorithms.HmacSha256Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);
			return token;
		}



		/// <summary>
		/// This method validates the refresh token
		/// </summary>
		/// <param name="refreshToken">Refresh token to be validated</param>
		/// <param name="refreshTokenSecret">Secret key to validate token</param>
		/// <returns>AppUser tyoe user if refresh token is valid otherwise null</returns>

		public async Task<AppUser?> ValidateRefreshToken(string refreshToken, string refreshTokenSecret)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var refreshSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));

			var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = refreshSecret,
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken validatedToken);

			var userId = principal.FindFirst("UserId")?.Value;

			if (userId == null)
			{
				return null;
			}

			AppUser? foundUser = await _userManager.FindByIdAsync(userId);
			if (foundUser == null)
			{
				return null;
			}

			return foundUser;
		}
	}
}
