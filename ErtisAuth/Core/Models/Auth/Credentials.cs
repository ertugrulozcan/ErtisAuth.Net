using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public sealed class Credentials
	{
		[JsonIgnore]
		public string Username { get; set; }
		
		[JsonIgnore]
		public string Password { get; set; }
	}
}