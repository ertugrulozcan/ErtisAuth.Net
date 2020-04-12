using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Applications
{
	public sealed class Application : ResourceBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties
		
		[JsonIgnore]
		public string Title
		{
			get
			{
				return this.Name;
			}
		}

		[JsonProperty("name")]
		[JsonIgnoreWhenNull]
		public string Name { get; set; }
		
		[JsonProperty("slug")]
		[JsonIgnoreWhenNull]
		public string Slug { get; set; }
		
		[JsonProperty("secret")]
		[JsonIgnoreWhenNull]
		public string Secret { get; set; }
		
		[JsonProperty("role")]
		[JsonIgnoreWhenNull]
		public string Role { get; set; }
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string MembershipId { get; set; }
		
		#endregion
	}
}