using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Common
{
	public class IPInformations
	{
		#region Properties

		[JsonProperty("city")]
		public string City { get; set; }
		
		[JsonProperty("country")]
		public string Country { get; set; }
		
		[JsonProperty("iso_code")]
		public string IsoCode { get; set; }
		
		[JsonProperty("ip_address")]
		public string IpAddress { get; set; }

		#endregion
	}
}