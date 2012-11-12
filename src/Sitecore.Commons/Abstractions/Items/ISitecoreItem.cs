using Sitecore.Data;
using Sitecore.SharedSource.Commons.Abstractions.Databases;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public interface ISitecoreItem
	{
		ID ID { get; }

		string DisplayName { get; }
		string Name { get; }

		IDatabase Database { get; }
	
		string this[ID fieldID] { get; set; }
		string this[string fieldName] { get; set; }

		string this[int fieldIndex] { get; }

		void BeginEdit();
		void EndEdit();

		IItemFactory ItemFactory { get; }
		IDatabaseFactory DatabaseFactory { get; }
	}
}