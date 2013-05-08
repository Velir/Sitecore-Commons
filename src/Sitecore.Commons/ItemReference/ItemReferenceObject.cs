using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.SharedSource.Commons.Utilities;

namespace Sitecore.SharedSource.Commons.ItemReference
{
	public partial class ItemReferenceObject
	{
		private readonly string _itemGuid;
		private Item _item;

		public ItemReferenceObject(string itemGuid)
		{
			_itemGuid = itemGuid;
		}

		public string Name
		{
			get
			{
				if(_item == null)
				{
					_item = SitecoreItemFinder.GetItemFromCurrentDatabase(_itemGuid);
				}
				if (_item == null)
				{
					return null;
				}
				return _item.Name;
			}
		}

		public string Path
		{
			get
			{
				if (_item == null)
				{
					_item = SitecoreItemFinder.GetItemFromCurrentDatabase(_itemGuid);
				}
				if (_item == null)
				{
					return null;
				}
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
				if (_item == null)
				{
					_item = SitecoreItemFinder.GetItemFromCurrentDatabase(_itemGuid);
				}
				if(_item == null)
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
				if (_item == null)
				{
					_item = SitecoreItemFinder.GetItemFromCurrentDatabase(_itemGuid);
				}
				return _item;
			}
		}
	}
}