using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class PdfItem : CustomItem
{

public static readonly string TemplateId = "{CC80011D-8EAE-4BFC-84F1-67ECD0223E9E}";

#region Inherited Base Templates

private readonly DocumentItem _DocumentItem;
public DocumentItem Document { get { return _DocumentItem; } }

#endregion

#region Boilerplate CustomItem Code

public PdfItem(Item innerItem) : base(innerItem)
{
	_DocumentItem = new DocumentItem(innerItem);

}

public static implicit operator PdfItem(Item innerItem)
{
	return innerItem != null ? new PdfItem(innerItem) : null;
}

public static implicit operator Item(PdfItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}