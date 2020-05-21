using ErtisAuth.Config;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public abstract class MembershipBoundedService : BaseService, IMembershipBoundedService
	{
		#region Properties

		public string MembershipId { get; }
		
		public string AdministratorToken { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		protected MembershipBoundedService(IErtisAuthConfiguration configuration) : base(configuration.BaseUrl)
		{
			this.MembershipId = configuration.MembershipId;
			this.AdministratorToken = configuration.AdminAccessToken;
		}

		#endregion

		#region Methods

		protected IHeaderCollection GetMembershipHeaders(string token = null)
		{
			if (string.IsNullOrEmpty(token))
			{
				return HeaderCollection.Add("X-Ertis-Alias", this.MembershipId);	
			}
			else
			{
				return HeaderCollection.Add("X-Ertis-Alias", this.MembershipId).AddHeader("Authorization", $"Bearer {token}");
			}
		}

		#endregion
	}
}