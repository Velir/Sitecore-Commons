using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Commons.Extensions
{
	///<summary>
	/// custom extension methods
	///</summary>
	public static class ItemExtensions
	{
		/// <summary>
		/// 	Will check the item's template, if deep is enabled it will check item's base templates
		/// This signature should be avoided because of potential performance issues. If you MUST recurse along template inheritance prefer passing depth as an int and work to minimised its value
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "deep"></param>
		/// <returns></returns>
		public static bool IsOfTemplate(this Item item, string templateId, bool deep)
		{
			return item.IsOfTemplate(templateId, (deep ? -1 : 0) );
		}

		/// <summary>
		/// 	Will check the item's template, if depth is non-zero it will recurse down the item's base templates
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "depth">0 = 'Do not recurse', -1 = 'recurse as far as system base template', n = 'recurse a maximum of n times'</param>
		/// <returns></returns>
		public static bool IsOfTemplate(this Item item, string templateId, int depth)
		{
			const string rootTemplateId = "{AB86861A-6030-46C5-B394-E8F99E8B87DB}";

			// Safety checks
			if (item.IsNull() || string.IsNullOrEmpty(item.TemplateID.ToString()))
			{
				return false;
			}
			if (depth < -1) // If depth is less than -1 something is wrong with recursion; error out
			{
				throw new ArgumentOutOfRangeException("depth", depth, @"Depth but be -1, 0, or greater.");
			}

			// Basic checks
			if (item.IsOfTemplate(templateId))
			{
				return true;
			}

			// Are we looking for descendants of the root template?  Everything is that.
			// TODO: Take this condition out, unless were explicitly looking at all levels, this may not match the rest of the functions behavior
			if ( (item.IsOfTemplate(rootTemplateId)) && (item.ID.ToString() == templateId))
			{
				return true;
			}

			// Reached maximum depth
			if ( (depth == 0) || (item.IsOfTemplate(rootTemplateId)))
			{
				return false; // halt recursion
			}

			// Check base templates
			// TODO - look into the impact of recursion on performance, particularly with respect to deep and broad trees.
			if (item.Template != null && item.Template.BaseTemplates != null && item.Template.BaseTemplates.Any())
			{
				foreach (TemplateItem baseTemplate in item.Template.BaseTemplates)
				{
					if (((Item)baseTemplate).IsNull())
					{// Log and continue on null base-templates.
						Log.Error(string.Format("Invalid base template, check the publication status for the base templates of item with guid {0} in db {1}", item.Name, item.Database.Name), "IsOfTemplate");
						continue;
					}

					// Recursively check templates
					if ( (baseTemplate.ID.ToString() == templateId) ||
						 (baseTemplate.IsTemplateOfTemplate(templateId, (depth > 0 ? depth - 1 : depth)))
						)
					{
						return true;
					}
				}
			}
			else
			{
				Log.Error(string.Format("Invalid base template, check the publication status for the base templates of item with guid {0} in db {1}", item.TemplateID, item.Database.Name), "IsOfTemplate");
			}

			//no matches
			return false;
		}

		/// <summary>
		/// 	Will check the item's template, if depth is non-zero it will recurse down the item's base templates
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name = "depth">0 = 'Do not recurse', -1 = 'recurse as far as system base template', n = 'recurse a maximum of n times'</param>
		/// <returns></returns>
		private static bool IsTemplateOfTemplate(this TemplateItem item, string templateId, int depth)
		{
			const string rootTemplateId = "{AB86861A-6030-46C5-B394-E8F99E8B87DB}";

			// Safety checks
			if (item == null || string.IsNullOrEmpty(item.ID.ToString()))
			{
				return false;
			}
			if (depth < -1) // If depth is less than -1 somthing is wrong with recursion; error out
			{
				throw new ArgumentOutOfRangeException("depth", depth, @"Depth but be -1, 0, or greater.");
			}

			// Basic checks
			if (item.ID.ToString() == templateId)
			{
				return true;
			}

			if (item.ID.ToString() == rootTemplateId)
			{
				return false;
			}

			// Reached maximum depth
			if (depth == 0)
			{
				return false; // halt recursion
			}

			// Check base templates
			// TODO - look into the impact of recursion on performance, particularly with respect to deep and broad trees.
			foreach (TemplateItem baseTemplate in item.BaseTemplates)
			{
				// Recursively check templates
				if ((baseTemplate.ID.ToString() == templateId) ||
					 (baseTemplate.IsTemplateOfTemplate(templateId, (depth > 0 ? depth - 1 : depth)))
					)
				{
					return true;
				}
			}

			//no matches
			return false;
		}

		/// <summary>
		/// 	check to verify that the item is of passed template
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <returns></returns>
		public static bool IsOfTemplate(this Item item, string templateId)
		{
			if (item.IsNull() || string.IsNullOrEmpty(item.TemplateID.ToString()))
			{
				return false;
			}

			return (item.TemplateID.ToString() == templateId);
		}

		/// <summary>
		/// 	Checks to see if an item is null
		/// </summary>
		/// <param name = "item"></param>
		/// <returns></returns>
		public static bool IsNull(this Item item)
		{
			return (item == null);
		}

		/// <summary>
		/// 	Checks to see if an item is not null
		/// </summary>
		/// <param name = "item"></param>
		/// <returns></returns>
		public static bool IsNotNull(this Item item)
		{
			return (!item.IsNull());
		}

		/// <summary>
		/// 	Crawls up the tree until the template is found
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, string templateId)
		{
			return item.GetAncestor(templateId, 0);
		}

		/// <summary>
		/// Gets the ancestor.  Recurses along the inheritance of an items template.  
		/// This signature should be avoided because of potential performance issues. If you MUST recurse along template inheritance prefer passing depth as an int and work to minimised its value
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="templateId">The template id.</param>
		/// <param name="deepInheritance">if set to <c>true</c> [deep inheritance].</param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, string templateId, bool deepInheritance)
		{
			return item.GetAncestor(templateId, (deepInheritance ? -1 : 0));
		}

		/// <summary>
		/// 	Crawls up the tree until the template is found.  Recurses along the inheritance of an items template.
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateId"></param>
		/// <param name="inheritanceDepth">0 = 'Do not recurse', -1 = 'recurse as far as system base template', n = 'recurse a maximum of n times'</param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, string templateId, int inheritanceDepth)
		{
			//check this item's template
			if (item.IsOfTemplate(templateId, inheritanceDepth))
			{
				return item;
			}

			//walk up the tree
			//need to reverse the list as sitecore returns getAncestors from the top down
			return item.Axes.GetAncestors().Reverse().Where(x => x.IsOfTemplate(templateId, inheritanceDepth)).FirstOrDefault();
		}

		/// <summary>
		/// Gets the ancestor.  Recurses along the inheritance of an items template.
		/// This signature should be avoided because of potential performance issues. 
		/// If you MUST recurse along template inheritance prefer passing depth as an int and work to minimized its value
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="templateIds">The template ids.</param>
		/// <param name="deepInheritance">if set to <c>true</c> [deep inheritance].</param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, List<string> templateIds, bool deepInheritance)
		{
			return item.GetAncestor(templateIds, (deepInheritance ? -1 : 0));
		}

		/// <summary>
		/// Gets the ancestor.  Recurses along the inheritance of an items template.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="templateIds">The template ids.</param>
		/// <param name="inheritanceDepth">The inheritance depth.</param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, List<string> templateIds, int inheritanceDepth)
		{
			return GetAncestor(item, templateIds, inheritanceDepth, true);
		}
		
		/// <summary>
		/// Gets the ancestor.  Recurses along the inheritance of an items template.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="templateIds">The template ids.</param>
		/// <param name="inheritanceDepth">The inheritance depth.</param>
		/// <param name="nodeUp">Bottom up ot top down search</param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, List<string> templateIds, int inheritanceDepth, bool nodeUp)
		{
			List<Item> ancestors = item.Axes.GetAncestors().ToList();
			//need to reverse the list as sitecore returns getAncestors from the top down
			if (nodeUp)
			{
				ancestors.Reverse();
			}

			foreach (Item ancestor in ancestors)
			{
				//verify item
				if (ancestor.IsNull())
				{
					continue;
				}

				//check item against templates
				foreach (string templateId in templateIds)
				{
					if (ancestor.IsOfTemplate(templateId, inheritanceDepth))
					{
						return ancestor;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 	Crawls up the tree until one of the passed templates are found and return that item.
		/// </summary>
		/// <param name = "item"></param>
		/// <param name = "templateIds"></param>
		/// <returns></returns>
		public static Item GetAncestor(this Item item, List<string> templateIds)
		{
			return item.GetAncestor(templateIds, 0);
		}

		/// <summary>
		/// Serialize Item to an xml file
		/// </summary>
		/// <param name="item"></param>
		/// <param name="folderPath"></param>
		public static void SerializeItem(this Item item, string folderPath)
		{
			//Write the file
			using (StreamWriter sw = new StreamWriter(@folderPath + @"\" + item.ID + " " + item.Name + ".xml"))
			{
				sw.Write(ItemSerializer.GetItemXml(item, false));
			}
		}

		/// <summary>
		/// Check against the item to see if it a media item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static bool IsMediaItem(this Item item)
		{
			return item.Paths.FullPath.ToLower().Contains("/sitecore/media library");
		}

		/// <summary>
		/// Gets a list of items linked to in fields of the item it is called on.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static IEnumerable<Item> GetRelatedItems(this Item item)
		{
			List<Item> retVal = new List<Item>();
			TemplateItem template = item.Template;
			foreach (TemplateFieldItem field in template.Fields)
			{
				//verify not a standard/system field
				if (field.InnerItem.IsNotNull() && field.InnerItem.Paths.FullPath.ToLower().StartsWith("/sitecore/templates/system"))
				{
					continue;
				}

				//get field value
				string fieldValue = item[field.Name];
				if (string.IsNullOrEmpty(fieldValue))
				{
					continue;
				}

				//split into guid array
				string[] values = fieldValue.Split('|');
				if (values.Length == 0)
				{
					continue;
				}

				foreach (string fieldValueItemId in values)
				{
					if (string.IsNullOrEmpty(fieldValueItemId))
					{
						continue;
					}

					ID additionalItemId;
					if (!ID.TryParse(fieldValueItemId, out additionalItemId))
					{
						continue;
					}

					// Try to get the related item, null-check it, if null, skip.
					Item relatedItem = item.Database.GetItem(additionalItemId);
					if (relatedItem.IsNull())
					{
						continue;
					}

					retVal.Add(relatedItem);
				}
			}

			return retVal;
		}
	}
}