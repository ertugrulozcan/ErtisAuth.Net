using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Response
{
	public sealed class CollectionResponse<T>
	{
		#region Properties
		
		[JsonProperty("data")]
		public CollectionResponseData<T> Data { get; set; }

		#endregion
	}
	
	public sealed class CollectionResponseData<T>
	{
		#region Properties

		[JsonProperty("items")]
		public IEnumerable<T> Items { get; set; }
		
		[JsonProperty("count")]
		public int Count { get; set; }

		#endregion
	}
}