using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Users;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class Me : UserBase
	{
		#region Properties
		
		[JsonProperty("membership")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public MembershipModel Membership { get; set; }
		
		[JsonProperty("role_permissions")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string[] RolePermissions { get; set; }
		
		[JsonProperty("token")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public ExtendedToken Token { get; set; }
		
		#endregion
	}
}