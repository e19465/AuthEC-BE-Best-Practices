using AuthEC.Abstractions.Dto.AccountRelated;
using AuthEC.Abstractions.Interfaces;
using AuthEC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthEC.WebApi.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IEmailService _emailService;

		public AccountController(IAccountService accountService, IEmailService emailService)
		{
			_accountService = accountService;
			_emailService = emailService;
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

		[AllowAnonymous]
		[HttpGet("send-welcome-email")]
		public async Task<IResult> SendWelcomeEmail()
		{
			string userEmail = "sasindudil0002@gmail.com";
			var subject = "Welcome to Our Service";
			var body = "<h1>Thank you for registering!</h1>";
			await _emailService.SendEmailAsync(userEmail, subject, body, isHtml: true);
			return Results.Ok(new { Message = "Email sent successfully" });
		}
	}
}
