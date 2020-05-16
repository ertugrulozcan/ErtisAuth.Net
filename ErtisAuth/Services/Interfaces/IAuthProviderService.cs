using System.Threading.Tasks;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;
using ErtisAuth.Providers;

namespace ErtisAuth.Services.Interfaces
{
	public interface IAuthProviderService
	{
		IResponseResult<LoginResponse> Login(IProviderTicket providerTicket);
		
		Task<IResponseResult<LoginResponse>> LoginAsync(IProviderTicket providerTicket);
	}
}