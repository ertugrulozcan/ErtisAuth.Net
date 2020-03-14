namespace ErtisAuth.Queries.MongoQueries
{
	public class RawQuery : IQuery
	{
		#region Properties
		
		private string RawJson { get; }

		public IQuery Value
		{
			get
			{
				Query query = this.RawJson;
				return query;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="json"></param>
		public RawQuery(string json)
		{
			this.RawJson = json;
		}
		
		#endregion
		
		#region Methods
		
		public string ToInnerString()
		{
			return this.ToString();
		}

		public override string ToString()
		{
			return this.RawJson;
		}

		#endregion
	}
}