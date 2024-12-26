using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthEC.Abstractions.Dto;
using AuthEC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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

		public string GenerateAccessToken(AppUser user, string userRole, string accessTokenSecret)
		{
			var accessSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));

			ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
			{
				new Claim("UserId", user.Id),
				new Claim("Gender", user.Gender!),
				new Claim("Age", (DateTime.Now.Year - user.DateOfBirth!.Value.Year).ToString()),
				new Claim("Role", userRole)
			});

			if (user.LibraryId != null) 
			{ 
				claims.AddClaim(new Claim("LibraryId", user.LibraryId.ToString()!));
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


		public string GenerateRefreshToken(AppUser user, string refreshTokenSecret)
		{
			var refreshSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));

			ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
			{
				new Claim("UserId", user.Id)
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
