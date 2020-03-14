using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Common;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models
{
	public abstract class ResourceBase
	{
		#region Properties

		[JsonProperty("_id")]
		[JsonIgnoreWhenPost(true)]
		[JsonIgnoreWhenNull]
		public string Id { get; set; }
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public string MembershipId { get; set; }
		
		[JsonProperty("sys")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public SysModel Sys { get; set; }

		#endregion
	}
}