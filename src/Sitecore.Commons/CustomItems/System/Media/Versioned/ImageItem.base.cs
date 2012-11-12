using CustomItemGenerator.Fields.SimpleTypes;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class ImageItem : CustomItem
{

public static readonly string TemplateId = "{C97BA923-8009-4858-BDD5-D8BE5FCCECF7}";

#region Inherited Base Templates

private readonly FileItem _FileItem;
public FileItem File { get { return _FileItem; } }
private readonly MediaItem _mediaItem;
public MediaItem MediaItem { get { return _mediaItem; } }

	#endregion

#region Boilerplate CustomItem Code

public ImageItem(Item innerItem) : base(innerItem)
{
	_FileItem = new FileItem(innerItem);
	_mediaItem = innerItem;
}

public static implicit operator ImageItem(Item innerItem)
{
	return innerItem != null ? new ImageItem(innerItem) : null;
}

public static implicit operator Item(ImageItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTextField Alt
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Alt"]);
	}
}
public CustomTextField Width
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Width"]);
	}
}
public CustomTextField Dimensions
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Dimensions"]);
	}
}
public CustomTextField Height
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Height"]);
	}
}

#endregion //Field Instance Methods
}
}