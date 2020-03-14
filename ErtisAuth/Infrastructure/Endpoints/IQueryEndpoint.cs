namespace ErtisAuth.Infrastructure.Endpoints  
{
	public interface IQueryEndpoint
	{
		
	}
	
	public abstract class QueryEndpoint<TUrlParams> : EndpointBase<TUrlParams>, IQueryEndpoint where TUrlParams : IQueryEndpointUrlParams<TUrlParams>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="basePath"></param>
		protected QueryEndpoint(string basePath) : base(basePath)
		{
			
		}

		#endregion
	}
}