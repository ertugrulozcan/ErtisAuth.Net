using System.Threading.Tasks;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IGoogleAuthService
	{
		IResponseResult<AuthenticationToken> LoginWithGoogle(string googleTokenId);

		Task<IResponseResult<AuthenticationToken>> LoginWithGoogleAsync(string googleTokenId);
	}
}