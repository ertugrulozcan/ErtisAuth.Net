using System;

namespace ErtisAuth.Queries.MongoQueries
{
	public class MongoOperator : Attribute
	{
		public string Name { get; }

		public MongoOperator(string name)
		{
			if (string.IsNullOrEmpty(name) || !name.StartsWith('$'))
			{
				throw new InvalidOperationException("Invalid mongo operator attribute!");	
			}
			
			this.Name = name;
		}
	}
}