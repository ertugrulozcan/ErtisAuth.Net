using System.Net.Http;
using System.Threading.Tasks;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Rest
{
	public interface IRestHandler
	{
		#region Methods

		IResponseResult<TResult> ExecuteRequest<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers);

		Task<IResponseResult<TResult>> ExecuteRequestAsync<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers);
		
		IResponseResult ExecuteRequest(HttpMethod method, string url, RequestBody body, IHeaderCollection headers);
		
		Task<IResponseResult> ExecuteRequestAsync(HttpMethod method, string url, RequestBody body, IHeaderCollection headers);
		
		#endregion
	}
}