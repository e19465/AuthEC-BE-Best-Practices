﻿namespace AuthEC.Utils
{
	public class AppSettings
	{
		public required string AccessTokenSecret { get; set; }
		public required string RefreshTokenSecret { get; set; }
	}
}