using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class FileItem : CustomItem
{

public static readonly string TemplateId = "{611933AC-CE0C-4DDC-9683-F830232DB150}";


#region Boilerplate CustomItem Code

public FileItem(Item innerItem) : base(innerItem)
{

}

public static implicit operator FileItem(Item innerItem)
{
	return innerItem != null ? new FileItem(innerItem) : null;
}

public static implicit operator Item(FileItem customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


//Could not find Field Type for Blob


//Could not find Field Type for Format


//Could not find Field Type for Title


//Could not find Field Type for File Path


//Could not find Field Type for Keywords


//Could not find Field Type for Description


//Could not find Field Type for Extension


//Could not find Field Type for Mime Type


//Could not find Field Type for Size


#endregion //Field Instance Methods
}
}