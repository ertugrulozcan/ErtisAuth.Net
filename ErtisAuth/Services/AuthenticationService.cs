using System.Net;
using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Auth;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class AuthenticationService : MembershipBoundedService, IAuthenticationService
	{
		#region Endpoints

		private readonly GenerateTokenEndpoint GenerateTokenEndpoint;
		private readonly RefreshTokenEndpoint RefreshTokenEndpoint;
		private readonly VerifyTokenEndpoint VerifyTokenEndpoint;
		private readonly RevokeTokenEndpoint RevokeTokenEndpoint;
		private readonly MeEndpoint MeEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public AuthenticationService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.GenerateTokenEndpoint = new GenerateTokenEndpoint(this.BaseUrl);
			this.RefreshTokenEndpoint = new RefreshTokenEndpoint(this.BaseUrl);
			this.VerifyTokenEndpoint = new VerifyTokenEndpoint(this.BaseUrl);
			this.RevokeTokenEndpoint = new RevokeTokenEndpoint(this.BaseUrl);
			this.MeEndpoint = new MeEndpoint(this.BaseUrl);
		}

		#endregion
		
		#region Methods

		public IResponseResult<AuthenticationToken> GetToken(string username, string password)
		{
			return this.GenerateTokenEndpoint.Post<AuthenticationToken>(body: new RequestBody(new { username, password }), headers: this.GetMembershipHeaders());
		}

		public async Task<IResponseResult<AuthenticationToken>> GetTokenAsync(string username, string password)
		{
			return await this.GenerateTokenEndpoint.PostAsync<AuthenticationToken>(body: new RequestBody(new { username, password }), headers: this.GetMembershipHeaders());
		}
		
		public IResponseResult<AuthenticationToken> RefreshToken(AuthenticationToken token)
		{
			return this.RefreshToken(token.RefreshToken);
		}

		public async Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(AuthenticationToken token)
		{
			return await this.RefreshTokenAsync(token.RefreshToken);
		}
		
		public IResponseResult<AuthenticationToken> RefreshToken(string refreshToken)
		{
			return this.RefreshTokenEndpoint.Post<AuthenticationToken>(body: new RequestBody(new { token = refreshToken }), headers: this.GetMembershipHeaders());
		}

		public async Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(string refreshToken)
		{
			return await this.RefreshTokenEndpoint.PostAsync<AuthenticationToken>(body: new RequestBody(new { token = refreshToken }), headers: this.GetMembershipHeaders());
		}
		
		public IResponseResult<AuthenticationToken> VerifyToken(AuthenticationToken token)
		{
			return this.VerifyToken(token.AccessToken);
		}

		public async Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(AuthenticationToken token)
		{
			return await this.VerifyTokenAsync(token.AccessToken);
		}
		
		public IResponseResult<AuthenticationToken> VerifyToken(string accessToken)
		{
			return this.VerifyTokenEndpoint.Post<AuthenticationToken>(body: new RequestBody(new { token = accessToken }), headers: this.GetMembershipHeaders());
		}

		public async Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(string accessToken)
		{
			return await this.VerifyTokenEndpoint.PostAsync<AuthenticationToken>(body: new RequestBody(new { token = accessToken }), headers: this.GetMembershipHeaders());
		}
		
		/// <summary>
		/// 204 döndüyse true, 401 döndüyse false
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public IResponseResult RevokeToken(AuthenticationToken token)
		{
			return this.RevokeToken(token.AccessToken);
		}

		public async Task<IResponseResult> RevokeTokenAsync(AuthenticationToken token)
		{
			return await this.RevokeTokenAsync(token.AccessToken);
		}
		
		/// <summary>
		/// 204 döndüyse true, 401 döndüyse false
		/// </summary>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		public IResponseResult RevokeToken(string accessToken)
		{
			var response = this.RevokeTokenEndpoint.Post(body: new RequestBody(new { token = accessToken }), headers: this.GetMembershipHeaders());
			return new ResponseResult(response.HttpCode != null && response.HttpCode.Value == HttpStatusCode.NoContent);
		}

		public async Task<IResponseResult> RevokeTokenAsync(string accessToken)
		{
			var response = await this.RevokeTokenEndpoint.PostAsync(body: new RequestBody(new { token = accessToken }), headers: this.GetMembershipHeaders());
			return new ResponseResult(response.HttpCode != null && response.HttpCode.Value == HttpStatusCode.NoContent);
		}
		
		public IResponseResult<User> WhoAmI(string token)
		{
			return this.MeEndpoint.Get<User>(headers: HeaderCollection.Add("Authorization", $"Bearer {token}"));
		}

		public async Task<IResponseResult<User>> WhoAmIAsync(string token)
		{
			return await this.MeEndpoint.GetAsync<User>(headers: HeaderCollection.Add("Authorization", $"Bearer {token}"));
		}
		
		#endregion
	}
}