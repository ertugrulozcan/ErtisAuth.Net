using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ErtisAuth.Helpers;
using ErtisAuth.Rest;

namespace ErtisAuth.Infrastructure.Endpoints
{
	public abstract class EndpointBase<TUrlParams> : IEndpoint, ICurlRequest<TUrlParams> where TUrlParams : IUrlParams
	{
		#region Fields

		private readonly string BaseUrl;

		#endregion
		
		#region Properties
		
		public abstract string SelfPath { get; }

		public string Path
		{
			get
			{
				if (this is IParentEndpoint parentEndpoint)
				{
					return $"{parentEndpoint.BasePath.TrimEnd('/')}/{this.SelfPath.TrimStart('/')}";
				}

				return this.SelfPath;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="basePath"></param>
		protected EndpointBase(string basePath)
		{
			this.BaseUrl = basePath;
		}

		#endregion

		#region Methods

		protected IResponseResult ExecuteRequest(HttpMethod method, TUrlParams urlParams, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			string url = this.GenerateUrl(urlParams, queryString);
			return RestHandler.Current.ExecuteRequest(method, url, body, headers);
		}
		
		protected async Task<IResponseResult> ExecuteRequestAsync(HttpMethod method, TUrlParams urlParams, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			string url = this.GenerateUrl(urlParams, queryString);
			return await RestHandler.Current.ExecuteRequestAsync(method, url, body, headers);
		}
		
		protected IResponseResult<T> ExecuteRequest<T>(HttpMethod method, TUrlParams urlParams, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			string url = this.GenerateUrl(urlParams, queryString);
			return RestHandler.Current.ExecuteRequest<T>(method, url, body, headers);
		}
		
		protected async Task<IResponseResult<T>> ExecuteRequestAsync<T>(HttpMethod method, TUrlParams urlParams, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			string url = this.GenerateUrl(urlParams, queryString);
			return await RestHandler.Current.ExecuteRequestAsync<T>(method, url, body, headers);
		}

		public string GenerateUrl(TUrlParams urlParams, IQueryString queryString = null)
		{
			string url = $"{this.BaseUrl.TrimEnd('/')}/{this.Path.TrimStart('/')}";

			if (urlParams != null)
			{
				foreach (var urlParam in urlParams.UrlParamsDictionary)
				{
					url = UrlHelper.ReplaceOrRemove(url, urlParam.Key, urlParam.Value);
				}	
			}

			url = UrlHelper.ClearTags(url);

			if (queryString != null)
			{
				var queryStringDictionary = new Dictionary<string, string>();
				foreach (var parameter in queryString.ToDictionary())
				{
					if (!string.IsNullOrEmpty(parameter.Key) && parameter.Value != null && !string.IsNullOrEmpty(parameter.Value.ToString()))
					{
						queryStringDictionary.Add(parameter.Key, parameter.Value.ToString());
					}
				}

				if (queryStringDictionary.Any())
				{
					url += $"?{string.Join("&", queryStringDictionary.Select(x => $"{x.Key}={x.Value}"))}";	
				}
			}
			
			return url;
		}

		public string GenerateCurlCode(HttpMethod method, TUrlParams urlParams = default(TUrlParams), RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			string curl = $"curl --location --request {method.ToString().ToUpper()} '{this.GenerateUrl(urlParams, queryString)}' \\" + Environment.NewLine;
			if (headers != null)
			{
				foreach (var header in headers)
				{
					curl += $"--header '{header.Key}: {header.Value}' \\" + Environment.NewLine;
				}
			}

			if (body != null)
			{
				curl += $"--data-raw '{body.Context}'";
			}

			return curl;
		}
		
		#endregion
	}
}