using Sitecore.Data.Items;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.ItemUrlHelper
{
public partial class ItemUrlHelperItem : CustomItem
{

public static readonly string TemplateId = "{E9099DA5-2F2D-4F1B-9B3E-E1AFC013CE14}";


#region Boilerplate CustomItem Code

public ItemUrlHelperItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator ItemUrlHelperItem(Item innerItem)
{
	return innerItem != null ? new ItemUrlHelperItem(innerItem) : null;
}

public static implicit operator Item(ItemUrlHelperItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomCheckboxField UseLanguages
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Use Languages"]);
	}
}


public CustomCheckboxField UseEnvironments
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Use Environments"]);
	}
}


#endregion //Field Instance Methods
}
}