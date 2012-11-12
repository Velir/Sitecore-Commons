using System.Collections.Generic;
using Sitecore.Data.Templates;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public interface ITemplateFactory
	{
		ITemplate BuildTemplate(Template template);
		IEnumerable<ITemplate> BuildTemplates(IEnumerable<Template> templates);
	}
}