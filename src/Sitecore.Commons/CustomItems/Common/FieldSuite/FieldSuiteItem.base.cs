using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.FieldSuite
{
public partial class FieldSuiteItem : CustomItem
{

public static readonly string TemplateId = "{451561BA-A8CD-4343-AF0D-FED456889B47}";


#region Boilerplate CustomItem Code

public FieldSuiteItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator FieldSuiteItem(Item innerItem)
{
	return innerItem != null ? new FieldSuiteItem(innerItem) : null;
}

public static implicit operator Item(FieldSuiteItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}