using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Applications;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Applications;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class ApplicationService : MembershipBoundedService, IApplicationService
	{
		#region Endpoints

		private readonly ApplicationsEndpoint ApplicationsEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public ApplicationService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.ApplicationsEndpoint = new ApplicationsEndpoint(this.BaseUrl);
		}

		#endregion
		
		#region Methods

		public IResponseResult<Application> GetApplication(string applicationId, string accessToken)
		{
			return this.GetApplicationAsync(applicationId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Application>> GetApplicationAsync(string applicationId, string accessToken)
		{
			var response = await this.ApplicationsEndpoint.GetAsync<Application>(
				new ApplicationsEndpoint.ApplicationsEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetApplicationId(applicationId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Application>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Application>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Application>(false, response.Message);
				}
			}
		}

		public IResponseResult<IEnumerable<Application>> GetApplications(string accessToken, int? skip, int? limit, out int totalCount)
		{
			var collectionResponse = this.GetApplicationsAsync(accessToken, skip, limit).ConfigureAwait(false).GetAwaiter().GetResult();
			if (collectionResponse.IsSuccess)
			{
				totalCount = collectionResponse.Data.Count;
				return new ResponseResult<IEnumerable<Application>>(true) { Data = collectionResponse.Data.Items };
			}
			else
			{
				totalCount = 0;
				return new ResponseResult<IEnumerable<Application>>(false, collectionResponse.Message);
			}
		}

		public async Task<IResponseResult<CollectionResponseData<Application>>> GetApplicationsAsync(string accessToken, int? skip, int? limit)
		{
			IQueryString queryString = null;
			if (skip != null)
				queryString = QueryString.Add("skip", skip.Value);
			if (limit != null)
				queryString = queryString == null ? QueryString.Add("limit", limit.Value) : queryString.Add("limit", limit.Value);
			
			var response = await this.ApplicationsEndpoint.PostAsync<CollectionResponse<Application>>(
				urlParams: new ApplicationsEndpoint.ApplicationsEndpointUrlParams().SetMembershipId(this.MembershipId).UseMongoQuery(),
				body: new RequestBody(),
				queryString: queryString,
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<CollectionResponseData<Application>>(true) { Data = response.Data.Data };
			}
			else
			{
				return new ResponseResult<CollectionResponseData<Application>>(false, response.Message);
			}
		}

		public IResponseResult<Application> CreateApplication(Application application, string accessToken)
		{
			return this.CreateApplicationAsync(application, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Application>> CreateApplicationAsync(Application application, string accessToken)
		{
			var response = await this.ApplicationsEndpoint.PostAsync<Application>(
				new ApplicationsEndpoint.ApplicationsEndpointUrlParams()
					.SetMembershipId(this.MembershipId),
				body: new RequestBody(application),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Application>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Application>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Application>(false, response.Message);
				}
			}
		}

		public IResponseResult<Application> UpdateApplication(Application application, string accessToken)
		{
			return this.UpdateApplicationAsync(application, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Application>> UpdateApplicationAsync(Application application, string accessToken)
		{
			var response = await this.ApplicationsEndpoint.PutAsync<Application>(
				new ApplicationsEndpoint.ApplicationsEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetApplicationId(application.Id),
				body: new RequestBody(application),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Application>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Application>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Application>(false, response.Message);
				}
			}
		}

		public IResponseResult DeleteApplication(string applicationId, string accessToken)
		{
			return this.DeleteApplicationAsync(applicationId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult> DeleteApplicationAsync(string applicationId, string accessToken)
		{
			var response = await this.ApplicationsEndpoint.DeleteAsync(
				new ApplicationsEndpoint.ApplicationsEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetApplicationId(applicationId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult(true);
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult(false, response.Message);
				}
			}
		}

		#endregion
	}
}