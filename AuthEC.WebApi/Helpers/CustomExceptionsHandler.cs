using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

public static class CustomExceptionsHandler
{
	public static IResult HandleException(Exception ex)
	{
		switch (ex)
		{
			case ArgumentNullException argumentNullException:
				return Results.Problem(new
				{
					Message = "A required parameter is missing.",
					Error = argumentNullException.Message,
					StackTrace = argumentNullException.StackTrace
				}.ToString(), statusCode: (int)HttpStatusCode.BadRequest);

			case ArgumentException argumentException:
				return Results.Problem(new
				{
					Message = "Invalid input data.",
					Error = argumentException.Message,
					StackTrace = argumentException.StackTrace
				}.ToString(), statusCode: (int)HttpStatusCode.BadRequest);

			default:
				return Results.Problem(new
				{
					Message = "An error occurred during registration.",
					Error = ex.Message,
					StackTrace = ex.StackTrace
				}.ToString(), statusCode: (int)HttpStatusCode.InternalServerError);
		}
	}
}
