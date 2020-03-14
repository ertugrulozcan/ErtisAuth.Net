using System;
using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public class UserProfile
	{
		#region Properties

		[JsonProperty("area_code")]
		[JsonIgnoreWhenNull]
		public string AreaCode { get; set; }
		
		[JsonProperty("phone")]
		[JsonIgnoreWhenNull]
		public string Phone { get; set; }

		[JsonProperty("birthdate")]
		[JsonIgnoreWhenNull]
		public DateTime? BirthDate { get; set; }
		
		[JsonProperty("address_line_1")]
		[JsonIgnoreWhenNull]
		public string AddressLine1 { get; set; }
		
		[JsonProperty("address_line_2")]
		[JsonIgnoreWhenNull]
		public string AddressLine2 { get; set; }
		
		[JsonProperty("city")]
		[JsonIgnoreWhenNull]
		public string City { get; set; }
		
		[JsonProperty("country")]
		[JsonIgnoreWhenNull]
		public string Country { get; set; }
		
		[JsonProperty("nationality")]
		[JsonIgnoreWhenNull]
		public string Nationality { get; set; }
		
		[JsonProperty("modified_date")]
		[JsonIgnoreWhenNull]
		public DateTime? ModifiedDate { get; set; }
		
		[JsonProperty("modified_by")]
		[JsonIgnoreWhenNull]
		public string ModifiedBy { get; set; }
		
		#endregion
	}
}