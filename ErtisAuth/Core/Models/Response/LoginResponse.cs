using ErtisAuth.Core.Models.Auth;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Response
{
	public class LoginResponse
	{
		#region Properties

		[JsonProperty("token")]
		public AuthenticationToken Token { get; set; }
		
		[JsonProperty("user")]
		public Me User { get; set; }

		#endregion
	}
}