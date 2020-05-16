using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class ResetPasswordToken
	{
		#region Properties

		[JsonProperty("reset_token")]
		public string ResetToken { get; set; }
		
		[JsonProperty("expires_in")]
		public long ExpiresIn { get; set; }
		
		[JsonProperty("expire_date")]
		public long ExpireUnixTime { get; set; }
		
		#endregion
	}
}