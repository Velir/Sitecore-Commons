using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class AudioItem : CustomItem
{

public static readonly string TemplateId = "{E19A2758-F802-4FDC-B497-5FF7B3BAC54B}";

#region Inherited Base Templates

private readonly FileItem _FileItem;
public FileItem File { get { return _FileItem; } }

#endregion

#region Boilerplate CustomItem Code

public AudioItem(Item innerItem) : base(innerItem)
{
	_FileItem = new FileItem(innerItem);

}

public static implicit operator AudioItem(Item innerItem)
{
	return innerItem != null ? new AudioItem(innerItem) : null;
}

public static implicit operator Item(AudioItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}