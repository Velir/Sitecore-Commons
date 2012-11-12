using System;
using System.Collections.Generic;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.SharedSource.Commons.Utilities;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// 	Extension methods for dealing with FieldCollections
	/// </summary>
	public static class FieldCollectionExtensionsClass
	{
		public static List<Field> GetSystemFields(this FieldCollection fields)
		{
			if (fields == null) throw new ArgumentNullException("fields");
			return FieldUtil.GetSystemFields(fields);
		}

		public static List<Field> GetAllFields(this FieldCollection fields)
		{
			if (fields == null) throw new ArgumentNullException("fields");
			return FieldUtil.GetAllFields(fields);
		}

		public static List<Field> GetUserFields(this FieldCollection fields)
		{
			if (fields == null) throw new ArgumentNullException("fields");
			return FieldUtil.GetUserFields(fields);
		}

		public static List<Field> GetUserRichTextFields(this FieldCollection fields)
		{
			if (fields == null) throw new ArgumentNullException("fields");
			return FieldUtil.GetUserRichTextFields(fields);
		}
	}
}