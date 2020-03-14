using ErtisAuth.Annotations;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Applications
{
	public sealed class Application : ResourceBase
	{
		#region Properties

		[JsonProperty("name")]
		[JsonIgnoreWhenNull]
		public string Name { get; set; }
		
		[JsonProperty("slug")]
		[JsonIgnoreWhenNull]
		public string Slug { get; set; }
		
		[JsonProperty("secret")]
		[JsonIgnoreWhenNull]
		public string Secret { get; set; }
		
		#endregion
	}
}