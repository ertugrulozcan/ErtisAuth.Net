namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class GenerateTokenEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/generate-token";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public GenerateTokenEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}