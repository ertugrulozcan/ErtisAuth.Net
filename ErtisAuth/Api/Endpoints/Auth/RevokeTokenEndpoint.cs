namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class RevokeTokenEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/revoke-token";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public RevokeTokenEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}