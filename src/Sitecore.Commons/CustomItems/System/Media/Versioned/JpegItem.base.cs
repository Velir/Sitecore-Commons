using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class JpegItem : CustomItem
{

public static readonly string TemplateId = "{EB3FB96C-D56B-4AC9-97F8-F07B24BB9BF7}";

#region Inherited Base Templates

private readonly ImageItem _ImageItem;
public ImageItem Image { get { return _ImageItem; } }

#endregion

#region Boilerplate CustomItem Code

public JpegItem(Item innerItem) : base(innerItem)
{
	_ImageItem = new ImageItem(innerItem);

}

public static implicit operator JpegItem(Item innerItem)
{
	return innerItem != null ? new JpegItem(innerItem) : null;
}

public static implicit operator Item(JpegItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


//Could not find Field Type for Artist


//Could not find Field Type for Copyright


//Could not find Field Type for DateTime


//Could not find Field Type for ImageDescription


//Could not find Field Type for Make


//Could not find Field Type for Model


//Could not find Field Type for Software


#endregion //Field Instance Methods
}
}