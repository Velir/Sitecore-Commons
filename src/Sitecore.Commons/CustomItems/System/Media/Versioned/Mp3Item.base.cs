using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Media.Versioned
{
public partial class Mp3Item : CustomItem
{

public static readonly string TemplateId = "{2449F96D-620E-4E8A-A3E0-D354F78BBD73}";

#region Inherited Base Templates

private readonly AudioItem _AudioItem;
public AudioItem Audio { get { return _AudioItem; } }

#endregion

#region Boilerplate CustomItem Code

public Mp3Item(Item innerItem) : base(innerItem)
{
	_AudioItem = new AudioItem(innerItem);

}

public static implicit operator Mp3Item(Item innerItem)
{
	return innerItem != null ? new Mp3Item(innerItem) : null;
}

public static implicit operator Item(Mp3Item customItem)
{
	return customItem != null ? customItem.InnerItem : null;
}

#endregion //Boilerplate CustomItem Code


#region Field Instance Methods


//Could not find Field Type for Artist


//Could not find Field Type for Song Title


//Could not find Field Type for Album


//Could not find Field Type for Track Number


//Could not find Field Type for Year


//Could not find Field Type for Comment


#endregion //Field Instance Methods
}
}