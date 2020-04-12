using System;
using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Users
{
	public class UserProfile
	{
		#region Properties

		[JsonProperty("photo_url")]
		[JsonIgnoreWhenNull]
		public string PhotoUrl { get; set; }
		
		[JsonProperty("phone")]
		[JsonIgnoreWhenNull]
		public string Phone { get; set; }

		[JsonProperty("birthdate")]
		[JsonIgnoreWhenNull]
		public DateTime? BirthDate { get; set; }
		
		#endregion
	}
}