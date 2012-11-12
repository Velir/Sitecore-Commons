using System.Collections.Generic;
using Sitecore.Data.Templates;

namespace Sitecore.SharedSource.Commons.Abstractions.Templates
{
	public class TemplateFactory : ITemplateFactory
	{
		public ITemplate BuildTemplate(Template template)
		{
			return new TemplateWrapper(template);
		}

		public IEnumerable<ITemplate> BuildTemplates(IEnumerable<Template> templates)
		{
			foreach (Template template in templates)
			{
				yield return BuildTemplate(template);
			}
		}

		#region Singleton implementation
		private TemplateFactory()
		{ }

		public static TemplateFactory Instance { get { return Nested._instance; } }

		private class Nested
		{
			static Nested() { }
			internal static readonly TemplateFactory _instance = new TemplateFactory();
		}
		#endregion

	}
}
