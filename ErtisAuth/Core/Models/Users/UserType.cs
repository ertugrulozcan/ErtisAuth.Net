using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public class UserType : ResourceBase, IMembershipBoundedResource, IHasTitle
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
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string Name { get; set; }
		
		[JsonProperty("description")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string Description { get; set; }
		
		[JsonProperty("slug")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string Slug { get; set; }
		
		[JsonProperty("scheme")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public object Scheme { get; set; }
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string MembershipId { get; set; }
		
		#endregion
	}
}