using Sitecore.Data.Items;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.ItemUrlHelper
{
public partial class ItemUrlHelperEnvironmentItem : CustomItem
{

public static readonly string TemplateId = "{202F3A68-59F1-4378-9818-655AE6185A1D}";


#region Boilerplate CustomItem Code

public ItemUrlHelperEnvironmentItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator ItemUrlHelperEnvironmentItem(Item innerItem)
{
	return innerItem != null ? new ItemUrlHelperEnvironmentItem(innerItem) : null;
}

public static implicit operator Item(ItemUrlHelperEnvironmentItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTextField EnvironmentName
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Environment Name"]);
	}
}


public CustomTextField Host
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Host"]);
	}
}


#endregion //Field Instance Methods
}
}