using ErtisAuth.Annotations;
using ErtisAuth.Core.Models.Common;
using Newtonsoft.Json;

namespace ErtisAuth.Core.Models.Events
{
	public abstract class EventLog : ResourceBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties

		[JsonIgnore]
		public string Title
		{
			get
			{
				return this.Name;
			}
		}
		
		[JsonProperty("type")]
		[JsonIgnoreWhenNull]
		public string Name { get; set; }

		[JsonIgnore]
		public EventTypes Type
		{
			get
			{
				if (this.Name == "TokenCreatedEvent")
					return EventTypes.TokenCreatedEvent;
				else if (this.Name == "TokenRefreshedEvent")
					return EventTypes.TokenRefreshedEvent;
				else if (this.Name == "TokenRevokedEvent")
					return EventTypes.TokenRevokedEvent;
				else if (this.Name == "UserCreatedEvent")
					return EventTypes.UserCreatedEvent;
				else if (this.Name == "UserUpdatedEvent")
					return EventTypes.UserUpdatedEvent;
				else if (this.Name == "UserDeletedEvent")
					return EventTypes.UserDeletedEvent;
				else if (this.Name == "ApplicationCreatedEvent")
					return EventTypes.ApplicationCreatedEvent;
				else if (this.Name == "ApplicationDeletedEvent")
					return EventTypes.ApplicationDeletedEvent;
				else if (this.Name == "ApplicationUpdatedEventt")
					return EventTypes.ApplicationUpdatedEvent;
				else if (this.Name == "RoleCreatedEvent")
					return EventTypes.RoleCreatedEvent;
				else if (this.Name == "RoleDeletedEvent")
					return EventTypes.RoleDeletedEvent;
				else if (this.Name == "RoleUpdatedEvent")
					return EventTypes.RoleUpdatedEvent;
				else if (this.Name == "PasswordChangedEvent")
					return EventTypes.PasswordChangedEvent;
				else if (this.Name == "PasswordResetEvent")
					return EventTypes.PasswordResetEvent;
				else if (this.Name == "UserTypeUpdatedEvent")
					return EventTypes.UserTypeUpdatedEvent;
				else
					return EventTypes.Unknown;
			}
		}
		
		[JsonProperty("utilizer")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public UtilizerBase Utilizer { get; set; }
		
		[JsonProperty("custom")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public object Custom { get; set; }
		
		[JsonProperty("membership_id")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public string MembershipId { get; set; }

		#endregion
	}
	
	public abstract class EventLog<TResourceType> : EventLog 
		where TResourceType : ResourceBase, IMembershipBoundedResource, IHasTitle
	{
		#region Properties

		[JsonProperty("document")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public TResourceType Document { get; set; }
		
		[JsonProperty("prior")]
		[JsonIgnoreWhenNull]
		[JsonIgnoreWhenPost]
		[JsonIgnoreWhenPut]
		public TResourceType Prior { get; set; }

		#endregion
	}
}