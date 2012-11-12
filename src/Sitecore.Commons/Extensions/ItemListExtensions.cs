using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Extensions
{
	///<summary>
	///	Extensions methods for a list of items
	///</summary>
	public static class ItemListExtensions
	{
		///<summary>
		///	Checks to see if the passed item is part of the current list
		///</summary>
		///<param name = "items"></param>
		///<param name = "item"></param>
		///<returns></returns>
		public static bool ContainsItem(this IList<Item> items, Item item)
		{
			Item foundItem = items.Where(x => x.ID == item.ID).FirstOrDefault();
			return (foundItem != null);
		}

		/// <summary>
		/// Checks to see if the passed id is part of the current list
		/// </summary>
		/// <param name="list"></param>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static bool ContainsItem(this List<Item> list, string itemId)
		{
			return (list.Where(x => x.ID.ToString() == itemId).FirstOrDefault() != null);
		}
	}
}