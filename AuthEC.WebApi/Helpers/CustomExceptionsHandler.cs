using System.Net;
using AuthEC.Services.Helpers;

public static class CustomExceptionsHandler
{
	public static IResult HandleException(Exception ex)
	{
		if (ex is CustomException customEx)
		{
			return Results.Problem(
				detail: customEx.Message,
				statusCode: (int)customEx.StatusCode
			);
		}
		else
		{
			return Results.Problem(
				detail: ex.Message,
				statusCode: (int)HttpStatusCode.InternalServerError
			);
		}
	}
}
