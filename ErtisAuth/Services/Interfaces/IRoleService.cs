using System.Collections.Generic;
using System.Threading.Tasks;
using ErtisAuth.Core.Models.Response;
using ErtisAuth.Core.Models.Roles;
using ErtisAuth.Infrastructure;

namespace ErtisAuth.Services.Interfaces
{
	public interface IRoleService
	{
		IResponseResult<Role> GetRole(string roleId, string accessToken);
		
		Task<IResponseResult<Role>> GetRoleAsync(string roleId, string accessToken);
		
		IResponseResult<IEnumerable<Role>> GetRoles(string accessToken, int? skip, int? limit, out int totalCount);
		
		Task<IResponseResult<CollectionResponseData<Role>>> GetRolesAsync(string accessToken, int? skip, int? limit);
		
		IResponseResult<Role> CreateRole(Role role, string accessToken);
		
		Task<IResponseResult<Role>> CreateRoleAsync(Role role, string accessToken);
		
		IResponseResult<Role> UpdateRole(Role role, string accessToken);
		
		Task<IResponseResult<Role>> UpdateRoleAsync(Role role, string accessToken);
		
		IResponseResult DeleteRole(string roleId, string accessToken);
		
		Task<IResponseResult> DeleteRoleAsync(string roleId, string accessToken);
	}
}