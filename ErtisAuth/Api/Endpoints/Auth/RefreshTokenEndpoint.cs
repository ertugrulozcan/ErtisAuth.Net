namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class RefreshTokenEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/refresh-token";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public RefreshTokenEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}