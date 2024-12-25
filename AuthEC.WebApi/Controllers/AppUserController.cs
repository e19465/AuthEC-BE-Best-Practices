using AuthEC.Abstractions.Dto.AppUserRelated;
using AuthEC.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthEC.WebApi.Controllers
{
	public class AppUserController : ControllerBase
	{
		private readonly IAppUserService _appUserService;

		public AppUserController(IAppUserService appUserService)
		{
			_appUserService = appUserService;
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
	}
}
