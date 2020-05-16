using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Providers.Facebook;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;
using ErtisAuth.Providers;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class FacebookAuthService : MembershipBoundedService, IFacebookAuthService
	{
		#region Endpoints

		private readonly FacebookAuthEndpoint FacebookAuthEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public FacebookAuthService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.FacebookAuthEndpoint = new FacebookAuthEndpoint(this.BaseUrl);
		}

		#endregion
		
		#region Methods

		public IResponseResult<LoginResponse> Login(IProviderTicket providerTicket)
		{
			return this.FacebookAuthEndpoint.Post<LoginResponse>(
				body: new RequestBody(providerTicket), 
				headers: this.GetMembershipHeaders());
		}

		public async Task<IResponseResult<LoginResponse>> LoginAsync(IProviderTicket providerTicket)
		{
			return await this.FacebookAuthEndpoint.PostAsync<LoginResponse>(
				body: new RequestBody(providerTicket), 
				headers: this.GetMembershipHeaders());
		}

		#endregion
	}
}