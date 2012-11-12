using System.Web;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.SharedSource.Commons.Abstractions.Items;
using Sitecore.Web;

namespace Sitecore.SharedSource.Commons.Abstractions.Managers
{
	public interface ILinkManager
	{
		IItemFactory ItemFactory
		{
			get;
		}

		bool AddAspxExtension
		{
			get;
		}

		bool AlwaysIncludeServerUrl
		{
			get;
		}

		LanguageEmbedding LanguageEmbedding
		{
			get;
		}

		LanguageLocation LanguageLocation
		{
			get;
		}

		LinkProvider Provider
		{
			get;
		}

		LinkProviderCollection Providers
		{
			get;
		}

		bool ShortenUrls
		{
			get;
		}

		bool UseDisplayName
		{
			get;
		}

		string ExpandDynamicLinks(string text);
		string ExpandDynamicLinks(string text, bool resolveSites);
		UrlOptions GetDefaultUrlOptions();
		string GetDynamicUrl(Item item);
		string GetDynamicUrl(Item item, LinkUrlOptions options);
		string GetItemUrl(Item item);
		string GetItemUrl(Item item, UrlOptions options);
		bool IsDynamicLink(string linkText);
		DynamicLink ParseDynamicLink(string linkText);
		RequestUrl ParseRequestUrl(HttpRequest request);
	}
}