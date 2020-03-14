namespace ErtisAuth.Services
{
	public abstract class BaseService
	{
		#region Properties

		protected string BaseUrl { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		protected BaseService(string baseUrl)
		{
			this.BaseUrl = baseUrl;
		}

		#endregion
	}
}