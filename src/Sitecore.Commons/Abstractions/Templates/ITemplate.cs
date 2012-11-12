using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Sitecore.Data;
using Sitecore.Data.Templates;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public interface ITemplate
	{
		ID ID { get; }

		string Name { get; }

		string FullName { get; }

		ID[] BaseIDs { get; }

		NameValueCollection CustomValues { get; }

		string Icon { get; }

		ID StandardValueHolderId { get; }

		void AddDefault(ID fieldID, string value);
		bool ContainsField(ID fieldID);
		bool DescendsFrom(ID templateId);
		bool DescendsFrom(Template template);
		bool DescendsFromOrEquals(ID templateId);

		IEnumerable<ITemplate> GetDescendants();

		TemplateList GetBaseTemplates();
		Hashtable GetDefaults();
	
		TemplateField GetField(ID fieldID);
		TemplateField GetField(string fieldName);
		TemplateField[] GetFields();
		TemplateField[] GetFields(bool includeBaseFields);
		
		TemplateSection GetSection(ID sectionID);
		TemplateSection GetSection(string sectionName);
		TemplateSection[] GetSections();
		
		TemplateChangeList GetTemplateChangeList(Template target);

		ITemplateFactory TemplateFactory { get; }
	}
} 