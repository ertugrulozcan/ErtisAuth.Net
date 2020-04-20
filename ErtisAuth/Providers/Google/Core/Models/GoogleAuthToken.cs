using Newtonsoft.Json;

namespace ErtisAuth.Providers.Google.Core.Models
{
	public class GoogleAuthToken
	{
		#region Properties

		[JsonProperty("token_type")]
		public string TokenType { get; set; }
		
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		
		[JsonProperty("scope")]
		public string Scope { get; set; }
		
		[JsonProperty("login_hint")]
		public string LoginHint { get; set; }
		
		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
		
		[JsonProperty("id_token")]
		public string IdToken { get; set; }
		
		[JsonProperty("session_state")]
		public GoogleSessionState SessionState { get; set; }

		[JsonProperty("first_issued_at")]
		public long FirstIssuedAtUnixTime { get; set; }
		
		[JsonProperty("expires_at")]
		public long ExpiresAtUnixTime { get; set; }
		
		[JsonProperty("idpId")]
		public string ProvidedBy { get; set; }
		
		#endregion
	}
}