using ErtisAuth.Helpers;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Applications
{
	public sealed class ApplicationsEndpoint : FullBaseEndpoint<ApplicationsEndpoint.IUrlParams>
	{
		#region Constants

		private const string APPLICATION_ID_TAG = "APPLICATION_ID";

		#endregion
		
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/applications/{APPLICATION_ID_TAG.ToUrlParam()}/{QUERY_ENDPOINT_TAG.ToUrlParam()}";
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public ApplicationsEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region QueryParams
		
		public new interface IUrlParams : MembershipBoundedEndpoint<IUrlParams>.IUrlParams
		{
			IUrlParams SetApplicationId(string applicationId);
		}

		public class ApplicationsEndpointUrlParams : MembershipUrlParams, IUrlParams
		{
			public IUrlParams SetApplicationId(string applicationId)
			{
				this.SetKeyValue(APPLICATION_ID_TAG, applicationId);
				return this;
			}
		}
		
		public new static class UrlParams
		{
			public static IUrlParams SetApplicationId(string applicationId)
			{
				var urlParams = new ApplicationsEndpointUrlParams();
				urlParams.SetApplicationId(applicationId);
				return urlParams;
			}
			
			public static IUrlParams SetMembershipId(string membershipId)
			{
				var urlParams = new ApplicationsEndpointUrlParams();
				urlParams.SetMembershipId(membershipId);
				return urlParams;
			}
		}

		#endregion
	}
}