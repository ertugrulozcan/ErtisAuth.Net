using System;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class ExtendedToken : AuthenticationToken
	{
		[JsonProperty("created_at")]
		public DateTime? CreatedAt { get; set; }
		
		[JsonProperty("access_token_status")]
		public string AccessTokenStatus { get; set; }
		
		[JsonProperty("refresh_token_status")]
		public string RefreshTokenStatus { get; set; }
	}
}