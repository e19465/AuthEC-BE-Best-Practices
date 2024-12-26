using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthEC.Abstractions.Dto.AppUserRelated
{
	public class SignInResponse
	{
		public required string AccessToken { get; set; }
		public required string RefreshToken { get; set; }
	}
}
