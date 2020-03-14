namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class ChangePasswordEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/change-password";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public ChangePasswordEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}