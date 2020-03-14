using System;

namespace ErtisAuth.Annotations
{
	/// <summary>
	/// Bu annotation ile flag'lenmiş propertyler'de, post ve put metodu için request body'si serialize edilirken IsIgnored:false olarak işaretlenmiş property'ler json'a dahil edilmez.
	/// Yalnızca property'ler için kullanılır.
	/// Serializer ayarlarında ContractResolver'ın JsonContractResolver ile override edilmesi gerekir. Aksi taktirde bir işe yaramaz.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonIgnoreWhenPut : Attribute
	{
		
	}
}