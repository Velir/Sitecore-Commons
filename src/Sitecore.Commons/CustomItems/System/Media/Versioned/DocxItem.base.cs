using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class DocxItem : CustomItem
{

public static readonly string TemplateId = "{F57FB07D-332A-4934-AA67-0A629C5396E2}";

#region Inherited Base Templates

private readonly DocumentItem _DocumentItem;
public DocumentItem Document { get { return _DocumentItem; } }

#endregion

#region Boilerplate CustomItem Code

public DocxItem(Item innerItem) : base(innerItem)
{
	_DocumentItem = new DocumentItem(innerItem);

}

public static implicit operator DocxItem(Item innerItem)
{
	return innerItem != null ? new DocxItem(innerItem) : null;
}

public static implicit operator Item(DocxItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}