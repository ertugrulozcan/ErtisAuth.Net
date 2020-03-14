using ErtisAuth.Api.Endpoints;
using ErtisAuth.Api.Endpoints.Users;
using ErtisAuth.Infrastructure;
using NUnit.Framework;

namespace ErtisAuth.Tests
{
	public class EndpointTests : UnitTestBase
	{
		[SetUp]
		public void Setup()
		{
			
		}
		
		[Test]
		public void GetApiMapTest()
		{
			ApiMapEndpoint apiMapEndpoint = new ApiMapEndpoint(this.BASE_URL);
			var apiMapResponse = apiMapEndpoint.Get();
			if (apiMapResponse.IsSuccess)
			{
				
			}
		}
		
		[Test]
		public void GetUsersTest()
		{
			UsersEndpoint usersEndpoint = new UsersEndpoint(this.BASE_URL);
			string url = usersEndpoint.GenerateUrl(
				urlParams: UsersEndpoint.UrlParams.SetMembershipId("<MEMBERSHIP_ID>").SetUserId("<USER_ID>"),
				queryString: QueryString.Add("key", "<API_KEY>"));

			Assert.AreEqual($"{this.BASE_URL}/memberships/<MEMBERSHIP_ID>/users/<USER_ID>?key=<API_KEY>", url);
		}
		
		[Test]
		public void GetUsersWithQueryTest()
		{
			UsersEndpoint usersEndpoint1 = new UsersEndpoint(this.BASE_URL);
			string url1 = usersEndpoint1.GenerateUrl(
				urlParams: UsersEndpoint.UrlParams.SetMembershipId("<MEMBERSHIP_ID>").SetUserId("<USER_ID>").UseMongoQuery(),
				queryString: QueryString.Add("key", "<API_KEY>"));

			Assert.AreEqual($"{this.BASE_URL}/memberships/<MEMBERSHIP_ID>/users/<USER_ID>/_query?key=<API_KEY>", url1);
			
			UsersEndpoint usersEndpoint2 = new UsersEndpoint(this.BASE_URL);
			string url2 = usersEndpoint2.GenerateUrl(
				urlParams: UsersEndpoint.UrlParams.SetMembershipId("<MEMBERSHIP_ID>").UseMongoQuery(),
				queryString: QueryString.Add("key", "<API_KEY>"));

			Assert.AreEqual($"{this.BASE_URL}/memberships/<MEMBERSHIP_ID>/users/_query?key=<API_KEY>", url2);
		}
	}
}