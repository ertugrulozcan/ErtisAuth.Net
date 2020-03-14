using System;
using System.Collections.Generic;
using System.Linq;
using ErtisAuth.Helpers;

namespace ErtisAuth.Infrastructure
{
	public static class QueryString
	{
		public static IQueryString Add(string key, object value)
		{
			return new HttpQueryString(new Dictionary<string, object>() { { key, value } });
		}
	}
	
	public interface IQueryString
	{
		IQueryString Add(string key, object value);

		IQueryString Add(KeyValuePair<string, object> pair);
		
		IQueryString Remove(string key);

		IDictionary<string, object> ToDictionary();
	}
	
	public class HttpQueryString : IQueryString
	{
		#region Properties

		private Dictionary<string, object> Dictionary { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor 1
		/// </summary>
		internal HttpQueryString() : this(new Dictionary<string, object>())
		{ }
		
		/// <summary>
		/// Constructor 2
		/// </summary>
		/// <param name="dictionary"></param>
		internal HttpQueryString(Dictionary<string, object> dictionary)
		{
			this.Dictionary = dictionary;
		}

		#endregion

		#region Methods

		public IQueryString Add(string key, object value)
		{
			if (!this.Dictionary.ContainsKey(key))
				this.Dictionary.Add(key, value);
			else
				this.Dictionary[key] = value;
			
			return this;
		}

		public IQueryString Add(KeyValuePair<string, object> pair)
		{
			return this.Add(pair.Key, pair.Value);
		}

		public IQueryString Remove(string key)
		{
			if (this.Dictionary.ContainsKey(key))
			{
				this.Dictionary.Remove(key);
			}

			return this;
		}

		public IDictionary<string, object> ToDictionary()
		{
			return this.Dictionary;
		}

		public override string ToString()
		{
			return string.Join("&", this.Dictionary.Select(x => $"{x.Key}={Uri.EscapeUriString(StringHelper.ToStringAsRestFormat(x.Value))}"));
		}

		#endregion
	}
}