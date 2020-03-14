using System.Net.Http;

namespace ErtisAuth.Infrastructure
{
	public interface ICurlRequest<in TUrlParams> where TUrlParams : IUrlParams
	{
		string GenerateUrl(TUrlParams urlParams, IQueryString queryString = null);
		
		string GenerateCurlCode(HttpMethod method, TUrlParams urlParams, RequestBody body = null, IQueryString queryString = null, IHeaderCollection headers = null);
	}
}