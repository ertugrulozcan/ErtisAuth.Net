using System;
using System.Collections.Generic;
using System.Linq;
using ErtisAuth.Services;

namespace ErtisAuth.Ioc
{
	public class ServiceProvider
	{
		#region Constants

		private const string SERVICE_CONTRACTS_NAMESPACE = "ErtisAuth.Services.Interfaces";
		private const string SERVICE_IMPLEMENTATIONS_NAMESPACE = "ErtisAuth.Services";

		#endregion

		#region Singleton

		private static ServiceProvider self;
        
		public static ServiceProvider Current
		{
			get
			{
				if (self == null)
					self = new ServiceProvider();

				return self;
			}
		}

		#endregion

		#region Fields

		private Dictionary<Type, object> serviceDictionary = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		private ServiceProvider()
		{

		}

		#endregion

		#region Methods

		public void RegisterInstance<T>(T implementation, Type contractType = null) where T : class
		{
			if (self == null)
				throw new ArgumentException("ServiceProvider is not configured!");

			Type type = typeof(T);
			if (contractType != null)
			{
				if (implementation.GetType().GetInterfaces().Contains(contractType))
				{
					type = contractType;	
				}
				else
				{
					throw new InvalidOperationException($"Wrong type registered for service type of '{type.Name}'!");
				}
			}
			
			if (!self.serviceDictionary.ContainsKey(type))
			{
				self.serviceDictionary.Add(type, implementation);
			}
			else
			{
				self.serviceDictionary[type] = implementation;
			}
		}

		public T GetInstance<T>()
		{
			if (self == null)
				throw new ArgumentException("ServiceProvider is not configured!");

			Type type = typeof(T);
			if (self.serviceDictionary.ContainsKey(type))
			{
				return (T)self.serviceDictionary[type];
			}
			else
			{
				return default(T);
			}
		}
		
		public object GetInstance(Type type)
		{
			if (self == null)
				throw new ArgumentException("ServiceProvider is not configured!");

			if (self.serviceDictionary.ContainsKey(type))
			{
				return self.serviceDictionary[type];
			}
			else
			{
				return null;
			}
		}

		public bool IsRegistered<T>()
		{
			Type type = typeof(T);
			return this.IsRegistered(type);
		}

		public bool IsRegistered(Type type)
		{
			if (self == null)
				throw new ArgumentException("ServiceProvider is not configured!");

			return self.serviceDictionary.ContainsKey(type);
		}

		public IEnumerable<Type> GetServiceContracts()
		{
			var allTypes = typeof(BaseService).Assembly.GetTypes();
			return allTypes.Where(x => x.Namespace == SERVICE_CONTRACTS_NAMESPACE);
		}
		
		public IEnumerable<Type> GetServiceImplementations()
		{
			IEnumerable<Type> interfaces = this.GetServiceContracts();
			if (interfaces != null)
			{
				var services = new List<Type>();
				
				var allTypes = typeof(BaseService).Assembly.GetTypes();
				var serviceTypes = allTypes.Where(x => x.Namespace == SERVICE_IMPLEMENTATIONS_NAMESPACE);
				
				foreach (var contractType in interfaces)
				{
					var serviceImplementations = serviceTypes.Where(x => x.GetInterfaces().Contains(contractType));
					services.AddRange(serviceImplementations);
				}

				return services;
			}

			return null;
		}
		
		private IEnumerable<Type> GetServiceImplementationTypes<TService>()
		{
			return this.GetServiceImplementationTypes(typeof(TService));
		}
		
		private IEnumerable<Type> GetServiceImplementationTypes(Type type)
		{
			IEnumerable<Type> interfaces = this.GetServiceContracts();
			if (interfaces != null)
			{
				var allTypes = typeof(BaseService).Assembly.GetTypes();
				var serviceTypes = allTypes.Where(x => x.Namespace == SERVICE_IMPLEMENTATIONS_NAMESPACE);
				
				return serviceTypes.Where(x => x.GetInterfaces().Contains(type)).ToArray();
			}

			return null;
		}
		
		public TService GetService<TService>(object[] constructorParameters = null) where TService : class
		{
			var instance = this.GetInstance<TService>();
			if (instance != null)
			{
				return instance;
			}
			else
			{
				var serviceContracts = this.GetServiceContracts();
				if (serviceContracts.Contains(typeof(TService)))
				{
					var serviceImplementationTypes = this.GetServiceImplementationTypes<TService>();
					var serviceImplType = serviceImplementationTypes.FirstOrDefault();
					if (serviceImplType != null)
					{
						if (Activator.CreateInstance(serviceImplType, args: constructorParameters) is TService implementation)
						{
							this.RegisterInstance(implementation);
							return implementation;
						}
					}
				}
			}

			return default(TService);
		}
		
		public object GetService(Type type)
		{
			var instance = this.GetInstance(type);
			if (instance != null)
			{
				return instance;
			}
			else
			{
				var serviceContracts = this.GetServiceContracts();
				if (serviceContracts.Contains(type))
				{
					var serviceImplementationTypes = this.GetServiceImplementationTypes(type);
					var serviceImplType = serviceImplementationTypes.FirstOrDefault();
					if (serviceImplType != null)
					{
						var constructorParameters = new List<object>();
						var constructors = serviceImplType.GetConstructors();
						if (constructors.Any())
						{
							foreach (var constructorInfo in constructors)
							{
								var parameters = constructorInfo.GetParameters();
								foreach (var parameterInfo in parameters)
								{
									var parameterValue = this.GetInstance(parameterInfo.ParameterType);
									constructorParameters.Add(parameterValue);
								}
							}
						}
						
						var implementation = Activator.CreateInstance(serviceImplType, args: constructorParameters.ToArray());
						if (implementation != null)
						{
							this.RegisterInstance(implementation, type);
							return implementation;
						}
					}
				}
			}

			return null;
		}

		#endregion
	}
}