using System;

namespace ErtisAuth.Annotations
{
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonIgnoreWhenNull : Attribute
	{
		// Bu annotation ile flag'lenmiş propertyler değeri null olduğunda serialization işlemine dahil edilmez, json'a eklenmez.
		// Yalnızca property'ler için kullanılır.
		// Serializer ayarlarında ContractResolver'ın BlupointJsonContractResolver ile override edilmesi gerekir. Aksi taktirde bir işe yaramaz.
	}
}