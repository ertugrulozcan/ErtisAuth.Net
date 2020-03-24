using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Common;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public abstract class UserBase : ResourceBase
	{
		#region Properties

		[JsonProperty("username")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPut]
		public string Username { get; set; }
		
		[JsonProperty("email")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPut]
		public string EmailAddress { get; set; }
		
		[JsonProperty("firstname")]
		[JsonIgnoreWhenNull]
		public string FirstName { get; set; }
		
		[JsonProperty("lastname")]
		[JsonIgnoreWhenNull]
		public string LastName { get; set; }
		
		[JsonProperty("email_verified")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPut]
		public bool? EmailVerified { get; set; }
		
		[JsonProperty("providers")]
		[JsonIgnoreWhenNull]
		public ProviderModel[] Providers { get; set; }

		[JsonProperty("role")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPut]
		public string Role { get; set; }
		
		[JsonProperty("status")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPut]
		public string Status { get; set; }
		
		[JsonProperty("ip_info")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public IPInformations IPInformation { get; set; }
		
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