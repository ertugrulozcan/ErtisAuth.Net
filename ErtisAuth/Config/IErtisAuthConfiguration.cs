namespace ErtisAuth.Config
{
	public interface IErtisAuthConfiguration
	{
		string BaseUrl { get; }
		
		string MembershipId { get; }
		
		string AdminAccessToken { get; }
	}
	
	public class ErtisAuthConfiguration : IErtisAuthConfiguration
	{
		#region Properties

		public string BaseUrl { get; }
		
		public string MembershipId { get; }
		
		public string AdminAccessToken { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		/// <param name="membershipId"></param>
		/// <param name="adminAccessToken"></param>
		public ErtisAuthConfiguration(string baseUrl, string membershipId, string adminAccessToken)
		{
			this.BaseUrl = baseUrl;
			this.MembershipId = membershipId;
			this.AdminAccessToken = adminAccessToken;
		}

		#endregion
	}
}