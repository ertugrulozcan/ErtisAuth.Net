using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Users;

namespace ErtisAuth.Extensions
{
	public static class UserExtensions
	{
		public static RegistrationForm ToRegistrationForm(this User user, string password)
		{
			return new RegistrationForm
			{
				Id = user.Id,
				Username = user.Username,
				EmailAddress = user.EmailAddress,
				FirstName = user.FirstName,
				LastName = user.LastName,
				MembershipId = user.MembershipId,
				Profile = user.Profile,
				Providers = user.Providers,
				Role = user.Role,
				Status = user.Status,
				Sys = user.Sys,
				EmailVerified = user.EmailVerified,
				IPInformation = user.IPInformation,
				Password = password
			};
		}
	}
}