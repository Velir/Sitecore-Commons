using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// Collection of extensions methods that operate on an Item
	/// and return a list of child items using the Fast Query algorithm.
	/// </summary>
	public static class FastQueryExtensions
	{
		/// <summary>
		/// Use Fast Query to get all immediate child items.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item)
		{
			// default to shallow
			return item.GetFastQueryItems(Sitecore.Context.Database, FastQueryOptions.Shallow);
		}

		/// <summary>
		/// Use Fast Query to get all child items.
		/// Option decides whether to get immediate children or all decendants
		/// </summary>
		/// <param name="item"></param>
		/// <param name="fqOption"></param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item, FastQueryOptions fqOption)
		{
			return item.GetFastQueryItems(Sitecore.Context.Database, fqOption);
		}

		/// <summary>
		/// Use Fast Query to get all child items.
		/// Option decides whether to get immediate children or all decendants
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="db">The db.</param>
		/// <param name="fqOption">The fq option.</param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item, Database db, FastQueryOptions fqOption)
		{
			if (item == null) return null;

			string path = item.Paths.Path;
			string fastPath = path.QueryEscape();
			string fastQueryString;

			if (fqOption == FastQueryOptions.Deep)
			{
				fastQueryString = string.Format("fast:{0}//*", fastPath);
			}
			// default to shallow
			else
			{
				fastQueryString = string.Format("fast:{0}/*", fastPath);
			}
			Item[] items = db.SelectItems(fastQueryString);

			return items;
		}

		/// <summary>
		/// Use Fast Query to get all immediate child items.
		/// child items must match the template
		/// </summary>
		/// <param name="item"></param>
		/// <param name="templateId"></param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item, string templateId)
		{
			// default to shallow
			return item.GetFastQueryItems(templateId, FastQueryOptions.Shallow);
		}

		/// <summary>
		/// Use Fast Query to get all child items.
		/// Option decides whether to get immediate children or all decendants
		/// child items must match the template
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="templateId">The template id.</param>
		/// <param name="fqOption">The fast query option.</param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item, string templateId, FastQueryOptions fqOption)
		{
			return item.GetFastQueryItems(Sitecore.Context.Database, templateId, fqOption);
		}

		/// <summary>
		/// Use Fast Query to get all child items.
		/// Option decides whether to get immediate children or all decendants
		/// child items must match the template
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="db">The db.</param>
		/// <param name="templateId">The template id.</param>
		/// <param name="fqOption">The fast query option.</param>
		/// <returns></returns>
		public static Item[] GetFastQueryItems(this Item item, Database db, string templateId, FastQueryOptions fqOption)
		{
			if (item == null || string.IsNullOrEmpty(templateId)) return null;

			string path = item.Paths.Path;
			string fastPath = path.QueryEscape();
			string fastQueryString;

			if (fqOption == FastQueryOptions.Deep)
			{
				fastQueryString = string.Format("fast:{0}//*[@@templateid='{1}']", fastPath, templateId);
			}
			// default to shallow
			else
			{
				fastQueryString = string.Format("fast:{0}/*[@@templateid='{1}']", fastPath, templateId);
			}

			Item[] items = db.SelectItems(fastQueryString);

			return items;
		}
	}



	public enum FastQueryOptions
	{
		Shallow,
		Deep
	}
}
