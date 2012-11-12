using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Extensions
{
	///<summary>
	/// custom extension methods
	///</summary>
	public static class CustomItemExtensions
	{
		/// <summary>
		/// 	Will check the item's template, if deep is enabled it will check item's base templates
		/// This signature should be avoided because of potential performance issues. 
		/// If you MUST recurse along template inheritance prefer passing depth as an int and work to minimised its value
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "deep"></param>
		/// <returns></returns>
		public static bool IsOfTemplate(this CustomItem item, string templateId, bool deep)
		{
			return (item.InnerItem.IsOfTemplate(templateId, deep));
		}

		/// <summary>
		/// 	Will check the item's template, if depth is non-zero it will recurse down the item's base templates
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "depth">0 = 'Do not recurse', -1 = 'recurse as far as system base template', n = 'recurse n times'</param>
		/// <returns></returns>
		public static bool IsOfTemplate(this CustomItem item, string templateId, int depth)
		{
			return (item.InnerItem.IsOfTemplate(templateId, depth));
		}

		/// <summary>
		/// 	check to verify that the item is of passed template
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <returns></returns>
		public static bool IsOfTemplate(this CustomItem item, string templateId)
		{
			return (item.InnerItem.IsOfTemplate(templateId));
		}

		/// <summary>
		/// 	Checks to see if an item is null
		/// </summary>
		/// <param name = "item"></param>
		/// <returns></returns>
		public static bool IsNull(this CustomItem item)
		{
			return (item == null || item.InnerItem.IsNull());
		}

		/// <summary>
		/// 	Checks to see if an item is not null
		/// </summary>
		/// <param name = "item"></param>
		/// <returns></returns>
		public static bool IsNotNull(this CustomItem item)
		{
			return (item != null && !item.InnerItem.IsNull());
		}

		/// <summary>
		/// 	Crawls up the tree until the template is found
		/// This signature may result in a performance hit. // TODO, paste around
		/// If you MUST recurse along template inheritance prefer passing depth as an int and work to minimised its value
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "deepInheritance"></param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, string templateId, bool deepInheritance)
		{
			return item.InnerItem.GetAncestor(templateId, deepInheritance);
		}

		/// <summary>
		/// 	Crawls up the tree until the template is found
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "inheritanceDepth">0 = 'Do not recurse', -1 = 'recurse as far as system base template', n = 'recurse n times'</param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, string templateId, int inheritanceDepth)
		{
			return item.InnerItem.GetAncestor(templateId, inheritanceDepth);
		}

		/// <summary>
		/// 	Crawls up the tree until the template is found
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, string templateId)
		{
			return item.InnerItem.GetAncestor(templateId);
		}

		/// <summary>
		/// 	Crawls up the tree until one of the passed templates are found and return that item.
		/// This signature should be avoided because of potential performance issues. 
		/// If you MUST recurse along template inheritance prefer passing depth as an int and work to minimised its value
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateIds"></param>
		/// <param name = "deepInheritance"></param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, List<string> templateIds, bool deepInheritance)
		{
			return item.InnerItem.GetAncestor(templateIds, deepInheritance);
		}

		/// <summary>
		/// 	Crawls up the tree until one of the passed templates are found and return that item.
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateIds"></param>
		/// <param name = "inheritanceDepth"></param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, List<string> templateIds, int inheritanceDepth)
		{
			return item.InnerItem.GetAncestor(templateIds, inheritanceDepth);
		}

		/// <summary>
		/// 	Crawls up the tree until one of the passed templates are found and return that item.
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateIds"></param>
		/// <returns></returns>
		public static Item GetAncestor(this CustomItem item, List<string> templateIds)
		{
			return item.InnerItem.GetAncestor(templateIds);
		}
	}
}