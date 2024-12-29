using AuthEC.Abstractions.Interfaces;
using AuthEC.Utils;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace AuthEC.Services
{
	public class EmailService : IEmailService
	{
		private readonly SmtpSettings _smtpSettings;

		public EmailService(IOptions<SmtpSettings> smtpSettings)
		{
			_smtpSettings = smtpSettings.Value;
		}

		public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
		{
			var email = new MimeMessage();
			email.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
			email.To.Add(MailboxAddress.Parse(to));
			email.Subject = subject;

			if (isHtml)
			{
				var builder = new BodyBuilder { HtmlBody = body };
				email.Body = builder.ToMessageBody();
			}
			else
			{
				email.Body = new TextPart("plain") { Text = body };
			}

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
			await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}
	}
}
