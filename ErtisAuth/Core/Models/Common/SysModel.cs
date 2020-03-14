using System;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Common
{
	public class SysModel
	{
		#region Properties

		[JsonProperty("created_at")]
		public DateTime? CreatedAt { get; set; }
		
		[JsonProperty("created_by")]
		public string CreatedBy { get; set; }
		
		[JsonProperty("modified_at")]
		public DateTime? ModifiedAt { get; set; }
		
		[JsonProperty("modified_by")]
		public string ModifiedBy { get; set; }

		#endregion
	}
}