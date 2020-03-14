using System.Net.Http;
using System.Threading.Tasks;
using ErtisAuth.Infrastructure;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class MeEndpoint : EndpointBase<MeEndpoint.IUrlParams>, IHasGet<MeEndpoint.IUrlParams>
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/me";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public MeEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region Methods
		
		public IResponseResult Get(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Get, urlParams, body, queryString, headers);
		}

		public async Task<IResponseResult> GetAsync(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Get<T>(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult<T>> GetAsync<T>(IUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Get, urlParams, body, queryString, headers);
		}

		#endregion
		
		#region QueryParams
		
		public interface IUrlParams : Infrastructure.IUrlParams
		{
			
		}

		public class MeEndpointUrlParams : UrlParamsBase, IUrlParams
		{
			
		}
		
		public static class UrlParams
		{
			
		}

		#endregion
	}
}