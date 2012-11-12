using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class ZipItem : CustomItem
{

public static readonly string TemplateId = "{1743421C-0D7F-4870-9F7B-70E6F0B63308}";

#region Inherited Base Templates

private readonly FileItem _FileItem;
public FileItem File { get { return _FileItem; } }

#endregion

#region Boilerplate CustomItem Code

public ZipItem(Item innerItem) : base(innerItem)
{
	_FileItem = new FileItem(innerItem);

}

public static implicit operator ZipItem(Item innerItem)
{
	return innerItem != null ? new ZipItem(innerItem) : null;
}

public static implicit operator Item(ZipItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


//Could not find Field Type for File Count


#endregion //Field Instance Methods
}
}