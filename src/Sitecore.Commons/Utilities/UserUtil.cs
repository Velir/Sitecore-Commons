using Sitecore.Security.Authentication;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Methods for dealing with Sitecore Membership users
	/// </summary>
	public class UserUtil
	{
		/// <summary>
		/// 	Logs in the user to the specified domain.
		/// </summary>
		/// <param name = "userName">Name of the user.</param>
		/// <param name = "userPassword">The user password.</param>
		/// <param name = "domain">The domain to log the user into</param>
		public static bool LoginUser(string userName, string userPassword, string domain)
		{
			return AuthenticationManager.Login(domain + "\\" + userName, userPassword);
		}
	}
}