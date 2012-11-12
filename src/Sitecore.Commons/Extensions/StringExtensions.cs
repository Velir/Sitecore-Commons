using System.Linq;
using System.Text.RegularExpressions;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// 	Extends the string class for custom methods
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// 	Gets the item by url
		/// </summary>
		/// <param name = "url"></param>
		/// <returns></returns>
		public static Item GetItemByUrlParts(this string url)
		{
			Database database = Sitecore.Context.Database;
			if(database == null)
			{
				return null;
			}
			return GetItemByUrlParts(url, database);
		}

		/// <summary>
		/// Gets the item by url
		/// </summary>
		/// <param name = "url"></param>
		/// <param name = "database"></param>
		/// <returns></returns>
		public static Item GetItemByUrlParts(this string url, Database database)
		{
			return GetItemByUrlParts(url, database, true);
		}

		/// <summary>
		/// 	Gets the item by url
		/// </summary>
		/// <param name = "url"></param>
		/// <param name = "database"></param>
		/// <param name="replaceDashes"></param>
		/// <returns></returns>
		public static Item GetItemByUrlParts(this string url, Database database, bool replaceDashes)
		{
			//set variables and clean up strings
			url = url.ToLower();

			if (replaceDashes)
			{
				url = url.Replace("-", " ");
			}

			//remove parameters
			if (url.Contains(".aspx"))
			{
				int aspxIndex = url.IndexOf(".aspx");
				url = url.Substring(0, aspxIndex);
			}

			//remove host
			Regex r = new Regex("^.*?://.*?(/.*)$");
			Match m = r.Match(url.ToLower());

			//verify we have a match
			if (m.Groups.Count < 2)
			{
				return null;
			}

			//bring back the item path without the host
			string itemPath = string.Format("/sitecore/content/home{0}", m.Groups[1].Value);

			//get item from sitecore
			Item item = database.GetItem(itemPath);
			if (item == null)
			{
				return null;
			}

			return item;
		}

		/// <summary>
		/// takes this string
		/// /sitecore/content/Home/Certified Education/Certified Learning/EO_MKM_0512/EO_Activity 0512/Module EAO
		/// and returns this string
		/// /#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#
		/// 
		/// Expects a leading slash and no trailing slash.
		/// This is what we get when calling InnerItem.Paths.path;
		/// </summary>
		/// <remarks>
		/// The xpath processor attempts to interpret certain words as keywords if they are found in an item's path.
		/// Examples include: "and", "or", and names with leading numbers
		/// </remarks>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string QueryEscape(this string path)
		{
			if (string.IsNullOrEmpty(path)) return null;

			// split on each '/' segment
			string[] escaped = path.Split('/').Where(x => !string.IsNullOrEmpty(x)).Select(x => string.Format("#{0}#", x)).ToArray();
			// rejoin the string
			string rejoined = string.Format("/{0}", string.Join("/", escaped));
			return rejoined;
		}

		///// <summary>
		///// takes this string
		///// /sitecore/content/Home/Certified Education/Certified Learning/EO_MKM_0512/EO_Activity 0512/Module EAO
		///// and returns this string
		///// /#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#
		///// also takes this string
		///// query:/#sitecore#/content/Home/Certified Education/#Certified Learning#/EO_MKM_0512/EO_Activity 0512/Module EAO//*[TemplateId='{00000000-0000-0000-0000-000000000000}']
		///// and returns this string
		///// query:/#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#//*[TemplateId='{00000000-0000-0000-0000-000000000000}']
		///// 
		///// Allows no leading slash and allows trailing slash
		///// Can act on any query string
		///// </summary>
		///// <remarks>
		///// The xpath processor attempts to interpret certain words as keywords if they are found in an item's path.
		///// Examples include: "and", "or", and names with leading numbers
		///// </remarks>
		///// <param name="path"></param>
		///// <returns></returns>
		//public static string QueryEscape(this string path)
		//{
		//  string retVal = "";
		//  if (string.IsNullOrEmpty(path)) return retVal;

		//  // split on each '/' segment
		//  string[] escaped = path.Split(new char[] { '/' }, StringSplitOptions.None).Select(x => string.Format("#{0}#", x.Trim(new char[] { '#' }))).ToArray();
		//  // rejoin the string
		//  retVal = string.Join("/", escaped);

		//  // Resolve over-formatting
		//  retVal = retVal.Replace("##", "");
		//  int firstOctothorpe = retVal.IndexOf('#');
		//  retVal = (firstOctothorpe < 0) ? retVal : retVal.Remove(firstOctothorpe, retVal.Length);
		//  int lastOctothorpe = retVal.LastIndexOf('#');
		//  retVal = (lastOctothorpe < 0) ? retVal : retVal.Remove(lastOctothorpe, retVal.Length);

		//  return retVal;
		//}
	}
}