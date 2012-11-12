using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class DocItem : CustomItem
{

public static readonly string TemplateId = "{3DB3A3CA-A0A9-4228-994B-F70C8E99A1CE}";

#region Inherited Base Templates

private readonly DocumentItem _DocumentItem;
public DocumentItem Document { get { return _DocumentItem; } }

#endregion

#region Boilerplate CustomItem Code

public DocItem(Item innerItem) : base(innerItem)
{
	_DocumentItem = new DocumentItem(innerItem);

}

public static implicit operator DocItem(Item innerItem)
{
	return innerItem != null ? new DocItem(innerItem) : null;
}

public static implicit operator Item(DocItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}