namespace ErtisAuth.Api.Endpoints.Auth
{
	public sealed class SetPasswordEndpoint : TokenEndpointBase
	{
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/set-password";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public SetPasswordEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
	}
}