using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Common;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public class User : ResourceBase
	{
		#region Properties

		[JsonProperty("username")]
		[JsonIgnoreWhenNull]
		public string Username { get; set; }
		
		[JsonProperty("email")]
		[JsonIgnoreWhenNull]
		public string EmailAddress { get; set; }
		
		[JsonProperty("display_name")]
		[JsonIgnoreWhenNull]
		public string DisplayName { get; set; }
		
		[JsonProperty("firstname")]
		[JsonIgnoreWhenNull]
		public string FirstName { get; set; }
		
		[JsonProperty("lastname")]
		[JsonIgnoreWhenNull]
		public string LastName { get; set; }
		
		[JsonProperty("photo_url")]
		[JsonIgnoreWhenNull]
		public string PhotoUrl { get; set; }
		
		[JsonProperty("link")]
		[JsonIgnoreWhenNull]
		public string Link { get; set; }
		
		[JsonProperty("email_verified")]
		[JsonIgnoreWhenNull]
		public bool? EmailVerified { get; set; }
		
		[JsonProperty("providers")]
		[JsonIgnoreWhenNull]
		public ProviderModel[] Providers { get; set; }

		[JsonProperty("role")]
		[JsonIgnoreWhenNull]
		public string Role { get; set; }
		
		[JsonProperty("status")]
		[JsonIgnoreWhenNull]
		public string Status { get; set; }
		
		[JsonProperty("last_refreshed_token")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public ExtendedToken LastRefreshedToken { get; set; }
		
		[JsonProperty("last_generated_token")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public ExtendedToken LastGeneratedToken { get; set; }
		
		[JsonProperty("ip_info")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public IPInformations IPInformation { get; set; }
		
		[JsonProperty("role_permissions")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost(true)]
		public string[] RolePermissions { get; set; }

		[JsonProperty("profile")]
		[JsonIgnoreWhenNull]
		public UserProfile Profile { get; set; }
		
		[JsonIgnore]
		public string FullName
		{
			get
			{
				return $"{this.FirstName} {this.LastName}";
			}
		}
		
		#endregion
	}
}