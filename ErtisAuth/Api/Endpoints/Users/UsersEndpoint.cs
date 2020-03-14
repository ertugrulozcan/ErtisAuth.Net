using ErtisAuth.Helpers;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Users
{
	public sealed class UsersEndpoint : FullBaseEndpoint<UsersEndpoint.IUrlParams>
	{
		#region Constants

		private const string USER_ID_TAG = "USER_ID";

		#endregion
		
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/users/{USER_ID_TAG.ToUrlParam()}/{QUERY_ENDPOINT_TAG.ToUrlParam()}";
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public UsersEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region QueryParams
		
		public new interface IUrlParams : MembershipBoundedEndpoint<IUrlParams>.IUrlParams
		{
			IUrlParams SetUserId(string userId);
		}

		public class UsersEndpointUrlParams : MembershipUrlParams, IUrlParams
		{
			public IUrlParams SetUserId(string userId)
			{
				this.SetKeyValue(USER_ID_TAG, userId);
				return this;
			}
		}
		
		public new static class UrlParams
		{
			public static IUrlParams SetUserId(string userId)
			{
				var urlParams = new UsersEndpointUrlParams();
				urlParams.SetUserId(userId);
				return urlParams;
			}
			
			public static IUrlParams SetMembershipId(string membershipId)
			{
				var urlParams = new UsersEndpointUrlParams();
				urlParams.SetMembershipId(membershipId);
				return urlParams;
			}
		}

		#endregion
	}
}