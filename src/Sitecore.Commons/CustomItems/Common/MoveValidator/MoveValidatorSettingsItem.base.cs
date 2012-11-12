using Sitecore.Data.Items;
using CustomItemGenerator.Fields.ListTypes;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.MoveValidator
{
public partial class MoveValidatorSettingsItem : CustomItem
{

public static readonly string TemplateId = "{A36700BE-5519-44A8-9399-C1508AB9C5EC}";


#region Boilerplate CustomItem Code

public MoveValidatorSettingsItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator MoveValidatorSettingsItem(Item innerItem)
{
	return innerItem != null ? new MoveValidatorSettingsItem(innerItem) : null;
}

public static implicit operator Item(MoveValidatorSettingsItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTreeListField AppliedLocations
{
	get
	{
		return new CustomTreeListField(InnerItem, InnerItem.Fields["Applied Locations"]);
	}
}


public CustomTextField AdminAlertMessage
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Admin Alert Message"]);
	}
}


public CustomTextField UserAlertShortMessage
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["User Alert Short Message"]);
	}
}


public CustomTextField UserAlertLongMessage
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["User Alert Long Message"]);
	}
}


#endregion //Field Instance Methods
}
}