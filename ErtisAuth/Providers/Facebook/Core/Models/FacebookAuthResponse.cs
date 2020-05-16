using Newtonsoft.Json;

namespace ErtisAuth.Providers.Facebook.Core.Models
{
	public class FacebookAuthResponse
	{
		#region Properties

		[JsonProperty("accessToken")]
		public string AccessToken { get; set; }
		
		[JsonProperty("userID")]
		public string UserID { get; set; }
		
		[JsonProperty("expiresIn")]
		public int ExpiresIn { get; set; }

		[JsonProperty("signedRequest")]
		public string SignedRequest { get; set; }
		
		[JsonProperty("graphDomain")]
		public string GraphDomain { get; set; }
		
		[JsonProperty("data_access_expiration_time")]
		public long DataAccessExpirationUnixTimeStamp { get; set; }
		
		#endregion
	}
}