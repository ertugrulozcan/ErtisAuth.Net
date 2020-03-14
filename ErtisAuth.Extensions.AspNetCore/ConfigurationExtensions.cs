using ErtisAuth.Config;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Services;
using ErtisAuth.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ErtisAuth.Extensions.AspNetCore
{
	public static class ConfigurationExtensions
	{
		public static IErtisAuthConfiguration GetErtisAuthConfiguration(this IConfiguration configuration)
		{
			var ertisAuthConfiguration = configuration.GetSection("ErtisAuth");
			string baseUrl = ertisAuthConfiguration["BaseUrl"];
			string membershipId = ertisAuthConfiguration["MembershipId"];
			string adminToken = ertisAuthConfiguration["AdministratorToken"];
			
			return new ErtisAuthConfiguration(baseUrl, membershipId, adminToken);
		}
	}
}