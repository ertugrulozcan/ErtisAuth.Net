using ErtisAuth.Helpers;

namespace ErtisAuth.Infrastructure.Endpoints
{
	public abstract class MembershipBoundedEndpoint<TUrlParams> : 
		QueryEndpoint<TUrlParams>, 
		IParentEndpoint 
		where TUrlParams : class, IQueryEndpointUrlParams<TUrlParams>
	{
		#region Constants

		private const string MEMBERSHIP_ID_TAG = "MEMBERSHIP_ID";
		
		protected const string QUERY_ENDPOINT_TAG = "_QUERY";

		#endregion
		
		#region Properties

		public string BasePath
		{
			get
			{
				return $"/memberships/{MEMBERSHIP_ID_TAG.ToUrlParam()}";
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="basePath"></param>
		protected MembershipBoundedEndpoint(string basePath) : base(basePath)
		{
			
		}

		#endregion
		
		#region QueryParams
		
		public interface IUrlParams : IQueryEndpointUrlParams<TUrlParams>
		{
			TUrlParams SetMembershipId(string userId);
		}

		public class MembershipUrlParams : UrlParamsBase, IQueryEndpointUrlParams<TUrlParams> 
		{
			public TUrlParams SetMembershipId(string membershipId)
			{
				this.SetKeyValue(MEMBERSHIP_ID_TAG, membershipId);
				return this as TUrlParams;
			}

			public TUrlParams UseMongoQuery()
			{
				this.SetKeyValue(QUERY_ENDPOINT_TAG, "_query");
				return this as TUrlParams;
			}
		}
		
		public static class UrlParams
		{
			public static TUrlParams SetMembershipId(string membershipId)
			{
				var urlParams = new MembershipUrlParams();
				urlParams.SetMembershipId(membershipId);
				return urlParams as TUrlParams;
			}
		}

		#endregion
	}
}