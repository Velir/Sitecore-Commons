using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public interface IItemFactory
	{
		IItem BuildItem(Item item);
		IEnumerable<IItem> BuildItems(IEnumerable<Item> items);

		ICustomItem BuildItem(CustomItem customItem);
		IEnumerable<ICustomItem> BuildItems(IEnumerable<CustomItem> customItems);
	}
}