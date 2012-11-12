using System.Text.RegularExpressions;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// Utilities for creating Sitecore items for imports
	/// </summary>
	public class CreateItemUtil
	{
		//Utility array for creating the alphabet folder structure
		public static string[] alphabetFolderNames = {
                                                   "123", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                                   "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                                                 };

		/// <summary>
		/// Checks to see if an item already exists under a parent. If it exists return the item,
		/// otherwise create and return the new item.  This defaults to using the master db.
		/// </summary>
		/// <param name="parentItem">The parent item.</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="template">The template.</param>
		/// <returns></returns>
		public static Item GetOrCreateItem(Item parentItem, string itemName, TemplateItem template)
		{
			Database masterDb = Factory.GetDatabase("master");
		  bool itemCreated;
			return GetOrCreateItem(parentItem, itemName, template, masterDb, out itemCreated);
		}

    /// <summary>
    /// Checks to see if an item already exists under a parent. If it exists return the item,
    /// otherwise create and return the new item.  This defaults to using the master db.
    /// 
    /// Added the override option to determine if the item was created or not. the out value of 
    /// itemCreated will return true if item has been created, false if the item already exists
    /// in sitecore and was found (and returned)
    /// 
    /// </summary>
    /// <param name="parentItem">The parent item.</param>
    /// <param name="itemName">Name of the item.</param>
    /// <param name="template">The template.</param>
    /// <param name="itemCreated"></param>
    /// <returns></returns>
    public static Item GetOrCreateItem(Item parentItem, string itemName, TemplateItem template, out bool itemCreated)
    {
      Database masterDb = Factory.GetDatabase("master");
      return GetOrCreateItem(parentItem, itemName, template, masterDb, out itemCreated);
    }

		/// <summary>
		/// Overriden without the itemCreated parameter to allow existing code to continue working
		/// </summary>
		/// <param name="parentItem">The parent item.</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="template">The template.</param>
		/// <param name="db">The db to use.</param>
		/// <returns>
		/// The found item if an item with the passed in name already existed under the parent,
		/// otherwise the newly created item.
		/// </returns>
	  public static Item GetOrCreateItem(Item parentItem, string itemName, TemplateItem template, Database db)
	  {
	    bool itemCreated;
	    return GetOrCreateItem(parentItem, itemName, template, db, out itemCreated);
	  }

		/// <summary>
		/// Checks to see if an item already exists under a parent. If it exists return the item,
		/// otherwise create and return the new item.
		/// </summary>
		/// <param name="parentItem">The parent item.</param>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="template">The template.</param>
		/// <param name="db">The db to use.</param>
		/// <param name="itemCreated">if set to <c>true</c> [item created].</param>
		/// <returns>
		/// The found item if an item with the passed in name already existed under the parent,
		/// otherwise the newly created item.
		/// </returns>
		/// <remarks>
		/// This has been updated to call the testable method.  
		/// Remaining logic is the item name cleaning.
		/// </remarks>
	  public static Item GetOrCreateItem(Item parentItem, string itemName, TemplateItem template, Database db, out bool itemCreated)
		{
			// Initialize itemCreated to be false.
			itemCreated = false;

			//If any of the passed in parameters are invalid, return null
			if (parentItem == null || string.IsNullOrEmpty(itemName) || template == null || db == null) return null;

			//Clean item name
			string cleanedItemName = ItemNameCleaner(itemName);

			// make a testable implementation of GetOrCreateItem with the interface
			ICreateItem iCreateItem;
			if (parentItem.Language != null)
			{
				iCreateItem = new CreateItem(parentItem.ID, cleanedItemName, template.ID, db.Name, parentItem.Language);
			}
			else
			{
				iCreateItem = new CreateItem(parentItem.ID, cleanedItemName, template.ID, db.Name);
			}
			ID itemId = GetOrCreateItem(iCreateItem, out itemCreated);
			return db.GetItem(itemId);
		}



		/// <summary>
		/// Gets the or create item.
		/// </summary>
		/// <param name="iCreateItem">The i create item.</param>
		/// <param name="itemCreated">if set to <c>true</c> [item created].</param>
		/// <returns></returns>
		/// <remarks>
		/// This is an updated version which has the complex dependencies removed
		/// So that it can be unit tested
		/// This method can/should be used directly, but for backwards compatability we still have the 
		/// overloads which call this
		/// </remarks>
		public static ID GetOrCreateItem(ICreateItem iCreateItem, out bool itemCreated)
		{
			// Initialize itemCreated to be false.
			itemCreated = false;
			if (iCreateItem == null) return (ID)null;

			// call the algorithm to see if the child already exists
			ID itemId = iCreateItem.GetChild();

			// if we didn't find the item, 
			// call the algorithm to create it
			if (itemId == (ID)null || ID.IsNullOrEmpty(itemId))
			{
				itemId = iCreateItem.CreateChild();
				itemCreated = true;
			}
			return itemId;
		}




		public const string ITEMNAMEFILTER_REGEX = @"[\/:\\?'<>|\[\]\-&@#\.%,\+\=;\{\}]";

		/// <summary>
		/// Cleans the Item Name - Changes & to "And", and removes disallowed characters
		/// </summary>
		/// <param name="itemName">The proposed item name</param>
		/// <returns>Cleaned Item name</returns>
		public static string ItemNameCleaner(string itemName)
		{
			string newName = itemName.Trim();

			newName = newName.Replace("&", "And");
			newName = newName.Replace("\"", " ");

			var regex = new Regex(ITEMNAMEFILTER_REGEX);
			var m = regex.Match(newName);
			if (m.Success)
			{
				// Do name replacement here
				newName = regex.Replace(newName, " ");
			}

			return newName.Trim();
		}

		/// <summary>
		/// Creates an alphabet folder structure under the passed in parent.  This will also inlcude 123 as a folder
		/// for handling items that start with numbers.  This defaults to lowercase letters for the folder names.
		/// </summary>
		/// <param name="parentItem">The parent item.</param>
		/// <param name="folderTemplate">The folder template.</param>
		public static void CreateAlphabetFolderStructure(Item parentItem, TemplateItem folderTemplate)
		{
			CreateAlphabetFolderStructure(parentItem, folderTemplate, false);
		}

		/// <summary>
		/// Creates an alphabet folder structure under the passed in parent.  This will also inlcude 123 as a folder
		/// for handling items that start with numbers.
		/// </summary>
		/// <param name="parentItem">The parent item.</param>
		/// <param name="folderTemplate">The folder template.</param>
		/// <param name="upperCase">if set to <c>true</c> make the letter folder name upper case.</param>
		public static void CreateAlphabetFolderStructure(Item parentItem, TemplateItem folderTemplate, bool upperCase)
		{
			if (parentItem == null || folderTemplate == null) return;

			Database masterDb = Factory.GetDatabase("master");
			using (new SecurityDisabler())
			{
				foreach (string letter in alphabetFolderNames)
				{
					//If we are supposed to make the folder name upper case, do so
					string folderName = letter;
					if (upperCase)
					{
						folderName = folderName.ToUpper();
					}

					//Only add the folder if it does not already exist, this way this method can be used to fill
					// in missing folders in an already existing partial alpha folder structure.
					string letterFolderPath = string.Format("{0}/{1}", parentItem.Paths.Path, folderName);
					Item alphaFolder = SitecoreItemFinder.GetItem(masterDb, letterFolderPath);
					if (alphaFolder == null)
					{
						parentItem.Add(letter.ToUpper(), folderTemplate);
					}
				}
			}
		}
	}
}