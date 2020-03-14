namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class ResetPasswordEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/reset-password";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public ResetPasswordEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}