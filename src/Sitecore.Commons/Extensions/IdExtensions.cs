using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data;

namespace Sitecore.SharedSource.Commons.Extensions
{
	public static class IdExtensions
	{
		/// <summary>
		/// Removes the left and right brackets of a sitecore item id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string RemoveBrackets(this ID id)
		{
			if (ID.IsNullOrEmpty(id))
			{
				return string.Empty;
			}

			string itemId = id.ToString().Replace("{", "");
			itemId = itemId.Replace("}", "");

			return itemId;
		}
	}
}
