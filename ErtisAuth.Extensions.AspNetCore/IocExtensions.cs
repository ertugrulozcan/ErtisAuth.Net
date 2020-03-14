using ErtisAuth.Boot;
using Microsoft.Extensions.DependencyInjection;

namespace ErtisAuth.Extensions.AspNetCore
{
	public static class IocExtensions
	{
		public static void InitializeServices(this Bootstrapper bootstrapper, IServiceCollection services)
		{
			var serviceImplementations = bootstrapper.InitializeServices();
			foreach (var serviceContract in serviceImplementations)
			{
				var serviceType = serviceContract.Key;
				var serviceImpl = serviceContract.Value;
				services.AddSingleton(serviceType, serviceImpl);
			}
		}
	}
}