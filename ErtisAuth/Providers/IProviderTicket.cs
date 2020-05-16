using Newtonsoft.Json;

namespace ErtisAuth.Providers
{
	public interface IProviderTicket
	{
		string UserId { get; }
		
		string AccessToken { get; }
		
		string RevokeToken { get; }
	}
	
	public class ProviderTicket : IProviderTicket
	{
		[JsonProperty("user_id")]
		public string UserId { get; set; }
		
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		
		[JsonProperty("revoke_token")]
		public string RevokeToken { get; set; }
	}
}