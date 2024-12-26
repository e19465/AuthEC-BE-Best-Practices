using System.Net;

namespace AuthEC.Services.Helpers
{
	public class CustomException : Exception
	{
		public HttpStatusCode StatusCode { get; }

		public CustomException(HttpStatusCode statusCode, string message) : base(message)
		{
			StatusCode = statusCode;
		}
	}
}
