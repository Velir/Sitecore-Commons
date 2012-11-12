using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Sitecore.Data;
using Sitecore.Data.Templates;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public class TemplateWrapper : ITemplate
	{
		private readonly Template _template;

		public TemplateWrapper(Template template)
		{
			_template = template;
		}

		public virtual ITemplateFactory TemplateFactory
		{
			get { return Templates.TemplateFactory.Instance; }
		}

		public virtual void AddDefault(ID fieldID, string value)
		{
			_template.AddDefault(fieldID, value);
		}

		public virtual bool ContainsField(ID fieldID)
		{
			return _template.ContainsField(fieldID);
		}

		public virtual bool DescendsFrom(ID templateId)
		{
			return _template.DescendsFrom(templateId);
		}

		public virtual bool DescendsFrom(Template template)
		{
			return _template.DescendsFrom(template);
		}

		public virtual bool DescendsFromOrEquals(ID templateId)
		{
			return _template.DescendsFromOrEquals(templateId);
		}

		public virtual TemplateList GetBaseTemplates()
		{
			return _template.GetBaseTemplates();
		}

		public virtual Hashtable GetDefaults()
		{
			return _template.GetDefaults();
		}

		public virtual IEnumerable<ITemplate> GetDescendants()
		{
			return TemplateFactory.BuildTemplates(_template.GetDescendants());
		}

		public virtual TemplateField GetField(ID fieldID)
		{
			return _template.GetField(fieldID);
		}

		public virtual TemplateField GetField(string fieldName)
		{
			return _template.GetField(fieldName);
		}

		public virtual TemplateField[] GetFields()
		{
			return _template.GetFields();
		}

		public virtual TemplateField[] GetFields(bool includeBaseFields)
		{
			return _template.GetFields(includeBaseFields);
		}

		public virtual TemplateSection GetSection(ID sectionID)
		{
			return _template.GetSection(sectionID);
		}

		public virtual TemplateSection GetSection(string sectionName)
		{
			return _template.GetSection(sectionName);
		}

		public virtual TemplateSection[] GetSections()
		{
			return _template.GetSections();
		}

		public virtual TemplateChangeList GetTemplateChangeList(Template target)
		{
			return _template.GetTemplateChangeList(target);
		}

		public virtual ID[] BaseIDs
		{
			get { return _template.BaseIDs; }
		}

		public virtual NameValueCollection CustomValues
		{
			get { return _template.CustomValues; }
		}

		public virtual string FullName
		{
			get { return _template.FullName; }
		}

		public virtual string Icon
		{
			get { return _template.Icon; }
		}

		public virtual ID ID
		{
			get { return _template.ID; }
		}

		public virtual string Name
		{
			get { return _template.Name; }
		}

		public virtual ID StandardValueHolderId
		{
			get { return _template.StandardValueHolderId; }
		}
	}
}
