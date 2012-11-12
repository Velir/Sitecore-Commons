using System;
using Sitecore.Data.Fields;
using Sitecore.SharedSource.Commons.Utilities;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// 	Extensions methods for dealing with Fields
	/// </summary>
	public static class FieldExtensionsClass
	{
		/// <summary>
		/// 	Determines whether a Field is a Rich Text field
		/// </summary>
		/// <param name = "field">The field.</param>
		/// <returns>
		/// 	<c>true</c> if the field is a rich text field; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsRichText(this Field field)
		{
			if (field == null) throw new ArgumentNullException("field");

			FieldUtil.FieldType fieldType = FieldUtil.GetFieldType(field.Type);
			return fieldType == FieldUtil.FieldType.RichText;
		}
	}
}