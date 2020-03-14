using System;
using System.Collections.Generic;
using ErtisAuth.Config;
using ErtisAuth.Ioc;

namespace ErtisAuth.Boot
{
	public class Bootstrapper
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options"></param>
		public Bootstrapper(IErtisAuthConfiguration options)
		{
			ServiceProvider.Current.RegisterInstance(options);
		}

		#endregion
		
		#region Methods
		
		public IDictionary<Type, object> InitializeServices()
		{
			var serviceDictionary = new Dictionary<Type, object>();
			
			var serviceTypes = ServiceProvider.Current.GetServiceContracts();
			if (serviceTypes != null)
			{
				foreach (var serviceContractType in serviceTypes)
				{
					var serviceImpl = ServiceProvider.Current.GetService(serviceContractType);
					if (serviceImpl != null)
					{
						Console.WriteLine($"{serviceImpl.GetType().Name} resolved.");
						serviceDictionary.Add(serviceContractType, serviceImpl);
					}
				}
			}
			
			return serviceDictionary;
		}

		#endregion
	}
}