using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.SharedSource.Commons.Utilities;

namespace Sitecore.SharedSource.Commons.ItemReference
{
	public partial class ItemReferenceObject
	{
		private string _itemGuid;
		private Item _item;

		public ItemReferenceObject(string itemGuid)
		{
			_itemGuid = itemGuid;
			_item = SitecoreItemFinder.GetItemFromCurrentDatabase(itemGuid);
		}

		public string Name
		{
			get
			{
				return _item.Name;
			}
		}

		public string Path
		{
			get
			{
				return _item.Paths.Path;
			}
		}

		public string Guid
		{
			get
			{
				return _itemGuid;
			}
		}

		public string URL
		{
			get
			{
				if(_item==null)
				{
					return null;
				}
				return LinkManager.GetItemUrl(_item);
			}
		}

		public Item InnerItem
		{
			get
			{
				return _item;
			}
		}
	}
}