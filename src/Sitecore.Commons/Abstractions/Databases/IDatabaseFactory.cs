using Sitecore.Data;

namespace Sitecore.SharedSource.Commons.Abstractions.Databases
{
	public interface IDatabaseFactory
	{
		IDatabase BuildDatabase(Database database);
	}
}