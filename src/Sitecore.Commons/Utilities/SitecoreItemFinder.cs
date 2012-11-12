using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Methods to help with getting items out of Sitecore
	/// </summary>
	public class SitecoreItemFinder
	{
		/// <summary>
		/// 	A convenience override for the get item call that defaults the database to the current context database.
		/// </summary>
		/// <param name = "path">The path of the item to find.</param>
		/// <returns></returns>
		public static Item GetItemFromCurrentDatabase(string path)
		{
			return GetItem(Context.Database, path);
		}

		/// <summary>
		/// 	Wraps the Sitecore get item call.  This will return null if a blank path is passed in, instead of erroring out.  This also
		/// 	checks for a null DB reference.
		/// </summary>
		/// <param name = "db">The Sitecore db to search for the item.</param>
		/// <param name = "path">The path of the item to find.</param>
		/// <returns>The item if it is found, otherwise it will return null.</returns>
		public static Item GetItem(Database db, string path)
		{
			if (!string.IsNullOrEmpty(path) && db != null)
			{
				return db.GetItem(path);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 	Get the sub items of an item that are using the specified template.
		/// </summary>
		/// <param name = "rootItem">The root item.</param>
		/// <param name = "template">The template to check for.</param>
		/// <param name = "recursive">if set to <c>true</c> all sub items will be searched, otherwise only the first level children
		/// 	will be looked at.</param>
		/// <returns>A list of items below the root item that are using the passed in template.</returns>
		public static List<Item> GetSubItemsOfTemplate(Item rootItem, TemplateItem template, bool recursive)
		{
			if (recursive)
			{
				return (from Item child in rootItem.Axes.GetDescendants()
				        where child.Template.ID == template.ID
				        select child).ToList();
			}
			else
			{
				return (from Item child in rootItem.Children
				        where child.Template.ID == template.ID
				        select child).ToList();
			}
		}

		/// <summary>
		/// 	Get the sub items of an item that are not using the specified template.
		/// </summary>
		/// <param name = "rootItem">The root item.</param>
		/// <param name = "template">The template to check for.</param>
		/// <param name = "recursive">if set to <c>true</c> all sub items will be searched, otherwise only the first level children
		/// 	will be looked at.</param>
		/// <returns>A list of items below the root item that are not using the passed in template.</returns>
		public static List<Item> GetSubItemsNotOfTemplate(Item rootItem, TemplateItem template, bool recursive)
		{
			if (recursive)
			{
				return (from Item child in rootItem.Axes.GetDescendants()
				        where child.Template.ID != template.ID
				        select child).ToList();
			}
			else
			{
				return (from Item child in rootItem.Children
				        where child.Template.ID != template.ID
				        select child).ToList();
			}
		}

		/// <summary>
		/// 	Gets items from a sitecore pipe delimited GUID list.
		/// </summary>
		/// <param name = "guidList">The GUID list.</param>
		/// <param name = "db">The db.</param>
		/// <returns></returns>
		public static List<Item> GetItemsFromSitecoreGuidList(string guidList, Database db)
		{
			return GetItemsFromGuidListString(guidList, '|', db);
		}

		/// <summary>
		/// 	Gets items from a delimited GUID list.
		/// </summary>
		/// <param name = "guidList">The delimited GUID list.</param>
		/// <param name = "listSeperator">The list delimiter.</param>
		/// <param name = "db">The db.</param>
		/// <returns></returns>
		public static List<Item> GetItemsFromGuidListString(string guidList, char listSeperator, Database db)
		{
			return GetItemsFromGuidList(guidList.Split(listSeperator).ToList(), db);
		}

		/// <summary>
		/// 	Gets items from a list of passed in guids.
		/// </summary>
		/// <param name = "guidList">The GUID list.</param>
		/// <param name = "db">The db.</param>
		/// <returns></returns>
		public static List<Item> GetItemsFromGuidList(List<string> guidList, Database db)
		{
			if (guidList == null || db == null) return new List<Item>();

			List<Item> items = new List<Item>();
			foreach (string guid in guidList)
			{
				Item tempItem = GetItem(db, guid);
				if (tempItem != null)
				{
					items.Add(tempItem);
				}
			}

			return items;
		}
	}
}