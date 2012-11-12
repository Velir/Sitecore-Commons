using CustomItemGenerator.Fields.SimpleTypes;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class FlashItem : CustomItem
{

public static readonly string TemplateId = "{2E16714D-8406-4445-98B7-CD70F658611B}";

#region Inherited Base Templates

private readonly MovieItem _MovieItem;
public MovieItem Movie { get { return _MovieItem; } }

#endregion

#region Boilerplate CustomItem Code

public FlashItem(Item innerItem) : base(innerItem)
{
	_MovieItem = new MovieItem(innerItem);

}

public static implicit operator FlashItem(Item innerItem)
{
	return innerItem != null ? new FlashItem(innerItem) : null;
}

public static implicit operator Item(FlashItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTextField Dimensions
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Dimensions"]);
	}
}


public CustomTextField Width
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Width"]);
	}
}


public CustomTextField Height
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Height"]);
	}
}


public CustomTextField Version
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Version"]);
	}
}


public CustomTextField FrameRate
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["FrameRate"]);
	}
}


public CustomTextField FrameCount
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["FrameCount"]);
	}
}


#endregion //Field Instance Methods
}
}