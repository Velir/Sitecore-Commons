using Sitecore.Data.Items;
using CustomItemGenerator.Fields.ListTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.FieldTypeSelector.Generic
{
public partial class GenericTemplateItem : CustomItem
{

public static readonly string TemplateId = "{8DA8E7C4-EB4F-4245-A9C3-A3F0EE5EAA75}";


#region Boilerplate CustomItem Code

public GenericTemplateItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator GenericTemplateItem(Item innerItem)
{
	return innerItem != null ? new GenericTemplateItem(innerItem) : null;
}

public static implicit operator Item(GenericTemplateItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTreeListField ExampleField
{
	get
	{
		return new CustomTreeListField(InnerItem, InnerItem.Fields["Example Field"]);
	}
}


#endregion //Field Instance Methods
}
}