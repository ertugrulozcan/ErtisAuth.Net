namespace ErtisAuth.Queries.MongoQueries
{
	public interface IQuery
	{
		IQuery Value { get; }

		string ToInnerString();
	}
}