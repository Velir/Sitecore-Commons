using Sitecore.Data;

namespace Sitecore.SharedSource.Commons.Abstractions.Databases
{
	public class DatabaseFactory : IDatabaseFactory
	{
		public IDatabase BuildDatabase(Database database)
		{
			return new DatabaseWrapper(database);
		}

		#region Singleton implementation
		private DatabaseFactory()
		{ }

		public static DatabaseFactory Instance { get { return Nested._instance; } }

		private class Nested
		{
			static Nested() { }
			internal static readonly DatabaseFactory _instance = new DatabaseFactory();
		}
		#endregion
	}
}