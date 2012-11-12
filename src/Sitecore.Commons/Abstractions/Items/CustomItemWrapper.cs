using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Commons.Abstractions.Databases;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public class CustomItemWrapper : ICustomItem
	{
		private readonly CustomItem _customItem;
		
		public CustomItemWrapper(CustomItem customItem)
		{
			if (customItem == null)
			{
				throw new ArgumentNullException("customItem");
			}
			_customItem = customItem;
		}

		public IItemFactory ItemFactory
		{
			get { return Items.ItemFactory.Instance; }
		}

		public IDatabaseFactory DatabaseFactory
		{
			get { return Databases.DatabaseFactory.Instance; }
		}

		public virtual void BeginEdit()
		{
			_customItem.BeginEdit();
		}

		public virtual void EndEdit()
		{
			_customItem.EndEdit();
		}

		public virtual IDatabase Database
		{
			get { return DatabaseFactory.BuildDatabase(_customItem.Database); }
		}

		public virtual string DisplayName
		{
			get { return _customItem.DisplayName; }
		}

		public virtual string Icon
		{
			get { return _customItem.Icon; }
		}

		public virtual ID ID
		{
			get { return _customItem.ID; }
		}

		public virtual IItem InnerItem
		{
			get { return ItemFactory.BuildItem(_customItem.InnerItem); }
		}

		public virtual string Name
		{
			get { return _customItem.Name; }
		}

		public virtual string this[ID fieldID]
		{
			get { return _customItem[fieldID]; }
			set { _customItem[fieldID] = value; }
		}

		public virtual string this[int fieldIndex]
		{
			get { return _customItem[fieldIndex]; }
		}

		public virtual string this[string fieldName]
		{
			get { return _customItem[fieldName]; }
			set { _customItem[fieldName] = value; }
		}
	}
}