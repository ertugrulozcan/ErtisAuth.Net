using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ErtisAuth.Queries.MongoQueries
{
	public static class QueryBuilder
	{
		#region Properties

		private static List<string> mongoOperators;

		private static List<string> MongoOperators
		{
			get
			{
				if (mongoOperators == null)
					mongoOperators = new List<string>(GetMongoOperators());

				return mongoOperators;
			}
		}

		#endregion
		
		#region Collection Queries

		public static IQuery Where(string key, Query value)
		{
			return Where(key, value as IQuery);
		}
		
		public static IQuery Where(string key, IQuery value)
		{
			return new Query()
			{
				Key = "where",
				Value = new Query()
				{
					Key = key,
					Value = value
				}
			};
		}
		
		public static IQuery Where(IQuery conditions)
		{
			return new Query()
			{
				Key = "where",
				Value = conditions
			};
		}
		
		public static IQuery Where(QueryGroup conditions)
		{
			return new Query()
			{
				Key = "where",
				Value = conditions
			};
		}
		
		/*
		public static IQuery Where(IEnumerable<KeyValuePair<string, object>> conditions)
		{
			QueryGroup conditionsQueryGroup = new QueryGroup(conditions.Select(x => new Query(x.Key, Query.CastToQuery(x.Value))));
			return new Query()
			{
				Key = "where",
				Value = conditionsQueryGroup
			};
		}
		*/
		
		public static IQuery Select(QueryArray values)
		{
			Query trueQuery = 1;
			return new Query()
			{
				Key = "select",
				Value = new QueryGroup(values.Select(x => new Query((x as Query).ValueAsVariable.ToString(), trueQuery)))
			};
		}
		
		public static IQuery Exclude(QueryArray values)
		{
			QueryGroup queryGroup = new QueryParams(values.ToDictionary(x => x, y => 0));
			return new Query()
			{
				Key = "select",
				Value = queryGroup
			};
		}

		#endregion
		
		#region Comparison Queries

		[MongoOperator("$eq")]
		public static IQuery Equal(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$eq",
					Value = value
				}
			};
		}
		
		[MongoOperator("$ne")]
		public static IQuery NotEqual(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$ne",
					Value = value
				}
			};
		}
		
		[MongoOperator("$gt")]
		public static IQuery GreaterThan(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$gt",
					Value = value
				}
			};
		}
		
		[MongoOperator("$gte")]
		public static IQuery GreaterThanOrEqual(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$gte",
					Value = value
				}
			};
		}
		
		[MongoOperator("$gte")]
		public static IQuery GreaterThanOrEqual(Query value)
		{
			return new Query()
			{
				Key = "$gte",
				Value = value
			};
		}
		
		[MongoOperator("$lt")]
		public static IQuery LessThan(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$lt",
					Value = value
				}
			};
		}
		
		[MongoOperator("$lt")]
		public static IQuery LessThan(Query value)
		{
			return new Query()
			{
				Key = "$lt",
				Value = value
			};
		}
		
		[MongoOperator("$lte")]
		public static IQuery LessThanOrEqual(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$lte",
					Value = value
				}
			};
		}
		
		[MongoOperator("$lte")]
		public static IQuery LessThanOrEqual(Query value)
		{
			return new Query()
			{
				Key = "$lte",
				Value = value
			};
		}
		
		[MongoOperator("$in")]
		public static IQuery Contains(string key, QueryArray array)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$in",
					Value = array
				}
			};
		}
		
		[MongoOperator("$nin")]
		public static IQuery NotContains(string key, QueryArray array)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$nin",
					Value = array
				}
			};
		}
		
		[MongoOperator("$exists")]
		public static IQuery Exists(string key, Query query)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$exists",
					Value = query
				}
			};
		}

		#endregion
		
		#region Logical Queries

		[MongoOperator("$and")]
		public static IQuery And(QueryArray array)
		{
			return new Query()
			{
				Key = "$and",
				Value = array
			};
		}
		
		[MongoOperator("$and")]
		public static IQuery And(IQuery query1, IQuery query2)
		{
			return new Query()
			{
				Key = "$and",
				Value = new QueryArray(new[] { query1, query2 })
			};
		}
		
		[MongoOperator("$or")]
		public static IQuery Or(QueryArray array)
		{
			return new Query()
			{
				Key = "$or",
				Value = array
			};
		}
		
		[MongoOperator("$or")]
		public static IQuery Or(IQuery query1, IQuery query2)
		{
			return new Query()
			{
				Key = "$or",
				Value = new QueryArray(new[] { query1, query2 })
			};
		}
		
		[MongoOperator("$nor")]
		public static IQuery Nor(QueryArray array)
		{
			return new Query()
			{
				Key = "$nor",
				Value = array
			};
		}

		[MongoOperator("$not")]
		public static IQuery Not(string key, Query value)
		{
			return new Query()
			{
				Key = key,
				Value = new Query()
				{
					Key = "$not",
					Value = value
				}
			};
		}
		
		#endregion

		#region Regular Expression Queries

		[MongoOperator("$regex")]
		public static IQuery Regex(string field, string regexString)
		{
			return new Query(field, new Query("$regex", regexString));
		}

		#endregion

		#region Other Methods

		public static IQuery Combine(IEnumerable<IQuery> values)
		{
			return new QueryGroup(values);
		}
		
		public static IQuery Combine(IQuery query1, IQuery query2)
		{
			return new QueryGroup(new[] { query1, query2 });
		}
		
		public static IQuery Combine(IQuery query1, IQuery query2, IQuery query3)
		{
			return new QueryGroup(new[] { query1, query2, query3 });
		}
		
		public static IQuery Combine(IQuery query1, IQuery query2, IQuery query3, IQuery query4)
		{
			return new QueryGroup(new[] { query1, query2, query3, query4 });
		}
		
		[MongoOperator("$type")]
		public static IQuery TypeOf(string value)
		{
			Query valueQuery = value;
			return new Query()
			{
				Key = "$type",
				Value = valueQuery
			};
		}

		#endregion

		#region Query Parser

		public static IQuery Parse(string json)
		{
			IQuery whereQuery = null;
			IQuery selectQuery = null;
			
			var root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
			if (root is JObject rootNode)
			{
				// Where
				if (rootNode.ContainsKey("where"))
				{
					var whereQueryRoot = rootNode["where"];
					if (whereQueryRoot != null)
					{
						var whereQueries = new List<IQuery>();
						foreach (var whereQueryItem in whereQueryRoot)
						{
							whereQueries.Add(ConvertJTokenToQuery(whereQueryItem));
						}

						whereQuery = Where(new QueryGroup(whereQueries));
					}
				}
				
				// Select
				if (rootNode.ContainsKey("select"))
				{
					var selectQueryRoot = rootNode["select"];
					if (selectQueryRoot != null)
					{
						var selectQueries = new QueryGroup();
						foreach (var selectQueryItem in selectQueryRoot)
						{
							if (selectQueryItem is JProperty jProperty)
							{
								selectQueries.Add(new Query(jProperty.Name, Query.CastToQuery(ConvertJTokenToQuery(jProperty.Value))));
							}
						}

						selectQuery = new Query()
						{
							Key = "select",
							Value = selectQueries
						};
					}
				}
			}

			if (whereQuery != null && selectQuery != null)
			{
				return Combine(whereQuery, selectQuery);
			}
			else
			{
				if (whereQuery != null)
				{
					return whereQuery;
				}
			
				if (selectQuery != null)
				{
					return selectQuery;
				}
			}
			
			return null;
		}

		private static IQuery ConvertJTokenToQuery(JToken jToken)
		{
			if (jToken is JProperty jProperty)
			{
				if (jProperty.Type == JTokenType.Property)
				{
					return new Query(jProperty.Name, Query.CastToQuery(ConvertJTokenToQuery(jProperty.Value)));
				}
				else if (jProperty.Type == JTokenType.Object)
				{
					return new Query(jProperty.Name, ConvertJTokenToQuery(jProperty.Value));
				}
				else
				{
					new Query(jProperty.Name, Query.CastToQuery(ConvertJsonProperty(jProperty)));
				}	
			}

			switch (jToken.Type)
			{
				case JTokenType.None:
				case JTokenType.Object:
				case JTokenType.Constructor:
				case JTokenType.Property:
					return Query.CastToQuery(jToken);
				case JTokenType.Array:
					QueryArray queryArray = new QueryArray();
					foreach (var item in jToken)
					{
						queryArray.Add(ConvertJTokenToQuery(item));
					}

					return queryArray;
				
				case JTokenType.String:
				case JTokenType.Comment:
				case JTokenType.Guid:
				case JTokenType.Uri:
				case JTokenType.Raw:
					return Query.CastToQuery(jToken.Value<string>());
				case JTokenType.Integer:
				case JTokenType.Bytes:
					return Query.CastToQuery(jToken.Value<int>());
				case JTokenType.Float:
					return Query.CastToQuery(jToken.Value<double>());
				case JTokenType.Boolean:
					return Query.CastToQuery(jToken.Value<bool>());
				case JTokenType.Date:
					return Query.CastToQuery(jToken.Value<DateTime>());
				case JTokenType.TimeSpan:
					return Query.CastToQuery(jToken.Value<TimeSpan>());
				case JTokenType.Null:
				case JTokenType.Undefined:
					return new Query();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private static object ConvertJsonProperty(JProperty jProperty)
		{
			if (jProperty.Type == JTokenType.Null)
			{
				return null;
			}
			
			if (jProperty.Type == JTokenType.String)
			{
				return jProperty.Value.ToString();
			}
			
			if (jProperty.Type == JTokenType.Integer)
			{
				if (int.TryParse(jProperty.Value.ToString(), out int value))
				{
					return value;
				}

				return 0;
			}
			
			if (jProperty.Type == JTokenType.Float)
			{
				if (double.TryParse(jProperty.Value.ToString(), out double value))
				{
					return value;
				}

				return 0.0d;
			}
			
			if (jProperty.Type == JTokenType.Boolean)
			{
				return jProperty.Value.ToString().ToLower() == "true";
			}
			
			if (jProperty.Type == JTokenType.Date)
			{
				if (DateTime.TryParse(jProperty.Value.ToString(), out DateTime dateTime))
				{
					return dateTime;
				}

				return default(DateTime);
			}
			
			if (jProperty.Type == JTokenType.TimeSpan)
			{
				if (TimeSpan.TryParse(jProperty.Value.ToString(), out TimeSpan timeSpan))
				{
					return timeSpan;
				}

				return default(TimeSpan);
			}
			
			throw new Exception("Unconvertible json type!");
		}

		private static IEnumerable<string> GetMongoOperators()
		{
			var staticMethods = typeof(QueryBuilder).GetMethods();
			var attributes = 
				staticMethods
					.Select(x => x.GetCustomAttribute<MongoOperator>())
					.Where(x => x != null)
					.Distinct();

			return attributes.Select(x => x.Name);
		}
		
		#endregion
	}
}