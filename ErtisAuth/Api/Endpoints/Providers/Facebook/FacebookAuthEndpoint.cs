using System.Net.Http;
using System.Threading.Tasks;
using ErtisAuth.Infrastructure;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Providers.Facebook
{
	public class FacebookAuthEndpoint : EndpointBase<FacebookAuthEndpoint.IUrlParams>, IHasPost<FacebookAuthEndpoint.IUrlParams>
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/sign-in/facebook";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public FacebookAuthEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region Methods
		
		public IResponseResult Post(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Post, urlParams, body, queryString, headers);
		}

		public async Task<IResponseResult> PostAsync(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Post<T>(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult<T>> PostAsync<T>(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Post, urlParams, body, queryString, headers);
		}

		#endregion
		
		#region QueryParams
		
		public interface IUrlParams : Infrastructure.IUrlParams
		{
			
		}

		public class FacebookAuthEndpointUrlParams : UrlParamsBase, IUrlParams
		{
			
		}
		
		public static class UrlParams
		{
			
		}

		#endregion	
	}
}