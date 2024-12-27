using AuthEC.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthEC.WebApi.Controllers
{
    [ApiController]
	[Route("api/authorization")]
	public class AuthorizationExampleController : ControllerBase
	{

		/// <summary>
		/// This method is only accessible to Admin users
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin-only")]
		[Authorize(Roles = AppUserRoles.Admin)]
		public Task<IResult> AdminOnly()
		{
			try
			{
				//var claims = HttpContext.User.Claims.ToList(); // How to access the claims
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}



		/// <summary>
		/// This method is only accessible to Teacher users
		/// </summary>
		/// <returns></returns>
		[HttpGet("teacher-only")]
		[Authorize(Roles = AppUserRoles.Teacher)]
		public Task<IResult> TeacherOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}



		/// <summary>
		/// This method is only accessible to Student users
		/// </summary>
		/// <returns></returns>
		[HttpGet("student-only")]
		[Authorize(Roles = AppUserRoles.Student)]
		public Task<IResult> StudentOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}


		/// <summary>
		/// This method is only accessible to Admin and Teacher users
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin-and-teacher-only")]
		[Authorize(Roles = AppUserRoles.Admin + "," + AppUserRoles.Teacher)]
		public Task<IResult> AdminAndTeacherOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}


		/// <summary>
		/// This method is only accessible to users with the HasLibraryId policy
		/// </summary>
		/// <returns></returns>
		[HttpGet("library-members-only")]
		[Authorize(Policy = CustomPolicyNames.HasLibraryId)]
		public Task<IResult> LibraryMembersOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}



		/// <summary>
		/// This method is only accessible to Female Teacher users
		/// </summary>
		/// <returns></returns>
		[HttpGet("apply-maternity-leave-female-teachers-only")]
		[Authorize(Roles = AppUserRoles.Teacher, Policy = CustomPolicyNames.FemalesOnly)]
		public Task<IResult> ApplyMaternityLeaveOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}




		/// <summary>
		/// This method is accessible to Age under 10, Female users only
		/// </summary>
		/// <returns></returns>
		[HttpGet("age-under-ten-female-only")]
		[Authorize(Policy = CustomPolicyNames.AgeUnderTenOnly)]
		[Authorize(Policy = CustomPolicyNames.FemalesOnly)]
		public Task<IResult> AgeUnderTenFemaleOnly()
		{
			try
			{
				return Task.FromResult(Results.Ok(new { Message = "You are authorized to view this data" }));
			}
			catch (Exception ex)
			{
				return Task.FromResult(CustomExceptionsHandler.HandleException(ex));
			}
		}
	}
}
