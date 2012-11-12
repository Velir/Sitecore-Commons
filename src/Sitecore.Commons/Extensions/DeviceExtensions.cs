using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Extensions
{
	/// <summary>
	/// 	Extensions methods for the Sitecore device object
	/// </summary>
	public static class DeviceExtensions
	{
		/// <summary>
		/// 	check the device item against a device id
		/// </summary>
		/// <param name = "deviceItem"></param>
		/// <param name = "deviceId"></param>
		/// <returns></returns>
		public static bool IsOfDevice(this DeviceItem deviceItem, string deviceId)
		{
			return (deviceItem.ID.ToString() == deviceId);
		}

		/// <summary>
		/// 	Checks to see if a device is null
		/// </summary>
		/// <param name = "deviceItem"></param>
		/// <returns></returns>
		public static bool IsNull(this DeviceItem deviceItem)
		{
			return (deviceItem == null);
		}

		/// <summary>
		/// 	Checks to see if a device is not null
		/// </summary>
		/// <param name = "deviceItem"></param>
		/// <returns></returns>
		public static bool IsNotNull(this DeviceItem deviceItem)
		{
			return (!deviceItem.IsNull());
		}
	}
}