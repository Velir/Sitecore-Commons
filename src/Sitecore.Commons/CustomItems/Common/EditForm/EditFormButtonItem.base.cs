using Sitecore.Data.Items;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.EditForm
{
public partial class EditFormButtonItem : CustomItem
{

public static readonly string TemplateId = "{09980A14-FEDA-4987-A86D-7BAAFA02738A}";


#region Boilerplate CustomItem Code

public EditFormButtonItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator EditFormButtonItem(Item innerItem)
{
	return innerItem != null ? new EditFormButtonItem(innerItem) : null;
}

public static implicit operator Item(EditFormButtonItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTextField Title
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Title"]);
	}
}


public CustomTextField Description
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Description"]);
	}
}


public CustomTextField Click
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Click"]);
	}
}


public CustomTextField ImagePath
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Image Path"]);
	}
}


#endregion //Field Instance Methods
}
}