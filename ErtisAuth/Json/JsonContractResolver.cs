using System.Linq;
using System.Net.Http;
using System.Reflection;
using ErtisAuth.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ErtisAuth.Json
{
	public class JsonContractResolver : DefaultContractResolver
	{
		#region Properties

		private HttpMethod IgnoredHttpMethod { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public JsonContractResolver()
		{
			
		}
		
		/// <summary>
		/// Constructor 2
		/// </summary>
		/// <param name="ignoredHttmMethod"></param>
		public JsonContractResolver(HttpMethod ignoredHttmMethod)
		{
			this.IgnoredHttpMethod = ignoredHttmMethod;
		}

		#endregion
		
		#region Methods

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			if (this.IgnoredHttpMethod != null)
			{
				// For POST
				if (this.IgnoredHttpMethod == HttpMethod.Post)
				{
					var postModelAttribute = member.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(JsonIgnoreWhenPost));
					if (postModelAttribute != null)
					{
						if (postModelAttribute.ToString() == $"[{typeof(JsonIgnoreWhenPost).FullName}(({typeof(bool).Name}){true.ToString()})]")
						{
							property.ShouldSerialize = i => false;
							property.Ignored = true;
						}
					}		
				}
				
				// For PUT
				if (this.IgnoredHttpMethod == HttpMethod.Put)
				{
					var putModelAttribute = member.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(JsonIgnoreWhenPut));
					if (putModelAttribute != null)
					{
						if (putModelAttribute.ToString() == $"[{typeof(JsonIgnoreWhenPut).FullName}(({typeof(bool).Name}){true.ToString()})]")
						{
							property.ShouldSerialize = i => false;
							property.Ignored = true;
						}
					}		
				}
			}
			
			// For NULL
			var nullIgnoreAttribute = member.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(JsonIgnoreWhenNull));
			if (nullIgnoreAttribute != null)
			{
				property.ShouldSerialize = i =>
				{
					var propertyValue = property.ValueProvider.GetValue(i);
					if (propertyValue == null)
					{
						property.Ignored = true;
						return false;
					}

					return true;
				};
			}

			return property;
		}

		#endregion
	}
}