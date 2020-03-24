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
		
		#endregion
	}
}