using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Common
{
	public class ErrorModel
	{
		#region Properties

		[JsonProperty("err_code")]
		public string ErrorCode { get; set; }
		
		[JsonProperty("err_msg")]
		public string ErrorMessage { get; set; }
		
		[JsonProperty("context")]
		public ErrorContext Context { get; set; }
		
		[JsonProperty("reason")]
		public string Reason { get; set; }

		#endregion
	}

	public class ErrorContext
	{
		#region Properties

		[JsonProperty("message")]
		public string Message { get; set; }

		#endregion
	}
}