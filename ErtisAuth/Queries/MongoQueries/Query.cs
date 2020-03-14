using System.Collections.Generic;
using ErtisAuth.Helpers;

namespace ErtisAuth.Queries.MongoQueries
{
	public class Query : IQuery
	{
		public string Key { get; set; }

		public IQuery Value { get; set; }

		public Query()
		{
			
		}
		
		public Query(string key, IQuery value)
		{
			this.Key = key;
			this.Value = value;
		}
		
		public Query(string key, string value)
		{
			this.Key = key;
			Query queryValue = value;
			this.Value = queryValue;
		}
		
		public Query(string key, int value)
		{
			this.Key = key;
			Query queryValue = value;
			this.Value = queryValue;
		}
		
		public Query(string key, double value)
		{
			this.Key = key;
			Query queryValue = value;
			this.Value = queryValue;
		}
		
		public Query(string key, bool value)
		{
			this.Key = key;
			Query queryValue = value;
			this.Value = queryValue;
		}
		
		public object ValueAsVariable { get; private set; }

		public static implicit operator Query(string value)
		{
			return new Query() { ValueAsVariable = value };
		}

		public static implicit operator Query(int value)
		{
			return new Query() { ValueAsVariable = value };
		}
		
		public static implicit operator Query(double value)
		{
			return new Query() { ValueAsVariable = value };
		}
		
		public static implicit operator Query(bool value)
		{
			return new Query() { ValueAsVariable = value };
		}
		
		public static implicit operator Query(KeyValuePair<string, object> pair)
		{
			return new Query() { Value = new Query() { Key = pair.Key, Value = CastToQuery(pair.Value) } };
		}

		public static IQuery CastToQuery(object value)
		{
			Query query;
			if (value is string valueStr)
				query = valueStr;
			else if (IsNumeric(value, out var numeric))
				query = numeric;
			else if (value is bool booleanValue)
				query = booleanValue;
			else
				return new Query() { ValueAsVariable = value };

			return query;
		}

		public static bool IsNumeric(object value, out double parsedValue)
		{
			if (value == null)
			{
				parsedValue = 0;
				return false;
			}

			return double.TryParse(value.ToString(), out parsedValue);
		}
		
		public string ToInnerString()
		{
			return "\"" + this.Key + "\": " + this.Value;
		}

		public override string ToString()
		{
			if (this.Value == null)
			{
				if (this.ValueAsVariable == null)
					return "{}";
				
				string valueStr = this.ValueAsVariable.ToString();
				if (this.ValueAsVariable is string)
					return $"\"{valueStr}\"";

				return StringHelper.ToStringAsRestFormat(this.ValueAsVariable);
			}

			return "{" + this.ToInnerString() + "}";
		}
	}
}