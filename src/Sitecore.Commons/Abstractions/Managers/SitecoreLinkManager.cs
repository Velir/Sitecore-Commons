using System.Web;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.SharedSource.Commons.Abstractions.Items;
using Sitecore.Web;

namespace Sitecore.SharedSource.Commons.Abstractions.Managers
{
	public class SitecoreLinkManager : ILinkManager
	{
		public virtual IItemFactory ItemFactory
		{
			get { return Items.ItemFactory.Instance; }
		}

		public bool AddAspxExtension
		{
			get { return LinkManager.AddAspxExtension; }
		}

		public bool AlwaysIncludeServerUrl
		{
			get { return LinkManager.AlwaysIncludeServerUrl; }
		}

		public LanguageEmbedding LanguageEmbedding
		{
			get { return LinkManager.LanguageEmbedding; }
		}

		public LanguageLocation LanguageLocation
		{
			get { return LinkManager.LanguageLocation; }
		}

		public LinkProvider Provider
		{
			get { return LinkManager.Provider; }
		}

		public LinkProviderCollection Providers
		{
			get { return LinkManager.Providers; }
		}

		public bool ShortenUrls
		{
			get { return LinkManager.ShortenUrls; }
		}

		public bool UseDisplayName
		{
			get { return LinkManager.UseDisplayName; }
		}

		public string ExpandDynamicLinks(string text)
		{
			return LinkManager.ExpandDynamicLinks(text, false);
		}

		public string ExpandDynamicLinks(string text, bool resolveSites)
		{
			return LinkManager.ExpandDynamicLinks(text, resolveSites);
		}

		public UrlOptions GetDefaultUrlOptions()
		{
			return LinkManager.GetDefaultUrlOptions();
		}

		public string GetDynamicUrl(Item item)
		{
			return LinkManager.GetDynamicUrl(item, LinkUrlOptions.Empty);
		}

		public string GetDynamicUrl(Item item, LinkUrlOptions options)
		{
			return LinkManager.GetDynamicUrl(item, options);
		}

		public string GetItemUrl(Item item)
		{
			return LinkManager.GetItemUrl(item, LinkManager.GetDefaultUrlOptions());
		}

		public string GetItemUrl(Item item, UrlOptions options)
		{
			return LinkManager.GetItemUrl(item, options);
		}

		public bool IsDynamicLink(string linkText)
		{
			return LinkManager.IsDynamicLink(linkText);
		}

		public DynamicLink ParseDynamicLink(string linkText)
		{
			return LinkManager.ParseDynamicLink(linkText);
		}

		public RequestUrl ParseRequestUrl(HttpRequest request)
		{
			return LinkManager.ParseRequestUrl(request);
		}

		#region Singleton implementation
		private SitecoreLinkManager()
		{ }

		public static SitecoreLinkManager Instance { get { return Nested._instance; } }

		private class Nested
		{
			static Nested() { }
			internal static readonly SitecoreLinkManager _instance = new SitecoreLinkManager();
		}
		#endregion

	}
}