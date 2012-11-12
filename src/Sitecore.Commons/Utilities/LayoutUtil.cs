using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using Sitecore.Exceptions;

namespace Sitecore.SharedSource.Commons.Utilities
{
	///<summary>
	///	Utility class for accessing various Layout Settings
	///</summary>
	public class LayoutUtil
	{
		///<summary>
		///	When working with the Presentation Layer for Sitecore, you can specify a Datasource as a property 
		///	for a control.  However, accessing the item that is set brings a bit of a challenge.  Hopefully this fixes that.
		///</summary>
		///<param name = "database">Sitecore Database to Use.  Usually From Context.</param>
		///<param name = "device">Sitecore Device to Use.  Usually from Context.</param>
		///<param name = "item">Item to get the Layout Renderings from.</param>
		///<param name = "sublayoutItem">
		///	Item reference to the Sublayout or Webcontrol that you would like to use for getting the datasource from.
		///</param>
		///<returns>Datasource Item.  If no Datasource Item is listed in the layout properties, returns Null.</returns>
		///<exception cref = "ItemNotFoundException">Thrown when Sitecore can't find a specified datasource item entry.</exception>
		public static Item GetDatasourceFromControl(Database database, DeviceItem device, Item item, Item sublayoutItem)
		{
			Item datasourceItem = null;

			// Get the Layout definition from the current item  
			string rend = item.Fields["__renderings"].Value;
			LayoutDefinition layout = LayoutDefinition.Parse(rend);
			// Get the current device definition  
			DeviceDefinition deviceDef = layout.GetDevice(device.ID.ToString());
			// Get the sublayout to find
			Item mySublayout = database.GetItem(sublayoutItem.ID);
			// Get the definition for the sublayout  
			RenderingDefinition rendering = deviceDef.GetRendering(mySublayout.ID.ToString());

			if (!String.IsNullOrEmpty(rendering.Datasource))
			{
				datasourceItem = database.GetItem(rendering.Datasource);
				if (datasourceItem == null)
				{
					throw new ItemNotFoundException("Could not find datasource item at " + rendering.Datasource);
				}
			}

			return datasourceItem;
		}
	}
}