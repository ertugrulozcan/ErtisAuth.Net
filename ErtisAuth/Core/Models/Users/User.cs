using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public class User : UserBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties

		[JsonIgnore]
		public string Title
		{
			get
			{
				return this.FullName;
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