using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Users;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class RegistrationForm : User
	{
		#region Properties

		[JsonIgnoreWhenNull]
		[JsonProperty("password")]
		public string Password { get; set; }

		#endregion
	}
}