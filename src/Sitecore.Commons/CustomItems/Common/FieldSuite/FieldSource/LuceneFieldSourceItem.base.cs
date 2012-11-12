using Sitecore.Data.Items;
using CustomItemGenerator.Fields.SimpleTypes;

namespace Sitecore.SharedSource.Commons.CustomItems.Common.FieldSuite.FieldSource
{
public partial class LuceneFieldSourceItem : CustomItem
{

public static readonly string TemplateId = "{19DC6B0C-EE6D-4580-BB31-4688EDD0A8AE}";

#region Inherited Base Templates

private readonly GenericFieldSourceItem _GenericFieldSourceItem;
public GenericFieldSourceItem GenericFieldSource { get { return _GenericFieldSourceItem; } }

#endregion

#region Boilerplate CustomItem Code

public LuceneFieldSourceItem(Item innerItem) : base(innerItem)
{
	_GenericFieldSourceItem = new GenericFieldSourceItem(innerItem);

}

public static implicit operator LuceneFieldSourceItem(Item innerItem)
{
	return innerItem != null ? new LuceneFieldSourceItem(innerItem) : null;
}

public static implicit operator Item(LuceneFieldSourceItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


public CustomTextField IndexName
{
	get
	{
		return new CustomTextField(InnerItem, InnerItem.Fields["Index Name"]);
	}
}


#endregion //Field Instance Methods
}
}