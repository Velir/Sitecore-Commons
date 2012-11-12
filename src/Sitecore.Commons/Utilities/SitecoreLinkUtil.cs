using System;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Utilities for retrieving URLs to various things in Sitecore.
	/// </summary>
	public class SitecoreLinkUtil
	{
		/// <summary>
		/// Return the Url for for a specified item to show a certain device.  If successful
		/// this method will return the url to the item with the proper device url parameter
		/// appended.  If not successful return an empty string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		public static string GetDeviceUrlForItem(Item item, DeviceItem device)
		{
			//call over loaded method with null url options
			return GetDeviceUrlForItem(item, device, null);
		}

		/// <summary>
		/// Returns the url for a specified item and a specific device.  UrlOptions will be used
		/// if they are provided, but are not required.  If successful, a url for the item will be
		/// returned.  If not successfully, an empty string will be returned.
		/// </summary>
		/// <param name="item">The item to get the url for.</param>
		/// <param name="device">[optional] The device for the url.</param>
		/// <param name="urlOptions">[optional] The url parameters for the url.</param>
		/// <returns>The url to the item provided.</returns>
		public static string GetDeviceUrlForItem(Item item, DeviceItem device, UrlOptions urlOptions)
		{
			//if an item was not provided then just return empty string
			if (item == null)
			{
				return string.Empty;
			}

			//get our item url depending on if url options were provided or not
			string itemUrl = string.Empty;
			if (urlOptions != null)
			{
				itemUrl = LinkManager.GetItemUrl(item, urlOptions);
			}
			else
			{
				itemUrl = LinkManager.GetItemUrl(item);
			}

			//if our item url is null or empty then just return empty string
			if (string.IsNullOrEmpty(itemUrl))
			{
				return string.Empty;
			}

			//get our device url parameter
			string deviceUrlParam = string.Empty;
			if (device != null)
			{
				deviceUrlParam = device.QueryString;
			}

			//if we don't have a device url parameter then return our item url
			//this could be the default device, which does not have a url parameter.
			if (string.IsNullOrEmpty(deviceUrlParam))
			{
				return itemUrl;
			}

			//return our item url with the device url parameter attached
			return string.Concat(itemUrl, "?", deviceUrlParam);
		}

		/// <summary>
		/// 	Returns the URL for a media library item.  This will return an empty string
		/// 	if there are problems with the image, or if the media id points to a non
		/// 	existant image.  This method should be used instead of the src for an image field.
		/// 	That src field is not guaranteed to be updated if the media item moves, by using this
		/// 	method, even if the item moves, the URL will be correct.
		/// </summary>
		/// <param name = "db">The Sitecore db.</param>
		/// <param name = "image">The image custom item.</param>
		/// <returns>If a valid image is found, the URL to said image is returned.  If there are
		/// 	any problems then an empty string is returned</returns>
		public static string GetFullMediaUrl(Database db, MediaItem image)
		{
			//If either param is invalid return empty string
			if (image == null || db == null) return string.Empty;

			string mediaSrc = GetMediaSrc(db, image.InnerItem);
			if (!string.IsNullOrEmpty(mediaSrc))
			{
				//TODO Update this method to take potential other types of URLS, like https for example
				string serverInfo = string.Format("{0}://{1}", "http", HttpContext.Current.Request.Url.Host);
				return string.Format("{0}/{1}", serverInfo, mediaSrc);
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// 	Gets the media source for a media item in the specified database.  The path will come out in the following form
		/// 	~/media/{MEDIA PATH}.{MEDIA EXTENSION}
		/// </summary>
		/// <param name = "db">The sitecore database where the media item lives.</param>
		/// <param name = "mediaItem">The media item to look for.</param>
		/// <returns>
		/// 	The source to be used for the passed in media item, in the following form 
		/// 	~/media/{MEDIA PATH}.{MEDIA EXTENSION}
		/// </returns>
		public static string GetMediaSrc(Database db, MediaItem mediaItem)
		{
			if (db == null || mediaItem == null) return string.Empty;

			string mediaItemId = mediaItem.ID.ToString();
			//Check that the image ID is not empty
			if (!String.IsNullOrEmpty(mediaItemId))
			{
				//Check to see if the media id is a valid sitecore id
				if (ID.IsID(mediaItemId))
				{
					//Get the media path and encode any spaces with %20
					//TODO Probably better way to do general encoding here
					string mediaPath = mediaItem.MediaPath.Replace(" ", "%20");
					return string.Format("~/media{0}.{1}", mediaPath, mediaItem.Extension);
				}
				else
				{
					Log.Error(
						string.Format("Invalid media ID for item {0} in db {1}", mediaItem.Name, db.Name),
						"GetMediaSrc");
					return string.Empty;
				}
			}
			else
			{
				Log.Error(
					string.Format("Media ID is blank for item {0} in db {1}", mediaItem.Name, db.Name),
					"GetMediaSrc");
				return string.Empty;
			}
		}
	}
}