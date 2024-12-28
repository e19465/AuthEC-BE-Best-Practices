namespace AuthEC.Utils
{
	public static class RegisterEmailTemplate
	{
		public static string GetRegisterEmailTemplate(string userName, string confirmationLink)
		{
			return $@"
					<html>
					<head>
						<style>
							.button {{
								display: inline-block;
								padding: 10px 20px;
								font-size: 16px;
								color: #ffffff !important;
								background-color: #00008B !important;
								text-decoration: none;
								border-radius: 5px;
							}}
							.button:hover {{
								background-color: #0056b3;
							}}
						</style>
					</head>
					<body>
						<p>Dear {userName},</p>
						<p>Thank you for registering. Please confirm your email by clicking the button below:</p>
						<p><a href='{confirmationLink}' class='button'>Confirm Email</a></p>
						<p>If the button doesn't work, please copy and paste the following link into your browser:</p>
						<p><a href='{confirmationLink}'>{confirmationLink}</a></p>
						<p>Best regards,<br/>AuthEC</p>
					</body>
					</html>
					";
		}
	}
}
