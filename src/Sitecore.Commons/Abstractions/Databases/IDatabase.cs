using System;
using System.Collections.Generic;
using Lucene.Net.Search;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Archiving;
using Sitecore.Data.DataProviders;
using Sitecore.Data.Events;
using Sitecore.Data.Indexing;
using Sitecore.Data.Items;
using Sitecore.Data.Proxies;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.SharedSource.Commons.Abstractions.Items;
using Sitecore.SharedSource.Commons.Abstractions.Templates;
using Sitecore.Workflows;
using Version = Sitecore.Data.Version;

namespace Sitecore.SharedSource.Commons.Abstractions.Databases
{
	public interface IDatabase
	{
		IItemFactory ItemFactory { get; }
		IDatabaseFactory DatabaseFactory { get; }
		ITemplateItemFactory TemplateItemFactory { get; }

		IItem CreateItemPath(string path);
		IItem CreateItemPath(string path, TemplateItem template);
		IItem CreateItemPath(string path, TemplateItem folderTemplate, TemplateItem itemTemplate);
		
		IItem GetItem(ID itemId);
		IItem GetItem(ID itemId, Language language);
		IItem GetItem(ID itemId, Language language, Version version);
		IItem GetItem(string path);
		IItem GetItem(string path, Language language);
		IItem GetItem(string path, Language language, Version version);
		IItem GetItem(DataUri uri);
		
		IItem SelectSingleItem(string query);
		
		IItem SelectSingleItemUsingXPath(string query);
		
		IItem GetRootItem();
		IItem GetRootItem(Language language);

		IndexSearcher GetSearcher(string name);

		bool CleanupDatabase();
	
		DataProvider[] GetDataProviders();
		
		long GetDataSize(int minEntitySize, int maxEntitySize);
		
		LanguageCollection GetLanguages();
		
		ITemplateItem GetTemplate(ID templateId);
		
		ITemplateItem GetTemplate(string fullName);
		
		IEnumerable<IItem> SelectItems(string query);
		
		ItemList SelectItemsUsingXPath(string query);

		AliasResolver Aliases { get; }

		List<string> ArchiveNames { get; }

		DataArchives Archives { get; }

		DatabaseCaches Caches { get; }

		string ConnectionStringName { get; set; }

		DataManager DataManager { get; }

		DatabaseEngines Engines { get; }

		bool HasContentItem { get; }

		string Icon { get; set; }

		DataIndexes Indexes { get; }

		ItemRecords Items { get; }

		Language[] Languages { get; }

		BranchRecords Branches { get; }

		BranchRecords Masters { get; }

		string Name { get; }

		DatabaseProperties Properties { get; }

		bool Protected { get; set; }

		bool ProxiesEnabled { get; set; }

		ProxyDataProvider ProxyDataProvider { get; set; }

		bool PublishVirtualItems { get; set; }

		bool ReadOnly { get; set; }

		ResourceItems Resources { get; }

		bool SecurityEnabled { get; set; }

		IItem SitecoreItem { get; }

		TemplateRecords Templates { get; }

		IWorkflowProvider WorkflowProvider { get; set; }

		event EventHandler<ConstructedEventArgs> Constructed;
	}
} 