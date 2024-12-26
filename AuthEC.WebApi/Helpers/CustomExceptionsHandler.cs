using System;
using System.Net;
using AuthEC.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

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
