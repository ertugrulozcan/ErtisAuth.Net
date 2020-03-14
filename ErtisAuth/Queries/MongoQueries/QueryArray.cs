using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ErtisAuth.Queries.MongoQueries
{
	public class QueryArray : IEnumerable<IQuery>, IQuery
	{
		#region Properties

		public IQuery Value { get; set; }

		#endregion
		
		#region Constructors

		public QueryArray()
		{
			
		}
		
		public QueryArray(IEnumerable<IQuery> values)
		{
			this.queries.AddRange(values);
		}

		#endregion
		
		#region IEnumerable Implementation

		private readonly List<IQuery> queries = new List<IQuery>();

		public void Add(IQuery href)
		{
			this.queries.Add(href);
		}

		public void AddRange(IEnumerable<IQuery> hrefs)
		{
			this.queries.AddRange(hrefs);
		}

		public IEnumerator<IQuery> GetEnumerator()
		{
			return this.queries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.queries).GetEnumerator();
		}

		#endregion

		#region Methods

		public static implicit operator QueryArray(IQuery[] values)
		{
			return new QueryArray(values);
		}
		
		public static implicit operator QueryArray(string[] values)
		{
			return new QueryArray(values.Select(x => (Query)x));
		}
		
		public string ToInnerString()
		{
			return this.ToString();
		}
		
		public override string ToString()
		{
			return "[" + string.Join(", ", this.Select(x => x.ToString())) + "]";
		}

		#endregion
	}
}