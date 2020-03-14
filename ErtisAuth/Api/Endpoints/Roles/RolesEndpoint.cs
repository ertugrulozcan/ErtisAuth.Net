using ErtisAuth.Helpers;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Roles
{
	public sealed class RolesEndpoint : FullBaseEndpoint<RolesEndpoint.IUrlParams>
	{
		#region Constants

		private const string ROLE_ID_TAG = "ROLE_ID";

		#endregion
		
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/roles/{ROLE_ID_TAG.ToUrlParam()}/{QUERY_ENDPOINT_TAG.ToUrlParam()}";
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public RolesEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region QueryParams
		
		public new interface IUrlParams : MembershipBoundedEndpoint<IUrlParams>.IUrlParams
		{
			IUrlParams SetRoleId(string roleId);
		}

		public class RolesEndpointUrlParams : MembershipUrlParams, IUrlParams
		{
			public IUrlParams SetRoleId(string roleId)
			{
				this.SetKeyValue(ROLE_ID_TAG, roleId);
				return this;
			}
		}
		
		public new static class UrlParams
		{
			public static IUrlParams SetRoleId(string roleId)
			{
				var urlParams = new RolesEndpoint.RolesEndpointUrlParams();
				urlParams.SetRoleId(roleId);
				return urlParams;
			}
			
			public static IUrlParams SetMembershipId(string membershipId)
			{
				var urlParams = new RolesEndpoint.RolesEndpointUrlParams();
				urlParams.SetMembershipId(membershipId);
				return urlParams;
			}
		}

		#endregion
	}
}