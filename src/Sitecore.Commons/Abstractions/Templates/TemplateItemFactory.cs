using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public class TemplateItemFactory : ITemplateItemFactory
	{
		public ITemplateItem BuildTemplateItem(TemplateItem templateItem)
		{
			return new TemplateItemWrapper(templateItem);
		}

		public IEnumerable<ITemplateItem> BuildTemplateItems(IEnumerable<TemplateItem> templateItems)
		{
			foreach (TemplateItem templateItem in templateItems)
			{
				yield return BuildTemplateItem(templateItem);
			}
		}

		#region Singleton implementation
		private TemplateItemFactory()
		{ }

		public static TemplateItemFactory Instance { get { return Nested._instance; } }

		private class Nested
		{
			static Nested() { }
			internal static readonly TemplateItemFactory _instance = new TemplateItemFactory();
		}
		#endregion

	}
}