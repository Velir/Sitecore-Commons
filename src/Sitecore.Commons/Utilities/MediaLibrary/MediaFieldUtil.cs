using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Utilities.MediaLibrary
{
	/// <summary>
	/// 	Methods for dealing with media library items in an import
	/// </summary>
	public class MediaFieldUtil
	{
		/// <summary>
		/// 	Gets the image field string for a specified media item.
		/// </summary>
		/// <param name = "mediaItem">The media item.</param>
		/// <returns></returns>
		public static string GetImageFieldXmlString(MediaItem mediaItem)
		{
			string mediaId = mediaItem.ID.ToString();
			string mediaPath = mediaItem.MediaPath;

			string mediaSrc = SitecoreLinkUtil.GetMediaSrc(Factory.GetDatabase("master"), mediaItem);

			return string.Format("<image mediaid=\"{0}\" mediapath=\"{1}\" src=\"{2}\" />",
			                     mediaId,
			                     mediaPath,
			                     mediaSrc);
		}

		/// <summary>
		/// 	Gets the file field string for a specified media item.
		/// </summary>
		/// <param name = "mediaItem">The media item.</param>
		/// <returns></returns>
		public static string GetFileFieldXmlString(MediaItem mediaItem)
		{
			string mediaId = mediaItem.ID.ToString();
			string mediaSrc = SitecoreLinkUtil.GetMediaSrc(Factory.GetDatabase("master"), mediaItem);

			return string.Format("<file mediaid=\"{0}\" src=\"{1}\" />",
			                     mediaId,
			                     mediaSrc);
		}

		/// <summary>
		/// 	Checks to see if the passed in file field is linking to a valid media item.
		/// </summary>
		/// <param name = "fileField">The file or image field.</param>
		/// <param name = "db">The Sitecore db.</param>
		/// <param name = "request">The request.</param>
		/// <returns></returns>
		public static bool FieldContainsValidMediaLink(FileField fileField, Database db, HttpRequest request)
		{
			return MediaLibraryUtil.MediaItemIsValid(db, fileField.MediaID.ToString(), true, request);
		}
	}
}