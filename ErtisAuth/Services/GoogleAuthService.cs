using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Providers.Google;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class GoogleAuthService : MembershipBoundedService, IGoogleAuthService
	{
		#region Endpoints

		private readonly GoogleAuthEndpoint GoogleAuthEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public GoogleAuthService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.GoogleAuthEndpoint = new GoogleAuthEndpoint(this.BaseUrl);
		}

		#endregion
		
		#region Methods

		public IResponseResult<AuthenticationToken> LoginWithGoogle(string googleTokenId)
		{
			return this.GoogleAuthEndpoint.Get<AuthenticationToken>(
				body: new RequestBody(), 
				headers: this.GetMembershipHeaders(), 
				queryString: QueryString.Add("id_token", googleTokenId));
		}

		public async Task<IResponseResult<AuthenticationToken>> LoginWithGoogleAsync(string googleTokenId)
		{
			return await this.GoogleAuthEndpoint.GetAsync<AuthenticationToken>(
				body: new RequestBody(), 
				headers: this.GetMembershipHeaders(), 
				queryString: QueryString.Add("id_token", googleTokenId));
		}

		#endregion
	}
}