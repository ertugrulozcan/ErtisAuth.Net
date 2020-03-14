using System.Collections.Generic;

namespace ErtisAuth.Infrastructure
{
	public interface IUrlParams
	{
		Dictionary<string, string> UrlParamsDictionary { get; }
	}
	
	public interface IQueryEndpointUrlParams<out TUrlParams> : IUrlParams where TUrlParams : IUrlParams
	{
		TUrlParams UseMongoQuery();
	}
	
	public abstract class UrlParamsBase : IUrlParams
	{
		public Dictionary<string, string> UrlParamsDictionary { get; } = new Dictionary<string, string>();
		
		protected void SetKeyValue(string key, string value)
		{
			if (!this.UrlParamsDictionary.ContainsKey(key))
			{
				this.UrlParamsDictionary.Add(key, value);
			}
			else
			{
				this.UrlParamsDictionary[key] = value;
			}
		}
	}
}