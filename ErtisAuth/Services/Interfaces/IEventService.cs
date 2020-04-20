using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Core.Models.Events;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IEventService
	{
		IResponseResult<EventLog> GetEvent(string accessToken, string eventId);

		Task<IResponseResult<EventLog>> GetEventAsync(string accessToken, string eventId);
		
		IResponseResult<IEnumerable<EventLog>> GetEvents(string accessToken, out int totalCount, int? skip = null, int? limit = null);

		Task<IResponseResult<CollectionResponseData<EventLog>>> GetEventsAsync(string accessToken, int? skip = null, int? limit = null);
	}
}