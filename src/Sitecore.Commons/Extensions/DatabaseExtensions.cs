using Sitecore.Data;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// 	Extensions methods for the Sitecore device object
	/// </summary>
	public static class DatabaseExtensions
	{
		/// <summary>
		/// 	Checks to see if a database is null
		/// </summary>
		/// <param name = "database"></param>
		/// <returns></returns>
		public static bool IsNull(this Database database)
		{
			return (database == null);
		}

		/// <summary>
		/// 	Checks to see if a database is not null
		/// </summary>
		/// <param name = "database"></param>
		/// <returns></returns>
		public static bool IsNotNull(this Database database)
		{
			return (!database.IsNull());
		}
	}
}