using System.Threading.Tasks;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface ISearchService<T>
	{
		IResponseResult<CollectionResponseData<T>> Search(string key, string accessToken);

		Task<IResponseResult<CollectionResponseData<T>>> SearchAsync(string key, string accessToken);
	}
}