using System.Net.Http;
using ErtisAuth.Api.Endpoints.Diagnostics;
using ErtisAuth.Api.Endpoints.Users;
using ErtisAuth.Infrastructure;
using NUnit.Framework;

namespace ErtisAuth.Tests
{
	public class UserEndpointTests : UnitTestBase
	{
		[SetUp]
		public void Setup()
		{
			
		}
		
		[Test]
		public void GetUsersTest()
		{
			// https://api.tahminmatik.com/api/v1/memberships/<MEMBERSHIP_ID>/users/<USER_ID>
			UsersEndpoint usersEndpoint = new UsersEndpoint(this.BASE_URL);
			var responseResult = usersEndpoint.Get(
				urlParams: UsersEndpoint.UrlParams.SetMembershipId("<MEMBERSHIP_ID>").SetUserId("<USER_ID>"),
				body: "<BODY>",
				queryString: QueryString.Add("key", "<API_KEY>"),
				headers: HeaderCollection.Add("Authorization", "<BEARER_TOKEN>"));

			if (responseResult.IsSuccess)
			{
				
			}
		}
	}
}