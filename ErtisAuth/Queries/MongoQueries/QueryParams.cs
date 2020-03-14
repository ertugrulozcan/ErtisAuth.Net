using System.Collections.Generic;
using System.Linq;

namespace ErtisAuth.Queries.MongoQueries
{
	public class QueryParams : Dictionary<string, object>
	{
		public QueryParams()
		{ }
		
		public QueryParams(IDictionary<IQuery, string> dictionary) : 
			base(dictionary.ToDictionary(x => (x.Key as Query).ValueAsVariable.ToString(), y => y.Value as object))
		{ }
		
		public QueryParams(IDictionary<IQuery, int> dictionary) : 
			base(dictionary.ToDictionary(x => (x.Key as Query).ValueAsVariable.ToString(), y => y.Value as object))
		{ }
		
		public QueryParams(IDictionary<IQuery, bool> dictionary) : 
			base(dictionary.ToDictionary(x => (x.Key as Query).ValueAsVariable.ToString(), y => y.Value as object))
		{ }
		
		public QueryParams(IDictionary<IQuery, object> dictionary) : 
			base(dictionary.ToDictionary(x => (x.Key as Query).ValueAsVariable.ToString(), y => y.Value))
		{ }
	}
}