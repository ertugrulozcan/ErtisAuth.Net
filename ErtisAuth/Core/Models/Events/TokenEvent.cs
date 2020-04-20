using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Auth;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Events
{
	public class TokenEvent : EventLog
	{
		#region Properties

		[JsonProperty("document")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public AuthenticationToken Document { get; set; }
		
		[JsonProperty("prior")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public AuthenticationToken Prior { get; set; }

		#endregion
	}
}