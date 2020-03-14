using System.Linq;
using System.Reflection;
using ErtisAuth.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ErtisAuth.Json
{
	public class BlupointJsonContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			var blupointPostModelAttribute = member.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(JsonIgnoreWhenPost));
			
			if (blupointPostModelAttribute != null)
			{
				if (blupointPostModelAttribute.ToString() == $"[{typeof(JsonIgnoreWhenPost).FullName}(({typeof(bool).Name}){true.ToString()})]")
				{
					property.ShouldSerialize = i => false;
					property.Ignored = true;	
				}
			}
			
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
	}
}