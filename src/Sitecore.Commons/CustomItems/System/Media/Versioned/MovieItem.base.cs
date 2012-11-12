using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class MovieItem : CustomItem
{

public static readonly string TemplateId = "{374D3A99-C098-4CD2-8FBC-DC2D1CA1C904}";

#region Inherited Base Templates

private readonly FileItem _FileItem;
public FileItem File { get { return _FileItem; } }

#endregion

#region Boilerplate CustomItem Code

public MovieItem(Item innerItem) : base(innerItem)
{
	_FileItem = new FileItem(innerItem);

}

public static implicit operator MovieItem(Item innerItem)
{
	return innerItem != null ? new MovieItem(innerItem) : null;
}

public static implicit operator Item(MovieItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


#endregion //Field Instance Methods
}
}