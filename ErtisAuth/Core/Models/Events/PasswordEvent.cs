using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Events
{
	public class PasswordEvent : EventLog
	{
		#region Properties

		[JsonProperty("document")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public object Document { get; set; }
		
		[JsonProperty("prior")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public object Prior { get; set; }

		#endregion
	}
}