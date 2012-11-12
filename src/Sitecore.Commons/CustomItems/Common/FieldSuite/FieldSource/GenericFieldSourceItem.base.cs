using Sitecore.Data.Items;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.FieldSuite.FieldSource
{
public partial class GenericFieldSourceItem : CustomItem
{

public static readonly string TemplateId = "{C88B0602-F1FE-4746-8D2B-D0E23256ED3E}";


#region Boilerplate CustomItem Code

public GenericFieldSourceItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator GenericFieldSourceItem(Item innerItem)
{
	return innerItem != null ? new GenericFieldSourceItem(innerItem) : null;
}

public static implicit operator Item(GenericFieldSourceItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomCheckboxField DeepQuery
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Deep Query"]);
	}
}

public CustomCheckboxField RootItem
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Deep Query"]);
	}
}

public CustomCheckboxField IncludedTemplates
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Deep Query"]);
	}
}

public CustomCheckboxField ExcludedTemplates
{
	get
	{
		return new CustomCheckboxField(InnerItem, InnerItem.Fields["Excluded Templates"]);
	}
}

public CustomTextField Parameters
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Parameters"]);
	}
}


#endregion //Field Instance Methods
}
}