using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models
{
	public class MembershipModel : ResourceBase
	{
		#region Properties
		
		[JsonProperty("name")]
		[JsonIgnoreWhenNull]
		public string Name { get; set; }
		
		[JsonProperty("slug")]
		[JsonIgnoreWhenNull]
		public string Slug { get; set; }
		
		[JsonProperty("token_ttl")]
		[JsonIgnoreWhenNull]
		public int TokenTTL { get; set; }
		
		[JsonProperty("refresh_token_ttl")]
		[JsonIgnoreWhenNull]
		public int RefreshTokenTTL { get; set; }
		
		#endregion
	}
}