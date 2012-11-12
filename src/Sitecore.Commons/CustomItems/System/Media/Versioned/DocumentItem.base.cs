using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class DocumentItem : CustomItem
{

public static readonly string TemplateId = "{2A130D0C-A2A9-4443-B418-917F857BF6C9}";

#region Inherited Base Templates

private readonly FileItem _FileItem;
public FileItem File { get { return _FileItem; } }

#endregion

#region Boilerplate CustomItem Code

public DocumentItem(Item innerItem) : base(innerItem)
{
	_FileItem = new FileItem(innerItem);

}

public static implicit operator DocumentItem(Item innerItem)
{
	return innerItem != null ? new DocumentItem(innerItem) : null;
}

public static implicit operator Item(DocumentItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}