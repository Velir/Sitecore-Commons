using System.Web.UI;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Utilities for dealing with presentation settings in Sitecore.
	/// </summary>
	public class PresentationUtil
	{
		/// <summary>
		/// 	Gets the name of the placeholder key that the passed in sublayout is bound to.  This will return an empty string
		/// 	if for some reason we cannot get the placeholder name.
		/// 
		/// 	You would use this from the code behind of a sublayout.  So for example if I had a sublayout sltList and 
		/// 	I wanted to know what placeholder it was bound to I would have something this in the code behind 
		/// 	"string plcKeyName = PresentationUtil.GetPlaceholderKeyName(this);"
		/// </summary>
		/// <param name = "sublayout">The sublayout.</param>
		/// <returns></returns>
		public static string GetPlaceholderKeyName(UserControl sublayout)
		{
			if (sublayout == null) return string.Empty;
			if (sublayout.Parent == null) return string.Empty;
			if (sublayout.Parent.Parent == null) return string.Empty;

			//Make sure my grandparent is a placeholder, if not return an empty string
			if (typeof (Placeholder) != sublayout.Parent.Parent.GetType()) return string.Empty;

			//The placeholder is my grandparent, so cast it and get the key value
			Placeholder grandParent = (Placeholder) sublayout.Parent.Parent;
			return grandParent.Key;
		}

		/// <summary>
		/// 	Checks whether the presenation settings are set directly on a template, this should ususally be set
		/// 	on the standard values.
		/// </summary>
		/// <param name = "template">The template to check.</param>
		/// <returns>True if the presenation settings are no empty on the template, false otherwise.</returns>
		public static bool TemplateHasPresentationSetDirectlyOnTemplate(TemplateItem template)
		{
			string renderingString = template.InnerItem.Fields["__renderings"].Value;
			return !string.IsNullOrEmpty(renderingString);
		}

		/// <summary>
		/// 	Gets the device definition for a given item and device.
		/// </summary>
		/// <param name = "theItem">The item.</param>
		/// <param name = "device">The device.</param>
		/// <returns>The device definition if it was found, null otherwise</returns>
		public static DeviceDefinition GetDeviceDefinition(Item theItem, DeviceItem device)
		{
			if (theItem == null || device == null) return null;

			//Make sure there is a renderings string
			string renderingString = theItem.Fields["__renderings"].Value;
			if (string.IsNullOrEmpty(renderingString)) return null;

			//Try and retrieve the layout definition by parsing the rendering string
			LayoutDefinition layout = LayoutDefinition.Parse(renderingString);
			if (layout == null) return null;

			//Try and get the device definition from the layout definition
			return layout.GetDevice(device.ID.ToString());
		}

		#region Methods for checking presentation settings

		/// <summary>
		/// 	Checks to see if the passed in item has presentation settings for the provided device and layout.
		/// </summary>
		/// <param name = "theItem">The item.</param>
		/// <param name = "device">The device.</param>
		/// <param name = "layoutItem">The layout item.</param>
		/// <returns>True if the item contains the device and layout in it's presentation settings, false otherwise.</returns>
		public static bool ItemHasPresentationSettingsForLayout(Item theItem, DeviceItem device, LayoutItem layoutItem)
		{
			if (theItem == null || device == null || layoutItem == null) return false;

			//If the item does not contain settings for the passed in device do not bother with the 
			// rest of the check.
			if (!ItemHasPresentationSettingsForDevice(theItem, device)) return false;

			//Try and get the device definition from the layout definition
			DeviceDefinition deviceDef = GetDeviceDefinition(theItem, device);
			if (deviceDef == null) return false;
			if (deviceDef.Layout == null || deviceDef.Renderings == null) return false;

			return (deviceDef.Layout == layoutItem.ID.ToString());
		}

		/// <summary>
		/// 	Checks to see if the passed in item has presentation settings for the provided device and sublayout.
		/// </summary>
		/// <param name = "theItem">The item.</param>
		/// <param name = "device">The device.</param>
		/// <param name = "sublayoutItem">The sublayout item.</param>
		/// <returns>True if the item contains the device and sublayout in it's presentation settings, false otherwise.</returns>
		public static bool ItemHasPresentationSettingsForSublayout(Item theItem, DeviceItem device,
		                                                           SublayoutItem sublayoutItem)
		{
			if (theItem == null || device == null || sublayoutItem == null) return false;

			//If the item does not contain settings for the passed in device do not bother with the 
			// rest of the check.
			if (!ItemHasPresentationSettingsForDevice(theItem, device)) return false;

			//Try and get the device definition from the layout definition
			DeviceDefinition deviceDef = GetDeviceDefinition(theItem, device);
			if (deviceDef == null) return false;

			//Try and get the rendering for the passed in display item
			RenderingDefinition rendering = deviceDef.GetRendering(sublayoutItem.ID.ToString());

			return !(rendering == null);
		}

		/// <summary>
		/// 	Checks to see if an item has presentation settings for the passed in device
		/// </summary>
		/// <param name = "theItem">The item to check.</param>
		/// <param name = "device">The device to check for.</param>
		/// <returns>True if the item contains the device in it's presentation settings, false otherwise.</returns>
		public static bool ItemHasPresentationSettingsForDevice(Item theItem, DeviceItem device)
		{
			if (theItem == null || device == null) return false;

			//Try and get the device definition from the layout definition
			DeviceDefinition deviceDef = GetDeviceDefinition(theItem, device);
			if (deviceDef == null) return false;

			//IF the device defintion layout or renderings are null, then the device does not have presentation 
			// settings for this item
			return !(deviceDef.Layout == null || deviceDef.Renderings == null);
		}

		#endregion
	}
}