using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthEC.Utils
{
	public class SmtpSettings
	{
		public required string Server { get; set; }
		public int Port { get; set; }
		public required string SenderName { get; set; }
		public required string SenderEmail { get; set; }
		public required string Username { get; set; }
		public required string Password { get; set; }
	}
}
