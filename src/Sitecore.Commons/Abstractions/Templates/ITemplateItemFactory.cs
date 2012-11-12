using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public interface ITemplateItemFactory
	{
		ITemplateItem BuildTemplateItem(TemplateItem templateItem);
		IEnumerable<ITemplateItem> BuildTemplateItems(IEnumerable<TemplateItem> templateItems);
	}
}