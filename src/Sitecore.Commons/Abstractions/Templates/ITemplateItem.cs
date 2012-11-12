using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Commons.Abstractions.Databases;
using Sitecore.SharedSource.Commons.Abstractions.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public interface ITemplateItem
	{
		ID ID { get; }

		IDatabase Database { get; }

		IItemFactory ItemFactory { get; }
		ITemplateItemFactory TemplateItemFactory { get; }
		IDatabaseFactory DatabaseFactory { get; }

		string DisplayName { get; }
		string Icon { get; }
		string Name { get; }
		string FullName { get; }
		string Key { get; }


		ISitecoreItem InnerItem { get; }
		ISitecoreItem StandardValues { get; }
		ISitecoreItem AddTo(Item item, string name);
		ISitecoreItem CreateItemFrom(string name, Item parent);
		ISitecoreItem CreateStandardValues();

		IEnumerable<ITemplateItem> BaseTemplates { get; }

		TemplateFieldItem[] Fields { get; }
		TemplateFieldItem[] OwnFields { get; }
		TemplateFieldItem AddField(string fieldName, string sectionName);
		TemplateFieldItem GetField(ID fieldID);
		TemplateFieldItem GetField(string fieldName);
				
		TemplateSectionItem AddSection(string sectionName);
		TemplateSectionItem AddSection(string sectionName, bool allowInheritedSection);
		TemplateSectionItem GetSection(ID sectionID);
		TemplateSectionItem GetSection(ID sectionID, bool allowInheritedSection);
		TemplateSectionItem GetSection(string sectionName);
		TemplateSectionItem[] GetSections();
		
		void RemoveField(ID fieldID);
		void RemoveField(string fieldName);
		void RemoveSection(ID sectionID);
		void RemoveSection(string sectionName);

		void BeginEdit();
		void EndEdit();
	}
}