using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Auth
{
	public class HealthCheckResponse
	{
		[JsonProperty("healthcheck")]
		public bool IsHealthy { get; set; }
	}
}