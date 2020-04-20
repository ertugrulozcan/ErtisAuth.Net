using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Common
{
	public class UtilizerBase : ResourceBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties

		[JsonIgnore]
		public string Title
		{
			get
			{
				return this.Id;
			}
		}
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string MembershipId { get; set; }

		#endregion
	}
}