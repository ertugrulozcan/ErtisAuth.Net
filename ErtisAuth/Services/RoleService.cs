using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Roles;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Core.Models.Roles;
using ErtisAuth.Infrastructure;
using ErtisAuth.Queries.MongoQueries;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class RoleService : MembershipBoundedService, IRoleService
	{
		#region Endpoints

		private readonly RolesEndpoint RolesEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public RoleService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.RolesEndpoint = new RolesEndpoint(this.BaseUrl);
		}

		#endregion

		#region Methods

		public IResponseResult<Role> GetRole(string roleId, string accessToken)
		{
			return this.GetRoleAsync(roleId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Role>> GetRoleAsync(string roleId, string accessToken)
		{
			var response = await this.RolesEndpoint.GetAsync<Role>(
				new RolesEndpoint.RolesEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetRoleId(roleId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Role>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Role>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Role>(false, response.Message);
				}
			}
		}

		public IResponseResult<IEnumerable<Role>> GetRoles(string accessToken, out int totalCount, int? skip = null, int? limit = null)
		{
			var collectionResponse = this.GetRolesAsync(accessToken, skip, limit).ConfigureAwait(false).GetAwaiter().GetResult();
			if (collectionResponse.IsSuccess)
			{
				totalCount = collectionResponse.Data.Count;
				return new ResponseResult<IEnumerable<Role>>(true) { Data = collectionResponse.Data.Items };
			}
			else
			{
				totalCount = 0;
				return new ResponseResult<IEnumerable<Role>>(false, collectionResponse.Message);
			}
		}

		public async Task<IResponseResult<CollectionResponseData<Role>>> GetRolesAsync(string accessToken, int? skip = null, int? limit = null)
		{
			IQueryString queryString = null;
			if (skip != null)
				queryString = QueryString.Add("skip", skip.Value);
			if (limit != null)
				queryString = queryString == null ? QueryString.Add("limit", limit.Value) : queryString.Add("limit", limit.Value);
			
			var response = await this.RolesEndpoint.PostAsync<CollectionResponse<Role>>(
				urlParams: new RolesEndpoint.RolesEndpointUrlParams().SetMembershipId(this.MembershipId).UseMongoQuery(),
				body: new RequestBody(),
				queryString: queryString,
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<CollectionResponseData<Role>>(true) { Data = response.Data.Data };
			}
			else
			{
				return new ResponseResult<CollectionResponseData<Role>>(false, response.Message);
			}
		}

		public IResponseResult<Role> CreateRole(Role role, string accessToken)
		{
			return this.CreateRoleAsync(role, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Role>> CreateRoleAsync(Role role, string accessToken)
		{
			var response = await this.RolesEndpoint.PostAsync<Role>(
				new RolesEndpoint.RolesEndpointUrlParams()
					.SetMembershipId(this.MembershipId),
				body: new RequestBody(role),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Role>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Role>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Role>(false, response.Message);
				}
			}
		}

		public IResponseResult<Role> UpdateRole(Role role, string accessToken)
		{
			return this.UpdateRoleAsync(role, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<Role>> UpdateRoleAsync(Role role, string accessToken)
		{
			var response = await this.RolesEndpoint.PutAsync<Role>(
				new RolesEndpoint.RolesEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetRoleId(role.Id),
				body: new RequestBody(role),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<Role>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<Role>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<Role>(false, response.Message);
				}
			}
		}

		public IResponseResult DeleteRole(string roleId, string accessToken)
		{
			return this.DeleteRoleAsync(roleId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult> DeleteRoleAsync(string roleId, string accessToken)
		{
			var response = await this.RolesEndpoint.DeleteAsync(
				new RolesEndpoint.RolesEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetRoleId(roleId),
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
		
		public IResponseResult<CollectionResponseData<Role>> Search(string key, string accessToken) => this.SearchAsync(key, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<CollectionResponseData<Role>>> SearchAsync(string key, string accessToken)
		{
			var response = await this.RolesEndpoint.PostAsync<CollectionResponse<Role>>(
				urlParams: new RolesEndpoint.RolesEndpointUrlParams().SetMembershipId(this.MembershipId).UseMongoQuery(),
				body: new RequestBody(QueryBuilder.Where(QueryBuilder.Search(key)), RequestBody.BodyTypes.MongoQuery),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<CollectionResponseData<Role>>(true) { Data = response.Data.Data };
			}
			else
			{
				return new ResponseResult<CollectionResponseData<Role>>(false, response.Message);
			}
		}
		
		#endregion
	}
}