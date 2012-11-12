using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Commons.Extensions;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Utility methods for serializing Sitecore objects to disk
	/// </summary>
	public class SerializationUtil
	{
		/// <summary>
		/// 	Serializes the items of a specified template recursively from the passed in root down to a file on disk.
		/// </summary>
		/// <param name = "folderPath">The folder path where the serialized items will be written to.</param>
		/// <param name = "rootGuid">The root GUID.</param>
		/// <param name = "templateName">Name of the template.</param>
		/// <param name = "db">The db.</param>
		public static void SerializeItemsOfTemplate(string folderPath, string rootGuid, string templateName, Database db)
		{
			Item contentRoot = SitecoreItemFinder.GetItem(db, rootGuid);
			if (contentRoot != null)
			{
				SerializeItemList(folderPath,
				                  (from Item child in contentRoot.Axes.GetDescendants()
				                   where child.Template.Name == templateName
				                   select child).ToList());
			}
		}

		/// <summary>
		/// 	Serializes a single item to a file on disk.
		/// </summary>
		/// <param name = "folderPath">The folder path where the serialized item will be written to.</param>
		/// <param name = "itemGuid">The item GUID.</param>
		/// <param name = "db">The db.</param>
		public static void SerializeItem(string folderPath, string itemGuid, Database db)
		{
			Item item = SitecoreItemFinder.GetItem(db, itemGuid);
			if (item == null) return;

			item.SerializeItem(folderPath);
		}

		/// <summary>
		/// 	Serializes a list of items to disk.
		/// </summary>
		/// <param name = "folderPath">The folder path where the serialized items will be written to.</param>
		/// <param name = "items">The items.</param>
		/// <param name = "db">The db.</param>
		public static void SerializeItemList(string folderPath, List<Item> items)
		{
			foreach (Item item in items)
			{
				if(item.IsNull())
				{
					continue;
				}

				item.SerializeItem(folderPath);
			}
		}
	}
}