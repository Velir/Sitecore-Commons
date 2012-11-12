using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Commons.Abstractions.Databases;
using Sitecore.SharedSource.Commons.Abstractions.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public class TemplateItemWrapper : ITemplateItem
	{
		private readonly TemplateItem _templateItem;

		public TemplateItemWrapper(TemplateItem templateItem)
		{
			_templateItem = templateItem;
		}

		public virtual IItemFactory ItemFactory
		{
			get { return Items.ItemFactory.Instance; }
		}

		public virtual IDatabaseFactory DatabaseFactory
		{
			get { return Databases.DatabaseFactory.Instance; }
		}

		public virtual ITemplateItemFactory TemplateItemFactory
		{
			get { return Templates.TemplateItemFactory.Instance; }
		}

		public virtual void BeginEdit()
		{
			_templateItem.BeginEdit();
		}

		public virtual void EndEdit()
		{
			_templateItem.EndEdit();
		}

		public virtual IDatabase Database
		{
			get { return DatabaseFactory.BuildDatabase(_templateItem.Database); }
		}

		public virtual string DisplayName
		{
			get { return _templateItem.DisplayName; }
		}

		public virtual string Icon
		{
			get { return _templateItem.Icon; }
		}

		public virtual ID ID
		{
			get { return _templateItem.ID; }
		}

		public virtual ISitecoreItem InnerItem
		{
			get { return ItemFactory.BuildItem(_templateItem.InnerItem); }
		}

		public virtual string Name
		{
			get { return _templateItem.Name; }
		}

		public virtual TemplateFieldItem AddField(string fieldName, string sectionName)
		{
			return _templateItem.AddField(fieldName, sectionName);
		}

		public virtual TemplateSectionItem AddSection(string sectionName)
		{
			return _templateItem.AddSection(sectionName);
		}

		public virtual TemplateSectionItem AddSection(string sectionName, bool allowInheritedSection)
		{
			return _templateItem.AddSection(sectionName, allowInheritedSection);
		}

		public virtual ISitecoreItem AddTo(Item item, string name)
		{
			return ItemFactory.BuildItem(_templateItem.AddTo(item, name));
		}

		public virtual ISitecoreItem CreateItemFrom(string name, Item parent)
		{
			return ItemFactory.BuildItem(_templateItem.CreateItemFrom(name, parent));
		}

		public virtual ISitecoreItem CreateStandardValues()
		{
			return ItemFactory.BuildItem(_templateItem.CreateStandardValues());
		}

		public virtual TemplateFieldItem GetField(ID fieldID)
		{
			return _templateItem.GetField(fieldID);
		}

		public virtual TemplateFieldItem GetField(string fieldName)
		{
			return _templateItem.GetField(fieldName);
		}

		public virtual TemplateSectionItem GetSection(ID sectionID)
		{
			return _templateItem.GetSection(sectionID);
		}

		public virtual TemplateSectionItem GetSection(ID sectionID, bool allowInheritedSection)
		{
			return _templateItem.GetSection(sectionID, allowInheritedSection);
		}

		public virtual TemplateSectionItem GetSection(string sectionName)
		{
			return _templateItem.GetSection(sectionName);
		}

		public virtual TemplateSectionItem[] GetSections()
		{
			return _templateItem.GetSections();
		}

		/* TODO - need to add lucene
		public virtual Lucene.Net.Search.Hits GetUsages()
		{
			return _templateItem.GetUsages();
		}
		*/

		public virtual void RemoveField(ID fieldID)
		{
			_templateItem.RemoveField(fieldID);
		}

		public virtual void RemoveField(string fieldName)
		{
			_templateItem.RemoveField(fieldName);
		}

		public virtual void RemoveSection(ID sectionID)
		{
			_templateItem.RemoveSection(sectionID);
		}

		public virtual void RemoveSection(string sectionName)
		{
			_templateItem.RemoveSection(sectionName);
		}

		public virtual IEnumerable<ITemplateItem> BaseTemplates
		{
			get { return TemplateItemFactory.BuildTemplateItems(_templateItem.BaseTemplates); }
		}

		public virtual TemplateFieldItem[] Fields
		{
			get { return _templateItem.Fields; }
		}

		public virtual string FullName
		{
			get { return _templateItem.FullName; }
		}

		public virtual string Key
		{
			get { return _templateItem.Key; }
		}

		public virtual TemplateFieldItem[] OwnFields
		{
			get { return _templateItem.OwnFields; }
		}

		public virtual ISitecoreItem StandardValues
		{
			get { return ItemFactory.BuildItem(_templateItem.StandardValues); }
		}
	}
}