using System;
using System.Collections.Generic;
using System.Text;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// 	Utilities for dealing with languages and multilingual content in Sitecore
	/// </summary>
	public class LanguageUtil
	{
		#region Item Language Methods

		/// <summary>
		/// 	Checks to see if the passed in item has a version in the specified language.
		/// </summary>
		/// <param name = "db">The Sitecore db to use.</param>
		/// <param name = "language">The language to check for.</param>
		/// <param name = "itemGuid">The guid of the item to check.</param>
		/// <returns>true if the item contains a version in the specified language, false otherwise.</returns>
		/// <exception cref = "ArgumentNullException">db is null</exception>
		/// <exception cref = "ArgumentNullException">language is null</exception>
		/// <exception cref = "ArgumentException">itemGuid is null or empty</exception>
		public static bool CheckItemForLanguage(string itemGuid, Language language, Database db)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}

			if (language == null)
			{
				throw new ArgumentNullException("language");
			}

			if (String.IsNullOrEmpty(itemGuid))
			{
				throw new ArgumentException("itemGuid");
			}

			//Try and get the item in the specified language
			Item langItem = db.GetItem(itemGuid, language);
			if (langItem == null) return false;

			//Return if there are any versions in the specified language
			return langItem.Versions.GetVersions().Length > 0;
		}

		/// <summary>
		/// 	Gets the available languages for an item.
		/// </summary>
		/// <param name = "item">The item.</param>
		/// <param name = "db">The db.</param>
		/// <returns>A list of languages for which the item has a version for.</returns>
		public static List<Language> GetAvailableLanguagesForItem(Item item, Database db)
		{
			List<Language> languages = new List<Language>();

			//Loop through all of the available languages and check to see if the passed in item
			// has versions for each, if so add them to the return list.
			foreach (Language language in LanguageManager.GetLanguages(db))
			{
				if (CheckItemForLanguage(item.ID.ToString(), language, db))
				{
					languages.Add(language);
				}
			}
			return languages;
		}

		#endregion

		#region Language Retrieval Methods

		/// <summary>
		/// 	Tries to get the passed in language, if it is not found the default
		/// 	language will be returned.
		/// </summary>
		/// <param name = "languageName">Name of the language.</param>
		/// <param name = "fallBackToDefaultLanguage">if set to <c>true</c> the default language will be returned if any problems occur 
		/// 	in retrieving the passed in language.</param>
		/// <returns>
		/// 	The specified language if it is found, the default language otherwise.
		/// </returns>
		public static Language GetLanguage(string languageName, bool fallBackToDefaultLanguage)
		{
			//If the passed in language name is empty, then depending on if we are
			// falling back to the default language or not, either return the default or null
			if (string.IsNullOrEmpty(languageName))
			{
				return fallBackToDefaultLanguage ? LanguageManager.DefaultLanguage : null;
			}

			//Try and get the language, then depending on if it should fall back,
			// it will either return the default language or whatever was returned from the 
			// LanguageManager.GetLanguage call, which could possibly be null.
			Language test = LanguageManager.GetLanguage(languageName);
			if (fallBackToDefaultLanguage && test == null)
			{
				return LanguageManager.DefaultLanguage;
			}
			else
			{
				return test;
			}
		}

		#endregion

		#region Language Switching Methods

		/// <summary>
		/// 	Switches the current language in Sitecore.Context
		/// </summary>
		/// <param name = "languageName">Name of the language to switch to.</param>
		/// <param name = "fallBackToDefaultLanguage">if set to <c>true</c> the default language will be returned if any problems occur 
		/// 	in retrieving the passed in language</param>
		public static void SwitchCurrentLanguage(string languageName, bool fallBackToDefaultLanguage)
		{
			Language language = GetLanguage(languageName, fallBackToDefaultLanguage);
			SwitchCurrentLanguage(language);
		}

		/// <summary>
		/// 	Switches the current language in Sitecore.Context
		/// </summary>
		/// <param name = "language">The language to switch to.</param>
		public static void SwitchCurrentLanguage(Language language)
		{
			//If the language is null fall back to the defualt language
			if (language == null) language = LanguageManager.DefaultLanguage;

			//Set the current language
			Context.Language = language;
		}

		/// <summary>
		/// 	Contructs a URL for a passed in URL that contains the parameter which will cause
		/// 	Sitecore to switch the current language.
		/// </summary>
		/// <param name = "baseUrl">The base URL.</param>
		/// <param name = "language">The language to switch to.</param>
		/// <returns>The passed in url with the addition of the language parameter.</returns>
		public static string GetLanguageUrlForUrl(string baseUrl, Language language)
		{
			return GetLanguageUrlForUrl(baseUrl, language.Name);
		}

		/// <summary>
		/// 	Contructs a URL for a passed in URL that contains the parameter which will cause
		/// 	Sitecore to switch the current language.
		/// </summary>
		/// <param name = "baseUrl">The base URL.</param>
		/// <param name = "languageName">The name of the language to switch to.</param>
		/// <returns>The passed in url with the addition of the language parameter.</returns>
		public static string GetLanguageUrlForUrl(string baseUrl, string languageName)
		{
			if (baseUrl == null) return string.Empty;
			if (string.IsNullOrEmpty(languageName)) return baseUrl;

			String sep = baseUrl.Contains(("?")) ? "&" : "?";
			StringBuilder url = new StringBuilder(baseUrl);
			url.Append(sep).Append("sc_lang=").Append(languageName);
			return url.ToString();
		}

		/// <summary>
		/// 	Contructs a URL for a passed in Item that contains the parameter which will cause
		/// 	Sitecore to switch the current language.
		/// </summary>
		/// <param name = "item">The item to link to.</param>
		/// <param name = "language">The language to switch to.</param>
		/// <returns>The url to the passed in item, with the addition of the language parameter.</returns>
		public static string GetLanguageUrlForItem(Item item, Language language)
		{
			return GetLanguageUrlForUrl(LinkManager.GetItemUrl(item), language);
		}

		#endregion
	}
}