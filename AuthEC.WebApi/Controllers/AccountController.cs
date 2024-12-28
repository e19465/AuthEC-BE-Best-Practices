using AuthEC.Abstractions.Dto.AccountRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AuthEC.WebApi.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		public readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}



		/// <summary>
		/// This is the controller end point to get account details
		/// </summary>
		/// <returns>AccountDetailsResponse type response</returns>
		[HttpGet("details")]
		public async Task<IResult> GetAccoiuntDetails()
		{
			try
			{
				var claims = HttpContext.User.Claims.ToList();
				string? userId = claims.First(x => x.Type == JwtClaimTypes.UserId).Value.ToString();
				if(userId == null)
				{
					return Results.BadRequest("User not found");
				}
				AccountDetailsResponse accountDetails = await _accountService.GetAccountDetails(Guid.Parse(userId));
				return Results.Ok(accountDetails);
			}
			catch(Exception ex)
			{
				return CustomExceptionsHandler.HandleException(ex);
			}
		}
	}
}
