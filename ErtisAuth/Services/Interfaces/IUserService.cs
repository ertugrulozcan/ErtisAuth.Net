using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IUserService
	{
		IResponseResult<User> Register(User user, string password);
		
		Task<IResponseResult<User>> RegisterAsync(User user, string password);
		
		IResponseResult<IEnumerable<User>> GetUsers(string token, int? skip, int? limit, out int totalCount);
		
		Task<IResponseResult<CollectionResponseData<User>>> GetUsersAsync(string token, int? skip, int? limit);
		
		IResponseResult<User> GetUser(string userId, string accessToken);
		
		Task<IResponseResult<User>> GetUserAsync(string userId, string accessToken);
		
		IResponseResult<User> CreateUser(RegistrationForm userForm, string accessToken);
		
		Task<IResponseResult<User>> CreateUserAsync(RegistrationForm userForm, string accessToken);
		
		IResponseResult<User> UpdateUser(User user);
		
		Task<IResponseResult<User>> UpdateUserAsync(User user);
		
		IResponseResult<User> UpdateUser(User user, string accessToken);
		
		Task<IResponseResult<User>> UpdateUserAsync(User user, string accessToken);
		
		IResponseResult DeleteUser(string userId, string accessToken);
		
		Task<IResponseResult> DeleteUserAsync(string userId, string accessToken);
	}
}