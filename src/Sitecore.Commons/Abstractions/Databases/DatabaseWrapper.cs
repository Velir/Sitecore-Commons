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
	public class DatabaseWrapper : IDatabase
	{
		private readonly Database _database;
	
		public DatabaseWrapper(Database database)
		{
			_database = database;
		}

		public virtual IItemFactory ItemFactory
		{
			get { return Abstractions.Items.ItemFactory.Instance; }
		}

		public virtual IDatabaseFactory DatabaseFactory
		{
			get { return Databases.DatabaseFactory.Instance; }
		}

		public virtual ITemplateItemFactory TemplateItemFactory
		{
			get { return Abstractions.Templates.TemplateItemFactory.Instance; }
		}

		public virtual bool CleanupDatabase()
		{
			return _database.CleanupDatabase();
		}

		public virtual IItem CreateItemPath(string path)
		{
			return ItemFactory.BuildItem(_database.CreateItemPath(path));
		}

		public virtual IItem CreateItemPath(string path, TemplateItem template)
		{
			return ItemFactory.BuildItem(_database.CreateItemPath(path, template));
		}

		public virtual IItem CreateItemPath(string path, TemplateItem folderTemplate, TemplateItem itemTemplate)
		{
			return ItemFactory.BuildItem(_database.CreateItemPath(path, folderTemplate, itemTemplate));
		}

		public virtual DataProvider[] GetDataProviders()
		{
			return _database.GetDataProviders();
		}

		public virtual long GetDataSize(int minEntitySize, int maxEntitySize)
		{
			return _database.GetDataSize(minEntitySize, maxEntitySize);
		}

		public virtual IItem GetItem(ID itemId)
		{
			return ItemFactory.BuildItem(_database.GetItem(itemId));
		}

		public virtual IItem GetItem(ID itemId, Language language)
		{
			return ItemFactory.BuildItem(_database.GetItem(itemId, language));
		}

		public virtual IItem GetItem(ID itemId, Language language, Version version)
		{
			return ItemFactory.BuildItem(_database.GetItem(itemId, language, version));
		}

		public virtual IItem GetItem(string path)
		{
			return ItemFactory.BuildItem(_database.GetItem(path));
		}

		public virtual IItem GetItem(string path, Language language)
		{
			return ItemFactory.BuildItem(_database.GetItem(path, language));
		}

		public virtual IItem GetItem(string path, Language language, Version version)
		{
			return ItemFactory.BuildItem(_database.GetItem(path, language, version));
		}

		public virtual IItem GetItem(DataUri uri)
		{
			return ItemFactory.BuildItem(_database.GetItem(uri));
		}

		public virtual LanguageCollection GetLanguages()
		{
			return _database.GetLanguages();
		}

		public virtual IItem GetRootItem()
		{
			return ItemFactory.BuildItem(_database.GetRootItem());
		}

		public virtual IItem GetRootItem(Language language)
		{
			return ItemFactory.BuildItem(_database.GetRootItem(language));
		}
		
		public virtual IndexSearcher GetSearcher(string name)
		{
			return _database.GetSearcher(name);
		}

		public virtual ITemplateItem GetTemplate(ID templateId)
		{
			return TemplateItemFactory.BuildTemplateItem(_database.GetTemplate(templateId));
		}

		public virtual ITemplateItem GetTemplate(string fullName)
		{
			return TemplateItemFactory.BuildTemplateItem(_database.GetTemplate(fullName));
		}

		public virtual IEnumerable<IItem> SelectItems(string query)
		{
			return ItemFactory.BuildItems(_database.SelectItems(query));
		}

		public virtual ItemList SelectItemsUsingXPath(string query)
		{
			return _database.SelectItemsUsingXPath(query);
		}

		public virtual IItem SelectSingleItem(string query)
		{
			return ItemFactory.BuildItem(_database.SelectSingleItem(query));
		}

		public virtual IItem SelectSingleItemUsingXPath(string query)
		{
			return ItemFactory.BuildItem(_database.SelectSingleItemUsingXPath(query));
		}

		public virtual AliasResolver Aliases
		{
			get { return _database.Aliases; }
		}

		public virtual List<string> ArchiveNames
		{
			get { return _database.ArchiveNames; }
		}

		public virtual DataArchives Archives
		{
			get { return _database.Archives; }
		}

		public virtual DatabaseCaches Caches
		{
			get { return _database.Caches; }
		}

		public virtual string ConnectionStringName
		{
			get { return _database.ConnectionStringName; }
			set { _database.ConnectionStringName = value; }
		}

		public virtual DataManager DataManager
		{
			get { return _database.DataManager; }
		}

		public virtual DatabaseEngines Engines
		{
			get { return _database.Engines; }
		}

		public virtual bool HasContentItem
		{
			get { return _database.HasContentItem; }
		}

		public virtual string Icon
		{
			get { return _database.Icon; }
			set { _database.Icon = value; }
		}

		public virtual DataIndexes Indexes
		{
			get { return _database.Indexes; }
		}

		public virtual ItemRecords Items
		{
			get { return _database.Items; }
		}

		public virtual Language[] Languages
		{
			get { return _database.Languages; }
		}

		public virtual BranchRecords Branches
		{
			get { return _database.Branches; }
		}

		public virtual BranchRecords Masters
		{
			get { return _database.Masters; }
		}

		public virtual string Name
		{
			get { return _database.Name; }
		}

		public virtual DatabaseProperties Properties
		{
			get { return _database.Properties; }
		}

		public virtual bool Protected
		{
			get { return _database.Protected; }
			set { _database.Protected = value; }
		}

		public virtual bool ProxiesEnabled
		{
			get { return _database.ProxiesEnabled; }
			set { _database.ProxiesEnabled = value; }
		}

		public virtual ProxyDataProvider ProxyDataProvider
		{
			get { return _database.ProxyDataProvider; }
			set { _database.ProxyDataProvider = value; }
		}

		public virtual bool PublishVirtualItems
		{
			get { return _database.PublishVirtualItems; }
			set { _database.PublishVirtualItems = value; }
		}

		public virtual bool ReadOnly
		{
			get { return _database.ReadOnly; }
			set { _database.ReadOnly = value; }
		}

		public virtual ResourceItems Resources
		{
			get { return _database.Resources; }
		}

		public virtual bool SecurityEnabled
		{
			get { return _database.SecurityEnabled; }
			set { _database.SecurityEnabled = value; }
		}

		public virtual IItem SitecoreItem
		{
			get { return ItemFactory.BuildItem(_database.SitecoreItem); }
		}

		public virtual TemplateRecords Templates
		{
			get { return _database.Templates; }
		}

		public virtual IWorkflowProvider WorkflowProvider
		{
			get { return _database.WorkflowProvider; }
			set { _database.WorkflowProvider = value; }
		}

		public virtual event EventHandler<ConstructedEventArgs> Constructed
		{
			add { _database.Constructed += value; }
			remove { _database.Constructed -= value; }
		}
	}
}