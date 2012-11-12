using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public sealed class ItemFactory : IItemFactory
	{
		public IItem BuildItem(Item item)
		{
			return new ItemWrapper(item);
		}

		public IEnumerable<IItem> BuildItems(IEnumerable<Item> items)
		{
			foreach (Item item in items)
			{
				yield return BuildItem(item);
			}
		}

		public ICustomItem BuildItem(CustomItem customItem)
		{
			return new CustomItemWrapper(customItem);
		}

		public IEnumerable<ICustomItem> BuildItems(IEnumerable<CustomItem> customItems)
		{
			foreach (CustomItem customItem in customItems)
			{
				yield return BuildItem(customItem);
			}
		}

		#region Singleton implementation
		private ItemFactory()
		{ }

		public static ItemFactory Instance { get { return Nested._instance; } }

		private class Nested
		{
			static Nested() { }
			internal static readonly ItemFactory _instance = new ItemFactory();
		}
		#endregion
	}
}
