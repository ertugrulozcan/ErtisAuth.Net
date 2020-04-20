using Newtonsoft.Json;

namespace ErtisAuth.Providers.Google.Core.Models
{
	public class GoogleUserModel
	{
		#region Properties

		[JsonProperty("MU")]
		public string Id { get; set; }
		
		[JsonProperty("Ad")]
		public string DisplayName { get; set; }
		
		[JsonProperty("pW")]
		public string FirstName { get; set; }
		
		[JsonProperty("qU")]
		public string LastName { get; set; }
		
		[JsonProperty("QK")]
		public string ProfileImageUrl { get; set; }
		
		[JsonProperty("yu")]
		public string EmailAddress { get; set; }

		#endregion
	}
}