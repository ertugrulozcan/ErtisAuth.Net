namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class VerifyTokenEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/verify-token";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public VerifyTokenEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}