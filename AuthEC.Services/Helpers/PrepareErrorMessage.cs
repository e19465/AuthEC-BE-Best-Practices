using Microsoft.AspNetCore.Identity;

namespace AuthEC.Services.Helpers
{
	public static class PrepareErrorMessage
	{
		public static string PrepareErrorMessageFromIdentityResult(IdentityResult result)
		{
			return string.Join("; ", result.Errors.Select(e => e.Description));
		}
	}
}
