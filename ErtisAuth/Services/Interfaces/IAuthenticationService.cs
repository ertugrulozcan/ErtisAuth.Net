using System.Threading.Tasks;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IAuthenticationService
	{
		#region Methods

		IResponseResult<AuthenticationToken> GetToken(string username, string password);
		
		Task<IResponseResult<AuthenticationToken>> GetTokenAsync(string username, string password);
		
		IResponseResult<AuthenticationToken> RefreshToken(AuthenticationToken token);
		
		Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(AuthenticationToken token);
		
		IResponseResult<AuthenticationToken> RefreshToken(string refreshToken);
		
		Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(string refreshToken);
		
		IResponseResult<AuthenticationToken> VerifyToken(AuthenticationToken token);
		
		Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(AuthenticationToken token);
		
		IResponseResult<AuthenticationToken> VerifyToken(string accessToken);
		
		Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(string accessToken);
		
		IResponseResult RevokeToken(AuthenticationToken token);
		
		Task<IResponseResult> RevokeTokenAsync(AuthenticationToken token);
		
		IResponseResult RevokeToken(string accessToken);
		
		Task<IResponseResult> RevokeTokenAsync(string accessToken);

		IResponseResult<Me> WhoAmI(string token);
		
		Task<IResponseResult<Me>> WhoAmIAsync(string token);

		#endregion
	}
}