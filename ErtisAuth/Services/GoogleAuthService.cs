using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Providers.Google;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;
using ErtisAuth.Providers;
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

		public IResponseResult<LoginResponse> Login(IProviderTicket providerTicket)
		{
			return this.GoogleAuthEndpoint.Post<LoginResponse>(
				body: new RequestBody(providerTicket), 
				headers: this.GetMembershipHeaders());
		}

		public async Task<IResponseResult<LoginResponse>> LoginAsync(IProviderTicket providerTicket)
		{
			return await this.GoogleAuthEndpoint.PostAsync<LoginResponse>(
				body: new RequestBody(providerTicket), 
				headers: this.GetMembershipHeaders());
		}

		#endregion
	}
}