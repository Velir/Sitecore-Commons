using System;
using System.Collections.Generic;
using System.Reflection;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Commons.Extensions;

namespace Sitecore.SharedSource.Commons.ItemReference
{
	public partial class ItemReferenceObject
	{
		/// <summary>
		/// Verified that the references exist in the context database
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static List<ItemReferenceObject> CheckReferences(Type type)
		{
			if(Sitecore.Context.Database == null)
			{
				return null;
			}

			return CheckReferences(type, Sitecore.Context.Database);
		}

		/// <summary>
		/// Verified that the references exist in the specified database
		/// </summary>
		/// <param name="type"></param>
		/// <param name="database"></param>
		/// <returns></returns>
		public static List<ItemReferenceObject> CheckReferences(Type type, Database database)
		{
			if (type == null || database == null)
			{
				return new List<ItemReferenceObject>();
			}

			//use reflection to bring back all item references
			List<ItemReferenceObject> failedItems = new List<ItemReferenceObject>();
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
			foreach (PropertyInfo property in properties)
			{
				ItemReferenceObject itemReferenceObject = (ItemReferenceObject)property.GetValue(type, null);
				if (itemReferenceObject == null)
				{
					continue;
				}

				//verification item exists
				Item item = database.GetItem(itemReferenceObject.Guid);
				if (item.IsNull())
				{
					//item does not exist, flag to be returned
					failedItems.Add(itemReferenceObject);
				}
			}

			return failedItems;
		}
	}
}
