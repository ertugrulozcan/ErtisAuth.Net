using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Roles
{
	public sealed class Role : ResourceBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties

		[JsonProperty("name")]
		[JsonIgnoreWhenNull]
		public string Name { get; set; }
		
		[JsonProperty("permissions")]
		[JsonIgnoreWhenNull]
		public string[] Permissions { get; set; }
		
		[JsonProperty("slug")]
		[JsonIgnoreWhenNull]
		public string Slug { get; set; }
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string MembershipId { get; set; }
		
		[JsonIgnore]
		public string Title
		{
			get
			{
				return this.Name;
			}
		}

		#endregion
	}
}