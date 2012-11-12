using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.EditForm
{
public partial class EditFormModuleItem : CustomItem
{

public static readonly string TemplateId = "{C8886860-B74F-4C1C-99B9-9AA531A94B54}";


#region Boilerplate CustomItem Code

public EditFormModuleItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator EditFormModuleItem(Item innerItem)
{
	return innerItem != null ? new EditFormModuleItem(innerItem) : null;
}

public static implicit operator Item(EditFormModuleItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}