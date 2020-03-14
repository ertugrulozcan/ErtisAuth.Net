using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Api.Endpoints.Users;
using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Extensions;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ErtisAuth.Services
{
	public class UserService : MembershipBoundedService, IUserService
	{
		#region Endpoints

		private readonly UsersEndpoint UsersEndpoint;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public UserService(IErtisAuthConfiguration configuration) : base(configuration)
		{
			this.UsersEndpoint = new UsersEndpoint(this.BaseUrl);
		}

		#endregion
		
		#region Methods

		public IResponseResult<User> Register(User user, string password)
		{
			return this.CreateUser(user.ToRegistrationForm(password), this.AdministratorToken);
		}

		public async Task<IResponseResult<User>> RegisterAsync(User user, string password)
		{
			return await this.CreateUserAsync(user.ToRegistrationForm(password), this.AdministratorToken);
		}

		public IResponseResult<IEnumerable<User>> GetUsers(string token, int? skip, int? limit, out int totalCount)
		{
			var collectionResponse = this.GetUsersAsync(token, skip, limit).ConfigureAwait(false).GetAwaiter().GetResult();
			if (collectionResponse.IsSuccess)
			{
				totalCount = collectionResponse.Data.Count;
				return new ResponseResult<IEnumerable<User>>(true) { Data = collectionResponse.Data.Items };
			}
			else
			{
				totalCount = 0;
				return new ResponseResult<IEnumerable<User>>(false, collectionResponse.Message);
			}
		}

		public async Task<IResponseResult<CollectionResponseData<User>>> GetUsersAsync(string token, int? skip, int? limit)
		{
			IQueryString queryString = null;
			if (skip != null)
				queryString = QueryString.Add("skip", skip.Value);
			if (limit != null)
				queryString = queryString == null ? QueryString.Add("limit", limit.Value) : queryString.Add("limit", limit.Value);
			
			var response = await this.UsersEndpoint.PostAsync<CollectionResponse<User>>(
				urlParams: new UsersEndpoint.UsersEndpointUrlParams().SetMembershipId(this.MembershipId).UseMongoQuery(),
				body: new RequestBody(),
				queryString: queryString,
				headers: HeaderCollection.Add("Authorization", $"Bearer {token}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<CollectionResponseData<User>>(true) { Data = response.Data.Data };
			}
			else
			{
				return new ResponseResult<CollectionResponseData<User>>(false, response.Message);
			}
		}

		public IResponseResult<User> GetUser(string userId, string accessToken)
		{
			return this.GetUserAsync(userId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<User>> GetUserAsync(string userId, string accessToken)
		{
			var response = await this.UsersEndpoint.GetAsync<User>(
				urlParams: new UsersEndpoint.UsersEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetUserId(userId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<User>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<User>(response.HttpCode.Value, response.Message);	
				}
				else
				{
					return new ResponseResult<User>(false, response.Message);
				}
			}
		}

		public IResponseResult<User> CreateUser(RegistrationForm userForm, string accessToken)
		{
			return this.CreateUserAsync(userForm, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<User>> CreateUserAsync(RegistrationForm userForm, string accessToken)
		{
			var response = await this.UsersEndpoint.PostAsync<User>(
				body: new RequestBody(userForm),
				urlParams: new UsersEndpoint.UsersEndpointUrlParams()
					.SetMembershipId(this.MembershipId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<User>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<User>(response.HttpCode.Value, response.Message);	
				}
				else
				{
					return new ResponseResult<User>(false, response.Message);
				}
			}
		}

		public IResponseResult<User> UpdateUser(User user, string accessToken)
		{
			return this.UpdateUserAsync(user, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<User>> UpdateUserAsync(User user, string accessToken)
		{
			var response = await this.UsersEndpoint.PutAsync<User>(
				body: new RequestBody(user),
				urlParams: new UsersEndpoint.UsersEndpointUrlParams()
					.SetMembershipId(this.MembershipId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult<User>(true) { Data = response.Data };
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<User>(response.HttpCode.Value, response.Message);	
				}
				else
				{
					return new ResponseResult<User>(false, response.Message);
				}
			}
		}

		public IResponseResult DeleteUser(string userId, string accessToken)
		{
			return this.DeleteUserAsync(userId, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult> DeleteUserAsync(string userId, string accessToken)
		{
			var response = await this.UsersEndpoint.DeleteAsync<User>(
				urlParams: new UsersEndpoint.UsersEndpointUrlParams()
					.SetMembershipId(this.MembershipId)
					.SetUserId(userId),
				headers: HeaderCollection.Add("Authorization", $"Bearer {accessToken}"));

			if (response.IsSuccess)
			{
				return new ResponseResult(true) { Data = response.Data };
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