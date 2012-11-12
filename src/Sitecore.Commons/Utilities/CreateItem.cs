using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Utilities
{

	/// <summary>
	/// Operations related to creating new items
	/// and first checking to see if the item already exists
	/// </summary>
	public class CreateItem : ICreateItem
	{

		public ID ParentId { get; set; }
		public string CleanName { get; set; }
		public ID TemplateId { get; set; }
		public string DbName { get; set; }
		public Language Language { get; set; }

		public CreateItem(ID parentId, string cleanName, ID templateId, string dbName, Language language)
		{
			ParentId = parentId;
			CleanName = cleanName;
			TemplateId = templateId;
			DbName = dbName;
			Language = language;
		}

		public CreateItem(ID parentId, string cleanName, ID templateId, string dbName)
		{
			ParentId = parentId;
			CleanName = cleanName;
			TemplateId = templateId;
			DbName = dbName;
		}

		/// <summary>
		/// Gets the child.
		/// </summary>
		/// <remarks>
		/// The algorithm is important for performs
		/// a) first get the parent item
		/// b) then check its children to see if the proposed item exists
		/// we don't want to look for the proposed item by path because it is slow if not found
		/// </remarks>
		/// <returns></returns>
		public virtual ID GetChild()
		{
			// return value for null
			ID nullId = ID.Null;
			// validate
			if (string.IsNullOrEmpty(CleanName) || ParentId == (ID)null || string.IsNullOrEmpty(DbName)) return nullId;

			// get and validate database
			Database db = Factory.GetDatabase(DbName);
			if(db == null) return nullId;

			// disable security for checking if the item exists
			using (new SecurityDisabler())
			{
				// get and validate parent item
				Item parentItem = Language == null ? db.GetItem(ParentId) : db.GetItem(ParentId, Language);
				if (parentItem == null) return nullId;

				// review the parent's children to see if there is one with this name
				Item foundItem = parentItem.Children.FirstOrDefault(x => x.Name.Equals(CleanName));
				return foundItem != null ? foundItem.ID : nullId;
			}
		}

		/// <summary>
		/// Creates the child.
		/// </summary>
		/// <returns></returns>
		public virtual ID CreateChild()
		{
			// return value for null
			ID nullId = ID.Null;
			// validate
			if (string.IsNullOrEmpty(CleanName) || ParentId == (ID)null || TemplateId == (ID)null || string.IsNullOrEmpty(DbName)) return nullId;

			// get and validate database
			Database db = Factory.GetDatabase(DbName);
			if (db == null) return nullId;

			// disable security for creating the new item
			using (new SecurityDisabler())
			{
				// get and validate parent item
				Item parentItem = Language == null ? db.GetItem(ParentId) : db.GetItem(ParentId, Language);
				if (parentItem == null) return nullId;

				// meat
				Item childItem = parentItem.Add(CleanName, new TemplateID(TemplateId));
				return childItem != null ? childItem.ID : nullId;
			}
		}
	}
}
