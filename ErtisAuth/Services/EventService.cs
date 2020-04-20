using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Events;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Events;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace ErtisAuth.Services
{
	public class EventService : MembershipBoundedService, IEventService
	{
		#region Endpoints

		private readonly EventsEndpoint EventsEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public EventService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.EventsEndpoint = new EventsEndpoint(this.BaseUrl);
		}

		#endregion
	
		#region Methods

		public IResponseResult<EventLog> GetEvent(string accessToken, string eventId)
		{
			return this.GetEventAsync(accessToken, eventId).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<EventLog>> GetEventAsync(string accessToken, string eventId)
		{
			var response = await this.EventsEndpoint.GetAsync(
				urlParams: new EventsEndpoint.EventsEndpointUrlParams().SetMembershipId(this.MembershipId).SetEventId(eventId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				string json = response.Data.ToString();
				var root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
				if (root is JObject rootNode)
				{
					var eventItem = this.DeserializeEvent(rootNode);
					if (eventItem != null)
					{
						return new ResponseResult<EventLog>(true) { Data = eventItem };
					}
				}
				
				return new ResponseResult<EventLog>(false, "Response could not deserialized!");
			}
			else
			{
				return new ResponseResult<EventLog>(false, response.Message);
			}
		}
		
		public IResponseResult<IEnumerable<EventLog>> GetEvents(string accessToken, out int totalCount, int? skip = null, int? limit = null)
		{
			var collectionResponse = this.GetEventsAsync(accessToken, skip, limit).ConfigureAwait(false).GetAwaiter().GetResult();
			if (collectionResponse.IsSuccess)
			{
				totalCount = collectionResponse.Data.Count;
				return new ResponseResult<IEnumerable<EventLog>>(true) { Data = collectionResponse.Data.Items };
			}
			else
			{
				totalCount = 0;
				return new ResponseResult<IEnumerable<EventLog>>(false, collectionResponse.Message);
			}
		}

		public async Task<IResponseResult<CollectionResponseData<EventLog>>> GetEventsAsync(string accessToken, int? skip = null, int? limit = null)
		{
			IQueryString queryString = null;
			if (skip != null)
				queryString = QueryString.Add("skip", skip.Value);
			if (limit != null)
				queryString = queryString == null ? QueryString.Add("limit", limit.Value) : queryString.Add("limit", limit.Value);
			
			var response = await this.EventsEndpoint.PostAsync(
				urlParams: new EventsEndpoint.EventsEndpointUrlParams().SetMembershipId(this.MembershipId).UseMongoQuery(),
				body: new RequestBody(),
				queryString: queryString,
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				string json = response.Data.ToString();
				var root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
				if (root is JObject rootNode)
				{
					var dataNode = rootNode["data"];
					var items = dataNode?["items"];
					int.TryParse(dataNode?["count"].ToString(), out int totalCount);
				
					if (items is JArray itemsArray)
					{
						List<EventLog> eventList = new List<EventLog>();

						foreach (var jsonToken in itemsArray)
						{
							var eventItem = this.DeserializeEvent(jsonToken);
							if (eventItem != null)
							{
								eventList.Add(eventItem);
							}
						}
						
						return new ResponseResult<CollectionResponseData<EventLog>>(true)
						{
							Data = new CollectionResponseData<EventLog>
							{
								Items = eventList,
								Count = totalCount
							}
						};
					}
				}
				
				return new ResponseResult<CollectionResponseData<EventLog>>(false, "Events coul not deserialized!");
			}
			else
			{
				return new ResponseResult<CollectionResponseData<EventLog>>(false, response.Message);
			}
		}

		private EventLog DeserializeEvent(JToken jsonToken)
		{
			try
			{
				if (jsonToken is JObject jObject)
				{
					string itemJson = jsonToken.ToString();
					
					if (jObject.ContainsKey("type"))
					{
						EventLog eventItem = null;
				
						string eventType = jsonToken["type"].Value<string>();
				
						if (eventType == EventTypes.TokenCreatedEvent.ToString() ||
							eventType == EventTypes.TokenRefreshedEvent.ToString() ||
							eventType == EventTypes.TokenRevokedEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenEvent>(itemJson);		
						}
						else if (eventType == EventTypes.UserCreatedEvent.ToString() ||
							eventType == EventTypes.UserDeletedEvent.ToString() ||
							eventType == EventTypes.UserUpdatedEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<UserEvent>(itemJson);		
						}
						else if (eventType == EventTypes.ApplicationCreatedEvent.ToString() ||
							eventType == EventTypes.ApplicationDeletedEvent.ToString() ||
							eventType == EventTypes.ApplicationUpdatedEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationEvent>(itemJson);		
						}
						else if (eventType == EventTypes.RoleCreatedEvent.ToString() ||
							eventType == EventTypes.RoleDeletedEvent.ToString() ||
							eventType == EventTypes.RoleUpdatedEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<RoleEvent>(itemJson);		
						}
						else if (eventType == EventTypes.UserTypeUpdatedEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<UserTypeEvent>(itemJson);		
						}
						else if (eventType == EventTypes.PasswordChangedEvent.ToString() ||
							eventType == EventTypes.PasswordResetEvent.ToString())
						{
							eventItem = Newtonsoft.Json.JsonConvert.DeserializeObject<PasswordEvent>(itemJson);		
						}

						return eventItem;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Event could not deserialized!");
				Console.WriteLine(ex);
			}

			return null;
		}
		
		#endregion
	}
}