using System.Net.Http;
using System.Threading.Tasks;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Rest.Impl
{
	public class RestSharpRestHandler : IRestHandler
	{
		#region Methods

		public IResponseResult<TResult> ExecuteRequest<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IResponseResult<TResult>> ExecuteRequestAsync<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			throw new System.NotImplementedException();
		}
		
		public IResponseResult ExecuteRequest(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IResponseResult> ExecuteRequestAsync(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}