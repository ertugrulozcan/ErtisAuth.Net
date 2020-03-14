using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Core.Models.Applications;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IApplicationService
	{
		IResponseResult<Application> GetApplication(string applicationId, string accessToken);
		
		Task<IResponseResult<Application>> GetApplicationAsync(string applicationId, string accessToken);
		
		IResponseResult<IEnumerable<Application>> GetApplications(string accessToken, int? skip, int? limit, out int totalCount);
		
		Task<IResponseResult<CollectionResponseData<Application>>> GetApplicationsAsync(string accessToken, int? skip, int? limit);
		
		IResponseResult<Application> CreateApplication(Application application, string accessToken);
		
		Task<IResponseResult<Application>> CreateApplicationAsync(Application application, string accessToken);
		
		IResponseResult<Application> UpdateApplication(Application application, string accessToken);
		
		Task<IResponseResult<Application>> UpdateApplicationAsync(Application application, string accessToken);
		
		IResponseResult DeleteApplication(string applicationId, string accessToken);
		
		Task<IResponseResult> DeleteApplicationAsync(string applicationId, string accessToken);
	}
}