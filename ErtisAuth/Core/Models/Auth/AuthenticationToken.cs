using System;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class AuthenticationToken
	{
		#region Properties

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		
		[JsonProperty("expires_in")]
		public long ExpiresIn { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
		
		[JsonProperty("refresh_token_expires_in")]
		public long RefreshTokenExpiresIn { get; set; }
		
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		#endregion
	}
}