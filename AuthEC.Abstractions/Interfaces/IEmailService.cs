using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthEC.Abstractions.Interfaces
{
	public interface IEmailService
	{

		/// <summary>
		/// This method sends an email to the user
		/// </summary>
		/// <param name="to">To user</param>
		/// <param name="subject">Subject of the email</param>
		/// <param name="body">body of the email</param>
		/// <param name="isHtml">is HTML or not</param>
		/// <returns></returns>
		public Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
	}
}
