using System;
using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Utilities
{
	///<summary>
	///</summary>
	public class ItemUtil
	{
		///<summary>
		///	Provides a boolean value for Checkboxes.
		///</summary>
		///<param name = "item">Sitecore Item with Checkbox</param>
		///<param name = "fieldName">Name of Checkbox Field</param>
		///<returns></returns>
		public static bool GetCheckboxValue(Item item, String fieldName)
		{
			//Return false if the fieldname is blank or null
			if (string.IsNullOrEmpty(fieldName)) return false;

			bool fieldValue = false;

			if (item != null && item.Fields[fieldName] != null)
			{
				if (item.Fields[fieldName].Value.Equals("1"))
					fieldValue = true;
			}

			return fieldValue;
		}

		/// <summary>
		/// 	Return a string value from a Sitecore text based field
		/// </summary>
		/// <param name = "item">Sitecore Item with a text field</param>
		/// <param name = "fieldName">Name of Sitecore field</param>
		/// <returns>String</returns>
		public static String GetTextFieldValue(Item item, String fieldName)
		{
			String fieldValue = String.Empty;
			if (item != null && item.Fields[fieldName] != null)
			{
				fieldValue = item.Fields[fieldName].ToString().Trim();
			}
			return fieldValue;
		}

		/// <summary>
		/// 	Returns a DateTime value from a Sitecore date/time field. If the item does not
		/// 	contain a valid DateField, or the DateField is not set, then DateTime.MinValue
		/// 	will be returned.
		/// </summary>
		/// <param name = "item">Sitecore Item with a date/time field</param>
		/// <param name = "fieldName">Name of Sitecore field</param>
		/// <returns></returns>
		public static DateTime GetDateFieldValue(Item item, String fieldName)
		{
			DateTime fieldValue = DateTime.MinValue;
			if (item != null && item.Fields[fieldName] != null)
			{
				fieldValue = DateUtil.IsoDateToDateTime(item.Fields[fieldName].ToString());
			}
			return fieldValue;
		}

		//TODO - TMB - Does this need to be replaced due to the fact the GeneralLinkField is now deprecated?
		///<summary>
		///	Returns a LinkField value from a Sitecore LinkField.  If the field is null,
		///	or does not contain a value, it'll create an empty LinkField with a Url of "#"
		///</summary>
		///<param name = "item">Sitecore Item with a LinkField</param>
		///<param name = "fieldName">Name of Sitecore Field</param>
		///<returns>Valid LinkField</returns>
		//public static GeneralLinkField GetLinkFieldValue(Item item, String fieldName)
		//{
		//    GeneralLinkField linkField = new GeneralLinkField(item.Fields[fieldName]);

		//    return linkField;
		//}

		/// <summary>
		/// 	Returns a hash map of TemplateItems comprised of all the base templates of the 
		/// 	specified Sitecore item. Notice that the lookup stops at the "Standard Template", so base 
		/// 	templates such as "Appearance", "Masters", "Workflow", etc. won't be included in the list.
		/// </summary>
		/// <param name = "item">the Sitecore item</param>
		/// <returns>A dictionary of TemplateItems, keyed by name.</returns>
		public static IDictionary<string, TemplateItem> GetBaseTemplates(Item item)
		{
			Dictionary<string, TemplateItem> dict = new Dictionary<string, TemplateItem>();
			if (item != null)
			{
				AddBaseTemplates(dict, item.Template);
			}
			return dict;
		}

		/// <summary>
		/// 	Recursively look up base templates for the specified item and add them to the list. 
		/// 	Notice that the lookup stops at the "Standard Template", so base templates such as
		/// 	"Appearance", "Masters", "Workflow", etc. won't be included in the list.
		/// </summary>
		/// <param name = "dict">Dictionary of TemplateItems, keyed by name.</param>
		/// <param name = "template">The item's template from which we want the base templates.</param>
		private static void AddBaseTemplates(Dictionary<string, TemplateItem> dict, TemplateItem template)
		{
			foreach (TemplateItem ti in template.BaseTemplates)
			{
				if (!ti.Name.Equals("Standard template")) //stop recursing at the standard template
				{
					AddBaseTemplates(dict, ti); //add the base templates of the base template
				}

				if (!dict.ContainsKey(ti.Name)) //don't add the same template twice
				{
					dict.Add(ti.Name, ti);
				}
			}
		}

		/// <summary>
		/// 	Determines whether a given sitecore item descends from the specified base template.
		/// 	Notice that the lookup stops at the "Standard Template", so base templates such as
		/// 	"Appearance", "Masters", "Workflow", etc. won't be included in the search.
		/// </summary>
		/// <param name = "item">a sitecore item</param>
		/// <param name = "templateName">the name of the base template you're looking for</param>
		/// <returns></returns>
		public static bool HasBaseTemplate(Item item, string templateName)
		{
			IDictionary<string, TemplateItem> baseTemplates = GetBaseTemplates(item);
			if (baseTemplates.ContainsKey(templateName))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 	Determines whether a given Sitecore item is of a certain template or descends from
		/// 	the provided tempalte Id.  Standard Tempaltes are excluded.
		/// </summary>
		/// <param name = "templateId">String Template Id</param>
		/// <param name = "item">Sitecore Item</param>
		/// <returns>bool</returns>
		public static bool HasRelationToTemplate(string templateId, Item item)
		{
			return HasRelationToTemplate(Context.Database, templateId, item);
		}

		/// <summary>
		/// 	Determines whether a given Sitecore item is of a certain template or descends from
		/// 	the provided tempalte Id.  Standard Tempaltes are excluded.
		/// </summary>
		/// <param name = "db">Sitecore Database</param>
		/// <param name = "templateId">String Template Id</param>
		/// <param name = "item">Sitecore Item</param>
		/// <returns>bool</returns>
		public static bool HasRelationToTemplate(Database db, string templateId, Item item)
		{
			TemplateItem itemTemplate = db.GetItem(templateId);
			//If the item is of the current template type or has a base template of the current type
			//return true.
			if (item != null && (item.TemplateName.Equals(itemTemplate.Name) ||
			                     HasBaseTemplate(item, itemTemplate.Name)))
			{
				return true;
			}

			return false;
		}
	}
}