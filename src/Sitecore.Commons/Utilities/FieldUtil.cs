using System;
using System.Collections.Generic;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.SharedSource.Commons.Extensions;

namespace Sitecore.SharedSource.Commons.Utilities
{
	public class FieldUtil
	{
		public enum FieldType
		{
			Checkbox,
			Date,
			Datetime,
			File,
			Image,
			Integer,
			MultiLineText,
			Number,
			Password,
			RichText,
			SingleLineText,
			Checklist,
			Droplist,
			Multilist,
			Treelist,
			Droplink,
			Droptree,
			GeneralLink,
			InternalLink,
			DailyReportList,
			IFrame,
			TemplateFieldSource
		}

		/// <summary>
		/// 	Translates the field type string from Sitecore to the proper field type enum option
		/// </summary>
		/// <param name = "fieldTypeString">The field type string.</param>
		/// <returns></returns>
		public static FieldType GetFieldType(string fieldTypeString)
		{
			//Force the field type to lower in case the field names in Sitecore are capitalized oddly
			fieldTypeString = fieldTypeString.ToLower();

			switch (fieldTypeString)
			{
				case "checkbox":
					return FieldType.Checkbox;
				case "date":
					return FieldType.Date;
				case "datetime":
					return FieldType.Datetime;
				case "file":
					return FieldType.File;
				case "image":
					return FieldType.Image;
				case "integer":
					return FieldType.Integer;
				case "multi-line text":
					return FieldType.MultiLineText;
				case "number":
					return FieldType.Number;
				case "password":
					return FieldType.Password;
				case "rich text":
					return FieldType.RichText;
				case "single-line text":
					return FieldType.SingleLineText;
				case "checklist":
					return FieldType.Checklist;
				case "droplist":
				case "grouped droplist":
					return FieldType.Droplist;
				case "multilist":
					return FieldType.Multilist;
				case "treelistex":
				case "treelist":
					return FieldType.Treelist;
				case "droplink":
				case "grouped droplink":
					return FieldType.Droplink;
				case "droptree":
					return FieldType.Droptree;
				case "internal link":
					return FieldType.InternalLink;
				case "general link":
					return FieldType.GeneralLink;
				case "iframe":
					return FieldType.IFrame;
				case "dev single line text":
					return FieldType.SingleLineText;
				case "dev date":
					return FieldType.Date;
				case "daily report list":
					return FieldType.DailyReportList;
				case "template field source":
					return FieldType.TemplateFieldSource;
				default:
					throw new ArgumentException("Field Type Not Found - " + fieldTypeString);
			}
		}

		/// <summary>
		/// 	Checks if field page editable, this is based on the type of field that it is.
		/// </summary>
		/// <param name = "fieldType">Type of the field.</param>
		/// <returns></returns>
		public static bool CheckIfFieldPageEditable(FieldType fieldType)
		{
			switch (fieldType)
			{
				case FieldType.RichText:
				case FieldType.SingleLineText:
				case FieldType.MultiLineText:
				case FieldType.Image:
				case FieldType.File:
				case FieldType.Date:
				case FieldType.Datetime:
				case FieldType.GeneralLink:
					return true;

				default:
					return false;
			}
		}

		/// <summary>
		/// 	Checks if field should be excluded from Generator.
		/// </summary>
		/// <param name = "fieldType">Type of the field.</param>
		/// <returns></returns>
		public static bool CheckIfFieldIsExcluded(FieldType fieldType)
		{
			switch (fieldType)
			{
				case FieldType.IFrame:
					return true;

				default:
					return false;
			}
		}

		/// <summary>
		/// 	Gets the non system rich text fields from a FieldCollection.
		/// </summary>
		/// <param name = "fields">The fields.</param>
		/// <returns>A list of all of the rich text fields</returns>
		public static List<Field> GetUserRichTextFields(FieldCollection fields)
		{
			List<Field> richTextFields = new List<Field>();

			foreach (Field field in fields)
			{
				if (field.IsRichText())
				{
					richTextFields.Add(field);
				}
			}

			return richTextFields;
		}

		/// <summary>
		/// 	Gets the system fields from a FieldCollection.
		/// </summary>
		/// <param name = "fields">The fields.</param>
		/// <returns></returns>
		public static List<Field> GetSystemFields(FieldCollection fields)
		{
			List<Field> systemFields = new List<Field>();

			foreach (Field field in fields)
			{
				if (field.Name.StartsWith("__")) systemFields.Add(field);
			}

			return systemFields;
		}

		/// <summary>
		/// 	Gets both user nad system fields from a FieldCollection.
		/// </summary>
		/// <param name = "fields">The fields.</param>
		/// <returns></returns>
		public static List<Field> GetAllFields(FieldCollection fields)
		{
			List<Field> returnFields = new List<Field>();

			foreach (Field field in fields)
			{
				returnFields.Add(field);
			}

			return returnFields;
		}

		/// <summary>
		/// 	Gets the non system fields from a FieldCollection.
		/// </summary>
		/// <param name = "fields">The fields.</param>
		/// <returns></returns>
		public static List<Field> GetUserFields(FieldCollection fields)
		{
			List<Field> userFields = new List<Field>();

			foreach (Field field in fields)
			{
				if (!field.Name.StartsWith("__")) userFields.Add(field);
			}

			return userFields;
		}
	}
}