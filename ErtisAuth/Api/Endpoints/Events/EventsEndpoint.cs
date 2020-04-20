using ErtisAuth.Helpers;
using ErtisAuth.Infrastructure.Endpoints;

namespace ErtisAuth.Api.Endpoints.Events
{
	public sealed class EventsEndpoint : FullBaseEndpoint<EventsEndpoint.IUrlParams>
	{
		#region Constants

		private const string EVENT_ID_TAG = "EVENT_ID";

		#endregion
		
		#region Properties

		public override string SelfPath
		{
			get
			{
				return $"/events/{EVENT_ID_TAG.ToUrlParam()}/{QUERY_ENDPOINT_TAG.ToUrlParam()}";
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="baseUrl"></param>
		public EventsEndpoint(string baseUrl) : base(baseUrl)
		{
			
		}

		#endregion
		
		#region QueryParams
		
		public new interface IUrlParams : MembershipBoundedEndpoint<IUrlParams>.IUrlParams
		{
			IUrlParams SetEventId(string eventId);
		}

		public class EventsEndpointUrlParams : MembershipUrlParams, IUrlParams
		{
			public IUrlParams SetEventId(string eventId)
			{
				this.SetKeyValue(EVENT_ID_TAG, eventId);
				return this;
			}
		}
		
		public new static class UrlParams
		{
			public static IUrlParams SetEventId(string eventId)
			{
				var urlParams = new EventsEndpointUrlParams();
				urlParams.SetEventId(eventId);
				return urlParams;
			}
			
			public static IUrlParams SetMembershipId(string membershipId)
			{
				var urlParams = new EventsEndpointUrlParams();
				urlParams.SetMembershipId(membershipId);
				return urlParams;
			}
		}

		#endregion
	}
}