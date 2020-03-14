using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ErtisAuth.Queries.MongoQueries
{
	public class QueryGroup : IEnumerable<IQuery>, IQuery
	{
		#region Properties

		public IQuery Value { get; set; }

		#endregion
		
		#region Constructors

		public QueryGroup()
		{
			
		}
		
		public QueryGroup(IEnumerable<IQuery> values)
		{
			var iQueries = values.Where(x => !(x is Query));
			this.queries.AddRange(iQueries);
			
			var queries = this.DistinctQueries(values.Where(x => x is Query).Cast<Query>());
			this.queries.AddRange(queries);
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

		private IEnumerable<IQuery> DistinctQueries(IEnumerable<Query> values)
		{
			List<IQuery> distinctQueries = new List<IQuery>();
			
			var groupedQueries = values.GroupBy(x => x.Key);
			foreach (var groupedQuery in groupedQueries)
			{
				if (groupedQuery.Any())
				{
					if (groupedQuery.Count() == 1)
					{
						distinctQueries.Add(new Query(groupedQuery.Key, groupedQuery.First().Value));
					}
					else
					{
						distinctQueries.Add(new Query(groupedQuery.Key, new QueryGroup(groupedQuery.Select(x => x.Value))));
					}	
				}
			}

			return distinctQueries;
		}

		public static implicit operator QueryGroup(IQuery[] values)
		{
			return new QueryGroup(values);
		}
		
		public static implicit operator QueryGroup(string[] values)
		{
			return new QueryGroup(values.Select(x => (Query)x));
		}
		
		public static implicit operator QueryGroup(QueryParams dict)
		{
			return new QueryGroup(dict.Select(x => new Query() { Key = x.Key, Value = Query.CastToQuery(x.Value) }));
		}
		
		public string ToInnerString()
		{
			return string.Join(", ", this.Select(x => x.ToInnerString()));
		}
		
		public override string ToString()
		{
			return "{" + this.ToInnerString() + "}";
		}

		#endregion
	}
}