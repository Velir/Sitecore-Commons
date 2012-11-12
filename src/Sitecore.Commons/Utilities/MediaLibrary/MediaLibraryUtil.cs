using System.IO;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Utilities.MediaLibrary
{
	/// <summary>
	/// 	General Media Library Utility methods
	/// </summary>
	public class MediaLibraryUtil
	{
		public static bool MediaItemIsValid(Database db, string mediaItemGuid, bool checkForFile, HttpRequest request)
		{
			return MediaItemIsValid(db, mediaItemGuid, checkForFile, request.PhysicalApplicationPath);
		}

		/// <summary>
		/// 	Checks to see if the passed in item guid represents a valid item.
		/// </summary>
		/// <param name = "db">The db.</param>
		/// <param name = "mediaItemGuid">The media item GUID.</param>
		/// <param name = "checkForFile">if set to <c>true</c> [check for file].</param>
		/// <param name = "applicationBasePath">The application base path.</param>
		/// <returns></returns>
		public static bool MediaItemIsValid(Database db, string mediaItemGuid, bool checkForFile, string applicationBasePath)
		{
			if (string.IsNullOrEmpty(mediaItemGuid) || db == null) return false;

			MediaItem mediaItem = SitecoreItemFinder.GetItem(db, mediaItemGuid);
			if (mediaItem == null) return false;

			if (checkForFile)
			{
				string filePath = applicationBasePath + mediaItem.FilePath.Replace("/", "\\");
				if (!File.Exists(filePath)) return false;
			}

			return true;
		}
	}
}