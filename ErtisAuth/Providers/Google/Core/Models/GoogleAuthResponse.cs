using Newtonsoft.Json;

namespace ErtisAuth.Providers.Google.Core.Models
{
	public class GoogleAuthResponse
	{
		#region Properties

		[JsonProperty("Ca")]
		public string UserId { get; set; }
		
		[JsonProperty("tc")]
		public GoogleAuthToken Token { get; set; }
		
		[JsonProperty("Pt")]
		public GoogleUserModel User { get; set; }

		#endregion
	}
}