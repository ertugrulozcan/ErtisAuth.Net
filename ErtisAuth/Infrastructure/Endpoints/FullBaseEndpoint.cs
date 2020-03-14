using System.Net.Http;
using System.Threading.Tasks;

namespace ErtisAuth.Infrastructure.Endpoints
{
	public abstract class FullBaseEndpoint<TUrlParams> :
		MembershipBoundedEndpoint<TUrlParams>, 
		IHasGet<TUrlParams>, 
		IHasPost<TUrlParams>, 
		IHasPut<TUrlParams>, 
		IHasDelete<TUrlParams>
		where TUrlParams : class, IQueryEndpointUrlParams<TUrlParams>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		protected FullBaseEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion

		#region Methods

		public IResponseResult Get(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult> GetAsync(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Get<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult<T>> GetAsync<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Get, urlParams, body, queryString, headers);
		}
		
		public IResponseResult Post(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult> PostAsync(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Post<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult<T>> PostAsync<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Post, urlParams, body, queryString, headers);
		}
		
		public IResponseResult Put(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Put, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult> PutAsync(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Put, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Put<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Put, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult<T>> PutAsync<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Put, urlParams, body, queryString, headers);
		}
		
		public IResponseResult Delete(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest(HttpMethod.Delete, urlParams, body, queryString, headers);
		}
		
		public async Task<IResponseResult> DeleteAsync(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync(HttpMethod.Delete, urlParams, body, queryString, headers);
		}
		
		public IResponseResult<T> Delete<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return this.ExecuteRequest<T>(HttpMethod.Delete, urlParams, body, queryString, headers);
		}

		public async Task<IResponseResult<T>> DeleteAsync<T>(TUrlParams urlParams = null, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null)
		{
			return await this.ExecuteRequestAsync<T>(HttpMethod.Delete, urlParams, body, queryString, headers);
		}
		
		#endregion
	}
}